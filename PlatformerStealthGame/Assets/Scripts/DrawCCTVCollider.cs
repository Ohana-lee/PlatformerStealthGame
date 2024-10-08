using UnityEngine;
using System.Collections.Generic;
public class DrawCCTVCollider : MonoBehaviour
{
    public GameObject linePrefab;
    private LineRenderer[] fillSegments;
    private PolygonCollider2D polygonCollider2D;
    private Vector2[] points;
    public int fillCount = 128;
    public bool active = true;
    
    void Start()
    {
        fillSegments = new LineRenderer[fillCount + 1];
        for (int i = 0; i <= fillCount; i++)
        {
            fillSegments[i] = Instantiate(linePrefab, transform).GetComponent<LineRenderer>();
            fillSegments[i].positionCount = 2;
            fillSegments[i].startWidth = fillSegments[i].endWidth = 0.1f;
            fillSegments[i].loop = false;
        }
        //fillRenderer = Instantiate(linePrefab).GetComponent<LineRenderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }
    void Update()
    {
        if (active)
        {
            HighlightCollider();
        }
    }
    
    void HighlightCollider()
    {
        points = polygonCollider2D.GetPath(0); // get 1st path; which is the only one
        Vector3[] positions = new Vector3[points.Length];
        /*lineRenderer.startColor = lineRenderer.endColor = new Color32(0, 0, 0, 0);
        lineRenderer.startWidth = lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = points.Length;*/
        for (int j = 0; j < points.Length; j++)
        {
            positions[j] = transform.TransformPoint(points[j]);
            // converts from local coordinates to world coordinates
            //lineRenderer.SetPosition(j, positions[j]);
        }
        float ratio = 0f;
        for (int i = 0; ratio <= 1; i++)
        {
            if (i % 2 == 0)
            {
                fillSegments[i].startColor = new Color32(255, 0, 0, 255);
                fillSegments[i].endColor = new Color32(255, 0, 0, 0);
                var fillLineVertex1 = Vector3.Lerp(positions[0], positions[1], ratio);
                var fillLineVertex2 = Vector3.Lerp(positions[3], positions[2], ratio);
                fillSegments[i].SetPosition(0, fillLineVertex1);
                fillSegments[i].SetPosition(1, fillLineVertex2);
            }
            else
            {
                fillSegments[i].startColor = new Color32(255, 0, 0, 0);
                fillSegments[i].endColor = new Color32(255, 0, 0, 255);
                var fillLineVertex1 = Vector3.Lerp(positions[0], positions[1], ratio);
                var fillLineVertex2 = Vector3.Lerp(positions[3], positions[2], ratio);
                fillSegments[i].SetPosition(0, fillLineVertex2);
                fillSegments[i].SetPosition(1, fillLineVertex1);
            }
            ratio += 1f / fillCount;
        }
    }

    public void ToggleCollider()
    {
        if (active)
        {
            active = false;
            for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                if(gameObject.transform.GetChild(i).gameObject != null)
                {
                    Object.Destroy(gameObject.transform.GetChild(i).gameObject);
                }
                
            }
            transform.GetComponent<PolygonCollider2D>().enabled = false;
        }
        else
        {
            active = true;
            transform.GetComponent<PolygonCollider2D>().enabled = true;
        }
    }
}