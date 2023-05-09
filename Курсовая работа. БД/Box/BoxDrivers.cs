using LibraryFor_CAR_DB.Entity;
using LibraryFor_CAR_DB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_работа._БД.Box
{
    class BoxDrivers : Box
    {
        Service_Driver service_Driver = new Service_Driver();
        List<Driver> drivers = new List<Driver>();
      

        public BoxDrivers() 
        {
            updateDrivers();
        }
        public void updateDrivers()
        {
            drivers = service_Driver.getAllDriver();
        }
        public List<Driver> GetDrivers() { return drivers; }
        public List<Driver> filterLimitsID(List<int> limits)
        {
            if (limits != null)
            {
                List<Driver> context = new List<Driver>();
                foreach (Driver driver in drivers)
                {
                    if (!limits.Contains(driver.Id))
                    {
                        context.Add(driver);
                    }
                }
                return context;
            }
            return GetDrivers();
        }
        public bool checkSameinDB(string Fio, string Address)
        {
            foreach (var item in drivers)
            {
                if (item.Fio == Fio & item.Address== Address)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Driver> filterFIODrivers(string Fio)
        {
            List<Driver> list = (from item in drivers
                                 where
                                 item.Fio.Contains(Fio)
                                 select item).ToList();
            return list;
        }
        public List<Driver> filterDrivers(string ID,string Fio,string Address)
        {
            List<Driver> list = (from item in drivers
                        where
                        item.Id.ToString().Contains(ID) &&
                        item.Fio.Contains(Fio) &&
                        item.Address.Contains(Address)
                        select item).ToList();
            return list;
        }
        public void saveDriver(Driver driver)
        {
            service_Driver.saveDriver(driver);
            updateDrivers();
        }
        public void edit(Driver driver,int ID)
        {
            service_Driver.edit(driver, ID);
            updateDrivers();
        }
        public void delete(Driver driver)
        {
            service_Driver.delete(driver);
            updateDrivers();
        }
    }
}
