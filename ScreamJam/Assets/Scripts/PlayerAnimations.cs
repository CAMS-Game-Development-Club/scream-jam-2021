using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : DamagableEntity
{
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
}
