using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health {get; set;}
    [SerializeField]
    private bool isAlive { get; set; }
    public void checkIfAlive() {
        if (health <= 0) {
            isAlive = false;
        }
    }
}
