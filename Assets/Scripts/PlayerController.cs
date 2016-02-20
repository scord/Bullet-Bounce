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

	}


    void FixedUpdate()
    {
        string[] gamepads = Input.GetJoystickNames();
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        

        rigidbody.velocity = new Vector2(speed * moveHorizontal, gameObject.GetComponent<Rigidbody2D>().velocity.y );
        //sprite.transform.Rotate(new Vector3(0, 0, -2*speed * moveHorizontal));

        if (moveHorizontal != 0)
            rigidbody.velocity = new Vector2(speed * moveHorizontal, gameObject.GetComponent<Rigidbody2D>().velocity.y );
        sprite.transform.Rotate(new Vector3(0, 0, -2*speed * moveHorizontal));

        if (collider.IsTouchingLayers())
            grounded = true;
        else
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
            print(angle);
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

    }

}