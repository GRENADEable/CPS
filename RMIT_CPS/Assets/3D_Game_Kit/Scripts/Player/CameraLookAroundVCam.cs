using UnityEngine;

namespace RedMountMedia
{
    public class CameraLookAroundVCam : MonoBehaviour
    {
        #region Serialized Variables
        [Space, Header("Data")]
        [SerializeField]
        [Tooltip("GameManager Scriptable Object")]
        private GameMangerData gmData = default;

        [Space, Header("Mouse Settings")]
        [SerializeField]
        [Tooltip("Minimum clamp on X Axis")]
        private float minXClamp = -90f;

        [SerializeField]
        [Tooltip("Maximum clamp on X Axis")]
        private float maxXClamp = 90f;

        [SerializeField]
        [Tooltip("Mouse sensitivity")]
        private float mouseSens = 300f;

        [SerializeField]
        [Tooltip("Transform Component of the root object")]
        private Transform playerRoot = default;

        [SerializeField]
        [Tooltip("Cinemachine Camera Target")]
        private GameObject CinemachineCameraTarget;
        #endregion

        #region Private Variables
        private float _xRotate = default;
        #endregion

        #region Unity Callbacks
        void Update()
        {
            if (gmData.currState == GameMangerData.GameState.Game)
                LookAround();
        }
        #endregion

        #region Functions
        void LookAround()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

            _xRotate -= mouseY;
            _xRotate = ClampAngle(_xRotate, minXClamp, maxXClamp);

            CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_xRotate, 0.0f, 0.0f);
            playerRoot.Rotate(Vector3.up * mouseX);
        }

        static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
        #endregion
    }
}