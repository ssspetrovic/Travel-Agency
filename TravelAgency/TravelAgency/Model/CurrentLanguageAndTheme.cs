using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    public static class CurrentLanguageAndTheme
    {
        public static int languageId { get; set; }  // 0 -> English  1 -> Serbian
        public static int themeId { get; set; }  // 0 -> Light  1 -> Dark
    }
}
