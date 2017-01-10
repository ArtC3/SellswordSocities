using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//<><><><><><><><><><><><><><>IMPORTANT!<><><><><><><><><><><><><><>


//This script is only to test tile selection in development on a computer!
//Tiles will generally not highlight in the published build!
//This script must be removed before shipping to save resource overhead!


//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
[RequireComponent(typeof(TileMap))]
public class TileMouseOver : MonoBehaviour {

    Collider c;
    Renderer r;
    TileMap tm;

    Vector2 currentTileCoords;
    public Transform selectionCube;

    void Start ()
    {
        c = GetComponent<Collider>();
        r = GetComponent<Renderer>();
        tm = GetComponent<TileMap>();
    }
    
    // Update is called once per frame
	void Update () {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (c.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            int x = Mathf.FloorToInt(hitInfo.point.x / tm.tileSize);
            int y = Mathf.FloorToInt(hitInfo.point.y / tm.tileSize);
            Debug.Log("Tile: " + x + ", " + y);

            currentTileCoords.x = x;
            currentTileCoords.y = y;

            selectionCube.transform.position = currentTileCoords * 5f;
        }
        else
        {

        }
	}
}
