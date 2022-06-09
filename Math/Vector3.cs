namespace UrsaEngine.Math
{
    public struct Vector3
    {
        public static Vector3 Zero { get; } = new Vector3(0, 0, 0);
        public static Vector3 One { get; } = new Vector3(1, 1, 1);
        public static Vector3 Up { get; } = new Vector3(0, 1, 0);
        public static Vector3 Down { get; } = new Vector3(0, -1, 0);
        public static Vector3 Right { get; } = new Vector3(1, 0, 0);
        public static Vector3 Left { get; } = new Vector3(-1, 0, 0);
        public static Vector3 Forward { get; } = new Vector3(0, 0, 1);
        public static Vector3 Back { get; } = new Vector3(0, 0, -1);
        public float x { get; set; } = 0;
        public float y { get; set; } = 0;
        public float z { get; set; } = 0;
        public float magnitude
        {
            get
            {
                return MathF.Sqrt(x * x + y * y + z * z);
            }
        }
        public float sqrMmagnitude
        {
            get
            {
                return x * x + y * y + z * z;
            }
        }
        public Vector3 normalized
        {
            get
            {
                if (magnitude <= 1) return Vector3.Zero;
                return this / magnitude;
            }
        }
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector3(float value)
        {
            this.x = value;
            this.y = value;
            this.z = value;
        }
        public Vector3(Vector2 vec, float z)
        {
            this.x = vec.x;
            this.y = vec.y;
            this.z = z;
        }
        public static Vector3 operator +(Vector3 left, Vector3 right)
             => new Vector3(left.x + right.x, left.y + right.y, left.z + right.z);
        public static Vector3 operator -(Vector3 left, Vector3 right)
            => new Vector3(left.x + (-right.x), left.y + (-right.y), left.z + (-right.z));
        public static Vector3 operator *(Vector3 left, float number)
            => new Vector3(left.x * number, left.y * number, left.z * number);
        public static Vector3 operator /(Vector3 left, float number)
        {
            if (number == 0) { throw new DivideByZeroException(); }
            return new Vector3(left.x / number, left.y / number, left.z / number);
        }
        public static bool operator ==(Vector3 left, Vector3 right)
            => (left.x == right.x && left.y == right.y && left.z == right.z) ? true : false;
        public static bool operator !=(Vector3 left, Vector3 right)
            => (left.x != right.x || left.y != right.y || left.z != right.z) ? true : false;
        public static float Distance(Vector3 target, Vector3 start)
            => MathF.Sqrt(MathF.Pow(target.x - start.x, 2) + MathF.Pow(target.y - start.y, 2) + MathF.Pow(target.z - start.z, 2));
        public static float Dot(Vector3 first, Vector3 second)
            => first.magnitude * second.magnitude * first.x * second.x + first.y * second.y + first.z * second.z;
        public static float Angle(Vector3 first, Vector3 second)
            => MathF.Acos(first.x * second.x + first.y * second.y + first.z * second.z);
        public static Vector3 Cross(Vector3 first, Vector3 second)
            => new Vector3(first.y * second.z - first.z * second.y, first.z * second.x - first.x * second.z, first.x * second.y - first.y * second.x);
        public static Vector3 Max(Vector3 first, Vector3 second)
            => new Vector3(MathF.Max(first.x, second.x), MathF.Max(first.y, second.y), MathF.Max(first.z, second.z));
        public static Vector3 Min(Vector3 first, Vector3 second)
            => new Vector3(MathF.Min(first.x, second.x), MathF.Min(first.y, second.y), MathF.Min(first.z, second.z));
        public void Normalize()
            => this = this.normalized;

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;
            return this == (Vector3)obj;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}