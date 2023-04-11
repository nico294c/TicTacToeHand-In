using MAUI_Intro.ViewModel;

namespace MAUI_Intro.View;

public partial class HighscorePage : ContentPage
{
	public HighscorePage(HighscoreViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}