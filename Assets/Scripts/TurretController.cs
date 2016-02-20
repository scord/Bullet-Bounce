using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {

    public GameObject gun;
    public BulletController bullet;
    public float repeatRate;
    public Vector3 direction =  new Vector3(-1,0,0);
    public bool followTarget;
    GameObject player;
    bool disabled = false;
    float disabledTimer;
    // Use this for initialization
    void Start () {
        gun = transform.Find("Gun").gameObject;

        gun.GetComponent<Animator>().Play("Idle");
        player = GameObject.FindWithTag("Player");
        InvokeRepeating("Shoot", 2f, repeatRate);
	}

    void Shoot ()
    {
        if (!disabled)
        {
            gun.GetComponent<Animator>().Play("Turret Animation");
            //gun.GetComponent<Animator>().Play("Turret Animation");
            if (followTarget)
                bullet.dir = Vector3.Normalize(player.transform.position - gameObject.transform.position);
            else
                bullet.dir = direction;
            Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
        }
        else
        {
            //play disabled animation
        }
    }

    // Update is called once per frame
	void Update ()
    {
        if (followTarget)
        {
            direction = Vector3.Cross(GameObject.FindGameObjectWithTag("Player").transform.position - gameObject.transform.position, Vector3.forward);
            gun.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
        if (disabled)
        {
            disabledTimer += Time.deltaTime;
            if (disabledTimer >= 5f)
            {
                disabledTimer = 0;
                disabled = false;
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player" && coll.transform.position.y > gameObject.transform.position.y)
        {
            disabled = true;
        }
    }
    
}
