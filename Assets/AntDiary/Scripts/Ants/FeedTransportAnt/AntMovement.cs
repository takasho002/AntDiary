using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMovement : MonoBehaviour
{
    
    public Vector2 nowPlace;
    public float speed;
    public Vector2 way;
    public Vector2 test;
    // Start is called before the first frame update
    //

    public void Start(){
        nowPlace = new Vector2(0,0);
        speed = 0.01f;
        way = new Vector2(0,0);
        test= new Vector2(0,0);
    }
    public Vector2 Explore(Vector2 target){
        //対象までの距離
       Vector2 distance = target - nowPlace;
       //対象までの進む道(この場合スピードと同義) これを足していく
       way.x = speed* distance.x/distance.magnitude;
       way.y = speed* distance.y/distance.magnitude;
       return way;
    }
    public void GotoTarget(){
        nowPlace = nowPlace + test;
        transform.position = nowPlace;
    }
}
