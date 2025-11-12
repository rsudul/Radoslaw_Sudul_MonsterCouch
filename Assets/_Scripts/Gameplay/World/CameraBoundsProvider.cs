using UnityEngine;

namespace MonsterCouchTest.Gameplay.World
{
    public sealed class CameraBoundsProvider : IBoundsProvider
    {
        private readonly Camera _camera;
        private readonly float _margin;

        public CameraBoundsProvider(Camera camera, float margin = 0.0f)
        {
            _camera = camera != null ? camera : Camera.main;
            _margin = Mathf.Max(0.0f, margin);
        }

        public Rect GetWorldRect()
        {
            var min = _camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, _camera.nearClipPlane));
            var max = _camera.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, _camera.nearClipPlane));

            var xMin = Mathf.Min(min.x, max.x) + _margin;
            var yMin = Mathf.Min(min.y, max.y) + _margin;
            var xMax = Mathf.Max(min.x, max.x) - _margin;
            var yMax = Mathf.Max(min.y, max.y) - _margin;

            if (xMin > xMax)
            {
                var c = (xMin + xMax) * 0.5f;
                xMin = xMax = c;
            }

            if (yMin > yMax)
            {
                var c = (yMin + yMax) * 0.5f;
                yMin = yMax = c;
            }

            return Rect.MinMaxRect(xMin, yMin, xMax, yMax);
        }

        public Vector3 Clamp(Vector3 position)
        {
            var r = GetWorldRect();
            position.x = Mathf.Clamp(position.x, r.xMin, r.xMax);
            position.y = Mathf.Clamp(position.y, r.yMin, r.yMax);
            return position;
        }
    }
}
