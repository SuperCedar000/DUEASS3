using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float rotationSpeed = 100f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI victoryText;

    private Rigidbody rb;
    private bool isGrounded;
    private int score = 0;
    private bool hasWon = false; // ✅ 新增变量，标记是否已经胜利

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        UpdateScoreText();
        victoryText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!hasWon)
        {
            HandleMovement();
            HandleJump();
            HandleRotation();
        }
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 direction = transform.forward * moveZ + transform.right * moveX;
        Vector3 velocity = direction * moveSpeed;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            score++;
            UpdateScoreText();
            Destroy(other.gameObject);

            if (score == 7 && !hasWon) // ✅ 只在刚好收集到7个时调用
            {
                ShowVictory();
            }
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void ShowVictory()
    {
        hasWon = true; // ✅ 标记胜利，防止重复调用
        victoryText.gameObject.SetActive(true);
        victoryText.text = "Victory!";
        StartCoroutine(HideVictoryText());
    }

    IEnumerator HideVictoryText()
    {
        yield return new WaitForSeconds(3f);
        victoryText.gameObject.SetActive(false);
    }
}
