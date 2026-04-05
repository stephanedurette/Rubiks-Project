using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleVisual : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveTime;

    [Header("Starting Colors")]
    [SerializeField] private Color FrontColor;
    [SerializeField] private Color BackColor;
    [SerializeField] private Color LeftColor;
    [SerializeField] private Color RightColor;
    [SerializeField] private Color UpColor;
    [SerializeField] private Color DownColor;

    [Header("References")]
    [SerializeField] private GameObject puzzleDotPrefab;
    [SerializeField] private TripleCircle leftCircle;
    [SerializeField] private TripleCircle rightCircle;
    [SerializeField] private TripleCircle topCircle;
    [SerializeField] private Transform dotParentTransform;

    [Header("Dot Positions")]
    [SerializeField] private Transform[] FrontDotPositions;
    [SerializeField] private Transform[] BackDotPositions;
    [SerializeField] private Transform[] LeftDotPositions;
    [SerializeField] private Transform[] RightDotPositions;
    [SerializeField] private Transform[] UpDotPositions;
    [SerializeField] private Transform[] DownDotPositions;

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
    private Dictionary<Cube.CubeFace, Transform[]> cubeFaceDotPositionMap;

    public static Dictionary<Cube.RotationDirection, int[]> rotationRemaps = new(){
            { Cube.RotationDirection.CounterClockwise, new int[]{ 5, 3, 0, 6, 1, 7, 4, 2 } },
            { Cube.RotationDirection.Clockwise, new int[]{ 2, 4, 7, 1, 6, 0, 3, 5 } }
        };

    private Dictionary<Cube.CubeFace, CubeRotationData> rotationData;

    private bool canMove = true;

    private void Awake()
    {
        cubeFaceDotPositionMap = new()
        {
            { Cube.CubeFace.Front, FrontDotPositions },
            { Cube.CubeFace.Back, BackDotPositions },
            { Cube.CubeFace.Left, LeftDotPositions },
            { Cube.CubeFace.Right, RightDotPositions },
            { Cube.CubeFace.Top, UpDotPositions },
            { Cube.CubeFace.Bottom, DownDotPositions },
        };

        rotationData = new()
        {
            { Cube.CubeFace.Front, 
                new CubeRotationData(leftCircle, new FaceRotationData[]{ 
                    new FaceRotationData(Cube.CubeFace.Left, new int[]{ 7, 4, 2}),
                    new FaceRotationData(Cube.CubeFace.Top, new int[]{ 5, 6, 7}),
                    new FaceRotationData(Cube.CubeFace.Right, new int[]{ 0, 3, 5}),
                    new FaceRotationData(Cube.CubeFace.Bottom, new int[]{ 2, 1, 0}),
                }) 
            },
            { Cube.CubeFace.Left,
                new CubeRotationData(rightCircle, new FaceRotationData[]{
                    new FaceRotationData(Cube.CubeFace.Front, new int[]{ 0, 3, 5}), //configure data here
                    new FaceRotationData(Cube.CubeFace.Top, new int[]{ 0, 3, 5}),
                    new FaceRotationData(Cube.CubeFace.Back, new int[]{ 7, 4, 2}),
                    new FaceRotationData(Cube.CubeFace.Bottom, new int[]{ 0, 3, 5}),
                })
            },
            { Cube.CubeFace.Right,
                new CubeRotationData(rightCircle, new FaceRotationData[]{
                    new FaceRotationData(Cube.CubeFace.Front, new int[]{ 2, 4, 7}),
                    new FaceRotationData(Cube.CubeFace.Top, new int[]{ 2, 4, 7}),
                    new FaceRotationData(Cube.CubeFace.Back, new int[]{ 5, 3, 0}),
                    new FaceRotationData(Cube.CubeFace.Bottom, new int[]{2, 4, 7 }),
                })
            },
            { Cube.CubeFace.Back,
                new CubeRotationData(leftCircle, new FaceRotationData[]{
                    new FaceRotationData(Cube.CubeFace.Left, new int[]{ 5, 3, 0}),
                    new FaceRotationData(Cube.CubeFace.Top, new int[]{ 0, 1, 2}),
                    new FaceRotationData(Cube.CubeFace.Right, new int[]{ 2, 4, 7}),
                    new FaceRotationData(Cube.CubeFace.Bottom, new int[]{ 7, 6, 5}),
                })
            },
            { Cube.CubeFace.Top,
                new CubeRotationData(topCircle, new FaceRotationData[]{
                    new FaceRotationData(Cube.CubeFace.Front, new int[]{ 0, 1, 2}),
                    new FaceRotationData(Cube.CubeFace.Left, new int[]{ 0, 1, 2}),
                    new FaceRotationData(Cube.CubeFace.Back, new int[]{ 0, 1, 2}),
                    new FaceRotationData(Cube.CubeFace.Right, new int[]{ 0, 1, 2}),
                })
            },
            { Cube.CubeFace.Bottom,
                new CubeRotationData(topCircle, new FaceRotationData[]{
                    new FaceRotationData(Cube.CubeFace.Front, new int[]{ 5, 6, 7}),
                    new FaceRotationData(Cube.CubeFace.Left, new int[]{ 5, 6, 7}),
                    new FaceRotationData(Cube.CubeFace.Back, new int[]{ 5, 6, 7}),
                    new FaceRotationData(Cube.CubeFace.Right, new int[]{5, 6, 7 }),
                })
            },
        };
    }

    private Queue<Action> cubeMoveInputBuffer;

    private void OnEnable()
    {
        SetupListeners();
        cubeMoveInputBuffer = new();
    }

    private void Update()
    {
        if (canMove && cubeMoveInputBuffer.TryDequeue(out var a))
        {
            a();
            canMove = false;
        }
    }

    public void Bind(Cube cube)
    {
        foreach (var move in cube.Moves)
        {
            move.OnExecuted += OnCubeMoved;
        }
    }

    public void UnBind(Cube cube)
    {
        foreach (var move in cube.Moves)
        {
            move.OnExecuted -= OnCubeMoved;
        }
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

    private Dot CreatePuzzleDot(Color color, Vector2 position)
    {
        var d = GameObject.Instantiate(puzzleDotPrefab, position, Quaternion.identity).GetComponent<Dot>();
        d.transform.SetParent(dotParentTransform, true);
        d.InnerColor = color;
        return d;
    }

    public void SetupPuzzleDots(Cube cube)
    {
        dotPositions = new();

        PopulateDots(UpDotPositions, dotPositions, cube.FaceU);
        PopulateDots(DownDotPositions, dotPositions, cube.FaceD);
        PopulateDots(LeftDotPositions, dotPositions, cube.FaceL);
        PopulateDots(RightDotPositions, dotPositions, cube.FaceR);
        PopulateDots(FrontDotPositions, dotPositions, cube.FaceF);
        PopulateDots(BackDotPositions, dotPositions, cube.FaceB);
    }

    private void PopulateDots(Transform[] dotPositions, Dictionary<Transform, Dot> dotPositionDict, Cube.Face cubeFace)
    {
        Dictionary<byte, Color> colorMap = new()
        {
            { Cube.Face.ColorBlue, BackColor},
            { Cube.Face.ColorRed, RightColor },
            { Cube.Face.ColorOrange, LeftColor },
            { Cube.Face.ColorWhite, UpColor },
            { Cube.Face.ColorYellow, DownColor },
            { Cube.Face.ColorGreen, FrontColor },
        };

        for (int i = 0; i < dotPositions.Length; i++)
        {
            Color faceColor = i == dotPositions.Length - 1 ? colorMap[cubeFace.StartingColor] : colorMap[cubeFace.GetColor(i)];
            Dot newDot = CreatePuzzleDot(faceColor, dotPositions[i].position);
            dotPositionDict.Add(dotPositions[i], newDot);
        }
    }
    private void RotateFace(Transform[] dotPositionsToMove, Cube.RotationDirection rotationDirection)
    {
        for (int i = 0; i < dotPositionsToMove.Length - 1; i++)
        {
            Transform fromPosition = dotPositions[dotPositionsToMove[i]].transform;
            Transform toPosition = dotPositions[dotPositionsToMove[rotationRemaps[rotationDirection][i]]].transform;
            Dot dot = dotPositions[dotPositionsToMove[i]];

            dot.transform.DOMove(toPosition.position, moveTime).OnComplete(() => { dotPositions[toPosition] = dot; canMove = true; });
        }
    }

    private IEnumerator RotateDot(Transform fromPosition, Transform toPosition, Transform centerPosition)
    {
        Dot dot = dotPositions[fromPosition];

        float radius = Vector3.Distance(dot.transform.position, centerPosition.position);
        float startingAngle = Vector2.Angle((fromPosition.position - centerPosition.position), Vector2.right);
        float endingAngle = Vector2.Angle((toPosition.position - centerPosition.position), Vector2.right);

        float angleSpeed = (endingAngle - startingAngle) / moveTime;

        float currentAnimationTime = 0f;
        while (currentAnimationTime < moveTime)
        {

            currentAnimationTime += Time.deltaTime;

            float easeOutQuadTime = 1f - (1f - currentAnimationTime / moveTime) * (1f - currentAnimationTime / moveTime);

            float newAngle = Mathf.Lerp(startingAngle, endingAngle, easeOutQuadTime);

            float x = Mathf.Cos(newAngle * Mathf.Deg2Rad) * radius + centerPosition.position.x;
            float y = Mathf.Sin(newAngle * Mathf.Deg2Rad) * radius + centerPosition.position.y;
            dot.transform.position = new Vector2(x, y);
            yield return new WaitForEndOfFrame();
        }

        dotPositions[toPosition] = dot;
    }
    public class CubeRotationData
    {
        public TripleCircle TripleCircle;
        public FaceRotationData[] FaceRotations;

        public CubeRotationData(TripleCircle tripleCircle, FaceRotationData[] faceRotations)
        {
            this.TripleCircle = tripleCircle;
            this.FaceRotations = faceRotations;
        }
    }

    public class FaceRotationData
    {
        public Cube.CubeFace Face;
        public int[] Indexes;

        public FaceRotationData(Cube.CubeFace face,  int[] indexes)
        {
            this.Face = face;
            this.Indexes = indexes;
        }
    }

    public void OnCubeMoved(Cube.CubeFace face, Cube.RotationDirection direction)
    {
        cubeMoveInputBuffer.Enqueue(() => { Move(face, direction); });
    }

    private void RotateEdges(CubeRotationData rotationData, Cube.RotationDirection direction)
    {
        int incrementer = 0;
        while (incrementer < 4)
        {
            int currentIndex = direction == Cube.RotationDirection.Clockwise ? incrementer : 4 - incrementer - 1;
            int nextIndex = direction == Cube.RotationDirection.Clockwise ? (currentIndex + 1) % 4 : (currentIndex - 1) % 4;

            for(int i = 0; i < rotationData.FaceRotations[currentIndex].Indexes.Length; i++) {

                int fromFaceIndex = rotationData.FaceRotations[currentIndex].Indexes[i];
                int toFaceIndex = rotationData.FaceRotations[nextIndex].Indexes[i];

                Transform from = cubeFaceDotPositionMap[rotationData.FaceRotations[currentIndex].Face][fromFaceIndex];
                Transform to = cubeFaceDotPositionMap[rotationData.FaceRotations[nextIndex].Face][toFaceIndex];
                TripleCircle circle = rotationData.TripleCircle;

                StartCoroutine(RotateDot(from, to, circle.transform));
            }

            incrementer++;
        }
    }

    private void Move(Cube.CubeFace face, Cube.RotationDirection direction)
    {
        RotateFace(cubeFaceDotPositionMap[face], direction);
        RotateEdges(rotationData[face], direction);
    }



}
