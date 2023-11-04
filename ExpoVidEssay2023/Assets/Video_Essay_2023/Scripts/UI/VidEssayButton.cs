using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class VidEssayButton : MonoBehaviour
{
    #region Serialized Variables
    public delegate void SendEventsInt(int id);
    /// <summary>
    /// Sends Event from VidEssayButton To GameManager Script;
    /// Changes Panel to Video Essay Panel and player the Video;
    /// </summary>
    public static event SendEventsInt OnVidButtonClick;
    #endregion

    #region Private Variables
    private TextMeshProUGUI _buttonEssayText = default;
    private Image _buttonEssayImg = default;
    [SerializeField] private int _essayIndex = default;
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        _buttonEssayText = GetComponentInChildren<TextMeshProUGUI>();
        _buttonEssayImg = GetComponent<Image>();
    }
    #endregion

    #region My Functions
    /// <summary>
    /// Button intialisd from GameManger;
    /// Sets up the UI of the Button when the Buttons spawn from the ScriptableObject Datas;
    /// </summary>
    /// <param name="essayNameText"> The Name of the person in the video essay; </param>
    /// <param name="essaySprite"> The thumbnail of the video essay; </param>
    /// <param name="essayIndex"> The Index number of the video essay; </param>
    public void OnIntialiseButton(string essayNameText, Sprite essaySprite, int essayIndex)
    {
        _buttonEssayText.text = essayNameText;
        _buttonEssayImg.sprite = essaySprite;
        _essayIndex = essayIndex;
    }

    /// <summary>
    /// Tied to Essay_Button;
    /// Updates the UI of the Game according to the current button pressed;
    /// </summary>
    public void OnClick_VidEssayButton()
    {
        OnVidButtonClick?.Invoke(_essayIndex);
    }

    //public void OnHover_MouseEnter() => _buttonEssayText.DOFade(0, 0.2f);

    //public void OnHover_MouseExit() => _buttonEssayText.DOFade(1, 0.2f);
    #endregion
}