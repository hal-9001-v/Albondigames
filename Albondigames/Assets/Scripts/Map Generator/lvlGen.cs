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
    public GameObject hab1;
    public GameObject hab2;
    public GameObject hab3;
    public GameObject hab4;
    
    //Dungeon Params
    int dungSize = 12;
    int dungWidth = 14;
    int dungLength = 8;
    SquaredDungeon d1 = new SquaredDungeon(16);


    //Dungeon info
    int[,] dungMatrix;
    int op = 2;
    List<GameObject> oneDoorPrefabList = new List<GameObject>();
    List<GameObject> twoDoorPrefabList = new List<GameObject>();
    List<GameObject> threeDoorPrefabList = new List<GameObject>();
    List<GameObject> fourDoorPrefabList = new List<GameObject>();
    List<int[]> bossList = new List<int[]>();
    

    void Start()
    {
        //We generate a dungeon and paint tiles given an empty tilemap and the selected tiles
       d1.Generate(dungSize, GamePX.Labyrinth.Method.Prim);
       // d1.Generate(dungSize);
        d1.PaintTiles(t1, tb, 8, 14);
        dungMatrix = d1.GetTileDescription();

        //Here we have to add all of the prefabs for rooms

        //One Door
        oneDoorPrefabList.Add(hab1);
        oneDoorPrefabList.Add(hab1);
        oneDoorPrefabList.Add(hab1);
        oneDoorPrefabList.Add(hab1);
    
        //Two Doors
        twoDoorPrefabList.Add(hab2);
        twoDoorPrefabList.Add(hab2);
        twoDoorPrefabList.Add(hab2);
        twoDoorPrefabList.Add(hab2);

        //Three Doors
        threeDoorPrefabList.Add(hab3);
        threeDoorPrefabList.Add(hab3);
        threeDoorPrefabList.Add(hab3);
        threeDoorPrefabList.Add(hab3);

        //Four Doors
        fourDoorPrefabList.Add(hab4);
        fourDoorPrefabList.Add(hab4);
        fourDoorPrefabList.Add(hab4);
        fourDoorPrefabList.Add(hab4);

        //Boss Room
        bossList = d1.GetLeaves();
        int randomOption = UnityEngine.Random.Range(1, bossList.Count - 1);
        ListUtil<int[]> listita = new ListUtil<int[]>();
        int[]bossRoom = listita.GetElement(bossList);

      

        //We invoke prefab handler

        for (int i = 0; i < dungSize; i++)
        {


            for (int j = 0; j < dungSize; j++)
            {
              
                    prefabHandler(bossRoom ,oneDoorPrefabList, twoDoorPrefabList, threeDoorPrefabList, fourDoorPrefabList, i, j);

            }

        }


    }

    // Update is called once per frame
    void Update()
    {



    }



    void prefabHandler(int[] p, List<GameObject> odpl, List<GameObject> tdpl, List<GameObject> thdpl, List<GameObject> fdpl, int i, int j)
    {

        GameObject o;
        int randomOption;

        //We check for number of doors, then we instantiate the prefab
        if (dungMatrix[i, j] == 1)
        {
            if (i == p[0] && j == p[1]) {

                //If boss room, we instantiate boss room
                o = hab4; 
            }
            else
            {
                randomOption = UnityEngine.Random.Range(1, odpl.Count - 1);
                o = odpl[randomOption];
            }

           (Instantiate(o, new Vector2(dungWidth * j + 7, dungLength * i + 5), Quaternion.identity) as GameObject).transform.parent = t1.transform;

        }
        else if (dungMatrix[i, j] == 2)
        {
            randomOption = UnityEngine.Random.Range(1, tdpl.Count - 1);
            o = tdpl[randomOption];
            (Instantiate(o, new Vector2(dungWidth * j + 7, dungLength * i + 5), Quaternion.identity) as GameObject).transform.parent = t1.transform;


        }
        else if (dungMatrix[i, j] == 3)
        {
            randomOption = UnityEngine.Random.Range(1, thdpl.Count - 1);
           o = thdpl[randomOption];
            (Instantiate(o, new Vector2(dungWidth * j + 7, dungLength * i + 5), Quaternion.identity) as GameObject).transform.parent = t1.transform;


        }
        else if (dungMatrix[i, j] == 4)
        {
            randomOption = UnityEngine.Random.Range(1, fdpl.Count - 1);
            o = fdpl[randomOption];
            (Instantiate(o, new Vector2(dungWidth * j + 7, dungLength * i + 5), Quaternion.identity) as GameObject).transform.parent = t1.transform;


        }


    }

  


}
