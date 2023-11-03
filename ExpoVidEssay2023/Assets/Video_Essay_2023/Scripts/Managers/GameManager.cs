using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    #region Serialized Variables
    [Space, Header("Datas")]
    [SerializeField]
    [Tooltip("")]
    private VidEssayData[] vidEssayData = default;

    [Space, Header("Essay Buttons")]
    [SerializeField]
    [Tooltip("Prefab Button of the Video Essay")]
    private GameObject vidEssayButtonPrefab = default;

    [SerializeField]
    [Tooltip("Position on where to Spawn the Prefab")]
    private Transform vidEssyButtonSpawnPos = default;

    [SerializeField]
    [Tooltip("Video Player for playing the Video Essays")]
    private VideoPlayer vidPlayer;

    [SerializeField]
    [Tooltip("Fade Background Image")]
    private Image fadeBG = default;
    #endregion

    #region Private Variables

    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {
        VidEssayButton.OnVidButtonClick += OnVidButtonClickEventReceived;
    }

    void OnDisable()
    {
        VidEssayButton.OnVidButtonClick -= OnVidButtonClickEventReceived;
    }

    void OnDestroy()
    {
        VidEssayButton.OnVidButtonClick -= OnVidButtonClickEventReceived;
    }
    #endregion

    void Start()
    {
        fadeBG.DOFade(0, 0.5f);
        IntialiseButtons();
    }

    void Update()
    {

    }
    #endregion

    #region My Functions
    /// <summary>
    /// Intialises the Buttons when the Scene Starts;
    /// </summary>
    void IntialiseButtons()
    {
        int essayIndex = 0;

        for (int i = 0; i < vidEssayData.Length; i++)
        {
            GameObject essayObj = Instantiate(vidEssayButtonPrefab, vidEssyButtonSpawnPos.position, Quaternion.identity, vidEssyButtonSpawnPos);
            essayObj.name = $"Essay_Button_{vidEssayData[i].vidEssayName}";

            if (essayObj.GetComponent<VidEssayButton>() != null)
                essayObj.GetComponent<VidEssayButton>().OnIntialiseButton(vidEssayData[i].vidEssayName, vidEssayData[i].vidEssayThumbnail, essayIndex);

            essayIndex++;
        }
    }
    #endregion

    #region Coroutines

    #endregion

    #region Events
    void OnVidButtonClickEventReceived(int vidEssayIndex)
    {
        vidPlayer.clip = vidEssayData[vidEssayIndex].vidEssayClip;
    }
    #endregion
}