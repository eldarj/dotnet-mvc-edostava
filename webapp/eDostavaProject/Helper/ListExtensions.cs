using eDostava.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Helper
{
    // Extension klasa za List<> - Helper za upravljanje stavkama narudzbi (Korpa)
    public static class ListExtensions
    {
        public static bool AddUnique(this List<StavkaNarudzbe> list, StavkaNarudzbe stavka)
        {
            if (stavka == null)
                return false;

            foreach(var obj in list)
            {
                if (obj.Equals(stavka))
                {
                    obj.Kolicina++;
                    return true;
                }
            }

            list.Add(stavka);
            return true;
        }

        public static bool UmanjiKolicinu(this List<StavkaNarudzbe> list, StavkaNarudzbe stavka)
        {
            if (stavka == null)
                return false;

            foreach (var obj in list)
            {
                if (obj.Equals(stavka))
                {
                    if (obj.Kolicina > 1)
                    {
                        obj.Kolicina--;
                        return true;
                    } else
                    {
                        list.Remove(stavka);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
