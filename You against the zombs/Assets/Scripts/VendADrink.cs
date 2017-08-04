using UnityEngine;
using System.Collections;

public class VendADrink : MonoBehaviour {

    public Transform Target;
    public GameObject Hint;
    public GameObject Drink;
    public GameObject PrimaryWeaponSetup;
    public GameObject SecondaryWeaponSetup;
    public GameObject PerkIcon;
    private GameObject WeaponBeforeDrink;

    private float distance;
    private bool HadADrink = false;


    // Update is called once per frame
    void Update () {

        distance = Vector3.Distance(transform.position, Target.position);

        if (distance < 3.5f && !HadADrink)
        {
            Hint.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (SecondaryWeaponSetup.activeSelf)
                {
                    SecondaryWeaponSetup.SetActive(false);
                    WeaponBeforeDrink = SecondaryWeaponSetup;
                }

                else
                {
                    PrimaryWeaponSetup.SetActive(false);
                    WeaponBeforeDrink = PrimaryWeaponSetup;
                }

                Drink.SetActive(true);
                Hint.SetActive(false);
                Drink.GetComponent<SoundEffect>().PlayDrinkSound();
                HadADrink = true;
                Invoke("ShowPerkIconAndWeapon", 2.5f);
            }
        }

        else
            Hint.SetActive(false);

    }

    void ShowPerkIconAndWeapon()
    {
        Drink.SetActive(false);
        PerkIcon.SetActive(true);
        WeaponBeforeDrink.SetActive(true);
    }
}
