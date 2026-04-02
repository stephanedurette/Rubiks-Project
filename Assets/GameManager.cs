using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        var c = new Cube();
        //TestSequence(c, new Cube.Move[] { c.F, c.R, c.D}); //doesn't work
        //TestSequence(c, new Cube.Move[] { c.R, c.D }); //doesn't work

        //TestSequence(c, new Cube.Move[] { c.R, c.U }); //doesn't work
        //TestSequence(c, new Cube.Move[] { c.F, c.U }); //works
        //TestSequence(c, new Cube.Move[] { c.F, c.R }); //works
        //TestSequence(c, new Cube.Move[] { c.D }); //works
        //TestSequence(c, new Cube.Move[] { c.R }); //works

        //TestSequence(c, new Cube.Move[] { c.F, c.L, c.D, c.L_, c.U, c.B, c.D_}); //doesn't work

        Debug.Log(c);
        c.F.Execute();
        Debug.Log(c);
        c.F.Reverse();
        Debug.Log(c);
    }

    private void TestSequence(Cube c, Cube.Move[] moves)
    {
        string[] states = new string[moves.Length];

        string initialState = c.ToString();

        for (int i = 0; i < moves.Length; i++)
        {
            moves[i].Execute();
            states[i] = c.ToString();
            Debug.Log(c.ToString());
        }

        for (int i = moves.Length - 1; i >= 0; i--)
        {
            Debug.Log(c.ToString() == states[i]);
            moves[i].Reverse();
            Debug.Log(c.ToString());
        }

        Debug.Log(c.ToString() == initialState);
    }

    private void TestAllMoves()
    {
        Cube c = new();

        string startingState = c.ToString();

        foreach (var move in c.Moves)
        {
            move.Execute();
            move.Reverse();

            Debug.Log(c.ToString() == startingState);
        }
    }
}
