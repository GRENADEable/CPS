using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Khatim.PPP
{
    [CreateAssetMenu(fileName = "Production_Data", menuName = "Production/ProductionData")]
    public class ProductionData : ScriptableObject
    {
        #region Public Variables
        public string productionDate;
        public Sprite productionImg;
        #endregion
    }
}