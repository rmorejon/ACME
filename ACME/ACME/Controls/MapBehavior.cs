namespace ACME.Controls
{
    using System.Collections.Generic;

    using Xamarin.Forms;
    using Xamarin.Forms.Maps;

    public class MapBehavior : Map
    {
        #region Static Fields and Constants

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.CreateAttached(
            "ItemsSource",
            typeof(IEnumerable<Pin>),
            typeof(MapBehavior),
            default(IEnumerable<Pin>),
            BindingMode.Default,
            null,
            OnItemsSourceChanged);

        #endregion

        #region Properties Public

        public IEnumerable<Pin> ItemsSource
        {
            get => (IEnumerable<Pin>)this.GetValue(ItemsSourceProperty);
            set => this.SetValue(ItemsSourceProperty, value);
        }

        #endregion

        #region Methods

        private static void OnItemsSourceChanged(BindableObject view, object oldValue, object newValue)
        {
            if (view is MapBehavior mapBehavior)
            {
                mapBehavior.Pins.Clear();
                var items = (IList<Pin>)newValue;

                foreach (var pin in items) mapBehavior.Pins.Add(pin);
            }
        }

        #endregion
    }
}