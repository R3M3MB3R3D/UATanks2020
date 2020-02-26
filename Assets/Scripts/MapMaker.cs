using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapMaker : MonoBehaviour
{
    public int rows;
    public int cols;
    public int mapSeed;

    private float roomWidth = 50.0f;
    private float roomHeight = 50.0f;

    public GameObject[] gridPrefabs;
    private Room[,] grid;

    public enum MapType{Seed, Random, Day}
    public MapType mapType = MapType.Random;

    private void Start()
    {
        switch (mapType)
        {
            case MapType.Day:
                mapSeed = DateToInt(DateTime.Now.Date);
                break;
            case MapType.Seed:
                break;
            case MapType.Random:
                mapSeed = DateToInt(DateTime.Now);
                break;
            default:
                Debug.LogError("[MapMaker] Map type not implemented.");
                break;
        }
        MakeMap();
    }

    public GameObject RandomRoom()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    public int DateToInt(DateTime dateToUse)
    {
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second;
    }

    public void MakeMap()
    {
        UnityEngine.Random.seed = mapSeed;
        grid = new Room[cols, rows];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                float xPosition = roomWidth * col;
                float zPosition = roomHeight * row;
                Vector3 newPosition = new Vector3(xPosition, y:0.0f, zPosition);

                GameObject tempRoomObj = Instantiate(original: RandomRoom(), newPosition, Quaternion.identity) as GameObject;
                tempRoomObj.transform.parent = this.transform;
                tempRoomObj.name = "Room_" + col + "," + row;

                Room tempRoom = tempRoomObj.GetComponent<Room>();
                grid[col, row] = tempRoom;

                if (col == 0)
                {
                    tempRoom.doorSouth.SetActive(false);
                }
                else if (col == cols - 1)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else
                {
                    tempRoom.doorSouth.SetActive(false);
                    tempRoom.doorNorth.SetActive(false);
                }

                if (row == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                else if (row == rows - 1)
                {
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }
            }

        }
    }
}
