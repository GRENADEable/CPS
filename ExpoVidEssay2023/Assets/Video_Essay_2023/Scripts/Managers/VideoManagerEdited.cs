using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class VideoManagerEdited : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    #region Serialized Variables

    #region Datas
    //[Space, Header("Datas")]
    //[SerializeField]
    //[Tooltip("Game Manager Scrtipable Objects")]
    //private GameManagerData gmData = default;
    #endregion

    #region UIs
    [Space, Header("UIs")]
    [SerializeField]
    [Tooltip("Video Player for playing the Video Essays")]
    private VideoPlayer vidPlayer;

    [SerializeField]
    [Tooltip("Image Component for the progress bar")]
    private Image vidProgressImg;

    [SerializeField]
    [Tooltip("Video Button Image")]
    private Image buttonImg = default;

    [SerializeField]
    [Tooltip("Video Button Image Sprites")]
    private Sprite[] buttonImgSprites = default;
    #endregion

    #region Events
    public delegate void SendEvents();
    /// <summary>
    /// Event sent from VideoManagerEdited to GameManager Scripts;
    /// Closes the Vdieo Player on Click;
    /// </summary>
    public static event SendEvents OnVidClose;
    #endregion

    #endregion

    #region Private Variables
    private bool _isVideoPaused = default;
    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {
        vidPlayer.loopPointReached += OnVidPlayerEnded;

        GameManager.OnEscapeButtonPressed += OnEscapeButtonPressedEventReceived;
    }

    void OnDisable()
    {
        vidPlayer.loopPointReached -= OnVidPlayerEnded;

        GameManager.OnEscapeButtonPressed -= OnEscapeButtonPressedEventReceived;
    }

    void OnDestroy()
    {
        vidPlayer.loopPointReached -= OnVidPlayerEnded;

        GameManager.OnEscapeButtonPressed -= OnEscapeButtonPressedEventReceived;
    }
    #endregion

    //void Start()
    //{
    //    if (gmData.currEssayVersion == GameManagerData.EssaySceneVerion.Version2)
    //        _isVideoPaused = true;
    //}

    void Update()
    {
        if (vidPlayer.frameCount > 0)
            vidProgressImg.fillAmount = (float)vidPlayer.frame / (float)vidPlayer.frameCount;

        if (Input.GetKeyDown(KeyCode.Space) && vidPlayer.gameObject.activeInHierarchy)
        {
            if (vidPlayer != null)
            {
                _isVideoPaused = !_isVideoPaused;

                if (_isVideoPaused)
                {
                    vidPlayer.Pause();
                    buttonImg.sprite = buttonImgSprites[0];
                }
                else
                {
                    vidPlayer.Play();
                    buttonImg.sprite = buttonImgSprites[1];
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData) => TrySkip(eventData);

    public void OnPointerDown(PointerEventData eventData) => TrySkip(eventData);
    #endregion

    #region My Functions
    void SkipToPercent(float pct)
    {
        var frame = vidPlayer.frameCount * pct;
        vidPlayer.frame = (long)frame;
    }

    void TrySkip(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            vidProgressImg.rectTransform, eventData.position, null, out Vector2 localPoint))
        {
            float pct = Mathf.InverseLerp(vidProgressImg.rectTransform.rect.xMin, vidProgressImg.rectTransform.rect.xMax, localPoint.x);
            SkipToPercent(pct);
        }
    }

    /// <summary>
    /// Tied to Play_Pause_Button;
    /// Updates the Button UI depending on the current Bool state;
    /// </summary>
    public void VideoPlayerPlayPause()
    {
        if (vidPlayer != null)
        {
            _isVideoPaused = !_isVideoPaused;

            if (_isVideoPaused)
            {
                vidPlayer.Pause();
                buttonImg.sprite = buttonImgSprites[0];
            }
            else
            {
                vidPlayer.Play();
                buttonImg.sprite = buttonImgSprites[1];
            }
        }
    }

    /// <summary>
    /// Tied to Back_Button;
    /// Closes the video player;
    /// </summary>
    public void OnClick_VideoPlayerClose()
    {
        OnVidClose?.Invoke();
        buttonImg.sprite = buttonImgSprites[1];
        _isVideoPaused = false;
        //if (gmData.currEssayVersion == GameManagerData.EssaySceneVerion.Version1)
        //{
        //    buttonImg.sprite = buttonImgSprites[1];
        //    _isVideoPaused = false;
        //}

        //if (gmData.currEssayVersion == GameManagerData.EssaySceneVerion.Version2)
        //{
        //    buttonImg.sprite = buttonImgSprites[0];
        //    _isVideoPaused = true;
        //}
    }

    /// <summary>
    /// Tied to Pause_Button;
    /// Pauses the Video;
    /// </summary>
    public void VideoPlayerPause()
    {
        if (vidPlayer != null)
            vidPlayer.Pause();
    }

    /// <summary>
    /// Tied to Play_Button;
    /// Plays the Video;
    /// </summary>
    public void VideoPlayerPlay()
    {
        if (vidPlayer != null)
            vidPlayer.Play();
    }
    #endregion

    #region Events
    /// <summary>
    /// Subbed to Event from Essay_Raw_Image_&_Vid_Player;
    /// Checks if the Video has ended. Update the UI;
    /// </summary>
    /// <param name="player"> VideoPlayer Component from Essay_Raw_Image_&_Vid_Player</param>
    void OnVidPlayerEnded(VideoPlayer player)
    {
        buttonImg.sprite = buttonImgSprites[0];
        _isVideoPaused = true;
    }

    void OnEscapeButtonPressedEventReceived()
    {
        buttonImg.sprite = buttonImgSprites[1];
        _isVideoPaused = false;
    }
    #endregion
}