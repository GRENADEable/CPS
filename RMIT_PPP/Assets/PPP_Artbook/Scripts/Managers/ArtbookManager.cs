using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem;

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
        [Tooltip("Pause Panel")]
        private GameObject pausePanel = default;

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

        [Tooltip("Menu Button in an Array that will be used to disable them when clicking on other Buttons")]
        [SerializeField]
        private Button[] menuButtons;
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

        #region Buttons
        /// <summary>
        /// Function tied with Resume_Button Button;
        /// Resumes the Game;
        /// </summary>
        public void OnClick_Resume()
        {
            if (gmData.currState == GameManagerArtbookData.GameState.Paused)
                gmData.ChangeGameState("Game");

            pausePanel.SetActive(false);
            gmData.TogglePause(false);
        }

        /// <summary>
        /// Function tied with Restart_Button Button;
        /// Restarts the game with a delay;
        /// </summary>
        public void OnClick_Restart() => StartCoroutine(RestartDelay());

        /// <summary>
        /// Button tied with Menu_Button;
        /// Goes to the Menu with a delay;
        /// </summary>
        public void OnClick_Menu() => StartCoroutine(MenuDelay());

        /// <summary>
        /// Button tied with Quit_Button;
        /// Quits the Game
        /// </summary>
        public void OnClick_Quit() => StartCoroutine(QuitGameDelay());

        /// <summary>
        /// Tied to the UI Buttons;
        /// All the buttons added in the Array gets disabled;
        /// </summary>
        public void OnClick_DisableButtons()
        {
            for (int i = 0; i < menuButtons.Length; i++)
                menuButtons[i].interactable = false;
        }
        #endregion

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

        #region Buttons
        /// <summary>
        /// Restarts the game with a Delay;
        /// </summary>
        /// <returns> Float Delay; </returns>
        IEnumerator RestartDelay()
        {
            gmData.TogglePause(false);
            fadeBG.Play("Fade_Out");
            yield return new WaitForSeconds(0.5f);
            gmData.ChangeLevel(Application.loadedLevel);
        }

        /// <summary>
        /// Goes to Menu with a Delay;
        /// </summary>
        /// <returns> Float Delay; </returns>
        IEnumerator MenuDelay()
        {
            gmData.TogglePause(false);
            fadeBG.Play("Fade_Out");
            yield return new WaitForSeconds(0.5f);
            gmData.ChangeLevel(0);
        }

        /// <summary>
        /// Quits the game with a Delay;
        /// </summary>
        /// <returns> Float Delay </returns>
        IEnumerator QuitGameDelay()
        {
            gmData.TogglePause(false);
            fadeBG.Play("Fade_Out");
            yield return new WaitForSeconds(0.5f);
            gmData.QuitGame();
        }
        #endregion

        #endregion

        #region Events
        //public void OnPlayerPause(InputAction.CallbackContext context)
        //{
        //    if (gmData.currState != GameManagerArtbookData.GameState.Paused &&
        //        gmData.currState != GameManagerArtbookData.GameState.Intro)
        //    {
        //        if (context.started)
        //            pausePanel.SetActive(true);
        //    }
        //}
        #endregion

    }
}