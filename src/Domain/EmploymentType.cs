namespace TeamHolidayPlanner.Domain
{
    public class EmploymentType : EnumBaseEntity<EmploymentTypeEnum>
    {
        public EmploymentType()
        {

        }

        public EmploymentType(EmploymentTypeEnum enumType) : base(enumType)
        {
        }

        public static implicit operator EmploymentType(EmploymentTypeEnum enumType) => new EmploymentType(enumType);
        public static implicit operator EmploymentTypeEnum(EmploymentType status) => status.ID;
    }
}
