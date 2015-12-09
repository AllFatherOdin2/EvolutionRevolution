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
        else if(col.gameObject.CompareTag("HealPad"))
        {
            StartCoroutine(HealOverTime( 10f, 1f, .05f));
            col.gameObject.SetActive(false);
        }
    }

    IEnumerator HealOverTime(float healAmt, float healTime, float healTick)
        //healAmt: total amount to heal player; healTime: total time to heal player over (sec); healTick frequency to heal for each amount (sec)
    {
        for(float f=0.0f; f<healTime; f+= healTick)
        {
            float healOnTick = healTick / healTime;
            healOnTick *= healAmt;
            GameControl.control.health += healOnTick;
            yield return new WaitForSeconds(healTick);
        }
    }

}

