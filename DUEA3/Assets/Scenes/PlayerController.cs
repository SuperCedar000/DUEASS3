using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float rotationSpeed = 100f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI victoryText;
    public TextMeshProUGUI taskText;         // ✅ 任务提示文本

    public AudioClip pickupSound;
    public AudioClip victorySound;

    public Button restartButton;

    private AudioSource audioSource;
    private Rigidbody rb;
    private bool isGrounded;
    private int score = 0;
    private bool hasWon = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        UpdateScoreText();
        victoryText.gameObject.SetActive(false);

        // ✅ 重启按钮一直显示，不再隐藏
        // restartButton.gameObject.SetActive(false);  <-- 删除或注释此行

        // ✅ 显示开场任务提示
        if (taskText != null)
        {
            StartCoroutine(ShowTaskText());
        }
    }

    void Update()
    {
        // ✅ 胜利后也可以继续移动
        HandleMovement();
        HandleJump();
        HandleRotation();
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

        if (victorySound != null && audioSource != null)
        {
            audioSource.PlayOneShot(victorySound);
        }

        if (restartButton != null)
            restartButton.gameObject.SetActive(true);

        StartCoroutine(HideVictoryText());
    }

    IEnumerator HideVictoryText()
    {
        yield return new WaitForSeconds(3f);
        victoryText.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ShowTaskText()
    {
        taskText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        taskText.gameObject.SetActive(false);
    }
}
