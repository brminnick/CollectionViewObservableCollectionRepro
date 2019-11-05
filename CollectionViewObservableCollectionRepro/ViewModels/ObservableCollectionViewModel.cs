using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CollectionViewObservableCollectionRepro
{
    class ObservableCollectionViewModel : BaseListViewModel
    {
        readonly bool _shouldContinueOnCapturedContext;

        public ObservableCollectionViewModel(bool shouldContinueOnCapturedContext) =>
            _shouldContinueOnCapturedContext = shouldContinueOnCapturedContext;

        public ObservableCollection<ListModel> DataList { get; } = new ObservableCollection<ListModel>();

        protected override async Task ExecutePullToRefreshCommand()
        {
            DataList.Clear();

            try
            {
                await foreach (var model in GenerateListModels().ConfigureAwait(_shouldContinueOnCapturedContext))
                {
                    DataList.Add(model);
                }
            }
            finally
            {
                IsRefreshing = false;
            }
        }
    }
}
