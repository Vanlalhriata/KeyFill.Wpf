using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Sandbox
{
    public partial class KeyWithTranslucencyWindow : Window
    {
        private Window fillWindow;

        public KeyWithTranslucencyWindow()
        {
            InitializeComponent();

            // In a real application you would want a better way to refer to the fill window,
            // possibly using dependency injection or MVVMbindings
            fillWindow = new FillWithTranslucencyWindow();
            fillWindow.Show();

            this.Loaded += keyWindow_Loaded;
        }

        private void keyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Width = fillWindow.ActualWidth;
            Height = fillWindow.ActualHeight;

            var fillPanel = (Visual)fillWindow.FindName("fillPanel");

            Background = new VisualBrush(fillPanel);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
