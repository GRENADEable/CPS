using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Vid_Essay_Data", menuName = "Video Essay/Video Essay Data")]
public class VidEssayData : ScriptableObject
{
    public VideoClip vidEssayClip = default;
    public Sprite vidEssayThumbnail = default;
    public string vidEssayName = default;
}