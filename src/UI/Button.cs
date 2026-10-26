using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

using Dow.UI.Utils.Alignment;

namespace Dow.UI;

public class Button : IUIElement
{
    public UIContainer Parent { get; set; }
    public Text Text;
    public string TextString
    {
        get => Text.TextString;
        set
        {
            Text.TextString = value;
            UpdateTextPosition();
        }
    }

    private Vector2 _size;
    public Vector2 Size
    {
        get => _size;
        set
        {
            _size = value;
            UpdateTextPosition();
        }
    }

    private Vector2 _anchor;
    public Vector2 Anchor
    {
        get => _anchor;
        set
        {
            _anchor = value;
            UpdateTextPosition();
        }
    }
    public int ZIndex { get; set; }
    private Vector2 _position;
    public Vector2 Position
    {
        get => _position;
        set
        {
            _position = value;
            UpdateTextPosition();
        }
    }
    public bool Visible { get; set; }
    public IUIElement InteractionTarget { get; set; }

    public Color BackgroundColor;

    public Action _OnClick;
    public Action<bool> _OnHover;
    public Vector2 AbsolutePosition => Parent == null ? Position : Parent.AbsolutePosition + Position;
    public Vector2 DrawPosition => AbsolutePosition - Size * Anchor;

    public Button(string textString, SpriteFont font, Color textColor, Color backgroundColor, Vector2 size, Vector2 position, Vector2 anchor = default, int zIndex = 0, Func<string> textUpdater = null, Action onClick = null, Action<bool> onHover = null, float backgroundOpacity = 1f)
    {

        Text = new Text(
            textString,
            position,
            font,
            textColor,
            AnchorPosition.TopLeft,
            zIndex + 1,
            textUpdater,
            () => GetTextPosition()
        );

        _position = position;
        Size = size;
        Anchor = anchor;
        BackgroundColor = backgroundColor * backgroundOpacity;

        _OnClick = onClick;
        _OnHover = onHover;
        Visible = true;

        UpdateTextPosition();

        Collection.Buttons.Add(this);
        Collection.Elements.Add(this);
    }

    void UpdateTextPosition()
    {
        Vector2 stringDims = Text.Font.MeasureString(TextString);
        Text.Size = stringDims;
        Text.Position = DrawPosition + (Size - stringDims) / 2f;
        Text.Anchor = AnchorPosition.TopLeft;
    }

    Vector2 GetTextPosition()
    {
        Vector2 stringDims = Text.Font.MeasureString(TextString);
        Text.Size = stringDims;

        return DrawPosition + (Size - stringDims) / 2f;
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
            new RectangleF(DrawPosition.X, DrawPosition.Y, Size.X, Size.Y),
            BackgroundColor
        );
    }
}