using System.Reflection;

namespace ShowCaseToDo
{
	public partial class App : Application
    {
		public App()
		{
			InitializeComponent();
			MainPage = new MainPage();
		}
	}
}
