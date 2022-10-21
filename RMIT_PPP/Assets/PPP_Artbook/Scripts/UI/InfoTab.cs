using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Khatim.PPP
{
    public class InfoTab : MonoBehaviour
    {
        #region Serialized Variables

        #region Datas
        [Space, Header("Datas")]
        [SerializeField]
        [Tooltip("Artbook Datas")]
        private ArtData artData = default;
        #endregion

        #region Ints
        [Space, Header("Ints")]
        [SerializeField]
        [Tooltip("Index to show what info")]
        private int infoIndex = default;
        #endregion

        #region GameObjects
        [Space, Header("GameObjects")]

        [SerializeField]
        [Tooltip("Vertical Scroll Tab")]
        private GameObject verticalScrollTab = default;
        #endregion

        #region UIs
        [Space, Header("UIs")]
        [SerializeField]
        [Tooltip("First Arrow Image")]
        private Image arrowImg1 = default;
        #endregion

        #endregion

        #region Private Variables
        private bool _isShowingInfo = default;
        #endregion

        #region Unity Callbacks

        #region Events
        void OnEnable()
        {
            ArtbookManager.OnHoverPeekInfo += OnHoverPeekInfoEventReceived;
            ArtbookManager.OnHoverUnPeekInfo += OnHoverUnPeekInfoEventReceived;
        }

        void OnDisable()
        {
            ArtbookManager.OnHoverPeekInfo -= OnHoverPeekInfoEventReceived;
            ArtbookManager.OnHoverUnPeekInfo -= OnHoverUnPeekInfoEventReceived;
        }

        void OnDestroy()
        {
            ArtbookManager.OnHoverPeekInfo -= OnHoverPeekInfoEventReceived;
            ArtbookManager.OnHoverUnPeekInfo -= OnHoverUnPeekInfoEventReceived;
        }
        #endregion

        void Start()
        {

        }

        void Update()
        {

        }
        #endregion

        #region My Functions
        void PeekInfo()
        {

        }

        void UnPeekInfo()
        {

        }

        public void OnClick_ToggleInfo()
        {
            _isShowingInfo = !_isShowingInfo;

            if (_isShowingInfo)
            {
                arrowImg1.DOFillAmount(1, 0.2f);
            }
            else
            {
                arrowImg1.DOFillAmount(0, 0f);
            }
        }
        #endregion

        #region Coroutines

        #endregion

        #region Events
        /// <summary>
        /// Subbed to event from ArtbookManager Script;
        /// Shows info according to the index;
        /// </summary>
        /// <param name="index"> Index received should be the same as the infoIndex for the right information; </param>
        void OnHoverPeekInfoEventReceived(int index)
        {
            if (index == infoIndex)
            {
                PeekInfo();
                //Debug.Log($"Showing Tab for {index}");
            }
        }

        /// <summary>
        /// Subbed to event from ArtbookManager Script;
        /// Hides info according to the index;
        /// </summary>
        /// <param name="index"> Index received should be the same as the infoIndex for the right information; </param>
        void OnHoverUnPeekInfoEventReceived(int index)
        {
            if (index == infoIndex)
            {
                UnPeekInfo();
                //Debug.Log($"Hiding Tab for {index}");
            }
        }
        #endregion
    }
}