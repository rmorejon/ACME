using ACME.Controls;
using ACME.iOS.Renderers;

using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]

namespace ACME.iOS.Renderers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;

    using ACME.Controls;

    using CoreAnimation;

    using CoreGraphics;

    using UIKit;

    using Xamarin.Forms;
    using Xamarin.Forms.Platform.iOS;

    public class ExtendedEntryRenderer : EntryRenderer
    {
        #region Properties Public

        public ExtendedEntry ExtendedEntryElement => this.Element as ExtendedEntry;

        #endregion

        #region Public Methods

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            var lineLayer = this.GetOrAddLineLayer();
            lineLayer.Frame = new CGRect(
                0,
                this.Frame.Size.Height - LineLayer.LineHeight,
                this.Control.Frame.Size.Width,
                LineLayer.LineHeight);
        }

        #endregion

        #region Nested type: LineLayer

        private class LineLayer : CALayer
        {
            #region Static Fields and Constants

            public static readonly nfloat LineHeight = 2f;

            #endregion

            #region Constructors

            public LineLayer()
            {
                this.BorderWidth = LineHeight;
            }

            #endregion
        }

        #endregion

        #region Methods

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (this.Control != null) this.Control.BorderStyle = UITextBorderStyle.None;

                this.UpdateLineColor();
                this.UpdateCursorColor();
            }

            if (e.OldElement != null || e.NewElement == null)
                return;

            var element = (ImageEntry)this.Element;
            var textField = this.Control;
            if (!string.IsNullOrEmpty(element.Image))
            {
                switch (element.ImageAlignment)
                {
                    case ImageAlignment.Left:
                        textField.LeftViewMode = UITextFieldViewMode.Always;
                        textField.LeftView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                        break;
                    case ImageAlignment.Right:
                        textField.RightViewMode = UITextFieldViewMode.Always;
                        textField.RightView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                        break;
                }
            }

            textField.BorderStyle = UITextBorderStyle.None;
            CALayer bottomBorder = new CALayer
                                       {
                                           Frame = new CGRect(0.0f, element.HeightRequest - 1, this.Frame.Width, 1.0f),
                                           BorderWidth = 2.0f,
                                           BorderColor = element.LineColor.ToCGColor()
                                       };

            textField.Layer.AddSublayer(bottomBorder);
            textField.Layer.MasksToBounds = true;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(nameof(ExtendedEntry.LineColorToApply))) this.UpdateLineColor();
            else if (e.PropertyName.Equals(Entry.TextColorProperty.PropertyName)) this.UpdateCursorColor();
        }

        private void UpdateLineColor()
        {
            var lineLayer = this.GetOrAddLineLayer();
            lineLayer.BorderColor = this.ExtendedEntryElement.LineColorToApply.ToCGColor();
        }

        private LineLayer GetOrAddLineLayer()
        {
            var lineLayer = this.Control.Layer.Sublayers?.OfType<LineLayer>().FirstOrDefault();

            if (lineLayer == null)
            {
                lineLayer = new LineLayer();

                this.Control.Layer.AddSublayer(lineLayer);
                this.Control.Layer.MasksToBounds = true;
            }

            return lineLayer;
        }

        private void UpdateCursorColor()
        {
            this.Control.TintColor = this.Element.TextColor.ToUIColor();
        }

        private UIView GetImageView(string imagePath, int height, int width)
        {
            var uiImageView = new UIImageView(UIImage.FromBundle(imagePath))
                                  {
                                      Frame = new RectangleF(0, 0, width, height)
                                  };
            UIView objLeftView = new UIView(new System.Drawing.Rectangle(0, 0, width + 10, height));
            objLeftView.AddSubview(uiImageView);

            return objLeftView;
        }

        #endregion
    }
}