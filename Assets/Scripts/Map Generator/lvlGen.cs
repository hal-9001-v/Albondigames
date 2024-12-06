using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePX;
using UnityEngine.Tilemaps;


public class lvlGen : MonoBehaviour
{
    // Start is called before the first frame update
    //Game Components

    public Tilemap t1 = new Tilemap();
    public TileBase[] tb = new TileBase[13];

    //Initiate rooms
    //B
    public GameObject B1;
    public GameObject B2;
    public GameObject BBoss;
    List<GameObject> oneDoorBPrefabList = new List<GameObject>();


    //BL
    public GameObject BL1;
    public GameObject BL2;
    List<GameObject> twoDoorBLPrefabList = new List<GameObject>();

    //BR
    public GameObject BR1;
    public GameObject BR2;
    List<GameObject> twoDoorBRPrefabList = new List<GameObject>();

    //BRL
    public GameObject BRL1;
    public GameObject BRL2;
    List<GameObject> threeDoorBRLPrefabList = new List<GameObject>();

    //L
    public GameObject L1;
    public GameObject L2;
    public GameObject LBoss;
    List<GameObject> oneDoorLPrefabList = new List<GameObject>();

    //LR
    public GameObject LR1;
    public GameObject LR2;
    List<GameObject> twoDoorLRPrefabList = new List<GameObject>();

    //R
    public GameObject R1;
    public GameObject R2;
    public GameObject RBoss;
    List<GameObject> oneDoorRPrefabList = new List<GameObject>();

    //T
    public GameObject T1;
    public GameObject T2;
    public GameObject TBoss;
    List<GameObject> oneDoorTPrefabList = new List<GameObject>();

    //TB
    public GameObject TB1;
    public GameObject TB2;
    List<GameObject> twoDoorTBPrefabList = new List<GameObject>();

    //TL
    public GameObject TL1;
    public GameObject TL2;
    List<GameObject> twoDoorTLPrefabList = new List<GameObject>();

    //TR
    public GameObject TR1;
    public GameObject TR2;
    List<GameObject> twoDoorTRPrefabList = new List<GameObject>();

    //TRL
    public GameObject TRL1;
    public GameObject TRL2;
    List<GameObject> threeDoorTRLPrefabList = new List<GameObject>();

    //TRB
    public GameObject TRB1;
    public GameObject TRB2;
    List<GameObject> threeDoorTRBPrefabList = new List<GameObject>();

    //TLB
    public GameObject TLB1;
    public GameObject TLB2;
    List<GameObject> threeDoorTLBPrefabList = new List<GameObject>();

    //TRLB
    public GameObject TRLB1;
    public GameObject TRLB2;
    public GameObject TRLB3;
    public GameObject TRLB4;
    public GameObject TRLB5;
    List<GameObject> fourDoorTRLBPrefabList = new List<GameObject>();

    //List of positions
    LinkedList<Vector2> positionList;

    //List of rooms
    LinkedList<RoomHandler> roomList = new LinkedList<RoomHandler>();

    //Dungeon Params
    int dungSize = 12;
    int dungWidth = 15;
    int dungLength = 9;
    SquaredDungeon d1 = new SquaredDungeon(16);


    //Dungeon info
    int[,] dungMatrix;
    //int op = 2;
    List<int[]> bossList = new List<int[]>();



