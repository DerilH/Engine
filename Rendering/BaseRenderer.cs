namespace UrsaEngine.Rendering
{
    internal abstract class BaseRenderer
    {
        public string WindowTitle { get; protected set; }
        public int WindowWidth { get; protected set; }
        public int WindowHeight { get; protected set; }
        protected Thread? RenderThread {get; set;}
        protected HashSet<Object> objects { get; set; } = new HashSet<Object>();
        public delegate void OnRenderDelegate();
        public abstract event OnRenderDelegate OnRender;
        public BaseRenderer(string WindowTitle, int WindowWidth, int WindowHeight)
        {
            this.WindowTitle = WindowTitle;
            this.WindowWidth = WindowWidth;
            this.WindowHeight = WindowHeight;
        }
        
        public abstract void Start();
        public abstract void Stop();
    }
}