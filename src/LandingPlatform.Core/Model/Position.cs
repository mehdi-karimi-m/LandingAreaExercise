using System;
using System.Collections.Generic;

namespace LandingPlatform.Core.Model
{
    public class Position
    {
        private readonly int _x;
        private readonly int _y;
        private readonly HashSet<string> _perimeter = new();
        public Position(int x, int y)
        {
            _x = x;
            _y = y;
            SetCoordinatePerimeter();
        }
        private void SetCoordinatePerimeter()
        {
            for (var x = _x - 1; x <= _x + 1; x++)
            {
                for (var y = _y - 1; y <= _y + 1; y++)
                {
                    if(x == _x && y == _y) continue;

                    _perimeter.Add(GetString(x, y));
                }
            }
        }

        internal bool IsItOnPerimeter(Position position)
        {
            return _perimeter.Contains(position.ToString());
        }

        public static bool operator <(Position a, Position b)
        {
            if (a == null || b == null) return false;

            return a._x < b._x && a._y < b._y;
        }

        public static bool operator >(Position a, Position b)
        {
            if (a == null || b == null) return false;

            return a._x > b._x && a._y > b._y;
        }

        public static bool operator <=(Position a, Position b)
        {
            if (a == null || b == null) return false;

            return a._x <= b._x && a._y <= b._y;
        }

        public static bool operator >=(Position a, Position b)
        {
            if (a == null || b == null) return false;

            return a._x >= b._x && a._y >= b._y;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Position coordinate) return false;

            return this._x == coordinate._x && this._y == coordinate._y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_x, _y);
        }

        public override string ToString()
        {
            return GetString(_x, _y);
        }
        private static string GetString(int x, int y)
        {
            return $"{x},{y}";
        }
    }
}