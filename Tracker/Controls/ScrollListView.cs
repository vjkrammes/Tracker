﻿using System.Windows.Controls;

namespace Tracker.Controls
{
    public class ScrollListView : ListView
    {
        public ScrollListView() : base()
        {
            SelectionChanged += Scroll;
        }

        private void Scroll(object sender, SelectionChangedEventArgs e)
        {
            if (e?.AddedItems != null && e.AddedItems.Count > 0)
            {
                ScrollIntoView(e.AddedItems[0]);
            }
        }
    }
}
