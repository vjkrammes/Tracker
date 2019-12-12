namespace TrackerCommon
{
    public struct PasswordOptions
    {
        public int MinimumLength;
        public int MinimumUniqueCharacters;
        public bool RequireSpecialCharacters;
        public bool RequireLowercase;
        public bool RequireUppercase;
        public bool RequireDigits;
    }
}
