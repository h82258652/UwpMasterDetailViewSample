using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using UwpMasterDetailViewSample.Views;

namespace UwpMasterDetailViewSample.ViewModels
{
    public class ViewModelLocator
    {
        public const string DetailViewKey = "Detail";

        static ViewModelLocator()
        {
            var serviceLocator = new UnityServiceLocator(ConfigureUnityContainer());
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        public DetailViewModel Detail => ServiceLocator.Current.GetInstance<DetailViewModel>();

        public MasterViewModel Master => ServiceLocator.Current.GetInstance<MasterViewModel>();

        private static IUnityContainer ConfigureUnityContainer()
        {
            var unityContainer = new UnityContainer();

            unityContainer.RegisterInstance(CreateNavigationService());

            unityContainer.RegisterType<MasterViewModel>();
            unityContainer.RegisterType<DetailViewModel>();

            return unityContainer;
        }

        private static INavigationService CreateNavigationService()
        {
            var navigationService = new Services.NavigationService();
            navigationService.Configure(DetailViewKey, typeof(DetailView));
            return navigationService;
        }
    }
}