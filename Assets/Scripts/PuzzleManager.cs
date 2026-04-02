using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PuzzleVisual puzzleVisual;

    private void OnEnable()
    {
        SetupListeners();
    }

    private void SetupListeners()
    {

    }
}
