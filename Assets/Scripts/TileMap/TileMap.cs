using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour {

    public int size_x = 10;
    public int size_y = 10;
    public float tileSize = 1.0f;

    public Texture2D terrainTiles;
    public int tileResolution;

    // Use this for initialization
    void Start () {

        BuildMesh();
	}

    Color[][] ChopTerrainTiles()
    {
        int numTilesPerRow = terrainTiles.width / tileResolution;
        int numRows = terrainTiles.height / tileResolution;

        Color[][] tiles = new Color[numTilesPerRow * numRows][];

        for(int y = 0; y < numRows; y++)
        {
            for(int x = 0; x < numTilesPerRow; x++)
            {
                tiles[y*numTilesPerRow + x] = terrainTiles.GetPixels(x * tileResolution, y * tileResolution, tileResolution, tileResolution);
            }
        }

        return tiles;
    }

    void BuildTexture()
    {

        int textWidth = size_x * tileResolution;
        int textHeight = size_y * tileResolution;
        Texture2D texture = new Texture2D(textWidth, textHeight);

        Color[][] tiles = ChopTerrainTiles();

        for(int y = 0; y < size_y; y++)
        {
            for(int x = 0; x< size_x; x++)
            {
                Color[] p = tiles[Random.Range(0, 4)];
                texture.SetPixels(x * tileResolution, y * tileResolution, tileResolution, tileResolution, p);
            }
        }

        texture.filterMode = FilterMode.Point;
        
        texture.Apply();

        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        mesh_renderer.sharedMaterials[0].mainTexture = texture;
        Debug.Log("Finished Textures");
    }

    public void BuildMesh()
    {
        int numTiles = size_x * size_y;
        int numTris = numTiles * 2;

        int vsize_x = size_x + 1;
        int vsize_y = size_y + 1;
        int numVerts = vsize_x * vsize_y;

        //generating mesh data
        Vector3[] vertices = new Vector3[numVerts];
        Vector3[] normals = new Vector3[numVerts];
        Vector2[] uv = new Vector2[numVerts];

        int[] triangles = new int[numTris * 3];

        int x, y;
        for(y = 0; y < vsize_y; y++)
        {
            for(x = 0; x < vsize_x; x++)
            {
                vertices[y * vsize_x + x] = new Vector2(x * tileSize, y * tileSize);
                normals[y * vsize_x + x] = Vector2.up;
                uv[y * vsize_x + x] = new Vector2((float)x / size_x, (float)y / size_y);
            }
        }
        Debug.Log("Vertices finished");

        for(y = 0; y < size_y; y++)
        {
            for(x = 0; x < size_x; x++)
            {
                int squareIndex = y * size_x + x;
                int triOffset = squareIndex * 6;
                triangles[triOffset + 0] = y * vsize_x + x +           0;
                triangles[triOffset + 1] = y * vsize_x + x + vsize_x + 0;
                triangles[triOffset + 2] = y * vsize_x + x + vsize_x + 1;

                triangles[triOffset + 3] = y * vsize_x + x +           0;
                triangles[triOffset + 4] = y * vsize_x + x + vsize_x + 1;
                triangles[triOffset + 5] = y * vsize_x + x +           1;
            }
        }
        Debug.Log("Triangles finished");

        //creates a new mesh and populates it with generated mesh data
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        //assigns created mesh to the filter, renderer, and collider
        MeshFilter mesh_filter = GetComponent<MeshFilter>();
        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        MeshCollider mesh_collider = GetComponent<MeshCollider>();

        mesh_filter.mesh = mesh;
        mesh_collider.sharedMesh = mesh;
        Debug.Log("Finished Mesh");

        BuildTexture();
    }
	
}
