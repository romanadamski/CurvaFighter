using UnityEngine;

public class MortalEnemyBulletController : BaseMortalObjectController
{
    protected override void OnTriggerWithEnemyEnter(Collider2D collider)
    {
        ShootingManager.Instance.ObjectPoolingController.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
    }
}
