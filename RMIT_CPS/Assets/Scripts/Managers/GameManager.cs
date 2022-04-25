using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField]
    [Tooltip("Fade Image Animation Component")]
    private Animator fadeBG = default;
    #endregion

    #region Unity Callbacks
    void Start() => fadeBG.Play("Fade_In");
    #endregion

    #region My Functions
    public void OnGameEnd() => StartCoroutine(EndDelay());
    #endregion

    #region Coroutines
    IEnumerator EndDelay()
    {
        fadeBG.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel(0);
    }
    #endregion
}