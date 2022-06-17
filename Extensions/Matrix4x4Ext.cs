using System.Numerics;
namespace UrsaEngine
{
    public static partial class Extensions
    {
        public static float[] ToArray(this Matrix4x4 mat)
        {
            List<float> l = new List<float>();
            l.Add(mat.M11);
            l.Add(mat.M12);
            l.Add(mat.M13);
            l.Add(mat.M14);
            l.Add(mat.M21);
            l.Add(mat.M22);
            l.Add(mat.M23);
            l.Add(mat.M24);
            l.Add(mat.M31);
            l.Add(mat.M32);
            l.Add(mat.M33);
            l.Add(mat.M34);
            l.Add(mat.M41);
            l.Add(mat.M42);
            l.Add(mat.M43);
            l.Add(mat.M44);
            return l.ToArray();
        }
    }
}
