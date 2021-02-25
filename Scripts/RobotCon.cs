using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCon : MonoBehaviour
{
    Rigidbody2D rob;
    Animator animator;
    public ParticleSystem smoke;//烟，打击特效
    public AudioClip fixC;

    public int speed, week;//速度,巡逻周数
    public float sTime;//间隔
    public float dire = 0.5f;//方向
    float timer;//计时器
    int empNumb, numb;//记录次数
    bool turn, isFix;//是否转向

    // Start is called before the first frame update
    void Start()
    {
        rob = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = sTime;//设置计时器
        numb = week * 2;//巡逻次数
        CoAni(turn);//初始化动画
    }

    // Update is called once per frame
    void Update()
    {
        if (isFix) return;

        Vector2 posi = rob.position;
        if (turn) { posi.y += speed * dire * Time.deltaTime; }
        else { posi.x += speed * dire * Time.deltaTime; }
        rob.MovePosition(posi);

        timer -= Time.deltaTime;//倒时
        if (timer < 0)
        {
            timer = sTime;
            empNumb += 1;//巡逻次数+1
            if (empNumb % numb == 0)//巡逻结束
            {
                empNumb = 0;//重置
                turn = !turn;//调转X、Y
            }
            dire *= -1;//同向调转 
            CoAni(turn);//封装动画
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController ruby = collision.gameObject.GetComponent<RubyController>();
        if (ruby != null && ruby.Hea > 0)
        {
            ruby.HitEffect();
            ruby.CoHea(-1);
        }
    }

    void CoAni(bool ind)
    {
        if (ind)
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", dire);
        }
        else
        {
            animator.SetFloat("MoveX", dire);
            animator.SetFloat("MoveY", 0);
        }
    }

    public void Fix()
    {
        isFix = true;
        rob.simulated = false;
        animator.SetTrigger("Fixed");
        AudioMan.instan.call(fixC);
        smoke.Stop();
    }
}
