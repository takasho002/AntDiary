using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    [SerializeField]Image BackgroundSpriteRenderer;

    //春画像
    public Sprite SpringSprite;

    //夏画像
    public Sprite SummerSprite;

    //秋画像
    public Sprite FallSprite;

    //冬画像
    public Sprite WinterSprite;

    // Start is called before the first frame update
    void Start()
    {
        //一番最初は春が表示されるようになっている
        BackgroundSpriteRenderer.sprite = SpringSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBackground(int season)
    {
        // 0-3 : 春-冬 でスプライトを設定する
        switch (season)
        {
            case 0:
                BackgroundSpriteRenderer.sprite = SpringSprite;
                break;

            case 1:
                BackgroundSpriteRenderer.sprite = SummerSprite;
                break;

            case 2:
                BackgroundSpriteRenderer.sprite = FallSprite;
                break;

            case 3:
                BackgroundSpriteRenderer.sprite = WinterSprite;
                break;

            //該当しない場合
            default:
                UnityEngine.Debug.Log("指定された整数値の季節は存在しません");
                break;
        }
    }

    void SetBackground(string str)
    {
        switch (str)
        {
            case "Spring":
                BackgroundSpriteRenderer.sprite = SpringSprite;
                break;

            case "Summer":
                BackgroundSpriteRenderer.sprite = SummerSprite;
                break;

            case "Fall":
                BackgroundSpriteRenderer.sprite = FallSprite;
                break;

            case "Winter":
                BackgroundSpriteRenderer.sprite = WinterSprite;
                break;

            //該当しない場合
            default:
                UnityEngine.Debug.Log("指定された文字列の季節は存在しません");
                break;
        }
    }

    void SetBackground(Sprite sp)
    {
        BackgroundSpriteRenderer.sprite = sp;
    }
}
