using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffsUIManager : MonoBehaviour
{
    public List<BuffUI> buffList = new List<BuffUI>();

    public BuffUI buff;

    public void AddBuffUI(PickUpType buffType)
    {
        bool newAddBuff = true;
        foreach (var buff in buffList)
        {
            if (buff.buffType == buffType)
            {
                newAddBuff = false;
            }
        }

        if (newAddBuff == true)
        {
            var newBuff = Instantiate(buff, transform.position + Vector3.right * 60 * buffList.Count, Quaternion.identity);
            buffList.Add(newBuff);
            newBuff.buffType = buffType;
            newBuff.SetBuffIcon();
            newBuff.transform.SetParent(transform);
        }

        IncreaseBuffUI(buffType);
    }

    public void IncreaseBuffUI(PickUpType buffType)
    {
        foreach (var buff in buffList)
        {
            if (buff.buffType == buffType)
            {
                buff.IncreaseBuffAmount();
            }
        }
    }
}
