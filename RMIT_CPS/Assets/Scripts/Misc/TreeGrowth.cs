using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    #region Serialized Variables

    [Space, Header("Audio")]
    [SerializeField]
    [Tooltip("One Watering Can SFX")]
    private AudioClip wateringCanSFXClip = default;

    #region Events
    public delegate void SendEvents();
    /// <summary>
    /// Event sent from TreeGrowth to GameManagerWeek9 Script;
    /// Changes the lighting and clears up the UI;
    /// </summary>
    public static event SendEvents OnTreeFullyGrown;

    /// <summary>
    /// Event sent from TreeGrowth to GameManagerWeek9 Script;
    /// Changes the lighting and clears up the UI;
    /// Also ends the game;
    /// </summary>
    public static event SendEvents OnTreeFullyGrownFinal;
    #endregion

    #endregion

    #region Private Variables
    [SerializeField] private AudioSource _oneShotSFXAud = default;
    #endregion

    #region Unity Callbacks
    void Start() => _oneShotSFXAud = GetComponent<AudioSource>();
    #endregion

    #region My Functions
    public void OnTreeGrown() => OnTreeFullyGrown?.Invoke();

    public void OnTreeGrownFinal() => OnTreeFullyGrownFinal?.Invoke();

    public void OnTreeWaterFlow() => _oneShotSFXAud.PlayOneShot(wateringCanSFXClip);
    #endregion

}