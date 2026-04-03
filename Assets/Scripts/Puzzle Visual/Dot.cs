using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer colorSprite;

    public Color InnerColor { get { return colorSprite.color; } set { colorSprite.color = value; } }
}
