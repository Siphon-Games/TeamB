using System;
using UnityEngine;

public class DungeonRoomDoor : MonoBehaviour
{
    [SerializeField]
    DoorLocation location;

    Action<Collider, DoorLocation> onEnterDoor;

    bool isDoorEntered = false;

    public void SetOnEnterDoor(Action<Collider, DoorLocation> _onEnterDoor)
    {
        onEnterDoor = _onEnterDoor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDoorEntered)
        {
            onEnterDoor(other, location);
            Debug.Log($"Woaaahh something entered the {name} !!");
            isDoorEntered = true;
        }
    }
}
