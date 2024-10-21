using System;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    [SerializeField]
    DungeonRoomDoor leftDoor,
        rightDoor,
        topDoor,
        bottomDoor;

    public void InitializeDoorTriggers(
        Action<Collider, DoorLocation> _onEnterDoor,
        DoorLocation lastEnteredDoor
    )
    {
        DisableDoor(lastEnteredDoor);
        leftDoor.SetOnEnterDoor(_onEnterDoor);
        rightDoor.SetOnEnterDoor(_onEnterDoor);
        topDoor.SetOnEnterDoor(_onEnterDoor);
        bottomDoor.SetOnEnterDoor(_onEnterDoor);
    }

    private void DisableDoor(DoorLocation lastEnteredDoor)
    {
        switch (lastEnteredDoor)
        {
            case DoorLocation.Left:
                rightDoor.gameObject.SetActive(false);
                break;
            case DoorLocation.Right:
                leftDoor.gameObject.SetActive(false);
                break;
            case DoorLocation.Top:
                bottomDoor.gameObject.SetActive(false);
                break;
            case DoorLocation.Bottom:
                topDoor.gameObject.SetActive(false);
                break;
        }
    }
}
