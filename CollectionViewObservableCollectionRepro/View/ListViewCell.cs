using Xamarin.Forms;

namespace CollectionViewObservableCollectionRepro
{
    public class ListViewCell : TextCell
    {
        public ListViewCell() => this.SetBinding(TextProperty, nameof(ListModel.Number));
    }
}
