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

        public void deleteChild(Child child)
        {
            //    foreach (var c in GetAllContract())
            //    {
            //        if (c.ChildID == child.ID)
            //            GetAllContract().Remove(c);
            //    }

        }

        public void deleteContract(Contract contract)
        {
            throw new NotImplementedException();
        }

        public void deleteMother(Mother mother)
        {
            throw new NotImplementedException();
        }

        public void deleteNanny(Nanny nanny)
        {
            throw new NotImplementedException();
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
        public List<Nanny> match(Mother mother)
        {
            List<Nanny> matchingNannys = new List<Nanny>();

            foreach (var n in dal.GetAllNanny())
            {
                bool match = true;
                for (int i = 0; i < 6 && match; i++)
                    if (mother.DaysOfNeedingNanny[i] == true && n.WorkDays[i] == true)
                        match = false;
                if (match)
                {
                    for (int i = 0; i < 6 && match; i++)
                    {
                        if ((mother.HoursOfNeedingNanny[0, i].Hour < n.WorkHours[0, i].Hour) || (mother.HoursOfNeedingNanny[1, i].Hour > n.WorkHours[1, i].Hour))
                            match = false;
                    }
                }
                if (match)
                    matchingNannys.Add(n);

            }
            return matchingNannys;
        }
        public static int CalculateDistance(string mother, string nanny)
        {
            string source = mother;
            string dest = nanny;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        List<IGrouping<int, Contract>> gropingByDistance(bool sortByHighDistance = false)
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
        List<IGrouping<int, Nanny>> gropingByAge(bool sortByHighAge = false)
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
    }
}
