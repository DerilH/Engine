using GLFW;
using System.IO;
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
    }
}