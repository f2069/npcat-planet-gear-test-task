using PlanetGearScheme.Core.Data;
using PlanetGearScheme.Core.Dictionares;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlanetGearScheme.View.Details {
    [RequireComponent(
        typeof(PlayerInput),
        typeof(Animator)
    )]
    public class DetailView : BaseDetailView {
        public void SetStateView(bool expandDetail) {
            if (_isExpanded == expandDetail || _isAnimated) {
                return;
            }

            _isAnimated = true;
            _lockRotate = true;
            _isExpanded = expandDetail;

            if (!_isExpanded) {
                SetCameraAxis(_mainViewCamera, _startCameraAxis, false);
                SetFollowTarget(_startTransform);
                SetChildView(true);
            }

            _animator.SetBool(DetailAnimatorConstants.SplitAnimationKey, _isExpanded);
        }

        public void SelectPart(PlanetarnyReductorDetail partData) {
            if (Equals(_currentPart, partData)) {
                _lockRotate = true;

                SetCameraAxis(_reviewCamera, _startCameraAxis, false);
                SetFollowTarget(_startTransform);
                SetChildView(true);

                OnAnimationSetReviewCamera();

                return;
            }

            _currentPart = partData;
            SetCameraAxis(_partViewCamera, _startCameraAxis, false);
            SetChildView(false);

            var partGo = root.Find(partData.ObjectName);
            partGo.gameObject.SetActive(true);

            SetFollowTarget(partGo);

            _stateDrivenCameraAnimator.CrossFade(StateDrivenCameraAnimatorConstants.PartViewState, 0);

            _lockRotate = false;

            _currentFreeLookCamera = _partViewCamera;
        }
    }
}
