using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool isAlive;
    public void checkIfAlive() {
        if (health <= 0) {
            isAlive = false;
        }
    }
}
