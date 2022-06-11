using GLFW;
using System.IO;
using GlmNet;
using static OpenGL.GL;

namespace UrsaEngine.Rendering.OpenGL
{
    internal class ShaderProgram
    {
        public uint ID { get; private set; }
        public string VertexShaderSrc { get; private set; }
        public string FragmentShaderSrc { get; private set; }
        public bool Compiled { get; private set; } = false;

        public ShaderProgram() { }
        public ShaderProgram(string VertexShaderSrc, string FragmentShaderSrc)
        {
            this.VertexShaderSrc = VertexShaderSrc;
            this.FragmentShaderSrc = FragmentShaderSrc;
        }
        public void LoadShaders(string VertexShaderSrc, string FragmentShaderSrc)
        {
            this.VertexShaderSrc = VertexShaderSrc;
            this.FragmentShaderSrc = FragmentShaderSrc;
            Compiled = false;
        }

        public void LoadShadersFromFile(string VertexSrcPath, string FragmentSrcPath)
        {

            VertexShaderSrc = File.ReadAllText(VertexSrcPath);
            FragmentShaderSrc = File.ReadAllText(FragmentSrcPath);
            Compiled = false;
        }
        public unsafe void Compile()
        {
            if (Compiled) return;
            int success;

            uint VID, FID;
            VID = glCreateShader(GL_VERTEX_SHADER);
            glShaderSource(VID, VertexShaderSrc);
            glCompileShader(VID);

            glGetShaderiv(VID, GL_COMPILE_STATUS, &success);
            if (success == 0)
            {
                string infoLog = glGetShaderInfoLog(VID, 512);
                throw new System.Exception("Vertex shader compilation error: " + infoLog);
            }

            FID = glCreateShader(GL_FRAGMENT_SHADER);
            glShaderSource(FID, FragmentShaderSrc);
            glCompileShader(FID);

            glGetShaderiv(FID, GL_COMPILE_STATUS, &success);
            if (success == 0)
            {
                string infoLog = glGetShaderInfoLog(FID, 512);
                throw new System.Exception("Fragment shader compilation error: " + infoLog);
            }

            ID = glCreateProgram();
            glAttachShader(ID, VID);
            glAttachShader(ID, FID);
            glLinkProgram(ID);

            glGetProgramiv(ID, GL_LINK_STATUS, &success);
            if (success == 0)
            {
                string infoLog = glGetProgramInfoLog(ID, 512);
                throw new System.Exception("Shader program linking error: " + infoLog);
            }

            glDeleteShader(VID);
            glDeleteShader(FID);

            Compiled = true;
        }

        public void Use()
        {
            if (!Compiled) throw new System.Exception("Shader program does not compiled");
            glUseProgram(ID);
        }
        public unsafe void Set<T>(string name, T value)
        {
            int loc = glGetUniformLocation(ID, name);
            Type type = typeof(T);
            if (type == typeof(int) || type == typeof(bool))
            {
                glUniform1i(loc, (int)(object)value);
            }
            else if (type == typeof(float))
            {
                glUniform1f(loc, (float)(object)value);
            }
            else if (type == typeof(vec2))
            {
                glUniform2fv(loc, 1, ((vec2)(object)value).to_array());
            }
            else if (type == typeof(vec3))
            {
                glUniform3fv(loc, 1, ((vec3)(object)value).to_array());
            }
            else if (type == typeof(vec4))
            {
                glUniform3fv(loc, 1, ((vec4)(object)value).to_array());
            }
            else if (type == typeof(mat2))
            {
                glUniformMatrix2fv(loc, 1, false, ((mat2)(object)value).to_array());
            }
            else if (type == typeof(mat3))
            {
                glUniformMatrix3fv(loc, 1, false, ((mat3)(object)value).to_array());
            }
            else if (type == typeof(mat4))
            {
                glUniformMatrix4fv(loc, 1, false, ((mat4)(object)value).to_array());
                UrsaEngine.Logging.DebugLogger.LogArray(((mat4)(object)value).to_array());
            }
            else if (type == typeof(UrsaEngine.Math.Matrix4x4))
            {
                //fixed (float* arr = &(((UrsaEngine.Math.Matrix4x4)(object)value).To2DArray())[0, 0])
                //{
                    glUniformMatrix4fv(loc, 1, false, ((UrsaEngine.Math.Matrix4x4)(object)value).ToArray());
                //}
            }
        }
    }
}