using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using UwpMasterDetailViewSample.Models;

namespace UwpMasterDetailViewSample.ViewModels
{
    public class MasterViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private RelayCommand<Article> _articleClickCommand;

        public MasterViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Articles = Enumerable.Range(1, 100).Select(temp => new Article()
            {
                Title = $"Title {temp}",
                Content = $"Article Content {temp}"
            }).ToList();
        }

        public RelayCommand<Article> ArticleClickCommand
        {
            get
            {
                _articleClickCommand = _articleClickCommand ?? new RelayCommand<Article>(article =>
                {
                    _navigationService.NavigateTo(ViewModelLocator.DetailViewKey, article);
                });
                return _articleClickCommand;
            }
        }

        public IReadOnlyList<Article> Articles
        {
            get;
        }
    }
}