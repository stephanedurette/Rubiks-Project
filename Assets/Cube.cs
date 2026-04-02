using System;
using System.Collections.Generic;
using System.Security.Cryptography;

public class Cube
{
    public Face FaceF, FaceR, FaceL, FaceB, FaceU, FaceD;

    public RotationData F_Rotation, R_Rotation, L_Rotation, B_Rotation, U_Rotation, D_Rotation;

    public enum RotationDirection { Clockwise, CounterClockwise }

    public Move F, R, B, L, U, D, F_, R_, B_, L_, U_, D_;

    public List<Move> Moves;

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
                new RotationFace(FaceD, new int[]{ 2, 4, 7}),
                new RotationFace(FaceB, new int[]{ 5, 3, 0}),
                new RotationFace(FaceU, new int[]{ 2, 4, 7}),
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
                new RotationFace(FaceL, new int[]{ 5, 6, 7}),
                new RotationFace(FaceB, new int[]{ 5, 6, 7}),
                new RotationFace(FaceR, new int[]{ 5, 6, 7}),
        });

        F = new(() => { FaceF.Rotate(); F_Rotation.Rotate(); });
        R = new(() => { FaceR.Rotate(); R_Rotation.Rotate(); });
        L = new(() => { FaceL.Rotate(); L_Rotation.Rotate(); });
        U = new(() => { FaceU.Rotate(); U_Rotation.Rotate(); });
        D = new(() => { FaceD.Rotate(); D_Rotation.Rotate(); });
        B = new(() => { FaceB.Rotate(); B_Rotation.Rotate(); });
        F_ = new(() => { FaceF.Rotate(RotationDirection.CounterClockwise); F_Rotation.Rotate(RotationDirection.CounterClockwise); });
        R_ = new(() => { FaceR.Rotate(RotationDirection.CounterClockwise); R_Rotation.Rotate(RotationDirection.CounterClockwise); });
        L_ = new(() => { FaceL.Rotate(RotationDirection.CounterClockwise); L_Rotation.Rotate(RotationDirection.CounterClockwise); });
        U_ = new(() => { FaceU.Rotate(RotationDirection.CounterClockwise); U_Rotation.Rotate(RotationDirection.CounterClockwise); });
        D_ = new(() => { FaceD.Rotate(RotationDirection.CounterClockwise); D_Rotation.Rotate(RotationDirection.CounterClockwise); });
        B_ = new(() => { FaceB.Rotate(RotationDirection.CounterClockwise); B_Rotation.Rotate(RotationDirection.CounterClockwise); });

        F.ReverseAction = F_.ExecuteAction;
        R.ReverseAction = R_.ExecuteAction;
        L.ReverseAction = L_.ExecuteAction;
        U.ReverseAction = U_.ExecuteAction;
        D.ReverseAction = D_.ExecuteAction;
        B.ReverseAction = B_.ExecuteAction;
        F_.ReverseAction = F.ExecuteAction;
        R_.ReverseAction = R.ExecuteAction;
        L_.ReverseAction = L.ExecuteAction;
        U_.ReverseAction = U.ExecuteAction;
        D_.ReverseAction = D.ExecuteAction;
        B_.ReverseAction = B.ExecuteAction;

        Moves = new() { F, R, B, L, U, D, F_, R_, B_, L_, U_, D_ };
    }

    public Move[] GetRandomMoveList(int count)
    {
        Random rnd = new Random();

        Move[] list = new Move[count];

        for(int i = 0; i < count; i++)
        {
            list[i] = Moves[rnd.Next(0, Moves.Count)];
        }

        return list;
    }

    public override string ToString()
    {
        return ($"---- CUBE STATE ----\n-- Front --\n{FaceF}\n-- Upper --\n{FaceU}\n-- Down --\n{FaceD}\n-- Left --\n{FaceL}\n-- Right --\n{FaceR}\n-- Back --\n{FaceB}");
    }

    public bool IsSolved()
    {
        if (!FaceF.IsSolved) return false;
        if (!FaceR.IsSolved) return false;
        if (!FaceL.IsSolved) return false;
        if (!FaceU.IsSolved) return false;
        if (!FaceD.IsSolved) return false;
        if (!FaceB.IsSolved) return false;
        return true;
    }

    public class Move
    {
        public Action ExecuteAction;
        public Action ReverseAction;

        public Move(Action ExecuteAction)
        {
            this.ExecuteAction = ExecuteAction;
        }

        public void Execute() => ExecuteAction?.Invoke();

        public void Reverse() => ReverseAction?.Invoke();
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
            { RotationDirection.CounterClockwise, new int[]{ 2, 4, 7, 1, 6, 0, 3, 5 } }
        };

        private ulong Value;

        private ulong solvedValue;

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

            solvedValue = Value;
        }

        public bool IsSolved => Value == solvedValue;

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
            Dictionary<byte, string> colorNames = new Dictionary<byte, string>()
            {
                { ColorRed, "Red"}, { ColorBlue, "Blue"}, { ColorGreen, "Green"}, { ColorYellow, "Yellow"}, { ColorWhite, "White"}, { ColorOrange, "Orange"}
            };

            return $"{colorNames[GetColor(0)]} {colorNames[GetColor(1)]} {colorNames[GetColor(2)]}\n{colorNames[GetColor(3)]} CENTER {colorNames[GetColor(4)]}\n{colorNames[GetColor(5)]} {colorNames[GetColor(6)]} {colorNames[GetColor(7)]}";
        }
    }
}
