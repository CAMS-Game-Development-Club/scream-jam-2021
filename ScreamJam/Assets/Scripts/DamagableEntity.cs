using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableEntity : MonoBehaviour
{
    public float hitInvincibility = 0.5f;
    public SpriteRenderer spriteR;
    
    public void DamageAnimation() {
        StartCoroutine(damageAnimation());
    }

    private IEnumerator damageAnimation() { //Sprite flashes to black 2 times during hitInvincibility
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
