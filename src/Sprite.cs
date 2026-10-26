using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dow.UI;

public class Sprite : IUIElement
{
    public Texture2D Texture;
    public UIContainer Parent { get; set; }
    public int ZIndex { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public bool Visible { get; set; }
    public Vector2 Anchor { get; set; }
    public IUIElement InteractionTarget { get; set; }
    public Vector2 AbsolutePosition => Parent == null ? Position : Parent.AbsolutePosition + Position;
    public Vector2 DrawPosition => AbsolutePosition - Size * Anchor;

    public Color Tint;
    private float _opacity = 1f;
    public float Opacity
    {
        get => _opacity;
        set => _opacity = Math.Clamp(value, 0f, 1f);
    }

    public Sprite(Texture2D texture, Vector2 position, Color tint = default, Vector2 anchor = default, int zIndex = 0, Vector2 size = default, float opacity = 1f)
    {
        Texture = texture;
        Position = position;
        Size = size;

        if (size == default)
            Size = new Vector2(texture.Width, texture.Height);

        if (tint == default)
            tint = Color.White;

        Tint = tint;
        Opacity = opacity;

        ZIndex = zIndex;
        Anchor = anchor;

        Visible = true;

        Collection.Sprites.Add(this);
        Collection.Elements.Add(this);
    }

    public void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.Draw(
            Texture,
            new Rectangle(
                (int)DrawPosition.X,
                (int)DrawPosition.Y,
                (int)Size.X,
                (int)Size.Y
            ),
            Tint * Opacity
        );
    }
}