using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float rotationSpeed = 100f;  // 旋转速度

    void Update()
    {
        // 每秒绕 Y 轴旋转
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
