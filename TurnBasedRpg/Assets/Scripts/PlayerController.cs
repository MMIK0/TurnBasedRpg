using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public float moveSpeed;
    public Animator myAnim;
    public static PlayerController instance;
    public string areaTransitionName;
    private float pressTime;
    public float running;
    private const float pressTimeTolerance = 1f;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    public bool canMove = true;
    void Start()
    {
        if (instance == null) 
        {
            instance = this;
        } else {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
        running = moveSpeed + 10f;
    }

    
    void Update()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        myAnim.SetFloat("moveX", rb.velocity.x);
        myAnim.SetFloat("moveY", rb.velocity.y); 

        if( Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (canMove)
            {
                myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (pressTime < pressTimeTolerance)
            {
                pressTime += Time.deltaTime;
            }
            else
            {
                moveSpeed = running;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            moveSpeed = running - 10f;
            pressTime = 0f;
        }



            //keep the player in the bounds
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);

    }
    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(0.5f,0.5f,0f);
        topRightLimit = topRight + new Vector3 (-0.5f,-0.5f,0f);
    }

}

