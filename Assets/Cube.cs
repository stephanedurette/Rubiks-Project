public class Cube
{
    public const byte RED = 0x01;
    public const byte BLUE = 0x02;
    public const byte GREEN = 0x04;
    public const byte YELLOW = 0x08;
    public const byte ORANGE = 0x10;
    public const byte WHITE = 0x20;

    public ulong F, R, L, B, U, D;

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
}
