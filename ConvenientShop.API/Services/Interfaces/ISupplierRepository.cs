using ConvenientShop.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConvenientShop.API.Services.Interfaces
{
    interface ISupplierRepository
    {
        IEnumerable<Supplier> GetSuppliers();
        Supplier GetSupplier(int supplierId, bool includeProducts);
        bool SupplierExists(int supplierId);
        bool AddProductToSupplier(int supplierId, Product product);
        void DeleteProductFromSupplier(int supplierId, int productId);
    }
}
