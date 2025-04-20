using UnityEngine;
using UnityEngine.UI;

public class TrophyCollector : MonoBehaviour
{
    public int silverCollected = 0;
    public bool bronzeCollected = false;

    public Text messageText; // �� UI ����ʾ��ʾ��Ϣ
    public GameObject winPanel; // ��Ϸʤ�����

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SilverTrophy"))
        {
            silverCollected++;
            Destroy(other.gameObject);
            ShowMessage("�ռ���һ����������");
        }
        else if (other.CompareTag("BronzeTrophy"))
        {
            bronzeCollected = true;
            Destroy(other.gameObject);
            ShowMessage("�ռ���ͭ������");
        }
        else if (other.CompareTag("GoldTrophy"))
        {
            if (silverCollected >= 2 && bronzeCollected)
            {
                Destroy(other.gameObject);
                ShowMessage("��Ӯ�ˣ�");
                if (winPanel != null)
                    winPanel.SetActive(true); // ��ʾʤ������
            }
            else
            {
                ShowMessage("����δ���㣬���ռ�������������һ��ͭ������");
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
