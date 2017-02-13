using Windows.UI.Xaml.Navigation;
using UwpMasterDetailViewSample.ViewModels;

namespace UwpMasterDetailViewSample.Views
{
    public sealed partial class DetailView
    {
        public DetailView()
        {
            InitializeComponent();
        }

        public DetailViewModel ViewModel => (DetailViewModel)DataContext;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.Activate(e.Parameter);
        }
    }
}