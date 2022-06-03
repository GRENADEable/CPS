using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField]
    [Tooltip("Object Rotate Type")]
    private RotateType _currRotType = RotateType.None;
    private enum RotateType { None, XRotate, YRotate, ZRotate };

    [SerializeField]
    [Tooltip("Rotation Speed")]
    private float rotationSpeed = default;
    #endregion

    #region Unity Callbacks
    void Update()
    {
        switch (_currRotType)
        {
            case RotateType.None:

                break;

            case RotateType.XRotate:
                transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.right);
                break;

            case RotateType.YRotate:
                transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up);
                break;

            case RotateType.ZRotate:
                transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
                break;

            default:
                break;
        }

        
    }
    #endregion
}