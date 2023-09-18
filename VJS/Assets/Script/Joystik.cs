using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Joystik : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image bgImg, joyImg;
    public Vector3 InputDirection { set; get; }

    void Start()
    {
        bgImg = GetComponent<Image>();
        joyImg = transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector3.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {

            float x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
            float y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

            InputDirection = new Vector3(x, 0, y);

            InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

            joyImg.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (bgImg.rectTransform.sizeDelta.y / 3), InputDirection.z * (bgImg.rectTransform.sizeDelta.y / 3));

        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        InputDirection = Vector3.zero;
        joyImg.rectTransform.anchoredPosition = Vector3.zero;
    }
}
