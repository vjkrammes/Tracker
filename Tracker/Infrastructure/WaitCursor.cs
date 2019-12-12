﻿using System;
using System.Windows.Input;

namespace Tracker.Infrastructure
{
    public class WaitCursor : IDisposable
    {
        private readonly Cursor _cursor;

        public WaitCursor()
        {
            _cursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
        }

        public void Dispose()
        {
            Mouse.OverrideCursor = _cursor;
        }
    }
}
