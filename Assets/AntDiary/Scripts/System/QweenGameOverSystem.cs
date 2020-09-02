using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace AntDiary
{
    public class QweenGameOverSystem : MonoBehaviour
    {
        private DebugAntData QweenAnt;//DebugAntDataを、これから先QweenAntDataとかができたらそちらに修正する必要あり
        public GameObject QweenObject;//ここにInspectorから女王バチのGameObjectをアタッチ(保存)する
        // Start is called before the first frame update
        void Start()
        {
            QweenAnt = QweenObject.GetComponent<DebugAntData>();
        }

        // Update is called once per frame
        void Update()
        {
            if(QweenAnt.IsAlive == false)
            {
                SceneManager.LoadScene("Sayoshi_GameOver");
            }
        }
    }
}
