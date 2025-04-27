using GoogleTranslateHelper.Core.Extensions.Enums;

namespace GoogleTranslateHelper.Core;

/// <summary> Список языков. </summary>
public enum Languages
{
    /// <summary> english </summary>
    [StringValue("en")]
    English,

    /// <summary> russian </summary>
    [StringValue("ru")]
    Russian,

    /// <summary> german </summary>
    [StringValue("de")]
    German,
}
