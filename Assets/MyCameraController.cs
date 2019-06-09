﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour {

    //unityちゃんのオブジェクト
    private GameObject unitychan;
    //unityちゃんとカメラの距離
    private float difference;

	// Use this for initialization
	void Start () {
        //unityちゃんのオブジェクトを取得
        this.unitychan = GameObject.Find("unitychan");

        //unityちゃんとカメラの距離を求める
        this.difference = unitychan.transform.position.z - this.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {

        //unityちゃんの距離に合わせてカメラの位置を移動
		this.transform.position=new Vector3(0,this.transform.position.y,this.unitychan.transform.position.z-difference);
	}
}
