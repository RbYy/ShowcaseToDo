using System.Reflection;

namespace ShowCaseToDo
{
	public partial class App : Application
    {
        public const string DataFilePath = @"C:\Data\data.json";
		public App()
		{
			InitializeComponent();
			MainPage = new MainPage();
		}
	}
}
