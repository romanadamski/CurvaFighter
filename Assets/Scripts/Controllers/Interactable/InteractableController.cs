using System;
using System.Linq;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    [SerializeField]
    private InteractableObjectSO interactableObjectSO;

    public event Action <Collision2D> OnCollisionWithEnemyEnter;
    public event Action<Collision2D> OnCollisionWithEnemyExit;
    public event Action<Collider2D> OnTriggerWithEnemyEnter;
    public event Action<Collider2D> OnTriggerWithEnemyExit;

    private bool _collideExited = true;
    private bool _triggerExited = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!interactableObjectSO.InteractableObjects.Contains(collision.transform.tag)
            && !interactableObjectSO.InteractableObjects.Contains(GameObjectTagsConstants.ALL)) return;
        if (!gameObject.activeSelf) return;
        if (!_collideExited) return;

        _collideExited = false;
        OnCollisionWithEnemyEnter?.Invoke(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!interactableObjectSO.InteractableObjects.Contains(collision.transform.tag)
            && !interactableObjectSO.InteractableObjects.Contains(GameObjectTagsConstants.ALL)) return;

        _collideExited = true;
        OnCollisionWithEnemyExit?.Invoke(collision);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!interactableObjectSO.InteractableObjects.Contains(collider.tag)
            && !interactableObjectSO.InteractableObjects.Contains(GameObjectTagsConstants.ALL)) return;
        if (!gameObject.activeSelf) return;
        if (!_triggerExited) return;

        _triggerExited = false;

        OnTriggerWithEnemyEnter?.Invoke(collider);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!interactableObjectSO.InteractableObjects.Contains(collider.tag)
            && !interactableObjectSO.InteractableObjects.Contains(GameObjectTagsConstants.ALL)) return;

        _triggerExited = true;
        OnTriggerWithEnemyExit?.Invoke(collider);
    }
}
