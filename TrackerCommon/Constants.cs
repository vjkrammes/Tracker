namespace TrackerCommon
{
    public static class Constants
    {
        // "Numbers"

        public const double ProductVersion = 1.00;

        public const double MinimumIconHeight = 16.0;
        public const double DefaultIconHeight = 24.0;
        public const double MaximumIconHeight = 32.0;

        public const int DefaultTimeout = 10;
        public const bool DefaultPooling = true;
        public const bool DefaultTrustedConnection = true;
        public const bool DefaultMARS = true;

        public const int HashIterations = 25_000;
        public const int HashLength = 64;           // bytes, not bits
        public const int SaltLength = 64;           // ditto

        // Exit codes, next = 903

        public const int MigrationFailed = 901;
        public const int ClientLoadFailed = 902;

        // strings

        public const string ProductName = "Tracker";

        public const string Alt0 = "Alt0";
        public const string Alt1 = "Alt1";
        public const string Background = "Background";
        public const string Border = "Border";
        public const string Foreground = "Foreground";
        public const string IconHeight = "IconHeight";
        public const string Locator = "Locator";

        public const string ConfigurationFilename = "appsettings.json";

        public const string DefaultServer = @".\SQLEXPRESS";
        public const string DefaultDatabase = ProductName;

        public const string ServerConfig = "Database:Server";
        public const string DatabaseConfig = "Database:Database";
        public const string TimeoutConfig = "Database:Timeout";
        public const string PoolingConfig = "Database:Pooling";
        public const string TrustedConnectionConfig = "Database:TrustedConnection";
        public const string MARSConfig = "Database:MARS";

        public const string True = "true";
        public const string Count = "Count";
        public const string Indexer = "Item[]";
        public const string Keys = "Keys";
        public const string Values = "Values";

        public const string Date = "date";
        public const string Datetime2 = "datetime2";
        public const string Varbinary = "varbinary(max)";
        public const string Miles = "decimal(6,2)";
        public const string Hours = "decimal(7,4)";

        public const string HexPattern = @"\A\b[0-9a-fA-F]+\b\Z";
        public const string NumberPattern = @"\A\b[0-9]_\b\Z";

        public const string Cancel = "/resources/cancel-32.png";
        public const string Checkmark = "/resources/checkmark-32.png";

        public const string DBE = "Database Error";
        public const string IOE = "I/O Error";
        public const string DuplicateKey = "An item with the same key has already been added.";
    }
}
