using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

    public GameObject GameWonScene;
    public bool KeyPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if (KeyPickedUp)
            GameWonScene.SetActive(true);
    }
}
