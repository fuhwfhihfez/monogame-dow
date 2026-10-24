using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using UI.Decoration;

namespace UI;

class Text : IUIElement
{
    public string TextString;
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public bool Visible { get; set; }
    public SpriteFont Font;
    public Color TextColor;
    public Func<string> TextUpdater;
    public Stroke TextStroke;

    public Text(string textString, Vector2 position, SpriteFont font, Color textColor, Func<string> textUpdater = null, Stroke textStroke = null)
    {
        TextString = textString;
        Position = position;
        Size = font.MeasureString(textString);
        Font = font;
        TextColor = textColor;
        TextUpdater = textUpdater;
        TextStroke = textStroke;
        
        Visible = true;

        UI.Collection.Elements.Add(this);
    }

    public void Update()
    {
        string result = TextUpdater();
        TextString = result;
    }

    public void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.DrawString(
            Font,
            TextString,
            Position,
            TextColor
        );
    }
}