using UnityEngine;
using DG.Tweening;


namespace Khatim_F2
{
    public class BoatSailDoTween : MonoBehaviour
    {
        #region Serialized Variables
        [Space, Header("Sail Variables")]
        [SerializeField]
        [Tooltip("Rotate speed of the sail")]
        private float sailSpeed = default;

        [SerializeField]
        [Tooltip("How much rotate?")]
        private Vector3 rotateVec = default;
        #endregion

        #region Unity Calbacks
        void Start() => transform.DORotate(rotateVec, sailSpeed).SetLoops(-1, LoopType.Yoyo);
        #endregion
    }
}