using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBar : MonoBehaviour
{
    public static UiBar instance  { get; private set; }//单例
    public Image bar;

    void Start()
    {
        instance = this;
    }

    public void ConBar(int cur,int max)
    {
        bar.fillAmount = cur / (float)max;//百分比调节
    }
}
