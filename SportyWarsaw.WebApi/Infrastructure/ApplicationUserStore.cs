using Microsoft.AspNet.Identity.EntityFramework;
using SportyWarsaw.Domain;
using SportyWarsaw.Domain.Entities;

namespace SportyWarsaw.WebApi.Infrastructure
{
    public class ApplicationUserStore : UserStore<User>
    {
        public ApplicationUserStore(SportyWarsawContext context)
            : base(context)
        {
        }
    }
}