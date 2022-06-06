using UrsaEngine.Rendering.OpenGL;
namespace UrsaEngine
{
    public class Object : IGLRenderable
    {
        public static Object Triangle = new Object(), Square = new Object(), Circle = new Object(), Cube = new Object(), Sphere = new Object();

        float[] IGLRenderable.vertices { get; set; } = new float[]{};
        uint IGLRenderable.VAO { get; set; } = 0;
        uint IGLRenderable.VBO { get; set; } = 0;
    }
}