//(C) 5778 David Rakovsky and Eyal Mor-Yosef 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    /// <summary>
    /// The interface of the DAL
    /// </summary>
    public interface IDAL
    {
        #region Nanny's functions
        void addNanny(Nanny nanny);
        void deleteNanny(Nanny nanny);
        void updatingNanny(Nanny nanny);
        Nanny getNanny(int id);
        #endregion

        #region Mother's functions
        void addMother(Mother mother);
        void deleteMother(Mother mother);
        void updatingMother(Mother mother);
        Mother getMother(int id);
        #endregion

        #region Child's functions
        void addChild(Child child);
        void deleteChild(Child child);
        void updatingChild(Child child);
        Child getChild(int id);
        #endregion

        #region Contract's functions
        void addContract(Contract contract);
        void deleteContract(Contract contract);
        void updatingContract(Contract contract);
        Contract getContract(int id);
        bool ContractExists(int id);
        #endregion

        #region Get lists functions
        List<Nanny> GetAllNanny();
        List<Mother> GetAllMother();
        List<Child> GetAllChild();
        List<Contract> GetAllContract();
        #endregion
    }
}
