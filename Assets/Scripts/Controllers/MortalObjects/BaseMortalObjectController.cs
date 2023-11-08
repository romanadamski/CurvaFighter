using System;
using System.Linq;
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
        _interactable.OnCollisionWithEnemyEnter += OnCollisionWithEnemyEnter;
        _interactable.OnCollisionWithEnemyExit += OnCollisionWithEnemyExit;
        _interactable.OnTriggerWithEnemyEnter += OnTriggerWithEnemyEnter;
        _interactable.OnTriggerWithEnemyExit += OnTriggerWithEnemyExit;
    }
    //todo overriding
    protected virtual void OnCollisionWithEnemyEnter(Collision2D collision2D)
    {
        DecrementLive();
    }

    protected virtual void OnCollisionWithEnemyExit(Collision2D collision2D)
    {
        DecrementLive();
    }

    protected virtual void OnTriggerWithEnemyEnter(Collider2D collider2D)
    {
        DecrementLive();
    }

    protected virtual void OnTriggerWithEnemyExit(Collider2D collider2D)
    {

    }

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
