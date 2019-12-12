using TrackerCommon;

namespace Tracker.Infrastructure
{
    #region ExplorerItemType

    public enum ExplorerItemType
    {
        [Description("Unspecified")]
        Unspecified = 0,
        [Description("This Computer")]
        ThisComputer = 1,
        [Description("Disk Drive")]
        Drive = 2,
        [Description("Directory")]
        Directory = 3,
        [Description("File")]
        File = 4,
        [Description("Placeholder")]
        Placeholder = 999
    }

    #endregion
}
