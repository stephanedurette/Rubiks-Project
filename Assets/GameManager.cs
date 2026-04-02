using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        Cube c = new();

        Debug.Log(c);

        var moveList = c.GetRandomMoveList(15);
        
        foreach (var move in moveList)
        {
            move.Execute();
        }

        Debug.Log(c);

        for (int i = moveList.Length - 1; i >= 0; i--) { 
            moveList[i].Reverse();
        }

        Debug.Log(c);
    }
}
