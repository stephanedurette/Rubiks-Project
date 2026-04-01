using System;
using System.Collections.Generic;

public class Cube
{
    public Face FaceF, FaceR, FaceL, FaceB, FaceU, FaceD;

    public RotationData F_Rotation;

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
    }

    public override string ToString()
    {
        return ($"Front: {FaceF}\nUpper: {FaceU}\nDown: {FaceD}\nLeft: {FaceL}\nRight: {FaceR}\nBack: {FaceB}");
    }

    public void F()
    {
        FaceF.Rotate(RotationDirection.Clockwise);
        F_Rotation.Rotate();
    }

    public class RotationData
    {
        public RotationFace[] RotationFaces;

        public RotationData(RotationFace[] rotationFaces)
        {
            this.RotationFaces = rotationFaces;
        }

        public void Rotate() //clockwise and counterclockwise
        {
            byte[] firstRotationFaceColors = new byte[3];
            for(int i = 0; i < RotationFaces[0].Indexes.Length; i++)
            {
                firstRotationFaceColors[i] = RotationFaces[0].Face.GetColor(RotationFaces[0].Indexes[i]);
            }

            for(int i = 1; i < RotationFaces.Length; i++)
            {
                for (int j = 0; j < RotationFaces[i].Indexes.Length; j++)
                {
                    Face.TransferColor(RotationFaces[i].Face, RotationFaces[i].Indexes[j], RotationFaces[i - 1].Face, RotationFaces[i - 1].Indexes[j]);
                }
            }

            for (int i = 0; i < RotationFaces[^1].Indexes.Length; i++)
            {
                RotationFaces[^1].Face.SetColor(RotationFaces[^1].Indexes[i], firstRotationFaceColors[i]);
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
