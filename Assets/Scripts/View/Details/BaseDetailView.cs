using Cinemachine;
using PlanetGearScheme.Core.Data;
using PlanetGearScheme.Core.Dictionares;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlanetGearScheme.View.Details {
    public abstract class BaseDetailView : MonoBehaviour {
        [SerializeField] protected Transform root;

        protected Animator _animator;
        protected Animator _stateDrivenCameraAnimator;
        protected CinemachineStateDrivenCamera _stateDrivenCamera;
        protected CinemachineFreeLook _mainViewCamera;
        protected CinemachineFreeLook _reviewCamera;
        protected CinemachineFreeLook _partViewCamera;
        protected CinemachineFreeLook _currentFreeLookCamera;
        protected Transform _startTransform;
        protected Vector2 _startCameraAxis;
        protected PlanetarnyReductorDetail? _currentPart;

        protected bool _lockRotate;
        protected bool _isExpanded;
        protected bool _isHoldPointer;
        protected bool _isAnimated;

        protected virtual void Awake() {
            _startTransform = transform;

            _animator = GetComponent<Animator>();
        }

        public void SetCameras(
            CinemachineStateDrivenCamera drivenCamera,
            CinemachineFreeLook mainViewCamera,
            CinemachineFreeLook reviewCamera,
            CinemachineFreeLook partViewCamera
        ) {
            _stateDrivenCamera = drivenCamera;
            _stateDrivenCameraAnimator = _stateDrivenCamera.GetComponent<Animator>();

            _mainViewCamera = _currentFreeLookCamera = mainViewCamera;
            _reviewCamera = reviewCamera;
            _partViewCamera = partViewCamera;

            foreach (var cinemachineVirtualCameraBase in _stateDrivenCamera.ChildCameras) {
                var virtualCamera = (CinemachineFreeLook) cinemachineVirtualCameraBase;

                virtualCamera.m_XAxis.m_InputAxisName = "";
                virtualCamera.m_YAxis.m_InputAxisName = "";
            }

            SetFollowTarget(_startTransform);

            _startCameraAxis = new Vector2(_currentFreeLookCamera.m_XAxis.Value, _currentFreeLookCamera.m_YAxis.Value);
        }

#region AnimatorCallbacks

        public void OnAnimationSetReviewCamera() {
            _currentPart = null;
            _currentFreeLookCamera = _reviewCamera;
            _stateDrivenCameraAnimator.CrossFade(StateDrivenCameraAnimatorConstants.ReviewState, 0);
        }

        public void OnAnimationSetPartCamera() {
            _currentFreeLookCamera = _mainViewCamera;
            SetCameraAxis(_currentFreeLookCamera, _startCameraAxis, false);

            _stateDrivenCameraAnimator.CrossFade(StateDrivenCameraAnimatorConstants.MainViewState, 0);
        }

        public void OnAnimationUnlockRotate()
            => _lockRotate = false;

        public void OnAnimationEnd()
            => _isAnimated = false;

#endregion

#region UserInput

        public void OnHoldPointer(InputValue inputValue)
            => _isHoldPointer = inputValue.isPressed;

        public void OnLook(InputValue inputValue) {
            if (_lockRotate || !enabled) {
                return;
            }

            var axisValue = Vector2.zero;

            if (_isHoldPointer) {
                axisValue = inputValue.Get<Vector2>();
            }

            SetCameraAxis(_currentFreeLookCamera, axisValue, true);
        }

#endregion

        protected void SetFollowTarget(Transform target)
            => _stateDrivenCamera.LookAt = _stateDrivenCamera.Follow = target;

        protected void SetCameraAxis(CinemachineFreeLook targetCamera, Vector2 axisValue, bool inputValue) {
            if (inputValue) {
                targetCamera.m_XAxis.m_InputAxisValue = axisValue.x;
                targetCamera.m_YAxis.m_InputAxisValue = axisValue.y;

                return;
            }

            targetCamera.m_XAxis.Value = axisValue.x;
            targetCamera.m_YAxis.Value = axisValue.y;
        }

        protected void SetChildView(bool value) {
            for (var i = 0; i < root.childCount; i++) {
                root.GetChild(i).gameObject.SetActive(value);
            }
        }
    }
}
