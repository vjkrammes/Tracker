using System.Windows.Controls;

namespace Tracker.Controls
{
    public class ScrollListBox : ListBox
    {
        public ScrollListBox() : base()
        {
            SelectionChanged += Scroll;
        }

        public void Scroll(object sender, SelectionChangedEventArgs e)
        {
            if (e?.AddedItems != null && e.AddedItems.Count > 0)
            {
                ScrollIntoView(e.AddedItems[0]);
            }
        }
    }
}
