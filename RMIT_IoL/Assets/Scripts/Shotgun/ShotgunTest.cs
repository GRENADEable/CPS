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
    [Tooltip("Shotgun Layer")]
    private LayerMask shotgunLayer = default;

    [SerializeField]
    [Tooltip("Holstered Shotgun GameObject")]
    private GameObject shotgunObj = default;
    #endregion

    #region Private Variables
    private bool _isHittingShotgun = default;
    private Ray _ray;
    private RaycastHit _hit;
    private Camera _mainCam = default;
    private bool _isShotgunActive = default;
    #endregion

    #region Unity Callbacks

    void Start() => _mainCam = Camera.main;

    void Update()
    {
        _ray = new Ray(_mainCam.transform.position, _mainCam.transform.forward);

        CheckShotgun();
    }
    #endregion

    #region My Functions
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
    #endregion
}