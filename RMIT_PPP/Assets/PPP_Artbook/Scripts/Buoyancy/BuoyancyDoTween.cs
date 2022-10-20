using UnityEngine;
using DG.Tweening;

namespace Khatim_F2
{
    public class BuoyancyDoTween : MonoBehaviour
    {
        #region Serialized Variables
        [Space, Header("Buoyancy Variables")]
        [SerializeField]
        [Tooltip("Buouancy speed of the boat")]
        private float buoyancySpeed = default;

        [SerializeField]
        [Tooltip("How much to offset on Y?")]
        private float offsetY = default;
        #endregion

        #region Unity Calbacks
        void Start() => transform.DOMoveY(offsetY, buoyancySpeed).SetLoops(-1, LoopType.Yoyo);
        #endregion
    }
}