using System.Collections.Generic;

namespace TeamHolidayPlanner.Web
{
    public interface IModelBuilder
    {
        M BuildModel<E, M>(E entity);

        IEnumerable<M> BuildModelList<M, E>(IEnumerable<E> entities);
    }
}
