using System;
using TMPro;
using UnityEngine;

public class DungeonRoomDoor : MonoBehaviour
{
    [SerializeField]
    DoorLocation location;

    DungeonRoom roomToSpawn;

    Action<Collider, DoorLocation, DungeonRoom> onEnterDoor;

    bool isDoorEntered = false;

    [SerializeField]
    TextMeshProUGUI spawnRoomTitle;

    public void SetOnEnterDoor(
        Action<Collider, DoorLocation, DungeonRoom> _onEnterDoor,
        DungeonRoom _roomToSpawn
    )
    {
        onEnterDoor = _onEnterDoor;
        roomToSpawn = _roomToSpawn;
        spawnRoomTitle.text = roomToSpawn.Type.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDoorEntered)
        {
            onEnterDoor(other, location, roomToSpawn);
            isDoorEntered = true;
        }
    }
}
