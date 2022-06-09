namespace UrsaEngine.Math
{
    public struct Vector4
    {
        public static Vector4 Zero { get; } = new Vector4(0, 0, 0, 0);
        public static Vector4 One { get; } = new Vector4(1, 1, 1, 1);
        public float x { get; set; } = 0;
        public float y { get; set; } = 0;
        public float z { get; set; } = 0;
        public float w { get; set; } = 0;
        public float magnitude
        {
            get
            {
                return MathF.Sqrt(x * x + y * y + z * z + w * w);
            }
        }
        public float sqrMmagnitude
        {
            get
            {
                return x * x + y * y + z * z + w * w;
            }
        }
        public Vector4 normalized
        {
            get
            {
                if (magnitude <= 1) return Vector4.Zero;
                return this / magnitude;//
            }
        }
        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public Vector4(float value)
        {
            this.x = value;
            this.y = value;
            this.z = value;
            this.w = value;
        }
        public Vector4(Vector3 vec, float w)
        {
            this.x = vec.x;
            this.y = vec.y;
            this.y = vec.z;
            this.w = w;
        }
        public static Vector4 operator +(Vector4 left, Vector4 right)
             => new Vector4(left.x + right.x, left.y + right.y, left.z + right.z, left.w + right.w);
        public static Vector4 operator -(Vector4 left, Vector4 right)
            => new Vector4(left.x + (-right.x), left.y + (-right.y), left.z + (-right.z), left.w + (-right.w));
        public static Vector4 operator *(Vector4 left, float number)
            => new Vector4(left.x * number, left.y * number, left.z * number, left.w * number);
        public static Vector4 operator /(Vector4 left, float number)
        {
            if (number == 0) { throw new DivideByZeroException(); }
            return new Vector4(left.x / number, left.y / number, left.z / number, left.w / number);
        }
        public static bool operator ==(Vector4 left, Vector4 right)
            => (left.x == right.x && left.y == right.y && left.z == right.z && left.w == right.w) ? true : false;
        public static bool operator !=(Vector4 left, Vector4 right)
            => (left.x != right.x || left.y != right.y || left.z != right.z || left.w != right.w) ? true : false;
        public static float Distance(Vector4 target, Vector4 start)
            => MathF.Sqrt(MathF.Pow(target.x - start.x, 2) + MathF.Pow(target.y - start.y, 2) + MathF.Pow(target.z - start.z, 2) + MathF.Pow(target.w - start.z, 2));
        public static float Dot(Vector4 first, Vector4 second)
            => first.magnitude * second.magnitude * first.x * second.x + first.y * second.y + first.z * second.z + first.w * second.w;
        public static Vector4 Max(Vector4 first, Vector4 second)
            => new Vector4(MathF.Max(first.x, second.x), MathF.Max(first.y, second.y), MathF.Max(first.z, second.z), MathF.Max(first.w, second.w));
        public static Vector4 Min(Vector4 first, Vector4 second)
            => new Vector4(MathF.Min(first.x, second.x), MathF.Min(first.y, second.y), MathF.Min(first.z, second.z), MathF.Min(first.w, second.w));
        public void Normalize()
            => this = this.normalized;

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;
            return this == (Vector4)obj;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}