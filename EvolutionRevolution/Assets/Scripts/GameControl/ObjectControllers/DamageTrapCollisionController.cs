using UnityEngine;
using System.Collections;

public class DamageTrapCollisionController : MonoBehaviour {

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
            float xColPos = col.gameObject.GetComponent<Transform>().position.x;
            float yColPos = col.gameObject.GetComponent<Transform>().position.y;
            float zColPos = col.gameObject.GetComponent<Transform>().position.z;
            float xPos = gameObject.GetComponent<Transform>().position.x;
            float yPos = gameObject.GetComponent<Transform>().position.y;
            float zPos = gameObject.GetComponent<Transform>().position.z;
            float xDif = xColPos - xPos;
            //float yDif = yColPos - yPos;
            float zDif = zColPos - zPos;
            xDif = xDif * xDif;
            //yDif = yDif * yDif;
            zDif = zDif * zDif;
            float distance = Mathf.Sqrt(Mathf.Abs(xDif  + zDif ));

            //GameControl.control.health = distance;
            if (distance <= 2)
                 GetsHurt(10.0f);
        }

    }

    public void GetsHurt(float damage)
    {
        GameControl.control.health = GameControl.control.health - damage;
    }

}
