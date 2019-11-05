using Xamarin.Forms;

namespace CollectionViewObservableCollectionRepro
{
    public class ListDataTemplate : DataTemplate
    {
        public ListDataTemplate() : base(CreateTemplate)
        {
        }

        static Layout CreateTemplate()
        {
            var numberLabel = new Label
            {
                FontSize = 14,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
            };
            numberLabel.SetBinding(Label.TextProperty, nameof(ListModel.Number));

            var grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(20,GridUnitType.Absolute) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };

            grid.Children.Add(numberLabel, 0, 0);

            return grid;
        }
    }
}
