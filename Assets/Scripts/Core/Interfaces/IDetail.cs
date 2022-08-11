using UnityEngine;

namespace PlanetGearScheme.Core.Interfaces {
    public interface IDetail {
        public string DetailName { get; }

        public GameObject Prefab { get; }
    }
}
