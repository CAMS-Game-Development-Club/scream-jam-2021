using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteR;
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    private Animator anim;

    // Update is called once per frame
    private void Update() {
        UpdateFlip();
        UpdateWalking();
    }

    public void SetAnimBool(string param, bool value) {
        anim.SetBool(param, value);
    }

    private void UpdateFlip() {
        // Make player face correct direction
        if (rb2d.velocity.x > 0) {
            spriteR.flipX = false;
        } else if (rb2d.velocity.x < 0) {
            spriteR.flipX = true;
        }
    }

    private void UpdateWalking() {
        if (rb2d.velocity.magnitude > 0.5) {
            anim.SetBool("IsWalking", true);
        } else {
            anim.SetBool("IsWalking", false);
        }
    }

    public void DamageAnimation() {
        StartCoroutine(damageAnimation());
    }

    private IEnumerator damageAnimation() { //Sprite flashes to black 2 times during hitInvincibility
        float hitInvincibility = Player.Instance.hitInvincibility;
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
