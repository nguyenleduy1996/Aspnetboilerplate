using CodeLearn.Debugging;

namespace CodeLearn
{
    public class CodeLearnConsts
    {
        public const string LocalizationSourceName = "CodeLearn";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "77c94e697d914cc4a528da096881f5e0";
    }
}
