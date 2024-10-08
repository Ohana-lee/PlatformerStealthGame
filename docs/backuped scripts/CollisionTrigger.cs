using UnityEngine;
using System.Collections;

public class CollisionTrigger : MonoBehaviour {


    /// The platform's collider
    public BoxCollider2D platformCollider;


    /// The platform's trigger

    public BoxCollider2D platformTrigger;


    /// The player's collider

    private BoxCollider2D playerCollider;

	// Use this for initialization
	void Start () 
    {
        //Creates a reference to the player's collider
        playerCollider = GameObject.Find("mc").GetComponent<BoxCollider2D>();

        //Makes sure that the collider doesn't collide with itself
        Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);
	}

    ///
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "mc")
        {
            //Ignores the players collider on trigger enter
            Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
        }
   
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "mc")
        {
            //Makes sure that we can collide with the platform after we exit the trigger
            Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
        }
    
    }
}
