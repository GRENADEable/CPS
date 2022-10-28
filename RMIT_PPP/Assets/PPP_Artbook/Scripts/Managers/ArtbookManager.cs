using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;
using TMPro;

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
        [Tooltip("Production Datas")]
        private ProductionData[] productionDatas = default;

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

        [SerializeField]
        [Tooltip("Production Slider")]
        private Slider productionSlider = default;

        [SerializeField]
        [Tooltip("Production Image")]
        private Image productionImg = default;

        [SerializeField]
        [Tooltip("Production Dates")]
        private TextMeshProUGUI productionDateText = default;
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

        #endregion

        #region Private Variables
        private CinemachineTrackedDolly _vCamTrackDolly = default;
        #endregion

        #region Unity Callbacks

        void Start()
        {
            _vCamTrackDolly = vCamDolly.GetCinemachineComponent<CinemachineTrackedDolly>();
            StartCoroutine(StartDelay());

            for (int i = 0; i < circularButtons.Length; i++)
                circularButtons[i].transform.DOScale(0.8f, 0.7f).SetLoops(-1, LoopType.Yoyo);

            SetProductionTimelineValues();
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
        /// Tied to Timeline_Horizontal_Scrollbar UI;
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

        /// <summary>
        /// Tied to Production_Slider UI;
        /// Changes image depending on how many sprites there are in the Array;
        /// </summary>
        /// <param name="scrollVal"> Scroll Value from the UI; </param>
        public void OnProductionTimelineScrollValChange(float scrollVal)
        {
            productionImg.sprite = productionDatas[(int)scrollVal].productionImg;
            productionDateText.text = productionDatas[(int)scrollVal].productionDate;
        }

        /// <summary>
        /// Intitalises the production panel timeline;
        /// </summary>
        void SetProductionTimelineValues()
        {
            productionSlider.handleRect.transform.DOScale(0.7f, 0.3f).SetLoops(-1, LoopType.Yoyo);
            productionSlider.maxValue = productionDatas.Length - 1;

            productionImg.sprite = productionDatas[0].productionImg;
            productionDateText.text = productionDatas[0].productionDate;
        }
        #endregion

        #region Coroutines
        /// <summary>
        /// Starts scene with delay;
        /// </summary>
        /// <returns> float delay; </returns>
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

    }
}