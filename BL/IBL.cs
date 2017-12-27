
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace BL
{
    public interface IBL 
    {
        #region nanny function
        void addNanny(Nanny nanny);
        void deleteNanny(Nanny nanny);
        void updatingNanny(Nanny nanny);
        #endregion

        #region mother function
        void addMother(Mother mother);
        void deleteMother(Mother mother);
        void updatingMother(Mother mother);
        #endregion

        #region child function
        void addChild(Child child);
        void deleteChild(Child child);
        void updatingChild(Child child);
        #endregion

        #region contract function
        void addContract(Contract contract);
        void deleteContract(Contract contract);
        void updatingContract(Contract contract);
        #endregion

        List<Nanny> GetAllNanny();
        List<Mother> GetAllMother();
        List<Child> GetAllChild();
        List<Contract> GetAllContract();

    }
}

