using System;
using UnityEngine;
using UnityEngine.UI;

public class LaserButton : MonoBehaviour
{
    public bool inRange = false;
    public string laserName;
    private bool isActive;
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    void Update()
    {
        if (inRange && Input.GetKeyDown("e")) //click e to trigger the button
        {
            //play button pressed sound?
            spriteRenderer.sprite = newSprite;
            ButtonClicked();
        }
    }

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        isActive = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("mc"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("mc"))
        {
            inRange = false;
        }
    }

    void ButtonClicked()
    {
        if (!isActive) return;
        GameObject laser = GameObject.Find(laserName);
        laser.SetActive(false);
        Destroy(laser.GetComponent<BoxCollider2D>());
        Destroy(laser.GetComponent<Rigidbody2D>());
        isActive = false;
    }
}