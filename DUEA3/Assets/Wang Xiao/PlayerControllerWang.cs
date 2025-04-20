using UnityEngine;

public class PlayerControllerWang : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度
    public float turnSpeed = 700f; // 旋转速度
    public float jumpHeight = 2f; // 跳跃高度
    public float crouchHeight = 0.5f; // 蹲下时的身高
    public float normalHeight = 2f; // 正常站立时的身高
    private float gravity = -9.8f; // 重力

    private CharacterController controller;
    private Camera playerCamera;
    private Vector3 velocity; // 角色的速度
    private bool isGrounded; // 是否在地面上
    private bool isCrouching = false; // 是否蹲下
    private float cameraRotationX = 0f; // 摄像机的旋转

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main; // 获取主摄像机
    }

    private void Update()
    {
        HandleMovement();
        HandleJumpAndCrouch();
        HandleCameraFollow();
    }

    // 处理角色移动
    private void HandleMovement()
    {
        isGrounded = controller.isGrounded; // 检查角色是否在地面上
        float horizontal = Input.GetAxis("Horizontal"); // A 和 D 键（左右移动）
        float vertical = Input.GetAxis("Vertical"); // W 和 S 键（前后移动）

        // 计算角色的移动方向
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;

        // 移动角色
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // 旋转角色使其面向移动的方向（这里只控制角色前后移动的方向，而不改变角色的旋转）
        if (moveDirection.magnitude > 0)
        {
            // 只在前后方向上进行旋转，不影响左右移动
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
    }

    // 处理跳跃和蹲下
    private void HandleJumpAndCrouch()
    {
        if (isGrounded)
        {
            velocity.y = -2f; // 重置垂直速度，防止一直向下掉

            if (Input.GetKeyDown(KeyCode.Space)) // 空格键跳跃
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // 跳跃公式
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) && !isCrouching) // Ctrl 键蹲下
            {
                isCrouching = true;
                controller.height = crouchHeight;
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouching) // 再次按 Ctrl 键站起来
            {
                isCrouching = false;
                controller.height = normalHeight;
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime; // 应用重力
        }

        // 应用跳跃和重力
        controller.Move(velocity * Time.deltaTime);
    }

    // 处理摄像机跟随
    private void HandleCameraFollow()
    {
        // 计算摄像机的位置，保持在角色的后方
        Vector3 targetPosition = transform.position - transform.forward * 5f + Vector3.up * 2f;
        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, targetPosition, Time.deltaTime * 5f);

        // 鼠标的 X 轴和 Y 轴偏移
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        // 限制摄像机上下旋转的角度，避免摄像机翻转
        cameraRotationX += mouseY;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -30f, 60f); // 限制上下旋转角度

        // 旋转摄像机（上下旋转和左右旋转）
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, playerCamera.transform.localRotation.eulerAngles.y, 0);

        // 角色左右旋转
        transform.Rotate(Vector3.up * mouseX * turnSpeed * Time.deltaTime);
    }
}
