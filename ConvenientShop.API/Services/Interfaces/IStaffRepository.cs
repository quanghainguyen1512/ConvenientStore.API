using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConvenientShop.API.Entities;

namespace ConvenientShop.API.Services.Interfaces
{
    public interface IStaffRepository
    {
        IEnumerable<Staff> GetAllStaffs();
        Staff GetStaff(int id);
        bool AddStaff(Staff staff);
        bool DeleteStaff(Staff staffToDel);
        bool StaffExists(int id);
        bool UpdateStaff(Staff staffToUpdate);
    }
}