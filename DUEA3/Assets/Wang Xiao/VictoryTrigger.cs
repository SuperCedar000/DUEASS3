using UnityEngine;
using TMPro;

public class VictoryTrigger : MonoBehaviour
{
    public GameObject winTextObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 角色必须是 Player tag
        {
            winTextObject.SetActive(true); // 显示胜利文本
        }
    }
}
