using UrsaEngine.Rendering;
using UrsaEngine.Rendering.OpenGL;
using UrsaEngine.Logging;
using UrsaEngine.Physics;

namespace UrsaEngine
{
    public delegate void BasicEvent();
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
        internal BaseRenderer renderer { get; private set; }
        public bool IsStopping {get; private set;} = false;
        private bool _initialized = false;

        public event BasicEvent OnInit = () => {}, OnStart = () => {}, OnStop = () => {};

        private Engine() { }

        public void Init(RenderingLib lib, string WindowTitle, int WindowHeight, int WindowWidth)
        {
            if (_initialized) throw new Exception("Engine already initialized");
            _initialized = true;
            if (lib == RenderingLib.OpenGL)
            {
                renderer = new OpenGLRenderer(WindowTitle, WindowHeight, WindowWidth);
                renderer.Init();
            }
            else renderer = new OpenGLRenderer(WindowTitle, WindowHeight, WindowWidth);

            OnInit.Invoke();
        }
        public void Run()
        {
            if (!_initialized) throw new Exception("Engine does not initialized");
            Console.WriteLine("Engine starting");

            PhysicsEngine.StartPhysics();
            OnStart.Invoke();

            renderer.Start();
        }

        public void Stop()
        {
            if(IsStopping) return;
            Console.WriteLine("Engine stoping");
            IsStopping = true;

            OnStop.Invoke();

            renderer.Stop();
            PhysicsEngine.Stop();
            Environment.Exit(0);
        }
    }
}