using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyer2 : MonoBehaviour
{
    //UnityChanのオブジェクト
    private GameObject unitychan;
    //UnityChanと障害物の距離
    private float difference;

    // Start is called before the first frame update
    void Start()
    {
        // UnityChanのオブジェクトを取得
        this.unitychan = GameObject.Find("unitychan");
    }

    // Update is called once per frame
    void Update()
    {
        //UnityChanとカメラの位置(z座標)の差を求める
        this.difference = unitychan.transform.position.z - this.transform.position.z;
        //6m離れたらアイテムを破壊
        if (difference > 6f) {
            Destroy(gameObject);
        }
    }
}
