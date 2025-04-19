using UnityEngine;

public class SpinningSnowman : MonoBehaviour
{
    // 旋转速度
    public float spinSpeed = 50.0f;

    void Update()
    {
        // 绕Y轴旋转
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }
}