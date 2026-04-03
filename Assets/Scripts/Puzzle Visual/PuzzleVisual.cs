using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleVisual : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] private PuzzleDotFactory dotFactory;

    [Header("Internal References")]
    [SerializeField] private TripleCircle leftCircle;
    [SerializeField] private TripleCircle rightCircle;
    [SerializeField] private TripleCircle topCircle;

    #region Events
    [HideInInspector] public UnityEvent On_Left_InnerCircle_RightClicked;
    [HideInInspector] public UnityEvent On_Left_InnerCircle_LeftClicked;
    [HideInInspector] public UnityEvent On_Left_MiddleCircle_RightClicked;
    [HideInInspector] public UnityEvent On_Left_MiddleCircle_LeftClicked; 
    [HideInInspector] public UnityEvent On_Left_OuterCircle_RightClicked;
    [HideInInspector] public UnityEvent On_Left_OuterCircle_LeftClicked;

    [HideInInspector] public UnityEvent On_Top_InnerCircle_RightClicked;
    [HideInInspector] public UnityEvent On_Top_InnerCircle_LeftClicked;
    [HideInInspector] public UnityEvent On_Top_MiddleCircle_RightClicked;
    [HideInInspector] public UnityEvent On_Top_MiddleCircle_LeftClicked;
    [HideInInspector] public UnityEvent On_Top_OuterCircle_RightClicked;
    [HideInInspector] public UnityEvent On_Top_OuterCircle_LeftClicked;

    [HideInInspector] public UnityEvent On_Right_InnerCircle_RightClicked;
    [HideInInspector] public UnityEvent On_Right_InnerCircle_LeftClicked;
    [HideInInspector] public UnityEvent On_Right_MiddleCircle_RightClicked;
    [HideInInspector] public UnityEvent On_Right_MiddleCircle_LeftClicked;
    [HideInInspector] public UnityEvent On_Right_OuterCircle_RightClicked;
    [HideInInspector] public UnityEvent On_Right_OuterCircle_LeftClicked;
    #endregion

    private Dictionary<Transform, Dot> dotPositions;

    private void OnEnable()
    {
        SetupListeners();
    }

    private void Awake()
    {
        SetupPuzzleDots();
    }

    private void SetupListeners()
    {
        leftCircle.OnInnerCircleLeftClicked.AddListener(() => On_Left_InnerCircle_LeftClicked?.Invoke());
        leftCircle.OnInnerCircleRightClicked.AddListener(() => On_Left_InnerCircle_RightClicked?.Invoke());
        leftCircle.OnMiddleCircleLeftClicked.AddListener(() => On_Left_MiddleCircle_LeftClicked?.Invoke());
        leftCircle.OnMiddleCircleRightClicked.AddListener(() => On_Left_MiddleCircle_RightClicked?.Invoke());
        leftCircle.OnOuterCircleLeftClicked.AddListener(() => On_Left_OuterCircle_LeftClicked?.Invoke());
        leftCircle.OnOuterCircleRightClicked.AddListener(() => On_Left_OuterCircle_RightClicked?.Invoke());

        rightCircle.OnInnerCircleLeftClicked.AddListener(() => On_Right_InnerCircle_LeftClicked?.Invoke());
        rightCircle.OnInnerCircleRightClicked.AddListener(() => On_Right_InnerCircle_RightClicked?.Invoke());
        rightCircle.OnMiddleCircleLeftClicked.AddListener(() => On_Right_MiddleCircle_LeftClicked?.Invoke());
        rightCircle.OnMiddleCircleRightClicked.AddListener(() => On_Right_MiddleCircle_RightClicked?.Invoke());
        rightCircle.OnOuterCircleLeftClicked.AddListener(() => On_Right_OuterCircle_LeftClicked?.Invoke());
        rightCircle.OnOuterCircleRightClicked.AddListener(() => On_Right_OuterCircle_RightClicked?.Invoke());

        topCircle.OnInnerCircleLeftClicked.AddListener(() => On_Top_InnerCircle_LeftClicked?.Invoke());
        topCircle.OnInnerCircleRightClicked.AddListener(() => On_Top_InnerCircle_RightClicked?.Invoke());
        topCircle.OnMiddleCircleLeftClicked.AddListener(() => On_Top_MiddleCircle_LeftClicked?.Invoke());
        topCircle.OnMiddleCircleRightClicked.AddListener(() => On_Top_MiddleCircle_RightClicked?.Invoke());
        topCircle.OnOuterCircleLeftClicked.AddListener(() => On_Top_OuterCircle_LeftClicked?.Invoke());
        topCircle.OnOuterCircleRightClicked.AddListener(() => On_Top_OuterCircle_RightClicked?.Invoke());
    }

    private void SetupPuzzleDots()
    {

    }


}
