using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TileGeneratorType
{
    BaseLayer,  //Sand, gravel, soil, rock
    Vegetation, //Grass, trees, plants
    Mineral     //Rocks, ores, gems
}

public enum VegetationType
{
    Grass,
    Plant,
    Tree
}

public enum MineralType
{
    Stone,
    Rock,
    Boulder
}


public class TileGenerator
{
    TileGeneratorType tileGenratorType;
    int worldWidth;
    int worldHeight;

    float DeepestWater = 0.1f;
    float DeepWater = 0.15f;
    float ShallowWater = 0.22f;
    float FertileLands = 0.37f;
    float Lowlands = 0.52f;
    float Highlands = 0.60f;
    float Hills = 0.85f;
    float Mountain = 0.93f;
    float Peak = 1f;

    int layer;

    List<Tile> tiles;
    GameObject tileLayerGO;

    public TileGenerator(TileGeneratorType tileGenratorType, int layer, int worldWidth, int worldHeight, World world)
    {
        this.tileGenratorType = tileGenratorType;
        this.layer = layer;
        this.worldWidth = worldWidth;
        this.worldHeight = worldHeight;

        //Create tile layer game object and parent to world
        tileLayerGO = new GameObject(SetLayerName());
        tileLayerGO.transform.SetParent(world.transform);

        //Create list of tiles
        tiles = new List<Tile>();

        //Create the tiles based on the TileGeneratorType
        CreateTilesByType(world);
    }

    protected string SetLayerName()
    {
        switch(tileGenratorType)
        {
            case TileGeneratorType.BaseLayer:
                return "Base Layer";
            case TileGeneratorType.Vegetation:
                return "Vegetation Layer";
            case TileGeneratorType.Mineral:
                return "Mineral Layer";
            default:
                return "Name Not Set";
        }
    }

    public void CreateTilesByType(World world)
    {
        switch(tileGenratorType)
        {
            case TileGeneratorType.BaseLayer:
                GenerateBaseLayer(world);
                break;
            case TileGeneratorType.Vegetation:
                GenerateVegetationLayer(world);
                break;
            case TileGeneratorType.Mineral:
                GenerateMineralLayer(world);
                break;
        }
    }

