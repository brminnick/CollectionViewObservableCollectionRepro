using System.Collections;
using Xamarin.Forms;

namespace CollectionViewObservableCollectionRepro
{
    abstract class BaseContentPage<T> : ContentPage where T : BaseViewModel
    {
        protected BaseContentPage(in string title, in T viewModel)
        {
            BindingContext = ViewModel = viewModel;
            Title = title;
        }

        protected T ViewModel { get; }

        protected bool IsNullOrEmpty(in IEnumerable? enumerable) => !enumerable?.GetEnumerator().MoveNext() ?? true;
    }
}
