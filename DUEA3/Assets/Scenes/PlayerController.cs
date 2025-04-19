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

    public AudioClip pickupSound; // ✅ 引用音效
    private AudioSource audioSource; // ✅ 音效播放器组件

    private Rigidbody rb;
    private bool isGrounded;
    private int score = 0;
    private bool hasWon = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>(); // ✅ 获取 AudioSource 组件

        // 限制角色不翻倒
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
            transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime); // Q 左转
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime); // E 右转
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

            // ✅ 播放音效
            if (pickupSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            if (score == 7 && !hasWon)
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
        hasWon = true;
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
