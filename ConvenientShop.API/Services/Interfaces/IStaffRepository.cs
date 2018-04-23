using ConvenientShop.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConvenientShop.API.Services.Interfaces
{
    public interface IStaffRepository
    {
        IEnumerable<Staff> GetAllStaffs();
        Staff GetStaff(int id);
        bool AddStaff(Staff staff);
        bool DeleteStaff(int id);
    }
}
