using UnityEngine;

public class GoldCupTrigger : MonoBehaviour
{
    // 用来显示胜利信息的面板
    public GameObject victoryPanel;

    // 当玩家触碰金杯时
    private void OnTriggerEnter(Collider other)
    {
        // 确保只有玩家触碰到金杯时才触发
        if (other.CompareTag("Player"))
        {
            ShowVictory();
        }
    }

    // 显示胜利面板
    private void ShowVictory()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);  // 激活胜利面板
        }
    }
}
