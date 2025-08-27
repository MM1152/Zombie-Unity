using UnityEngine;

public class IncreseAmmo : Item
{
    public override void GetItem(Collider collider)
    {
        var findObj = collider.GetComponent<PlayerShooter>();
        if(findObj != null)
        {
            findObj.gun.IncreaseAmmo(10);
        }
    }
}
