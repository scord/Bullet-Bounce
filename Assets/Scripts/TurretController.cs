using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {

    //public GameObject gun;
    public BulletController bullet;
    public float repeatRate;
	// Use this for initialization
	void Start () {
        //gun = transform.Find("Gun").gameObject;

        // gun.GetComponent<Animator>().Play("Idle");

        InvokeRepeating("Shoot", 2f, repeatRate);
	}

    void Shoot ()
    {
        //gun.GetComponent<Animator>().Play("Turret Animation");
        Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
	void Update ()
    {
        
	}
}
