using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xaml.Interactivity;
using WinRTXamlToolkit.Controls.Extensions;

namespace UwpMasterDetailViewSample.Behaviors
{
    public class GoBackAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                try
                {
                    var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
                    navigationService.GoBack();
                    return true;
                }
                catch (ActivationException)
                {
                }
            }

            var frame = (sender as DependencyObject)?.GetFirstAncestorOfType<Frame>() ?? Window.Current.Content?.GetFirstDescendantOfType<Frame>();
            if (frame != null && frame.CanGoBack)
            {
                frame.GoBack();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}