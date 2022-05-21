using System.Collections;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelTrigger : MonoBehaviour
{
    #region Serialized Variables

    #region Editor
    private static bool showBoundries = false;
    private enum TriggerType { General, Gameplay, Audio, Camera, None };

    [SerializeField]
    [Tooltip("Shows differnt types of icons for the trigger")]
    private TriggerType type = TriggerType.General;
    #endregion

    [SerializeField]
    [Tooltip("Call event with specific tag")]
    private string activatorTag = "Player";

    #region Trigger Counters
    [Space, Header("Trigger Counter Clamp")]
    [SerializeField]
    [Tooltip("How many times can it enter the trigger")]
    private int triggerCounterEnter = 0;

    [SerializeField]
    [Tooltip("How many times can it exit the trigger")]
    private int triggerCounterExit = 0;
    #endregion

    #region Trigger After X Tries
    [Space, Header("Trigger Counter")]
    [SerializeField]
    [Tooltip("When to trigger after x amount of enteries")]
    private int triggerAfterEnter = 0;

    [SerializeField]
    [Tooltip("When to trigger after x amount of exits")]
    private int triggerAfterExit = 0;
    #endregion

    #region Unity Events
    [SerializeField]
    [Space, Header("Trigger Delay")]
    [Tooltip("Call event after x amount of seconds")]
    private float triggerDelay = 0f;

    [Header("Event Trigger Enter / Exit")]
    public UnityEvent EventTriggerEnter;
    public UnityEvent EventTriggerExit;
    #endregion

    #endregion

    #region Private Variables
    private float _timeStayed = 0f;
    private bool _stayActive = false;
    private int _triggerCountedEnter = 0;
    private int _triggerCountedExit = 0;
    #endregion

    #region Unity Callbacks
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(activatorTag))
        {
            int triggerRange = _triggerCountedEnter - triggerAfterEnter;

            if (triggerCounterEnter == 0 || (triggerRange >= 0 && triggerRange < triggerCounterEnter))
            {
                _timeStayed = 0f;
                _stayActive = true;

                Debug.Log("Trigger Entered");

                if (triggerDelay > 0)
                    StartCoroutine(EnterDelayed());
                else
                    EventTriggerEnter.Invoke();
            }

            _triggerCountedEnter++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(activatorTag))
        {
            int triggerRange = _triggerCountedExit - triggerAfterExit;

            if (triggerCounterExit == 0 || (triggerRange >= 0 && triggerRange < triggerCounterExit))
            {
                _timeStayed = 0f;

                Debug.Log("Trigger Exit");

                if (triggerDelay > 0)
                    StartCoroutine(ExitDelayed());
                else
                    EventTriggerExit.Invoke();
            }

            _triggerCountedExit++;
        }
    }

    void OnDrawGizmos()
    {
        if (Vector3.Distance(transform.position, Camera.current.transform.position) > 1f)
        {
            switch (type)
            {
                case TriggerType.General:
                    Gizmos.DrawIcon(transform.position, "GeneralIcon.png");
                    break;

                case TriggerType.Gameplay:
                    Gizmos.DrawIcon(transform.position, "GameplayIcon.png");
                    break;

                case TriggerType.Audio:
                    Gizmos.DrawIcon(transform.position, "AudioIcon.png");
                    break;

                case TriggerType.Camera:
                    Gizmos.DrawIcon(transform.position, "CameraIcon.png");
                    break;

                case TriggerType.None:
                    break;

                default:
                    break;

            }
        }

        if (TryGetComponent(out BoxCollider bc) && showBoundries)
        {
            Gizmos.color = Color.yellow;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(bc.center, bc.size);
        }
        else if (TryGetComponent(out SphereCollider sc) && showBoundries)
        {
            Gizmos.color = Color.yellow;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireSphere(sc.center, sc.radius);
        }
    }
    #endregion

    #region Coroutines
    IEnumerator EnterDelayed()
    {
        yield return new WaitForSeconds(triggerDelay);
        EventTriggerEnter.Invoke();
    }

    IEnumerator ExitDelayed()
    {
        yield return new WaitForSeconds(triggerDelay);
        EventTriggerExit.Invoke();
    }
    #endregion

    #region Editor
#if UNITY_EDITOR
    [MenuItem("Tools/Trigger/ToggleBoundries #t")]
    static void ToggleBoundries() => showBoundries = !showBoundries;
#endif
    #endregion
}