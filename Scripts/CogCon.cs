using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogCon : MonoBehaviour
{
    Rigidbody2D rig;
    // Start is called before the first frame update
    void Awake()//实例调用
    {
        rig = GetComponent<Rigidbody2D>();
    }

    public void launch(Vector2 dir,int force)
    {
        rig.AddForce(dir * force);//方向和大小
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RobotCon rob = collision.gameObject.GetComponent<RobotCon>();
        if (rob != null)
        {
            rob.Fix();
        }
        Destroy(gameObject);
    }
}
