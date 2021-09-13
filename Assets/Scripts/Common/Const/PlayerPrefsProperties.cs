namespace Common.Const
{
    public static class PlayerPrefsProperties // todo wrap every pref with get; set; maybe?
    {
        public const string CharacterSelected = "character";
        public const string TeamSelected = "team";
        public const string IsServerBool = "isServer"; // prefs dont support bool, 1 - server, 0 - client
        // public const string IsEvenPlayerId = "IsEvenPlayerId"; // 1 - even, 0 - odd
    }
}