using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_Intro.Model
{
    public partial class HighscoreModel : ObservableObject
    {

        public HighscoreModel(string name, int score)
        {
            this.HighscoreName = name;
            this.Score = score;
        }

        [ObservableProperty]
        private int score;

        [ObservableProperty]
        private string highscoreName;

        public override string ToString()
        {
            return this.HighscoreName + " - Score: " + this.Score;
        }
    }
}
