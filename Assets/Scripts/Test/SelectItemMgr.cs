using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItemMgr : UnitySingleton<SelectItemMgr>
{
    public float DistanceToSelect = 5.0f;
    List<ISelectItem> items = new List<ISelectItem>();

    int currentIndex = 0;
    List<ISelectItem> selectedItems = new List<ISelectItem>();

    public void Select()
    {
        // 当玩家处于选取物体状态, 比如当玩家按下 LT 时候进入此状态

        // 可选: 按到玩家距离排序?
        // 显示在范围内, 可选取的物体
        foreach (ISelectItem item in items)
        {
            if (item.DistanceToPlayer() <= DistanceToSelect)
            {
                selectedItems.Add(item);
            }
        }
        currentIndex = 0;
    }

    public void SelectItem()
    {
        for (int i = 0; i < selectedItems.Count; i++)
        {
            selectedItems[i].Selected = false;
        }
        selectedItems[currentIndex].Selected = true;
    }

    public void SelectNextItem()
    {
        selectedItems[currentIndex].Selected = false;
        // 如果到了最后一个, 则从头开始
        if (currentIndex < selectedItems.Count - 1)
        {
            currentIndex++;
        }
        else
        {
            currentIndex = 0;
        }

        selectedItems[currentIndex].Selected = true;
    }

    public void SelectPreItem()
    {
        selectedItems[currentIndex].Selected = false;
        // 如果到了第一个, 则从尾开始
        if (currentIndex > 0)
        {
            currentIndex--;
        }
        else
        {
            currentIndex = selectedItems.Count - 1;
        }

        selectedItems[currentIndex].Selected = true;
    }

    public void Confirm()
    {
        selectedItems[currentIndex].ConfirmSelected = true;
    }

}
