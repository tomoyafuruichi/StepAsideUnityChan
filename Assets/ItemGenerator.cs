using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{

    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //conePrefabを入れる
    public GameObject conePrefab;

    //スタート地点
    private int startPos = -160;
    //ゴール地点
    private int goalPos = 120;
    //アイテムを出すX軸の範囲
    private float posRange = 3.4f;

    //unitychanを入れる
    private GameObject unitychan;

    private float interval = 0.9f;
    private float time;






    // Use this for initialization
    void Start()
    {
        //unityちゃんのゲームオブジェクトを取得
        this.unitychan = GameObject.Find("unitychan");



    }



    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= interval)
        {
            if (this.unitychan.transform.position.z + 50 < goalPos)
            {
                //アイテムの種類をランダムに決定
                int num = Random.Range(1, 11);
                if (num <= 2)
                {
                    //コーンを一直線に配置
                    for (float j = -1; j <= 1; j += 0.4f)
                    {
                        GameObject cone = Instantiate(conePrefab) as GameObject;
                        cone.transform.position = new Vector3(4 * j, cone.transform.position.y, this.unitychan.transform.position.z + 50);
                    }
                }
                else
                {
                    //レーンごとにアイテム生成
                    for (int j = -1; j <= 1; j++)
                    {   //アイテムの種類を決める
                        int item = Random.Range(1, 11);
                        //アイテムを置くZ座標のオフセットをランダムに設定
                        int offsetZ = Random.Range(-5, 6);
                        //60%コイン30%車10%なし
                        if (1 <= item && item <= 6)
                        {
                            GameObject coin = Instantiate(coinPrefab) as GameObject;
                            coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, this.unitychan.transform.position.z + 50 + offsetZ);
                        }
                        else if (7 <= item && item <= 9)
                        {
                            GameObject car = Instantiate(carPrefab) as GameObject;
                            car.transform.position = new Vector3(posRange * j, car.transform.position.y, this.unitychan.transform.position.z + 50 + offsetZ);
                        }
                    }
                }
                time = 0.00f;
            }

        }


    }
}
        

       

