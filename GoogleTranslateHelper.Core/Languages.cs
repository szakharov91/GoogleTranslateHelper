using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleTranslateHelper
{
    /// <summary> Список языков </summary>
    public enum Languages
    {
        [StringValue("en")]
        English,

        [StringValue("ru")]
        Russian,

        [StringValue("de")]
        German
    }
}
