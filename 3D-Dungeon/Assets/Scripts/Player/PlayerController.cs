using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;
    private Vector2 curMoveInput;

    [Header("Jump")]
    public float jumpHeight;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minX;
    public float maxX;
    public float lookSensitivity;
    private float camCurXRot;

    private Vector2 mouseDelta;


    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMoveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMoveInput = Vector2.zero;
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;
        _rigidbody.velocity = dir;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && OnGround())
        {
            _rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
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

        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {

        }
    }
}
