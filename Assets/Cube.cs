using System;
using Unity.VisualScripting;

public class Cube
{
    public const byte ColorRed = 0x01;
    public const byte ColorBlue = 0x02;
    public const byte ColorGreen = 0x04;
    public const byte ColorYellow = 0x08;
    public const byte ColorOrange = 0x10;
    public const byte ColorWhite = 0x20;

    public ulong FaceF, FaceR, FaceL, FaceB, FaceU, FaceD;

    private readonly int[] clockWiseRotationFaceIndexRemap = { 5, 3, 0, 6, 1, 7, 4, 2 };
    private readonly int[] counterClockWiseRotationFaceIndexRemap = { 2, 4, 7, 1, 6, 0, 5, 3 };

    private byte GetFaceColor(ulong face, byte index) => (byte)((face >> (index * 8)) & 0xFF);
    private void SetFaceColor(ref ulong face, byte index, byte color) => face |= ((ulong)color << index * 8);

    private void TransferFaceColor(ulong fromFace, byte fromIndex, ref ulong toFace, byte toIndex) => SetFaceColor(ref toFace, toIndex, GetFaceColor(fromFace, fromIndex));

    enum RotationDirection { Clockwise, CounterClockwise }

    public Cube()
    {
        SetInitialColors();
    }

    private void SetInitialColors()
    {
        FillFace(ref FaceF, ColorGreen);
        FillFace(ref FaceR, ColorRed);
        FillFace(ref FaceB, ColorBlue);
        FillFace(ref FaceU, ColorWhite);
        FillFace(ref FaceD, ColorYellow);
        FillFace(ref FaceL, ColorOrange);
    }

    private void FillFace(ref ulong face, byte color)
    {
        for (int i = 0; i < 8; i++)
        {
            face |= ((ulong)color << (i * 8));
        }
    }

    private void RotateFace(ref ulong face, RotationDirection direction = RotationDirection.Clockwise) {
        ulong newFace = 0;

        for (int i = 0; i < 8; i++) { 
            byte newIndex = (byte)(direction == RotationDirection.Clockwise ? clockWiseRotationFaceIndexRemap : counterClockWiseRotationFaceIndexRemap)[i];
            newFace |= ((ulong)GetFaceColor(face, newIndex) << (i * 8));
        }

        face = newFace;
    }

    public override string ToString()
    {
        return ($"Front Face:    {FaceF:x16}\nUpper Face:    {FaceU:x16}\nDown Face:    {FaceD:x16}\nLeft Face:    {FaceL:x16}\nRight Face:    {FaceR:x16}\nBack Face:    {FaceB:x16}");
    }

    public void F()
    {
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
    }
}
