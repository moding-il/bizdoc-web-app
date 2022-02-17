using System.Linq;

namespace WebApp.Data
{
    public class SeedData
{
    private readonly CustomStore store;

    public SeedData(CustomStore store)
    {
        this.store = store;
    }

    public void Execute()
    {
        if (!store.Accounts.Any())
        {
            store.Accounts.AddRange(new Account { Id = "a", Name = "A!" },
                new Account { Id = "b", Name = "B!" });
        }
        store.SaveChanges();
    }
}
}
