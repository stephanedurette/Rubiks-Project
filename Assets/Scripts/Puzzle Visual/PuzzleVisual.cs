using UnityEngine;
using UnityEngine.Events;

public class PuzzleVisual : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TripleCircle leftCircle;
    [SerializeField] private TripleCircle rightCircle;
    [SerializeField] private TripleCircle topCircle;

    [HideInInspector] private UnityEvent On_Left_InnerCircle_RightClicked;
    [HideInInspector] private UnityEvent On_Left_InnerCircle_LeftClicked;
    [HideInInspector] private UnityEvent On_Left_MiddleCircle_RightClicked;
    [HideInInspector] private UnityEvent On_Left_MiddleCircle_LeftClicked; 
    [HideInInspector] private UnityEvent On_Left_OuterCircle_RightClicked;
    [HideInInspector] private UnityEvent On_Left_OuterCircle_LeftClicked;

    private void OnEnable()
    {
        SetupListeners();
    }

    private void SetupListeners()
    {
        leftCircle.OnInnerCircleLeftClicked.AddListener(() => On_Left_InnerCircle_LeftClicked?.Invoke());
        leftCircle.OnInnerCircleRightClicked.AddListener(() => On_Left_InnerCircle_RightClicked?.Invoke());
        leftCircle.OnMiddleCircleLeftClicked.AddListener(() => On_Left_MiddleCircle_LeftClicked?.Invoke());
        leftCircle.OnMiddleCircleRightClicked.AddListener(() => On_Left_MiddleCircle_RightClicked?.Invoke());
        leftCircle.OnOuterCircleLeftClicked.AddListener(() => On_Left_OuterCircle_LeftClicked?.Invoke());
        leftCircle.OnOuterCircleRightClicked.AddListener(() => On_Left_OuterCircle_RightClicked?.Invoke());
    }
}
