using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10.0f;

    private Rigidbody rb;
    private bool isGrounded = false;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 检测是否在地面上
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
        {
            if (hit.collider.gameObject.tag == "Ground")
            {
                isGrounded = true;
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 计算移动方向
        moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        moveDirection = moveDirection.normalized;

        // 移动角色
        if (moveDirection != Vector3.zero)
        {
            rb.AddForce(moveDirection * speed);
        }
        else
        {
            // 如果没有输入，停止角色移动
            rb.velocity = Vector3.zero;
        }

        // 跳跃
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}