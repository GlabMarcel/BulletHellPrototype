using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tile[] tiles;
    public Vector3 tileScale = new Vector3(1.5f, 1.5f, 1);

    public GameObject player;
    public float generateDistance = 8f;
    public float unloadDistance = 12f;

    public int extraTiles = 6;
    private const float PathTileProbability = 0.2f;

    private Tilemap tilemap;
    private Vector3Int lastPlayerTile;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        GenerateFloor();
        lastPlayerTile = tilemap.WorldToCell(player.transform.position);
    }

    private void Update()
    {
        CheckForGeneration();
        UnloadTiles();
    }

    private void CheckForGeneration()
    {
        Vector3Int currentPlayerTile = GetCurrentPlayerTile();
        if (IsGenerationNeeded(currentPlayerTile))
        {
            GenerateFloor();
            lastPlayerTile = currentPlayerTile;
        }
    }

    private Vector3Int GetCurrentPlayerTile()
    {
        return tilemap.WorldToCell(player.transform.position);
    }

    private bool IsGenerationNeeded(Vector3Int currentPlayerTile)
    {
        return Vector3.Distance(currentPlayerTile, lastPlayerTile) >= generateDistance;
    }

    private void UnloadTiles()
    {
        BoundsInt bounds = tilemap.cellBounds;
        Vector3Int currentPlayerTile = GetCurrentPlayerTile();

        foreach (var pos in bounds.allPositionsWithin)
        {
            if (Vector3.Distance(pos, currentPlayerTile) > unloadDistance)
            {
                tilemap.SetTile(pos, null);
            }
        }
    }

    private void GenerateFloor()
    {
        Vector3Int playerTilePos = GetCurrentPlayerTile();
        int rows = CalculateRows();
        int cols = CalculateCols();

        for (int i = -extraTiles; i < rows - extraTiles; i++)
        {
            for (int j = -extraTiles; j < cols - extraTiles; j++)
            {
                Vector3Int tilePos = new Vector3Int(j + playerTilePos.x, i + playerTilePos.y, 0);
                PlaceTile(tilePos, i, j);
            }
        }
    }

    private int CalculateRows()
    {
        return Mathf.CeilToInt(Camera.main.orthographicSize * 2) + extraTiles * 2;
    }

    private int CalculateCols()
    {
        return Mathf.CeilToInt(Camera.main.aspect * Camera.main.orthographicSize * 2) + extraTiles * 2;
    }

    private void PlaceTile(Vector3Int tilePos, int i, int j)
    {
        if (tilemap.GetTile(tilePos) == null)
        {
            Tile tileToPlace = ChooseTile(tilePos, i, j);
            tilemap.SetTile(tilePos, tileToPlace);
            tilemap.SetTransformMatrix(tilePos, Matrix4x4.TRS(Vector3.zero, Quaternion.identity, tileScale));
        }
    }

    private Tile ChooseTile(Vector3Int tilePos, int i, int j)
    {
        bool hasPathNeighbor = HasPathNeighbor(tilePos);
        float randomChance = Random.Range(0f, 1f);

        if (i == 0 || j == 0 || (hasPathNeighbor && randomChance < PathTileProbability))
        {
            return tiles[0];
        }
        else
        {
            return tiles[Random.Range(1, tiles.Length)];
        }
    }

    private bool HasPathNeighbor(Vector3Int tilePos)
    {
        Vector3Int[] neighbors = {
            new Vector3Int(tilePos.x + 1, tilePos.y, tilePos.z),
            new Vector3Int(tilePos.x - 1, tilePos.y, tilePos.z),
            new Vector3Int(tilePos.x, tilePos.y + 1, tilePos.z),
            new Vector3Int(tilePos.x, tilePos.y - 1, tilePos.z)
        };

        foreach (Vector3Int neighbor in neighbors)
        {
            if (tilemap.GetTile(neighbor) == tiles[0])
            {
                return true;
            }
        }
        return false;
    }
}
