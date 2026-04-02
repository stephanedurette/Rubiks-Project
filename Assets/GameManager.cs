using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        Cube c = new();
        Debug.Log(c);
        c.B_.Execute();
        Debug.Log(c);
    }
}
