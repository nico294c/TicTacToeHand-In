using MAUI_Intro.ViewModel;

namespace MAUI_Intro;

public partial class App : Application
{

    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
