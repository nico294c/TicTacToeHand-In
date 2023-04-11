using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_Intro.Model;
using MAUI_Intro.View;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;

namespace MAUI_Intro.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly HubConnection _connection;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PlayerTurnMessage))]
        private int playerTurn = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PlayerTurnMessage))]
        private int turnCounter = 1;

        public ObservableCollection<TicTacToeModel> TicTacToeModels { get; set; } = new ObservableCollection<TicTacToeModel>();

        public MainPageViewModel()
        {
            SetupModels();

            /*Hard coded IP for API using my laptop at home network.
              Pretty sure we use proxy server, so I'll probably have to change this from time to time*/
            _connection = new HubConnectionBuilder()
                .WithUrl("http://192.168.0.54:5265/game")
                .Build();

            _connection.On<string, int>("MoveReceived", (displayText, index) =>
            {
                foreach (TicTacToeModel model in TicTacToeModels)
                {
                    if (model.Index == index)
                        model.DisplayText = displayText;
                    
                }
                PlayerTurn = PlayerTurn == 0 ? 1 : 0;
                TurnCounter++;
            });

            _connection.On("ResetReceived", () =>
            {
                foreach (TicTacToeModel model in TicTacToeModels)
                {
                    if (model.DisplayText != null)
                    {
                        model.DisplayText = null;
                    }
                }
                TurnCounter = 1;
                PlayerTurn = 0;
            });

            Task.Run(() =>
            {
                Application.Current.Dispatcher.Dispatch(async () =>
                await _connection.StartAsync());
            });
        }

        private void SetupModels()
        {
            TicTacToeModels.Clear();

            for (int i = 0; i < 9; i++)
            {
                TicTacToeModels.Add(new TicTacToeModel(i));
            }
        }

        
        public string PlayerTurnMessage
        {
            get
            {
                if (PlayerTurn == 0 && TurnCounter == 0)
                {
                    return "Tic tac toe! X starts!";
                }
                else if (PlayerTurn == 0)
                {
                    return $"It is X's turn! (Turn: {TurnCounter})";
                }
                else
                {
                    return $"It is O's turn! (Turn: {TurnCounter})";
                }
            }
        }

        [RelayCommand]
        public async void Reset()
        {
            await _connection.InvokeAsync("SendReset");
        }

        [RelayCommand]
        public async void Move(TicTacToeModel pressedItem)
        {
            if(PlayerTurn == 0)
            {
                //Reaches API endpoint "SendMove", and sends 'X' as argument alongside the index of the pressed square.
                await _connection.InvokeCoreAsync("SendMove", args: new object[] {'X', pressedItem.Index});
            }
            else
            {
                //Reaches API endpoint "SendMove", and sends 'O' as argument alongside the index of the pressed square.
                await _connection.InvokeCoreAsync("SendMove", args: new object[] {'O', pressedItem.Index});
            }
        }

        [RelayCommand]
        async Task Tap()
        {
            await Shell.Current.GoToAsync($"{nameof(HighscorePage)}?TurnCounter={TurnCounter}");
        }
    }
}
