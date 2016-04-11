using UnityEngine;
using System.Collections;

public enum TileType
{
    Grass0,
    Grass1,
    Grass2,
    Water0,
    Water1,
    Water2,
    Rock0,
    Rock1,
    Rock2,

    //Objects
    Grass,
    Plant,
    Tree,

    Stone,
    Rock,
    Boulder
}

public class Tile
{
    TileType tileType;
    GameObject tileGameObject;
    SpriteRenderer renderer;

    int xPosition;
    int yPosition;

    int tileWidth;
    int tileHeight;
    int tileLayer;

    World world;

    public Tile(TileType tileType, int xPosition, int yPosition, int tileWidth, int tileHeight, int tileLayer, World world, GameObject layer)
    {
        this.tileType = tileType;

        this.xPosition = xPosition;
        this.yPosition = yPosition;

        this.tileWidth = tileWidth;
        this.tileHeight = tileHeight;
        this.tileLayer = tileLayer;

        this.world = world;

        tileGameObject = new GameObject();
       
        tileGameObject.name = string.Format("tile_{0}_{1}", xPosition, yPosition);
        tileGameObject.transform.SetParent(layer.transform);

        //Set position
        Vector2 pos = new Vector2(xPosition, yPosition);
        tileGameObject.transform.position = pos;

        //Rendering
        tileGameObject.AddComponent<SpriteRenderer>();
        renderer = tileGameObject.GetComponent<SpriteRenderer>();

        renderer.sortingOrder = tileLayer;

        //Material
        Material mat = Resources.Load("Material/LightMaterial", typeof(Material)) as Material;
        renderer.material = mat;

        SetSprite();
    }

    public void UpdateDisplay()
    {
       
    }

    public void SetSprite()
    {
        if(world.tiles.ContainsKey(tileType))
        {
            renderer.sprite = world.tiles[tileType];
        }
    }
}
