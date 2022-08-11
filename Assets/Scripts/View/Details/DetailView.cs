using PlanetGearScheme.Core.Data;
using PlanetGearScheme.Core.Dictionares;

namespace PlanetGearScheme.View.Details {
    public class DetailView : BaseDetailView {
        public void DisassembleDetail(bool expandDetail) {
            if (IsExpanded == expandDetail || IsAnimated) {
                return;
            }

            IsAnimated = true;
            LockRotate = true;
            IsExpanded = expandDetail;

            if (CurrentPartName != null && !IsExpanded) {
                SetReviewCamera(AnimateExplosionDetail);
            } else {
                AnimateExplosionDetail();
            }
        }

        public void SelectPart(PlanetarnyReductorDetail partData) {
            if (IsAnimated) {
                return;
            }

            if (CurrentPartName == partData.ObjectName) {
                LockRotate = true;
                IsAnimated = true;

                SetReviewCamera(() => { IsAnimated = false; });

                return;
            }

            CurrentPartName = partData.ObjectName;

            var partTransform = root.Find(partData.ObjectName);
            SetPartCamera(partTransform);
        }

        private void AnimateExplosionDetail() {
            DetailAnimator.SetBool(DetailAnimatorConstants.SplitAnimationKey, IsExpanded);
        }
    }
}
