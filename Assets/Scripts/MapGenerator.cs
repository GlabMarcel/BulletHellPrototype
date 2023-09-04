using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tile[] tiles;
    public int extraTiles = 6;
    public Vector3 tileScale = new Vector3(1.5f, 1.5f, 1);
    public GameObject player;
    public float generateDistance = 8f;
    public float mapWidth = 50f;  // The width of the map
    public float mapHeight = 50f; // The height of the map

    private Tilemap tilemap;
    private Vector3Int lastPlayerTile;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        GenerateFloor();
        lastPlayerTile = tilemap.WorldToCell(player.transform.position);
    }

    void Update()
    {
        CheckForGeneration();

        Vector3 position = player.transform.position;

        // Check for horizontal wrapping
        if (position.x > mapWidth / 2)
        {
            position.x = -mapWidth / 2;
        }
        else if (position.x < -mapWidth / 2)
        {
            position.x = mapWidth / 2;
        }

        // Check for vertical wrapping
        if (position.y > mapHeight / 2)
        {
            position.y = -mapHeight / 2;
        }
        else if (position.y < -mapHeight / 2)
        {
            position.y = mapHeight / 2;
        }

        // Update player position
        player.transform.position = position;
    }

    void CheckForGeneration()
    {
        Vector3Int currentPlayerTile = tilemap.WorldToCell(player.transform.position);

        // Check distance to the nearest edge
        Vector3Int distanceToEdge = new Vector3Int(
            Mathf.Abs(currentPlayerTile.x - lastPlayerTile.x),
            Mathf.Abs(currentPlayerTile.y - lastPlayerTile.y),
            0
        );

        if (distanceToEdge.x >= generateDistance || distanceToEdge.y >= generateDistance)
        {
            GenerateFloor();
            lastPlayerTile = currentPlayerTile;
        }
    }

    void GenerateFloor()
    {
        Vector3Int playerTilePos = tilemap.WorldToCell(player.transform.position);

        // Calculate rows and columns based on camera view
        int rows = Mathf.CeilToInt(Camera.main.orthographicSize * 2) + extraTiles * 2;
        int cols = Mathf.CeilToInt(Camera.main.aspect * Camera.main.orthographicSize * 2) + extraTiles * 2;

        // Generate tiles around the player
        for (int i = -extraTiles; i < rows - extraTiles; i++)
        {
            for (int j = -extraTiles; j < cols - extraTiles; j++)
            {
                Vector3Int tilePos = new Vector3Int(j + playerTilePos.x, i + playerTilePos.y, 0);

                // Check if a tile is already set at this position
                if (tilemap.GetTile(tilePos) == null)
                {
                    // Choose a random tile from the available tiles
                    Tile randomTile = tiles[Random.Range(0, tiles.Length)];

                    // Set the tile at the calculated position
                    tilemap.SetTile(tilePos, randomTile);

                    // Scale the tile GameObject
                    tilemap.SetTransformMatrix(tilePos, Matrix4x4.TRS(Vector3.zero, Quaternion.identity, tileScale));
                }
            }
        }
    }


}
