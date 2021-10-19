using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private static HUD _instance;
    public static HUD Instance {
        get {
            if (_instance == null) {
                Debug.LogError("GUI is null");
            }
            return _instance;
        }
        private set { }
    }

    private Text score;

    // Start is called before the first frame update
    private void Start() {
        _instance = this;
        score = GetComponentInChildren<Text>();
    }
    
    public void UpdateScore() {
        score.text = "Candy Collected: " + Player.Instance.candy_collected;
    }
}
