using GLFW;
using GLFW.Game;

namespace UrsaEngine.Rendering.OpenGL
{
    public class OpenGLRenderer : BaseRenderer
    {
        public static NativeWindow? _window {get; private set;}
        private bool _started = false;

        public override event OnRenderDelegate? OnRender;

        public OpenGLRenderer(string WindowTitle, int WindowWidth, int WindowHeight) : base(WindowTitle, WindowHeight, WindowWidth)
        {
            Glfw.Init();

            OnRender += () => {};
        }

        public override void StartRender()
        {
            if (_started) throw new System.Exception("Rendering alredy running");
            _started = true;
            RenderThread = Thread.CurrentThread;

            using (NativeWindow window = new NativeWindow(WindowHeight, WindowWidth, WindowTitle))
            {
                _window = window;
                
                while (!window.IsClosing)
                {
                    RenderFrame();
                    OnRender.Invoke();
                }

                Engine.instance.Stop();
            }
        }

        private void RenderFrame()
        {
            _window.SwapBuffers();

            Glfw.PollEvents();
        }
    }
}