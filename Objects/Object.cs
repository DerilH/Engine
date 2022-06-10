using UrsaEngine.Math;
using UrsaEngine.Rendering.OpenGL;
using GlmNet;
namespace UrsaEngine
{
    public class Object : IGLRenderable
    {
        public static Object Triangle = new Object(), Square = new Object(), Circle = new Object(), Cube = new Object(), Sphere = new Object();
        float[] IGLRenderable.vertices { get; set; }
        int IGLRenderable.verticesCount { get; set; } = 0;
        uint IGLRenderable.VAO { get; set; } = 0;
        uint IGLRenderable.VBO { get; set; } = 0;
        Texture IGLRenderable.texture { get; set; }
        public Transform transform {get; set;} = new Transform();

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