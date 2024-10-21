using UnityEngine;

public class InteractableProp : Prop, IInteractable
{
    public void Interact(InteractionArgs args)
    {
        Debug.Log($"{args.interactor.name} interacted with {EntityName}");
    }
}
