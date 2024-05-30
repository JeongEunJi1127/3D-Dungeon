using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;
    private Vector2 curMoveInput;

    [Header("Jump")]
    public float jumpHeight;
    public float jumpItemHeight;
    public float jumpStamina;
    public LayerMask groundLayerMask;

    [Header("Run")]
    public float runSpeed;

    [Header("Look")]
    public Transform cameraContainer;
    public float minX;
    public float maxX;
    public float lookSensitivity;
    private float camCurXRot;

    [Header("Inventory")]
    public GameObject Inventory;

    private Vector2 mouseDelta;

    private bool canLook = true;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook && !CharacterManager.Instance.Player.condition.isDie)
        {
            CameraLook();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMoveInput = context.ReadValue<Vector2>();
            _animator.SetBool("Walk", true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMoveInput = Vector2.zero;
            _animator.SetBool("Walk", false);
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;
        _rigidbody.velocity = dir;
    }
    
    public void RunForAWhile(float runTime)
    {       
        StartCoroutine(RunningTime(runTime));
    }

    IEnumerator RunningTime(float time)
    {
        float curMoveSpeed = moveSpeed;
        moveSpeed = runSpeed;
        yield return new WaitForSeconds(time);
        moveSpeed = curMoveSpeed;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    void CameraLook()
    {
        // 마우스가 위로 올라가면 카메라는 아래로 내려가야함 -> x축 Rotate이므로
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minX, maxX);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0,mouseDelta.x*lookSensitivity, 0);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && OnGround())
        {
            Jump(jumpHeight);
        }
    }

    public void Jump(float jumpHeight)
    {
        if (CharacterManager.Instance.Player.condition.GetStamina() >= jumpStamina)
        {
            UseJumpStamina();
            _animator.SetTrigger("IsJump");
            _rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    public void JumpHighForAWhile(float jumpHighTime)
    {
        StartCoroutine(JumpHighTime(jumpHighTime));
    }

    IEnumerator JumpHighTime(float time)
    {
        float curJumpHeight = jumpHeight;
        jumpHeight = jumpItemHeight;
        yield return new WaitForSeconds(time);
        jumpHeight = curJumpHeight;
    }

    public void UseJumpStamina()
    {
        CharacterManager.Instance.Player.condition.UseStamina(jumpStamina);
    }

    bool OnGround()
    {
        Ray[] ray = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f),Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f),Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f),Vector3.down)
        };

        for (int i = 0; i < ray.Length; i++)
        {
            if (Physics.Raycast(ray[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            ToggleCursor();
            if (Inventory.activeInHierarchy) Inventory.SetActive(false);
            else  Inventory.SetActive(true);
        }
    }

    public void ToggleCursor()
    {
        // toggle은 현재 커서가 잠겨있는지 여부
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        // 잠겨있으면 풀고, 안잠겨있으면 잠궈버림
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
