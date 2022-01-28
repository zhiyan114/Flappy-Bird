using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Name: 数学
 * Description: 数学的应用程序编程接口
 * Author: 兽迷
 */
public static class 数学
{
    /// <summary>
    /// 正弦 or known as Sine
    /// </summary>
    /// <return>
    /// 退浮点数
    /// </return>
    public static float 正弦(float 角度)
    {
        return Mathf.Sin(角度);
    }
    /// <summary>
    /// 余弦 or known as Cosine
    /// </summary>
    /// <return>
    /// 退浮点数
    /// </return>
    public static float 余弦(float 角度)
    {
        return Mathf.Cos(角度);
    }
    /// <summary>
    /// 切线 or known as Tangent
    /// </summary>
    /// <return>
    /// 退浮点数
    /// </return>
    public static float 切线(float 角度)
    {
        return Mathf.Tan(角度);
    }
    /// <summary>
    /// 反正弦 or known as Inverse of Sine
    /// </summary>
    /// <return>
    /// 退浮点数
    /// </return>
    public static float 反正弦(float 角度)
    {
        return Mathf.Acos(角度);
    }
    // 我不需要更多解释了。。。
    public static float 反余弦(float 角度)
    {
        return Mathf.Acos(角度);
    }
    public static float 割线(float 角度)
    {
        return Mathf.Cos(1 / 角度);
    }
    public static float 余割(float 角度)
    {
        return Mathf.Sin(1 / 角度);
    }
    public static float 余切(float 角度)
    {
        return Mathf.Tan(1 / 角度);
    }

}
