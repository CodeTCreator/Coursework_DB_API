using LibraryFor_CAR_DB.Entity;
using LibraryFor_CAR_DB.Services;
using System.Collections.Generic;
using System.Linq;

namespace Курсовая_работа._БД.Box
{
    internal class BoxDepartments
    {
        Service_PoliceDepartment _service_PoliceDepartment = new Service_PoliceDepartment();
        List<PoliceDepartment> _departments = new List<PoliceDepartment>();
        
        public BoxDepartments() { UpdateDepartments(); }

        public void UpdateDepartments()
        {
            _departments = _service_PoliceDepartment.getAllPoliceDepartment();
        }

        public List<PoliceDepartment> GetPoliceDepartments()
        {
            return _departments;
        }

        public void SaveDepartments(PoliceDepartment policeDepartment)
        {
            _service_PoliceDepartment.savePoliceDepartment(policeDepartment);
            UpdateDepartments(); ;
        }
        public List<PoliceDepartment> filterLocations(string location)
        {
            List<PoliceDepartment> list = (from item in _departments
                                 where
                                 item.Location.ToString().Contains(location)
                                 select item).ToList();
            return list;
        }

        public void edit(PoliceDepartment policeDepartment, int ID) 
        { 
            _service_PoliceDepartment.edit(policeDepartment, ID);
            UpdateDepartments();
        }
        public void delete(PoliceDepartment policeDepartment)
        {
            _service_PoliceDepartment.delete(policeDepartment);
            UpdateDepartments();
        }

    }

}