    public void GenerateVegetationLayer(World world)
    {
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                int tileWidth = 32;
                int tileHeight = 32;
                float height = world.heightMap[x, y];

                //Chance for vegetation to spawn 1/5
                TileType vegetationType = TileType.Grass;
                int vegetationBonus = 5;

                bool noGrowthAltitude = false;

                if (height > ShallowWater && height <= FertileLands)
                {
                    int typeChance = Random.Range(0, 10);
                    switch (typeChance)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            vegetationType = TileType.Grass;
                            vegetationBonus = 4;
                            break;
                        case 9:
                            vegetationType = TileType.Plant;
                            vegetationBonus = 50;
                            break;
                    }
                }
                else if (height > FertileLands && height <= Lowlands)
                {
                    int typeChance = Random.Range(0, 10);
                    switch (typeChance)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            vegetationType = TileType.Grass;
                            vegetationBonus = 10;
                            break;
                        case 8:
                            vegetationType = TileType.Plant;
                            vegetationBonus = 30;
                            break;
                        case 9:
                            vegetationType = TileType.Tree;
                            vegetationBonus = 10;
                            break;
                    }
                }
                else if (height > Lowlands && height <= Highlands)
                {
                    int typeChance = Random.Range(0, 10);
                    switch (typeChance)
                    {
                        case 0:
                        case 1:
                        case 2:
                            vegetationType = TileType.Grass;
                            vegetationBonus = 20;
                            break;
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            vegetationType = TileType.Tree;
                            vegetationBonus = 20;
                            break;
                    }
                }
                else if (height > Highlands && height <= Hills)
                {
                    int typeChance = Random.Range(0, 10);
                    switch (typeChance)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            vegetationType = TileType.Tree;
                            vegetationBonus = 100;
                            break;
                    }
                }
                else
                {
                    noGrowthAltitude = true;
                }

                int vegetationChance = Random.Range(0, vegetationBonus);

                if (!noGrowthAltitude && vegetationChance == 1)
                {
                    Tile t = new Tile(vegetationType, x, y, tileWidth, tileHeight, layer, world, tileLayerGO);
                    tiles.Add(t);
                }
            }
        }
    }

    public void GenerateMineralLayer(World world)
    {
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                int tileWidth = 32;
                int tileHeight = 32;
                float height = world.heightMap[x, y];

                TileType mineralType = TileType.Stone;
                int bonus = 5;

                bool noGrowthAltitude = false;

                if (height > FertileLands && height <= Lowlands)
                {
                    int typeChance = Random.Range(0, 10);
                    switch (typeChance)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 9:
                            mineralType = TileType.Stone;
                            bonus = 500;
                            break;
                    }
                }
                else if (height > Lowlands && height <= Highlands)
                {
                    int typeChance = Random.Range(0, 10);
                    switch (typeChance)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            mineralType = TileType.Stone;
                            bonus = 500;
                            break;
                        case 8:
                        case 9:
                            mineralType = TileType.Rock;
                            bonus = 200;
                            break;
                    }
                }
                else if (height > Highlands && height <= Hills)
                {
                    int typeChance = Random.Range(0, 10);
                    switch (typeChance)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            mineralType = TileType.Rock;
                            bonus = 200;
                            break;
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            mineralType = TileType.Boulder;
                            bonus = 100;
                            break;
                    }
                }
                else if (height > Hills && height <= Peak)
                {
                    int typeChance = Random.Range(0, 10);
                    switch (typeChance)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            mineralType = TileType.Boulder;
                            bonus = 10;
                            break;
                    }
                }
                else
                {
                    noGrowthAltitude = true;
                }

                int mineralChance = Random.Range(0, bonus);

                if (!noGrowthAltitude && mineralChance == 1)
                {
                    Tile t = new Tile(mineralType, x, y, tileWidth, tileHeight, layer, world, tileLayerGO);
                    tiles.Add(t);
                }
            }
        }
    }

    public void GenerateBaseLayer(World world)
    {
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                //Create tile and determine tile type noise etc
                int tileWidth = 32;
                int tileHeight = 32;

                float height = world.heightMap[x, y];

                //Determine heights
                //Water
                if(height <= DeepestWater) 
                {
                    Tile t = new Tile(TileType.Water2, x, y, tileWidth, tileHeight, layer, world, tileLayerGO);
                    tiles.Add(t);
                }
                else if (height > DeepestWater && height <= DeepWater) 
                {
                    Tile t = new Tile(TileType.Water1, x, y, tileWidth, tileHeight, layer, world, tileLayerGO);
                    tiles.Add(t);

                }
                else if (height > DeepWater && height <= ShallowWater) 
                {
                    Tile t = new Tile(TileType.Water0, x, y, tileWidth, tileHeight, layer, world, tileLayerGO);
                    tiles.Add(t);
                }

                //Grass
                else if (height > ShallowWater && height <= FertileLands)
                {
                    Tile t = new Tile(TileType.Grass0, x, y, tileWidth, tileHeight, layer, world, tileLayerGO);
                    tiles.Add(t);
                }

                else if (height > FertileLands && height <= Lowlands)
                {
                    Tile t = new Tile(TileType.Grass1, x, y, tileWidth, tileHeight, layer, world, tileLayerGO);
                    tiles.Add(t);
                }

                else if (height > Lowlands && height <= Highlands)
                {
                    Tile t = new Tile(TileType.Grass2, x, y, tileWidth, tileHeight, layer, world, tileLayerGO);
                    tiles.Add(t);
                }

                //Rock
                else if (height > Highlands && height <= Hills)
                {
                    Tile t = new Tile(TileType.Rock0, x, y, tileWidth, tileHeight, layer, world, tileLayerGO);
                    tiles.Add(t);
                }

                else if (height > Hills && height <= Mountain)
                {
                    Tile t = new Tile(TileType.Rock1, x, y, tileWidth, tileHeight, layer, world, tileLayerGO);
                    tiles.Add(t);
                }

                else 
                {
                    Tile t = new Tile(TileType.Rock2, x, y, tileWidth, tileHeight, layer, world, tileLayerGO);
                    tiles.Add(t);
                }
            }
        }
    }
}
