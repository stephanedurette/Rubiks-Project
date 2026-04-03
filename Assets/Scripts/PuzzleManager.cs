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
        puzzleVisual.SetupPuzzleDots(cube);
    }

    private void SetupListeners()
    {

    }
}
