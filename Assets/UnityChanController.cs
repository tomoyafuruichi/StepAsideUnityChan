using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {

    //コンポーネントを入れる
    private Animator myAnimator;
    private Rigidbody myRigidbody;

    //前進させるための力
    private float forwardForce = 800.0f;

    //左右に移動するための力
    private float turnForce = 500.0f;
    //左右の移動範囲
    private float movableRange = 3.4f;

    //ジャンプするための力
    private float upForce = 500.0f;

    //unityちゃんの動きを減速させる係数
    private float coefficient = 0.95f;

    //ゲーム終了の判定
    private bool isEnd = false;

    //ゲーム終了時に表示するテキスト
    private GameObject stateText;

    //スコアを表示するテキスト
    private GameObject scoreText;

    //スコア
    private int score = 0;
    //左ボタンの判定
    private bool isLButtonDown = false;
    //右ボタンの判定
    private bool isRButtonDown = false;



	// Use this for initialization
	void Start () {
        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", 1);

        //Rigidbodyコンポーネントを入れる
        this.myRigidbody = GetComponent<Rigidbody>();

        //stateTextオブジェクトをついか
        this.stateText = GameObject.Find("GameResultText");

        //scoreTextオブジェクトを追加
        this.scoreText = GameObject.Find("ScoreText");

	}
	
	// Update is called once per frame
	void Update () {

        //ゲーム終了時Unityちゃんを減速させる
        if (this.isEnd)
        {
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }

        //Unityちゃんに前方向の力を加える
        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);

        //Unityちゃんを矢印キーの入力によって左右に移動させる
        if (Input.GetKey(KeyCode.LeftArrow) && -this.movableRange < this.transform.position.x || this.isLButtonDown && -this.movableRange < this.transform.position.x)
        {   //左に移動
            this.myRigidbody.AddForce(-this.turnForce, 0, 0);
        }else if (Input.GetKey(KeyCode.RightArrow)&& this.transform.position.x < this.movableRange || this.isRButtonDown && this.transform.position.x < this.movableRange)
        {   //右に移動
            this.myRigidbody.AddForce(this.turnForce, 0, 0);
        }

        //ジャンプ状態の時Jumpにfalseをセットする
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        //ジャンプしてないときにSpaceを押した場合ジャンプする
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {   //ジャンプアニメを再生
            this.myAnimator.SetBool("Jump", true);
            //unityちゃんに上方向の力を加える
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }


		
	}

    void OnTriggerEnter(Collider other)
    {
        //障害物にあたったとき
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;
            //ゲームオーバー表示
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }
        //ゴールしたとき
        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            //クリアー表示
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }
        //コインにあたったとき
        if (other.gameObject.tag == "CoinTag")
        {
            //パーティクルを再生
            GetComponent<ParticleSystem>().Play();

            //スコア加算
            this.score += 10;

            this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";


            //coinを消す
            Destroy(other.gameObject);
        }
    }

    //ジャンプボタンを押したときの処理
    public void GetMyJumpButton()
    {
        if (this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
    }

    //左ボタンを押したときの処理
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }
    //左ボタンをはなしたときの処理
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    //右ボタンを押したときの処理
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    //右ボタンを離したときの処理
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }

}
