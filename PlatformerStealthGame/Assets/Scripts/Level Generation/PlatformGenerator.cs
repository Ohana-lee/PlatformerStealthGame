using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject laserPrefab;
    public GameObject player;
    public GameObject door;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private float mapWidth;
    private float mapHeight;
    private float platformLength;
    private int rows;
    private int columns;
    private List<HorizontalPlatformItem>[] horizontalPlatforms;
    private List<int>[] horizontalSpaces;
    private List<VerticalPlatformItem>[] verticalPlatforms;
    private List<int>[] verticalSpaces;
    private List<int>[] lasers;

    public void PrintPlatforms()
    {
        for (int row = 0; row < horizontalSpaces.Length; row++)
        {
            for (int column = 0; column < horizontalSpaces[row].Count; column++)
            {
                Debug.Log("Horizontal space at " + row + ", " + horizontalSpaces[row][column]);
            }
        }
    }

    public void SetValues(float _minX, float _maxX, float _minY, float _maxY, float _platformLength)
    {
        minX = _minX;
        maxX = _maxX;
        minY = _minY;
        maxY = _maxY;
        platformLength = _platformLength;
        mapWidth = maxX - minX;
        mapHeight = maxY - minY;
        rows = (int)(mapHeight / platformLength);
        columns = (int)(mapWidth / platformLength);

        horizontalPlatforms = new List<HorizontalPlatformItem>[rows];
        horizontalSpaces = new List<int>[rows];
        lasers = new List<int>[rows];
        for (int y = 0; y < rows; y++)
        {
            horizontalPlatforms[y] = new List<HorizontalPlatformItem>();
            horizontalSpaces[y] = new List<int>();
            lasers[y] = new List<int>();
            for (int x = 0; x < columns; x++)
            {
                horizontalSpaces[y].Add(x);
            }
        }
        verticalPlatforms = new List<VerticalPlatformItem>[columns];
        verticalSpaces = new List<int>[columns];
        for (int x = 0; x < columns; x++)
        {
            verticalPlatforms[x] = new List<VerticalPlatformItem>();
            verticalSpaces[x] = new List<int>();
            for (int y = 0; y < rows; y++)
            {
                verticalSpaces[x].Add(y);
            }
        }
    }
    /*public void GeneratePlatforms(int horizontalPlatformCount, int verticalPlatformCount, int maxLasers)
    {
        for (int x = 0; x < columns; x++)
        {
            InstantiateHorizontalPlatform(0, x, true);
        }
        int h = horizontalPlatformCount;
        while (0 < h)
        {
            int y;
            while (true)
            {
                y = Random.Range(0, rows);
                if (0 < horizontalSpaces[y].Count)
                {
                    break;
                }
            }
            int column = Random.Range(0, horizontalSpaces[y].Count);

            int x = horizontalSpaces[y][column];
            int index = column;
            while (0 < h)
            {
                if (column == columns || index == horizontalSpaces[y].Count || horizontalSpaces[y][index] != x)
                {
                    break;
                }
                InstantiateHorizontalPlatform(y, x, false);
                column++;
                x++;
                h -= 1;
                int chance = Random.Range(0, 3);
                if (chance == 0)
                {
                    break;
                }
            }
        }
        int v = verticalPlatformCount;
        while (0 < v)
        {
            int x;
            while (true)
            {
                x = Random.Range(1, columns);
                if (0 < verticalSpaces[x].Count)
                {
                    break;
                }
            }
            int row = Random.Range(0, verticalSpaces[x].Count);
            int y = verticalSpaces[x][row];
            int index = row;
            while (0 < v)
            {
                if (row == rows || index == verticalSpaces[x].Count || verticalSpaces[x][index] != y)
                {
                    break;
                }
                InstantiateVerticalPlatform(x, y);
                row++;
                y++;
                v -= 1;
                int chance = Random.Range(0, 3);
                if (chance == 0)
                {
                    break;
                }
            }
        }
        // horizontal lasers
        List<HorizontalPlatformItem> middlePlatforms = new();
        for (int y = 1; y < horizontalPlatforms.Length; y++)
        {
            for (int x = 1; x < horizontalPlatforms[y].Count; x++)
            {
                HorizontalPlatformItem platform = horizontalPlatforms[y][x];
                platform.leftPlatforms = GetLeftPlatforms(platform);
                platform.rightPlatforms = GetRightPlatforms(platform);
                if (0 < platform.leftPlatforms && 0 < platform.rightPlatforms)
                {
                    middlePlatforms.Add(platform);
                }
            }
        }
        foreach (HorizontalPlatformItem platform in middlePlatforms)
        {
            Debug.Log("y = " + platform.y + ", x = " + platform.x);
            Debug.Log("lefts = " + platform.leftPlatforms + ", rights = " + platform.rightPlatforms);
        }
        int lasers = maxLasers;
        while (0 < lasers && middlePlatforms.Count != 0)
        {
            int i = Random.Range(0, middlePlatforms.Count);
            HorizontalPlatformItem laserPlatform = middlePlatforms[i];
            middlePlatforms.RemoveAt(i);
            Debug.Log("Instantiating at y: " + laserPlatform.y + ", x: " + laserPlatform.x);
            InstantiateLaser(laserPlatform);
            lasers--;
        }
    }*/
    public void GeneratePlatforms(int horizontalPlatformCount, int verticalPlatformCount, int maxLasers)
    {
        // if counts are beyond maximum possibles, lower them to max
        if ((rows - 1) * columns < horizontalPlatformCount)
        {
            horizontalPlatformCount = (rows - 1) * columns;
        }
        if (rows * (columns - 1) < verticalPlatformCount)
        {
            verticalPlatformCount = rows * (columns - 1);
        }

        // PHASE 1: generate path across map with horizontal platforms composed of two trees starting in opposite corners of the screen.
        int y = 0;
        int x = 0;
        List<HorizontalPlatformItem>[] branch1 = new List<HorizontalPlatformItem>[rows];
        for (int i = 0; i < rows; i++)
        {
            branch1[i] = new List<HorizontalPlatformItem>();
        }
        HorizontalPlatformItem nb;
        HorizontalPlatformItem prev = InstantiateHorizontalPlatform(y, x, true);
        branch1[y].Add(prev);
        for (int p = 0; p < 25; p++)
        {
            nb = GetNeighbour(y, x);
            if (nb != null)
            {
                y = nb.y;
                x = nb.x;
                branch1[y].Add(nb);
                prev = nb;
            }
            int chance = Random.Range(0, 3);
            if (chance == 0)
            {
                nb = GetHorizontalPlatform(branch1);
                y = nb.y;
                x = nb.x;
                prev = nb;
            }
        }
        y = rows - 1;
        x = columns - 1;
        List<HorizontalPlatformItem>[] branch2 = new List<HorizontalPlatformItem>[rows];
        for (int i = 0; i < rows; i++)
        {
            branch2[i] = new List<HorizontalPlatformItem>();
        }
        prev = InstantiateHorizontalPlatform(y, x, false);
        branch2[y].Add(prev);
        for (int p = 0; p < 20; p++)
        {
            nb = GetNeighbour(y, x);
            if (nb != null)
            {
                y = nb.y;
                x = nb.x;
                branch2[y].Add(nb);
                prev = nb;
            }
            int chance = Random.Range(0, 3);
            if (chance == 0)
            {
                nb = GetHorizontalPlatform(branch2);
                y = nb.y;
                x = nb.x;
                prev = nb;
            }
        }
        // PHASE 2: add remaining floors to HorizontalPlatformSpaces + remove platforms such that every platform segment has at least 1 platform with at least 1 space on the
        //          row above within 0/1 columns
        for (x = 0; x < columns; x++)
        {
            if (horizontalSpaces[0].Contains(x))
            {
                InstantiateHorizontalPlatform(0, x, true);
            }
        }
        for (y = 0; y < rows - 1; y++)
        {
            for (x = 0; x < columns; x++)
            {
                foreach (HorizontalPlatformItem platform in horizontalPlatforms[y])
                {
                    if (platform.x == x)
                    {
                        int rights = GetRightPlatforms(y, x, null);
                        for (int r = 0; r <= rights; r++)
                        {
                            if (CheckAboveNeighbours(y, x + r))
                            {
                                break;
                            }
                            if (r == rights)
                            {
                                int _x = RemoveAboveNeighbour(y, x + Random.Range(0, rights + 1));
                                for (int j = 0; j < branch1[y + 1].Count; j++)
                                {
                                    HorizontalPlatformItem p = branch1[y + 1][j];
                                    if (platform.x == _x)
                                    {
                                        branch1[y + 1].RemoveAt(j);
                                    }
                                }
                                for (int j = 0; j < branch2[y + 1].Count; j++)
                                {
                                    HorizontalPlatformItem p = branch2[y + 1][j];
                                    if (platform.x == _x)
                                    {
                                        branch2[y + 1].RemoveAt(j);
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }
        /*x = Random.Range(0, horizontalPlatforms[1].Count);
        Destroy(horizontalPlatforms[1][x].obj);
        horizontalPlatforms[1].RemoveAt(x);
        horizontalSpaces[1].Add(x);*/
        // PHASE 3: generate walls, excluding spaces that block access between any platform and its neighbouring platforms
        // find platform segments
        for (y = 0; y < rows; y++)
        {
            x = 0;
            while (x < columns)
            {
                if (!horizontalSpaces[y].Contains(x))
                {
                    int rights = GetRightPlatforms(y, x, null);
                    // find corresponding platform item
                    for (int i = 0; i < horizontalPlatforms[y].Count; i++)
                    {
                        if (horizontalPlatforms[y][i].x == x)
                        {
                            prev = horizontalPlatforms[y][i];
                            break;
                        }
                    }
                    // mark below neighbour of first platform in segment (if it exists)
                    ConnectBelowNeighbour(y, x, prev, true);
                    // iterate through platform segment and mark left neighbours
                    for (int r = 1; r <= rights; r++)
                    {
                        for (int i = 0; i < horizontalPlatforms[y].Count; i++)
                        {
                            if (horizontalPlatforms[y][i].x == x + r)
                            {
                                horizontalPlatforms[y][i].leftnb = prev;
                                prev = horizontalPlatforms[y][i];
                                break;
                            }
                        }
                    }
                    // mark below neighbour of last platform in segment
                    ConnectBelowNeighbour(y, x + rights, prev, false);
                    x += rights + 1;
                }
                else
                {
                    x++;
                }
            }
        }

        // generate walls in all spaces not excluded
        List<int>[] walls = new List<int>[columns];
        for (int column = 0; column < columns; column++)
        {
            walls[column] = new List<int>(verticalSpaces[column]);
        }
        for (int row = rows - 1; 0 <= row; row--)
        {
            for (int column = 0; column < horizontalPlatforms[row].Count; column++)
            {
                HorizontalPlatformItem platform = horizontalPlatforms[row][column];
                x = platform.x;
                if (platform.leftnb != null)
                {
                    walls[x].Remove(row);
                }
                if (platform.belownb != null)
                {
                    if (platform.belownb.x == platform.x)
                    {
                        walls[x].Remove(row);
                        walls[x].Remove(row - 1);
                        if (x + 1 != columns)
                        {
                            walls[x + 1].Remove(row);
                            walls[x + 1].Remove(row - 1);
                        }
                    }
                    if (platform.belownb.x == platform.x - 1 && x != 0)
                    {
                        walls[x].Remove(row);
                        walls[x].Remove(row - 1);
                    }
                    if (platform.belownb.x == platform.x + 1 && x + 1 != columns)
                    {
                        walls[x + 1].Remove(row);
                        walls[x + 1].Remove(row - 1);
                    }
                }
            }
        }
        for (int column = 1; column < columns; column++)
        {
            int limit = walls[column].Count;
            for (int row = 0; row < limit; row++)
            {
                InstantiateVerticalPlatform(column, walls[column][0]);
            }
        }
        // horizontal lasers
        List<HorizontalPlatformItem> middlePlatforms = new();
        for (y = 1; y < horizontalPlatforms.Length; y++)
        {
            for (x = 0; x < horizontalPlatforms[y].Count; x++)
            {
                HorizontalPlatformItem platform = horizontalPlatforms[y][x];
                platform.leftPlatforms = GetLeftPlatforms(platform.y, platform.x, platform);
                platform.rightPlatforms = GetRightPlatforms(platform.y, platform.x, platform);
                if (0 < platform.leftPlatforms && 0 < platform.rightPlatforms)
                {
                    middlePlatforms.Add(platform);
                }
            }
        }
        int lasers = maxLasers;
        while (0 < lasers && middlePlatforms.Count != 0)
        {
            int i = Random.Range(0, middlePlatforms.Count);
            HorizontalPlatformItem laserPlatform = middlePlatforms[i];
            middlePlatforms.RemoveAt(i);
            InstantiateLaser(laserPlatform);
            lasers--;
        }
        SpawnPlayer(branch1[0][0]);
        SpawnDoor(branch2[rows - 1][branch2[rows - 1].Count - 1]);
    }
    public void Test()
    {
        Debug.Log("Rows: " + rows);
        Debug.Log("Columns: " + columns);
        for (int x = 0; x < columns; x++)
        {
            InstantiateHorizontalPlatform(0, x, true);
        }
        for (int y = 1; y < rows; y++)
        {
            int limit = horizontalSpaces[y].Count;
            for (int x = 0; x < limit; x++)
            {
                InstantiateHorizontalPlatform(y, horizontalSpaces[y][0], false);
            }
        }
        for (int x = 1; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                InstantiateVerticalPlatform(x, y);
            }
        }
    }
    public HorizontalPlatformItem GetHorizontalPlatform()
    {
        int row;
        while (true)
        {
            row = Random.Range(0, rows);
            if (0 < horizontalPlatforms[row].Count)
            {
                break;
            }
        }
        int column = Random.Range(0, horizontalPlatforms[row].Count);
        HorizontalPlatformItem platform = horizontalPlatforms[row][column];
        horizontalPlatforms[row].RemoveAt(column);
        platform.leftPlatforms = GetLeftPlatforms(platform.y, platform.x, platform);
        platform.rightPlatforms = GetRightPlatforms(platform.y, platform.x, platform);
        return platform;
    }
    public HorizontalPlatformItem GetHorizontalPlatform(List<HorizontalPlatformItem>[] grid)
    {
        int row;
        while (true)
        {
            row = Random.Range(0, rows);
            if (0 < grid[row].Count)
            {
                break;
            }
        }
        int column = Random.Range(0, grid[row].Count);
        return grid[row][column];
    }
    public VerticalPlatformItem GetVerticalPlatform()
    {
        int column;
        while (true)
        {
            column = Random.Range(0, columns);
            if (0 < verticalPlatforms[column].Count)
            {
                break;
            }
        }
        int row = Random.Range(0, verticalPlatforms[column].Count);
        VerticalPlatformItem platform = verticalPlatforms[column][row];
        verticalPlatforms[column].RemoveAt(row);

        platform.belowPlatforms = GetBelowPlatforms(platform);
        platform.abovePlatforms = GetAbovePlatforms(platform);
        return platform;
    }
    public VerticalPlatformItem GetVerticalPlatform(List<VerticalPlatformItem>[] grid)
    {
        int column;
        while (true)
        {
            column = Random.Range(0, columns);
            if (0 < grid[column].Count)
            {
                break;
            }
        }
        int row = Random.Range(0, grid[column].Count);
        return grid[column][row];
    }
    public void SpawnPlayer(HorizontalPlatformItem platform)
    {
        player.transform.position = new Vector3(platform.obj.transform.position.x, platform.obj.transform.position.y + 4.09f, 1);
    }
    public void SpawnDoor(HorizontalPlatformItem platform)
    {
        door.transform.position = new Vector3(platform.obj.transform.position.x, platform.obj.transform.position.y + 2.75f, 1);
    }
    private HorizontalPlatformItem GetNeighbour(int y, int x)
    {
        List<int>[] nbs = new List<int>[3];
        for (int i = -1; i <= 1; i++)
        {
            nbs[i + 1] = new();
            if (y + i <= 0 || rows == y + i)
            {
                continue;
            }
            for (int j = -1; j <= 1; j++)
            {
                if (x + j < 0 || columns == x + j)
                {
                    continue;
                }
                if (i == 0 && j == 0)
                {
                    continue;
                }
                if (horizontalSpaces[y + i].Contains(x + j))
                {
                    nbs[i + 1].Add(j);
                }
            }
        }
        int _y;
        List<int> nonEmptyLists = new();
        for (int i = 0; i < 3; i++)
        {
            if (0 < nbs[i].Count)
            {
                nonEmptyLists.Add(i);
            }
        }
        if (nonEmptyLists.Count == 0)
        {
            return null;
        }
        _y = nonEmptyLists[Random.Range(0, nonEmptyLists.Count)];
        x += nbs[_y][Random.Range(0, nbs[_y].Count)];
        y += _y - 1;
        if (y == 0)
        {
            return InstantiateHorizontalPlatform(y, x, true);
        }
        else
        {
            return InstantiateHorizontalPlatform(y, x, false);
        }
    }
    private bool CheckAboveNeighbours(int y, int x)
    {
        if (y + 1 == rows)
        {
            return true;
        }
        for (int j = -1; j <= 1; j++)
        {
            if (x + j < 0 || columns == x + j)
            {
                continue;
            }
            if (horizontalSpaces[y + 1].Contains(x + j))
            {
                return true;
            }
        }
        return false;
    }
    private int RemoveAboveNeighbour(int y, int x)
    {
        if (y + 1 == rows)
        {
            return -1;
        }
        List<int> nbs = new();
        for (int j = -1; j <= 1; j++)
        {
            if (x + j < 0 || x + j == columns)
            {
                continue;
            }
            if (!horizontalSpaces[y + 1].Contains(x + j))
            {
                nbs.Add(x + j);
            }
        }
        int _x = nbs[Random.Range(0, nbs.Count)];
        for (int j = 0; j < horizontalPlatforms[y + 1].Count; j++)
        {
            HorizontalPlatformItem platform = horizontalPlatforms[y + 1][j];
            if (platform.x == _x)
            {
                horizontalPlatforms[y + 1].RemoveAt(j);
                horizontalSpaces[y + 1].Add(_x);
                platform.obj.SetActive(false);
            }
        }
        return _x;
    }
    private void ConnectBelowNeighbour(int y, int x, HorizontalPlatformItem node, bool left)
    {
        if (y == 0 || (left && x == 0) || (!left && x == columns - 1))
        {
            return;
        }
        int i;
        if (horizontalSpaces[y - 1].Contains(x))
        {
            if (left)
            {
                i = -1;
            }
            else
            {
                i = 1;
            }
        }
        else
        {
            i = 0;
        }
        for (int j = 0; j < horizontalPlatforms[y - 1].Count; j++)
        {
            HorizontalPlatformItem platform = horizontalPlatforms[y - 1][j];
            if (platform.x == x + i)
            {
                node.belownb = platform;
                break;
            }
        }
        return;
    }
    private int GetLeftPlatforms(int y, int x, HorizontalPlatformItem platform)
    {
        int leftPlatforms = 0;
        while (true)
        {
            x--;
            if (x < 0)
            {
                if (platform != null)
                {
                    platform.leftWall = true;
                }
                break;
            }
            if (!horizontalSpaces[y].Contains(x) && !lasers[y].Contains(x) && verticalSpaces[x + 1].Contains(y))
            {
                leftPlatforms++;
            }
            else
            {
                break;
            }
        }
        return leftPlatforms;
    }
    private int GetRightPlatforms(int y, int x, HorizontalPlatformItem platform)
    {
        int rightPlatforms = 0;
        while (true)
        {
            x++;
            if (x == columns)
            {
                if (platform != null)
                {
                    platform.rightWall = true;
                }
                break;
            }
            if (!horizontalSpaces[y].Contains(x) && !lasers[y].Contains(x) && verticalSpaces[x].Contains(y))
            {
                rightPlatforms++;
            }
            else if (!verticalSpaces[x].Contains(y) && platform != null)
            {
                platform.rightWall = true;
            }
            else
            {
                break;
            }
        }
        return rightPlatforms;

    }
    private int GetBelowPlatforms(VerticalPlatformItem platform)
    {
        int belowPlatforms = 0;
        int x = platform.x;
        int y = platform.y;
        while (true)
        {
            y--;
            if (y < 0)
            {
                if (platform != null)
                {
                    platform.belowWall = true;
                }
                break;
            }
            if (!verticalSpaces[x].Contains(y) && horizontalSpaces[y + 1].Contains(x - 1) && !lasers[y + 1].Contains(x - 1))
            {
                belowPlatforms++;
            }
            else if (!horizontalSpaces[y + 1].Contains(x - 1) && platform != null)
            {
                platform.belowWall = true;
            }
            else
            {
                break;
            }
        }
        return belowPlatforms;
    }
    private int GetAbovePlatforms(VerticalPlatformItem platform)
    {
        int abovePlatforms = 0;
        int x = platform.x;
        int y = platform.y;
        while (true)
        {
            y++;
            if (y == rows)
            {
                if (platform != null)
                {
                    platform.aboveWall = true;
                }
                break;
            }
            if (!verticalSpaces[x].Contains(y) && horizontalSpaces[y].Contains(x - 1) && !lasers[y].Contains(x - 1))
            {
                abovePlatforms++;
            }
            else if (!horizontalSpaces[y].Contains(x - 1) && platform != null)
            {
                platform.aboveWall = true;
            }
            else
            {
                break;
            }
        }
        return abovePlatforms;
    }
    private HorizontalPlatformItem InstantiateHorizontalPlatform(int y, int x, bool floor)
    {
        horizontalSpaces[y].Remove(x);
        float posX = minX + x * platformLength + (mapWidth % platformLength) / 2 + 4.25f;
        float posY = minY + y * platformLength;
        GameObject obj = Instantiate(platformPrefab, new Vector3(posX, posY, 1), Quaternion.identity);
        if (floor)
        {
            foreach (SpriteRenderer child in obj.GetComponentsInChildren<SpriteRenderer>())
            {
                child.enabled = false;
            }
        }
        HorizontalPlatformItem platform = new HorizontalPlatformItem(obj, x, y);
        horizontalPlatforms[y].Add(platform);
        return platform;
    }
    private VerticalPlatformItem InstantiateVerticalPlatform(int x, int y)
    {
        verticalSpaces[x].Remove(y);
        float posX = minX + x * platformLength + (mapWidth % platformLength) / 2 + 0.35f;
        float posY = minY + y * platformLength + 4.34f;
        GameObject obj = Instantiate(platformPrefab, new Vector3(posX, posY, 1), Quaternion.Euler(0, 0, 90));
        foreach (SpriteRenderer child in obj.GetComponentsInChildren<SpriteRenderer>())
        {
            child.sortingOrder = 17;
        }
        VerticalPlatformItem platform = new VerticalPlatformItem(obj, x, y);
        verticalPlatforms[x].Add(platform);
        return platform;
    }
    private void InstantiateLaser(HorizontalPlatformItem platform)
    {
        horizontalPlatforms[platform.y].Remove(platform);
        Destroy(platform.obj);
        platform.obj = Instantiate(laserPrefab, new Vector3(platform.obj.transform.position.x - 0.20f, platform.obj.transform.position.y + 0.35f, 1), Quaternion.identity);
        lasers[platform.y].Add(platform.x);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int y = 1; y < rows; y++)
        {
            foreach (HorizontalPlatformItem platform in horizontalPlatforms[y])
            {
                if (platform.leftnb != null)
                {
                    Gizmos.DrawLine(new Vector3(platform.leftnb.obj.transform.position.x, platform.leftnb.obj.transform.position.y, 1), new Vector3(platform.obj.transform.position.x, platform.obj.transform.position.y, 1));

                }
                if (platform.belownb != null)
                {
                    Gizmos.DrawLine(new Vector3(platform.belownb.obj.transform.position.x, platform.belownb.obj.transform.position.y, 1), new Vector3(platform.obj.transform.position.x, platform.obj.transform.position.y, 1));
                }
            }
        }
    }
}