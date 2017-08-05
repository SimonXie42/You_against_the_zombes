using UnityEngine;
using System.Collections;

public class WeaponUpgrade : MonoBehaviour {

    public GameObject Hint2;
    public GameObject Hint3;
    public GameObject PriWeaponSetup;
    public GameObject SecWeaponSetup;
    public GameObject M4A1;
    public GameObject _1911;
    public Material M4A1Camouflage;
    public Material _1911Camouflage;
    public GameObject[] effects;
    public AnimWeapon animWeapon;

    private Material[] mats;
    private AnimWeapon[] Weapons;

    private bool AboutToUpgrade = false;
    private bool CanCollectWeapon = false;
    private bool PlayerWalkedAway = false;


    private void OnTriggerStay(Collider other)
    {
        if (!PriWeaponSetup.GetComponent<AnimWeapon>().WeaponUpgraded || !SecWeaponSetup.GetComponent<AnimWeapon>().WeaponUpgraded)   
        {
            if (!AboutToUpgrade)
                Hint2.SetActive(true);

            if (CanCollectWeapon)
                Hint3.SetActive(true);

            // If Player initiates weapon upgrade. The second parameter handles the case of Player pressing key E multiple times, which yields undesirable behavior due to the onTriggerStay().
            if (Input.GetKeyDown(KeyCode.E) && !AboutToUpgrade)        
            {
                Hint2.SetActive(false);

                if (PriWeaponSetup.activeSelf && !PriWeaponSetup.GetComponent<AnimWeapon>().WeaponUpgraded)
                {
                    PriWeaponSetup.SetActive(false);
                    SecWeaponSetup.SetActive(true);
                    M4A1.SetActive(true);
                    _1911.SetActive(false);
                    AboutToUpgrade = true;
                    SecWeaponSetup.GetComponent<AnimWeapon>().CanSwitchWeapon = false;      // Once the upgrade has started, Player can't switch weapon until the upgraded weapon is collected.
                    Invoke("UpgradeWeapon", .5f);
                    Invoke("DoneUpgradeWeapon", 2.5f);
                }

                else if (SecWeaponSetup.activeSelf && !SecWeaponSetup.GetComponent<AnimWeapon>().WeaponUpgraded)
                {
                    PriWeaponSetup.SetActive(true);
                    SecWeaponSetup.SetActive(false);
                    _1911.SetActive(true);
                    M4A1.SetActive(false);
                    AboutToUpgrade = true;
                    PriWeaponSetup.GetComponent<AnimWeapon>().CanSwitchWeapon = false;
                    Invoke("UpgradeWeapon", .5f);
                    Invoke("DoneUpgradeWeapon", 2.5f);
                }

                // Else upgrade is disallowed as any weapon can only be upgraded once.
            }

            if (Input.GetKeyDown(KeyCode.R) && CanCollectWeapon)        // If Player collects the weapon
            {
                GameObject UpgradedWeapon = GameObject.Find("M4A1");

                if (UpgradedWeapon)
                {
                    M4A1.SetActive(false);
                    PriWeaponSetup.SetActive(true);
                    SecWeaponSetup.SetActive(false);
                    mats = PriWeaponSetup.GetComponentInChildren<MeshRenderer>().materials;     // Direct material assignment doesn't work
                    mats[0] = M4A1Camouflage;
                    PriWeaponSetup.GetComponentInChildren<MeshRenderer>().materials = mats;
                    PriWeaponSetup.GetComponent<AnimWeapon>().WeaponUpgraded = true;
                    PriWeaponSetup.GetComponent<AnimWeapon>().CanSwitchWeapon = true;
                    SecWeaponSetup.GetComponent<AnimWeapon>().CanSwitchWeapon = true;
                }

                else
                {
                    _1911.SetActive(false);
                    SecWeaponSetup.SetActive(true);
                    PriWeaponSetup.SetActive(false);
                    mats = SecWeaponSetup.GetComponentInChildren<MeshRenderer>().materials;     
                    mats[0] = _1911Camouflage;
                    SecWeaponSetup.GetComponentInChildren<MeshRenderer>().materials = mats;
                    SecWeaponSetup.GetComponent<AnimWeapon>().WeaponUpgraded = true;
                    SecWeaponSetup.GetComponent<AnimWeapon>().CanSwitchWeapon = true;
                    PriWeaponSetup.GetComponent<AnimWeapon>().CanSwitchWeapon = true;

                }

                AboutToUpgrade = false;
                CanCollectWeapon = false;
                Hint3.SetActive(false);
            }

        }
        
        else
        {
            Hint2.SetActive(false);
        }
    }

    private void UpgradeWeapon()
    {
        foreach (GameObject effect in effects)
        {
            effect.SetActive(true);
        }

        GetComponent<AudioSource>().Play();
    }

    private void DoneUpgradeWeapon()
    {
        foreach (GameObject effect in effects)
        {
            effect.SetActive(false);
        }

        CanCollectWeapon = true;

        if (!PlayerWalkedAway)
            Hint3.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Hint2.SetActive(false);
        Hint3.SetActive(false);

        if (!CanCollectWeapon)      // To deal with the case of Player walks away before the upgrade is finished, which gives undesirable behavior i.e. causes hint3 to stay active while Player is away.
            PlayerWalkedAway = true;
    }
}
