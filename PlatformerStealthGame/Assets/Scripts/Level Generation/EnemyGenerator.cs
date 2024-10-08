using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject robotPrefab;
    public GameObject CCTVPrefab;
    private List<GameObject> robots = new();
    private List<GameObject> cctvs = new();

    public void GenerateRobot(HorizontalPlatformItem platform)
    {
        robots.Add(InstantiateRobot(platform, platform.leftPlatforms, platform.rightPlatforms));
    }
    public void GenerateCCTV(VerticalPlatformItem platform)
    {
        cctvs.Add(InstantiateCCTV(platform, platform.belowPlatforms, platform.abovePlatforms));
    }

    private GameObject InstantiateRobot(HorizontalPlatformItem platform, int leftPlatforms, int rightPlatforms)
    {
        float x = platform.obj.transform.position.x - 0.1f;
        float y = platform.obj.transform.position.y + 2.1f;
        GameObject robot = Instantiate(robotPrefab, new Vector3(x, y, 1), Quaternion.identity);
        EnemyBehavior enemyBehaviour = robot.transform.GetChild(1).gameObject.GetComponent<EnemyBehavior>();
        enemyBehaviour.y = y;
        enemyBehaviour.minX = x - 0.01f - (leftPlatforms * 8.4f);
        if (!platform.leftWall)
        {
            enemyBehaviour.minX -= 3.3f;
        }
        enemyBehaviour.maxX = x + 0.01f + (rightPlatforms * 8.4f);
        if (!platform.rightWall)
        {
            enemyBehaviour.maxX += 3.3f;
        }
        return robot;
    }

    private GameObject InstantiateCCTV(VerticalPlatformItem platform, int belowPlatforms, int abovePlatforms)
    {
        float x = platform.obj.transform.position.x - 2.10f;
        float y = platform.obj.transform.position.y;
        GameObject cctv = Instantiate(CCTVPrefab, new Vector3(x, y, 1), Quaternion.identity);
        CCTVBehavior cctvBehaviour = cctv.transform.GetChild(1).gameObject.GetComponent<CCTVBehavior>();
        cctvBehaviour.x = x;
        cctvBehaviour.minY = y + 2.75f - (belowPlatforms * 8.40f);
        if (!platform.belowWall)
        {
            cctvBehaviour.minY -= 6.5f;
        }
        cctvBehaviour.maxY = y + 3.50f + (abovePlatforms * 8.40f);
        if (!platform.aboveWall)
        {
            cctvBehaviour.maxY += 0.88f;
        }
        return cctv;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        foreach (GameObject robot in robots)
        {
            Gizmos.DrawLine(new Vector3(robot.GetComponentInChildren<EnemyBehavior>().minX, robot.transform.position.y, 1), new Vector3(robot.GetComponentInChildren<EnemyBehavior>().maxX, robot.transform.position.y, 1));
        }
        Gizmos.color = Color.green;
        foreach (GameObject cctv in cctvs)
        {
            Gizmos.DrawLine(new Vector3(cctv.transform.position.x, cctv.GetComponentInChildren<CCTVBehavior>().minY, 1), new Vector3(cctv.transform.position.x, cctv.GetComponentInChildren<CCTVBehavior>().maxY, 1));
        }
    }
}
