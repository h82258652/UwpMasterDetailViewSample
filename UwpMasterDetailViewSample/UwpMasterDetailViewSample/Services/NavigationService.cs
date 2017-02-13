using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Views;
using UwpMasterDetailViewSample.Controls;
using UwpMasterDetailViewSample.ViewModels;
using WinRTXamlToolkit.Controls.Extensions;

namespace UwpMasterDetailViewSample.Services
{
    public class NavigationService : INavigationService
    {
        public const string UnknownPageKey = "-- UNKNOWN --";

        protected readonly Dictionary<string, Type> PagesByKey = new Dictionary<string, Type>();

        public string CurrentPageKey
        {
            get
            {
                lock (PagesByKey)
                {
                    var masterDetailView = Window.Current.Content.GetFirstDescendantOfType<MasterDetailView>();
                    if (masterDetailView == null)
                    {
                        return UnknownPageKey;
                    }

                    var frame = masterDetailView.IsDisplayDetail ? DetailFrame : MasterFrame;
                    if (frame.Content == null)
                    {
                        return UnknownPageKey;
                    }

                    var currentType = frame.Content.GetType();
                    if (PagesByKey.ContainsValue(currentType) == false)
                    {
                        return UnknownPageKey;
                    }

                    var item = PagesByKey.FirstOrDefault(temp => temp.Value == currentType);
                    return item.Key;
                }
            }
        }

        protected virtual Frame DetailFrame => Window.Current.Content?.GetDescendantsOfType<Frame>().FirstOrDefault(temp => temp.Name == "DetailFrame");

        protected virtual Frame MasterFrame => Window.Current.Content?.GetDescendantsOfType<Frame>().FirstOrDefault(temp => temp.Name == "MasterFrame");

        public void Configure(string key, Type pageType)
        {
            lock (PagesByKey)
            {
                if (PagesByKey.ContainsKey(key))
                {
                    throw new ArgumentException("This key is already used: " + key);
                }

                if (PagesByKey.Any(temp => temp.Value == pageType))
                {
                    throw new ArgumentException("This type is already configured with key " + PagesByKey.First(temp => temp.Value == pageType).Key);
                }

                PagesByKey.Add(key, pageType);
            }
        }

        public void GoBack()
        {
            var masterDetailView = Window.Current.Content.GetFirstDescendantOfType<MasterDetailView>();
            if (masterDetailView == null)
            {
                return;
            }

            var frame = masterDetailView.IsDisplayDetail ? DetailFrame : MasterFrame;
            if (frame != null && frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            lock (PagesByKey)
            {
                if (PagesByKey.ContainsKey(pageKey) == false)
                {
                    throw new ArgumentException($"No such page: {pageKey}. Did you forget to call NavigationService.Configure?", nameof(pageKey));
                }

                switch (pageKey)
                {
                    case ViewModelLocator.DetailViewKey:
                        DetailFrame?.Navigate(PagesByKey[pageKey], parameter);
                        break;
                }
            }
        }
    }
}