using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour {

    private Rigidbody myrigidbody;
    private float velocityZ = 16.3f;
    //UnityChanのオブジェクト
    private GameObject unitychan;

    // Start is called before the first frame update
    void Start() {
        this.myrigidbody = GetComponent<Rigidbody>();
        //UnityChanのオブジェクトを取得
        this.unitychan = GameObject.Find("unitychan");
    }

    // Update is called once per frame
    void Update() {
        //ItemDestroyerに速度を与える
        this.myrigidbody.velocity = new Vector3(0, 0, velocityZ);
    }

    //衝突判定が怒ったものを消失させる
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "CarTag" ||
            other.gameObject.tag == "TrafficConeTag" ||
            other.gameObject.tag == "CoinTag") {
            Destroy(other.gameObject);
        }
    }
    //UnityChanと接触した場合、ItemDestroyerのオブジェクトが消失する
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject == unitychan) {
            Destroy(gameObject);
        }
    }
}