using UnityEngine;

public class SomeInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log($"Interacted with {name}");
    }
}
