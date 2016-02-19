using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    public float speed;
    Collider2D coll;
    GameObject player;
    Vector3 dir;

    // Use this for initialization
    void Start () {
        print("bullet created");
        coll = gameObject.GetComponent<Collider2D>();
        player = GameObject.FindWithTag("Player");
        dir = Vector3.Normalize(player.transform.position - gameObject.transform.position);
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        gameObject.transform.Translate(dir * speed / 10f);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        print("test");
        if (coll.gameObject.tag == "Player")
        {
            print("hit");
            dir = -dir;
        }
    }

}
