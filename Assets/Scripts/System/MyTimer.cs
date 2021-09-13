using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自定义协程封装的函数
/// </summary>
public class MyTimer
{
    /// <summary>
    /// 延迟指定时间 delaySeconds 后执行动作 action
    /// </summary>
    /// <param name="action">要执行的动作</param>
    /// <param name="delaySeconds">延迟的时间, 单位秒</param>
    /// <returns></returns>
    public static IEnumerator Wait(Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }
}