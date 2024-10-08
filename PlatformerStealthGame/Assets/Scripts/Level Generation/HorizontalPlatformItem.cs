using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HorizontalPlatformItem
{
    public GameObject obj;
    public int x;
    public int y;
    public HorizontalPlatformItem leftnb;
    public HorizontalPlatformItem belownb;
    public int leftPlatforms = -1;
    public bool leftWall;
    public int rightPlatforms = -1;
    public bool rightWall;

    public HorizontalPlatformItem(GameObject _obj, int _x, int _y)
    {
        obj = _obj;
        x = _x;
        y = _y;
    }

}

