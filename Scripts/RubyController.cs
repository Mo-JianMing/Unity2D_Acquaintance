using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制角色移动、生命、动画..
/// </summary>

public class RubyController : MonoBehaviour
{
    public float speed;//速度
    public int maxHea;//最大血条
    int currHea;//当前血条
    public int Hea { get { return currHea; } }//只读属性

    public float invTime;
    float timer;//无敌时间
    bool ifInv = false;//是否进入无敌
    Rigidbody2D r2d;
    Animator Anim;
    public GameObject gameObjectPlu;
    public ParticleSystem hit,straw;//打击、草莓特效
    public AudioClip sw,hitC,luanchC;//草莓、受击音效
    AudioSource foot;//走路音效
    Vector2 look = new Vector2(0, 1);

    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();//获得刚体
        Anim = GetComponent<Animator>();//获得动画
        foot = GetComponent<AudioSource>();//获得音效
        currHea = maxHea;//初始化血条
    }

    // Update is called once per frame
    void Update()
    {
        float mX = Input.GetAxis("Horizontal");//A:-1、D:1
        float mY = Input.GetAxis("Vertical");//S:-1、W:1
        Vector2 move = new Vector2(mX, mY);

        if (!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0))
        {
            look.Set(move.x, move.y);
            look.Normalize();
            //foot.loop = true;
        }
        //else foot.loop = false;

        Anim.SetFloat("Look X", look.x);
        Anim.SetFloat("Look Y", look.y);
        Anim.SetFloat("Speed", move.magnitude);

        Vector2 position = r2d.position;
        position += move * speed * Time.deltaTime;
        r2d.MovePosition(position);
        //position.x += mX * speed * Time.deltaTime;//增量时间
        //position.y += mY * speed * Time.deltaTime;
        //transform.position = position;
        //transform.Translate(transform.right * speed * Time.deltaTime); 

        if (ifInv)
        {
            timer -= Time.deltaTime;//计时器运作
            if (timer < 0)
            {
                ifInv = false;//不无敌
                speed *= 2;//速度恢复
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Launch();
        }

    }

    public void CoHea(int ind)
    {
        if (ind < 0)
        {
            if (ifInv) return;
            ifInv = true;//受伤开启无敌
            speed /= 2;//速度降低
            timer = invTime;//重置计时器
            Anim.SetTrigger("Hit");//受击动画
            AudioMan.instan.call(hitC);
        }

        currHea = Mathf.Clamp(currHea + ind, 0, maxHea);//限制在0-max间加减血
        UiBar.instance.ConBar(currHea, maxHea);
        //Debug.Log("血条：" + currHea);
    }

    public void HitEffect()
    {
        Instantiate(hit, r2d.position + Vector2.up * 0.5f, Quaternion.identity);
    }

    public void Straw()
    {
        Instantiate(straw, r2d.position + Vector2.up * 0.5f, Quaternion.identity);
        AudioMan.instan.call(sw);
    }

    void Launch()
    {
        GameObject pob = Instantiate(gameObjectPlu, r2d.position + Vector2.up * 0.5f, Quaternion.identity);//复制预制体
        CogCon poj = pob.GetComponent<CogCon>();//获得脚本
        poj.launch(look, 500);
        AudioMan.instan.call(luanchC);
        Anim.SetTrigger("Launch");
    }
}
