using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Serialized Variables

    #region Datas
    [Space, Header("Datas")]
    //[SerializeField]
    //[Tooltip("Game Manager Scrtipable Objects")]
    //private GameManagerData gmData = default;

    [SerializeField]
    [Tooltip("Scrtipable Objects for the Video Essays")]
    private VidEssayData[] vidEssayData = default;
    #endregion

    #region UIs
    [Space, Header("UIs")]
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
    [Tooltip("Render Texture where the Video will be played")]
    private RenderTexture vidRendTex = default;

    //[SerializeField]
    //[Tooltip("Fade Background Image")]
    //private Image fadeBG = default;

    [SerializeField]
    [Tooltip("Video Essay Title Text")]
    private TextMeshProUGUI vidEssayLibraryText = default;

    [SerializeField]
    [Tooltip("Video Essay Title Text")]
    private TextMeshProUGUI vidEssayTitleText = default;

    [SerializeField]
    [Tooltip("Video Essay Description Text")]
    private TextMeshProUGUI vidEssayDescriptionText = default;

    [SerializeField]
    [Tooltip("")]
    private Scrollbar essayLibraryScrollbar = default;
    #endregion

    #region GameObjects
    [Space, Header("GameObjects")]
    [SerializeField]
    [Tooltip("Essay Library Video Panel GameObject")]
    private GameObject vidEssayLibraryPanel = default;

    [SerializeField]
    [Tooltip("Essay Video Canvas GameObject")]
    private GameObject vidEssayVideoCanvas = default;
    #endregion

    #region Events
    public delegate void SendEvents();
    public static event SendEvents OnEscapeButtonPressed;
    #endregion

    #endregion

    #region Private Variables
    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {
        VidEssayButton.OnVidButtonClick += OnVidButtonClickEventReceived;
        VidEssayButton.OnVidButtonClickTitleText += OnVidButtonClickTitleTextEventReceived;
        VidEssayButton.OnVidButtonClickDescriptionText += OnVidButtonClickDescriptionTextEventReceived;

        VideoManagerEdited.OnVidClose += OnVidCloseEventReceived;
    }

    void OnDisable()
    {
        VidEssayButton.OnVidButtonClick -= OnVidButtonClickEventReceived;
        VidEssayButton.OnVidButtonClickTitleText -= OnVidButtonClickTitleTextEventReceived;
        VidEssayButton.OnVidButtonClickDescriptionText -= OnVidButtonClickDescriptionTextEventReceived;

        VideoManagerEdited.OnVidClose -= OnVidCloseEventReceived;
    }

    void OnDestroy()
    {
        VidEssayButton.OnVidButtonClick -= OnVidButtonClickEventReceived;
        VidEssayButton.OnVidButtonClickTitleText -= OnVidButtonClickTitleTextEventReceived;
        VidEssayButton.OnVidButtonClickDescriptionText -= OnVidButtonClickDescriptionTextEventReceived;

        VideoManagerEdited.OnVidClose -= OnVidCloseEventReceived;
    }
    #endregion

    void Start()
    {
        //fadeBG.DOFade(0, 0.5f);
        vidEssayLibraryText.DOFade(1, 2f);
        IntialiseButtons();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CloseVidPlayer();
            OnEscapeButtonPressed?.Invoke();
        }
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
                essayObj.GetComponent<VidEssayButton>().OnIntialiseButton(vidEssayData[i], essayIndex);

            essayIndex++;
        }
    }

    /// <summary>
    /// Closes Video Player;
    /// </summary>
    void CloseVidPlayer()
    {
        vidEssayLibraryPanel.SetActive(true);
        vidEssayVideoCanvas.SetActive(false);
        vidPlayer.Stop();
        vidRendTex.Release();
    }
    #endregion

    #region Coroutines

    #endregion

    #region Events
    /// <summary>
    /// Subbed to OnVidCloseEvent from VideoGameManagerEdited Script;
    /// </summary>
    public void OnVidCloseEventReceived()
    {
        CloseVidPlayer();
    }

    /// <summary>
    /// Subbed to event from VidEssayButton Script;
    /// Updates the current Video to play from the Index;
    /// Switches Panel and plays Video
    /// </summary>
    /// <param name="vidEssayIndex"> Index from the Essay Panel Button; </param>
    void OnVidButtonClickEventReceived(int vidEssayIndex)
    {
        vidPlayer.clip = vidEssayData[vidEssayIndex].vidEssayClip;

        vidEssayLibraryPanel.SetActive(false);
        vidEssayVideoCanvas.SetActive(true);

        //if (gmData.currEssayVersion == GameManagerData.EssaySceneVerion.Version1)
        vidPlayer.Play();
        vidRendTex.Release();

        if (essayLibraryScrollbar != null)
            essayLibraryScrollbar.value = 1;
    }

    void OnVidButtonClickTitleTextEventReceived(string vidEssayTitle)
    {
        vidEssayTitleText.text = vidEssayTitle;
    }

    void OnVidButtonClickDescriptionTextEventReceived(string vidEssayDescription)
    {
        vidEssayDescriptionText.text = vidEssayDescription;
    }
    #endregion
}