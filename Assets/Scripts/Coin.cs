using UnityEngine;

public class Coin : Item
{
    public override void GetItem(Collider collder)
    {
        var playerShooter = collder.GetComponent<PlayerShooter>();
        if(playerShooter != null)
        {
            playerShooter.GetCoin();
        }
    }
}
