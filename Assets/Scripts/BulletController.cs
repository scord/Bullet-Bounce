using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    const int MAX_DISTANCE = 30;
    public float speed;
    Collider2D coll;
    GameObject player;
    Vector3 dir;

    // Use this for initialization
    void Start () {
        coll = gameObject.GetComponent<Collider2D>();
        player = GameObject.FindWithTag("Player");
        dir = Vector3.Normalize(player.transform.position - gameObject.transform.position);
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), dir, speed*Time.deltaTime);
        if (hit.collider != null)
        {
            print(hit.collider.name.ToString());
            TriggerHit(hit.collider);   
        }
        gameObject.transform.Translate(dir * speed);
        if (gameObject.transform.position.magnitude > MAX_DISTANCE)
        {
            Destroy(gameObject);
        }
    }

    //void OnTriggerEnter2D(Collider2D coll)
    //{
    //    if (coll.gameObject.tag == "Shield")
    //    {
    //        print("hitold");
    //        dir = -dir;
    //    }
    //}

    void TriggerHit(Collider2D coll)
    {
        if (coll.gameObject.tag == "Shield")
        {
            print("hitnew");
            dir = -dir;
        }
    }

}
