using System.Reflection;

namespace ShowCaseToDo
{
	public partial class App : Application
    {
        public const string DataFilePath = @"D:\Data\data.json";
		public App()
		{
			InitializeComponent();
			MainPage = new MainPage();
		}
	}
}
