using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

namespace AntDiary
{
    /// <summary>
    /// 効果音再生システム、音を鳴らしたいオブジェクトに取り付ける
    /// </summary>
    public class SESystem: MonoBehaviour
    {
        [SerializeField] private SECategory[] secategories = default;
        [SerializeField] private AudioSource audiosource = default;

        void Start()
        {
            //audiosource = GetComponent<AudioSource>();
            //secategories = GetComponents<SECategory>();
            PlaySE("");
        }

        /// <summary>
        /// 指定されたカテゴリの効果音を再生
        /// </summary>
        /// <param name="name">指定カテゴリ名</param>
        public void PlaySE (string name)
        {
            int num = -1;

            for (int i = 0; i < secategories.Length; i++)
            {
                if (name.Equals(secategories[i].Category))
                {
                    num = i;
                    break;
                }
            }

            if (num == -1)
            {
                Debug.Log("指定した名前の効果音カテゴリは見つかりませんでした");
                return;
            }

            if (secategories[num].randomizePitch)
            {
                audiosource.pitch = 1.0f + Random.Range(-secategories[num].pitchRange, secategories[num].pitchRange);
            }
            else
            {
                audiosource.pitch = 1.0f;
            }

            audiosource.PlayOneShot(secategories[num].Audioclips[(int)Random.Range(0, secategories[num].Audioclips.Length)]);
            
        }
    }
}

