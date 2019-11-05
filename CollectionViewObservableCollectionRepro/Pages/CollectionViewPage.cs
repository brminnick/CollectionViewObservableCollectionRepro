using Xamarin.Forms;

namespace CollectionViewObservableCollectionRepro
{
    class CollectionViewPage : BaseContentPage<BaseListViewModel>
    {
        public CollectionViewPage(string title, BaseListViewModel viewModel) : base(title, viewModel)
        {
            var collectionView = new CollectionView
            {
                ItemTemplate = new ListDataTemplate()
            };
            collectionView.SetBinding(CollectionView.ItemsSourceProperty, nameof(ObservableCollectionViewModel.DataList));

            var refreshView = new RefreshView
            {
                Content = collectionView
            };
            refreshView.SetBinding(RefreshView.IsRefreshingProperty, nameof(ObservableCollectionViewModel.IsRefreshing));
            refreshView.SetBinding(RefreshView.CommandProperty, nameof(ObservableCollectionViewModel.PullToRefreshCommand));

            Content = refreshView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Content is RefreshView refreshView
                && refreshView.Content is CollectionView collectionView
                && IsNullOrEmpty(collectionView.ItemsSource))
            {
                refreshView.IsRefreshing = true;
            }
        }
    }
}
