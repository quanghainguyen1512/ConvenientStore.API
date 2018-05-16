using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ConvenientShop.API.Models;
using ConvenientShop.API.Services.Interfaces;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace ConvenientShop.API.Services
{
    public class ConvenientStoreRepository : IConvenientStoreRepository
    {
        protected readonly StoreConfig _storeConfig;

        public ConvenientStoreRepository(IOptions<StoreConfig> config)
        {
            _storeConfig = config.Value;
        }

        public IDbConnection Connection => new MySqlConnection(_storeConfig.DbConnectionString);
    }
}
