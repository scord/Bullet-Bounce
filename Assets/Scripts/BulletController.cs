using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    const int MAX_DISTANCE = 20;
    public float speed;
    Collider2D coll;
    GameObject player;
    public Vector3 dir;
    GameObject shield;
    EdgeCollider2D shieldEdge;
    PlayerController playerControl;
    Rigidbody2D playerRigid;

    public bool followTarget;
    float hitTimer = 0f;
    bool canHit = true;
    // Use this for initialization
    void Start () {
        coll = gameObject.GetComponent<Collider2D>();

        player = GameObject.FindWithTag("Player");
        shield = GameObject.FindWithTag("Shield");
        
        if (player)
        {
            playerRigid = player.GetComponent<Rigidbody2D>();


            playerControl = player.GetComponent<PlayerController>();
            shieldEdge = shield.GetComponent<EdgeCollider2D>();
        }
        else
        {
            dir = Vector3.right;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!canHit)
        {
            hitTimer += Time.deltaTime;
            if (hitTimer > 0.5f)
            {
                hitTimer = 0f;
                canHit = true;
            }
        }
    }

    void FixedUpdate()
    {
        float dotp = Vector3.Dot(playerRigid.velocity, dir);
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, dir, speed * 3);
            
        RaycastHit2D hitside1 = Physics2D.Raycast(gameObject.transform.position, Vector3.Cross(dir, Vector3.forward), speed + Mathf.Abs(playerRigid.velocity.y)*Time.fixedDeltaTime);
        RaycastHit2D hitside2 = Physics2D.Raycast(gameObject.transform.position, Vector3.Cross(dir, Vector3.back), speed);
        if (hit.collider != null)
        {
            bulletCollision(hit, "front");
        }
        if (hitside1.collider != null)
        {
            bulletCollision(hitside1, "above");
        }
        if (hitside2.collider != null)
        {
            bulletCollision(hitside2, "below");
        }
        gameObject.transform.Translate(dir * speed);
    }

    void bulletCollision(RaycastHit2D hit, string str)
    {
        if (hit.collider.gameObject.tag == "Shield")
        {
            if (canHit)
            {
                if (str == "front")
                {
                    dir = Vector3.Reflect(dir, hit.normal);
                    float angle = Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg;

                    if (angle >= 300 || angle <= -60)
                        playerRigid.velocity = (-dir * playerControl.jumpPower * 2f);
                    hit.collider.gameObject.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
                    hit.collider.gameObject.GetComponent<AudioSource>().Play();
                }
                else if (str == "above")
                {
                    Vector3 rot = gameObject.transform.up;
                    playerRigid.velocity = (rot * playerControl.jumpPower * 2f);
                    dir = Quaternion.Euler(0, 0, 45) * dir;
                }
                else if (str == "below")
                {
                    //playerRigid.velocity = (-dir * playerControl.jumpPower * 2f);
                    dir = Quaternion.Euler(0, 0, -45) * dir;
                }
                canHit = false;
            }
        }
        else if (hit.collider.gameObject.tag == "Floor" && str != "front")
        {
            Destroy(gameObject);
        }
        else if (hit.collider.gameObject.tag == "Power" && str != "front")
        {
            hit.collider.gameObject.GetComponent<PowerBoxController>().Destroy();
            Destroy(gameObject);
        }
        else if (hit.collider.gameObject.tag == "Player" && playerControl.getKillable() && str != "front")
        {
            hit.collider.gameObject.GetComponent<PlayerController>().Destroy();
            Destroy(gameObject);
        }
    }
}
