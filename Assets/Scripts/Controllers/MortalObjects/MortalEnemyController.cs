using UnityEngine;

public class MortalEnemyController : BaseMortalObjectController
{
    protected override void OnTriggerWithEnemyEnter(Collider2D collision)
    {
        PlayAudio();

        gameObject.SetActive(false);
        Respawn();
    }
}
