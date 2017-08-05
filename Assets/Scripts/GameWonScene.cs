using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameWonScene : MonoBehaviour
{
    public GameObject Remark;

    private Image fadePanel;
    private Color currentColor;

    private float fadeOutTime = 2f;

    // Use this for initialization
    void Start()
    {
        fadePanel = GetComponent<Image>();
        currentColor = fadePanel.color;     // To handle the "cannot modify the return value of graphic.color" error.
        Invoke("LoadStartMenu", fadeOutTime + 2f); 
    }

    // Update is called once per frame
    void Update()
    {
        float alphaChange = Time.deltaTime / fadeOutTime;
        currentColor.a += alphaChange;
        fadePanel.color = currentColor;

        if (fadePanel.color.a >= 1.5f)
            Remark.SetActive(true);
    }

    void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
}