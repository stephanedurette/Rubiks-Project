using NUnit.Framework;

public class CubeTest
{
    [Test]
    public void TestAllReverseFunctions()
    {
        Cube c = new();

        foreach (var move in c.Moves)
        {
            move.Execute();
            move.Reverse();

            Assert.IsTrue(c.IsSolved());
        }
    }

    [Test]
    public void TestSequences()
    {
        var c = new Cube();
        TestSequence(c, new Cube.Move[] { c.F, c.R, c.D });
        TestSequence(c, new Cube.Move[] { c.R, c.D }); 

        TestSequence(c, new Cube.Move[] { c.R, c.U }); 
        TestSequence(c, new Cube.Move[] { c.F, c.U }); 
        TestSequence(c, new Cube.Move[] { c.F, c.R }); 
        TestSequence(c, new Cube.Move[] { c.D }); 
        TestSequence(c, new Cube.Move[] { c.R }); 

        TestSequence(c, new Cube.Move[] { c.F, c.L, c.D, c.L_, c.U, c.B, c.D_}); 
    }

    private void TestSequence(Cube c, Cube.Move[] moves)
    {
        string[] states = new string[moves.Length];

        for (int i = 0; i < moves.Length; i++)
        {
            moves[i].Execute();
            states[i] = c.ToString();
        }

        for (int i = moves.Length - 1; i >= 0; i--)
        {
            moves[i].Reverse();
        }

        Assert.IsTrue(c.IsSolved());
    }
}
