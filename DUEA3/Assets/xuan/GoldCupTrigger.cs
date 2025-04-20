using UnityEngine;

public class GoldCupTrigger : MonoBehaviour
{
    // ������ʾʤ����Ϣ�����
    public GameObject victoryPanel;

    // ����Ҵ�����ʱ
    private void OnTriggerEnter(Collider other)
    {
        // ȷ��ֻ����Ҵ�������ʱ�Ŵ���
        if (other.CompareTag("Player"))
        {
            ShowVictory();
        }
    }

    // ��ʾʤ�����
    private void ShowVictory()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);  // ����ʤ�����
        }
    }
}
