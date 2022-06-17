namespace UrsaEngine.Math
{
    public static class UMathF
    {
        public const float PI = 3.14159265359f;
        public static float Radians(float degrees) => degrees * (PI / 180);
        public static float Degrees(float radians) => radians * (180 / PI);
    }
}