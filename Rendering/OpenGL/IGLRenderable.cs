namespace UrsaEngine.Rendering.OpenGL
{
    internal interface IGLRenderable
    {
        internal float[] vertices {get; set;}
        internal int verticesCount {get; set;}
        internal uint VAO {get; set;}
        internal uint VBO {get; set;}
    }
}