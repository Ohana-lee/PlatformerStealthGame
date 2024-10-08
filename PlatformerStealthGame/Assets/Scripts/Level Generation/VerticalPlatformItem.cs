using UnityEngine;
[System.Serializable]
public class VerticalPlatformItem
{
    public GameObject obj;
    public int x;
    public int y;
    public int belowPlatforms = -1;
    public int abovePlatforms = -1;
    public bool belowWall;
    public bool aboveWall;
    public VerticalPlatformItem(GameObject _obj, int _x, int _y)
    {
        obj = _obj;
        x = _x;
        y = _y;
    }
}

