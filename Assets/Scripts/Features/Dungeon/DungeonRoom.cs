using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    [SerializeField]
    DungeonRoomDoor leftDoor,
        rightDoor,
        topDoor,
        bottomDoor;

    [SerializeField]
    RoomTypes type;
    public RoomTypes Type => type;

    public void InitializeDoorTriggers(
        Action<Collider, DoorLocation, DungeonRoom> _onEnterDoor,
        DoorLocation lastEnteredDoor,
        List<DungeonRoom> roomsToSpawn
    )
    {
        var activeDoors = DisableDoor(
            lastEnteredDoor,
            new() { leftDoor, rightDoor, topDoor, bottomDoor }
        );

        int index = 0;

        activeDoors.ForEach(door =>
        {
            door.SetOnEnterDoor(_onEnterDoor, roomsToSpawn[index]);
            index++;
        });
    }

    private List<DungeonRoomDoor> DisableDoor(
        DoorLocation lastEnteredDoor,
        List<DungeonRoomDoor> doors
    )
    {
        switch (lastEnteredDoor)
        {
            case DoorLocation.Left:
                doors.Remove(rightDoor);
                rightDoor.gameObject.SetActive(false);
                break;
            case DoorLocation.Right:
                doors.Remove(leftDoor);
                leftDoor.gameObject.SetActive(false);
                break;
            case DoorLocation.Top:
                doors.Remove(bottomDoor);
                bottomDoor.gameObject.SetActive(false);
                break;
            case DoorLocation.Bottom:
                doors.Remove(topDoor);
                topDoor.gameObject.SetActive(false);
                break;
        }

        return doors;
    }
}
