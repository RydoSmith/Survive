using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour
{
    TileGenerator baseLayer;
    TileGenerator vegetationLayer;
    TileGenerator mineralLayer;

    public float[,] heightMap;
    public Dictionary<TileType, Sprite> tiles;

    int worldWidth = 300;
    int worldHeight = 300;

	// Use this for initialization
	void Start ()
    {
        tiles = new Dictionary<TileType, Sprite>();

        //Load the tile sprites
        LoadTiles();

        //Generate height map, this determines a tiles altitude
        GenerateHeightMap();

        //Generate layers
        baseLayer = new TileGenerator(TileGeneratorType.BaseLayer, 0, worldWidth, worldHeight, this);
        mineralLayer = new TileGenerator(TileGeneratorType.Mineral, 1, worldWidth, worldHeight, this);
        vegetationLayer = new TileGenerator(TileGeneratorType.Vegetation, 2, worldWidth, worldHeight, this);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void LoadTiles()
    {
        //Load Grass
        Sprite grass0 = Resources.Load("Tiles/grass_0", typeof(Sprite)) as Sprite;
        Sprite grass1 = Resources.Load("Tiles/grass_1", typeof(Sprite)) as Sprite;
        Sprite grass2 = Resources.Load("Tiles/grass_2", typeof(Sprite)) as Sprite;

        tiles.Add(TileType.Grass0, grass0);
        tiles.Add(TileType.Grass1, grass1);
        tiles.Add(TileType.Grass2, grass2);

        //Load water
        Sprite water0 = Resources.Load("Tiles/water_0", typeof(Sprite)) as Sprite;
        Sprite water1 = Resources.Load("Tiles/water_1", typeof(Sprite)) as Sprite;
        Sprite water2 = Resources.Load("Tiles/water_2", typeof(Sprite)) as Sprite;

        tiles.Add(TileType.Water0, water0);
        tiles.Add(TileType.Water1, water1);
        tiles.Add(TileType.Water2, water2);

        //Load rock
        Sprite rock0 = Resources.Load("Tiles/rock_0", typeof(Sprite)) as Sprite;
        Sprite rock1 = Resources.Load("Tiles/rock_1", typeof(Sprite)) as Sprite;
        Sprite rock2 = Resources.Load("Tiles/rock_2", typeof(Sprite)) as Sprite;

        tiles.Add(TileType.Rock0, rock0);
        tiles.Add(TileType.Rock1, rock1);
        tiles.Add(TileType.Rock2, rock2);

        //Objects
        Sprite grass = Resources.Load("Tiles/grass", typeof(Sprite)) as Sprite;
        Sprite plant = Resources.Load("Tiles/plant", typeof(Sprite)) as Sprite;
        Sprite tree = Resources.Load("Tiles/tree", typeof(Sprite)) as Sprite;

        Sprite stone = Resources.Load("Tiles/stone", typeof(Sprite)) as Sprite;
        Sprite rock = Resources.Load("Tiles/rock", typeof(Sprite)) as Sprite;
        Sprite boulder = Resources.Load("Tiles/boulder", typeof(Sprite)) as Sprite;

        tiles.Add(TileType.Grass, grass);
        tiles.Add(TileType.Plant, plant);
        tiles.Add(TileType.Tree, tree);

        tiles.Add(TileType.Stone, stone);
        tiles.Add(TileType.Rock, rock);
        tiles.Add(TileType.Boulder, boulder);
    }

    void GenerateHeightMap()
    {
        heightMap = Noise.GenerateNoiseMap(worldWidth, worldWidth, Random.Range(-1000, 1000), 150, 4, 0.3f, 1, Vector2.zero);
    }

}
