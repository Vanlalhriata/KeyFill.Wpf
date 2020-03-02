using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Sandbox
{
    public partial class KeyWindow : Window
    {
        private Window fillWindow;

        public KeyWindow()
        {
            InitializeComponent();

            // In a real application you would want a better way to refer to the fill window,
            // possibly using dependency injection or MVVMbindings
            fillWindow = new FillWindow();
            fillWindow.Show();

            this.Loaded += keyWindow_Loaded;
        }

        private void keyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Width = fillWindow.ActualWidth;
            Height = fillWindow.ActualHeight;

            Background = new VisualBrush(fillWindow);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
