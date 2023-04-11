using MAUI_Intro.View;

namespace MAUI_Intro;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(HighscorePage), typeof(HighscorePage));
	}
}