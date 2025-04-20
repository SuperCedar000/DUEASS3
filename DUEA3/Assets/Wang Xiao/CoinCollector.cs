using UnityEngine;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    public TMP_Text coinText;  // 拖入 UI 文本
    private int coinCount = 0;

    private void Start()
    {
        UpdateCoinUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++;
            UpdateCoinUI();
            Destroy(other.gameObject);
        }
    }

    private void UpdateCoinUI()
    {
        coinText.text = "Coin " + coinCount;
    }
}
