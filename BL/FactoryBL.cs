using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class FactoryBL
    {
        public static IBL bl = null;
        public IBL GetBL()
        {
            bl = new BL();
            return bl;
        }
    }
}
