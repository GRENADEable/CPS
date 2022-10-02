using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Khatim_F2
{
    public class SharkWaypoints : MonoBehaviour
    {
        #region Private Variables
        private Vector3[] waypointPos = default;
        #endregion

        #region Unity Callbacks
        void Awake()
        {
            waypointPos = new Vector3[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
                waypointPos[i] = transform.GetChild(i).transform.position;
        }

        /// <summary>
        /// Gets the Vector3 Positions of the waypoints in the scene and stores them in the SharkAI;
        /// </summary>
        /// <returns> Returns Vector3 Positions; </returns>
        public Vector3[] GetPos() => waypointPos;
        #endregion
    }
}