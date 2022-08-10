using PlanetGearScheme.Core.Data;
using PlanetGearScheme.Core.Disposables;
using PlanetGearScheme.Core.Interfaces;
using TMPro;
using UnityEngine;

namespace PlanetGearScheme.View.UI.Widgets {
    public class PartWidget : MonoBehaviour, IItemRenderer<PlanetarnyReductorDetail> {
        [SerializeField] private TMP_Text label;

        public delegate void OnChange(PlanetarnyReductorDetail partData);

        private event OnChange OnChangeEvent;

        private PlanetarnyReductorDetail _data;

        public void OnClick()
            => OnChangeEvent?.Invoke(_data);

        public void SetData(PlanetarnyReductorDetail data) {
            _data = data;

            UpdateView();
        }

        private void UpdateView() {
            label.text = _data.PartName;
        }

        public ActionDisposable SubscribeOnChange(OnChange call) {
            OnChangeEvent += call;

            return new ActionDisposable(() => { OnChangeEvent -= call; });
        }
    }
}
