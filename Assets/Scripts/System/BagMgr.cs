using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagMgr : UnitySingleton<BagMgr>
{
    // 数据结构:
    // key:物品,  value:物品数量
    private Dictionary<IItem, int> items = new Dictionary<IItem, int>();

    // 最大物品数量
    private int maxItemsNum = 20;

    public void AddItem(IItem item)
    {
        if (items.Count >= maxItemsNum)
        {
            //TODO: UI显示: 达到最大数量,无法添加
            return;
        }

        int cnt;
        // 如果item对应有值, 则原有的物品数量加1
        if (items.TryGetValue(item, out cnt))
        {
            cnt++;
        }
        else // 否则, 数量设为1
        {
            cnt = 1;
        }
        items[item] = cnt;
    }

    public void RemoveItem(IItem item)
    {
        int cnt;
        // 如果item对应有值
        if (items.TryGetValue(item, out cnt))
        {
            // 如果item数量大于1, 则原有的物品数量减1
            if (cnt > 1)
            {
                cnt--;
                items[item] = cnt;
            }
            else if (cnt == 1)// 如果item数量等于1, 则移除该物品
            {
                items.Remove(item);
            }
        }
        else // 如果item不存在, 则报错
        {
            throw new System.Exception("Try to remove item that not exited!!");
        }
    }

    public void Show()
    {
        // 遍历物体, 调用显示方法
        foreach (var item in items)
        {
        }
    }
}