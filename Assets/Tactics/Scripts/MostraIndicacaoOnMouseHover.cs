using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MostraIndicacaoOnMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    void Update()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.FindChild("Indicacao").gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.FindChild("Indicacao").gameObject.SetActive(false);
    }
}