using PlanetGearScheme.Core.Disposables;
using PlanetGearScheme.Core.Interfaces;
using TMPro;
using UnityEngine;

namespace PlanetGearScheme.View.UI.Widgets {
    public class PartWidget : MonoBehaviour, IItemRenderer<IDetailPart> {
        [SerializeField] private TMP_Text label;

        public delegate void OnChange(IDetailPart partData);

        private event OnChange OnChangeEvent;

        private IDetailPart _data;

        public void OnClick()
            => OnChangeEvent?.Invoke(_data);

        public void SetData(IDetailPart data) {
            _data = data;

            UpdateView();
        }

        public void Active()
            => gameObject.SetActive(true);

        private void UpdateView() {
            label.text = _data.PartName;
        }

        public ActionDisposable SubscribeOnChange(OnChange call) {
            OnChangeEvent += call;

            return new ActionDisposable(() => { OnChangeEvent -= call; });
        }
    }
}
