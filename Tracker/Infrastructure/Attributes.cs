using System;

namespace Tracker.Infrastructure
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class ExplorerIconAttribute : Attribute
    {
        private readonly string _icon;
        public ExplorerIconAttribute(string icon) => _icon = icon;
        public string ExplorerIcon { get => _icon; }
    }
}
