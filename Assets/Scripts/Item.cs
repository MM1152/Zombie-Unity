using UnityEngine;

public abstract class Item : MonoBehaviour
{
    static readonly string playerTag = "Player";

    public abstract void GetItem(Collider collder);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            GetItem(other);
            Destroy(gameObject);
        }
    }
}
