using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public VideoPlayer player;
    public Image progress;
    public RenderTexture rendTex = default;

    void Start() => rendTex.Release();

    void Update()
    {
        if (player.frameCount > 0)
            progress.fillAmount = (float)player.frame / (float)player.frameCount;
    }
    public void OnDrag(PointerEventData eventData) => TrySkip(eventData);

    public void OnPointerDown(PointerEventData eventData) => TrySkip(eventData);

    void SkipToPercent(float pct)
    {
        var frame = player.frameCount * pct;
        player.frame = (long)frame;
    }

    void TrySkip(PointerEventData eventData)
    {
        Vector2 localPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            progress.rectTransform, eventData.position, null, out localPoint))
        {
            float pct = Mathf.InverseLerp(progress.rectTransform.rect.xMin, progress.rectTransform.rect.xMax, localPoint.x);
            SkipToPercent(pct);
        }
    }

    public void VideoPlayerPause()
    {
        if (player != null)
            player.Pause();
    }

    public void VideoPlayerPlay()
    {
        if (player != null)
            player.Play();
    }

}