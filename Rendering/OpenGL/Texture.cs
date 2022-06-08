using StbiSharp;
using static OpenGL.GL;

namespace UrsaEngine.Rendering.OpenGL
{
    internal class Texture 
    {
        public uint ID {get; private set;}
        public string? Path {get; private set;}
        public int Width {get; private set;}
        public int Height {get; private set;}
        public int NumChannels {get; private set;}
        public unsafe Texture(string Path)
        {
            if(!File.Exists(Path)) throw new FileNotFoundException("Invalid path to texture");
            this.Path = Path;
            ID = glGenTexture();
            glBindTexture(GL_TEXTURE_2D, ID);

            StbiImage Image;
            using(var stream = File.OpenRead(Path))
            using(var mStream = new MemoryStream())
            {
                stream.CopyTo(mStream);
                Image = Stbi.LoadFromMemory(mStream, 0);
                Width = Image.Width;
                Height = Image.Height;
                NumChannels = Image.NumChannels;
            }
            fixed(byte* data = &Image.Data.ToArray()[0])
            {
                glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, Width, Height, 0, GL_RGBA, GL_UNSIGNED_BYTE, data);
                glGenerateMipmap(GL_TEXTURE_2D);
            }
            Image.Dispose();
        }
    }
}