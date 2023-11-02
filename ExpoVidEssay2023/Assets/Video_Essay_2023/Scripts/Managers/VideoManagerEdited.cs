using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoManagerEdited : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    #region Serialized Variables
    [SerializeField]
    [Tooltip("Video Player for playing the Video Essays")]
    private VideoPlayer vidPlayer;

    [SerializeField]
    [Tooltip("Image Component for the progress bar")]
    private Image vidProgressImg;

    [SerializeField]
    [Tooltip("Render Texture where the Video will be played")]
    private RenderTexture vidRendTex = default;

    [SerializeField]
    [Tooltip("Video Button Image")]
    private Image buttonImg = default;

    [SerializeField]
    [Tooltip("Video Button Image Sprites")]
    private Sprite[] buttonImgSprites = default;
    #endregion

    #region Private Variables
    private bool _isVideoPaused = default;
    #endregion


    void Start()
    {
        vidRendTex.Release();
    }

    void Update()
    {
        if (vidPlayer.frameCount > 0)
            vidProgressImg.fillAmount = (float)vidPlayer.frame / (float)vidPlayer.frameCount;
    }
    public void OnDrag(PointerEventData eventData) => TrySkip(eventData);

    public void OnPointerDown(PointerEventData eventData) => TrySkip(eventData);

    void SkipToPercent(float pct)
    {
        var frame = vidPlayer.frameCount * pct;
        vidPlayer.frame = (long)frame;
    }

    void TrySkip(PointerEventData eventData)
    {
        Vector2 localPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            vidProgressImg.rectTransform, eventData.position, null, out localPoint))
        {
            float pct = Mathf.InverseLerp(vidProgressImg.rectTransform.rect.xMin, vidProgressImg.rectTransform.rect.xMax, localPoint.x);
            SkipToPercent(pct);
        }
    }

    public void VideoPlayerPlayPause()
    {
        if (vidPlayer != null)
        {
            _isVideoPaused = !_isVideoPaused;

            if (_isVideoPaused)
            {
                vidPlayer.Pause();
                buttonImg.sprite = buttonImgSprites[1];
            }
            else
            {
                vidPlayer.Play();
                buttonImg.sprite = buttonImgSprites[0];
            }
        }
    }

    public void VideoPlayerPause()
    {
        if (vidPlayer != null)
            vidPlayer.Pause();
    }

    public void VideoPlayerPlay()
    {
        if (vidPlayer != null)
            vidPlayer.Play();
    }

}