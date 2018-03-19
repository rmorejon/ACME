using ACME.iOS.Renderers;

using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ViewCell), typeof(ExtendedListViewCellRenderer))]

namespace ACME.iOS.Renderers
{
    using UIKit;

    using Xamarin.Forms;
    using Xamarin.Forms.Platform.iOS;

    public class ExtendedListViewCellRenderer : ViewCellRenderer
    {
        #region Public Methods

        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            return cell;
        }

        #endregion
    }
}