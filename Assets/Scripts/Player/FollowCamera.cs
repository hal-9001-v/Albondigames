using UnityEngine;

public class FollowCamera : EffectCamera
{
    private GameObject target;
    lvlGen mylvlGen;
    Vector3 destination;

    [Range(0.5f, 10)]
    public float speedFactor;

    RoomHandler currentRoom;

    // Use this for initialization
    void Start()
    {
        mylvlGen = FindObjectOfType<lvlGen>();

        target = GameObject.FindGameObjectWithTag("Player");

        currentRoom = mylvlGen.getRoomAtPosition(target.transform, currentRoom);

        Vector3 newPosition = currentRoom.transform.position;
        newPosition.y -= 1;
        newPosition.z = transform.position.z;

        destination = newPosition;
        transform.position = newPosition;
    }

    private void LateUpdate()
    {
        updateRoom();

        if (Vector3.Distance(destination, transform.position) > 0.01)
        {
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * speedFactor);
        }
        else
        {
            transform.position = destination;
        }

    }

    void updateRoom()
    {
        RoomHandler auxiliarRoom = mylvlGen.getRoomAtPosition(target.transform, currentRoom);
        //Get exact position of room

        //No room exists check
        if (auxiliarRoom != null)
        {
            currentRoom = auxiliarRoom;

            Vector3 finalPosition = currentRoom.transform.position;
            finalPosition.z = transform.position.z;

            finalPosition.y -= 1;

            destination = finalPosition;
        }

    }

}
