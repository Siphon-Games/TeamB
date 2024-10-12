using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class NpcMovement2D : MonoBehaviour, ICharacterMovement2D
{
    [SerializeReference]
    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    Transform cameraTransform;

    [SerializeReference]
    [SerializeField]
    float movementRange = 5f;

    Vector3 targetPosition;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        InitializeNpc();
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        float step = moveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = GetNewTargetPosition();
        }

        spriteRenderer.transform.rotation = Quaternion.LookRotation(
            cameraTransform.forward,
            cameraTransform.up
        );
    }

    public virtual Vector3 GetNewTargetPosition()
    {
        float randomX = Random.Range(-movementRange, movementRange);
        float randomZ = Random.Range(-movementRange, movementRange);

        return new Vector3(
            transform.position.x + randomX,
            transform.position.y,
            transform.position.z + randomZ
        );
    }

    public virtual void InitializeNpc()
    {
        targetPosition = transform.position;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}
