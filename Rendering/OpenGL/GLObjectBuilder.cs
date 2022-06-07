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
            GL.glVertexAttribPointer(0, 3, GL.GL_FLOAT, false, 3 * sizeof(float), GL.NULL);
            GL.glEnableVertexAttribArray(0);
        }
    }
}