using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektPOS
{
    /// <summary>
    /// List extensions.
    /// </summary>
    public static class ListExtensions
    {
/// <summary>
/// This method is signed to list that contains Postac class objects.
/// </summary>
    public static int MaxId(this List<Postac> postacie)
        {
            int res = 0;
            foreach(var p in postacie)
            {
                if (p.Id > res)
                    res = p.Id;
            }
            return res;
        }
    }
}
