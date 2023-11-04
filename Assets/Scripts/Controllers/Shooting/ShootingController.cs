using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField]
    private string bulletTypeName;
    [SerializeField]
    private ObjectPoolingController objectPoolingController;

    private ShootingTrigger _shootingTrigger;

    private void Awake()
    {
        _shootingTrigger = GetComponent<ShootingTrigger>();

        _shootingTrigger.OnUpdate += OnUpdate;
    }

    private void OnUpdate()
    {
        if (_shootingTrigger.TriggerShoot)
        {
            ReleaseBullet();
        }
    }

    private void ReleaseBullet()
    {
        var bullet = GetBulletFromPool();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.gameObject.SetActive(true);

        EventsManager.Instance.OnBulletFired();
    }

    private BaseBulletMovementController GetBulletFromPool()
    {
        return objectPoolingController.GetFromPool().GetComponent<BaseBulletMovementController>();
    }
}
