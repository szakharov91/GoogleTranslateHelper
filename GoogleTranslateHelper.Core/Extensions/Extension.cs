using System;
using GoogleTranslateHelper.Core;
using GoogleTranslateHelper.Core.Extensions.Enums;
using GoogleTranslateHelper.Utils.HttpClient;

namespace GoogleTranslateHelper.Extensions;

/// <summary> Класс расширений </summary>
internal static class Extension
{
    /// <summary> Возвращает строкое представление enum, заданное в аттрибуте </summary>
    /// <param name="enum">Enum объект</param>
    /// <returns>Строковое представление енума</returns>
    public static string GetStringValue(this Enum @enum)
    {
        var type = @enum.GetType();

        var fieldInfo = type.GetField(@enum.ToString());

        if (fieldInfo is null)
        {
            throw new NullReferenceException(nameof(fieldInfo));
        }

        var stringValueAttributes = (StringValueAttribute[])fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false);

        if (stringValueAttributes is null)
        {
            throw new NullReferenceException(nameof(stringValueAttributes));
        }

        return stringValueAttributes.Length > 0 ? stringValueAttributes[0].StringValue : null;
    }

    /// <summary> Возвращает enum по коду языка </summary>
    /// <param name="langCode">Код языка ISO 639-1</param>
    public static Languages? GetLanguages(string langCode)
    {
        var langs = ((System.Enum.GetValues<Languages>()) ?? Enumerable.Empty<Languages>()).ToList();
        return langs.Find(x => x.GetStringValue() == langCode);
    }
}
