using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chochikuko : MonoBehaviour
{
    //  貯蓄庫の在庫量をここで管理する
    [HideInInspector] public int stockAmount;

    // Start is called before the first frame update
    void Start()
    {
        // 貯蓄庫の初期の貯蓄量を0とする
        stockAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(stockAmount);
    }
}
