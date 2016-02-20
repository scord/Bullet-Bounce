using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

    bool open = false;
    float openHeight = 0;
    Vector3 origin;
	// Use this for initialization
	void Start () {
        origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if (open && openHeight < 2.0f)
            openHeight += 0.1f;

        transform.position = origin + new Vector3(0, openHeight, 0);

    }


    public void Open()
    {
        open = true;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
