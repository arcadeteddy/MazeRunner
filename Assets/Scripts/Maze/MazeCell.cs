using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// : is extending or inheriting
public class MazeCell
{
    public bool visited = false;
    public GameObject northWall, southWall, eastWall, westWall, floor, boost_obj, spray_obj;
    public GameObject objects_obj;
}
