using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedMountMedia;

public class GameManagerArchivalObject : MonoBehaviour
{
    #region Serialized Variables

    #region Datas
    [Space, Header("Datas")]
    [SerializeField]
    [Tooltip("GameManager Scriptable Object")]
    private GameMangerData gmData = default;

    [SerializeField]
    [Tooltip("Do you want to disable the curosr?")]
    private bool isCursorDisabled = default;
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

    [SerializeField]
    [Tooltip("Player HUD Panel")]
    private GameObject hudPanel = default;
    #endregion

    #region GameObjects
    [Space, Header("GameObjects")]
    [SerializeField]
    [Tooltip("Crane Object")]
    private GameObject craneObj = default;
    #endregion

    #region Floats
    [Space, Header("Floats")]
    [SerializeField]
    [Tooltip("Crane Rotation Speed")]
    private float craneRotationSpeed = default;
    #endregion

    #region Events Float
    public delegate void SendEventsFloat(float floatIndex);
    /// <summary>
    /// Event sent from GameManagerArchivalObject to FPSController Scripts;
    /// Changes the Player's speed;
    /// </summary>
    public static event SendEventsFloat OnSpeedToggle;
    #endregion

    #endregion

    #region Private Variables
    private Camera _cam = default;
    private bool _isCraneRotating = default;
    private
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

        if (isCursorDisabled)
            gmData.DisableCursor();
    }

    void Update()
    {
        RotatingCrane();
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
        {
            hudPanel.SetActive(false);
            _cam.cullingMask = switchLayer;
        }
        else
        {
            hudPanel.SetActive(true);
            _cam.cullingMask = defaultLayer;
        }
    }

    /// <summary>
    /// Makes the crane rotate with the bool from the Unity Event;
    /// </summary>
    /// <param name="isRotatingCrane"> If true, rotates the crane, else it stops rotation; </param>
    public void OnCraneRotate(bool isRotatingCrane)
    {
        if (isRotatingCrane)
            _isCraneRotating = true;
        else
            _isCraneRotating = false;
    }

    public void OnIsPlayerSpeedy(float speed) => OnSpeedToggle?.Invoke(speed);

    /// <summary>
    /// Crane rotation;
    /// </summary>
    void RotatingCrane()
    {
        if (_isCraneRotating)
            craneObj.transform.Rotate(Vector3.up * craneRotationSpeed * Time.deltaTime);
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