namespace UrsaEngine.Rendering
{
    internal abstract class BaseRenderer
    {
        public string WindowTitle { get; protected set; }
        public int WindowWidth { get; protected set; }
        public int WindowHeight { get; protected set; }
        protected Thread? RenderThread {get; set;}
        protected HashSet<Object> objects { get; set; } = new HashSet<Object>();
        public abstract event BasicEvent OnRender;
        public BaseRenderer(string WindowTitle, int WindowWidth, int WindowHeight)
        {
            this.WindowTitle = WindowTitle;
            this.WindowWidth = WindowWidth;
            this.WindowHeight = WindowHeight;
        }
        public abstract void Init();
        public abstract void Start();
        public abstract void Stop();
        public abstract void DrawObject(Object obj);
        public abstract void AddToRender(Object obj);
        public abstract void RemoveFromRender(Object obj);
    }
}