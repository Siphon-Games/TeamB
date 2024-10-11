using UnityEngine;

public class InteractableSample : MonoBehaviour, IInteractable
{
    public void Interact(InteractionArgs args)
    {
        Debug.Log($"{args.interactor.name} interacted with {name}");
    }
}
