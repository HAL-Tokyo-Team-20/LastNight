using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItemMgr : UnitySingleton<SelectItemMgr>
{
    [Range(1.0f, 100.0f)]
    public float DistanceToSelect = 100.0f;

    List<ISelectItem> items = new List<ISelectItem>();

    int currentIndex = 0;
    List<ISelectItem> selectedItems = new List<ISelectItem>();

    private UIManager uIManager;

    private void Start()
    {
        uIManager = UIManager.Instance;
    }

    public void AddToList(ISelectItem item)
    {
        items.Add(item);
    }

    public void Select()
    {
        // 当玩家处于选取物体状态, 比如当玩家按下 LT 时候进入此状态
        selectedItems.Clear();

        foreach (ISelectItem item in items)
        {

            // 使用collider判定
            if (item.PlayerIsEnter)
            {
                selectedItems.Add(item);
            }
        }
    }

    public void SelectFirstItem()
    {
        if (selectedItems.Count == 0) return;
        else currentIndex = 0;
    }

    public void SelectNextItem()
    {
        if (selectedItems.Count == 0) return;
        // 如果到了最后一个, 则从头开始
        var result = currentIndex < selectedItems.Count - 1 ? currentIndex++ : currentIndex = 0;
    }

    public void SelectPreItem()
    {
        if (selectedItems.Count == 0)
        {
            return;
        }
        // 如果到了第一个, 则从尾开始
        var result = currentIndex > 0 ? currentIndex-- : currentIndex = selectedItems.Count - 1;
        
    }

    public void SelectItem()
    {
        if (selectedItems.Count == 0) return;
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
        if (selectedItems.Count == 0) return;
        for (int i = 0; i < selectedItems.Count; i++)
        {
            selectedItems[i].Selected = false;
            selectedItems[i].ConfirmSelected = false;
        }
    }

    public int GetSelecteditemsLength() { return selectedItems.Count; }

}
