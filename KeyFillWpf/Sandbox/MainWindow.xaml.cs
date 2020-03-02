using System.Windows;

namespace Sandbox
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void without_Click(object sender, RoutedEventArgs e)
        {
            var keyWindow = new KeyWindow();
            keyWindow.Show();
        }

        private void with_Click(object sender, RoutedEventArgs e)
        {
            var keyWindow = new KeyWithTranslucencyWindow();
            keyWindow.Show();
        }
    }
}
