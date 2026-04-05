using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PuzzleVisual puzzleVisual;

    private Cube cube;

    private void OnEnable()
    {
        SetupListeners();
    }

    private void Start()
    {
        cube = new();
        cube.Shuffle(25);
        puzzleVisual.SetupPuzzleDots(cube);
        puzzleVisual.Bind(cube);
    }

    private void SetupListeners()
    {
        puzzleVisual.On_Left_InnerCircle_LeftClicked.AddListener(() => cube.F.Execute());
        puzzleVisual.On_Left_InnerCircle_RightClicked.AddListener(() => cube.F_.Execute());
        puzzleVisual.On_Left_OuterCircle_LeftClicked.AddListener(() => cube.B.Execute());
        puzzleVisual.On_Left_OuterCircle_RightClicked.AddListener(() => cube.B_.Execute());

        puzzleVisual.On_Right_InnerCircle_LeftClicked.AddListener(() => cube.R.Execute());
        puzzleVisual.On_Right_InnerCircle_RightClicked.AddListener(() => cube.R_.Execute());
        puzzleVisual.On_Right_OuterCircle_LeftClicked.AddListener(() => cube.L.Execute());
        puzzleVisual.On_Right_OuterCircle_RightClicked.AddListener(() => cube.L_.Execute());

        puzzleVisual.On_Top_InnerCircle_LeftClicked.AddListener(() => cube.U.Execute());
        puzzleVisual.On_Top_InnerCircle_RightClicked.AddListener(() => cube.U_.Execute());
        puzzleVisual.On_Top_OuterCircle_LeftClicked.AddListener(() => cube.D.Execute());
        puzzleVisual.On_Top_OuterCircle_RightClicked.AddListener(() => cube.D_.Execute());
    }
}
