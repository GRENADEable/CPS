using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Response_Data", menuName = "Response/ResponseData")]
public class ResponseData : ScriptableObject
{
    #region Public Variables
    public string responseWeek;
    public VideoClip responseVid;
    #endregion
}