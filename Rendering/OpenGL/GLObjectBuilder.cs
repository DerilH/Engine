using GLFW;
using GLFW.Game;
using OpenGL;

namespace UrsaEngine.Rendering.OpenGL
{
    static class GLObjectBuilder
    {
        public unsafe static void CreateGLObject(IGLRenderable obj)
        {
            uint VAO , VBO;

            GL.glGenVertexArrays(1, &VAO);
            GL.glGenBuffers(1, &VBO);
            
            obj.VAO = VAO;
            obj.VBO = VBO;

            GL.glBindVertexArray(VAO);

            GL.glBindBuffer(GL.GL_ARRAY_BUFFER, VBO);
            float[] v = obj.vertices;
            fixed(float* vertices = &v[0])
            {
                GL.glBufferData(GL.GL_ARRAY_BUFFER, sizeof(float) * v.Length, vertices, GL.GL_STATIC_DRAW);
            }
            GL.glVertexAttribPointer(0, 3, GL.GL_FLOAT, false, 5 * sizeof(float), (void*)0);
            GL.glEnableVertexAttribArray(0);
            GL.glVertexAttribPointer(1, 2, GL.GL_FLOAT, false, 5 * sizeof(float), (void*)(3 * sizeof(float)));
            GL.glEnableVertexAttribArray(1);
            
            string currentDir = Directory.GetCurrentDirectory();
            obj.texture = new Texture(currentDir + @"\Textures\container.jpg");
        }
    }
}