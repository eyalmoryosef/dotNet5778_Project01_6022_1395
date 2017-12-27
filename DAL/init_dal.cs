using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace DAL
{
    public class init_dal
    {
        public static IDAL init()
        {
            return new Dal_imp();
        }

    }
}

