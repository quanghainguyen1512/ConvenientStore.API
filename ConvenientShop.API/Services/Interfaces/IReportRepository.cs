using System;
using System.Collections.Generic;
using ConvenientShop.API.Entities;

namespace ConvenientShop.API.Services.Interfaces
{
    public interface IReportRepository
    {
        object GetRevenue(char timespan, DateTime time);
        object GetDataForImportExport(char timespan, DateTime time, int productId);
    }
}