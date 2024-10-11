using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class NpcMovement2D : MonoBehaviour, ICharacterMovement2D
{
    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    float movementRange = 5f;

    Vector3 targetPosition;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        targetPosition = transform.position;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
            float randomX = Random.Range(-movementRange, movementRange);
            float randomZ = Random.Range(-movementRange, movementRange);
            targetPosition = new Vector3(
                transform.position.x + randomX,
                transform.position.y,
                transform.position.z + randomZ
            );
        }

        spriteRenderer.transform.rotation = Quaternion.LookRotation(
            cameraTransform.forward,
            cameraTransform.up
        );
    }
}
