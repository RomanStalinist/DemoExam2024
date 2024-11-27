using Demo1.Data;
using Demo1.Views;

namespace Demo1
{
    public partial class App
    {
        public static readonly DemoExam2025Entities Context = new DemoExam2025Entities();

        private void Application_Startup(object sender, System.Windows.StartupEventArgs e)
        {
            var mWindow = new MainWindow();
            MainWindow = mWindow;
            mWindow.Show();
        }
    }
}