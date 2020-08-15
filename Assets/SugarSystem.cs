using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SugarSystem : MonoBehaviour
{
    public double Amount_of_sugar = 0;
    public float Season_coefficient = 1.0f;
    public float Ant_ability_sugar = 2.0f;
    Ant ant;
    // トリガーとの接触時に呼ばれるコールバック
    void OnTriggerEnter(Collider hit)
    {
        // 接触対象がSugarタグを持つオブジェクトだった場合に処理をする
        // ※ ant同士で接触して砂糖が得られないようにする
        if (hit.CompareTag("Sugar"))
        {
            // 砂糖を持たせる
            ant.HavingSugar = Math.Ceiling(Season_coefficient * ant.SugarAbility);

        }
    }
}
public class Ant : MonoBehaviour
{
    public double HavingSugar = 0;
    public float SugarAbility = 2.0f;

}