using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace CodeLearn.Localization
{
    public static class CodeLearnLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(CodeLearnConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(CodeLearnLocalizationConfigurer).GetAssembly(),
                        "CodeLearn.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
