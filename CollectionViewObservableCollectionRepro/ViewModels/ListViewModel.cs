using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollectionViewObservableCollectionRepro
{
    class ListViewModel : BaseListViewModel
    {
        readonly bool _shouldContinueOnCapturedContext;

        List<ListModel> _dataList = new List<ListModel>();

        public ListViewModel(bool shouldContinueOnCapturedContext) =>
            _shouldContinueOnCapturedContext = shouldContinueOnCapturedContext;

        public List<ListModel> DataList
        {
            get => _dataList;
            set => SetProperty(ref _dataList, value);
        }

        protected override async Task ExecutePullToRefreshCommand()
        {
            var dataList = new List<ListModel>();

            try
            {
                await foreach(var model in GenerateListModels().ConfigureAwait(_shouldContinueOnCapturedContext))
                {
                    dataList.Add(model);
                }

                DataList = dataList;
            }
            finally
            {
                IsRefreshing = false;
            }
        }
    }
}
