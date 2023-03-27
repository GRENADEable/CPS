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
    #endregion

    #region My Functions
    public void ShotgunSFX(int index) => shotgunSFXAud.PlayOneShot(shotgunSFX[index]);
    #endregion
}