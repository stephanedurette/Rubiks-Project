using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickDetector : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector] public UnityEvent OnLeftClicked;
    [HideInInspector] public UnityEvent OnRightClicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) OnLeftClicked?.Invoke();
        if (eventData.button == PointerEventData.InputButton.Right) OnRightClicked?.Invoke();
    }
}
