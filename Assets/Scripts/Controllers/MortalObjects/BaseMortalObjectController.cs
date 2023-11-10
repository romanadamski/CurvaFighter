using UnityEngine;

[RequireComponent(typeof(InteractableController))]
public abstract class BaseMortalObjectController : MonoBehaviour
{
    [SerializeField]
    protected bool infiniteLives;
    public bool InfiniteLives => infiniteLives;

    [SerializeField]
    protected bool respawnable;

    [SerializeField]
    protected uint livesCount;
    public uint LivesCount => livesCount;

    [SerializeField]
    protected float respawnDelay;

    protected AudioController audioController;

    private InteractableController _interactable;

    public uint CurrentLivesCount { get; protected set; }

    private void Awake()
    {
        _interactable = GetComponent<InteractableController>();
        audioController = GetComponent<AudioController>();
        SubscribeToInteractableEvents();
    }

    private void SubscribeToInteractableEvents()
    {
        _interactable.OnCollisionWithEnemyEnter += CollisionWithEnemyEnter;
        _interactable.OnCollisionWithEnemyExit += CollisionWithEnemyExit;
        _interactable.OnTriggerWithEnemyEnter += TriggerWithEnemyEnter;
        _interactable.OnTriggerWithEnemyExit += TriggerWithEnemyExit;
    }

    private void CollisionWithEnemyEnter(Collision2D collision2D)
    {
        DecrementLive();
        OnCollisionWithEnemyEnter(collision2D);
    }

    private void CollisionWithEnemyExit(Collision2D collision2D)
    {
        DecrementLive();
        OnCollisionWithEnemyExit(collision2D);
    }

    private void TriggerWithEnemyEnter(Collider2D collider2D)
    {
        DecrementLive();
        OnTriggerWithEnemyEnter(collider2D);
    }

    private void TriggerWithEnemyExit(Collider2D collider2D)
    {
        OnTriggerWithEnemyExit(collider2D);
    }

    protected virtual void OnCollisionWithEnemyEnter(Collision2D collision2D) { }
    protected virtual void OnCollisionWithEnemyExit(Collision2D collision2D) { }
    protected virtual void OnTriggerWithEnemyEnter(Collider2D collider2D) { }
    protected virtual void OnTriggerWithEnemyExit(Collider2D collider2D) { }

    protected void DecrementLive()
    {
        if (!infiniteLives && CurrentLivesCount > 0)
        {
            CurrentLivesCount--;
        }
    }

    protected void Respawn()
    {
        if (!respawnable ||
            !infiniteLives && CurrentLivesCount == 0) return;
        
        Invoke(nameof(DoRespawn), respawnDelay);
    }

    private void DoRespawn()
    {
        gameObject.SetActive(true);
    }

    protected void PlayAudio()
    {
        if (!audioController) return;

        audioController.PlayAudio();
    }
}
