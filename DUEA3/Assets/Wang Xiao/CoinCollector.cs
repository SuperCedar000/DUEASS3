using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public int coinCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++;
            Destroy(other.gameObject);  // 收集金币后移除金币

            Debug.Log("收集金币！当前金币数：" + coinCount);
        }
    }
}
