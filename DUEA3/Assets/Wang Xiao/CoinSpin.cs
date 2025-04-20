using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public Vector3 rotationAxis = new Vector3(15f, 100f, 45f);

    void Update()
    {
        transform.Rotate(rotationAxis * Time.deltaTime);
    }
}
