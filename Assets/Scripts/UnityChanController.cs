using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {
     
    private Animator myAnimator;        //アニメーションするためのコンポーネントを入れる
    private Rigidbody myrigidbody;      //UnityChanを移動させるコンポーネントを入れる
    private float velocityZ    = 16f;   //前方向(z方向)の速度
    private float velocityX    = 10f;   //左右方向(x方向)の速度
    private float movableRange = 3.4f;  //左右移動できる範囲
    private float velocityY    = 10f;   //上方向(y方向)の速度
    private float coefficient  = 0.99f; //動きを減速させる係数
    private bool isEnd = false;         //ゲーム終了の判定
    private GameObject stateText;       //ゲーム終了時に表示するテキスト
    private GameObject scoreText;       //スコアを表示するテキスト
    private int score = 0;              //得点
    private bool isLButtonDown = false; //左ボタン押下の判定
    private bool isRButtonDown = false; //右ボタン押下の判定
    private bool isJButtonDown = false; //ジャンプボタン押下の判定


    // Start is called before the first frame update
    void Start() {
        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();
        //走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", 1);
        //Rigidbodyコンポーネントを取得
        this.myrigidbody = GetComponent<Rigidbody>();
        //シーン中のstateTextオブジェクトを取得
        this.stateText = GameObject.Find("GameResultText");
        //シーン中のscoreTextオブジェクトを取得
        this.scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update() {
        //ゲーム終了ならUnityChanの動きを減衰
        if (this.isEnd) {
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.velocityZ *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }


        //横方向の入力による速度
        float inputVelocityX = 0;
        //上方向の入力による速度
        float inputVelocityY = 0;


        //UnityChanを矢印キーまたはボタンに応じて左右移動
        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) &&
            -this.movableRange < this.transform.position.x) {
            //左方向への速度を代入
            inputVelocityX = -this.velocityX;
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) &&
            this.transform.position.x < this.movableRange) {
            //左方向への速度を代入
            inputVelocityX = this.velocityX;
        }


        //ジャンプしていない時にスペースを押すとジャンプする
        if ((Input.GetKeyDown(KeyCode.Space) || this.isJButtonDown) &&
            this.transform.position.y < 0.5f) {
            //ジャンプアニメを再生
            this.myAnimator.SetBool("Jump", true);
            //上方向への速度を代入
            inputVelocityY = this.velocityY;
        }
        else {
            //現在のY軸の速度を代入
            inputVelocityY = this.myrigidbody.velocity.y;
        }

        //Jumπステートの場合はJumpにfalseをセットする(連続でジャンプする事を防ぐ)
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
            this.myAnimator.SetBool("Jump", false);
        }

        //UnityChanに速度を与える
        this.myrigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, velocityZ);
    }


    //トリガーモードで他のオブジェクトと接触した場合の処理
    private void OnTriggerEnter(Collider other) {

        //障害物に衝突した場合
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag") {
            this.isEnd = true;
            //stateTextにGAME OVERを表示
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }
        //ゴール地点に到着した場合
        if (other.gameObject.tag == "GoalTag") {
            this.isEnd = true;
            //stateTextにGAME CLEARを表示
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }
        //コインに衝突した場合
        if (other.gameObject.tag == "CoinTag") {

            //スコアを加算
            this.score += 10;

            //ScoreTextに獲得した点数を表示
            this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";

            //パーティクルを再生(コイン獲得のサイン)
            GetComponent<ParticleSystem>().Play();
            //接触したコインオブジェクトを破棄
            Destroy(other.gameObject);
        }
    }

    //ジャンプボタンを押した時の処理
    public void GetMyJumpButtonDown() {
        this.isJButtonDown = true;
    }
    //ジャンプボタンを離した時の処理
    public void GetMyJumpButtonUp() {
        this.isJButtonDown = false;
    }
    //左ボタンを押した時の処理
    public void GetMyLeftButtonDown() {
        this.isLButtonDown = true;
    }
    //左ボタンを離した時の処理
    public void GetMyLeftButtonUp() {
        this.isLButtonDown = false;
    }
    //右ボタンを押した時の処理
    public void GetMyRightButtonDown() {
        this.isRButtonDown = true;
    }
    //右ボタンを離した時の処理
    public void GetMyRightButtonUp() {
        this.isRButtonDown = false;
    }
}
