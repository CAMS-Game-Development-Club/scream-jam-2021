using System.Collections;
using UnityEngine;

public class Enemy : DamagableEntity
{
    [SerializeField]
    private Animator anim;

    public int health;
    public bool isAlive;
    public void checkIfAlive() {
        if (health <= 0) {
            isAlive = false;
            anim.SetBool("Dead", true);
            StartCoroutine(die());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) { //Detects Collisions
        // Check if collided object is an enemy weapon hitbox
        if (collision.tag == "PlayerWeapon") { 
            // Deal damage
            health -= collision.GetComponent<Weapon>().weaponDamage;
            DamageAnimation();
        }
        checkIfAlive();
    }

    private IEnumerator die() {
        yield return new WaitForSeconds(1f);
        Destroy(transform.parent.gameObject);
    }


}
