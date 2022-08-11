using System.Collections.Generic;

namespace PlanetGearScheme.Core.Interfaces {
    public interface IListRenderer<in TDataType, in TItemType> {
        public void SetData<T>(
            TDataType data,
            List<T> detailParts
        ) where T : TItemType;
    }
}
