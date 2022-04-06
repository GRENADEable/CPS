using UnityEngine;

public class MouseEnableDisable : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField]
    [Tooltip("Is the mouse enabled or disabled?")]
    private bool isMouseEnabled = default;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        if (isMouseEnabled)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    #endregion
}