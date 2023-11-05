using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "VidEssay_Data", menuName = "Video Essay/Video Essay Data")]
public class VidEssayData : ScriptableObject
{
    public VideoClip vidEssayClip = default;
    public Sprite vidEssayThumbnail = default;
    public string vidEssayName = default;
    [TextArea(2, 5)] public string vidEssayTitle = default;
    [TextArea(30, 5)] public string vidEssayDescription = default;
}