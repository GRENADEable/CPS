using UnityEngine;

public class CamFollow : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField]
    [Tooltip("How much does the Camera Offset?")]
    private Vector3 camOffset = default;

    [SerializeField]
    [Tooltip("Transform Position to follow")]
    private Transform followPos = default;
    #endregion

    #region Unity Callbacks
    void Update()
    {
        transform.position = followPos.transform.position + camOffset;
    }
    #endregion
}