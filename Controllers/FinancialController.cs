using BizDoc.Core.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FinancialController : Controller
    {
        private readonly Store _store;
        public FinancialController(Store store)
        {
            _store = store;
        }
        [HttpGet("{id}")]
        public FinancialModel Get(short id)
        {
            return new FinancialModel
            {
                Usage = _store.Cube.Where(c => c.Axis1 == id.ToString()).GroupBy(c => c.Axis2).
                ToDictionary(g => char.Parse(g.Key), g => g.GroupBy(c => c.Axis3).
               ToDictionary(p => byte.Parse(p.Key), p=> p.Sum(c=> c.Value))),
                Index = _store.Indices.Where(c => c.Axis1 == id.ToString()).GroupBy(c => c.Axis3).
                    ToDictionary(g => byte.Parse(g.Key), g=> g.Sum(c => c.Value))
            };
        }

        public class FinancialModel
        {
            public Dictionary<char, Dictionary<byte, decimal>> Usage { get; set; }
            public Dictionary<byte, decimal> Index { get; set; }
        }
    }
}
