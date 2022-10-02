using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Khatim_F2
{
    public class SharkPatrol : MonoBehaviour
    {
        #region Serialized Variables
        [SerializeField]
        [Tooltip("Shark Speed")]
        private float sharkSpeed = default;

        [SerializeField]
        [Tooltip("Shark waypoint Component")]
        private SharkWaypoints waypoints = default;

        [SerializeField]
        [Tooltip("Min distance to change to next Waypoint")]
        private float minWaypointDistance = default;
        #endregion

        #region Private Variables
        private Vector3[] _sharkWaypoints = default;
        private int _currentWayPointIndex = default;
        private float _waypointDistance = default;
        private NavMeshAgent _sharkAgent = default;
        #endregion

        #region Unity Callbacks
        void Start()
        {
            _sharkWaypoints = waypoints.GetPos();
            _sharkAgent = GetComponent<NavMeshAgent>();
            sharkSpeed = _sharkAgent.speed;
        }

        void Update() => SharkPatrolling();
        #endregion

        #region My Functions
        void SharkPatrolling()
        {
            //Vector3 target = _sharkWaypoints[_currentWayPointIndex];
            //target.y = transform.position.y;

            //_waypointDistance = Vector3.Distance(target, transform.position);
            _waypointDistance = Vector3.Distance(_sharkWaypoints[_currentWayPointIndex], transform.position);
            //transform.LookAt(target);

            //transform.position = Vector3.MoveTowards(transform.position, target, sharkSpeed * Time.deltaTime);
            _sharkAgent.SetDestination(_sharkWaypoints[_currentWayPointIndex]);

            if (_waypointDistance <= minWaypointDistance)
                _currentWayPointIndex++;

            if (_currentWayPointIndex == _sharkWaypoints.Length)
                _currentWayPointIndex = 0;
        }
        #endregion
    }
}