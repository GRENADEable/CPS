using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAnim : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField]
    [Tooltip("Shotgun SFX Audio Source")]
    private AudioSource shotgunSFXAud = default;

    [SerializeField]
    [Tooltip("Shotgun SFXs")]
    private AudioClip[] shotgunSFX = default;

    #region Events
    public delegate void SendEvents();
    public static event SendEvents OnShotgunReady;
    #endregion

    #endregion

    #region My Functions
    public void OnShotgunSFX(int index) => shotgunSFXAud.PlayOneShot(shotgunSFX[index]);

    public void OnShotgunReloaded() => OnShotgunReady?.Invoke();
    #endregion
}