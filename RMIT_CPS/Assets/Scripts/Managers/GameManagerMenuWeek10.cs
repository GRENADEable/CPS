using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerMenuWeek10 : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField]
    [Tooltip("DarkenData Scriptable Object")]
    private DarkenData darkData = default;

    [SerializeField]
    [Tooltip("Fade Image Animation Component")]
    private Animator fadeBG = default;

    [SerializeField]
    [Tooltip("Buttons Components")]
    private Button[] menuButtons = default;

    [SerializeField]
    [Tooltip("Which scene to switch to?")]
    private int sceneNo = default;
    #endregion

    #region Unity Callbacks

    void OnApplicationQuit() => darkData.darkenLevel = 0;

    void Start()
    {
        fadeBG.Play("Fade_In");

#if UNITY_WEBGL
        menuButtons[1].interactable = false;
#endif
    }
    #endregion

    #region My Functions
    /// <summary>
    /// Tied to button Start_Button;
    /// Starts the Game;
    /// </summary>
    public void OnClick_StartGame() => StartCoroutine(StartGameDelay());

    /// <summary>
    /// Tied to button Quit_Button;
    /// Quits the Game;
    /// </summary>
    public void OnClick_QuitGame() => StartCoroutine(QuitGameDelay());

    public void OnClick_DisableButtons()
    {
        for (int i = 0; i < menuButtons.Length; i++)
            menuButtons[i].interactable = false;
    }
    #endregion

    #region Coroutines
    /// <summary>
    /// Starts the game with a delay;
    /// </summary>
    /// <returns> Float Delay; </returns>
    IEnumerator StartGameDelay()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel(1);
    }

    /// <summary>
    /// Quits the game with a delay;
    /// </summary>
    /// <returns> Float Delay; </returns>
    IEnumerator QuitGameDelay()
    {
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
    #endregion
}