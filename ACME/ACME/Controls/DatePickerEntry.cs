namespace ACME.Controls
{
    using Xamarin.Forms;

    public class DatePickerEntry : DatePicker
    {
        #region Static Fields and Constants

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(
            nameof(Image),
            typeof(string),
            typeof(DatePickerEntry),
            string.Empty);

        public static readonly BindableProperty ImageHeightProperty = BindableProperty.Create(
            nameof(ImageHeight),
            typeof(int),
            typeof(DatePickerEntry),
            40);

        public static readonly BindableProperty ImageWidthProperty = BindableProperty.Create(
            nameof(ImageWidth),
            typeof(int),
            typeof(DatePickerEntry),
            40);

        public static readonly BindableProperty ImageAlignmentProperty = BindableProperty.Create(
            nameof(ImageAlignment),
            typeof(ImageAlignment),
            typeof(DatePickerEntry),
            ImageAlignment.Left);

        #endregion

        #region Constructors

        #endregion

        #region Properties Public

        public int ImageWidth
        {
            get => (int)this.GetValue(ImageWidthProperty);
            set => this.SetValue(ImageWidthProperty, value);
        }

        public int ImageHeight
        {
            get => (int)this.GetValue(ImageHeightProperty);
            set => this.SetValue(ImageHeightProperty, value);
        }

        public string Image
        {
            get => (string)this.GetValue(ImageProperty);
            set => this.SetValue(ImageProperty, value);
        }

        public ImageAlignment ImageAlignment
        {
            get => (ImageAlignment)this.GetValue(ImageAlignmentProperty);
            set => this.SetValue(ImageAlignmentProperty, value);
        }

        #endregion
    }
}