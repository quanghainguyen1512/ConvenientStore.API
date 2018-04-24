using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;

namespace ConvenientShop.API.Services.Interfaces
{
    interface ISupplierRepository
    {
        IEnumerable<SupplierDto> GetSuppliers();
        Supplier GetSupplier(int supplierId, bool includeProducts);
        bool SupplierExists(int supplierId);
        bool AddSupplier(Supplier supplier);
        bool AddProductToSupplier(int supplierId, Product product);
        void DeleteProductFromSupplier(int supplierId, int productId);
    }
}