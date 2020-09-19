﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Horizontal_Straight_Corridor: Basic_Corridor
{
    public Horizontal_Straight_Corridor(List<GameObject> tiles, string type, int tiles_x, int tiles_z) : base(tiles, type, tiles_x, tiles_z)
    {
        Category = "Corridor";
        Available_Sides = new List<string>() { "Left", "Right"};
    }

    public override void CreateRoom(List<GameObject> tiles)
    {
        Tile newtile;
        float xpos = Position.x, ypos = Position.y, zpos = Position.z;
        for (int i = 0; i < Tiles_number_x; i++)
        { 
            newtile = new Tile("Horizontal_Wall", tiles.Where(obj => obj.name == "Corridor_Horizontal").First(), xpos, zpos);
            RoomTiles.Add(newtile);
            xpos += Tile.X_length;
        }

    }
}
