using UrsaEngine.Math;
namespace UrsaEngine.Math
{
    public class Matrix4x4
    {
        public static Matrix4x4 Identity
        {
            get => new Matrix4x4(new float[,]
        {
            {1.0f, 0.0f, 0.0f, 0.0f},
            {0.0f, 1.0f, 0.0f, 0.0f},
            {0.0f, 0.0f, 1.0f, 0.0f},
            {0.0f, 0.0f, 0.0f, 1.0f}
        });
        }
        public static Matrix4x4 Zero { get => new Matrix4x4(0); }

        private float[,] matArr = new float[4, 4];
        public Matrix4x4() { }
        public Matrix4x4(Vector4 vec1, Vector4 vec2, Vector4 vec3, Vector4 vec4)
        {
            matArr[0, 0] = vec1.x;
            matArr[0, 1] = vec1.y;
            matArr[0, 2] = vec1.z;
            matArr[0, 3] = vec1.w;

            matArr[1, 0] = vec2.x;
            matArr[1, 1] = vec2.y;
            matArr[1, 2] = vec2.z;
            matArr[1, 3] = vec2.w;

            matArr[2, 0] = vec3.x;
            matArr[2, 1] = vec3.y;
            matArr[2, 2] = vec3.z;
            matArr[2, 3] = vec3.w;

            matArr[3, 0] = vec4.x;
            matArr[3, 1] = vec4.y;
            matArr[3, 2] = vec4.z;
            matArr[3, 3] = vec4.w;
        }
        public Matrix4x4(int value)
        {
            for (int c = 0; c < 4; c++)
            {
                for (int r = 0; r < 4; r++)
                {
                    matArr[r, c] = value;
                }
            }
        }
        public Matrix4x4(float[,] arr)
        {
            matArr = arr;
        }
        public float this[int col, int row]
        {
            get
            {
                return matArr[row, col];
            }
            set
            {
                matArr[row, col] = value;
            }
        }
        public Vector4 GetRow(int row)
            => new Vector4(matArr[0, row], matArr[1, row], matArr[2, row], matArr[3, row]);
        public Vector4 GetCol(int col)
            => new Vector4(matArr[0, col], matArr[1, col], matArr[2, col], matArr[3, col]);
        public static Matrix4x4 Scale(Vector3 scale)
        {
            Matrix4x4 result = Matrix4x4.Identity;
            result[0, 0] = scale.x;
            result[1, 1] = scale.y;
            result[2, 2] = scale.z;
            return result;
        }
        public static Matrix4x4 Translate(Vector3 translation)
        {
            Matrix4x4 result = Matrix4x4.Identity;
            result[3, 0] = translation.x;
            result[3, 1] = translation.y;
            result[3, 2] = translation.z;
            return result;
        }
        public static Matrix4x4 Rotate(Quaternion rotation)
        {
            //float r0c0 = 2(rotation.w * rotation.w + rotation.x * rotation.x) - 1;
            float r0c0 = rotation.w * rotation.w + rotation.x * rotation.x - rotation.y * rotation.y - rotation.z * rotation.z;
            float r1c0 = 2 * (rotation.x * rotation.y + rotation.w * rotation.z);
            float r2c0 = 2 * (rotation.x * rotation.w - rotation.w * rotation.y);

            //float r0c1 = 2(rotation.x * rotation.y - rotation.w * rotation.z);
            float r0c1 = rotation.w * rotation.w - rotation.x * rotation.x + rotation.y * rotation.y - rotation.z * rotation.z;
            float r1c1 = 2 * (rotation.w * rotation.w + rotation.y * rotation.y) - 1;
            float r2c1 = 2 * (rotation.y * rotation.z + rotation.w * rotation.x);

            //float r0c2 = 2(rotation.x * rotation.z + rotation.w * rotation.y);
            float r0c2 = rotation.w * rotation.w - rotation.x * rotation.x - rotation.y * rotation.y + rotation.z * rotation.z;
            float r1c2 = 2 * (rotation.y * rotation.z - rotation.w * rotation.x);
            float r2c2 = 2 * (rotation.w * rotation.w + rotation.z * rotation.z) - 1;
            Matrix4x4 mat = new Matrix4x4(new float[4, 4]
                {
                    {r0c0, r0c1, r0c2, 0.0f},
                    {r1c0, r1c1, r1c2, 0.0f},
                    {r2c0, r2c1, r2c2, 0.0f},
                    {0.0f, 0.0f, 0.0f, 1.0f}
                });
            return Matrix4x4.Identity * mat;
        }
        public static Matrix4x4 TRS(Vector3 scale, Quaternion rotation, Vector3 translation)
        {
            Matrix4x4 result = Matrix4x4.Identity;
            result *= Scale(scale);
            result *= Rotate(rotation);
            result *= Translate(translation);
            return result;
        }
        public static Matrix4x4 operator *(Matrix4x4 left, Matrix4x4 right)
        {
            float[,] res = new float[4, 4];
            for (int c = 0; c < 4; c++)
            {
                for (int r = 0; r < 4; r++)
                {
                    res[r, c] = left[r, 0] * right[0, c] + left[r, 1] * right[1, c] + left[r, 2] * right[2, c] + left[r, 3] * right[3, c];
                }
            }
            return new Matrix4x4(res);
        }
        public static Matrix4x4 operator *(Matrix4x4 left, float number)
        {
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    left[r, c] *= number;
                }
            }
            return left;
        }
        public static Vector4 operator *(Matrix4x4 left, Vector4 right)
        {
            Vector4 result = new Vector4();
            result[0] = left[0, 0] * right[0] + left[0, 1] * right[0] + left[0, 2] * right[0] + left[0, 3] * right[0];
            result[1] = left[1, 0] * right[0] + left[1, 1] * right[0] + left[1, 2] * right[0] + left[1, 3] * right[0];
            result[2] = left[2, 0] * right[0] + left[2, 1] * right[0] + left[2, 2] * right[0] + left[2, 3] * right[0];
            result[3] = left[3, 0] * right[0] + left[3, 1] * right[0] + left[3, 2] * right[0] + left[3, 3] * right[0];
            return result;
        }
        public static Matrix4x4 operator +(Matrix4x4 left, Matrix4x4 right)
        {
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    left[r, c] += right[r, c];
                }
            }
            return left;
        }
        public static Matrix4x4 operator -(Matrix4x4 left, Matrix4x4 right)
        {
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    left[r, c] -= right[r, c];
                }
            }
            return left;
        }
        public float[,] ToArray() => matArr;
    }
}