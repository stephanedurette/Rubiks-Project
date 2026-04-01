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
                new RotationFace(FaceL, new int[]{ 2, 4, 7}),
                new RotationFace(FaceD, new int[]{ 0, 1, 2}),
                new RotationFace(FaceR, new int[]{ 5, 3, 0}),
        });
    }

    public override string ToString()
    {
        return ($"Front: {FaceF}\nUpper: {FaceU}\nDown: {FaceD}\nLeft: {FaceL}\nRight: {FaceR}\nBack: {FaceB}");
    }

    public void F()
    {
        /*
        RotateFace(ref FaceF);

        byte upperEdge5 = GetFaceColor(FaceU, 5);
        byte upperEdge6 = GetFaceColor(FaceU, 6);
        byte upperEdge7 = GetFaceColor(FaceU, 7);

        TransferFaceColor(FaceL, 2, ref FaceU, 5);
        TransferFaceColor(FaceL, 4, ref FaceU, 6);
        TransferFaceColor(FaceL, 7, ref FaceU, 7);

        TransferFaceColor(FaceD, 0, ref FaceL, 2);
        TransferFaceColor(FaceD, 1, ref FaceL, 4);
        TransferFaceColor(FaceD, 2, ref FaceL, 7);

        TransferFaceColor(FaceR, 5, ref FaceD, 0);
        TransferFaceColor(FaceR, 3, ref FaceD, 1);
        TransferFaceColor(FaceR, 0, ref FaceD, 2);

        SetFaceColor(ref FaceR, 0, upperEdge5);
        SetFaceColor(ref FaceR, 3, upperEdge6);
        SetFaceColor(ref FaceR, 5, upperEdge7);
        */
    }

    public class RotationData
    {
        public RotationFace[] RotationFaces;

        public RotationData(RotationFace[] rotationFaces)
        {
            this.RotationFaces = rotationFaces;
        }

        public void Rotate()
        {

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

        public void SetColor(byte index, byte color) => Value |= ((ulong)color << index * 8);

        public static void TransferColor(Face from, byte fromIndex, Face to, byte toIndex) => to.SetColor(toIndex, from.GetColor(fromIndex));

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
