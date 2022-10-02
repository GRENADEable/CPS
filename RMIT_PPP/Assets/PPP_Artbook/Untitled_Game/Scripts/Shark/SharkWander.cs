using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Khatim_F2
{
    public class SharkWander : MonoBehaviour
    {
        #region Serialized Variables
        [Space, Header("Shark")]
        [SerializeField]
        [Tooltip("Shark Speed")]
        private float sharkSpeed = default;

        [Space, Header("Wander Variables")]
        [SerializeField]
        [Tooltip("Shark Wander Radius for Gizmo")]
        private float wanderRadius = default;

        [SerializeField]
        [Tooltip("How long is each wandering stage gonna last?")]
        private float maxWanderTimer = default;
        #endregion

        #region Private Variables
        private NavMeshAgent _sharkAgent = default;
        private float _timer = default;
        #endregion

        #region Unity Callbacks
        void Start()
        {
            _sharkAgent = GetComponent<NavMeshAgent>();
            sharkSpeed = _sharkAgent.speed;
        }

        void Update() => SharkWandering();

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, wanderRadius);
        }
        #endregion

        #region My Functions
        void SharkWandering()
        {
            _timer += Time.deltaTime;
            if (_timer >= maxWanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                _sharkAgent.SetDestination(newPos);
                _timer = 0;
            }
        }

        static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
        {
            //Sets a random position inside the sphere and that is multiplied with the distance and the center of the sphere.
            Vector3 randomPos = Random.insideUnitSphere * dist;

            //Vector 3 position is returned to the origin parameter.
            randomPos += origin;

            //Bool check if the random position is suitable on the navmesh. If true, then return the hit position.
            NavMesh.SamplePosition(randomPos, out NavMeshHit hit, dist, layermask);
            return hit.position;
        }
        #endregion
    }
}