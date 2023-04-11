namespace MAUI_Intro;
using MAUI_Intro.ViewModel;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		
	}
}

