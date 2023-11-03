using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class VidEssayButton : MonoBehaviour
{
    #region Serialized Variables
    //[SerializeField]
    //[Tooltip("")]

    public delegate void SendEventsInt(int id);
    public static event SendEventsInt OnVidButtonClick;
    #endregion

    #region Private Variables
    private TextMeshProUGUI _buttonEssayText = default;
    private Image _buttonEssayImg = default;
    [SerializeField] private int _essayIndex = default;
    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    void OnDestroy()
    {

    }
    #endregion

    void Awake()
    {
        _buttonEssayText = GetComponentInChildren<TextMeshProUGUI>();
        _buttonEssayImg = GetComponent<Image>();
    }
    #endregion

    #region My Functions
    public void OnIntialiseButton(string essayNameText, Sprite essaySprite, int essayIndex)
    {
        _buttonEssayText.text = essayNameText;
        _buttonEssayImg.sprite = essaySprite;
        _essayIndex = essayIndex;

    }
    public void OnClick_VidEssayButton()
    {
        OnVidButtonClick?.Invoke(_essayIndex);
    }
    public void OnHover_MouseEnter() => _buttonEssayText.DOFade(0, 0.2f);

    public void OnHover_MouseExit() => _buttonEssayText.DOFade(1, 0.2f);
    #endregion

    #region Coroutines

    #endregion

    #region Events

    #endregion
}