    void Start()
    {
        //We generate a dungeon and paint tiles given an empty tilemap and the selected tiles
        d1.Generate(dungSize, GamePX.Labyrinth.Method.Prim);
        // d1.Generate(dungSize);
        d1.PaintTiles(t1, tb, dungLength, dungWidth);
        dungMatrix = d1.GetTileDescription();

        //Here we have to add all of the prefabs for rooms

        //One Door
        oneDoorBPrefabList.Add(B1);
        oneDoorBPrefabList.Add(B1);
        oneDoorBPrefabList.Add(B2);
        oneDoorBPrefabList.Add(B2);

        oneDoorLPrefabList.Add(L1);
        oneDoorLPrefabList.Add(L1);
        oneDoorLPrefabList.Add(L2);
        oneDoorLPrefabList.Add(L2);

        oneDoorRPrefabList.Add(R1);
        oneDoorRPrefabList.Add(R1);
        oneDoorRPrefabList.Add(R2);
        oneDoorRPrefabList.Add(R2);

        oneDoorTPrefabList.Add(T1);
        oneDoorTPrefabList.Add(T2);
        oneDoorTPrefabList.Add(T2);
        oneDoorTPrefabList.Add(T2);

        //Two Doors
        twoDoorBLPrefabList.Add(BL1);
        twoDoorBLPrefabList.Add(BL1);
        twoDoorBLPrefabList.Add(BL1);
        twoDoorBLPrefabList.Add(BL2);
        twoDoorBLPrefabList.Add(BL2);
        twoDoorBLPrefabList.Add(BL2);

        twoDoorBRPrefabList.Add(BR1);
        twoDoorBRPrefabList.Add(BR1);
        twoDoorBRPrefabList.Add(BR1);
        twoDoorBRPrefabList.Add(BR2);

        twoDoorBRPrefabList.Add(BR2);
        twoDoorBRPrefabList.Add(BR2);

        twoDoorLRPrefabList.Add(LR1);
        twoDoorLRPrefabList.Add(LR1);
        twoDoorLRPrefabList.Add(LR1);
        twoDoorLRPrefabList.Add(LR2);
        twoDoorLRPrefabList.Add(LR2);
        twoDoorLRPrefabList.Add(LR2);

        twoDoorTBPrefabList.Add(TB1);
        twoDoorTBPrefabList.Add(TB1);
        twoDoorTBPrefabList.Add(TB1);
        twoDoorTBPrefabList.Add(TB2);
        twoDoorTBPrefabList.Add(TB2);
        twoDoorTBPrefabList.Add(TB2);

        twoDoorTLPrefabList.Add(TL1);
        twoDoorTLPrefabList.Add(TL1);
        twoDoorTLPrefabList.Add(TL1);
        twoDoorTLPrefabList.Add(TL2);
        twoDoorTLPrefabList.Add(TL2);
        twoDoorTLPrefabList.Add(TL2);

        twoDoorTRPrefabList.Add(TR1);
        twoDoorTRPrefabList.Add(TR1);
        twoDoorTRPrefabList.Add(TR1);
        twoDoorTRPrefabList.Add(TR2);
        twoDoorTRPrefabList.Add(TR2);
        twoDoorTRPrefabList.Add(TR2);

        //Three Doors
        threeDoorBRLPrefabList.Add(BRL1);
        threeDoorBRLPrefabList.Add(BRL1);
        threeDoorBRLPrefabList.Add(BRL2);
        threeDoorBRLPrefabList.Add(BRL2);

        threeDoorTRLPrefabList.Add(TRL1);
        threeDoorTRLPrefabList.Add(TRL1);
        threeDoorTRLPrefabList.Add(TRL2);
        threeDoorTRLPrefabList.Add(TRL2);

        threeDoorTRBPrefabList.Add(TRB1);
        threeDoorTRBPrefabList.Add(TRB1);
        threeDoorTRBPrefabList.Add(TRB2);
        threeDoorTRBPrefabList.Add(TRB2);

        threeDoorTLBPrefabList.Add(TLB1);
        threeDoorTLBPrefabList.Add(TLB1);
        threeDoorTLBPrefabList.Add(TLB2);
        threeDoorTLBPrefabList.Add(TLB2);




        //Four Doors
        fourDoorTRLBPrefabList.Add(TRLB1);
        fourDoorTRLBPrefabList.Add(TRLB1);
        fourDoorTRLBPrefabList.Add(TRLB2);
        fourDoorTRLBPrefabList.Add(TRLB2);
        fourDoorTRLBPrefabList.Add(TRLB3);
        fourDoorTRLBPrefabList.Add(TRLB3);
        fourDoorTRLBPrefabList.Add(TRLB4);
        fourDoorTRLBPrefabList.Add(TRLB4);
        fourDoorTRLBPrefabList.Add(TRLB5);
        fourDoorTRLBPrefabList.Add(TRLB5);



        //Boss Room
        bossList = d1.GetLeaves();

        positionList = new LinkedList<Vector2>();

        GamePX.SimpleRandom myRandom = new GamePX.SimpleRandom();

        int randomOption = myRandom.GetInt(1, bossList.Count);
        ListUtil<int[]> listita = new ListUtil<int[]>();
        int[] bossRoom = listita.GetElement(bossList);



        //We invoke prefab handler

        for (int i = 0; i < dungSize; i++)
        {
            for (int j = 0; j < dungSize; j++)
            {

                prefabHandler(bossRoom, oneDoorBPrefabList, oneDoorTPrefabList, oneDoorRPrefabList, oneDoorLPrefabList,
                    twoDoorBLPrefabList, twoDoorBRPrefabList, twoDoorLRPrefabList, twoDoorTBPrefabList, twoDoorTLPrefabList, twoDoorTRPrefabList,
                    threeDoorBRLPrefabList, threeDoorTRLPrefabList, threeDoorTRBPrefabList, threeDoorTLBPrefabList,
                    fourDoorTRLBPrefabList, i, j);

            }

        }
    }

