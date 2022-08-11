using Cinemachine;
using PlanetGearScheme.Core.Data;
using PlanetGearScheme.Core.Disposables;
using PlanetGearScheme.Core.Interfaces;
using PlanetGearScheme.View.Details;
using PlanetGearScheme.View.UI.Widgets;
using UnityEngine;

namespace PlanetGearScheme.View.Managers {
    public class LevelManager : MonoBehaviour {
        [SerializeField] private PlanetarnyReductor detailData;
        [SerializeField] private Transform detailSpawnPoint;
        [SerializeField] private PartsListWidget partsListWidget;

        [Space] [Header("Cameras")] [SerializeField]
        private CinemachineStateDrivenCamera stateDrivenCamera;

        [SerializeField] private CinemachineFreeLook mainViewCamera;
        [SerializeField] private CinemachineFreeLook reviewCamera;
        [SerializeField] private CinemachineFreeLook partViewCamera;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private DetailView _modelView;

        private void Awake() {
            _trash.Retain(partsListWidget.SubscribeOnSwitchList(OnToggleList));
            _trash.Retain(partsListWidget.SubscribeOnOnSelectPart(OnSelectPart));

            partsListWidget.SetData(detailData, detailData.Parts);
        }

        private void Start()
            => InitDetail();

        private void OnDestroy()
            => _trash.Dispose();

        private void InitDetail() {
            // @todo remove this
            if (detailSpawnPoint.transform.childCount > 0) {
                Destroy(detailSpawnPoint.GetChild(0).gameObject);
            }

            _modelView = Instantiate(detailData.Prefab, detailSpawnPoint)
                .GetComponent<DetailView>();

            _modelView.SetCamers(
                stateDrivenCamera,
                mainViewCamera,
                reviewCamera,
                partViewCamera
            );
        }

        private void OnToggleList(bool listState)
            => _modelView.DisassembleDetail(listState);

        private void OnSelectPart(IDetailPart partData)
            => _modelView.SelectPart(partData);
    }
}
