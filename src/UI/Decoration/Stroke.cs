using System;
using Microsoft.Xna.Framework;

namespace UI.Decoration;

class Stroke
{
    public float Width;
    public Color Color;
    private float _opacity;
    public float Opacity
    {
        get => _opacity;
        set => _opacity = Math.Clamp(value, 0f, 1f);
    }

    public Stroke(float width, Color color, float opacity = 1f)
    {
        Width = width;
        Color = color;
        Opacity = opacity;
    }
}