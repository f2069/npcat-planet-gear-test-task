using System.Collections.Generic;
using PlanetGearScheme.Core.Interfaces;
using UnityEngine;

namespace PlanetGearScheme.Core.Data {
    public class BaseDetail<T> : ScriptableObject, IDetail where T : IDetailPart {
        [SerializeField] private string detailName;
        [SerializeField] private GameObject prefab;
        [SerializeField] private List<T> parts;

        public string DetailName => detailName;
        public GameObject Prefab => prefab;
        public List<T> Parts => parts;
    }
}
