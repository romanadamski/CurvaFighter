using UnityEngine;

public class MortalPlayerBulletController : BaseMortalObjectController
{
    protected override void OnTriggerWithEnemyEnter(Collider2D collider)
    {
        ShootingManager.Instance.ObjectPoolingController.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
        EventsManager.Instance.OnScore();
    }
}
