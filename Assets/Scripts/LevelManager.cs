using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public GameObject level;
    public GameObject turret;

	// Use this for initialization
	void Start () {
        GameObject turretLayer = level.transform.FindChild("Turrets").gameObject;
        foreach (Transform transform in turretLayer.transform)
        {
            Instantiate(turret, transform.position - new Vector3(0,0.62f,0), Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
