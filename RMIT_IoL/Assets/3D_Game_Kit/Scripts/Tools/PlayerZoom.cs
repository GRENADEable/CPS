using UnityEngine;
using Cinemachine;

namespace RedMountMedia
{
    public class PlayerZoom : MonoBehaviour
    {
        #region Serialized Variables
        [Space, Header("Data")]
        [SerializeField]
        [Tooltip("GameManager Scriptable Object")]
        private GameMangerData gmData = default;

        [SerializeField]
        [Tooltip("Can it depend on the GameManager Scriptable Object?")]
        private bool isUsingScriptableObject = default;

        #region Player Zoom
        [Space, Header("Player Zoom")]
        [SerializeField]
        [Tooltip("Cinemachine VCam")]
        private CinemachineVirtualCamera playerVCam = default;

        [SerializeField]
        [Tooltip("Which key to press when zooming")]
        private KeyCode zoomKey = KeyCode.Mouse1;

        [SerializeField]
        [Tooltip("How much FoV will it zoom into")]
        private float zoomFovVal = 25f;

        [Range(0f, 10f)]
        [SerializeField]
        [Tooltip("Zoom lerp speed")]
        private float lerpTime = 10f;
        #endregion

        #region Events
        public delegate void SendEventsBool(bool isZooming);
        /// <summary>
        /// Event sent from FPSController to FPSDefaultUI Scripts;
        /// This event just changes the variables when player zooms in or out;
        /// </summary>
        public static event SendEventsBool OnZoomInCam;
        #endregion

        #endregion

        #region Private Variables

        #region Player Zoom
        [Header("Player Zoom")]
        private float _currZoomFov;
        #endregion

        #endregion

        #region Unity Callbacks
        void Start() => _currZoomFov = playerVCam.m_Lens.FieldOfView;

        void Update()
        {
            if (isUsingScriptableObject)
            {
                if (gmData.currState == GameMangerData.GameState.Game)
                    PlayerZooming();
            }
            PlayerZooming();
        }
        #endregion

        #region My Functions
        /// <summary>
        /// Zoom using the player's Camera FoV;
        /// </summary>
        void PlayerZooming()
        {
            if (Input.GetKey(zoomKey))
                playerVCam.m_Lens.FieldOfView = Mathf.Lerp(playerVCam.m_Lens.FieldOfView, zoomFovVal, lerpTime * Time.deltaTime);
            else
                playerVCam.m_Lens.FieldOfView = Mathf.Lerp(playerVCam.m_Lens.FieldOfView, _currZoomFov, lerpTime * Time.deltaTime);

            // Placed a single frame key down for the event to be sent to the FPSDefaultUI Script;
            if (Input.GetKeyDown(zoomKey))
                OnZoomInCam?.Invoke(true);
            else if (Input.GetKeyUp(zoomKey))
                OnZoomInCam?.Invoke(false);
        }
        #endregion
    }
}