using UnityEngine;

public class BulletCollision : Projectile
{
    [SerializeField] protected float inactiveAtTheCollisionTime = 0.05f;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        trailRenderer.Clear();

        Invoke("SetInActive", inactiveAtTheCollisionTime);
    }

    protected override void SetInActive()
    {
        base.SetInActive();
        trailRenderer.Clear();
    }
}
