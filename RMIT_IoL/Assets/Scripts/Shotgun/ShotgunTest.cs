using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunTest : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField]
    [Tooltip("Distance of the ray from the Cam")]
    private float rayDistance = 1f;

    [SerializeField]
    [Tooltip("Distance of the ray from the Cam")]
    private float rayDistanceSiren = 1f;

    [SerializeField]
    [Tooltip("Shotgun Layer")]
    private LayerMask shotgunLayer = default;

    [SerializeField]
    [Tooltip("Holstered Shotgun GameObject")]
    private GameObject shotgunObj = default;

    [SerializeField]
    [Tooltip("Shotgun SFX Audio Source")]
    private AudioSource shotgunSFXAud = default;

    [SerializeField]
    [Tooltip("Shotgun SFXs")]
    private AudioClip[] shotgunSFX = default;

    [SerializeField]
    [Tooltip("Shotgun Anim Controller")]
    private Animator shotgunAnim = default;
    #endregion

    #region Private Variables
    private bool _isHittingShotgun = default;
    private Ray _ray;
    private RaycastHit _hit;
    private Camera _mainCam = default;
    private bool _isShotgunActive = default;
    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {
        ShotgunAnim.OnShotgunReady += OnShotgunReadyEventReceived;
    }

    void OnDisable()
    {
        ShotgunAnim.OnShotgunReady -= OnShotgunReadyEventReceived;
    }

    void OnDestroy()
    {
        ShotgunAnim.OnShotgunReady -= OnShotgunReadyEventReceived;
    }
    #endregion

    void Start() => _mainCam = Camera.main;

    void Update()
    {
        _ray = new Ray(_mainCam.transform.position, _mainCam.transform.forward);

        CheckShotgun();

        if (Input.GetMouseButtonDown(0) && _isShotgunActive)
        {
            shotgunSFXAud.PlayOneShot(shotgunSFX[0]);
            shotgunAnim.Play("Shotgun_Recoil");
            _isShotgunActive = false;
            Shoot();
        }
    }
    #endregion

    #region My Functions
    /// <summary>
    /// Raycast check to see where the shotgun is placed;
    /// </summary>
    void CheckShotgun()
    {
        _isHittingShotgun = Physics.Raycast(_ray.origin, _mainCam.transform.forward, out _hit, rayDistance, shotgunLayer);
        Debug.DrawRay(_ray.origin, _mainCam.transform.forward * rayDistance, _isHittingShotgun ? Color.green : Color.red);

        if (_isHittingShotgun)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(_hit.transform.gameObject);
                shotgunObj.SetActive(true);
            }
        }
    }

    void Shoot()
    {
        bool isHittingSiren = Physics.Raycast(_ray.origin, _mainCam.transform.forward, out _hit, rayDistanceSiren);
        Debug.DrawRay(_ray.origin, _mainCam.transform.forward * rayDistanceSiren, Color.yellow);

        if (isHittingSiren && _hit.collider.CompareTag("Siren"))
        {
            Destroy(_hit.collider.transform.parent.gameObject);
        }
    }
    #endregion

    #region Events
    void OnShotgunReadyEventReceived()
    {
        _isShotgunActive = true;
    }
    #endregion
}