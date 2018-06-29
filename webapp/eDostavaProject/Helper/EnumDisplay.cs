using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace eDostava.Web.Helper
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Extension metoda za prikazivanje enum "Display Name" atributa.
        /// </summary>
        /// <param name="enumType">EnumType</param>
        /// <returns></returns>
        public static string GetDisplay(this Enum enumType) 
            => enumType
                .GetType()
                .GetMember(enumType.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .Name;
    }
}
