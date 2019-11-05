using Xamarin.Forms;

namespace CollectionViewObservableCollectionRepro
{
    public class App : Application
    {
        public App() => MainPage = new NavigationPage(new ChoicePage());
    }
}
