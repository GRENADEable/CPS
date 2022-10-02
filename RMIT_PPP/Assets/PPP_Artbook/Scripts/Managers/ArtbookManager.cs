using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;

namespace Khatim.PPP
{
    public class ArtbookManager : MonoBehaviour
    {
        #region Serialized Variables

        #region Datas
        [Space, Header("Datas")]
        [SerializeField]
        [Tooltip("GameManager Data")]
        private GameManagerArtbookData gmData = default;

        [SerializeField]
        [Tooltip("Is Intro fade skipped?")]
        private bool isIntroSkipped = default;
        #endregion

        #region Cinemachine Cams
        [Space, Header("Cinemachine Cams")]
        [SerializeField]
        [Tooltip("VCam Dolly")]
        private CinemachineVirtualCamera vCamDolly;
        #endregion

        #region UIs
        [Space, Header("UIs")]
        [SerializeField]
        [Tooltip("Timeline Horizontal Scrollbar")]
        private ScrollRect timelineScrollRect = default;

        [SerializeField]
        [Tooltip("Circular Buttons")]
        private Button[] circularButtons = default;
        #endregion

        #region Animators
        [Space, Header("Animators")]
        [SerializeField]
        [Tooltip("Fade panel Animation Component")]
        private Animator fadeBG = default;
        #endregion

        #region Floats
        [Space, Header("Floats")]
        [SerializeField]
        [Tooltip("Dolly Scroll Speed")]
        private float dollyScrollSpeed = default;

        [SerializeField]
        [Tooltip("Coroutine Fade Delay")]
        private float fadeDelay = default;
        #endregion

        #region Int Events
        public delegate void SendEventsInt(int index);
        /// <summary>
        /// Event sent from ArtbookManager to InfoTab Scripts;
        /// Sends appropite index to show info;
        /// </summary>
        public static event SendEventsInt OnHoverPeekInfo;

        /// <summary>
        /// Event sent from ArtbookManager to InfoTab Scripts;
        /// Sends appropite index to hide info;
        /// </summary>
        public static event SendEventsInt OnHoverUnPeekInfo;
        #endregion

        #endregion

        #region Private Variables
        private CinemachineTrackedDolly _vCamTrackDolly = default;
        #endregion

        #region Unity Callbacks

        #region Events
        void OnEnable()
        {

        }

        void OnDisable()
        {

        }

        void OnDestroy()
        {

        }
        #endregion

        void Start()
        {
            _vCamTrackDolly = vCamDolly.GetCinemachineComponent<CinemachineTrackedDolly>();
            StartCoroutine(StartDelay());

            for (int i = 0; i < circularButtons.Length; i++)
                circularButtons[i].transform.DOScale(0.8f, 0.7f).SetLoops(-1, LoopType.Yoyo);
        }

        void Update()
        {
            if (gmData.currState == GameManagerArtbookData.GameState.Game)
            {

            }
        }
        #endregion

        #region My Functions
        /// <summary>
        /// Tied to Timeline_Horizontal_Scrollbar UIl;
        /// Moves the camera accross the dolly depending on the current scrolling value;
        /// </summary>
        /// <param name="scrollVal"> Scroll Value from the UI; </param>
        public void OnScrollValChange(float scrollVal)
        {
            if (gmData.currState == GameManagerArtbookData.GameState.Game)
            {
                _vCamTrackDolly.m_PathPosition = Mathf.Clamp(_vCamTrackDolly.m_PathPosition, 0, Mathf.Infinity);
                _vCamTrackDolly.m_PathPosition = scrollVal * dollyScrollSpeed;
            }
        }

        public void OnButtonHoverEnter(int buttonIndex)
        {
            OnHoverPeekInfo?.Invoke(buttonIndex);
            //Debug.Log($"Button{buttonIndex} Hover Enter");
        }

        public void OnButtonHoverExit(int buttonIndex)
        {
            OnHoverUnPeekInfo?.Invoke(buttonIndex);
            //Debug.Log($"Button{buttonIndex} Hover Exit");
        }
        #endregion

        #region Coroutines
        IEnumerator StartDelay()
        {
            if (isIntroSkipped)
            {
                gmData.ChangeGameState("Intro");
                yield return new WaitForSeconds(1f);
                timelineScrollRect.enabled = true;
                gmData.ChangeGameState("Game");
                fadeBG.Play("Fade_In");
            }
            else
            {
                gmData.ChangeGameState("Intro");
                yield return new WaitForSeconds(fadeDelay);
                timelineScrollRect.enabled = true;
                gmData.ChangeGameState("Game");
                fadeBG.Play("Intro_Fade_In");
            }
        }
        #endregion

        #region Events

        #endregion
    }
}