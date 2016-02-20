using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public BulletController bullet;

    public float speed;
    public float jumpPower;
    Rigidbody2D rigidbody;
    Collider2D collider;
    public GameObject shield;
    bool grounded = false;
    public GameObject sprite;
    float timer = 0f;
    // Use this for initialization
    void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<Collider2D>();
       
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if (timer > 1)
        {
            timer = 0f;
            //Instantiate(bullet, new Vector3(2f, 0.5f), Quaternion.identity);
        }

	}


    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveShieldX = Input.GetAxis("RightX");
        float moveShieldY = Input.GetAxis("RightY");
        
        Vector3 mousePos = Input.mousePosition;
        float mousex = mousePos.x;
        float mousey = mousePos.y;

        rigidbody.velocity = new Vector2(speed * moveHorizontal, gameObject.GetComponent<Rigidbody2D>().velocity.y );
        sprite.transform.Rotate(new Vector3(0, 0, -2*speed * moveHorizontal));
        if (collider.IsTouchingLayers())
            grounded = true;
        else
            grounded = false;

        if (grounded && Input.GetAxis("Fire1") > 0.5f)
        {
            rigidbody.AddForce(new Vector2(0, jumpPower));
        }

        if (shield.GetComponent<ShieldController>().collision)
        {
                
            Vector3 rot = shield.gameObject.transform.up;
            //Debug.Log(rot);
            
            rigidbody.AddForce(rot*jumpPower);
        }

        if (Mathf.Abs(moveShieldX) + Mathf.Abs(moveShieldY) >= 1)
        {
            Vector3 direction = new Vector3(-moveShieldX, moveShieldY, 0);
            shield.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
        /*
        Vector3 playerPos = Camera.main.WorldToScreenPoint(gameObject.transform.localPosition);
        mousePos.z = 0f;
        mousePos.x = mousex - playerPos.x;
        mousePos.y = mousey - playerPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        shield.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));
        */
        if (Input.GetKeyDown("f"))
        {

            Instantiate(bullet, new Vector3(2f, 0.5f), Quaternion.identity);
        }

    }

}