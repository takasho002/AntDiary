using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    /// <summary>
    /// オーディオクリップ保管クラス
    /// ランダム再生したいクリップごとにコンポネント作成
    /// SESystemにアタッチ
    /// </summary>
    public class SECategory : MonoBehaviour
    {
        [SerializeField] private string category = default;
        [SerializeField] private AudioClip[] audioclips = default;

        //ピッチ変更のオンオフと変更レンジ
        public bool randomizePitch = false;
        public float pitchRange = 0.1f;

        public string Category { get { return category; } }
        public AudioClip[] Audioclips { get { return audioclips; } }
    }
}