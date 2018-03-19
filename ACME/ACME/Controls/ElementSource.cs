namespace ACME.Controls
{
    using System;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [ContentProperty("ElementName")]
    public class ElementSource : IMarkupExtension
    {
        #region Properties Public

        public string ElementName { get; set; }

        #endregion

        #region IMarkupExtension Members

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var rootProvider = serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;
            if (!(rootProvider?.RootObject is Element root))
                return null;
            return root.FindByName<Element>(this.ElementName);
        }

        #endregion
    }
}