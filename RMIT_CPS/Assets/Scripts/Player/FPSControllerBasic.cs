using UnityEngine;

public class FPSControllerBasic : MonoBehaviour
{
    #region Serialized Variables

    #region Inputs
    [Space, Header("Player Keys")]
    [SerializeField]
    [Tooltip("Which key to press when running")]
    private KeyCode runKey = KeyCode.LeftShift;

    [SerializeField]
    [Tooltip("Which key to press when jumping")]
    private KeyCode jumpKey = KeyCode.Space;
    #endregion

    #region Player Variables
    [Space, Header("Player Variables")]
    [SerializeField]
    [Tooltip("Walk speed of the player")]
    private float playerWalkSpeed = 3f;

    [SerializeField]
    [Tooltip("Run speed of the player")]
    private float playerRunSpeed = 6f;

    [SerializeField]
    [Tooltip("Gravity of the player when falling")]
    private float gravity = -9.81f;
    #endregion

    #region Ground Check
    [Space, Header("Ground Check")]
    [SerializeField]
    [Tooltip("Transform Component for checking the ground")]
    private Transform groundCheck = default;

    [SerializeField]
    [Tooltip("Spherecast radius for the ground")]
    private float groundDistance = 0.4f;

    [SerializeField]
    [Tooltip("Which layer(s) is used for the ground?")]
    private LayerMask groundMask = default;
    #endregion

    #region Player Jump
    [Space, Header("Jump Variables")]
    [SerializeField]
    [Tooltip("Can the player Jump?")]
    private bool canJump = true;

    [SerializeField]
    [Tooltip("Power of how high the player can jump")]
    private float jumpPower = 2.5f;
    #endregion

    #endregion

    #region Private Variables
    [Header("Player Variables")]
    private CharacterController _charControl = default;
    private Vector3 _vel = default;
    private float _currSpeed = default;
    private bool _isGrounded = default;
    private bool _isJumping = default;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _charControl = GetComponent<CharacterController>();
        _currSpeed = playerWalkSpeed;
    }

    void Update()
    {
        GroundCheck();
        PlayerCurrStance();
        PlayerMovement();

        if (Input.GetKeyDown(jumpKey) && canJump)
            PlayerJump();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
            Application.LoadLevel(Application.loadedLevel);
    }
    #endregion

    #region My Functions
    /// <summary>
    /// Ground check for gavity and jumping;
    /// </summary>
    void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded && _vel.y < 0)
        {
            _isJumping = false;
            _vel.y = -2f;
        }

        _vel.y += gravity * Time.deltaTime;
        _charControl.Move(_vel * Time.deltaTime);
    }

    /// <summary>
    /// This is where the player movement takes place;
    /// </summary>
    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.right * horizontal + transform.forward * vertical).normalized;
        _charControl.Move(_currSpeed * Time.deltaTime * moveDirection);
    }

    /// <summary>
    /// Checks stance if the player is Running;
    /// </summary>
    void PlayerCurrStance()
    {
        if (Input.GetKeyDown(runKey))
        {
            _currSpeed = playerRunSpeed;
            //Debug.Log("Running");
        }

        if (Input.GetKeyUp(runKey))
        {
            _currSpeed = playerWalkSpeed;
            //Debug.Log("Walking");
        }
    }

    /// <summary>
    /// Jump's player;
    /// </summary>
    void PlayerJump()
    {
        if (_isGrounded && !_isJumping)
        {
            float jumpForce = Mathf.Sqrt(jumpPower * Mathf.Abs(gravity) * 2);
            _vel.y += jumpForce;
            _isJumping = true;
        }
    }
    #endregion
}