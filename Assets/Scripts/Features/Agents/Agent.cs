using System;
using UnityEngine;

public abstract class Agent<TEnum, TState> : Entity, IInteractor where TEnum : Enum where TState : BaseState<TEnum>
{
    #region INTERACTOR PROPERTIES
    public InteractionMethods InteractionMethod { get; set; }   

    // Trigger 
    public Collider TriggerCollider { get; set; }
    public Collider2D TriggerCollider2D { get; set; }

    // Raycasting
    public float RayRange { get; set; }
    public bool OverrideRayDirection { get; set; }
    public Vector3 RayDirection { get; set; }

    // Extra
    public LayerMask InteractableLayers { get; set; }
    
    public IInteractable CurrentInteractable { get; set; }
    #endregion

    #region INTERACTOR METHODS
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (InteractionMethod == InteractionMethods.RAYCASTING) CheckRay();

        // Replace with new InputSystem.. but might fail without subscription?
        if (Input.GetKeyDown(KeyCode.E))
        {
            FireInteraction();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (InteractionMethod == InteractionMethods.TRIGGER_3D)
        {
            if (other.TryGetComponent(out IInteractable interactable) && ValidateLayer(other.gameObject.layer))
            {
                CheckTrigger(interactable);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (InteractionMethod == InteractionMethods.TRIGGER_3D)
        {
            if (other.TryGetComponent(out IInteractable interactable) && ValidateLayer(other.gameObject.layer))
            {
                CurrentInteractable = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (InteractionMethod == InteractionMethods.TRIGGER_2D)
        {
            if (collision.TryGetComponent(out IInteractable interactable) && ValidateLayer(collision.gameObject.layer))
            {
                CheckTrigger(interactable);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (InteractionMethod == InteractionMethods.TRIGGER_2D)
        {
            if (collision.TryGetComponent(out IInteractable interactable) && ValidateLayer(collision.gameObject.layer))
            {
                CurrentInteractable = null;
            }
        }
    }

    public void CheckTrigger(IInteractable interactable)
    {
        CurrentInteractable = interactable;
    }

    public void CheckRay()
    {
        RaycastHit hit;

        Vector3 direction = OverrideRayDirection ? RayDirection : transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, direction, out hit, RayRange, InteractableLayers))
        {
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out IInteractable interactable))
                {
                    CurrentInteractable = interactable;
                }
                else
                {
                    CurrentInteractable = null;
                }
            }
            else
            {
                CurrentInteractable = null;
            }
        }
        else
        {
            Debug.Log("Nothing hit");
            CurrentInteractable = null;
        }
    }

    public void FireInteraction()
    {
        if (CurrentInteractable is null) return;

        CurrentInteractable.Interact(new InteractionArgs(gameObject));
    }

    private bool ValidateLayer(LayerMask layer)
    {
        return (InteractableLayers.value & (1 << layer.value)) != 0;
    }
    #endregion

    public abstract StateMachine<TEnum, TState> StateMachine { get; set; }

    protected override void Start()
    {
        base.Start();

        InitStateMachine();
    }

    protected abstract void InitStateMachine();
}

public interface IControllerData<TData> where TData : AgentControllerData
{
    public TData Data { get; set; }
}
