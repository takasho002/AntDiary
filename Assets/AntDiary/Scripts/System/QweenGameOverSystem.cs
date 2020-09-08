using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace AntDiary
{
    public class QweenGameOverSystem : MonoBehaviour
    {
        private QueenAntData QueenAnt;//DebugAntDataを、これから先QweenAntDataとかができたらそちらに修正する必要あり
        //public GameObject QueenObject;//ここにInspectorから女王バチのGameObjectをアタッチ(保存)する
        // Start is called before the first frame update
        void Start()
        {
            QueenAnt = (QueenAntData)gameObject.GetComponent<QueenAnt>().Data;
        }

        // Update is called once per frame
        void Update()
        {
            if(QueenAnt.IsAlive == false)
            {
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }
}
