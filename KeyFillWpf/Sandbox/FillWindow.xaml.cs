using System.Windows;
using System.Windows.Input;

namespace Sandbox
{
    public partial class FillWindow : Window
    {
        public FillWindow()
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
