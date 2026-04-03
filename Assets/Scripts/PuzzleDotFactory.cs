using UnityEngine;

public class PuzzleDotFactory : MonoBehaviour
{
    [SerializeField] private GameObject dotPrefab;

    public Dot CreatePuzzleDot(Color color, Vector2 position)
    {
        var d = GameObject.Instantiate(dotPrefab, position, Quaternion.identity).GetComponent<Dot>();
        d.InnerColor = color;
        return d;
    }
}
