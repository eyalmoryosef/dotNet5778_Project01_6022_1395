//(C) 5778 David Rakovsky and Eyal Mor-Yosef 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    public class Dal_imp : IDAL
    {
        private int runningNumber = 1;

        #region add functions        
        /// <summary>
        /// A function that adds a new child to the ChildList
        /// </summary>
        public void addChild(Child child)
        {
            if (idExist(child.ID))//Check whether the ID already exists
                throw new DuplicateWaitObjectException("The inserted child's ID already exists");
            DataSource.IDList.Add(child.ID);

            DataSource.ChildList.Add(child);
        }

        /// <summary>
        /// A function that adds a new contract to the ContractList
        /// </summary>
        public void addContract(Contract contract)
        {
            if (!idExist(contract.ChildID))
                throw new ArgumentException("The child that in the contract doesnt exist");
            if (!idExist(contract.NannyID))
                throw new ArgumentException("The nanny that in the contract doesnt exist");

            contract.Num = Convert.ToString(runningNumber++);//adding the running number
            contract.Num.PadLeft(8, '0');//padding the num with '0' to reach 8 digits

            DataSource.ContractList.Add(contract);
        }

        /// <summary>
        /// A function that adds a new mother to the MotherList
        /// </summary>
        public void addMother(Mother mother)
        {
            if (idExist(mother.ID))
                throw new DuplicateWaitObjectException("The inserted mother's ID already exists");

            DataSource.IDList.Add(mother.ID);
            DataSource.MotherList.Add(mother);
        }

        /// <summary>
        /// A function that adds a new nanny to the NannyList
        /// </summary>
        public void addNanny(Nanny nanny)
        {
            if (idExist(nanny.ID))
                throw new DuplicateWaitObjectException("The inserted nanny's ID already exists");

            DataSource.IDList.Add(nanny.ID);
            DataSource.NannyList.Add(nanny);
        }
        #endregion

        #region delete functions
        /// <summary>
        /// A function that deletes a child from the ChildList
        /// </summary>
        public void deleteChild(Child child)
        {
            if (!DataSource.ChildList.Remove(child))
                throw new KeyNotFoundException("The child does not exist and therefore can not be deleted");

            DataSource.IDList.Remove(child.ID);
        }

        /// <summary>
        /// A function that deletes a contract from the ContractList
        /// </summary>
        public void deleteContract(Contract contract)
        {
            if (!DataSource.ContractList.Remove(contract))
                throw new KeyNotFoundException("The contract does not exist and therefore can not be deleted");
        }

        /// <summary>
        /// A function that deletes a mother from the MotherList
        /// </summary>
        public void deleteMother(Mother mother)
        {
            if (!DataSource.MotherList.Remove(mother))
                throw new KeyNotFoundException("The mother does not exist and therefore can not be deleted");

            DataSource.IDList.Remove(mother.ID);
        }

        /// <summary>
        /// A function that deletes a nanny from the NannyList
        /// </summary>
        public void deleteNanny(Nanny nanny)
        {
            if (!DataSource.NannyList.Remove(nanny))
                throw new KeyNotFoundException("The nanny does not exist and therefore can not be deleted");

            DataSource.IDList.Remove(nanny.ID);
        }
        #endregion

        #region get list functions
        /// <summary>
        /// A function that returns the ChildList
        /// </summary>
        public List<Child> GetAllChild()
        {
            return DataSource.ChildList;
        }

        /// <summary>
        /// A function that returns the ContractList
        /// </summary>
        public List<Contract> GetAllContract()
        {
            return DataSource.ContractList;
        }

        /// <summary>
        /// A function that returns the MotherList
        /// </summary>
        public List<Mother> GetAllMother()
        {
            return DataSource.MotherList;
        }

        /// <summary>
        /// A function that returns the NannyList
        /// </summary>
        public List<Nanny> GetAllNanny()
        {
            return DataSource.NannyList;
        }
        #endregion

        #region updating functions
        /// <summary>
        /// A function that updates an existing child
        /// </summary>
        public void updatingChild(Child child)
        {
            Child old_child;
            try { old_child = getChild(child.ID); }
            catch (Exception ex)
            {
                throw new KeyNotFoundException("The child doesn't exist and therefore can't be updated");
            }

            if (old_child.MotherID != child.MotherID)
                throw new ArgumentException("It's not possible to update the Mother's ID of an existing child");

            if (old_child.DateOfBirth != child.DateOfBirth)
                throw new ArgumentException("It's not possible to update the date of birth of an existing child");

            deleteChild(old_child);
            addChild(child);
        }

        /// <summary>
        /// A function that updates an existing contract
        /// </summary>
        public void updatingContract(Contract contract)
        {
            Contract old_contract;
            try { old_contract = getContract(contract.ChildID); }
            catch (Exception ex)
            {
                throw new KeyNotFoundException("The contract doesn't exist and therefore can't be updated");
            }

            if (old_contract.NannyID != contract.NannyID)
                throw new ArgumentException("It's not possible to update the Nanny's ID of an existing contract");

            deleteContract(old_contract);
            addContract(contract);
        }

        /// <summary>
        /// A function that updates an existing mother
        /// </summary>
        public void updatingMother(Mother mother)
        {
            Mother old_mother;
            try { old_mother = getMother(mother.ID); }
            catch (Exception ex)
            {
                throw new KeyNotFoundException("The mother doesn't exist and therefore can't be updated");
            }

            deleteMother(old_mother);
            addMother(mother);
        }

        /// <summary>
        /// A function that updates an existing nanny
        /// </summary>
        public void updatingNanny(Nanny nanny)
        {
            Nanny old_nanny;
            try { old_nanny = getNanny(nanny.ID); }
            catch (Exception ex)
            {
                throw new KeyNotFoundException("The nanny doesn't exist and therefore can't be updated");
            }

            if (old_nanny.DateOfBirth != nanny.DateOfBirth)
                throw new ArgumentException("It's not possible to update the date of birth of an existing nanny");

            deleteNanny(old_nanny);
            addNanny(nanny);
        }
        #endregion

        #region get functions
        /// <summary>
        /// EXTRA: A function that returns the specific mother based on ID
        /// (using the lambda expression)
        /// </summary>
        public Mother getMother(int id)
        {
            return DataSource.MotherList.Find(m => m.ID == id);
        }

        /// <summary>
        /// EXTRA: A function that returns the specific nanny based on ID
        /// (using the lambda expression)
        /// </summary>
        public Nanny getNanny(int id)
        {
            return DataSource.NannyList.Find(n => n.ID == id);
        }

        /// <summary>
        /// EXTRA: A function that returns the specific child based on ID
        /// (using the lambda expression)
        /// </summary>
        public Child getChild(int id)
        {
            return DataSource.ChildList.Find(c => c.ID == id);
        }

        /// <summary>
        /// EXTRA: A function that returns the specific contract based on Child's ID
        /// (using the lambda expression)
        /// </summary>
        public Contract getContract(int id)
        {
            return DataSource.ContractList.Find(c => c.ChildID == id);
        }
        #endregion

        #region 'check if exists' functions
        /// <summary>
        /// EXTRA: A function that checks whether the ID exists in the system
        /// (including all: nannies, mothers, and children)
        /// </summary>
        /// <returns>whether the ID exists in the system</returns>
        private bool idExist(int id)
        {
            foreach (int item in DataSource.IDList)
            {
                if (item == id)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// EXTRA: A function that checks whether the specific contract (according to ID) exists
        /// (using the lambda expression)
        /// </summary>
        public bool ContractExists(int id)
        {
            return DataSource.ContractList.Exists(c => c.ChildID == id);
        }
        #endregion
    }
}
