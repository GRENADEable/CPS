using UnityEngine;

public class CameraLookAround : MonoBehaviour
{
    #region Serialized Variables
    [Space, Header("Mouse Settings")]
    [SerializeField]
    [Tooltip("Minimum clamp on X Axis")]
    private float minXClamp = -90f;

    [SerializeField]
    [Tooltip("Maximum clamp on X Axis")]
    private float maxXClamp = 90f;

    [SerializeField]
    [Tooltip("Mouse sensitivity")]
    private float mouseSens = 300f;

    [SerializeField]
    [Tooltip("Transform Component of the root object")]
    private Transform playerRoot = default;

    [Space, Header("Footsteps")]
    [SerializeField]
    [Tooltip("Footsteps SFX Audio Source")]
    private AudioSource footstepAud;

    [SerializeField]
    [Tooltip("Footsteps SFX Audio Clips")]
    private AudioClip footStepsSfx;

    [SerializeField]
    [Tooltip("Footsteps SFX Volume")]
    [Range(0f, 1f)] private float footstepVolume;
    #endregion

    private float _xRotate = default;

    #region Unity Callbacks
    void Update() => CamLookAround();
    #endregion

    #region Functions
    void CamLookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        _xRotate -= mouseY;
        _xRotate = Mathf.Clamp(_xRotate, minXClamp, maxXClamp);

        transform.localRotation = Quaternion.Euler(_xRotate, 0f, 0f);

        playerRoot.Rotate(Vector3.up * mouseX);
    }

    public void FootStep1()
    {
        footstepAud.pitch = Random.Range(0.8f, 1f);
        footstepAud.PlayOneShot(footStepsSfx, footstepVolume);
    }

    public void FootStep2()
    {
        footstepAud.pitch = Random.Range(0.8f, 1f);
        footstepAud.PlayOneShot(footStepsSfx, footstepVolume);
    }
    #endregion
}