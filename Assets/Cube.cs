using System;

public class Cube
{
    public const byte RED = 0x01;
    public const byte BLUE = 0x02;
    public const byte GREEN = 0x04;
    public const byte YELLOW = 0x08;
    public const byte ORANGE = 0x10;
    public const byte WHITE = 0x20;

    public ulong F, R, L, B, U, D;

    private readonly int[] clockWiseRotationFaceIndexRemap = { 5, 3, 0, 6, 1, 7, 4, 2 };
    private readonly int[] counterClockWiseRotationFaceIndexRemap = { 2, 4, 7, 1, 6, 0, 5, 3 };

    private byte GetFaceColor(ulong face, byte index) => (byte)((face >> (index * 8)) & 0xFF);

    enum RotationDirection { Clockwise, CounterClockwise }

    public Cube()
    {
        SetInitialColors();
    }

    private void SetInitialColors()
    {
        FillFace(ref F, GREEN);
        FillFace(ref R, RED);
        FillFace(ref B, BLUE);
        FillFace(ref U, WHITE);
        FillFace(ref D, YELLOW);
        FillFace(ref L, ORANGE);
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

    private void RotateFaceEdges(ref ulong face)
    {

    }
}
