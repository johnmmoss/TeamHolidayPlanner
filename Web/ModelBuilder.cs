using System.Collections.Generic;

namespace TeamHolidayPlanner.Web
{
    public class IModelEntityBuilder : IModelBuilder
    {
        public M BuildModel<E,M>(E entity)
        {
            return AutoMapper.Mapper.Map<M>(entity);
        }

        public IEnumerable<M> BuildModelList<M, E>(IEnumerable<E> entities)
        {
            return AutoMapper.Mapper.Map<IEnumerable<M>>(entities);
        }
    }
}
