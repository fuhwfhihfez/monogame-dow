using Microsoft.Xna.Framework;

using Dow.Main;

namespace Dow.UI.Utils.Alignment;

static public class Position
{
    static public Vector2 SizeRel(float x, float y, Vector2 parentSize) => parentSize * new Vector2(x, y);
    static public Vector2 ScreenRel(float x, float y) => Values.ScreenDims * new Vector2(x, y);
    static public Vector2 Pixel(float x, float y) => new Vector2(x, y);
}