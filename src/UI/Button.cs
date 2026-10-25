using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

using Dow.UI.Utils.Alignment;

namespace Dow.UI;

public class Button : IUIElement
{
    public UIContainer Parent { get; set; }
    public Frame Frame;
    public Text Text;
    public string TextString
    {
        get => Text.TextString;
        set => Text.TextString = value;
    }
    public int ZIndex { get; set; }
    public Vector2 Position
    {
        get => Frame.Rectangle.Position;
        set
        {
            Frame.Rectangle.Position = value;
            UpdateTextPosition();
        }
    }
    public Vector2 Size
    {
        get => Frame.Rectangle.Size;
        set => Frame.Rectangle.Size = value;
    }
    public bool Visible { get; set; }
    public Vector2 Anchor { get; set; }

    public Action _OnClick;
    public Action<bool> _OnHover;
    public Vector2 DrawPosition => Position - Size * Anchor;

    public Button(string textString, SpriteFont font, Color textColor, Color backgroundColor, Vector2 size, Vector2 position, int zIndex = 0, Action onClick = null, Action<bool> onHover = null, float backgroundOpacity = 1f)
    {

        Frame = new Frame(
            position,
            size,
            backgroundColor,
            zIndex-1,
            backgroundOpacity
        );

        Text = new Text(
            textString,
            Vector2.Zero,
            font,
            textColor,
            zIndex
        );

        UpdateTextPosition();

        Position = position;
        Size = size;

        _OnClick = onClick;
        _OnHover = onHover;

        Anchor = AnchorPosition.TopLeft;

        Visible = true;

        Collection.Buttons.Add(this);
        Collection.Elements.Add(this);
    }

    void UpdateTextPosition()
    {
        Vector2 stringDims = Text.Font.MeasureString(TextString);
        Vector2 padding = Size / 2f;
        Vector2 offset = stringDims / 2f;

        Text.Position = Position + padding - offset;
    }

    public void OnHover(bool Enter)
    {
        _OnHover?.Invoke(Enter);
    }

    public void OnClick()
    {
        _OnClick?.Invoke();
    }

    public void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.FillRectangle(
            Frame.Rectangle,
            Frame.BackgroundColor
        );

        _spriteBatch.DrawString(
            Text.Font,
            Text.TextString,
            Text.Position,
            Text.TextColor
        );
    }
}