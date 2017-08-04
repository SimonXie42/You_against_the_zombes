using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public AudioClip FiringSound, EmptyMagSound, ReloadSound, NewFiringSound;
    public GameObject BulletHole, Sparks, BloodBurst;
    public UI Panel;

    private AnimZombie animZ;
    private Ray ray;
    private RaycastHit hit;

    public float FireRate;
    public int Ammo, Max_Ammo, Reloads;
    public int Firepower = 50;
    private float NextFire;

    // Update is called once per frame
    void Update()
    {
        //Shoot 
        if (Input.GetButton("Fire1") && Time.time > NextFire)
        {
            if (Ammo > 0)
            {
                NextFire = Time.time + FireRate;
                Ammo--;

                if (GetComponent<AnimWeapon>().WeaponUpgraded)
                {
                    GetComponent<AudioSource>().volume = 0.2f;
                    GetComponent<AudioSource>().PlayOneShot(NewFiringSound);
                }

                else
                {
                    GetComponent<AudioSource>().PlayOneShot(FiringSound);
                }

                Vector2 ScreenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);       // The crosshair is always at the center of screen no matter where the gun is pointing at.
                ray = Camera.main.ScreenPointToRay(ScreenCenterPoint);

                if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))       // The out keyword is similar to a pass by reference
                {
                    if (hit.transform.gameObject.tag == "Enemy")
                    {
                        GameObject bloodburst = Instantiate(BloodBurst, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                        Destroy(bloodburst, 3f);
                        animZ = hit.transform.gameObject.GetComponent<AnimZombie>();
                        animZ.Health -= Firepower;

                        if (animZ.Health == 0)
                            animZ.Die();
                    }

                    else if (hit.transform.gameObject.tag == "Decor")
                    {
                        // Bullet hole
                        GameObject impact = Instantiate(BulletHole, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;     // To make bullet hole look realistic, i.e. the sprite lays flat anywhere on the hit surface.
                        Destroy(impact, 10f);

                        // Sparks
                        GameObject sparks = Instantiate(Sparks, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;    
                        Destroy(sparks, 3f);
                    }
                }
            }

            else
            {
                GetComponent<AudioSource>().PlayOneShot(EmptyMagSound);
                NextFire = Time.time + FireRate;
            }
        }

        //Reload
        else if (Input.GetKeyDown(KeyCode.R) && Ammo == 0 && Reloads > 0)
        {
            Invoke("FillAmmo", 0.2f);       // Ensure that Ammo is 0 when AnimWeapon.cs checks for reload. 
        }

        Panel.updateUIText(Ammo, Max_Ammo, Reloads);
    }

    private void FillAmmo()
    {
        GetComponent<AudioSource>().PlayOneShot(ReloadSound);
        Ammo = Max_Ammo;
        Reloads--;
    }
}
