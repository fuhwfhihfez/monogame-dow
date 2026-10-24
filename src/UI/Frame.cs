using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace UI;

class Frame : IUIElement
{
    public RectangleF Rectangle;
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

    public Color BackgroundColor;
    private float _opacity = 1f;
    public float Opacity
    {
        get => _opacity;
        set => _opacity = Math.Clamp(value, 0f, 1f);
    }

    public Frame(Vector2 position, Vector2 size, Color backgroundColor, float opacity = 1f)
    {
        Rectangle = new RectangleF(
            position.X,
            position.Y,
            size.X,
            size.Y
        );

        Opacity = (byte)(opacity*255);

        BackgroundColor = backgroundColor * Opacity;

        Visible = true;

        UI.Collection.Elements.Add(this);
    }

    public void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.FillRectangle(
            Rectangle,
            BackgroundColor
        );
    }
}