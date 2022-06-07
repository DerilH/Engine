using UrsaEngine.Rendering.OpenGL;
namespace UrsaEngine
{
    public class Object : IGLRenderable
    {
        public static Object Triangle = new Object(), Square = new Object(), Circle = new Object(), Cube = new Object(), Sphere = new Object();
        float[] IGLRenderable.vertices { get; set; } = new float[]{};
        int IGLRenderable.verticesCount { get; set; }
        uint IGLRenderable.VAO { get; set; } = 0;
        uint IGLRenderable.VBO { get; set; } = 0;

        public static Object Instantiate(Object original)
        {
            Object clone = original.Clone();
            Engine.instance.renderer.AddToRender(clone);
            return clone;
        }
        
        public Object Clone()
        {
            return (Object)MemberwiseClone();
        }
    }
}