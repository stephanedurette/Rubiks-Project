using System.Collections.Generic;
using System.Security.Cryptography;

public class Cube
{
    public Face FaceF, FaceR, FaceL, FaceB, FaceU, FaceD;

    public RotationData F_Rotation, R_Rotation, L_Rotation, B_Rotation, U_Rotation, D_Rotation;

    public enum RotationDirection { Clockwise, CounterClockwise }

    public Cube()
    {
        FaceF = new(Face.ColorGreen);
        FaceR = new(Face.ColorRed);
        FaceL = new(Face.ColorOrange);
        FaceB = new(Face.ColorBlue);
        FaceU = new(Face.ColorWhite);
        FaceD = new(Face.ColorYellow);

        F_Rotation = new RotationData(
            new RotationFace[] {
                new RotationFace(FaceU, new int[]{ 5, 6, 7}),
                new RotationFace(FaceL, new int[]{ 7, 4, 2}),
                new RotationFace(FaceD, new int[]{ 2, 1, 0}),
                new RotationFace(FaceR, new int[]{ 0, 3, 5}),
        });

        R_Rotation = new RotationData(
            new RotationFace[] {
                new RotationFace(FaceF, new int[]{ 2, 4, 7}),
                new RotationFace(FaceU, new int[]{ 2, 4, 7}),
                new RotationFace(FaceB, new int[]{ 5, 3, 0}),
                new RotationFace(FaceD, new int[]{ 2, 4, 7}),
        });

        L_Rotation = new RotationData(
            new RotationFace[] {
                new RotationFace(FaceF, new int[]{ 0, 3, 5}),
                new RotationFace(FaceD, new int[]{ 0, 3, 5}),
                new RotationFace(FaceB, new int[]{ 7, 4, 2}),
                new RotationFace(FaceU, new int[]{ 0, 3, 5}),
        });

        B_Rotation = new RotationData(
            new RotationFace[] {
                new RotationFace(FaceR, new int[]{ 2, 4, 7}),
                new RotationFace(FaceD, new int[]{ 7, 6, 5}),
                new RotationFace(FaceL, new int[]{ 5, 3, 0}),
                new RotationFace(FaceU, new int[]{ 0, 1, 2}),
        });

        U_Rotation = new RotationData(
            new RotationFace[] {
                new RotationFace(FaceF, new int[]{ 2, 1, 0}),
                new RotationFace(FaceL, new int[]{ 2, 1, 0}),
                new RotationFace(FaceB, new int[]{ 2, 1, 0}),
                new RotationFace(FaceR, new int[]{ 2, 1, 0}),
        });

        D_Rotation = new RotationData(
            new RotationFace[] {
                new RotationFace(FaceF, new int[]{ 5, 6, 7}),
                new RotationFace(FaceR, new int[]{ 5, 6, 7}),
                new RotationFace(FaceB, new int[]{ 5, 6, 7}),
                new RotationFace(FaceL, new int[]{ 5, 6, 7}),
        });
    }

    public override string ToString()
    {
        return ($"Front: {FaceF}\nUpper: {FaceU}\nDown: {FaceD}\nLeft: {FaceL}\nRight: {FaceR}\nBack: {FaceB}");
    }

    public class D : Move
    {
        public D(Cube c) : base(c)
        {
        }

        public override void Execute()
        {
            c.FaceD.Rotate();
            c.D_Rotation.Rotate();
        }

        public override void Reverse()
        {
            c.FaceD.Rotate(RotationDirection.CounterClockwise);
            c.D_Rotation.Rotate(RotationDirection.CounterClockwise);
        }
    }

    public class L : Move
    {
        public L(Cube c) : base(c)
        {
        }

        public override void Execute()
        {
            c.FaceL.Rotate();
            c.L_Rotation.Rotate();
        }

        public override void Reverse()
        {
            c.FaceL.Rotate(RotationDirection.CounterClockwise);
            c.L_Rotation.Rotate(RotationDirection.CounterClockwise);
        }
    }

