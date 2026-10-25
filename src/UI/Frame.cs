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
    public Vector2 Position
    {
        get => Rectangle.Position;
        set => Rectangle.Position = value;
    }
    public Vector2 Size
    {
        get => Rectangle.Size;
        set => Rectangle.Size = value;
    }
    public bool Visible { get; set; }
    public Vector2 Anchor { get; set; }

    public Color BackgroundColor;
    private float _opacity = 1f;
    public float Opacity
    {
        get => _opacity;
        set => _opacity = Math.Clamp(value, 0f, 1f);
    }

    public Frame(Vector2 position, Vector2 size, Color backgroundColor, int zIndex = 0, float opacity = 1f)
    {
        Rectangle = new RectangleF(
            position.X,
            position.Y,
            size.X,
            size.Y
        );

        Opacity = opacity;

        BackgroundColor = backgroundColor * Opacity;

        ZIndex = zIndex;

        Visible = true;

        Collection.Frames.Add(this);
        Collection.Elements.Add(this);
    }

    public void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.FillRectangle(
            Rectangle,
            BackgroundColor
        );
    }
}