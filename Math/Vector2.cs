using System;
namespace UrsaEngine.Math
{
    public struct Vector2
    {
        public static Vector2 Zero { get => new Vector2(0, 0); }
        public static Vector2 One { get => new Vector2(1, 1); } 
        public static Vector2 Up { get => new Vector2(0, 1); } 
        public static Vector2 Down { get => new Vector2(0, -1); } 
        public static Vector2 Right { get => new Vector2(1, 0); }
        public static Vector2 Left { get => new Vector2(-1, 0); } 
        public float x { get; set; } = 0;
        public float y { get; set; } = 0;
        public float magnitude
        {
            get
            {
                return MathF.Sqrt(x * x + y * y);
            }
        }
        public float sqrMmagnitude
        {
            get
            {
                return x * x + y * y;
            }
        }
        public Vector2 normalized
        {
            get
            {
                if (magnitude <= 1) return Vector2.Zero;
                return this / magnitude;
            }
        }
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2(float value)
        {
            this.x = value;
            this.y = value;
        }
        public static Vector2 operator +(Vector2 left, Vector2 right)
             => new Vector2(left.x + right.x, left.y + right.y);
        public static Vector2 operator -(Vector2 left, Vector2 right)
            => new Vector2(left.x + (-right.x), left.y + (-right.y));
        public static Vector2 operator-(Vector2 vec) => new Vector2(-vec.x, -vec.y);
        public static Vector2 operator *(Vector2 left, float number)
            => new Vector2(left.x * number, left.y * number);
        public static Vector2 operator /(Vector2 left, float number)
        {
            if (number == 0) { throw new DivideByZeroException(); }
            return new Vector2(left.x / number, left.y / number);
        }
        public static bool operator ==(Vector2 left, Vector2 right)
            => (left.x == right.x && left.y == right.y) ? true : false;
        public static bool operator !=(Vector2 left, Vector2 right)
            => (left.x != right.x || left.y != right.y) ? true : false;
        public static float Distance(Vector2 target, Vector2 start)
            => MathF.Sqrt(MathF.Pow(target.x - start.x, 2) + MathF.Pow(target.y - start.y, 2));
        public static float Dot(Vector2 first, Vector2 second)
            => first.magnitude * second.magnitude * first.x * second.x + first.y * second.y;
        public static float Angle(Vector2 first, Vector2 second)
            => MathF.Acos(first.x * second.x + first.y * second.y);
        public static Vector2 Max(Vector2 first, Vector2 second)
            => new Vector2(MathF.Max(first.x, second.x), MathF.Max(first.y, second.y));
        public static Vector2 Min(Vector2 first, Vector2 second)
            => new Vector2(MathF.Min(first.x, second.x), MathF.Min(first.y, second.y));
        public void Normalize()
            => this = this.normalized;

        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;
            return this == (Vector2)obj;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}