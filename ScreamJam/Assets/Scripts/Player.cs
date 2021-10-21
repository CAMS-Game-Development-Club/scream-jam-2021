using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance {
        get {
            if (_instance == null) {
                Debug.LogError("Playerattack is null");
            }
            return _instance;
        } private set { }
    }

    public float health = 3f;
    public int candy_collected = 0;

    [SerializeField]
    private float cooldownSeconds = 3;
    private float secondsSinceLastAttack = 3;

    [SerializeField]
    private float hitInvincibility = 0.5f;
    private float invincibilityTimePassed = 0.5f;

    private bool mouseAlreadyClicked = false;

    [SerializeField]
    private PlayerAnimations anim;

    public Rigidbody2D rb2d { get; private set; }

    private bool dead = false;

    private void Awake() { //Initialize Variables
        _instance = this;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        invincibilityTimePassed = hitInvincibility;
    }
    
    private void Update() {
        //Update attack and invincibility timers
        if (secondsSinceLastAttack < cooldownSeconds) {
            secondsSinceLastAttack += Time.deltaTime;
        }
        if (invincibilityTimePassed < hitInvincibility) {
            invincibilityTimePassed += Time.deltaTime;
        }
        Attack();
    }

    private void Attack() {
        if (Input.GetMouseButtonDown(0)) {
            if (!mouseAlreadyClicked) {
                mouseAlreadyClicked = true; //Prevent input from being detected twice
                if (secondsSinceLastAttack >= cooldownSeconds) {
                    anim.SetAnimBool("IsAttacking", true);
                    StartCoroutine(endAttack());
                    secondsSinceLastAttack = 0f;
                }
            }
        } else {
            mouseAlreadyClicked = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) { //Detects Collisions
        if (invincibilityTimePassed !< cooldownSeconds) {
            if (collision.tag == "EnemyWeapon") { // Check if collided object is an enemy weapon hitbox
                // Deal damage
                health -= collision.GetComponentInChildren<Weapon>().weaponDamage;
                anim.DamageAnimation();
                invincibilityTimePassed = 0;
            } else if (collision.tag == "Candy") {
                Destroy(collision.gameObject);
                candy_collected += 1;
                HUD.Instance.UpdateScore();
            }
        }
        
        if (health <= 0) { //Check for player death

            if (dead == false) { // Prevent Lose from being called multiple times
                anim.SetAnimBool("IsDead", true);
                dead = true;
            }
        }
    }

    private IEnumerator endAttack() {
        yield return new WaitForSeconds(0.5f); // Hard-coded as finding whether the animation has ended is slow
        anim.SetAnimBool("IsAttacking", false);
    }
}
