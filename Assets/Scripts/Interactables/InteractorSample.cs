using UnityEngine;

public class InteractorSample : MonoBehaviour, IInteractor
{
    public InteractionMethods InteractionMethod { get; set; }
    public IInteractable CurrentInteractable { get; set; }

    // 2D Trigger
    public Collider2D TriggerCollider2D { get; set; }

    // 3D Trigger
    public Collider TriggerCollider {  get; set; }  

    // Raycasting
    [field: SerializeField] public float RayRange { get; set; }

    [field: SerializeField] public bool OverrideRayDirection { get; set; } = false;
    [field: SerializeField] public Vector3 RayDirection { get; set; } // Use ShowIf from Odin

    // Sorting
    public LayerMask InteractableLayers { get; set; }

    private void Start()
    {
        TriggerCollider2D = GetComponent<Collider2D>();
    }

    public void DetectTrigger(IInteractable interactable)
    {
        CurrentInteractable = interactable;
    }

    public void DetectRay()
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

    // Update is called once per frame
    void Update()
    {
        if (InteractionMethod == InteractionMethods.RAYCASTING) DetectRay();

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
                DetectTrigger(interactable);
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
                DetectTrigger(interactable);
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
    
    /// <summary>
    /// Use this method to invoke the interaction on the currently observed interactable
    /// </summary>
    public void FireInteraction()
    {
        if (CurrentInteractable is null) return;

        CurrentInteractable.Interact(new InteractionArgs(gameObject));
    }

    private bool ValidateLayer(LayerMask layer)
    {
        return (InteractableLayers.value & (1 << layer.value)) != 0;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            if (InteractionMethod == InteractionMethods.RAYCASTING)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward) * RayRange);
            }
        }
    }
}
