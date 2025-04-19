using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 10.0f; // 跳跃力度
    private Rigidbody rb; // 角色的Rigidbody组件

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // 获取Rigidbody组件
    }

    void Update()
    {
        // 检测跳跃输入
        if (Input.GetKeyDown(KeyCode.Space)) // 检测空格键按下
        {
            Jump();
        }
    }

    void Jump()
    {
        // 检查角色是否在地面上
        if (IsGrounded())
        {
            // 应用向上的力来实现跳跃
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        // 检测角色是否在地面上
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f))
        {
            if (hit.collider.CompareTag("Ground")) // 确保地面的标签是"Ground"
            {
                return true;
            }
        }
        return false;
    }
}