using Library.Entities;
using Library.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Infrastructure.Extensions
{
    public static class IServiceExtensions
    {
        public static async Task<ApiUser> FindUserById(this IService service, LibraryDatabaseContext context, Guid guid) 
        {
            string id = guid.ToString();
            ApiUser user = await context.ApiUsers.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public static async Task<ApiUser> FindUserById(this IService service, LibraryDatabaseContext context, Guid guid, string include)
        {
            string id = guid.ToString();
            ApiUser user = await context.ApiUsers
                .Include(include)
                .FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }
    }
}
