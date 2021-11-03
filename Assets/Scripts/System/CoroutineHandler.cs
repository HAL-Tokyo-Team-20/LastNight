using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandler : UnitySingleton<CoroutineHandler>
{
    // 非Mono类使用协程
    public Coroutine StartMyCoroutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }
}
