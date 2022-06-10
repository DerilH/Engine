namespace UrsaEngine.Math
{
    public struct Quaternion
    {
        public static Quaternion Identity { get; } = new Quaternion(0, 0, 0, 1);
        public float x { get; set; } = 0;
        public float y { get; set; } = 0;
        public float z { get; set; } = 0;
        public float w { get; set; } = 0;
        public Quaternion normalized
        {
            get
            {
                float d = System.MathF.Sqrt(w * w + x * x + y * y + z * z);
                if (d <= 1) return Identity;
                return new Quaternion(x / d, y / d, z / d, w / d);
            }
        }
        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    case 3: w = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }
        public Quaternion() { }
        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public Quaternion(Vector3 axis, float w)
        {
            this.x = axis.x;
            this.y = axis.y;
            this.z = axis.z;
            this.w = w;
        }
        public void Set(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public static float Dot(Quaternion left, Quaternion right)
            => left.x * right.x + left.y * right.y + left.z * right.z + left.w * right.w;
        public void SetLookRotation(Vector3 look, Vector3 up)
        {
            look.Normalize();
            up.Normalize();
            if (look == -up)
            {
                return;
            }
            Vector3 cross = Vector3.Cross(look, up);
            this.x = look.x;
            this.y = look.y;
            this.z = look.z;
            this.w = System.MathF.Sqrt(look.sqrMmagnitude + up.sqrMmagnitude  ) + Vector3.Dot(look, up);
        }
        public static Quaternion FromEulers(float x, float y, float z)
        {
            //X: Pitch
            //Y: Yaw
            //Z: Roll
            float cy = System.MathF.Cos(y * 0.5f);
            float sy = System.MathF.Sin(y * 0.5f);
            float cp = System.MathF.Cos(x * 0.5f);
            float sp = System.MathF.Sin(x * 0.5f);
            float cr = System.MathF.Cos(z * 0.5f);
            float sr = System.MathF.Sin(z * 0.5f);

            Quaternion q = new Quaternion();
            q.w = cr * cp * cy + sr * sp * sy;
            q.x = sr * cp * cy - cr * sp * sy;
            q.y = cr * sp * cy + sr * cp * sy;
            q.z = cr * cp * sy - sr * sp * cy;
            return q;
        }
    }
}