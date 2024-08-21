using Effortless.Net.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Shared
{
    public static class DevCode
    {
        public static string ToHash(this string password, string sharedKey)
        {
            return Hash.Create(HashType.SHA256, password, sharedKey, false);
        }
        public static IQueryable<TSource> Pagination<TSource>(this IQueryable<TSource> source, int pageNo, int pageSize)
        {
            return source
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
