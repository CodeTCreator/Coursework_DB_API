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
        List<Driver> _drivers = new List<Driver>();
      

        public BoxDrivers() 
        {
            updateDrivers();
        }
        public void updateDrivers()
        {
            _drivers = service_Driver.getAllDriver();
        }
        public List<Driver> GetDrivers() { return _drivers; }
        public List<Driver> filterLimitsID(List<int> limits)
        {
            if (limits != null)
            {
                List<Driver> context = new List<Driver>();
                foreach (Driver driver in _drivers)
                {
                    if (!limits.Contains(driver.Id))
                    {
                        context.Add(driver);
                    }
                }
                _drivers = context;
                return context;
            }
            return GetDrivers();
        }
        public bool checkSameinDB(string Fio, string Address)
        {
            foreach (var item in _drivers)
            {
                if (item.Fio == Fio & item.Address== Address)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Driver> FilterFIODrivers(string Fio)
        {
            List<Driver> list = (from item in _drivers
                                 where
                                 item.Fio.Contains(Fio)
                                 select item).ToList();
            return list;
        }
        public List<Driver> FilterDrivers(string ID,string Fio,string Address)
        {
            List<Driver> list = (from item in _drivers
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
