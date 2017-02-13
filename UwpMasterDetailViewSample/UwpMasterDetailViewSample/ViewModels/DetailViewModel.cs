using GalaSoft.MvvmLight;
using UwpMasterDetailViewSample.Models;

namespace UwpMasterDetailViewSample.ViewModels
{
    public class DetailViewModel : ViewModelBase
    {
        private Article _article;

        public Article Article
        {
            get
            {
                return _article;
            }
            private set
            {
                Set(ref _article, value);
            }
        }

        public void Activate(object parameter)
        {
            Article = (Article)parameter;
        }
    }
}