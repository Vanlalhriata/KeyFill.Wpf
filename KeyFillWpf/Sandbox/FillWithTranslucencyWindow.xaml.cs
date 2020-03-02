using System.Windows;
using System.Windows.Input;

namespace Sandbox
{
    public partial class FillWithTranslucencyWindow : Window
    {
        public FillWithTranslucencyWindow()
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
