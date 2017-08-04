using UnityEngine;
using System.Collections;

public class RedMark : MonoBehaviour {

    private float time;
    private Color color;

	public void RemoveRedMark() {

        color = gameObject.GetComponent<UnityEngine.UI.Text>().color;
        time = Time.time;

    }

    // Update is called once per frame
    void Update () {

        if (Time.time - time >= 1.5f)
        {
            gameObject.GetComponent<UnityEngine.UI.Text>().color = color;
            gameObject.SetActive(false);
        }

    }
}
