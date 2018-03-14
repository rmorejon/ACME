namespace ACME.Controls
{
    using System.Runtime.CompilerServices;

    using Xamarin.Forms;

    public class ExtendedEntry : Entry
    {
        #region Static Fields and Constants

        public static readonly BindableProperty LineColorProperty = BindableProperty.Create(
            "LineColor",
            typeof(Color),
            typeof(ExtendedEntry),
            Color.Default);

        public static readonly BindableProperty FocusLineColorProperty = BindableProperty.Create(
            "FocusLineColor",
            typeof(Color),
            typeof(ExtendedEntry),
            Color.Default);

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
            "IsValid",
            typeof(bool),
            typeof(ExtendedEntry),
            true);

        public static readonly BindableProperty InvalidLineColorProperty = BindableProperty.Create(
            "InvalidLineColor",
            typeof(Color),
            typeof(ExtendedEntry),
            Color.Default);

        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(ExtendedEntry), string.Empty);

        public static readonly BindableProperty ImageHeightProperty =
            BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(ExtendedEntry), 40);

        public static readonly BindableProperty ImageWidthProperty =
            BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(ExtendedEntry), 40);

        public static readonly BindableProperty ImageAlignmentProperty =
            BindableProperty.Create(nameof(ImageAlignment), typeof(ImageAlignment), typeof(ExtendedEntry), ImageAlignment.Left);

        #endregion

        #region Fields

        private Color lineColorToApply;

        #endregion

        #region Constructors

        public ExtendedEntry()
        {
            this.Focused += this.OnFocused;
            this.Unfocused += this.OnUnfocused;
            this.ResetLineColor();
        }

        #endregion

        #region Properties Public

        public Color LineColorToApply
        {
            get => this.lineColorToApply;
            private set
            {
                this.lineColorToApply = value;
                this.OnPropertyChanged(nameof(this.LineColorToApply));
            }
        }

        public Color LineColor
        {
            get => (Color)this.GetValue(LineColorProperty);
            set => this.SetValue(LineColorProperty, value);
        }

        public Color FocusLineColor
        {
            get => (Color)this.GetValue(FocusLineColorProperty);
            set => this.SetValue(FocusLineColorProperty, value);
        }

        public bool IsValid
        {
            get => (bool)this.GetValue(IsValidProperty);
            set => this.SetValue(IsValidProperty, value);
        }

        public Color InvalidLineColor
        {
            get => (Color)this.GetValue(InvalidLineColorProperty);
            set => this.SetValue(InvalidLineColorProperty, value);
        }

        public int ImageWidth
        {
            get { return (int)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public int ImageHeight
        {
            get { return (int)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public ImageAlignment ImageAlignment
        {
            get { return (ImageAlignment)GetValue(ImageAlignmentProperty); }
            set { SetValue(ImageAlignmentProperty, value); }
        }

        #endregion

        #region Methods

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsValidProperty.PropertyName) this.CheckValidity();
        }

        private void OnFocused(object sender, FocusEventArgs e)
        {
            this.IsValid = true;
            this.LineColorToApply = this.FocusLineColor != Color.Default
                                        ? this.FocusLineColor
                                        : this.GetNormalStateLineColor();
        }

        private void OnUnfocused(object sender, FocusEventArgs e)
        {
            this.ResetLineColor();
        }

        private void ResetLineColor()
        {
            this.LineColorToApply = this.GetNormalStateLineColor();
        }

        private void CheckValidity()
        {
            if (!this.IsValid) this.LineColorToApply = this.InvalidLineColor;
        }

        private Color GetNormalStateLineColor()
        {
            return this.LineColor != Color.Default ? this.LineColor : this.TextColor;
        }

        #endregion

      
    }
    public enum ImageAlignment
    {
        Left,
        Right
    }
}