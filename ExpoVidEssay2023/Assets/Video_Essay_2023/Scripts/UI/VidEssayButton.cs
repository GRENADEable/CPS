using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class VidEssayButton : MonoBehaviour
{
    #region Serialized Variables

    #region Events Int
    public delegate void SendEventsInt(int id);
    /// <summary>
    /// Sends Event from VidEssayButton To GameManager Script;
    /// Changes Panel to Video Essay Panel and player the Video;
    /// </summary>
    public static event SendEventsInt OnVidButtonClick;
    #endregion

    #region Event String
    public delegate void SendEventString(string text);
    public static event SendEventString OnVidButtonClickTitleText;
    public static event SendEventString OnVidButtonClickDescriptionText;
    #endregion

    #endregion

    #region Private Variables
    private TextMeshProUGUI _buttonEssayNameText = default;
    [TextArea(2, 5)] private string _vidEssayTitleText = default;
    [TextArea(30, 5)] private string _vidEssayDescriptionText = default;
    private Image _buttonEssayImg = default;
    private int _essayIndex = default;
    private VidEssayData _vidEssayData = default;
    #endregion

    #region Unity Callbacks
    void Awake()
    {

    }

    void Start()
    {
        _buttonEssayNameText = GetComponentInChildren<TextMeshProUGUI>();
        _buttonEssayImg = GetComponent<Image>();

        IntialiseButton();
    }
    #endregion

    #region My Functions
    ///// <summary>
    ///// Button intialisd from GameManger;
    ///// Sets up the UI of the Button when the Buttons spawn from the ScriptableObject Datas;
    ///// </summary>
    /// <param name="essayData"> Video Essay Scrtipable Object set from the GameManager Script on Runtime; </param>
    /// <param name="essayIndex"> Video Essay Index set from the GameManager Script on Runtime; </param>
    public void OnIntialiseButton(VidEssayData essayData, int essayIndex)
    {
        _vidEssayData = essayData;
        _essayIndex = essayIndex;
    }

    /// <summary>
    /// Sets the text of the buttons on Runtime;
    /// </summary>
    void IntialiseButton()
    {
        _buttonEssayNameText.text = _vidEssayData.vidEssayName;
        _buttonEssayImg.sprite = _vidEssayData.vidEssayThumbnail;

        _vidEssayTitleText = _vidEssayData.vidEssayTitle;
        _vidEssayDescriptionText = _vidEssayData.vidEssayDescription;

        _buttonEssayImg.transform.DOPunchScale(Vector3.one, 1f);
    }

    /// <summary>
    /// Tied to Essay_Button_V1;
    /// Updates the UI of the Game according to the current button pressed;
    /// </summary>
    public void OnClick_VidEssayButtonV1() => OnVidButtonClick?.Invoke(_essayIndex);

    /// <summary>
    /// Tied to Essay_Button_V2;
    /// Updates the UI of the Game according to the current button pressed;
    /// </summary>
    public void OnClick_VidEssayButtonV2()
    {
        //OnHover_MouseExit();
        OnVidButtonClick?.Invoke(_essayIndex);
        OnVidButtonClickTitleText?.Invoke(_vidEssayTitleText);
        OnVidButtonClickDescriptionText?.Invoke(_vidEssayDescriptionText);
    }

    ///// <summary>
    ///// Tied to Event Trigger Pointer Enter on Essay_Button_V2;
    ///// Hides the name Text and reaveals the info Text;
    ///// </summary>
    //public void OnHover_MouseEnter()
    //{
    //    _buttonEssayNameText.DOFade(0, 0.2f);
    //}

    ///// <summary>
    ///// Tied to Event Trigger Pointer Enter on Essay_Button_V2;
    ///// Shows the name Text and hides the info Text;
    ///// </summary>
    //public void OnHover_MouseExit()
    //{
    //    _buttonEssayNameText.DOFade(1, 0.2f);
    //}
    #endregion
}