using UnityEngine;
using System.Collections;

public class HealpadCollisionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameControl.control.StartCoroutine(GameControl.control.HealOverTime(10f, 1f, .05f));
            this.gameObject.SetActive(false);
        }

    }

    public void GetsHealed(float gain)
    {
        GameControl.control.health = GameControl.control.health + gain;
    }

    

}
