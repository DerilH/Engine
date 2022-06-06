using GLFW;
using GLFW.Game;

namespace UrsaEngine.Rendering.OpenGL
{
    internal class OpenGLRenderer : BaseRenderer
    {
        public static NativeWindow? _window {get; private set;}
        private bool _started = false;

        public override event OnRenderDelegate? OnRender;

        public OpenGLRenderer(string WindowTitle, int WindowWidth, int WindowHeight) : base(WindowTitle, WindowHeight, WindowWidth)
        {
            Glfw.Init();

            OnRender += () => {};
        }

        public override void Start()
        {
            if (_started) throw new System.Exception("Rendering alredy running");
            _started = true;
            RenderThread = Thread.CurrentThread;

            using (NativeWindow window = new NativeWindow(WindowHeight, WindowWidth, WindowTitle))
            {
                _window = window;
                Glfw.MakeContextCurrent(_window);
                
                while (!window.IsClosing)
                {
                    RenderFrame();
                    OnRender.Invoke();
                }
                Engine.instance.Stop();
            }
        }

        public override void Stop()
        {
            Glfw.SetWindowShouldClose(_window, true);
            Glfw.Terminate();
        }
        private void RenderFrame()
        {
            _window.SwapBuffers();
            Glfw.PollEvents();
        }
        
        private void InitPrimitives()
        {
            
        }
    }
}