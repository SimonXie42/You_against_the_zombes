using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* A Note about spawning zombies: I chose not to spawn zombies at designated locations
 * because the zombies will all be spawned in the idle state, which looks
 * like a single zombie from a distance and suddenly splits into multiple when the player 
 * approaches them. Thus, for the sake of being realistic, I think placing zombies in 
 * the scene is the way to go here.
 */

public class AnimZombie : MonoBehaviour {

    public Transform Target;
    public AudioClip slap, moan, ZombieDeath, PlayerDeath;
    public GameObject BloodyVision;
    public RedMark[] DamageIndications;
    public GameObject GameOverScene;
    public Animator PlayerDeathAnim;

    private NavMeshAgent Agent;
    private Animator anim;
    private RedMark[] RedMarksOnScreen;
    private Color color;
    private AudioSource audioSource;

    public int Health = 100;
    public float WalkDist;
    public float AttackDist;

    private float distance;
    private bool HasInvoked = false;
    
	// Use this for initialization
	void Start () {
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {

        if (Target)
        {
            distance = Vector3.Distance(transform.position, Target.position);

            if (distance <= AttackDist)
            {
                anim.SetBool("Attack", true);
                anim.SetBool("Walk", false);
                Agent.SetDestination(transform.position);
            }

            else if (distance <= WalkDist)
            {
                anim.SetBool("Attack", false);
                anim.SetBool("Walk", true);

                if (audioSource.clip != moan)
                {
                    audioSource.clip = moan;
                    audioSource.volume = 0.25f;
                    audioSource.Play();
                    audioSource.loop = true;
                }

                Agent.SetDestination(Target.position);
            }

            else        // If Player is outside the range
            {
                anim.SetBool("Walk", false);
                anim.SetBool("Attack", false);
                Agent.SetDestination(transform.position);
            }

            RedMarksOnScreen = FindObjectsOfType<RedMark>();

            if (RedMarksOnScreen.Length == 3)      
            {
                PlayerDeathAnim.enabled = true;
                audioSource.clip = PlayerDeath;
                audioSource.Play();
                audioSource.loop = false;
                Target = null;
                anim.SetBool("Attack", false);       // One simply doesn't kick a dead dog.
                anim.SetBool("Walk", false);

                if (!HasInvoked)
                {
                    Invoke("LoadGameOverScene", 1f);
                    Invoke("LoadStartMenu", 6f);
                    HasInvoked = true;
                }
            }
        }
           
    } 

    private void LoadGameOverScene()
    {
        GameOverScene.SetActive(true);
    }

    void DamageToPlayer()
    {
        if (distance <= AttackDist)
        {
            audioSource.volume = 0.5f;
            audioSource.PlayOneShot(slap);

            for (int i = 0; i < DamageIndications.Length; i++)
            {
                RedMark redMark = DamageIndications[i];

                if (!redMark.gameObject.activeSelf)         // If Player can still sustain damage
                {
                    redMark.gameObject.SetActive(true);     // Show a red mark on screen
                    redMark.RemoveRedMark();
                    break;
                }
            }

            BloodyVision.SetActive(true);
            Invoke("DeactivateBloodyVision", 1f);       // Let it fade out.
        }
        
    }

    void DeactivateBloodyVision()
    {
        BloodyVision.SetActive(false);
    }
  

    public void Die()
    {
        anim.SetTrigger("Death");
        anim.SetBool("Attack", false);
        
        audioSource.clip = ZombieDeath;         // Don't want the moan sound to interfere with the death sound. 
        audioSource.volume = 0.5f;
        audioSource.Play();
        audioSource.loop = false;

        GetComponent<MeshCollider>().enabled = false;        // To ensure that animation clip is played once only in case of rapid fire repartee on zombie. 
        Agent.enabled = false;       // To prevent zombie from floating towards Player when on the ground.
        this.enabled = false;
        Destroy(gameObject, 2f);
    }

    void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
}
