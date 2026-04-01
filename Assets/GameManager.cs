using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        Cube c = new();
        Debug.Log(c);
        c.F();
        Debug.Log(c);
    }
}
