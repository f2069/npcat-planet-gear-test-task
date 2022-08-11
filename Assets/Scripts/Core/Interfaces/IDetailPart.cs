namespace PlanetGearScheme.Core.Interfaces {
    public interface IDetailPart {
        public string PartName { get; }
        public string ObjectName { get; }
        public bool HideInMenu { get; }
    }
}
