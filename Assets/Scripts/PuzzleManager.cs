using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PuzzleVisual puzzleVisual;

    private Cube cube;

    private enum CubeFace { Front, Back, Left, Right, Top, Bottom }

    private void OnEnable()
    {
        SetupListeners();
    }

    private void Start()
    {
        cube = new();
        cube.Shuffle(25);
        puzzleVisual.SetupPuzzleDots(cube);
    }

    private void SetupListeners()
    {
        puzzleVisual.On_Left_InnerCircle_LeftClicked.AddListener(() => Move(CubeFace.Front, Cube.RotationDirection.Clockwise));
        puzzleVisual.On_Left_InnerCircle_RightClicked.AddListener(() => Move(CubeFace.Front, Cube.RotationDirection.CounterClockwise));
        puzzleVisual.On_Left_OuterCircle_LeftClicked.AddListener(() => Move(CubeFace.Back, Cube.RotationDirection.Clockwise));
        puzzleVisual.On_Left_OuterCircle_RightClicked.AddListener(() => Move(CubeFace.Back, Cube.RotationDirection.CounterClockwise));

        puzzleVisual.On_Right_InnerCircle_LeftClicked.AddListener(() => Move(CubeFace.Right, Cube.RotationDirection.Clockwise));
        puzzleVisual.On_Right_InnerCircle_RightClicked.AddListener(() => Move(CubeFace.Right, Cube.RotationDirection.CounterClockwise));
        puzzleVisual.On_Right_OuterCircle_LeftClicked.AddListener(() => Move(CubeFace.Left, Cube.RotationDirection.Clockwise));
        puzzleVisual.On_Right_OuterCircle_RightClicked.AddListener(() => Move(CubeFace.Left, Cube.RotationDirection.CounterClockwise));

        puzzleVisual.On_Top_InnerCircle_LeftClicked.AddListener(() => Move(CubeFace.Top, Cube.RotationDirection.Clockwise));
        puzzleVisual.On_Top_InnerCircle_RightClicked.AddListener(() => Move(CubeFace.Top, Cube.RotationDirection.CounterClockwise));
        puzzleVisual.On_Top_OuterCircle_LeftClicked.AddListener(() => Move(CubeFace.Bottom, Cube.RotationDirection.Clockwise));
        puzzleVisual.On_Top_OuterCircle_RightClicked.AddListener(() => Move(CubeFace.Bottom, Cube.RotationDirection.CounterClockwise));
    }

    private void Move(CubeFace face, Cube.RotationDirection direction)
    {
        switch (face)
        {
            case CubeFace.Front:
                switch (direction)
                {
                    case Cube.RotationDirection.Clockwise:
                        cube.F.Execute();
                        puzzleVisual.F();
                        break;
                    case Cube.RotationDirection.CounterClockwise:
                        cube.F_.Execute();
                        puzzleVisual.F_();
                        break;
                }
            break;
            case CubeFace.Back:
                switch (direction)
                {
                    case Cube.RotationDirection.Clockwise:
                        cube.B.Execute();
                        puzzleVisual.B();
                        break;
                    case Cube.RotationDirection.CounterClockwise:
                        cube.B_.Execute();
                        puzzleVisual.B_();
                        break;
                }
                break;
            case CubeFace.Left:
                switch (direction)
                {
                    case Cube.RotationDirection.Clockwise:
                        cube.L.Execute();
                        puzzleVisual.L();
                        break;
                    case Cube.RotationDirection.CounterClockwise:
                        cube.L_.Execute();
                        puzzleVisual.L_();
                        break;
                }
                break;
            case CubeFace.Right:
                switch (direction)
                {
                    case Cube.RotationDirection.Clockwise:
                        cube.R.Execute();
                        puzzleVisual.R();
                        break;
                    case Cube.RotationDirection.CounterClockwise:
                        cube.R_.Execute();
                        puzzleVisual.R_();
                        break;
                }
                break;
            case CubeFace.Top:
                switch (direction)
                {
                    case Cube.RotationDirection.Clockwise:
                        cube.U.Execute();
                        puzzleVisual.U();
                        break;
                    case Cube.RotationDirection.CounterClockwise:
                        cube.U_.Execute();
                        puzzleVisual.U_();
                        break;
                }
                break;
            case CubeFace.Bottom:
                switch (direction)
                {
                    case Cube.RotationDirection.Clockwise:
                        cube.D.Execute();
                        puzzleVisual.D();
                        break;
                    case Cube.RotationDirection.CounterClockwise:
                        cube.D_.Execute();
                        puzzleVisual.D_();
                        break;
                }
                break;
        }
    }
}
