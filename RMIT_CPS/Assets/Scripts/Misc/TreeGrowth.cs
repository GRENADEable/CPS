using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    #region Serialized Variables
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

    #region My Functions
    public void OnTreeGrown() => OnTreeFullyGrown?.Invoke();

    public void OnTreeGrownFinal() => OnTreeFullyGrownFinal?.Invoke();
    #endregion

}