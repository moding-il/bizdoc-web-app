using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BizDoc.Configuration;
using BizDoc.Configuration.Annotations;
using BizDoc.Core.Configuration.Models;
    using BizDoc.Core.Models;

namespace WebApp.Models
{
    public enum Balance
    {
        Open,
        Close
    }
    [Cube(Title = "My cube")]
    public class MyCube : CubeBase, CubeBase.IBrowsable<MyCube.PO>
    {
        Task<IEnumerable<PO>> IBrowsable<PO>.QueryAsync(params Axis[][] axes)
        {
            throw new System.NotImplementedException();
        }

        public class PO
        {

        }
    }
/// <summary>
/// Data source
/// </summary>
public class Accounts : TypeBase<string, string, string>
{
    private readonly CustomStore _store;

    public Accounts(CustomStore store)
    {
        _store = store;
    }
    
    public override Task<Dictionary<string, string>> GetValuesAsync(string args) => 
        _store.Accounts.ToDictionaryAsync(a => a.Id, a => a.Name);
}
}
