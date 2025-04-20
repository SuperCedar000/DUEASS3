using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // ��ɫ��Transform�����ø����Ŀ�꣩
    public float distance = 5.0f; // ������ɫ֮��ľ���
    public float xSpeed = 120.0f; // �����ˮƽ��ת�ٶ�
    public float ySpeed = 120.0f; // ����Ĵ�ֱ��ת�ٶ�

    public float yMinLimit = -20f; // ��ֱ��ת����С�Ƕ�
    public float yMaxLimit = 80f;  // ��ֱ��ת�����Ƕ�

    public float smoothTime = 0.2f; // ���ƽ������ʱ��

    private float x = 0.0f;
    private float y = 0.0f;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        // ��ʼ���������ת�Ƕ�
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        // ���û��Ŀ����ִ���κβ���
        if (target == null)
            return;

        // ��ȡ������루X����ת��Y����ת��
        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

        // ���ƴ�ֱ��ת�Ƕ�
        y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

        // ���������Ŀ��λ��
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 targetPosition = target.position - (rotation * Vector3.forward * distance);

        // ƽ���������λ��
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // ʹ���ʼ�տ����ɫ
        transform.LookAt(target);
    }
}

