namespace UrsaEngine.Rendering.OpenGL
{
    internal interface IGLRenderable
    {
        internal float[] vertices {get; set;}
        internal uint VAO {get; set;}
        internal uint VBO {get; set;}
    }
}