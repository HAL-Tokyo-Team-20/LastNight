using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItemMgr : UnitySingleton<SelectItemMgr>
{
    public float DistanceToSelect = 100.0f;
    List<ISelectItem> items = new List<ISelectItem>();

    int currentIndex = 0;
    List<ISelectItem> selectedItems = new List<ISelectItem>();

    public void AddToList(ISelectItem item)
    {
        items.Add(item);
        Debug.Log("items count = " + items.Count);
    }

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
    }

    public void SelectFirstItem()
    {
        if (selectedItems.Count == 0)
        {
            return;
        }
        currentIndex = 0;
    }

    public void SelectNextItem()
    {
        if (selectedItems.Count == 0)
        {
            return;
        }
        // 如果到了最后一个, 则从头开始
        if (currentIndex < selectedItems.Count - 1)
        {
            currentIndex++;
        }
        else
        {
            currentIndex = 0;
        }
    }

    public void SelectPreItem()
    {
        if (selectedItems.Count == 0)
        {
            return;
        }
        // 如果到了第一个, 则从尾开始
        if (currentIndex > 0)
        {
            currentIndex--;
        }
        else
        {
            currentIndex = selectedItems.Count - 1;
        }
    }

    public void SelectItem()
    {
        if (selectedItems.Count == 0)
        {
            return;
        }
        for (int i = 0; i < selectedItems.Count; i++)
        {
            selectedItems[i].Selected = false;
        }
        selectedItems[currentIndex].Selected = true;
    }

    public void Confirm()
    {
        selectedItems[currentIndex].ConfirmSelected = true;
    }

    public void CancelAll()
    {
        if (selectedItems.Count == 0)
        {
            return;
        }
        for (int i = 0; i < selectedItems.Count; i++)
        {
            selectedItems[i].Selected = false;
        }
    }

}
