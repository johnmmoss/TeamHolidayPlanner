using System;

namespace TeamHolidayPlanner.Domain
{
    public interface IModificationHistory
    {
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}
