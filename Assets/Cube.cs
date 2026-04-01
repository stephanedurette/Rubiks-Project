using System;

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

    public void F()
    {
        RotateFace(ref FaceF);


    }
}
