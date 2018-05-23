using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;

namespace ConvenientShop.API.Services.Interfaces
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetSuppliers();
        Supplier GetSupplier(int supplierId, bool includeProducts);
        bool SupplierExists(int supplierId);
        bool AddSupplier(Supplier supplier);
        bool AddProductToSupplier(int supplierId, Product product);
        bool DeleteProductFromSupplier(int supplierId, Product proToDel);
        bool UpdateSupplier(Supplier supToUpdate);
    }
}