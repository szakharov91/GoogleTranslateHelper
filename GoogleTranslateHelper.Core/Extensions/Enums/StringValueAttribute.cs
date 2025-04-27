namespace GoogleTranslateHelper.Core.Extensions.Enums;

/// <summary> Возвращает строкое представление для енумераторов </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
internal sealed class StringValueAttribute : Attribute
{
    /// <summary> Gets Свойство, возвращающее значение </summary>
    public string StringValue
    {
        get
        {
            return this.value;
        }
    }

    private readonly string value;

    /// <summary> .ctor </summary>
    /// <param name="value">Текстовое представление для enum</param>
    public StringValueAttribute(string value)
    {
        this.value = value;
    }
}
