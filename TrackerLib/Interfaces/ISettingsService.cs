﻿using System;

namespace TrackerLib.Interfaces
{
    public interface ISettingsService
    {
        Guid SystemId { get; }
        string ProductName { get; }
        double ProductVersion { get; }

        string Alt0 { get; set; }
        string Alt1 { get; set; }
        string Background { get; set; }
        string Border { get; set; }
        string Foreground { get; set; }
        double IconHeight { get; set; }
        bool ShowStatusBar { get; set; }
        string BackupDirectory { get; set; }
        byte[] PasswordSalt { get; set; }
        byte[] PasswordHash { get; set; }

        object this[string key] { get; }
    }
}
