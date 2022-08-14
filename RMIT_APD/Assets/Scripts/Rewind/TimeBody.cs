using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField]
    [Tooltip("How many seconds do you want to record?")]
    private float recordTime = 5f;
    #endregion

    #region Private Variables
    private bool _isRewinding = false;
    private List<PointInTime> _pointsInTime;
    private Rigidbody _rg;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _pointsInTime = new List<PointInTime>();
        _rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartRewind();
        if (Input.GetMouseButtonUp(0))
            StopRewind();
    }

    void FixedUpdate()
    {
        if (_isRewinding)
            Rewind();
        else
            Record();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
            Destroy(gameObject);
    }
    #endregion

    #region My Functions
    void StartRewind()
    {
        _isRewinding = true;
        _rg.isKinematic = true;
    }

    void StopRewind()
    {
        _isRewinding = false;
        _rg.isKinematic = false;
    }


    void Rewind()
    {
        if (_pointsInTime.Count > 0)
        {
            PointInTime pointInTime = _pointsInTime[0];
            transform.SetPositionAndRotation(pointInTime.position, pointInTime.rotation);
            _pointsInTime.RemoveAt(0);
        }
        else
            StopRewind();
    }

    void Record()
    {
        if (_pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
            _pointsInTime.RemoveAt(_pointsInTime.Count - 1);

        _pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }
    #endregion
}