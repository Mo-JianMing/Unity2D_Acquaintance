using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dama : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        RubyController rb = collision.GetComponent<RubyController>();//获得对象脚本
        //int emp = rb.Hea;
        if (rb != null && rb.Hea > 0)
        {
            rb.CoHea(-1);
        }
    }
}
