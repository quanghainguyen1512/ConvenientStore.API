using System;
using System.Data;
using ConvenientShop.API.Models;
using Dapper;
using Microsoft.Extensions.Options;

namespace ConvenientShop.API.Services
{
    public class ReportRepository : ConvenientStoreRepository, Interfaces.IReportRepository
    {
        public ReportRepository(IOptions<StoreConfig> config) : base(config) { }

        public object GetDataForImportExport(char timespan, DateTime time)
        {
            throw new NotImplementedException();
        }

        public object GetRevenue(char timespan, DateTime date)
        {
            using(var conn = Connection)
            {
                conn.Open();

                var param = new DynamicParameters();
                param.Add("date", dbType : DbType.Date, value : date);
                param.Add("revenue", dbType : DbType.Int32, direction : ParameterDirection.Output);
                switch (timespan)
                {
                    case 'd':
                        {
                            var sql = "USP_RevenueByDay";
                            var result = conn.Query<object>(
                                sql,
                                param : param,
                                commandType : CommandType.StoredProcedure
                            );
                            return new { result, revenue = param.Get<object>("revenue") };
                        }
                    case 'm':
                        {
                            return null;
                        }
                    case 'y':
                        {
                            return null;
                        }
                    default:
                        return null;
                }
            }
        }
    }
}