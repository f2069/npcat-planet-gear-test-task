using System;
using PlanetGearScheme.Core.Interfaces;
using UnityEngine;

namespace PlanetGearScheme.Core.Data {
    [CreateAssetMenu(fileName = "PlanetarnyReductor", menuName = "Details/Planetarny reductor parts", order = 0)]
    public class PlanetarnyReductor : BaseDetail<PlanetarnyReductorDetail> {
    }

    [Serializable]
    public struct PlanetarnyReductorDetail : IDetailPart {
        [SerializeField] private string partName;
        [SerializeField] private string objectName;
        [SerializeField] private bool hideInMenu;

        public string PartName => partName;
        public string ObjectName => objectName;
        public bool HideInMenu => hideInMenu;
    }
}
