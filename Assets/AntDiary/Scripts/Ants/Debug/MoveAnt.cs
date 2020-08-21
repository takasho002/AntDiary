using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnt : MonoBehaviour
{
    public Vector2 way = (0,0);
    public Vector2 nowPlace = (0,0);
    // Start is called before the first frame update
    //
    public Vector2 Explore(Vector2 target){
        //対象までの距離
       Vector2 distance = target - nowPlace;
       //対象までの進む道(この場合スピードと同義) これを足していく
       
       way.x = speed* distance.x/distance.magnitude;
       way.y = speed* distance.y/distance.magnitude;
       return way;
    }
    public void GotoTarget(){
        nowPlace = transform.position;
        nowPlace = nowPlace + way;
        transform.position = nowPlace;
        return transform.position;
    }
}
