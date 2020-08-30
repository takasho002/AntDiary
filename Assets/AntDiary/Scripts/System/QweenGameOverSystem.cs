using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace AntDiary
{
    public class QweenGameOverSystem : MonoBehaviour
    {
        public AntData QweenAnt;
        // Start is called before the first frame update
        void Start()
        {

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
