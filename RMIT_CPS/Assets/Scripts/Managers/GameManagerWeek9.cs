using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerWeek9 : MonoBehaviour
{
    #region Serialized Variables

    #region Enums
    [Space, Header("Enums")]
    [SerializeField] private GameState _currGameState = GameState.Intro;
    private enum GameState { Intro, Game, Paused, Outro };
    #endregion

    #region UI
    [Space, Header("UI")]
    [SerializeField]
    [Tooltip("Pause Buttons")]
    private Button[] pauseButtons = default;

    [SerializeField]
    [Tooltip("Pause Panel")]
    private GameObject pausePanel;

    [SerializeField]
    [Tooltip("HUD Panel")]
    private GameObject hudPanel;

    [SerializeField]
    [Tooltip("Fade Image Animation Component")]
    private Animator fadeBG = default;

    [SerializeField]
    [Tooltip("Darken Image Component")]
    private Image darkenImg = default;

    [SerializeField]
    [Tooltip("Intro Text GameObject")]
    private GameObject introText = default;

    [SerializeField]
    [Tooltip("End Text GameObject")]
    private GameObject endText = default;
    #endregion

    #region Game
    [Space, Header("Game")]
    [SerializeField]
    [Tooltip("Platform Array")]
    private GameObject[] platforms = default;

    [SerializeField]
    [Tooltip("Sunlight")]
    private Light mainLight = default;

    [SerializeField]
    [Tooltip("Sunlight Intensity Increment")]
    private float lightIntensityIncrement = default;
    #endregion

    #endregion

    #region Private Variables
    private int _currPlatform = default;
    private float _currDarkenUI = default;
    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {
        FPSControllerBasicWeek9.OnPlayerDead += OnPlayerDeadEventReceived;

        TreeGrowth.OnTreeFullyGrown += OnTreeFullyGrownEventReceived;
        TreeGrowth.OnTreeFullyGrownFinal += OnTreeFullyGrownFinalEventReceived;
    }

    void OnDisable()
    {
        FPSControllerBasicWeek9.OnPlayerDead -= OnPlayerDeadEventReceived;

        TreeGrowth.OnTreeFullyGrown -= OnTreeFullyGrownEventReceived;
        TreeGrowth.OnTreeFullyGrownFinal -= OnTreeFullyGrownFinalEventReceived;
    }

    void OnDestroy()
    {
        FPSControllerBasicWeek9.OnPlayerDead -= OnPlayerDeadEventReceived;

        TreeGrowth.OnTreeFullyGrown -= OnTreeFullyGrownEventReceived;
        TreeGrowth.OnTreeFullyGrownFinal -= OnTreeFullyGrownFinalEventReceived;
    }
    #endregion

    void Start()
    {
        StartCoroutine(StartDelay());
        DisableCursor();
        _currDarkenUI = 0.9f;
#if UNITY_WEBGL
        // Disables the Quit button on WebGL;
        pauseButtons[3].interactable = false;
#endif
    }

    void Update()
    {
        if (_currGameState == GameState.Game)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                TogglePause(true);
        }
    }
    #endregion

    #region My Functions
    void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void EnableCursor()
    {
        Cursor.visible = transform;
        Cursor.lockState = CursorLockMode.None;
    }

    void ChangeScene(int index) => Application.LoadLevel(index);

    #region UI
    void TogglePause(bool isPaused)
    {
        if (isPaused)
        {
            _currGameState = GameState.Paused;
            EnableCursor();
            Time.timeScale = 0;
            hudPanel.SetActive(false);
            pausePanel.SetActive(true);
        }
        else
        {
            _currGameState = GameState.Game;
            DisableCursor();
            Time.timeScale = 1;
            hudPanel.SetActive(true);
            pausePanel.SetActive(false);
        }

    }

    #region Buttons
    /// <summary>
    /// Function tied with Resume_Button Button;
    /// Resumes the Game;
    /// </summary>
    public void OnClick_Resume() => TogglePause(false);

    /// <summary>
    /// Function tied with Restart_Button Button;
    /// Restarts the game with a delay;
    /// </summary>
    public void OnClick_Restart() => StartCoroutine(RestartGameDelay());

    /// <summary>
    /// Button tied with Menu_Button;
    /// Goes to the Menu with a delay;
    /// </summary>
    public void OnClick_Menu() => StartCoroutine(MenuDelay());

    /// <summary>
    /// Function tied with Quit_Button Buttons;
    /// Quits the game with a delay;
    /// </summary>
    public void OnClick_Quit() => StartCoroutine(QuitGameDelay());

    /// <summary>
    /// Function tied with Restart_Button, Menu_Button and Quit_Button Buttons;
    /// Disables the buttons so the Player can't interact with them when the panel is fading out;
    /// </summary>
    public void OnClick_DisableButtons()
    {
        for (int i = 0; i < pauseButtons.Length; i++)
            pauseButtons[i].interactable = false;
    }
    #endregion

    #endregion

    #endregion

    #region Coroutines

    #region UI
    /// <summary>
    /// Starts game with a Delay;
    /// </summary>
    /// <returns> Float Delay; </returns>
    IEnumerator StartDelay()
    {
        fadeBG.Play("Fade_In");
        _currGameState = GameState.Intro;
        yield return new WaitForSeconds(0.5f);
        _currGameState = GameState.Game;
    }

    /// <summary>
    /// Restarts the game with a Delay;
    /// </summary>
    /// <returns> Float Delay; </returns>
    IEnumerator RestartGameDelay()
    {
        TogglePause(false);
        fadeBG.Play("Fade_Out");
        _currGameState = GameState.Outro;
        yield return new WaitForSeconds(0.5f);
        ChangeScene(Application.loadedLevel);
    }

    /// <summary>
    /// Goes to Menu with a Delay;
    /// </summary>
    /// <returns> Float Delay; </returns>
    IEnumerator MenuDelay()
    {
        TogglePause(false);
        _currGameState = GameState.Outro;
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        ChangeScene(0);
    }

    /// <summary>
    /// Quits with a Delay;
    /// </summary>
    /// <returns> Float Delay; </returns>
    IEnumerator QuitGameDelay()
    {
        TogglePause(false);
        fadeBG.Play("Fade_Out");
        _currGameState = GameState.Outro;
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
    #endregion

    /// <summary>
    /// Ends with a Delay;
    /// </summary>
    /// <returns> Float Delay; </returns>
    IEnumerator EndGameDelay()
    {
        _currGameState = GameState.Outro;
        introText.SetActive(false);
        endText.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        ChangeScene(0);
    }

    #endregion

    #region Events

    /// <summary>
    /// Subbed to event from FPSControllerBasicWeek9 Script;
    /// Restarts the Game with delay;
    /// </summary>
    void OnPlayerDeadEventReceived() => StartCoroutine(RestartGameDelay());

    /// <summary>
    /// Subbed to event from FPSControllerBasicWeek9 Script;
    /// Changes the lighting and UI to be more clearer;
    /// </summary>
    void OnTreeFullyGrownEventReceived()
    {
        platforms[_currPlatform].SetActive(true);
        _currPlatform++;

        mainLight.intensity = Mathf.Clamp01(mainLight.intensity);
        mainLight.intensity += lightIntensityIncrement;

        _currDarkenUI -= 0.1f;
        Color darkColour = darkenImg.color;
        darkColour.a = _currDarkenUI;
        darkenImg.color = darkColour;
    }

    /// <summary>
    /// Subbed to event from FPSControllerBasicWeek9 Script;
    /// Ends the game with delay;
    /// </summary>
    void OnTreeFullyGrownFinalEventReceived()
    {
        mainLight.intensity = Mathf.Clamp01(mainLight.intensity);
        mainLight.intensity += lightIntensityIncrement;

        _currDarkenUI -= 0.1f;
        Color darkColour = darkenImg.color;
        darkColour.a = _currDarkenUI;
        darkenImg.color = darkColour;

        StartCoroutine(EndGameDelay());
    }
    #endregion
}