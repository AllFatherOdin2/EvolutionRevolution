using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour
{

    public Transform player;

    // Use this for initialization
    void Start()
    {
        Instantiate(player, new Vector3(0, 0.5f, 0), Quaternion.identity);
    }
}