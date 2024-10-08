using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject wallLeft;
    public GameObject wallRight;
    public GameObject ceiling;
    public GameObject floor;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    public bool testing = false;
    private float platformLength = 8.3f;
    public int horizontalPlatformCount = 10;
    public int verticalPlatformCount = 10;
    public int robotCount = 3;
    public int CCTVCount = 3;
    public int maxLasers = 3;
    public PlatformGenerator platformGenerator;
    public EnemyGenerator enemyGenerator;

    void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        minX = wallLeft.transform.position.x;
        maxX = wallRight.transform.position.x;
        minY = floor.transform.position.y;
        maxY = ceiling.transform.position.y;
        platformGenerator.SetValues(minX, maxX, minY, maxY, platformLength);

        if (!testing)
        {
            platformGenerator.GeneratePlatforms(horizontalPlatformCount, verticalPlatformCount, maxLasers);
        }
        else
        {
            platformGenerator.Test();
        }

        for (int r = 0; r < robotCount; r++)
        {
            enemyGenerator.GenerateRobot(platformGenerator.GetHorizontalPlatform());
        }

        for (int c = 0; c < CCTVCount; c++)
        {
            enemyGenerator.GenerateCCTV(platformGenerator.GetVerticalPlatform());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(ceiling.transform.position, floor.transform.position);
        Gizmos.DrawLine(wallLeft.transform.position, wallRight.transform.position);
    }
}