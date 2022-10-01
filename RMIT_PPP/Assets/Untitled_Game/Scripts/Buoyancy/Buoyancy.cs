using UnityEngine;

namespace Khatim_F2
{
    public class Buoyancy : MonoBehaviour
    {
        #region Public Variables
        [Space, Header("Buoyancy Variables")]
        [SerializeField]
        [Tooltip("How much buoyancy does the object have?")]
        private float buoyancy = 20.0f;

        [SerializeField]
        [Tooltip("How much viscosity does the object have?")]
        private float viscosity;
        #endregion

        #region Private Variables
        private Rigidbody _rg;
        #endregion

        #region Unity Calbacks
        void Awake() => _rg = GetComponent<Rigidbody>();

        void FixedUpdate()
        {
            Vector3[] vertices = WaterDeformation.mesh.vertices;
            Vector3[] worldVertices = new Vector3[vertices.Length];

            for (int i = 0; i < vertices.Length; i++)
                worldVertices[i] = WaterDeformation.water.TransformPoint(vertices[i]);

            Vector3 nearestVertices = NearVertices(transform.position, worldVertices);

            if (transform.position.y < nearestVertices.y)
            {
                _rg.AddForce(Vector3.up * buoyancy);
                _rg.velocity /= ((viscosity / 100) + 1);
            }
        }
        #endregion

        #region My Functions
        Vector3 NearVertices(Vector3 position, Vector3[] vertices)
        {
            Vector3 nearestVertices = Vector3.zero;

            float minimumDistance = 100;

            for (int i = 0; i < vertices.Length; i++)
            {
                if (Vector3.Distance(position, vertices[i]) < minimumDistance)
                {
                    nearestVertices = vertices[i];
                    minimumDistance = Vector3.Distance(position, vertices[i]);
                }
            }
            return nearestVertices;
        }
        #endregion
    }
}