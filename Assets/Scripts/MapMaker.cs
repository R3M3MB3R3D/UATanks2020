using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapMaker : MonoBehaviour
{
    //Create variables for building rooms
    public int rows;
    public int cols;
    public int mapSeed;

    //Set the size of each room.
    private float roomWidth = 50.0f;
    private float roomHeight = 50.0f;

    //Create an array of rooms to use.
    public GameObject[] gridPrefabs;
    //Create an array of labelled rooms
    private Room[,] grid;

    //Create a list of ways to make maps.
    public enum MapType{Seed, Random, Day}
    //Create a default map type and variable.
    public MapType mapType = MapType.Random;

    private void Start()
    {
        //Tell the game manager that 'this' is the room.
        GameManager.instance.levelGameObject = this.gameObject;
        //Switch case to help decide which map type to use.
        switch (mapType)
        {
            case MapType.Day:
                mapSeed = DateToInt(DateTime.Now.Date);
                break;
            case MapType.Seed:
                UnityEngine.Random.seed = mapSeed;
                break;
            case MapType.Random:
                mapSeed = DateToInt(DateTime.Now);
                break;
            default:
                Debug.LogError("[MapMaker] Map type not implemented.");
                break;
        }
        //Begin making the map.
        MakeMap();
        //Begin spawning tanks.
        GameManager.instance.SpawnPlayer(GameManager.instance.RandomSpawnPoint(GameManager.instance.playerSpawnPoints));
        GameManager.instance.SpawnEnemy(GameManager.instance.RandomSpawnPoint(GameManager.instance.enemySpawnPoints));
    }

    //Create a function for grabbing prefabricated rooms.
    public GameObject RandomRoom()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    //Create a function for determining a map seed.
    public int DateToInt(DateTime dateToUse)
    {
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second;
    }

    //Create a function for making the map.
    public void MakeMap()
    {
        //Create a new room.
        grid = new Room[cols, rows];
        //For the number of rows, create that many rows of rooms.
        for (int row = 0; row < rows; row++)
        {
            //For the number columns, create that many columns of rooms.
            for (int col = 0; col < cols; col++)
            {
                //Place the rooms properly together.
                float xPosition = roomWidth * col;
                float zPosition = roomHeight * row;
                Vector3 newPosition = new Vector3(xPosition, y:0.0f, zPosition);

                //Create the room, place it, and label it.
                GameObject tempRoomObj = Instantiate(original: RandomRoom(), newPosition, Quaternion.identity) as GameObject;
                tempRoomObj.transform.parent = this.transform;
                tempRoomObj.name = "Room_" + col + "," + row;

                //Turn all separate rooms into a single coherent object, separated by their labels.
                Room tempRoom = tempRoomObj.GetComponent<Room>();
                grid[col, row] = tempRoom;

                //Keep the top-most and bottom-most walls up only
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

                //Keep the left-most and right-most walls up only.
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