    void prefabHandler(int[] p, List<GameObject> odBpl, List<GameObject> odTpl, List<GameObject> odRpl, List<GameObject> odLpl,
        List<GameObject> tdBLpl, List<GameObject> tdBRpl, List<GameObject> tdLRpl, List<GameObject> tdTBpl, List<GameObject> tdTLpl, List<GameObject> tdTRpl,
        List<GameObject> thdBRLpl, List<GameObject> thdTRLpl, List<GameObject> thdTRBpl, List<GameObject> thdTLBpl,
        List<GameObject> fdTRLBpl, int i, int j)
    {
        GameObject auxiliarGO;

        GameObject o;
        int randomOption;

        GamePX.SimpleRandom myRandom = new GamePX.SimpleRandom();

        //Vector2 roomPosition = new Vector2(dungWidth * j + 7.5F, dungLength * i + 5.5F);
        Vector2 roomPosition = new Vector2(dungWidth * j + 7.5f, dungLength * i + 5.5f);
        positionList.AddFirst(roomPosition);

        //We check for number of doors, then we instantiate the prefab
        if (dungMatrix[i, j] == 1)
        {
            if (i == p[0] && j == p[1])
            {

                //If boss room, we instantiate boss room
                if (d1.HasSouth(i, j))
                {
                    o = BBoss;
                }
                else if (d1.HasNorth(i, j))
                {

                    o = TBoss;

                }
                else if (d1.HasEast(i, j))
                {
                    o = RBoss;
                }
                else
                {
                    o = LBoss;
                }
            }
            else
            {


                if (d1.HasSouth(i, j))
                {
                    //NOTA!!! PUSE odBP1 intuyendo que era eso -Vicente
                    randomOption = myRandom.GetInt(1, odBpl.Count - 1);
                    o = odBpl[randomOption];
                }
                else if (d1.HasNorth(i, j))
                {

                    randomOption = myRandom.GetInt(1, odTpl.Count - 1);
                    o = odTpl[randomOption];

                }
                else if (d1.HasEast(i, j))
                {
                    randomOption = myRandom.GetInt(1, odRpl.Count - 1);
                    o = odRpl[randomOption];
                }
                else
                {
                    randomOption = myRandom.GetInt(1, odLpl.Count - 1);
                    o = odLpl[randomOption];

                }

            }

            auxiliarGO = Instantiate(o, roomPosition, Quaternion.identity) as GameObject;
            auxiliarGO.transform.parent = t1.transform;

            roomList.AddFirst(auxiliarGO.AddComponent<RoomHandler>());

        }
        else if (dungMatrix[i, j] == 2)
        {

            if (d1.HasSouth(i, j) && d1.HasNorth(i, j))
            {
                randomOption = myRandom.GetInt(1, tdTBpl.Count - 1);
                o = tdTBpl[randomOption];
            }
            else if (d1.HasEast(i, j) && d1.HasWest(i, j) && !d1.HasSouth(i, j) && !d1.HasNorth(i, j))
            {

                randomOption = myRandom.GetInt(1, tdLRpl.Count - 1);
                o = tdLRpl[randomOption];

            }
            else if (d1.HasEast(i, j) && d1.HasNorth(i, j) && !d1.HasWest(i, j) && !d1.HasSouth(i, j))
            {
                randomOption = myRandom.GetInt(1, tdTRpl.Count - 1);
                o = tdTRpl[randomOption];
            }
            else if (d1.HasSouth(i, j) && d1.HasEast(i, j) && !d1.HasWest(i, j) && !d1.HasNorth(i, j))
            {
                randomOption = myRandom.GetInt(1, tdBRpl.Count - 1);
                o = tdBRpl[randomOption];
            }
            else if (d1.HasNorth(i, j) && d1.HasWest(i, j) && !d1.HasSouth(i, j) && !d1.HasEast(i, j))
            {

                randomOption = myRandom.GetInt(1, tdTLpl.Count - 1);
                o = tdTLpl[randomOption];

            }

            else
            {
                randomOption = myRandom.GetInt(1, tdBLpl.Count - 1);
                o = tdBLpl[randomOption];
            }


            auxiliarGO = Instantiate(o, roomPosition, Quaternion.identity) as GameObject;
            auxiliarGO.transform.parent = t1.transform;

            roomList.AddFirst(auxiliarGO.AddComponent<RoomHandler>());

            //(Instantiate(o, roomPosition, Quaternion.identity) as GameObject).transform.parent = t1.transform;

        }
        else if (dungMatrix[i, j] == 3)
        {

            if (!d1.HasEast(i, j) && d1.HasSouth(i, j) && d1.HasNorth(i, j) && d1.HasWest(i, j))
            {
                randomOption = myRandom.GetInt(1, thdTLBpl.Count - 1);
                o = thdTLBpl[randomOption];
            }
            else if (!d1.HasSouth(i, j) && d1.HasNorth(i, j) && d1.HasEast(i, j) && d1.HasWest(i, j))
            {
                randomOption = myRandom.GetInt(1, thdTRLpl.Count - 1);
                o = thdTRLpl[randomOption];
            }
            else if (!d1.HasNorth(i, j) && d1.HasEast(i, j) && d1.HasWest(i, j) && d1.HasSouth(i, j))
            {

                randomOption = myRandom.GetInt(1, thdBRLpl.Count - 1);
                o = thdBRLpl[randomOption];

            }

            else
            {
                randomOption = myRandom.GetInt(1, thdTRBpl.Count - 1);
                o = thdTRBpl[randomOption];
            }


            //(Instantiate(o, roomPosition, Quaternion.identity) as GameObject).transform.parent = t1.transform;
            auxiliarGO = Instantiate(o, roomPosition, Quaternion.identity) as GameObject;
            auxiliarGO.transform.parent = t1.transform;


            roomList.AddFirst(auxiliarGO.AddComponent<RoomHandler>());

        }
        else if (dungMatrix[i, j] == 4)
        {
            randomOption = myRandom.GetInt(1, fdTRLBpl.Count - 1);
            o = fdTRLBpl[randomOption];

            //(Instantiate(o, roomPosition, Quaternion.identity) as GameObject).transform.parent = t1.transform;
            auxiliarGO = Instantiate(o, roomPosition, Quaternion.identity) as GameObject;
            auxiliarGO.transform.parent = t1.transform;


            roomList.AddFirst(auxiliarGO.AddComponent<RoomHandler>());
        }

        foreach (RoomHandler room in roomList)
        {
            room.deactiveChildren();
        }
    }


    public RoomHandler getRoomAtPosition(Transform transform, RoomHandler currentRoom)
    {
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
        foreach (RoomHandler room in roomList)
        {
            if (currentRoom == room) continue;

            Vector2 roomPosition = new Vector2(room.transform.position.x, room.transform.position.y);

            float maxX = roomPosition.x + 7.5f;
            //float maxX = roomPosition.x;
            float minX = roomPosition.x - 7.5f;

            float maxY = roomPosition.y + 4.5f;
            //float maxY = roomPosition.y;
            float minY = roomPosition.y - 4.5f;



            //Debug.Log("Comparing player Position: "+playerPosition+" with: "+roomPosition);
            if (playerPosition.x < maxX && playerPosition.x > minX)
            {
                if (playerPosition.y < maxY && playerPosition.y > minY)
                {
                    //Stop Action on old room
                    if (currentRoom != null)
                        currentRoom.deactiveChildren();

                    //New Room
                    currentRoom = room;

                    //Start action on new Room
                    currentRoom.activeChildren();

                    return room;
                }
            }
            //Debug.Log(roomPosition);

        }

        return null;
    }
}

