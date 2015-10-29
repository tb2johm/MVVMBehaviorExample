using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace MVVMBehaviorExample.Views.Behaviors
{
    public static class CloseBehavior
    {
        /* Dependency property that attached to the window */
        public static readonly DependencyProperty ClosingProperty = DependencyProperty.RegisterAttached(
            "Closing", typeof (ICommand), typeof (CloseBehavior), new UIPropertyMetadata(OnClosingChanged));

        public static ICommand GetClosing(DependencyObject obj)
        {
            return (ICommand) obj.GetValue(ClosingProperty);
        }

        public static void SetClosing(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ClosingProperty, value);
        }



        private static void OnClosingChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var window = target as Window;

            if (window == null || e.NewValue == null) return;

            window.Closing += window_Closed;
        }

        static void window_Closed(object sender, EventArgs e)
        {
            var closeCommand = GetClosing(sender as Window);

            if (closeCommand == null) return;

            var canClose = closeCommand.CanExecute(null);
            if (canClose)
            {
                closeCommand.Execute(null);
                return;
            }

            var ce = e as CancelEventArgs;

            if (ce == null) return;
            ce.Cancel = true;
        }
    }
}
