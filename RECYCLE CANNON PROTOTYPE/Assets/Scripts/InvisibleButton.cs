using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvisibleButton : MonoBehaviour , IPointerDownHandler
{
    public UnityEvent OnClicked;

    private void Awake()
    {
        GetComponent<Image>().color = Color.clear;
    }

    public void OnPointerDown(PointerEventData eventData) => OnClicked.Invoke();
}
