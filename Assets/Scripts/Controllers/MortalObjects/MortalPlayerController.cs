using UnityEngine;

public class MortalPlayerController : BaseMortalObjectController
{
    [SerializeField]
    private bool mainPlayer;

    protected override void OnTriggerWithEnemyEnter(Collider2D collision)
    {
        if (mainPlayer)
        {
            EventsManager.Instance.OnPlayerLoseLife(CurrentLivesCount);
        }
        else
        {
            gameObject.SetActive(false);
            Respawn();
        }
    }

    private void Start()
    {
        CurrentLivesCount = livesCount;
        if (mainPlayer)
        {
            EventsManager.Instance.OnPlayerSpawned(CurrentLivesCount);
        }
    }
}
