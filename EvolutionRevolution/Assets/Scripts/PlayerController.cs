using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    public void GetsHurt(float damage)
    {
        GameControl.control.health = GameControl.control.health - damage;      
    }

    public void GetsHealed(float gain)
    {
        GameControl.control.health = GameControl.control.health + gain;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("DamageTrap"))
        {
            GetsHurt(10.0f);
        }
    }

}