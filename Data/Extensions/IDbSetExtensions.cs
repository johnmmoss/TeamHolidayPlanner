using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace TeamHolidayPlanner.Data.Extensions
{
    public static class IDbSetExtensions
    {
        //TODO Figure out how to seed enum data
        //public static void SeedEnumValues<T, TEnum>(this DbSet<T> dbSet, Func<TEnum, T> converter)
        //                        where T : class => Enum.GetValues(typeof(TEnum))
        //                           .Cast<object>()
        //                           .Select(value => converter((TEnum)value))
        //                           .ToList()
        //                           .ForEach(instance => dbSet.AddOr);
    }
}
