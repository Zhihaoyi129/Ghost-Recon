using TMPro;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 2f;  // 旋转速度
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;
    private bool canControl;

    void Start() => controller = GetComponent<CharacterController>();

    public void SetControl(bool state)
    {
        canControl = state;
        // 在游戏过程中，不再锁定光标，光标始终可见
        Cursor.lockState = CursorLockMode.None;  // 光标不锁定
        Cursor.visible = true;  // 始终显示光标
        if (!state) velocity = Vector3.zero;
    }

    public void ResetPosition(Vector3 position)
    {
        controller.enabled = false;
        transform.position = position;
        controller.enabled = true;
    }

    void Update()
    {
        if (!canControl) return;

        HandleMovement();  // 控制前进后退
        HandleRotation();  // 控制视角旋转
    }

    // 控制前进和后退
    void HandleMovement()
    {
        float moveDirectionY = velocity.y;

        // 获取 `W` 和 `S` 键（前进和后退）
        float moveForward = Input.GetAxis("Vertical");  // `W` 为正，`S` 为负
        float moveRight = Input.GetAxis("Horizontal");  // `A` 为负，`D` 为正

        // 计算水平移动
        Vector3 move = (transform.right * moveRight + transform.forward * moveForward) * moveSpeed;
        controller.Move(move * Time.deltaTime);

        // 如果角色在地面上
        if (controller.isGrounded)
        {
            velocity.y = -2f;  // 让角色保持在地面
            if (Input.GetButtonDown("Jump"))
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);  // 跳跃
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // 控制视角旋转（左右控制）
    void HandleRotation()
    {
        // 获取 `A` 和 `D` 键（旋转）
        float horizontalRotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // 旋转角色（左右控制）
        transform.Rotate(0, horizontalRotation, 0);
    }
}
