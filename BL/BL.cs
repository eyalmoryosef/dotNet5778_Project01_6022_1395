using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi;

namespace BL
{
    internal class BL : IBL
    {
        DAL.IDAL dal = new Dal_imp();
        //public BL()
        //{
        //    dal = new DAL.init_dal.init();
        //}

        #region add "item" function
        public void addChild(Child child)
        {
            throw new NotImplementedException();
        }
        public void addContract(Contract contract)
        {
            Nanny n = dal.getNanny(contract.NannyID);
            Child c = dal.getChild(contract.ChildID);
            Mother m = dal.getMom(c.MotherID);
            if (c.DateOfBirth.Year < 1 && c.DateOfBirth.Month < 3)
                throw new Exception("the child's age is less than 3 month");
            if (n.MaxChilds == (dal.GetAllContract().FindAll(x => x.NannyID == n.ID).Count))
                throw new Exception("this nanny is full");
            if (n.HourlyRate && (contract.HourOrMonth == BE.SalaryBy.Hour))
            {
                int hours = 0;
                for (int i = 0; i < 6; i++)
                    hours += m.HoursOfNeedingNanny[1, i].Hour - m.HoursOfNeedingNanny[0, i].Hour;
                contract.MonthlySalary = n.PricePerHour * 4 * hours;
            }
            else
                contract.MonthlySalary = n.PricePerMonth;

            int momChildren = dal.GetAllContract().FindAll(x => dal.getChild(x.ChildID).MotherID == m.ID && x.NannyID == n.ID).Count;
            for (int i = 0; i < momChildren; i++)
                contract.MonthlySalary = contract.MonthlySalary * 0.02;
            try
            {
                dal.addContract(contract);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void addMother(Mother mother)
        {
            throw new NotImplementedException();
        }

        public void addNanny(Nanny nanny)
        {
            if (DateTime.Now.Year - nanny.DateOfBirth.Year < 18)
                throw new Exception("Nanny's age is less than 18");
            else dal.addNanny(nanny);

        }
        #endregion

        #region delate "item"  function
        public void deleteChild(Child child)
        {
            try
            {
                dal.deleteChild(child);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void deleteContract(Contract contract)
        {
            try
            {
                dal.deleteContract(contract);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void deleteMother(Mother mother)
        {
            try
            {
                dal.deleteMother(mother);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void deleteNanny(Nanny nanny)
        {
            try
            {
                dal.deleteNanny(nanny);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region get list of "item" function
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
        #endregion

        #region update "item" function
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
        #endregion

        /// <summary>
        /// the function return list of all the nannies who match to mom constraints
        /// </summary>
        /// <param name="FullMatch">A parameter indicating whether or not the nanny is suitable 100% of the mother's needs</param>
        /// <returns>returns the list of the matching nannies</returns>
        public List<Nanny> match(Mother mother, bool FullMatch)
        {
            List<Nanny> matchingNannys = new List<Nanny>(); // list will contain match nannies to mom constraints
            int Km;

            foreach (var n in dal.GetAllNanny())
            {
                bool match = true;
                for (int i = 0; i < 6 && match; i++)
                    // if mother need nanny & nanny doesn't workes on that day - nanny is not match
                    if (mother.DaysOfNeedingNanny[i] == true && n.WorkDays[i] == false) 
                        match = false;
                if (match) // nanny workes when mother need a nanny
                {
                    for (int i = 0; i < 6 && match; i++)
                    {
                        //if nanny start working in time that mom need nanny | nanny stop working(in other job) after mom need nanny - nanny cant work
                        if ((mother.HoursOfNeedingNanny[0, i].Hour < n.WorkHours[0, i].Hour) || (mother.HoursOfNeedingNanny[1, i].Hour > n.WorkHours[1, i].Hour))
                            match = false; // nanny isnt match
                    }
                }
                
                
                //If full match is required:
                if(FullMatch)
                   Km = 1;
                else//If make do with partial match:
                   Km = 5;
                
                //If the nanny lives far from the area that the mother requested, it's not appropriate
                if(match && CalculateDistance(mother.DesiredAddressOfNanny , n.Adress) > Km)
                    match = false;
                

                if (match)
                    matchingNannys.Add(n);

            }
            return matchingNannys; // return the all nannies who match mom constrains
        }

        /// <summary>
        /// calculte the distance between two  addresses
        /// </summary>
        /// <param name="mother" - mother's address
        /// <param name="nanny"  - nanny's address
        /// <returns></returns>
        public static int CalculateDistance(string mother, string nanny)
        {
            string source = mother; // mother's address
            string dest = nanny; // nanny's address
            var drivingDirectionRequest = new DirectionsRequest
            {
                TravelMode = TravelMode.Walking,
                Origin = source,
                Destination = dest,
            };
            DirectionsResponse drivingDirections = GoogleMaps.Directions.Query(drivingDirectionRequest);
            Route route = drivingDirections.Routes.First();
            Leg leg = route.Legs.First();
            return leg.Distance.Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mother"></param>
        /// <returns></returns>
        public List<Nanny> defaultNanny(Mother mother)
        {
            return GetAllNanny();
        }

        public List<Nanny> nannysDistance(Mother mother)
        {
            List<Nanny> inDistance = new List<Nanny>();
            int distance = CalculateDistance(mother.DesiredAddressOfNanny, mother.Adress);
            foreach (var n in dal.GetAllNanny())
            {
                if (CalculateDistance(mother.Adress, n.Adress) <= distance)
                    inDistance.Add(n);
            }
            return inDistance;
        }

        public List<Child> childWithoutNanny()
        {
            List<Child> withoutNanny = new List<Child>();
            foreach (var c in dal.GetAllChild())
            {
                if (dal.ContractExsit(c.ID))
                    withoutNanny.Add(c);

            }
            return withoutNanny;
        }

        public List<Nanny> nannyTAMAT()
        {
            List<Nanny> TAMAT = new List<Nanny>();
            foreach (var n in dal.GetAllNanny())
            {
                if (n.StateDaysOff)
                    TAMAT.Add(n);
            }
            return TAMAT;
        }

        public int distanceMotherNanny(Contract c)
        {
            return CalculateDistance(dal.getMom(dal.getChild(c.ChildID).MotherID).Adress, dal.getNanny(c.NannyID).Adress);
        }

        #region grouping function
        List<IGrouping<int, Contract>> groupingByDistance(bool sortByHighDistance = false)
        {
            List<IGrouping<int, Contract>> list = new List<IGrouping<int, Contract>>();
            for (int i = 1; i < 10; i++)
            {
                var gropingContracts = from c in dal.GetAllContract()
                                       group c
                                           by (distanceMotherNanny(c) <= (5000 * i))
                    into groping
                                       where groping.Key
                                       select new { distanceGroup = i * 5, gruopingContracts = groping };
                list.AddRange(gropingContracts as List<IGrouping<int, Contract>>);
            }
            if (sortByHighDistance)
            {
                List<IGrouping<int, Contract>> temp = new List<IGrouping<int, Contract>>();
                foreach (var member in list)
                {
                    temp.Add(member);
                    list.Remove(member);
                }
                return temp;
            }
            return list;
        }

        List<IGrouping<int, Nanny>> groupingByAge(bool sortByHighAge = false)
        {
            List<IGrouping<int, Nanny>> list = new List<IGrouping<int, Nanny>>();
            for (int i = 1; i < 100; i++)
            {
                var gropingNannies = from n in dal.GetAllNanny()
                                     group n
                                         by (n.MinAgeOfChild <= (3 * i))
                    into grouping
                                     where grouping.Key
                                     select new { distanceGroup = i * 3, groupingNannies = grouping };
                list.AddRange(gropingNannies as List<IGrouping<int, Nanny>>);
            }
            if (sortByHighAge)
            {
                List<IGrouping<int, Nanny>> temp = new List<IGrouping<int, Nanny>>();
                foreach (var member in list)
                {
                    temp.Add(member);
                    list.Remove(member);
                }
                return temp;
            }
            return list;
        }
        #endregion


    }
}
