using UnityEngine;
using System.Collections.Generic;

public class TileDataMap {

    protected class DataBuilding
    {
        public int left;
        public int top;
        public int width;
        public int height;

        public bool isConnected = false;

        public int right
        {
            get { return left + width - 1; }
        }
        public int bottom
        {
            get { return top + height - 1; }
        }

        public int center_x
        {
            get { return left + width / 2; }
        }
        public int center_y
        {
            get { return top + height / 2; }
        }

        public bool CollidesWith(DataBuilding other)
        {
            if (left > other.right)
                return false;

            if (top > other.bottom)
                return false;

            if (right < other.left)
                return false;

            if (bottom < other.top)
                return false;

            return true;
        }
    }

    int size_x;
    int size_y;

    int[,] map_data;

    List<DataBuilding> buildings;

    //0 = street
    //1 = building
    //2 = grass
    //3 = path

    public TileDataMap(int x, int y)
    {
        size_x = x;
        size_y = y;

        map_data = new int[size_x, size_y];

        //sets the initial map to entirely grass
        for (int x1 = 0; x1 < size_x; x1++)
        {
            for (int y1 = 0; y1 < size_y; y1++)
            {
                map_data[x1, y1] = 2;
            }
        }

        //creates a random avenue or main street to act as an anchor for building spawns
        MakeAvenue();

        //buildings = new List<DataBuilding>();

        ////number of allowed fails before the guaranteed buildings loop is ditched
        //int maxFails = 10;

        ////set the number of guaranteed buildings per level here
        //while (buildings.Count < 6)
        //{
        //    int roomSize_x = Random.Range(4, 14);
        //    int roomSize_y = Random.Range(4, 12);

        //    DataBuilding b = new DataBuilding();
        //    b.left = Random.Range(0, size_x - roomSize_x);
        //    b.top = Random.Range(0, size_y - roomSize_y);
        //    b.width = roomSize_x;
        //    b.height = roomSize_y;

        //    if (!BuildingCollides(b))
        //    {
        //        buildings.Add(b);
        //    }
        //    else
        //    {
        //        maxFails--;
        //        if(maxFails <=0)
        //        {
        //            break;
        //        }
        //    } 

        //    foreach(DataBuilding b2 in buildings)
        //    {
        //        MakeBuilding(b2);
        //    }

        //    //connects every building with a path
        //    for(int i = 0; i < buildings.Count; i++)
        //    {
        //        if (!buildings[i].isConnected)
        //        {
        //            int j = Random.Range(1, buildings.Count);

        //            MakePath(buildings[i], buildings[(i + j) % buildings.Count]);
        //        }
        //    }

        //    ThickenPath();
        //}
    }

    void MakeAvenue()
    {
        int avenueThickness = 2;

        //generates starting coordinates for the avenue
        int start_x = Random.Range(0, size_x);
        int start_y = 0;

        //generates ending coordinates for the avenue
        int end_x = Random.Range(0, size_x);
        int end_y = size_y - 1;
        map_data[end_x, end_y] = 3;

        //generates a middle point for the two nodes to connect to 
        int mid_x = Random.Range(0, size_x);
        int mid_y = size_y / 2;

        //connects the start node to the middle node
        while (start_y != mid_y)
        {
            map_data[start_x, start_y] = 3;
            for (int x = 0; x <= avenueThickness; x++)
            {
                map_data[start_x + x, start_y] = 3;
                map_data[start_x - x, start_y] = 3;
            }
            start_y += start_y < mid_y ? 1 : -1;
        }
        while (start_x != mid_x)
        {
            map_data[start_x, start_y] = 3;
            for (int y = 0; y <= avenueThickness; y++)
            {
                map_data[start_x, start_y + y] = 3;
                map_data[start_x, start_y - y] = 3;
            }
            start_x += start_x < mid_x ? 1 : -1;
        }

        //connects the middle node to the end node
        while (mid_y != end_y)
        {
            map_data[mid_x, mid_y] = 3;
            for (int x = 0; x <= avenueThickness; x++)
            {
                map_data[mid_x + x, mid_y] = 3;
                map_data[mid_x - x, mid_y] = 3;
            }
            mid_y += mid_y < end_y ? 1 : -1;
        }
        while (mid_x != end_x)
        {
            map_data[mid_x, mid_y] = 3;
            for (int y = 0; y <= avenueThickness; y++)
            {
                map_data[mid_x, mid_y + y] = 3;
                map_data[mid_x, mid_y - y] = 3;
            }
            mid_x += mid_x < end_x ? 1 : -1;
        }
    }


    bool BuildingCollides(DataBuilding b)
    {
        foreach(DataBuilding b2 in buildings)
        {
            if (b.CollidesWith(b2))
                return true;
        }

        return false;
    }

    public int GetTileAt(int x, int y)
    {
        return map_data[x, y];
    }

    void MakeBuilding(DataBuilding b)
    {
        for (int x = 0; x < b.width; x++)
        {
            for (int y = 0; y < b.height; y++)
            {
                if (x == 0 || x == b.width - 1 || y == 0 || y == b.height - 1)
                {
                    map_data[b.left + x, b.top + y] = 1;
                }
                else
                    map_data[b.left + x, b.top + y] = 0;
            }
        }
    }

    void MakePath(DataBuilding b1, DataBuilding b2)
    {
        int x = b1.center_x;
        int y = b1.center_y;

        //connects buildings with a path in the x direction
        while (x != b2.center_x)
        {
            map_data[x, y] = 0;
            x += x < b2.center_x ? 1 : -1;
        }

        //connects buildings with a path in the y direction
        while (y != b2.center_y)
        {
            map_data[x, y] = 0;
            y += y < b2.center_y ? 1 : -1;
        }

        b1.isConnected = true;
        b2.isConnected = true;
    }

    void ThickenPath()
    {
        for (int x = 0; x < size_x; x++)
        {
            for (int y = 0; y <size_y; y++)
            {
                if (map_data[x, y] == 3 && HasAdjacentGrass(x, y))
                    map_data[x, y] = 3;
            }
        }
    }

    bool HasAdjacentGrass(int x, int y)
    {
        //checks up, down, left, and right of each tile for grass
        if (x > 0 && map_data[x - 1, y] == 2)
            return true;
        if (x < size_x - 1 && map_data[x + 1, y] == 2)
            return true;
        if (y > 0 && map_data[x, y - 1] == 2)
            return true;
        if (y < size_y - 1 && map_data[x, y + 1] == 2)
            return true;

        //checks diagonals of each tile for grass
        if (x > 0 && y > 0 && map_data[x - 1, y - 1] == 2)
            return true;
        if (x < size_x - 1 && y > 0 && map_data[x + 1, y - 1] == 2)
            return true;
        if (x > 0 && y < size_y - 1 && map_data[x - 1, y + 1] == 2)
            return true;
        if (x < size_x - 1 && y < size_y - 1 && map_data[x + 1, y + 1] == 2)
            return true;

        return false;
    }
}
