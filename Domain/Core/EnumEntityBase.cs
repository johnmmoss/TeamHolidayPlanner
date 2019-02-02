using System;
using TeamHolidayPlanner.Domain.Extensions;

namespace TeamHolidayPlanner.Domain
{
    public class EnumBaseEntity<TEnum> where TEnum : struct
    {
        public TEnum ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        protected EnumBaseEntity() { }

        public EnumBaseEntity(TEnum enumType)
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new Exception($"Invalid generic method argument of type {typeof(TEnum)}");
            }

            ID = enumType;
            Name = enumType.ToString();
            Description = enumType.GetEnumDescription();
        }
    }
}
