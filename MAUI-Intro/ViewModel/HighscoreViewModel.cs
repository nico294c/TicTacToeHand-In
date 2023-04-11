
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_Intro.Model;
using System.Collections.ObjectModel;

namespace MAUI_Intro.ViewModel
{
    [QueryProperty("Highscore", "TurnCounter")]
    public partial class HighscoreViewModel : ObservableObject
    {
        public HighscoreViewModel()
        {
        }

        
        public ObservableCollection<HighscoreModel> Highscores { get; set; } = new();

        [ObservableProperty]
        int highscore;

        [ObservableProperty]
        string highscoreName;

        [RelayCommand]
        void Add()
        {
            if (string.IsNullOrWhiteSpace(HighscoreName))
                return;

            Highscores.Add(new HighscoreModel(HighscoreName, Highscore));

            HighscoreName = string.Empty;
        }

        [RelayCommand]
        void Delete(HighscoreModel model)
        {
            if(Highscores.Contains(model))
            {
                Highscores.Remove(model);
            }
        }
    }
}
