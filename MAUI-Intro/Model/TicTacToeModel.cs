using CommunityToolkit.Mvvm.ComponentModel;


namespace MAUI_Intro.Model
{
    public partial class TicTacToeModel : ObservableObject
    {

        public TicTacToeModel(int index) 
        {
            this.Index = index;
        }

        public int Index { get; set; }

        [ObservableProperty]
        private string displayText;
    }
}
