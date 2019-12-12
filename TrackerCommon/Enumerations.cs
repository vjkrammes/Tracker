using System;

namespace TrackerCommon
{

    #region PasswordStrength

    public enum PasswordStrength
    {
        [Description("Blank")]
        Blank = 0,
        [Description("Very Weak")]
        VeryWeak = 1,
        [Description("Weak")]
        Weak = 2,
        [Description("Medium")]
        Medium = 3,
        [Description("Strong")]
        Strong = 4,
        [Description("Very Strong")]
        VeryStrong = 5
    }

    #endregion

    #region PopupButtons

    [Flags]
    public enum PopupButtons
    {
        [Description("None")]
        None = 0,
        [Description("Ok")]
        Ok = 1,
        [Description("Cancel")]
        Cancel = 2,
        [Description("Yes")]
        Yes = 4,
        [Description("No")]
        No = 8,
        [Description("Ok and Cancel")]
        OkCancel = Ok | Cancel,
        [Description("Yes and No")]
        YesNo = Yes | No,
        [Description("Yes, No and Cancel")]
        YesNoCancel = Yes | No | Cancel
    }

    #endregion

    #region PopupImage

    public enum PopupImage
    {
        [Description("None")]
        None = 0,
        [Description("Question")]
        Question = 1,
        [Description("Information")]
        Information = 2,
        [Description("Warning")]
        Warning = 3,
        [Description("Stop")]
        Stop = 4,
        [Description("Error")]
        Error = 5
    }

    #endregion

    #region PopupResult

    public enum PopupResult
    {
        [Description("Unspecified")]
        Unspecified = 0,
        [Description("Yes")]
        Yes = 1,
        [Description("No")]
        No = 2,
        [Description("Ok")]
        Ok = 3,
        [Description("Cancel")]
        Cancel = 4
    }

    #endregion

    #region SFGAO

    [Flags]
    public enum SFGAO : long
    {
        SFGAO_CANCOPY = 1,
        SFGAO_CANMOVE = 2,
        SFGAO_CANLINK = 4,
        SFGAO_STORAGE = 8,
        SFGAO_CANRENAME = 0x10,
        SFGAO_CANDELETE = 0x20,
        SFGAO_HASPROPSHEET = 0x40,
        SFGAO_DROPTARGET = 0x100,
        SFGAO_CAPABILITYMASK = 0x177,
        SFGAO_SYSTEM = 0x1000,
        SFGAO_ENCRYPTED = 0x2000,
        SFGAO_ISSLOW = 0x4000,
        SFGAO_GHOSTED = 0x8000,
        SFGAO_LINK = 0x1_0000,
        SFGAO_SHARE = 0x2_0000,
        SFGAO_READONLY = 0x4_0000,
        SFGAO_HIDDEN = 0x8_0000,
        SFGAO_DISPLAYATTRMASK = 0xF_C000,
        SFGAO_NOENUMERATED = 0x10_0000,
        SFGAO_NEWCONTENT = 0x20_0000,
        // SFGAO_CANMONIKER - not supported
        // SFGAO_HASSTORAGE - not supported
        SFGAO_STREAM = 0x40_0000,
        SFGAO_STORAGEANCESTOR = 0x80_0000,
        SFGAO_VALIDATE = 0x100_0000,
        SFGAO_REMOVABLE = 0x200_0000,
        SFGAO_COMPRESSED = 0x400_0000,
        SFGAO_BROWSABLE = 0x800_00000,
        SFGAO_FILESYSANCESTOR = 0x1000_0000,
        SFGAO_FOLDER = 0x2000_0000,
        SFGAO_FILESYSTEM = 0x4000_0000,
        SFGAO_STORAGECAPMASK = 0x70C5_0008,
        SFGAO_HASSUBFOLDER = 0x8000_0000,
        SFGAO_CONTENTSMASK = 0x8000_0000,
        SFGAO_PKEYSFGAOMASK = 0x8104_4000
    }

    #endregion

    #region SHGFI

    [Flags]
    public enum SHGFI : uint
    {
        ADDOVERLAYS = 0x20,
        ATTR_SPECIFIED = 0x20000,
        ATTRIBUTES = 0x800,
        DISPLAYNAME = 0x200,
        EXETYPE = 0x2000,
        ICON = 0x100,
        ICONLOCATION = 0x1000,
        LARGEICON = 0,
        LINKOVERLAY = 0x8000,
        OPENICON = 2,
        OVERLAYINDEX = 0x40,
        PIDL = 8,
        SELECTED = 0x10000,
        SHELLICONSIZE = 4,
        SMALLICON = 1,
        SYSICONINDEX = 0x4000,
        TYPENAME = 0x400,
        USEFILEATTRIBUTES = 0x10
    }

    #endregion
}
