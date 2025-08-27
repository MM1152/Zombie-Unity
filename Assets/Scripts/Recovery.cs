using UnityEngine;

public class Recovery : Item
{
    public override void GetItem(Collider collider)
    {
        collider.GetComponent<PlayerHealth>()?.Heal(10);
    }
}
