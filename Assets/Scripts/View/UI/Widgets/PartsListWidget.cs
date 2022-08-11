using System.Collections.Generic;
using PlanetGearScheme.Core.Disposables;
using PlanetGearScheme.Core.Interfaces;
using TMPro;
using UnityEngine;

namespace PlanetGearScheme.View.UI.Widgets {
    public class PartsListWidget : MonoBehaviour, IListRenderer<IDetail, IDetailPart> {
        [SerializeField] private TMP_Text mainLabel;
        [SerializeField] private Transform container;
        [SerializeField] private Transform itemPrefab;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private bool _listState;

        public delegate void OnSwitchList(bool listState);

        public delegate void OnSelectPart(IDetailPart partData);

        private event OnSwitchList OnSwitchListEvent;
        private event OnSelectPart OnSelectPartEvent;

        private void Awake() {
            itemPrefab.gameObject.SetActive(false);
        }

        private void OnDestroy()
            => _trash.Dispose();

        private void ChangeActivePart<TItemType>(TItemType partData) where TItemType : IDetailPart
            => OnSelectPartEvent?.Invoke(partData);

        public ActionDisposable SubscribeOnSwitchList(OnSwitchList call) {
            OnSwitchListEvent += call;

            return new ActionDisposable(() => { OnSwitchListEvent -= call; });
        }

        public ActionDisposable SubscribeOnOnSelectPart(OnSelectPart call) {
            OnSelectPartEvent += call;

            return new ActionDisposable(() => { OnSelectPartEvent -= call; });
        }

        public void OnToggleList() {
            _listState = !_listState;

            container.gameObject.SetActive(_listState);

            OnSwitchListEvent?.Invoke(_listState);
        }

        public void SetData<TItemType>(
            IDetail data,
            List<TItemType> detailParts
        ) where TItemType : IDetailPart {
            mainLabel.text = data.DetailName;

            foreach (var partData in detailParts) {
                if (partData.HideInMenu) {
                    continue;
                }

                var partWidget = Instantiate(itemPrefab, container)
                    .GetComponent<PartWidget>();

                _trash.Retain(partWidget.SubscribeOnChange(ChangeActivePart));

                partWidget.SetData(partData);
                partWidget.Active();
            }
        }
    }
}
