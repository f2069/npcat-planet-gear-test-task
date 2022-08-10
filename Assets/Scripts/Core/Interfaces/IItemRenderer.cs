namespace PlanetGearScheme.Core.Interfaces {
    public interface IItemRenderer<in TDataType> {
        void SetData(TDataType data);
    }
}
