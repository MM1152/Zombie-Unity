using UnityEngine;

public class Rotation : MonoBehaviour
{

    public float speed = 10f;

    private void Update()
    {
        transform.rotation *= Quaternion.Euler(0f, speed * Time.deltaTime, 0f);
    }
}
