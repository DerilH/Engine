using UrsaEngine.Rendering;
using UrsaEngine.Rendering.OpenGL;
using UrsaEngine.Logging;
using UrsaEngine.Physics;

namespace UrsaEngine
{
    public enum RenderingLib
    {
        DirectX,
        OpenGL
    }
    public sealed class Engine
    {
        private static Engine? _instance;
        public static Engine instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Engine();
                }
                return _instance;
            }
        }
        internal BaseRenderer? renderer { get; private set; }
        public bool IsStopping {get; private set;} = false;
        private bool _initialized = false;
        private Engine() { }

        public void Init(RenderingLib lib, string WindowTitle, int WindowHeight, int WindowWidth)
        {
            if (_initialized) throw new Exception("Engine already initialized");
            _initialized = true;
            if (lib == RenderingLib.OpenGL)
            {
                renderer = new OpenGLRenderer(WindowTitle, WindowHeight, WindowWidth);
            }
            else renderer = new OpenGLRenderer(WindowTitle, WindowHeight, WindowWidth);
        }
        public void Run()
        {
            if (!_initialized) throw new Exception("Engine does not initialized");
            Console.WriteLine("Engine starting");

            PhysicsEngine.StartPhysics();
            renderer.Start();

        }

        public void Stop()
        {
            Console.WriteLine("Engine stopping");
            IsStopping = true;

            renderer.Stop();
            PhysicsEngine.Stop();
            Environment.Exit(0);
        }
    }
}