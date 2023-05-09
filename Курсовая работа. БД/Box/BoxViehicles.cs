using LibraryFor_CAR_DB.Entity;
using LibraryFor_CAR_DB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_работа._БД.Box
{
    class BoxViehicles
    {
        Service_Vehicle _service_Vehicle = new Service_Vehicle();
        List<Vehicle> _vehicles = new List<Vehicle>();


        public BoxViehicles() { UpdateVehicles(); }

        public void UpdateVehicles()
        {
            _vehicles = _service_Vehicle.getAllVehicle();
        }

        public List<Vehicle> GetVehicles() 
        { 
            return _vehicles;  
        }


        public List<Vehicle> filterVINVehicles (string VIN)
        {
            List<Vehicle> list = (from item in _vehicles
                                         where
                                 item.Vin.ToString().Contains(VIN)
                                         select item).ToList();
            return list;
        }
        public List<Vehicle> FilterLimitsID(List<int> limits)
        {
            if (limits != null)
            {
                List<Vehicle> context = new List<Vehicle>();
                foreach (Vehicle vehicle in _vehicles)
                {
                    if (!limits.Contains(vehicle.Id))
                    {
                        context.Add(vehicle);
                    }
                }
                return context;
            }
            return GetVehicles();
        }

        public bool checkSameinDB(Vehicle vehicle)
        {
            foreach (var item in _vehicles)
            {
                if (item.CarPower == vehicle.CarPower 
                    & item.EngineCapacity == vehicle.EngineCapacity & 
                    item.Vin == vehicle.Vin & item.Body == vehicle.Body & 
                    item.BrandModel == vehicle.BrandModel &
                    item.Color == vehicle.Color)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Vehicle> filterVehicles(params string[] list)
        {
            List<Vehicle> resultList = (from item in _vehicles
                        where
                        item.Id.ToString().Contains(list[0]) &&
                        item.Vin.Contains(list[1]) &&
                        item.Body.Contains(list[2]) &&
                        item.CarPower.ToString().Contains(list[3]) &&
                        item.EngineCapacity.ToString().Contains(list[4].Replace('.', ',')) &&
                        item.BrandModel.Contains(list[5]) &&
                        item.Color.Contains(list[6])
                        select item).ToList();
            return resultList;
        }

        public void SaveVehicle(Vehicle vehicle)
        {
            _service_Vehicle.saveVehicle(vehicle);
            UpdateVehicles();
        }
        public void edit(Vehicle vehicle,int ID)
        {
            _service_Vehicle.edit(vehicle, ID);
            UpdateVehicles();
        }
        public void delete(Vehicle vehicle)
        {
            _service_Vehicle.delete(vehicle); 
            UpdateVehicles();
        }
    }
}
