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
        
    }

    void FixedUpdate()
    {
        float dotp = Vector3.Dot(playerRigid.velocity, dir);
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, dir, speed*3);

        RaycastHit2D hitside1 = Physics2D.Raycast(gameObject.transform.position, Vector3.Cross(dir, Vector3.forward), speed);
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
        Debug.Log(hit.collider.ToString());
        if (hit.collider.gameObject.tag == "Shield")
        {
           
            if (str == "front") {
                dir = Vector3.Reflect(dir, hit.normal);
                float angle = Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg;
                print(angle);
                if (angle >= 315 || angle <= -45)
                    playerRigid.velocity = (-dir * playerControl.jumpPower * 2f);
                hit.collider.gameObject.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
                hit.collider.gameObject.GetComponent<AudioSource>().Play();
            }
            else if (str == "above")
            {
                playerRigid.velocity = (-dir * playerControl.jumpPower * 2f);
                dir = Quaternion.Euler(0, 0, 45) * dir;
            }
            else if (str == "below")
            {


                hit.collider.gameObject.GetComponent<PowerBoxController>().Destroy();
                Destroy(gameObject);
            }
            if (hit.collider.gameObject.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<PlayerController>().Destroy();
                Destroy(gameObject);

            }
        }
        else if (hit.collider.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
        else if (hit.collider.gameObject.tag == "Power")
        {
            hit.collider.gameObject.GetComponent<PowerBoxController>().Destroy();
            Destroy(gameObject);
        }
        else if (hit.collider.gameObject.tag == "Player" && playerControl.getKillable())
        {
            hit.collider.gameObject.GetComponent<PlayerController>().Destroy();
            Destroy(gameObject);
        }
    }
}
