using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Dow.UI.Decoration;

namespace Dow.UI;

public class Text : IUIElement
{
    public UIContainer Parent { get; set; }
    public string TextString;
    public int ZIndex { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public bool Visible { get; set; }
    public Vector2 Anchor { get; set; }
    public IUIElement InteractionTarget { get; set; }
    public Vector2 AbsolutePosition => Parent == null ? Position : Parent.AbsolutePosition + Position;
    public Vector2 DrawPosition => AbsolutePosition - Size * Anchor;
    public SpriteFont Font;
    public Color TextColor;
    public Func<string> TextUpdater;
    public Func<Vector2> PositionUpdater;
    public Stroke TextStroke;

    public Text(string textString, Vector2 position, SpriteFont font, Color textColor, Vector2 anchor = default, int zIndex = 0, Func<string> textUpdater = null, Func<Vector2> positionUpdater = null, Stroke textStroke = null)
    {
        TextString = textString;
        Position = position;
        Size = font.MeasureString(textString);
        Font = font;
        TextColor = textColor;
        TextUpdater = textUpdater;
        PositionUpdater = positionUpdater;
        TextStroke = textStroke;
        ZIndex = zIndex;

        Anchor = anchor;

        Visible = true;

        Collection.Texts.Add(this);
        Collection.Elements.Add(this);
    }

    public void UpdateText()
    {
        string result = TextUpdater();
        TextString = result;
    }

    public void UpdatePosition()
    {
        Vector2 result = PositionUpdater();
        Position = result;
    }

    public void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.DrawString(
            Font,
            TextString,
            DrawPosition,
            TextColor
        );
    }
}

public enum TextOverflow
{
    Visible,
    Clip,
    Truncate,
    Wrap
}