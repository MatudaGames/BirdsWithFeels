﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tooltipText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(tooltipText != "")
            Helpers.Instance.ShowTooltip(tooltipText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Helpers.Instance.HideTooltip();
    }
    public void OnMouseEnter()
    {
        if (tooltipText != "")
            Helpers.Instance.ShowTooltip(tooltipText);
    }

    public void OnMouseExit()
    {
        Helpers.Instance.HideTooltip();
    }
}
