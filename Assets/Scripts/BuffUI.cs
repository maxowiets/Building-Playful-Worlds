using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuffUI : MonoBehaviour
{
    public PickUpType buffType;
    RawImage buffIcon;
    public Dictionary<PickUpType, Texture2D> imageDic = new Dictionary<PickUpType, Texture2D>();

    public PickUpType[] allTypes;
    public Texture2D[] allImages;

    int buffAmount;
    public TextMeshProUGUI buffAmountText;

    private void Awake()
    {
        for (int i = 0; i < allTypes.Length; i++)
        {
            imageDic.Add(allTypes[i], allImages[i]);
        }
        buffIcon = GetComponent<RawImage>();
    }

    public void IncreaseBuffAmount()
    {
        buffAmount++;
        buffAmountText.text = buffAmount.ToString();
    }

    public void SetBuffIcon()
    {
        buffIcon.texture = imageDic[buffType];
    }
}
