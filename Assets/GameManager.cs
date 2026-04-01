using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        Cube c = new();
        Debug.Log(c);
        c.F(Cube.RotationDirection.CounterClockwise);
        Debug.Log(c);
    }
}
