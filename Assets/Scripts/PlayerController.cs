using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public BulletController bullet;

    public float speed;
    public float jumpPower;
    Rigidbody2D rigidbody;
    Collider2D collider;
    public Camera camera;
    public GameObject shield;
    bool grounded = false;
    public GameObject sprite;
    float timer = 0f;
    float bounceTimer = 10f;
    Vector2 acceleration = Vector2.zero;
    Vector2 decceleration = new Vector2(0.5f,0);
    // Use this for initialization
    void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<Collider2D>();
       
    }
	
	// Update is called once per frame
	void Update () {

	}
   
    void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = false;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (gameObject.transform.position.y > contact.point.y)
                grounded = true;
        } 
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        grounded = false;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (gameObject.transform.position.y > contact.point.y)
                grounded = true;
        }
    }


    void FixedUpdate()
    {
        string[] gamepads = Input.GetJoystickNames();
        
        float moveHorizontal = Input.GetAxis("Horizontal");

        camera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
        
        if (grounded || Mathf.Abs(moveHorizontal) >= 0.0)
            acceleration = new Vector2(moveHorizontal*(bounceTimer), 0 );
        
        if (grounded && Mathf.Abs(moveHorizontal) <= 0.1)
        {
            if (rigidbody.velocity.x < 0)
            {
                rigidbody.velocity += decceleration*bounceTimer;

                if (rigidbody.velocity.x > 0)
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            }
            if (rigidbody.velocity.x > 0)
            {
                rigidbody.velocity -= decceleration*bounceTimer;
                if (rigidbody.velocity.x < 0)
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            }
        }

        rigidbody.velocity += acceleration/2;

        if (rigidbody.velocity.x > speed)
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
        else if (rigidbody.velocity.x < -speed)
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);

        sprite.transform.Rotate(new Vector3(0, 0, -2*speed * moveHorizontal));
        
        if (!collider.IsTouchingLayers())
            grounded = false;

        if (grounded && Input.GetAxis("Fire1") > 0.5f)
        {
            rigidbody.velocity = (new Vector2(0, jumpPower));
        }

        if (shield.GetComponent<ShieldController>().collision)
        {
            Vector3 rot = shield.gameObject.transform.up;
            //Debug.Log(rot);
            
            rigidbody.velocity = (rot*jumpPower*2f);
            bounceTimer = 0;
        }

        if (gamepads.Length > 0)
        {
            float moveShieldX = Input.GetAxis("RightX");
            float moveShieldY = Input.GetAxis("RightY");

            if (Mathf.Abs(moveShieldX) + Mathf.Abs(moveShieldY) >= 1)
            {
                Vector3 direction = new Vector3(-moveShieldX, moveShieldY, 0);
                Quaternion rot = Quaternion.LookRotation(Vector3.forward, direction);
                if (!grounded)
                    shield.transform.rotation = rot;
                else if (rot.eulerAngles.z < 90)
                    shield.transform.rotation = Quaternion.Euler(0, 0, 90);
                else if (rot.eulerAngles.z > 270)
                    shield.transform.rotation = Quaternion.Euler(0, 0, 270);
                else
                    shield.transform.rotation = rot;

                Debug.Log(Quaternion.LookRotation(Vector3.forward, direction).eulerAngles);
            }
        }
        else
        {
            Vector3 mousePos = Input.mousePosition;
            float mousex = mousePos.x;
            float mousey = mousePos.y;

            Vector3 playerPos = Camera.main.WorldToScreenPoint(gameObject.transform.localPosition);
            mousePos.z = 0f;
            mousePos.x = mousex - playerPos.x;
            mousePos.y = mousey - playerPos.y;
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg + 90;
            Quaternion rotate = Quaternion.Euler(new Vector3(0, 0, angle));
            if (!grounded)
                shield.transform.rotation = rotate;
            else if (angle < 90 && angle > 0) 
                shield.transform.rotation = Quaternion.Euler(0, 0, 90);
            else if (angle > 270 || angle < 0)
                shield.transform.rotation = Quaternion.Euler(0, 0, 270);
            else
                shield.transform.rotation = rotate;
        }
        if (Input.GetKeyDown("f"))
        {
            Instantiate(bullet, new Vector3(2f, 0.5f), Quaternion.identity);
        }

        if (bounceTimer < 10f)
            bounceTimer += Time.fixedDeltaTime;

        if (Input.GetKeyDown("r"))
        {
            resetPlayer();
        }

 

    }

    void resetPlayer()
    {
        gameObject.transform.position = new Vector3(1, 0.5f, 1.23f);
    }
}