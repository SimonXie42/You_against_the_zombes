using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

    public GameObject ZombiesToSpawn;
    public GameObject Exit;

	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().Play();
        ZombiesToSpawn.SetActive(true);
        Exit.GetComponent<Exit>().KeyPickedUp = true;
        Destroy(gameObject, .3f);
    }
}
