using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {

    public GameObject gun;
    public BulletController bullet;
    public float repeatRate;
    public Vector3 direction;
    public bool followTarget;
    GameObject player;
    // Use this for initialization
    void Start () {
        gun = transform.Find("Gun").gameObject;

        gun.GetComponent<Animator>().Play("Idle");
        player = GameObject.FindWithTag("Player");
        InvokeRepeating("Shoot", 2f, repeatRate);
	}

    void Shoot ()
    {
        gun.GetComponent<Animator>().Play("Turret Animation");
        //gun.GetComponent<Animator>().Play("Turret Animation");
        Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
        if (followTarget)
            bullet.dir = Vector3.Normalize(player.transform.position - gameObject.transform.position);
        else
            bullet.dir = direction;
    }

    // Update is called once per frame
	void Update ()
    {
        if (followTarget)
        {
            direction = Vector3.Cross(GameObject.FindGameObjectWithTag("Player").transform.position - gameObject.transform.position, Vector3.forward);
            gun.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }

    public bool getFollowTarget()
    {
        return getFollowTarget();
    }
    
}
