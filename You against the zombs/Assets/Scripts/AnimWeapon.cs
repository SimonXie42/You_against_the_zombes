using UnityEngine;
using System.Collections;

public class AnimWeapon : MonoBehaviour {
    
    public Animator animFlame;
    public GameObject WeaponToSwitch;
    public AudioClip Switch;
    private Animator anim;

    public bool WeaponUpgraded = false;
    public bool CanSwitchWeapon = true;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Q) && CanSwitchWeapon)
        {
            WeaponToSwitch.SetActive(true);
            gameObject.SetActive(false);
            WeaponToSwitch.GetComponent<AudioSource>().PlayOneShot(Switch);
        }

        if (Input.GetButton("Fire1") && GetComponent<Shoot>().Ammo > 0)
        {
            anim.SetTrigger("Fire");

            if (WeaponUpgraded)
                animFlame.SetTrigger("EnhancedFlame");
            else
                animFlame.SetTrigger("Flame");
        }

        // Reload
        if (Input.GetKeyDown(KeyCode.R) && GetComponent<Shoot>().Ammo == 0 && GetComponent<Shoot>().Reloads > 0)
            anim.SetTrigger("Reload");

        // Walk
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
            anim.SetBool("Walk", true);
        else
            anim.SetBool("Walk", false);

        // Run
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            anim.SetBool("Run", true);
        else
            anim.SetBool("Run", false);


        if (WeaponUpgraded)
        {
            GetComponent<Shoot>().Firepower = 100;
        }
    }
}
