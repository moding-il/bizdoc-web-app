using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BizDoc.ComponentModel.Annotations;
using BizDoc.ComponentModel.Resolvers;
using BizDoc.Configuration;
using BizDoc.Configuration.Annotations;
using BizDoc.Configuration.Types;
using BizDoc.Core.Http;
using Newtonsoft.Json;

namespace WebApp.Models
{
    [Template("app-my-form")]
    public class MyFormModel
    {
        public MyFormModel()
        {
            Due = DateTime.Today;
            Lines = Array.Empty<Line>();
        }
        [Subject, Required]
        public string Subject { get; set; }
        [Value, JsonIgnore]
        public decimal? Total => Lines?.Sum(l => l.Amount);
        public DateTime Due { get; set; }
        public Line[] Lines { get; set; }
        [CubeMapping(typeof(MyCube), nameof(Amount), nameof(Balance), nameof(Year))]
        public class Line
        {
            [ValueResolver(typeof(StateAxisResolver<Balance>))]
            public Balance? Balance { get; set; }
            public DateTime Due { get; set; }
            [JsonIgnore, ListType(typeof(Years))]
            public short Year => (short)Due.Year;
            public decimal Amount { get; set; }
        }
    }

    [Form(Title = "My form")]
    public class MyForm : FormBase<MyFormModel>
    {
        private readonly IHttpContext _context;

        public MyForm(IHttpContext context)
        {
            _context = context;
        }

        public override Task FlowStartAsync(MyFormModel model) => Task.CompletedTask;
    }
}
