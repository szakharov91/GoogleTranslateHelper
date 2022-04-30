using System;

namespace GoogleTranslateHelper
{
    /// <summary> Возвращает строкое представление для енумераторов </summary>
    internal class StringValueAttribute : Attribute
    {
        /// <summary> Свойство, возвращающее значение </summary>
        public string StringValue { get; private set; }

        /// <summary> Конструктор с параметром </summary>
        /// <param name="value">Текстовое представление для enum</param>
        public StringValueAttribute(string value) => StringValue = value;
    }
}