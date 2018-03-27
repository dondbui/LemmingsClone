using Model;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Engine : MonoBehaviour
{
    public static Tilemap tileMap;

    private TileBase spawnTile;
    private Vector3Int spawnPos;
    private TileBase goalTile;

    private int walkersMade = 0;

	// Use this for initialization
	public void Start()
    {
        GameObject tmObj = GameObject.Find("Tilemap");
        tileMap = tmObj.GetComponent<Tilemap>();

        // Load up the first level
        //GameObject.Instantiate(Resources.Load<GameObject>(Constants.PREFAB_LVL01));

        Grid grid = tileMap.layoutGrid;

        Bounds mapBounds = tileMap.localBounds;
        Debug.Log("TileMap Size: " + tileMap.size);
        Debug.Log("Cell Bounds: " + tileMap.cellBounds);
        Debug.Log("Origin: " + tileMap.origin);
        Debug.Log("Center: " + mapBounds.center);

        //ScanTiles();
        InitSpawnPosFromGameObject();
        SpawnWalkers();
	}
	
	// Update is called once per frame
	public void Update()
    {
		
	}

    private void InitSpawnPosFromGameObject()
    {
        GameObject spawnObj = GameObject.Find("spawn");
        Vector3Int pos = tileMap.WorldToCell(spawnObj.transform.position);
        spawnPos = new Vector3Int(pos.x, pos.y - 1, pos.z);
    }

    private void ScanTiles()
    {
        BoundsInt bounds = tileMap.cellBounds;
        Vector3Int origin = bounds.position;
        Vector3Int size = bounds.size;

        int startX = origin.x;
        int endX = origin.x + size.x;

        int startY = origin.y;
        int endY = origin.y + size.y;

        Vector3Int scratchPos = new Vector3Int();
        TileData scratchTD = new TileData();

        for (int x = startX; x < endX; x++)
        {
            for (int y = startY; y < endY; y++)
            {
                scratchPos.x = x;
                scratchPos.y = y;

                Tile tile = tileMap.GetTile<Tile>(scratchPos);
                if (tile == null)
                {
                    continue;
                }
                

                // If it's a spawn tile then set the spawn tile and position
                if (tile.sprite.name == Constants.TILE_SPAWN)
                {
                    Debug.Log("Tile Sprite: " + tile.sprite + ": " + scratchPos);
                    spawnTile = tile;

                    spawnPos = new Vector3Int(x, y, 0);
                }
            }
        }

    }

    private void SpawnWalkers()
    {
        GameObject walker = GameObject.Instantiate(Resources.Load<GameObject>(Constants.PREFAB_WALKER));
        walker.transform.position = tileMap.CellToWorld(spawnPos);

        walkersMade++;

        if (walkersMade < Constants.NUM_WALKERS)
        {
            Invoke("SpawnWalkers", Constants.SPAWN_DELAY);
        }
    }

   
}