    public class U : Move
    {
        public U(Cube c) : base(c)
        {
        }

        public override void Execute()
        {
            c.FaceU.Rotate();
            c.U_Rotation.Rotate();
        }

        public override void Reverse()
        {
            c.FaceU.Rotate(RotationDirection.CounterClockwise);
            c.U_Rotation.Rotate(RotationDirection.CounterClockwise);
        }
    }

    public class R : Move
    {
        public R(Cube c) : base(c)
        {
        }

        public override void Execute()
        {
            c.FaceR.Rotate();
            c.R_Rotation.Rotate();
        }

        public override void Reverse()
        {
            c.FaceR.Rotate(RotationDirection.CounterClockwise);
            c.R_Rotation.Rotate(RotationDirection.CounterClockwise);
        }
    }

    public class F : Move
    {
        public F(Cube c) : base(c)
        {
        }

        public override void Execute()
        {
            c.FaceF.Rotate();
            c.F_Rotation.Rotate();
        }

        public override void Reverse()
        {
            c.FaceF.Rotate(RotationDirection.CounterClockwise);
            c.F_Rotation.Rotate(RotationDirection.CounterClockwise);
        }
    }

    public class B : Move
    {
        public B(Cube c) : base(c)
        {
        }

        public override void Execute()
        {
            c.FaceB.Rotate();
            c.B_Rotation.Rotate();
        }

        public override void Reverse()
        {
            c.FaceB.Rotate(RotationDirection.CounterClockwise);
            c.B_Rotation.Rotate(RotationDirection.CounterClockwise);
        }
    }

    public class D_ : Move
    {
        public D_(Cube c) : base(c)
        {
        }

        public override void Reverse()
        {
            c.FaceD.Rotate();
            c.D_Rotation.Rotate();
        }

        public override void Execute()
        {
            c.FaceD.Rotate(RotationDirection.CounterClockwise);
            c.D_Rotation.Rotate(RotationDirection.CounterClockwise);
        }
    }

    public class L_ : Move
    {
        public L_(Cube c) : base(c)
        {
        }

        public override void Reverse()
        {
            c.FaceL.Rotate();
            c.L_Rotation.Rotate();
        }

        public override void Execute()
        {
            c.FaceL.Rotate(RotationDirection.CounterClockwise);
            c.L_Rotation.Rotate(RotationDirection.CounterClockwise);
        }
    }

    public class U_ : Move
    {
        public U_(Cube c) : base(c)
        {
        }

        public override void Reverse()
        {
            c.FaceU.Rotate();
            c.U_Rotation.Rotate();
        }

        public override void Execute()
        {
            c.FaceU.Rotate(RotationDirection.CounterClockwise);
            c.U_Rotation.Rotate(RotationDirection.CounterClockwise);
        }
    }

    public class R_ : Move
    {
        public R_(Cube c) : base(c)
        {
        }

        public override void Reverse()
        {
            c.FaceR.Rotate();
            c.R_Rotation.Rotate();
        }

        public override void Execute()
        {
            c.FaceR.Rotate(RotationDirection.CounterClockwise);
            c.R_Rotation.Rotate(RotationDirection.CounterClockwise);
        }
    }

    public class F_ : Move
    {
        public F_(Cube c) : base(c)
        {
        }

        public override void Reverse()
        {
            c.FaceF.Rotate();
            c.F_Rotation.Rotate();
        }

        public override void Execute()
        {
            c.FaceF.Rotate(RotationDirection.CounterClockwise);
            c.F_Rotation.Rotate(RotationDirection.CounterClockwise);
        }
    }

    public class B_ : Move
    {
        public B_(Cube c) : base(c)
        {
        }

        public override void Reverse()
        {
            c.FaceB.Rotate();
            c.B_Rotation.Rotate();
        }

        public override void Execute()
        {
            c.FaceB.Rotate(RotationDirection.CounterClockwise);
            c.B_Rotation.Rotate(RotationDirection.CounterClockwise);
        }
    }

