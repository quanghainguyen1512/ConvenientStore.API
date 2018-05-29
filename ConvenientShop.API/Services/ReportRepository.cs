using System;
using System.Data;
using System.Linq;
using ConvenientShop.API.Models;
using Dapper;
using Microsoft.Extensions.Options;

namespace ConvenientShop.API.Services
{
    public class ReportRepository : ConvenientStoreRepository, Interfaces.IReportRepository
    {
        public ReportRepository(IOptions<StoreConfig> config) : base(config) { }

        public object GetDataForImportExport(char timespan, DateTime date, int productId)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var param = new DynamicParameters();
                param.Add("date", dbType : DbType.Date, value : date);
                param.Add("productId", dbType : DbType.Int32, value : productId);
                param.Add("printTables", dbType : DbType.Boolean, value : true);
                param.Add("imp", dbType : DbType.Int32, direction : ParameterDirection.Output);
                param.Add("exp", dbType : DbType.Int32, direction : ParameterDirection.Output);

                switch (timespan)
                {
                    case 'd':
                        {
                            var sql = "USP_ImExReportByDay";
                            var result = conn.QueryMultiple(
                                sql,
                                param : param,
                                commandType : CommandType.StoredProcedure
                            );
                            return new
                            {
                                importList = result.Read<ImportDto>(),
                                    exportList = result.Read<ExportDto>(),
                                    imp = param.Get<object>("imp"),
                                    exp = param.Get<object>("exp")
                            };
                        }
                    case 'm':
                        {
                            var sql = "USP_ImExReportByMonth";
                            var result = conn.Query(
                                sql,
                                param : param,
                                commandType : CommandType.StoredProcedure
                            );
                            return new
                            {
                                result,
                                imp = param.Get<object>("imp"),
                                exp = param.Get<object>("exp")
                            };
                        }
                    case 'y':
                        {
                            var sql = "USP_ImExReportByYear";
                            var result = conn.Query(
                                sql,
                                param : param,
                                commandType : CommandType.StoredProcedure
                            );
                            return new
                            {
                                result,
                                imp = param.Get<object>("imp"),
                                exp = param.Get<object>("exp")
                            };
                        }
                    default:
                        return null;
                }
            }
        }

        public object GetRevenue(char timespan, DateTime date)
        {
            using(var conn = Connection)
            {
                conn.Open();

                var param = new DynamicParameters();
                param.Add("date", dbType : DbType.Date, value : date);
                param.Add("printTables", dbType : DbType.Boolean, value : true);
                param.Add("in", dbType : DbType.Int32, direction : ParameterDirection.Output);
                param.Add("out", dbType : DbType.Int32, direction : ParameterDirection.Output);
                param.Add("revenue", dbType : DbType.Int32, direction : ParameterDirection.Output);
                switch (timespan)
                {
                    case 'd':
                        {
                            var sql = "USP_RevenueByDay";
                            var result = conn.QueryMultiple(
                                sql,
                                param : param,
                                commandType : CommandType.StoredProcedure
                            );
                            return new
                            {
                                bills = result.Read<BillForRevenueDto>(),
                                    delivery = result.Read<DeliveryDto>(),
                                    _in = param.Get<object>("in"),
                                    _out = param.Get<object>("out"),
                                    revenue = param.Get<object>("revenue")
                            };
                        }
                    case 'm':
                        {
                            var sql = "USP_RevenueByMonth";
                            var result = conn.Query(
                                sql,
                                param : param,
                                commandType : CommandType.StoredProcedure
                            );
                            return new
                            {
                                result,
                                _in = param.Get<object>("in"),
                                _out = param.Get<object>("out"),
                                revenue = param.Get<object>("revenue")
                            };
                        }
                    case 'y':
                        {
                            var sql = "USP_RevenueByYear";
                            var result = conn.Query(
                                sql,
                                param : param,
                                commandType : CommandType.StoredProcedure
                            );
                            return new
                            {
                                result,
                                _in = param.Get<object>("in"),
                                _out = param.Get<object>("out"),
                                revenue = param.Get<object>("revenue")
                            };
                        }
                    default:
                        return null;
                }
            }
        }
    }
}