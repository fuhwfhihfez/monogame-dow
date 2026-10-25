using Microsoft.Xna.Framework;

namespace Dow.UI.Utils.Alignment;

static public class AnchorPosition
{
    static public Vector2 TopLeft => new Vector2(0, 0);
    static public Vector2 Top => new Vector2(0.5f, 0);
    static public Vector2 TopRight => new Vector2(1, 0);
    static public Vector2 Left => new Vector2(0, 0.5f);
    static public Vector2 Center => new Vector2(0.5f, 0.5f);
    static public Vector2 Right => new Vector2(1, 0.5f);
    static public Vector2 BottomLeft => new Vector2(0, 1);
    static public Vector2 Bottom => new Vector2(0.5f, 1);
    static public Vector2 BottomRight => new Vector2(1, 1);
    static public Vector2 Custom(float x, float y) => new Vector2(x, y);
}