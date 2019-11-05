using Xamarin.Forms;

namespace CollectionViewObservableCollectionRepro
{
    class ListViewPage : BaseContentPage<BaseListViewModel>
    {
        public ListViewPage(string title, BaseListViewModel viewModel) : base(title, viewModel)
        {
            var listView = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                IsPullToRefreshEnabled = true,
                ItemTemplate = new DataTemplate(typeof(ListViewCell))
            };
            listView.SetBinding(ListView.ItemsSourceProperty, nameof(ListViewModel.DataList));
            listView.SetBinding(ListView.IsRefreshingProperty, nameof(ListViewModel.IsRefreshing));
            listView.SetBinding(ListView.RefreshCommandProperty, nameof(ListViewModel.PullToRefreshCommand));

            Content = listView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(Content is ListView listView
                && IsNullOrEmpty(listView.ItemsSource))
            {
                listView.BeginRefresh();
            }
        }
    }
}
