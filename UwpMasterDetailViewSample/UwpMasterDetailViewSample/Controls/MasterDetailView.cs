using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpMasterDetailViewSample.Controls
{
    [TemplateVisualState(GroupName = AdaptiveStatesGroupName, Name = BothStateName)]
    [TemplateVisualState(GroupName = AdaptiveStatesGroupName, Name = MasterStateName)]
    [TemplateVisualState(GroupName = AdaptiveStatesGroupName, Name = DetailStateName)]
    public class MasterDetailView : Control
    {
        public static readonly DependencyProperty DetailContentProperty = DependencyProperty.Register(nameof(DetailContent), typeof(object), typeof(MasterDetailView), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty DetailContentTemplateProperty = DependencyProperty.Register(nameof(DetailContentTemplate), typeof(DataTemplate), typeof(MasterDetailView), new PropertyMetadata(default(DataTemplate)));

        public static readonly DependencyProperty IsDisplayDetailProperty = DependencyProperty.Register(nameof(IsDisplayDetail), typeof(bool), typeof(MasterDetailView), new PropertyMetadata(default(bool), OnIsDisplayDetailChanged));

        public static readonly DependencyProperty MasterContentProperty = DependencyProperty.Register(nameof(MasterContent), typeof(object), typeof(MasterDetailView), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty MasterContentTemplateProperty = DependencyProperty.Register(nameof(MasterContentTemplate), typeof(DataTemplate), typeof(MasterDetailView), new PropertyMetadata(default(DataTemplate)));

        public static readonly DependencyProperty MasterPaneWidthProperty = DependencyProperty.Register(nameof(MasterPaneWidth), typeof(GridLength), typeof(MasterDetailView), new PropertyMetadata(default(GridLength)));

        public static readonly DependencyProperty ViewStateProperty = DependencyProperty.Register(nameof(ViewState), typeof(MasterDetailViewState), typeof(MasterDetailView), new PropertyMetadata(MasterDetailViewState.Both, OnViewStateChanged));

        private const string AdaptiveStatesGroupName = "AdaptiveStates";

        private const string BothStateName = "Both";

        private const string DetailStateName = "Detail";

        private const string MasterStateName = "Master";

        public MasterDetailView()
        {
            DefaultStyleKey = typeof(MasterDetailView);

            SizeChanged += MasterDetailView_SizeChanged;
        }

        public object DetailContent
        {
            get
            {
                return GetValue(DetailContentProperty);
            }
            set
            {
                SetValue(DetailContentProperty, value);
            }
        }

        public DataTemplate DetailContentTemplate
        {
            get
            {
                return (DataTemplate)GetValue(DetailContentTemplateProperty);
            }
            set
            {
                SetValue(DetailContentTemplateProperty, value);
            }
        }

        public bool IsDisplayDetail
        {
            get
            {
                return (bool)GetValue(IsDisplayDetailProperty);
            }
            set
            {
                SetValue(IsDisplayDetailProperty, value);
            }
        }

        public object MasterContent
        {
            get
            {
                return GetValue(MasterContentProperty);
            }
            set
            {
                SetValue(MasterContentProperty, value);
            }
        }

        public DataTemplate MasterContentTemplate
        {
            get
            {
                return (DataTemplate)GetValue(MasterContentTemplateProperty);
            }
            set
            {
                SetValue(MasterContentTemplateProperty, value);
            }
        }

        public GridLength MasterPaneWidth
        {
            get
            {
                return (GridLength)GetValue(MasterPaneWidthProperty);
            }
            set
            {
                SetValue(MasterPaneWidthProperty, value);
            }
        }

        public MasterDetailViewState ViewState
        {
            get
            {
                return (MasterDetailViewState)GetValue(ViewStateProperty);
            }
            private set
            {
                SetValue(ViewStateProperty, value);
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateViewState();
        }

        private static void OnIsDisplayDetailChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (MasterDetailView)d;
            var value = (bool)e.NewValue;

            switch (obj.ViewState)
            {
                case MasterDetailViewState.Master:
                    if (value)
                    {
                        obj.ViewState = MasterDetailViewState.Detail;
                    }
                    break;

                case MasterDetailViewState.Detail:
                    if (value == false)
                    {
                        obj.ViewState = MasterDetailViewState.Master;
                    }
                    break;
            }
        }

        private static void OnViewStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (MasterDetailView)d;
            var value = (MasterDetailViewState)e.NewValue;

            if (Enum.IsDefined(typeof(MasterDetailViewState), value) == false)
            {
                throw new ArgumentOutOfRangeException(nameof(ViewState));
            }

            obj.UpdateViewState();
        }

        private void MasterDetailView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width >= 720)
            {
                ViewState = MasterDetailViewState.Both;
            }
            else
            {
                if (DetailContent == null && DetailContentTemplate == null)
                {
                    ViewState = MasterDetailViewState.Master;
                }
                else
                {
                    ViewState = IsDisplayDetail ? MasterDetailViewState.Detail : MasterDetailViewState.Master;
                }
            }
        }

        private void UpdateViewState()
        {
            VisualStateManager.GoToState(this, ViewState.ToString(), false);
        }
    }
}