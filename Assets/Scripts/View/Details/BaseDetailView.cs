using System;
using System.Collections;
using Cinemachine;
using PlanetGearScheme.Core.Dictionares;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlanetGearScheme.View.Details {
    [RequireComponent(
        typeof(PlayerInput),
        typeof(Animator)
    )]
    public abstract class BaseDetailView : MonoBehaviour {
        [SerializeField] protected Transform root;
        [SerializeField] protected float delayBeforeShowParts = .5f;

        protected Animator DetailAnimator;

        private CinemachineFreeLook _mainViewCamera;
        private CinemachineFreeLook _reviewCamera;
        private CinemachineFreeLook _partViewCamera;
        private CinemachineFreeLook _currentFreeLookCamera;
        private Transform _startTransform;
        private Vector2 _startCameraAxis;
        private Animator _stateDrivenCameraAnimator;
        private CinemachineStateDrivenCamera _stateDrivenCamera;
        private Coroutine _reviewCameraCoroutine;

        protected string CurrentPartName;
        protected bool LockRotate;
        protected bool IsExpanded;
        protected bool IsAnimated;

        private bool _isHoldPointer;

        protected virtual void Awake() {
            _startTransform = transform;

            DetailAnimator = GetComponent<Animator>();
        }

        public void SetCamers(
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

            _startCameraAxis = new Vector2(
                _currentFreeLookCamera.m_XAxis.Value,
                _currentFreeLookCamera.m_YAxis.Value
            );
        }

#region AnimatorCallbackEvents

        public void OnAnimationSetReviewCamera()
            => SetReviewCamera();

        public void OnAnimationSetMainViewCamera()
            => SetMainViewCamera();

        public void OnAnimationUnlockRotate()
            => LockRotate = false;

        public void OnAnimationEnd()
            => IsAnimated = false;

#endregion

#region UserInputActions

        public void OnHoldPointer(InputValue inputValue)
            => _isHoldPointer = inputValue.isPressed;

        public void OnLook(InputValue inputValue) {
            if (LockRotate || !enabled) {
                return;
            }

            var axisValue = Vector2.zero;

            if (_isHoldPointer) {
                axisValue = inputValue.Get<Vector2>();
            }

            SetCameraAxis(_currentFreeLookCamera, axisValue, true);
        }

#endregion

        protected void ResetPartState(CinemachineFreeLook targetCamera, bool showChilds = true) {
            CurrentPartName = null;
            _currentFreeLookCamera = targetCamera;

            SetCameraAxis(targetCamera, _startCameraAxis, false);
            SetFollowTarget(_startTransform);

            if (showChilds) {
                SetChildView(true);
            }
        }

        protected void SetMainViewCamera() {
            ResetPartState(_mainViewCamera);

            SetCameraAnimatorState(StateDrivenCameraAnimatorConstants.MainViewState);
        }

        protected void SetReviewCamera(Action callback = null) {
            TryStopCoroutine(ref _reviewCameraCoroutine);

            ResetPartState(_reviewCamera, false);

            SetCameraAnimatorState(StateDrivenCameraAnimatorConstants.ReviewState);

            _reviewCameraCoroutine = StartCoroutine(AfterSetReviewState(callback));
        }

        private IEnumerator AfterSetReviewState(Action callback) {
            yield return new WaitForSeconds(delayBeforeShowParts);

            SetChildView(true);

            callback?.Invoke();
        }

        protected void SetPartCamera(Transform partTransform) {
            SetCameraAxis(_partViewCamera, _startCameraAxis, false);
            SetFollowTarget(partTransform);
            SetChildView(false);

            partTransform.gameObject.SetActive(true);

            SetCameraAnimatorState(StateDrivenCameraAnimatorConstants.PartViewState);

            LockRotate = false;

            _currentFreeLookCamera = _partViewCamera;
        }

        private void SetFollowTarget(Transform target)
            => _stateDrivenCamera.LookAt = _stateDrivenCamera.Follow = target;

        private void SetCameraAxis(CinemachineFreeLook targetCamera, Vector2 axisValue, bool inputValue) {
            if (inputValue) {
                targetCamera.m_XAxis.m_InputAxisValue = axisValue.x;
                targetCamera.m_YAxis.m_InputAxisValue = axisValue.y;

                return;
            }

            targetCamera.m_XAxis.Value = axisValue.x;
            targetCamera.m_YAxis.Value = axisValue.y;
        }

        private void SetChildView(bool value) {
            for (var i = 0; i < root.childCount; i++) {
                root.GetChild(i).gameObject.SetActive(value);
            }
        }

        private void SetCameraAnimatorState(string state)
            => _stateDrivenCameraAnimator.CrossFade(state, 0);

        private void TryStopCoroutine(ref Coroutine targetCoroutine) {
            if (targetCoroutine == null) {
                return;
            }

            StopCoroutine(targetCoroutine);
            targetCoroutine = null;
        }
    }
}
