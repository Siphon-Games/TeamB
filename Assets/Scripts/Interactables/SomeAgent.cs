using UnityEngine;

public class SomeAgent : MonoBehaviour, IInteractor
{
    [field: SerializeField] public InteractionMethods InteractionMethod { get; set; }
    [field: SerializeField] public Collider2D TriggerCollider { get; set; }
    [field: SerializeField] public float RayRange { get; set; }
    [field: SerializeField] public LayerMask LayerMask { get; set; }
    public IInteractable CurrentInteractable { get; set; }

    public void DetectTrigger(IInteractable interactable)
    {
        CurrentInteractable = interactable;
    }

    public void DetectRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RayRange, LayerMask))
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (InteractionMethod == InteractionMethods.TRIGGER)
        {
            if (collision.TryGetComponent(out IInteractable interactable))
            {
                DetectTrigger(interactable);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (InteractionMethod == InteractionMethods.TRIGGER)
        {
            if (collision.TryGetComponent(out IInteractable interactable))
            {
                CurrentInteractable = null;
            }
        }
    }

    public void FireInteraction()
    {
        if (CurrentInteractable is null) return;

        CurrentInteractable.Interact();
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            if (InteractionMethod == InteractionMethods.RAYCASTING)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward) * RayRange);
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(new Ray(transform.position, transform.TransformDirection(Vector3.forward * RayRange)));
            }
        }
    }
}
