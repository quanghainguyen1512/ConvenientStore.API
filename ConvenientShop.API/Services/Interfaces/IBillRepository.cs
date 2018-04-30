using System;
using System.Collections.Generic;
using ConvenientShop.API.Entities;

namespace ConvenientShop.API.Services.Interfaces
{
    public interface IBillRepository
    {
        IEnumerable<Bill> GetBills();
        Bill GetBill(int id);
        bool AddBill(Bill bill);
        IEnumerable<Bill> GetBillsByStaff(int staffId);
    }
}