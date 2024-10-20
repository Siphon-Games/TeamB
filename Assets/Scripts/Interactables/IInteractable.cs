using UnityEngine;

public class InteractionArgs
{
    public GameObject interactor;

    public InteractionArgs(GameObject interactor)
    {
        this.interactor = interactor;
    }
}

public interface IInteractable
{
    public void Interact(InteractionArgs args);
}

public interface IInteractor
{
    public InteractionMethods InteractionMethod { get; set; }

    // Add ShowIf decorator from Odin
    public Collider2D TriggerCollider2D {  get; set; }

    // Add ShowIf decorator from Odin
    public Collider TriggerCollider { get; set; }
    
    // Add ShowIf decorator from Odin
    public float RayRange { get; set; }
    public Vector3 RayDirection { get; set; }   
    public LayerMask InteractableLayers { get; set; }
    public bool OverrideRayDirection { get; set; }

    public IInteractable CurrentInteractable { get; set; }

    public void CheckTrigger(IInteractable interactable);
    public void CheckRay();

    public void FireInteraction();
}

public enum InteractionMethods
{
    TRIGGER_2D,
    TRIGGER_3D,
    RAYCASTING
}
