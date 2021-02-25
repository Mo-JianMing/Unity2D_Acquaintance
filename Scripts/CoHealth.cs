using UnityEngine;

public class CoHealth : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController rb = collision.GetComponent<RubyController>();//获得对象脚本
        if (rb != null && rb.Hea < rb.maxHea)
        {
            Destroy(gameObject);
            rb.Straw();
            rb.CoHea(1);
        }
    }
}
