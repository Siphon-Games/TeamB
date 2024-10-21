using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public string EntityName;

    protected virtual void Awake() { }
    protected virtual void Start() { }
    protected virtual void Update() { }
    protected virtual void FixedUpdate() { }
}