    public abstract class Move
    {
        protected Cube c;

        public Move(Cube c)
        {
            this.c = c;
        }

        public abstract void Execute();

        public abstract void Reverse();
    }

    public class RotationData
    {
        public RotationFace[] RotationFaces;

        public RotationData(RotationFace[] rotationFaces)
        {
            this.RotationFaces = rotationFaces;
        }

        public void Rotate(RotationDirection direction = RotationDirection.Clockwise) //clockwise and counterclockwise
        {
            int startIndex = direction == RotationDirection.Clockwise ? 0 : 3;
            int endIndex = direction == RotationDirection.Clockwise ? 3 : 0;
            int incrementer = direction == RotationDirection.Clockwise ? 1 : -1;

            //cache starting edge values
            byte[] firstRotationFaceColors = new byte[3];
            for (int i = 0; i < RotationFaces[startIndex].Indexes.Length; i++)
            {
                firstRotationFaceColors[i] = RotationFaces[startIndex].Face.GetColor(RotationFaces[startIndex].Indexes[i]);
            }

            //loop through edges, replacing values with the next ones
            int iterations = 0;
            int currentIndex = startIndex;
            while (iterations < 3)
            {
                iterations++;
                currentIndex += incrementer;

                for (int j = 0; j < RotationFaces[currentIndex].Indexes.Length; j++)
                {
                    Face.TransferColor(RotationFaces[currentIndex].Face, RotationFaces[currentIndex].Indexes[j], RotationFaces[currentIndex - incrementer].Face, RotationFaces[currentIndex - incrementer].Indexes[j]);
                }
            }

            //set the final edge to the cached values
            for (int i = 0; i < RotationFaces[endIndex].Indexes.Length; i++)
            {
                RotationFaces[endIndex].Face.SetColor(RotationFaces[endIndex].Indexes[i], firstRotationFaceColors[i]);
            }
        }
    }

    public class RotationFace
    {
        public Face Face;
        public int[] Indexes = { 0, 0, 0 };

        public RotationFace(Face face, int[] indices)
        {
            this.Face = face;
            this.Indexes = indices;
        }

    }

    public class Face
    {
        public static readonly byte ColorRed = 0x01;
        public static readonly byte ColorBlue = 0x02;
        public static readonly byte ColorGreen = 0x04;
        public static readonly byte ColorYellow = 0x08;
        public static readonly byte ColorOrange = 0x10;
        public static readonly byte ColorWhite = 0x20;

        public static Dictionary<RotationDirection, int[]> rotationRemaps = new(){
            { RotationDirection.Clockwise, new int[]{ 5, 3, 0, 6, 1, 7, 4, 2 } },
            { RotationDirection.CounterClockwise, new int[]{ 2, 4, 7, 1, 6, 0, 5, 3 } }
        };

        private ulong Value;

        public Face(byte color)
        {
            Fill(color);
        }

        public byte GetColor(int index) => (byte)((Value >> (index * 8)) & 0xFF);

        public void SetColor(int index, byte color) => Value = (Value & ~((ulong)0xFF << index * 8)) | ((ulong)color << (index * 8));

        public static void TransferColor(Face from, int fromIndex, Face to, int toIndex) => to.SetColor(toIndex, from.GetColor(fromIndex));

        public void Fill(byte color)
        {
            for (int i = 0; i < 8; i++)
            {
                Value |= ((ulong)color << (i * 8));
            }
        }

        public void Rotate(RotationDirection direction = RotationDirection.Clockwise)
        {
            ulong newValue = 0;

            for (int i = 0; i < 8; i++)
            {
                byte newIndex = (byte)rotationRemaps[direction][i];
                newValue |= ((ulong)GetColor(newIndex) << (i * 8));
            }

            Value = newValue;
        }

        public override string ToString()
        {
            return $"{Value:x16}";
        }
    }
}
