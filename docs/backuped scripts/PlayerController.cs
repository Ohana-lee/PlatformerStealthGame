using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed; // adds movementSpeed field to the script's component on the Unity editor
    [SerializeField] private Animator anim;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float gravityOfPlayer;
    [SerializeField] private float gravityScale;

    //public GameObject gameObject;

    private Vector2 movementDirection;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    //private bool isGrounded = true;
    //private float dirX = 0f;
    private float velocity;
    //private float old_pos;
    SpriteRenderer spi;

    public TextMeshPro timer;
    public static float currentTime = 0f;
    public static float startingTime = 50f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        currentTime = startingTime;
        timer.color = new Color32(255, 69, 0, 252);
        timer.text = currentTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        processDirection();
        processAnimation();
        //dirX = Input.GetAxisRaw("Horizontal");
        //rb.velocity = new Vector2(dirX * movementSpeed, rb.velocity.y);

        velocity += gravityOfPlayer * gravityScale * Time.deltaTime;
        if (isPlayerGrounded() && velocity < 0)
        {
            anim.SetBool("Jump", false);
            velocity = 0;
        }
        if (Input.GetButtonDown("Jump") && isPlayerGrounded())
        {
            anim.SetBool("Jump", true);
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //velocity = 0;
            velocity = jumpForce;
        }
        /*if ((Input.GetAxisRaw("Horizontal") < 0) && (velocity == 0))
          {
              spi.flipX = true;
          }*/
        anim.SetFloat("yVelocity", rb.velocity.y);
        transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);




        //int spacePress = 0;
        // add ISGROUNDED checks !!
        /*isGrounded = isPlayerGrounded();
        print("isGrounded: "+isGrounded); //idk why but it is forever false
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)//spacePress == 0)
        {
          //spacePress++;
          isGrounded = false;
          rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
          //spacePress--;
        }*/

        currentTime -= 1 * Time.deltaTime;
        timer.text = ((int)currentTime).ToString();
        if (currentTime <= 0)
        {
            //Application.Quit();
            //EditorApplication.isPlaying=false;
            SceneManager.LoadScene(6);
        }
    }

    // FixedUpdate runs depending on how many physics fps are set in the time settings & how fast/slow the framerate is
    private void FixedUpdate()
    {
        move();
    }

    private bool isPlayerGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 1f, jumpableGround);
    }

    private void processDirection()
    {
        //movementDirection =  new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
    }

    private void move()
    {
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
        // rb.AddForce(movementDirection.normalized * movementSpeed * Time.deltaTime, ForceMode2D.Force);
        // rb.velocity = movementDirection.normalized * movementSpeed * Time.deltaTime; // normalized: makes movement in all directions have the same speed
    }

    private void processAnimation()
    {
        anim.SetFloat("Horizontal", movementDirection.x);
        //anim.SetFloat("Vertical", movementDirection.y);
    }
}