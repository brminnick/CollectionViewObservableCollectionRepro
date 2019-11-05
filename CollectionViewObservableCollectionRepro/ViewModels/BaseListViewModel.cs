using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices.MVVM;

namespace CollectionViewObservableCollectionRepro
{
    abstract class BaseListViewModel : BaseViewModel
    {
        bool _isRefreshing;
        ICommand? _pullToRefreshCommand;

        public ICommand PullToRefreshCommand => _pullToRefreshCommand ??= new AsyncCommand(ExecutePullToRefreshCommand);

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        protected abstract Task ExecutePullToRefreshCommand();

        protected static async IAsyncEnumerable<ListModel> GenerateListModels()
        {
            for (int i = 0; i < 30; i++)
            {
                await Task.Delay(200).ConfigureAwait(false);
                yield return new ListModel(i);
            }
        }
    }
}
