using BizDoc.Configuration;
using BizDoc.Configuration.Annotations;
using BizDoc.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class MyReportDataModel
    {
        [Key]
        public string Number { get; set; }
        public decimal? Amount { get; set; }
    }
    public struct MyReportArgsModel
    {
        public DateTime Starting { get; set; }
    }
    [Report(Title = "My report")]
    public class MyReport : ReportBase<MyReportDataModel, MyReportArgsModel>
    {
        private readonly Store _store;

        public MyReport(Store store)
        {
            _store = store;
        }

        public override async Task<IEnumerable<MyReportDataModel>> PopulateAsync(MyReportArgsModel args, IProgressUpdate progress)=>
            await _store.Documents.Where(d => d.Log.Any(l => l.Time >= args.Starting)).Select(d => new MyReportDataModel
            {
                Number = d.Number,
                Amount = d.Value
            }).ToListAsync();
    }
}
