using UnityEngine;
using UnityEngine.Events;

public class TripleCircle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ClickDetector inner;
    [SerializeField] private ClickDetector outer;
    [SerializeField] private ClickDetector middle;

    [HideInInspector] public UnityEvent OnInnerCircleLeftClicked;
    [HideInInspector] public UnityEvent OnInnerCircleRightClicked;
    [HideInInspector] public UnityEvent OnMiddleCircleLeftClicked;
    [HideInInspector] public UnityEvent OnMiddleCircleRightClicked;
    [HideInInspector] public UnityEvent OnOuterCircleLeftClicked;
    [HideInInspector] public UnityEvent OnOuterCircleRightClicked;

    private void OnEnable()
    {
        inner.OnLeftClicked.AddListener(() => OnInnerCircleLeftClicked?.Invoke());
        inner.OnRightClicked.AddListener(() => OnInnerCircleRightClicked?.Invoke());
        outer.OnLeftClicked.AddListener(() => OnOuterCircleLeftClicked?.Invoke());
        outer.OnRightClicked.AddListener(() => OnOuterCircleRightClicked?.Invoke()); 
        middle.OnLeftClicked.AddListener(() => OnMiddleCircleLeftClicked?.Invoke());
        middle.OnRightClicked.AddListener(() => OnMiddleCircleRightClicked?.Invoke());
    }
}
