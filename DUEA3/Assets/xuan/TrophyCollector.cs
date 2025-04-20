using UnityEngine;
using UnityEngine.UI;

public class TrophyCollector : MonoBehaviour
{
    public int silverCollected = 0;
    public bool bronzeCollected = false;

    public Text messageText; // 在 UI 上显示提示信息
    public GameObject winPanel; // 游戏胜利面板

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SilverTrophy"))
        {
            silverCollected++;
            Destroy(other.gameObject);
            ShowMessage("收集到一个银奖杯！");
        }
        else if (other.CompareTag("BronzeTrophy"))
        {
            bronzeCollected = true;
            Destroy(other.gameObject);
            ShowMessage("收集到铜奖杯！");
        }
        else if (other.CompareTag("GoldTrophy"))
        {
            if (silverCollected >= 2 && bronzeCollected)
            {
                Destroy(other.gameObject);
                ShowMessage("你赢了！");
                if (winPanel != null)
                    winPanel.SetActive(true); // 显示胜利界面
            }
            else
            {
                ShowMessage("条件未满足，先收集两个银奖杯和一个铜奖杯！");
            }
        }
    }

    void ShowMessage(string msg)
    {
        if (messageText != null)
        {
            messageText.text = msg;
            CancelInvoke("ClearMessage");
            Invoke("ClearMessage", 2f);
        }
    }

    void ClearMessage()
    {
        if (messageText != null)
            messageText.text = "";
    }
}
