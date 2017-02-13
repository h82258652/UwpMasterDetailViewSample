using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using UwpMasterDetailViewSample.Views;

namespace UwpMasterDetailViewSample.ViewModels
{
    public class ViewModelLocator
    {
        public const string AboutViewKey = "About";

        public const string CommentViewKey = "Comment";

        public const string DetailViewKey = "Detail";

        public const string MasterViewKey = "Master";

        public const string NoDetailViewKey = "NoDetail";

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
            navigationService.Configure(MasterViewKey, typeof(MasterView));
            navigationService.Configure(DetailViewKey, typeof(DetailView));
            navigationService.Configure(AboutViewKey, typeof(AboutView));
            navigationService.Configure(CommentViewKey, typeof(CommentView));
            navigationService.Configure(NoDetailViewKey, typeof(NoDetailView));
            return navigationService;
        }
    }
}