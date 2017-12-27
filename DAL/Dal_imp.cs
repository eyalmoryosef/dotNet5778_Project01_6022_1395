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

        public void addChild(Child child)
        {
            if (idExist(child.ID))//Check whether the ID already exists
                throw new DuplicateWaitObjectException("The inserted child's ID already exists");
            DataSource.IDList.Add(child.ID);

            DataSource.ChildList.Add(child);
        }

        public void addContract(Contract contract)
        {
            if (!idExist(contract.ChildID))
                throw new ArgumentException("The child that in the contract doesnt exist");
            if (!idExist(contract.NannyID))
                throw new ArgumentException("The nanny that in the contract doesnt exist");

            contract.Num = Convert.ToString(runningNumber++);
            contract.Num.PadLeft(8, '0');//padding the num with '0' to reach 8 digits

            DataSource.ContractList.Add(contract);
        }

        public void addMother(Mother mother)
        {
            if (idExist(mother.ID))
                throw new DuplicateWaitObjectException("The inserted mother's ID already exists");

            DataSource.IDList.Add(mother.ID);
            DataSource.MotherList.Add(mother);
        }

        public void addNanny(Nanny nanny)
        {
            if (idExist(nanny.ID))
                throw new DuplicateWaitObjectException("The inserted nanny's ID already exists");

            DataSource.IDList.Add(nanny.ID);
            DataSource.NannyList.Add(nanny);
        }

        public void deleteChild(Child child)
        {
            if (!DataSource.ChildList.Remove(child))
                throw new KeyNotFoundException("The child does not exist and therefore can not be deleted");

            DataSource.IDList.Remove(child.ID);
        }

        public void deleteContract(Contract contract)
        {
            if (!DataSource.ContractList.Remove(contract))
                throw new KeyNotFoundException("The contract does not exist and therefore can not be deleted");
        }

        public void deleteMother(Mother mother)
        {
            if (!DataSource.MotherList.Remove(mother))
                throw new KeyNotFoundException("The mother does not exist and therefore can not be deleted");

            DataSource.IDList.Remove(mother.ID);
        }

        public void deleteNanny(Nanny nanny)
        {
            if (!DataSource.NannyList.Remove(nanny))
                throw new KeyNotFoundException("The nanny does not exist and therefore can not be deleted");

            DataSource.IDList.Remove(nanny.ID);
        }

        public List<Child> GetAllChild()
        {
            throw new NotImplementedException();
        }

        public List<Contract> GetAllContract()
        {
            throw new NotImplementedException();
        }

        public List<Mother> GetAllMother()
        {
            throw new NotImplementedException();
        }

        public List<Nanny> GetAllNanny()
        {
            throw new NotImplementedException();
        }

        public void updatingChild(Child child)
        {
            throw new NotImplementedException();
        }

        public void updatingContract(Contract contract)
        {
            throw new NotImplementedException();
        }

        public void updatingMother(Mother mother)
        {
            throw new NotImplementedException();
        }

        public void updatingNanny(Nanny nanny)
        {
            throw new NotImplementedException();
        }

        private bool idExist(int id)
        {
            foreach (int item in DataSource.IDList)
            {
                if (item == id)
                    return true;
            }
            return false;
        }
        public Mother getMom(int id)
        {
            return DataSource.MotherList.Find(m => m.ID == id);
        }
        public Nanny getNanny(int id)
        {
            return DataSource.NannyList.Find(n => n.ID == id);
        }
        public Child getChild(int id)
        {
            return DataSource.ChildList.Find(c => c.ID == id);
        }
        public Contract getContract(int id)
        {
            return DataSource.ContractList.Find(c => c.ChildID == id);
        }
        public bool ContractExsit(int id)
        {
           return DataSource.ContractList.Exists(c => c.ChildID == id);

        }
    }
}
