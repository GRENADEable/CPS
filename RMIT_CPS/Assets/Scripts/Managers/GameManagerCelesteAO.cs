using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameManagerCelesteAO : MonoBehaviour
{
    #region Serialized Variables

    #region Datas
    [Space, Header("Datas")]
    [SerializeField]
    [Tooltip("Response Datas")]
    private ResponseData[] responseDatas = default;
    #endregion

    #region GameObjects
    [Space, Header("GameObjects")]
    [SerializeField]
    [Tooltip("Response Panel GameObject")]
    private GameObject responsePanel = default;
    #endregion

    #region Floats
    [Space, Header("Floats")]
    [SerializeField]
    [Tooltip("Fade Background Delay")]
    private float fadeDelay = default;
    #endregion

    #region Buttons
    [Space, Header("Buttons")]
    [SerializeField]
    [Tooltip("Response Back Button")]
    private Button responseBackButton = default;
    #endregion

    #region Images 
    [Space, Header("Images")]
    [SerializeField]
    [Tooltip("Fade Background Image")]
    private Image fadeBG = default;
    #endregion

    #region Texts
    [Space, Header("Texts")]
    [SerializeField]
    [Tooltip("Response Text")]
    private TextMeshProUGUI responseTitleText = default;
    #endregion

    #endregion

    #region Private Variables

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

    void Start()
    {
        fadeBG.DOFade(0, fadeDelay);
    }

    void Update()
    {

    }
    #endregion

    #region My Functions
    public void OnClick_ShowResponse(int responseIndex)
    {
        responsePanel.SetActive(true);
        responseTitleText.text = $"Response Week {responseDatas[responseIndex].responseWeek}";
        responseBackButton.interactable = true;
    }

    public void OnClickResponseBack()
    {
        responseTitleText.text = "";
        responsePanel.SetActive(false);
        responseBackButton.interactable = false;
    }
    #endregion

    #region Coroutines

    #endregion

    #region Events

    #endregion
}