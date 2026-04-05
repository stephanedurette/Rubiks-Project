using System;
using System.Collections.Generic;

public class Cube
{
    public Face FaceF, FaceR, FaceL, FaceB, FaceU, FaceD;

    public RotationData F_Rotation, R_Rotation, L_Rotation, B_Rotation, U_Rotation, D_Rotation;

    public enum CubeFace { Front, Back, Left, Right, Top, Bottom }

    public enum RotationDirection { Clockwise, CounterClockwise }

    public Move F, R, B, L, U, D, F_, R_, B_, L_, U_, D_;

    public List<Move> Moves;

    private Dictionary<CubeFace, Face> faceDict;
    private Dictionary<CubeFace, RotationData> rotationsDict;

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

        F = new(CubeFace.Front, RotationDirection.Clockwise, this);
        F_ = new(CubeFace.Front, RotationDirection.CounterClockwise, this);
        B = new(CubeFace.Back, RotationDirection.Clockwise, this);
        B_ = new(CubeFace.Back, RotationDirection.CounterClockwise, this);
        U = new(CubeFace.Top, RotationDirection.Clockwise, this);
        U_ = new(CubeFace.Top, RotationDirection.CounterClockwise, this);
        D = new(CubeFace.Bottom, RotationDirection.Clockwise, this);
        D_ = new(CubeFace.Bottom, RotationDirection.CounterClockwise, this);
        R = new(CubeFace.Right, RotationDirection.Clockwise, this);
        R_ = new(CubeFace.Right, RotationDirection.CounterClockwise, this);
        L = new(CubeFace.Left, RotationDirection.Clockwise, this);
        L_ = new(CubeFace.Left, RotationDirection.CounterClockwise, this);

        faceDict = new()
        {
            { CubeFace.Front, FaceF},
            { CubeFace.Back, FaceB},
            { CubeFace.Left, FaceL},
            { CubeFace.Right, FaceR},
            { CubeFace.Top, FaceU},
            { CubeFace.Bottom, FaceB},
        };

        rotationsDict = new() {
            { CubeFace.Front, F_Rotation},
            { CubeFace.Back, B_Rotation},
            { CubeFace.Left, L_Rotation},
            { CubeFace.Right, R_Rotation},
            { CubeFace.Top, U_Rotation},
            { CubeFace.Bottom, B_Rotation},
        };

        Moves = new() { F, R, B, L, U, D, F_, R_, B_, L_, U_, D_ };
    }

    public Move[] Shuffle(int moveCount)
    {
        Move[] moves = GetRandomMoveList(moveCount);
        foreach (Move move in moves)
        {
            move.Execute();
        }
        return moves;
    }

    public Move[] GetRandomMoveList(int count)
    {
        Random rnd = new Random();

        Move[] list = new Move[count];

        for (int i = 0; i < count; i++)
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
        private CubeFace face;
        private RotationDirection direction;
        private RotationDirection reverseDirection;
        private Cube cube;

        public Action<CubeFace, RotationDirection> OnExecuted = delegate { };

        public static RotationDirection Reverse(RotationDirection original) => original == RotationDirection.Clockwise ? RotationDirection.CounterClockwise : RotationDirection.Clockwise;

        public Move(CubeFace face, RotationDirection direction, Cube cube)
        {
            this.face = face;
            this.direction = direction;
            this.reverseDirection = Reverse(direction);
            this.cube = cube;
        }

        public void Execute()
        {
            cube.faceDict[face].Rotate(direction);
            cube.rotationsDict[face].Rotate(direction);

            OnExecuted?.Invoke(face, direction);
        }

        public void Reverse(){
            cube.faceDict[face].Rotate(reverseDirection);
            cube.rotationsDict[face].Rotate(reverseDirection);

            OnExecuted?.Invoke(face, reverseDirection);
        }
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

        public byte StartingColor { get; private set; }

        public Face(byte color)
        {
            Fill(color);
            StartingColor = color;
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
