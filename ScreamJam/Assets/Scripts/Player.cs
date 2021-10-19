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
    }

    private void OnTriggerEnter2D(Collider2D collision) { //Detects Collisions
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "EnemyWeapon") { // Check if collided object is an enemy weapon hitbox
            // Deal damage
            health -= collidedObject.GetComponent<Weapon>().weaponDamage;
            StartCoroutine(damageAnimation());
            invincibilityTimePassed = 0;
        } else if (collidedObject.tag == "Candy") {
            Destroy(collidedObject.gameObject);
            candy_collected += 1;
            HUD.Instance.UpdateScore();
        }
        
        if (health <= 0) { //Check for player death

            if (dead == false) { // Prevent Lose from being called multiple times
                // GameManager.Instance.Lose();
                dead = true;
            }
        }
    }
    
    private IEnumerator damageAnimation() { //Sprite flashes to black 2 times during hitInvincibility
        SpriteRenderer spriteR = gameObject.GetComponentInChildren<SpriteRenderer>();
        Color originalColor = spriteR.color;
        for (int j = 0; j < 2; j++) { 
            for (float i = 1; i > 0.5f; i += -0.5f / ((hitInvincibility / 4) * 60) ) {
                spriteR.color = new Color(i * originalColor.r, i * originalColor.g, i * originalColor.b, spriteR.color.a);
                yield return new WaitForSeconds(0.016f);
            }
            for (float i = 0.5f; i < 1; i += 0.5f / ((hitInvincibility / 4) * 60)) {
                spriteR.color = new Color(i * originalColor.r, i * originalColor.g, i * originalColor.b, spriteR.color.a);
                yield return new WaitForSeconds(0.016f);
            }
        }
        spriteR.color = originalColor;
        yield return null;
    }
}
