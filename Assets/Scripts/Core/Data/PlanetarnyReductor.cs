using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGearScheme.Core.Data {
    [CreateAssetMenu(fileName = "PlanetarnyReductor", menuName = "Details/Planetarny reductor parts", order = 0)]
    public class PlanetarnyReductor : ScriptableObject {
        [SerializeField] private string detailName;
        [SerializeField] private GameObject prefab;
        [SerializeField] private List<PlanetarnyReductorDetail> parts;

        public string DetailName => detailName;

        public GameObject Prefab => prefab;

        public List<PlanetarnyReductorDetail> Parts => parts;
    }

    [Serializable]
    public struct PlanetarnyReductorDetail {
        [SerializeField] private string partName;
        [SerializeField] private string objectName;
        [SerializeField] private bool hideInMenu;

        public string PartName => partName;

        public string ObjectName => objectName;
        public bool HideInMenu => hideInMenu;
    }
}
