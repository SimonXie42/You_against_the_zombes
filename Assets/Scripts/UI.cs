using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Text AmmoText;
    public Text ReloadsText;
    
    public void updateUIText(int Ammo, int Max_Ammo, int Reloads)
    {
        AmmoText.text = "Ammo: " + Ammo.ToString() + "/" + Max_Ammo.ToString();
        ReloadsText.text = "Reloads: " + Reloads.ToString();
    }
}
