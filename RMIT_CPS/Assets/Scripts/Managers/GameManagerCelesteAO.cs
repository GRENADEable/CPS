using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Video;

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

    [SerializeField]
    [Tooltip("Response Panel Delay")]
    private float responsePanelDelay = default;
    #endregion

    #region Buttons
    [Space, Header("Buttons")]
    [SerializeField]
    [Tooltip("All the Buttons in the Scene")]
    private Button[] sceneButtons = default;

    [SerializeField]
    [Tooltip("Respone Buttons")]
    private Button[] responseButtons = default;

    [SerializeField]
    [Tooltip("Response Back Button")]
    private Button responseBackButton = default;
    #endregion

    #region Images 
    [Space, Header("Images")]
    [SerializeField]
    [Tooltip("Fade Background Image")]
    private Image fadeBG = default;

    [SerializeField]
    [Tooltip("Response Video RawImage")]
    private RenderTexture responseVidTex = default;

    [SerializeField]
    [Tooltip("Response Video Player")]
    private VideoPlayer responseVidPlayer = default;
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
    void Start() => fadeBG.DOFade(0, fadeDelay);
    #endregion

    #region My Functions
    /// <summary>
    /// Tied to response Buttons;
    /// Shows that response video respective to the ResponseData Array Index;
    /// </summary>
    /// <param name="responseIndex"> Response Data Index from the Button; </param>
    public void OnClick_ShowResponse(int responseIndex)
    {
        responsePanel.transform.DOScale(Vector3.one, responsePanelDelay);
        responseTitleText.text = $"Response Week {responseDatas[responseIndex].responseWeek}";
        responseVidPlayer.clip = responseDatas[responseIndex].responseVid;
        responseVidPlayer.Play();
        InteractableResponeButtons(false);
        responseBackButton.interactable = true;
    }

    /// <summary>
    /// Tied to response back Button;
    /// Removes title text, stops video and closes responses window;
    /// </summary>
    public void OnClick_ResponseBack()
    {
        responsePanel.transform.DOScale(Vector3.zero, responsePanelDelay);
        responseTitleText.text = "";
        responseVidPlayer.Stop();
        InteractableResponeButtons(true);
        responseBackButton.interactable = false;
        responseVidTex.Release();
    }

    /// <summary>
    /// Tied to quit Button;
    /// Closes game with delay;
    /// </summary>
    public void OnClick_QuitGame() => StartCoroutine(QuitDelay());

    /// <summary>
    /// Reponse Buttons interaction;
    /// </summary>
    /// <param name="isInteractable"> If true, can push buttons and vice versa; </param>
    void InteractableResponeButtons(bool isInteractable)
    {
        if (isInteractable)
        {
            for (int i = 0; i < responseButtons.Length; i++)
                responseButtons[i].interactable = true;
        }
        else
        {
            for (int i = 0; i < responseButtons.Length; i++)
                responseButtons[i].interactable = false;
        }
    }
    #endregion

    #region Coroutines
    /// <summary>
    /// Closes game with delay;
    /// </summary>
    /// <returns> Float delay; </returns>
    IEnumerator QuitDelay()
    {
        fadeBG.DOFade(1, fadeDelay);

        for (int i = 0; i < sceneButtons.Length; i++)
            sceneButtons[i].interactable = false;

        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
    #endregion
}