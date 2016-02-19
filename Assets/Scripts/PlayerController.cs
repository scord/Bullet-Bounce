using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpPower;
    Rigidbody2D rigidbody;
    Collider2D collider;

    public BulletController bullet;

    bool grounded = false;
    // Use this for initialization
    void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("f"))
        {
            Instantiate(bullet, new Vector3(2f, 0.5f), Quaternion.identity);
        }
	}

    void OnCollisionEnter2D(Collider2D collider)
    {
     
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        
        rigidbody.velocity = new Vector2(speed * moveHorizontal, gameObject.GetComponent<Rigidbody2D>().velocity.y );


        if (grounded && Input.GetButtonDown("Fire1"))
        {
            rigidbody.AddForce(new Vector2(0, jumpPower));
        }

        if (collider.IsTouchingLayers())
            grounded = true;
        else
            grounded = false;


    }

}
