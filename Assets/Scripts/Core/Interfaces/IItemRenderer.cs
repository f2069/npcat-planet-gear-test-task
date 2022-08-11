namespace PlanetGearScheme.Core.Interfaces {
    public interface IItemRenderer<in TDataType> {
        public void SetData(TDataType data);
        public void Active();
    }
}
