using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorAnt : MonoBehaviour
{
    private bool walking = false;
    private Vector2 prePos;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        prePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (prePos != (Vector2)transform.position && !walking)
        {
            walking = true;
            animator.SetBool("Walking", walking);
        }else if (prePos == (Vector2)transform.position && walking){
            walking = false;
            animator.SetBool("Walking", walking);
        }
        //進行方向への回転
        if (walking)
        {
            var v = (Vector2)transform.position - prePos;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg; ;
            var scale = transform.localScale;
            var rotation = transform.eulerAngles;
            Debug.Log(angle);
            if (v.x > 0)
            {
                scale.x = -Math.Abs(scale.x);
                rotation.z = angle;

            }
            else
            {
                scale.x = Math.Abs(scale.x);
                rotation.z = angle - 180f;
            }
            transform.localScale = scale;
            transform.eulerAngles = rotation;
        }
        prePos = transform.position;
    }
}
