namespace UrsaEngine.Math
{
    public class Matrix4x4
    {
        public static Matrix4x4 Identity { get; } = new Matrix4x4(new float[4, 4]
        {
            1.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 1.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 1.0f,
        });
        public static Matrix4x4 Zero { get; } = new Matrix4x4(0);

        private float[,] matArr = new float[4, 4];
        public Matrix4x4() { }
        public Matrix4x4(Vector4 vec1, Vector4 vec2, Vector4 vec3, Vector4 vec4)
        {
            matArr[0][0] = vec1.x;
            matArr[0][1] = vec1.y;
            matArr[0][2] = vec1.z;
            matArr[0][3] = vec1.w;

            matArr[1][0] = vec2.x;
            matArr[1][1] = vec2.y;
            matArr[1][2] = vec2.z;
            matArr[1][3] = vec2.w;

            matArr[2][0] = vec3.x;
            matArr[2][1] = vec3.y;
            matArr[2][2] = vec3.z;
            matArr[2][3] = vec3.w;

            matArr[3][0] = vec4.x;
            matArr[3][1] = vec4.y;
            matArr[3][2] = vec4.z;
            matArr[3][3] = vec4.w;
        }
        public Matrix4x4(int value)
        {
            for (int c = 0; c < 4; c++)
            {
                for (int r = 0; r < 4; r++)
                {
                    matArr[r][c] = value;
                }
            }
        }
        public Matrix4x4(float[4, 4] arr)
        {
            arr.CopyTo(matArr);
        }
        public float this[int col, int row]
        {
            get
            {
                return matArr[row][col];
            }
            set
            {
                matArr[row][col] = value;
            }
        }
        public Vector4 GetRow(int row)
            => new Vector4(matArr[0][row], matArr[1][row], matArr[2][row], matArr[3][row]);
        public Vector4 GetCol(int col)
            => new Vector4(matArr[0][row], matArr[1][row], matArr[2][row], matArr[3][row]);
        public float[4,4] ToArray() => matArr;
    }
}