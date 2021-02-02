﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClientConstructDungeon
{
    #region Singleton
    private static ClientConstructDungeon instance = null;
    public static ClientConstructDungeon GetInstance()
    {
        if (instance == null)
        {
            return new ClientConstructDungeon();
        }
        return instance;
    }
    
    private ClientConstructDungeon()
    {
        instance = this;
    }
    #endregion
    
    GameObject dungeon;
    // IRoom currentConstructionRoom;
    string category;
    List<IRoom> allrooms;
    List<GameObject> instantiated_tiles;
    bool initialized = false;
    public bool isInitialized()
    {
        return initialized;
    }
    public void InitializeDungeon()
    {
        dungeon = new GameObject("Dungeon");
        allrooms = new List<IRoom>();
        initialized = true;
    }
    public void ReadNInitializeRoom(string roomName,Vector3 roomLocation,int tilesX,int tilesZ,string category, string type)
    {   
       // currentConstructionRoom = RoomFactory.Build()
        //currentConstructionRoom.RoomObject = new GameObject(roomName);
        GameObject gr = new GameObject(roomName);
        this.category = category;
        //currentConstructionRoom.Type = type;
        // currentConstructionRoom.Category = category;
        // currentConstructionRoom.RoomObject.transform.position = new Vector3(roomLocation.x + (Mathf.Abs((roomLocation.x + tilesX * Tile.X_length) - roomLocation.x) / 2), roomLocation.y + 2.5f, roomLocation.z - (Mathf.Abs((roomLocation.z + tilesZ * Tile.Z_length) - roomLocation.z) / 2));
        gr.transform.position = new Vector3(roomLocation.x + (Mathf.Abs((roomLocation.x + tilesX * Tile.X_length) - roomLocation.x) / 2), roomLocation.y + 2.5f, roomLocation.z - (Mathf.Abs((roomLocation.z + tilesZ * Tile.Z_length) - roomLocation.z) / 2));
        instantiated_tiles = new List<GameObject>();
        //currentConstructionRoom.RoomObject.transform.parent = dungeon.transform;
        //currentConstructionRoom.Position = roomLocation;
        gr.transform.parent = dungeon.transform;
    }
    public void ReadNConstructTile(string tileName, Vector3 tileLocation,Quaternion tileRotation)
    {
        Tile tile;
        if (category.Equals("Room"))//currentConstructionRoom.Category.Equals("Room"))
        {
             tile = new Tile(tileName, PrefabManager.GetInstance().GetAllRoomTiles().Where(obj => obj.name == tileName).First(), tileLocation);
        }
        else
        {
             tile = new Tile(tileName, PrefabManager.GetInstance().GetAllCorridorTiles().Where(obj => obj.name == tileName).First(), tileLocation);
        }

        instantiated_tiles.Add(Object.Instantiate(tile.Objtile, tileLocation, tileRotation));// currentConstructionRoom.RoomObject.transform));
    }
    public void FinalizeRoom()
    {
      /*  if (currentConstructionRoom != null)
        {
            currentConstructionRoom.Instantiated_Tiles = instantiated_tiles;
            allrooms.Add(currentConstructionRoom);
        }
    */
    }
}
