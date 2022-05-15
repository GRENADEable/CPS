using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GameManagerWeek10 : MonoBehaviour
{
    #region Serialized Variables

    #region Datas
    [Space, Header("Datas")]
    [SerializeField]
    [Tooltip("DarkenData Scriptable Object")]
    private DarkenData darkData = default;
    #endregion

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

    //[SerializeField]
    //[Tooltip("HUD Panel")]
    //private GameObject hudPanel;

    [SerializeField]
    [Tooltip("Win Panel")]
    private GameObject winPanel;

    [SerializeField]
    [Tooltip("Fade Image Animation Component")]
    private Animator fadeBG = default;

    [SerializeField]
    [Tooltip("Darken Image Component")]
    private Image darkenImg = default;
    #endregion

    #region Audio
    [Space, Header("Audio")]
    [SerializeField]
    [Tooltip("SFX Audio Source")]
    private AudioSource sfxAud = default;

    [SerializeField]
    [Tooltip("BG Audio Source")]
    private AudioSource bgAud = default;

    [SerializeField]
    [Tooltip("Arrays of SFXs")]
    private AudioClip[] sfxClips = default;

    [SerializeField]
    [Tooltip("BG Music Loop Delay")]
    private float bgAudLoopDelay = default;
    #endregion

    #endregion

    #region Private Variables
    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {
        FPSControllerBasicWeek10.OnScreenDarken += OnScreenDarkenEventReceived;
        FPSControllerBasicWeek10.OnPlayerDead += OnPlayerDeadEventReceived;
    }

    void OnDisable()
    {
        FPSControllerBasicWeek10.OnScreenDarken -= OnScreenDarkenEventReceived;
        FPSControllerBasicWeek10.OnPlayerDead -= OnPlayerDeadEventReceived;
    }

    void OnDestroy()
    {
        FPSControllerBasicWeek10.OnScreenDarken -= OnScreenDarkenEventReceived;
        FPSControllerBasicWeek10.OnPlayerDead -= OnPlayerDeadEventReceived;
    }
    #endregion

    void OnApplicationQuit() => darkData.darkenLevel = 0;

    void Start()
    {
        ChangeDarkenUI();
        StartCoroutine(StartDelay());
        StartCoroutine(BGAudLoop());
        DisableCursor();
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
            {
                TogglePause(true);
                sfxAud.PlayOneShot(sfxClips[1]);
            }
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
    public void OnGameWon() => StartCoroutine(EndGameDelay());

    void ChangeDarkenUI()
    {
        Color darkColour = darkenImg.color;
        darkColour.a = darkData.darkenLevel;
        darkenImg.color = darkColour;
    }

    void TogglePause(bool isPaused)
    {
        if (isPaused)
        {
            _currGameState = GameState.Paused;
            EnableCursor();
            Time.timeScale = 0;
            //hudPanel.SetActive(false);
            pausePanel.SetActive(true);
        }
        else
        {
            _currGameState = GameState.Game;
            DisableCursor();
            Time.timeScale = 1;
            //hudPanel.SetActive(true);
            pausePanel.SetActive(false);
        }

    }

    #region Buttons
    /// <summary>
    /// Function tied with Resume_Button Button;
    /// Resumes the Game;
    /// </summary>
    public void OnClick_Resume()
    {
        TogglePause(false);
        sfxAud.PlayOneShot(sfxClips[1]);
    }

    /// <summary>
    /// Function tied with Restart_Button Button;
    /// Restarts the game with a delay;
    /// </summary>
    public void OnClick_Restart()
    {
        sfxAud.PlayOneShot(sfxClips[1]);
        StartCoroutine(RestartGameDelay());
    }

    /// <summary>
    /// Button tied with Menu_Button;
    /// Goes to the Menu with a delay;
    /// </summary>
    public void OnClick_Menu()
    {
        sfxAud.PlayOneShot(sfxClips[1]);
        StartCoroutine(MenuDelay());
    }

    /// <summary>
    /// Function tied with Quit_Button Buttons;
    /// Quits the game with a delay;
    /// </summary>
    public void OnClick_Quit()
    {
        sfxAud.PlayOneShot(sfxClips[1]);
        StartCoroutine(QuitGameDelay());
    }

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
        sfxAud.PlayOneShot(sfxClips[0], 0.1f);
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

    /// <summary>
    /// Ends with a Delay;
    /// </summary>
    /// <returns> Float Delay; </returns>
    IEnumerator EndGameDelay()
    {
        winPanel.SetActive(true);
        _currGameState = GameState.Outro;
        darkData.darkenLevel = 0;
        yield return new WaitForSeconds(3.5f);
        fadeBG.Play("Fade_Out");
        ChangeScene(0);
    }
    #endregion

    #region Audio
    IEnumerator BGAudLoop()
    {
        yield return new WaitForSeconds(bgAudLoopDelay);
        bgAud.Play();
        StartCoroutine(BGAudLoop());
    }
    #endregion

    #endregion

    #region Events

    /// <summary>
    /// Subbed to event from FPSControllerBasicWeek10 Script
    /// Restarts the Game with delay;
    /// </summary>
    void OnPlayerDeadEventReceived()
    {
        if (darkData.darkenLevel >= 1)
        {
            darkData.darkenLevel = 0;
            StartCoroutine(MenuDelay());
        }
        else
            StartCoroutine(RestartGameDelay());
    }

    /// <summary>
    /// Subbed to event from FPSControllerBasicWeek10 Script
    /// Makes the screen go dark;
    /// </summary>
    void OnScreenDarkenEventReceived() => darkData.darkenLevel += 0.3f;
    #endregion
}