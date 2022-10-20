using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Response_Data", menuName = "Response/ResponseData")]
public class ResponseData : ScriptableObject
{
    #region Public Variables
    public int responseWeek;
    public VideoClip responseVid;
    #endregion
}