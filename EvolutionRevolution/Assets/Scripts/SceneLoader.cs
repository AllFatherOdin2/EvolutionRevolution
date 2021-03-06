﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

    private bool loadScene = false;

    [SerializeField]
    private int scene;
    [SerializeField]
    private Text loadingText;
    public Camera cam;


    // Updates once per frame
	void Update() {

        // If the player has pressed the space bar and a new scene is not loading yet...
        if (Input.GetKeyUp(KeyCode.Space) && loadScene == false) {

            // ...set the loadScene boolean to true to prevent loading a new scene more than once...
            loadScene = true;

            // ...change the instruction text to read "Loading..."
            loadingText.text = "Loading...";

            // ...and start a coroutine that will load the desired scene.
            StartCoroutine(LoadNewScene());

        }

        // If the new scene has started loading...
        if (loadScene == true) {

            // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, Mathf.PingPong(Time.time, 255), loadingText.color.a);

        }

	}


    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene() {

        
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = Application.LoadLevelAsync(scene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone) {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, Mathf.PingPong(Time.time, 255), loadingText.color.a);
            cam.Render();
            yield return null;
        }

    }

}
