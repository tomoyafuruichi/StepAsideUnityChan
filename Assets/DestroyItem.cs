using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour {

    //cameraを入れる
    private GameObject camera;


	// Use this for initialization
	void Start () {
        this.camera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.z <= this.camera.transform.position.z)
        {
            Destroy(this.gameObject);
        }
		
	}
}
