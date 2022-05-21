using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedMountMedia;
using Cinemachine;

public class GameManagerArchivalObject : MonoBehaviour
{
    #region Serialized Variables

    #region Datas
    [Space, Header("Datas")]
    [SerializeField]
    [Tooltip("GameManager Scriptable Object")]
    private GameMangerData gmData = default;
    #endregion

    #region Cinemachine Cams
    [Space, Header("Cinemachine Cameras")]

    [SerializeField]
    [Tooltip("LayerMask to switch to")]
    private LayerMask switchLayer = default;

    [SerializeField]
    [Tooltip("Default LayerMask")]
    private LayerMask defaultLayer = default;
    #endregion

    #region UI
    [Space, Header("UI")]
    [SerializeField]
    [Tooltip("Fade Panel")]
    private Animator fadeBG = default;
    #endregion

    #endregion

    #region Private Variables
    private Camera _cam = default;
    //[SerializeField] private CinemachineVirtualCamera _vCamMain = default;
    private GameObject _player = default;
    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    void OnDestroy()
    {

    }
    #endregion

    void Start()
    {
        _cam = Camera.main;
        StartCoroutine(StartDelay());
        //Intialise();
    }

    void Update()
    {

    }
    #endregion

    #region My Functions
    /// <summary>
    /// Switches the layer of the Camera's CullingMask;
    /// Also toggles the main Virutal Camera GameObject;
    /// </summary>
    /// <param name="isSwitchLayer"> If true, change to custom LayerMask, if false, change to default; </param>
    public void OnSwitchCameraLayer(bool isSwitchLayer)
    {
        if (isSwitchLayer)
            _cam.cullingMask = switchLayer;
        else
            _cam.cullingMask = defaultLayer;
    }

    void Intialise()
    {

    }
    #endregion

    #region Coroutines
    /// <summary>
    /// Starts Game with delay
    /// </summary>
    /// <returns> Float delay; </returns>
    IEnumerator StartDelay()
    {
        fadeBG.Play("FadeIn");
        gmData.ChangeGameState("Intro");
        yield return new WaitForSeconds(0.5f);
        gmData.ChangeGameState("Game");
    }
    #endregion

    #region Events

    #endregion
}