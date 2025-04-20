using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // 角色的Transform（设置跟随的目标）
    public float distance = 5.0f; // 相机与角色之间的距离
    public float xSpeed = 120.0f; // 相机的水平旋转速度
    public float ySpeed = 120.0f; // 相机的垂直旋转速度

    public float yMinLimit = -20f; // 垂直旋转的最小角度
    public float yMaxLimit = 80f;  // 垂直旋转的最大角度

    public float smoothTime = 0.2f; // 相机平滑跟随时间

    private float x = 0.0f;
    private float y = 0.0f;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        // 初始化相机的旋转角度
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        // 如果没有目标则不执行任何操作
        if (target == null)
            return;

        // 获取鼠标输入（X轴旋转和Y轴旋转）
        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

        // 限制垂直旋转角度
        y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

        // 计算相机的目标位置
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 targetPosition = target.position - (rotation * Vector3.forward * distance);

        // 平滑过渡相机位置
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // 使相机始终看向角色
        transform.LookAt(target);
    }
}

