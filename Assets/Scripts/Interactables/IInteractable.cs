using UnityEngine;

public interface IInteractable
{
    public void Interact();
}

public interface IInteractor
{
    public InteractionMethods InteractionMethod { get; set; }

    // Add ShowIf decorator from Odin
    public Collider2D TriggerCollider {  get; set; }
    
    // Add ShowIf decorator from Odin
    public float RayRange { get; set; }
    public LayerMask LayerMask { get; set; }

    public IInteractable CurrentInteractable { get; set; }  

    public void DetectTrigger(IInteractable interactable);
    public void DetectRay();

    public void FireInteraction();
}

public enum InteractionMethods
{
    TRIGGER,
    RAYCASTING
}
