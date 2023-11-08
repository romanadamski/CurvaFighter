using UnityEngine;

[CreateAssetMenu(fileName = "InteractableObject", menuName = "ScriptableObjects/Interactable Object")]
public class InteractableObjectSO : ScriptableObject
{
    [SerializeField]
    private string[] interactableObjects;
    public string[] InteractableObjects => interactableObjects;
}
