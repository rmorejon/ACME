using ACME.Controls;
using ACME.Droid.Renderers;

using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]

namespace ACME.Droid.Renderers
{
    using System.ComponentModel;

    using ACME.Controls;

    using Android.Content;
    using Android.Graphics;
    using Android.Graphics.Drawables;
    using Android.Support.V4.Content;
    using Android.Text;

    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    public class ExtendedEntryRenderer : EntryRenderer
    {
        #region Constructors

        public ExtendedEntryRenderer(Context context)
            : base(context)
        {
        }

        public ExtendedEntryRenderer()
            : base(null)
        {
        }

        #endregion

        #region Properties Public

        public ExtendedEntry ExtendedEntryElement => this.Element as ExtendedEntry;

        #endregion

        #region Methods

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                this.Control.InputType |= InputTypes.TextFlagNoSuggestions;
                this.UpdateLineColor();
            }

            if (e.OldElement != null || e.NewElement == null)
                return;

            var element = (ExtendedEntry)this.Element;

            var editText = this.Control;
            if (!string.IsNullOrEmpty(element.Image))
                switch (element.ImageAlignment)
                {
                    case ImageAlignment.Left:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(
                            this.GetDrawable(element.Image, element),
                            null,
                            null,
                            null);
                        break;
                    case ImageAlignment.Right:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(
                            null,
                            null,
                            this.GetDrawable(element.Image, element),
                            null);
                        break;
                }
            editText.CompoundDrawablePadding = 25;
        }

        private BitmapDrawable GetDrawable(string imageEntryImage, ExtendedEntry element)
        {
            var resID = this.Resources.GetIdentifier(imageEntryImage, "drawable", this.Context.PackageName);
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            return new BitmapDrawable(
                this.Resources,
                Bitmap.CreateScaledBitmap(bitmap, element.ImageWidth * 2, element.ImageHeight * 2, true));
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(nameof(ExtendedEntry.LineColorToApply))) this.UpdateLineColor();
        }

        private void UpdateLineColor()
        {
            this.Control?.Background?.SetColorFilter(
                this.ExtendedEntryElement.LineColorToApply.ToAndroid(),
                PorterDuff.Mode.SrcAtop);
        }

        #endregion
    }
}