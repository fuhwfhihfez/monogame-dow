using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using UI.Decoration;

namespace UI;

static class Collection
{
    static public List<Text> Texts = [];
    static public List<Button> Buttons = [];
    static public List<Frame> Frames = [];
    static public List<IUIElement> Elements = [];

    public static void Update()
    {
        // Text
        foreach (Text text in Texts)
        {
            if (text.TextUpdater != null)
            {
                text.Update();
            }
        }

        // Button
        foreach (Button button in Buttons)
        {
            button.Update();
        }
    }
}

public interface IUIElement
{
    Vector2 Position { get; set; }
    Vector2 Size { get; }
    bool Visible { get; set; }

    void Draw(SpriteBatch _spriteBatch);
}