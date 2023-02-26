using UnityEngine;

namespace BlackBall
{
    public class ScreenSize
    {
        public static float GetScreenToWorldHeight()
        {
            Vector2 topRightCorner = new Vector2(1, 1);
            Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
            var height = edgeVector.y * 2;
            return height;
        }

        public static float GetScreenToWorldWidth()
        {
            Vector2 topRightCorner = new Vector2(1, 1);
            Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
            var width = edgeVector.x * 2;
            return width;
        }
    }
}