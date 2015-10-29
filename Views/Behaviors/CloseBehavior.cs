using System;
using System.Windows;
using System.Windows.Input;

namespace MVVMBehaviorExample.Views.Behaviors
{
    public static class CloseBehavior
    {
        /* Dependency property that attached to the window */
        public static readonly DependencyProperty ClosedProperty = DependencyProperty.RegisterAttached(
            "Closed", typeof (ICommand), typeof (CloseBehavior), new UIPropertyMetadata(OnClosedChanged));

        public static ICommand GetClosed(DependencyObject obj)
        {
            return (ICommand) obj.GetValue(ClosedProperty);
        }

        public static void SetClosed(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ClosedProperty, value);
        }



        private static void OnClosedChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var window = target as Window;

            if (window == null || e.NewValue == null) return;

            window.Closed += window_Closed;
        }

        static void window_Closed(object sender, EventArgs e)
        {
            var closeCommand = GetClosed(sender as Window);

            if (closeCommand == null) return;

            closeCommand.Execute(null);
        }
    }
}
