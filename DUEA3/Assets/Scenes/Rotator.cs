using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        // 每一帧围绕Y轴旋转
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
