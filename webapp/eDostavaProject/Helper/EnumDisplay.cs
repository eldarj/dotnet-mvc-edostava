using eDostava.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace eDostava.Web.Helper
{
    // Extension klasa za Enum - Helper za prikazivanje enum atributa.
    public static class EnumExtensions
    {
        public static string GetDisplay(this Stanje enumType) 
            => enumType
                .GetType()
                .GetMember(enumType.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .Name;
    }
}
