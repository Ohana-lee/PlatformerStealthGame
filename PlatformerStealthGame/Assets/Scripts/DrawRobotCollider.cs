using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class DrawRobotCollider : MonoBehaviour
{
    public GameObject linePrefab;
    private LineRenderer[] fillSegments;
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider2D;
    private Vector2[] points = new Vector2[4];
    private Vector2[] colliderPoints = new Vector2[4];
    public int fillCount = 64;
    private bool right = false;
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
        polygonCollider2D = GetComponentInParent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (spriteRenderer.sprite.name == "spritesheets1_6")
        {
            points[0] = new Vector2(transform.position.x - 0.4941696f, transform.position.y + 1.042271f);
            points[1] = new Vector2(transform.position.x - 0.4941696f, transform.position.y + 0.3609848f);
            points[2] = new Vector2(transform.position.x - 3.827797f, transform.position.y - 0.5711975f);
            points[3] = new Vector2(transform.position.x - 3.827797f, transform.position.y + 1.974081f);
        }
        else if (spriteRenderer.sprite.name == "spritesheets1_5")
        {
            points[0] = new Vector2(transform.position.x - 0.4941696f + 0.026f, transform.position.y + 1.042271f + 0.177f);
            points[1] = new Vector2(transform.position.x - 0.4941696f + 0.026f, transform.position.y + 0.3609848f + 0.177f);
            points[2] = new Vector2(transform.position.x - 3.827797f + 0.026f, transform.position.y - 0.5711975f + 0.177f);
            points[3] = new Vector2(transform.position.x - 3.827797f + 0.026f, transform.position.y + 1.974081f + 0.177f);
            polygonCollider2D.offset = new Vector2(0.09f, 0.578f);
        }
        else if (spriteRenderer.sprite.name == "spritesheets1_0")
        {
            points[0] = new Vector2(transform.position.x - 0.4941696f + 0.055f, transform.position.y + 1.042271f + 0.11f);
            points[1] = new Vector2(transform.position.x - 0.4941696f + 0.055f, transform.position.y + 0.3609848f + 0.11f);
            points[2] = new Vector2(transform.position.x - 3.827797f + 0.055f, transform.position.y - 0.5711975f + 0.11f);
            points[3] = new Vector2(transform.position.x - 3.827797f + 0.055f, transform.position.y + 1.974081f + 0.11f);
            polygonCollider2D.offset = new Vector2(0.185f, 0.37f);
        }
        if (spriteRenderer.flipX)
        {
            points[0] = new Vector2(points[0].x + 0.99f, points[0].y);
            points[1] = new Vector2(points[1].x + 0.99f, points[1].y);
            points[2] = new Vector2(points[2].x + 7.656f, points[2].y);
            points[3] = new Vector2(points[3].x + 7.656f, points[3].y);
        }

        //polygonCollider2D.SetPath(0, points);

        Vector3[] positions = new Vector3[points.Length];
        for (int j = 0; j < points.Length; j++)
        {
            positions[j] = new Vector3(points[j].x, points[j].y, 1);
            // converts from local coordinates to world coordinates
        }
        float ratio = 0f;
        for (int i = 0; ratio <= 1; i++)
        {
            if (i % 2 == 0)
            {
                fillSegments[i].startColor = new Color32(241, 228, 166, 255);
                fillSegments[i].endColor = new Color32(241, 228, 166, 0);
                var fillLineVertex1 = Vector3.Lerp(positions[0], positions[1], ratio);
                var fillLineVertex2 = Vector3.Lerp(positions[3], positions[2], ratio);
                fillSegments[i].SetPosition(0, fillLineVertex1);
                fillSegments[i].SetPosition(1, fillLineVertex2);
            }
            else
            {
                fillSegments[i].startColor = new Color32(241, 228, 166, 0);
                fillSegments[i].endColor = new Color32(241, 228, 166, 255);
                var fillLineVertex1 = Vector3.Lerp(positions[0], positions[1], ratio);
                var fillLineVertex2 = Vector3.Lerp(positions[3], positions[2], ratio);
                fillSegments[i].SetPosition(0, fillLineVertex2);
                fillSegments[i].SetPosition(1, fillLineVertex1);
            }
            ratio += 1f / fillCount;
        }
    }
    public void SetColliderRight(bool _right)
    {
        if (_right != right && polygonCollider2D)
        {
            colliderPoints[0] = new Vector2(polygonCollider2D.GetPath(0)[0].x * -1, polygonCollider2D.GetPath(0)[0].y);
            colliderPoints[1] = new Vector2(polygonCollider2D.GetPath(0)[1].x * -1, polygonCollider2D.GetPath(0)[1].y);
            colliderPoints[2] = new Vector2(polygonCollider2D.GetPath(0)[2].x * -1, polygonCollider2D.GetPath(0)[2].y);
            colliderPoints[3] = new Vector2(polygonCollider2D.GetPath(0)[3].x * -1, polygonCollider2D.GetPath(0)[3].y);
            polygonCollider2D.SetPath(0, colliderPoints);
        }
        right = _right;
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
                //Object.Destroy(gameObject.transform.GetChild(i).gameObject);
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