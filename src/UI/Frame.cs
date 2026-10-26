using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace Dow.UI;

public class Frame : IUIElement
{
    public UIContainer Parent { get; set; }
    public RectangleF Rectangle;
    public int ZIndex { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public bool Visible { get; set; }
    public Vector2 Anchor { get; set; }
    public IUIElement InteractionTarget { get; set; }
    public Vector2 AbsolutePosition => Parent == null ? Position : Parent.AbsolutePosition + Position;
    public Vector2 DrawPosition => AbsolutePosition - Size * Anchor;

    public Color BackgroundColor;
    private float _opacity = 1f;
    public float Opacity
    {
        get => _opacity;
        set => _opacity = Math.Clamp(value, 0f, 1f);
    }

    public Frame(Vector2 position, Vector2 size, Color backgroundColor, Vector2 anchor = default, int zIndex = 0, float opacity = 1f)
    {
        Position = position;
        Size = size;

        Opacity = opacity;

        BackgroundColor = backgroundColor * Opacity;

        ZIndex = zIndex;

        Anchor = anchor;

        Visible = true;

        Collection.Frames.Add(this);
        Collection.Elements.Add(this);
    }

    public void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.FillRectangle(
            new RectangleF(DrawPosition.X, DrawPosition.Y, Size.X, Size.Y),
            BackgroundColor
        );
    }
}