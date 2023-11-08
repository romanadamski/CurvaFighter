using System;
using UnityEngine;

[RequireComponent(typeof(InteractableController))]
public class ResourceController : MonoBehaviour
{
    [SerializeField]
    private ResourceObjectSO resourceObject;

    private InteractableController interactable;

    private void Awake()
    {
        interactable = GetComponent<InteractableController>();
        SubscribeToInteractableEvents();
    }

    private void SubscribeToInteractableEvents()
    {
    }
}
