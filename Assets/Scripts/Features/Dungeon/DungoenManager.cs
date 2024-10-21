using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungoenManager : MonoBehaviour
{
    [SerializeField]
    List<DungeonRoom> dungeonRooms;

    readonly List<DungeonRoom> spawnedRooms = new();

    private void Start()
    {
        GenerateDungeon(0);
    }

    void GenerateDungeon(int roomAmount)
    {
        GenerateRoom(DoorLocation.Right);
    }

    void GenerateRoom(DoorLocation enteredDoor)
    {
        var room = GetRoomToSpawn();
        var spawnedRoom = Instantiate(room);
        var lastRoom = GetLastSpawnedRoom();

        if (lastRoom != null)
        {
            PositionRoom(spawnedRoom, lastRoom.transform, enteredDoor);
        }

        spawnedRooms.Add(spawnedRoom);
        spawnedRoom.InitializeDoorTriggers(OnEnterDoor, enteredDoor);
    }

    void OnEnterDoor(Collider collider, DoorLocation location)
    {
        GenerateRoom(location);
    }

    DungeonRoom GetRoomToSpawn()
    {
        return dungeonRooms[0];
    }

    DungeonRoom GetLastSpawnedRoom()
    {
        if (spawnedRooms.Count == 0)
            return null;

        return spawnedRooms.Last();
    }

    void PositionRoom(DungeonRoom spawnedRoom, Transform lastTransform, DoorLocation enteredDoor)
    {
        Vector3 lastRoomSize = lastTransform.GetComponent<Renderer>().bounds.size;
        Vector3 newPosition =
            lastTransform.position + GetModificactionsByEnteredDoor(lastRoomSize, enteredDoor);
        spawnedRoom.transform.position = newPosition;
    }

    Vector3 GetModificactionsByEnteredDoor(Vector3 lastRoomsize, DoorLocation enteredDoor)
    {
        switch (enteredDoor)
        {
            case DoorLocation.Left:
                return new(lastRoomsize.x * -1, 0, 0);
            case DoorLocation.Right:
                return new(lastRoomsize.x, 0, 0);
            case DoorLocation.Top:
                return new(0, 0, lastRoomsize.z);
            case DoorLocation.Bottom:
                return new(0, 0, lastRoomsize.z * -1);
            default:
                return new(0, 0, 0);
        }
    }
}
