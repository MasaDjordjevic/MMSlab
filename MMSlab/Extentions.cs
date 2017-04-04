using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab
{
    public static class Extentions
    {
        public static void Push<T>(this List<T> list, T el)
        {
            if(list.Capacity == list.Count)
            {
                list.RemoveAt(list.Count - 1);
            }

            list.Insert(0, el);
        }

        public static T Pop<T>(this List<T> list)
        {
            if (list.Count == 0)
                return default(T);

            T el = list[0];
            list.RemoveAt(0);
            return el;
        }
        
    }
}
