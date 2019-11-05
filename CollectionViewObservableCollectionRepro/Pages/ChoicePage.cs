using System.Threading.Tasks;
using Xamarin.Forms;

namespace CollectionViewObservableCollectionRepro
{
    class ChoicePage : BaseContentPage<BaseViewModel>
    {
        public ChoicePage() : base("Choose List Type", new BaseViewModel())
        {
            const string listViewListTitle = "ListView Page List<T>, ";
            const string listViewObservableCollectionTitle = "ListView Page ObservableCollection<T>, ";
            const string collectionViewListTitle = "CollectionView Page List<T>, ";
            const string collectionViewObservableCollectionTitle = "CollectionView Page ObservableCollection<T>, ";

            const string configureAwaitTrue = "ConfigureAwait(true)";
            const string configureAwaitFalse = "ConfigureAwait(false)";

            var keyLabel = new Label
            {
                FontAttributes = FontAttributes.Italic,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Text = "Note: Red Buttons will open a page that crashes the app"
            };

            var listViewListPageTrueButton = new ChoiceButton(Color.Green, Color.White, listViewListTitle + configureAwaitTrue);
            listViewListPageTrueButton.Clicked += async (s, e) => await NavigateToPage(new ListViewPage(listViewListTitle + configureAwaitTrue, new ListViewModel(true)));

            var listViewListPageFalseButton = new ChoiceButton(Color.Green, Color.White, listViewListTitle + configureAwaitFalse);
            listViewListPageFalseButton.Clicked += async (s, e) => await NavigateToPage(new ListViewPage(listViewListTitle + configureAwaitFalse, new ListViewModel(false)));

            var listViewObservableCollectionPageTrueButton = new ChoiceButton(Color.Green, Color.White, listViewObservableCollectionTitle + configureAwaitTrue);
            listViewObservableCollectionPageTrueButton.Clicked += async (s, e) => await NavigateToPage(new ListViewPage(listViewObservableCollectionTitle + configureAwaitTrue, new ObservableCollectionViewModel(true)));

            var listViewObservableColletionPageFalseButton = new ChoiceButton(Color.Green, Color.White, listViewObservableCollectionTitle + configureAwaitFalse);
            listViewObservableColletionPageFalseButton.Clicked += async (s, e) => await NavigateToPage(new ListViewPage(listViewObservableCollectionTitle + configureAwaitFalse, new ObservableCollectionViewModel(false)));


            var collectionViewListPageTrueButton = new ChoiceButton(Color.Green, Color.White, collectionViewListTitle + configureAwaitTrue);
            collectionViewListPageTrueButton.Clicked += async (s, e) => await NavigateToPage(new CollectionViewPage(collectionViewListTitle + configureAwaitTrue, new ListViewModel(true)));

            var collectionViewListPageFalseButton = new ChoiceButton(Color.Green, Color.White, collectionViewListTitle + configureAwaitFalse);
            collectionViewListPageFalseButton.Clicked += async (s, e) => await NavigateToPage(new CollectionViewPage(collectionViewListTitle + configureAwaitFalse, new ListViewModel(false)));

            var collectionViewObservableCollectionPageTrueButton = new ChoiceButton(Color.Green, Color.White, collectionViewObservableCollectionTitle + configureAwaitTrue);
            collectionViewObservableCollectionPageTrueButton.Clicked += async (s, e) => await NavigateToPage(new CollectionViewPage(collectionViewListTitle + configureAwaitTrue, new ObservableCollectionViewModel(true)));

            var collectionViewObservableCollectionPageFalseButton = new ChoiceButton(Color.DarkRed, Color.White, collectionViewObservableCollectionTitle + configureAwaitFalse);
            collectionViewObservableCollectionPageFalseButton.Clicked += async (s, e) => await NavigateToPage(new CollectionViewPage(collectionViewListTitle + configureAwaitFalse, new ObservableCollectionViewModel(false)));

            var stackLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    keyLabel,
                    listViewListPageTrueButton,
                    listViewListPageFalseButton,
                    listViewObservableCollectionPageTrueButton,
                    listViewObservableColletionPageFalseButton,
                    collectionViewListPageTrueButton,
                    collectionViewListPageFalseButton,
                    collectionViewObservableCollectionPageTrueButton,
                    collectionViewObservableCollectionPageFalseButton
                }
            };

            Padding = new Thickness(10);

            Content = new ScrollView { Content = stackLayout };
        }

        Task NavigateToPage(Page page) => Device.InvokeOnMainThreadAsync(() => Navigation.PushAsync(page));

        class ChoiceButton : Button
        {
            public ChoiceButton(Color backgroundColor, Color textColor, string text) =>
                (BackgroundColor, TextColor, Text, FontSize) = (backgroundColor, textColor, text, 12);
        }
    }
}
