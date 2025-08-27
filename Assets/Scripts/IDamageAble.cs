using UnityEngine;

public interface IDamageAble
{
    public void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
