using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

namespace Khatim.PPP
{
    public class InfoTab : MonoBehaviour
    {
        #region Serialized Variables

        #region GameObjects
        [Space, Header("GameObjects")]
        [SerializeField]
        [Tooltip("Vertical Panel GameObjects")]
        private GameObject[] verticalPanels = default;
        #endregion

        #region Strings
        [Space, Header("Strings")]
        [SerializeField]
        [Tooltip("Strings to change the button text")]
        private string[] verticalPanelsTexts = default;
        #endregion

        #region UI
        [Space, Header("UI")]
        [SerializeField]
        [Tooltip("Text where the strings applied to")]
        private TextMeshProUGUI buttonText = default;
        #endregion

        #endregion

        #region Private Variables
        private int _currPanelIndex = default;
        #endregion

        #region My Functions
        /// <summary>
        /// Toggles between the Info Panels;
        /// </summary>
        public void OnClick_ToggleInfo()
        {
            for (int i = 0; i < verticalPanels.Length; i++)
                verticalPanels[i].SetActive(false);

            verticalPanels[_currPanelIndex].SetActive(true);
            buttonText.text = verticalPanelsTexts[_currPanelIndex];

            _currPanelIndex++;

            if (_currPanelIndex == verticalPanels.Length)
                _currPanelIndex = 0;
        }
        #endregion
    }
}