using UrsaEngine.Rendering.OpenGL;
namespace UrsaEngine
{
    public class Object : IGLRenderable
    {
        public static Object Triangle;


        float[] IGLRenderable.vertices { get; set; }
        uint IGLRenderable.VAO { get; set; }
        uint IGLRenderable.VBO { get; set; }
    }
}