using UnityEngine;
using System.Collections;

public class StartGameButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Application.LoadLevel(1); // load the wall generation scene
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BeginGame()
    {

        Application.LoadLevel(1); // load the wall generation scene
    }

}
