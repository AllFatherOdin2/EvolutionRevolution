using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

    public static UIControl control;

    public Text healthText;

	// Use this for initialization
	void Start () {
        SetHealthText();
	}
	
	// Update is called once per frame
	void Update () {
        SetHealthText();
   	}

    public void SetHealthText()
    {
        healthText.text = "Health: " + GameControl.control.health;
    }
}
