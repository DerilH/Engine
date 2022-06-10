using GLFW;
using GLFW.Game;
using UrsaEngine;
using GlmNet;
using UrsaEngine.Math;
using static OpenGL.GL;
namespace UrsaEngine.Rendering.OpenGL
{
    internal class OpenGLRenderer : BaseRenderer
    {
        public static Window _window { get; private set; }
        public ShaderProgram currentShaderProgram { get; private set; }
        private bool _started = false, _inited = false;
        public override event BasicEvent OnRender = () => {};

        public OpenGLRenderer(string WindowTitle, int WindowWidth, int WindowHeight) : base(WindowTitle, WindowHeight, WindowWidth)
        {
        }

        public override void Init()
        {
            Glfw.Init();
            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Doublebuffer, true);
            Glfw.WindowHint(Hint.Decorated, true);

            CreateWindow();
            string currentDir = Directory.GetCurrentDirectory();
            currentShaderProgram = new ShaderProgram();
            currentShaderProgram.LoadShadersFromFile(currentDir + @"\Rendering\OpenGL\Shaders\BaseVertexShader.vs", currentDir + @"\Rendering\OpenGL\Shaders\BaseFragmentShader.fs");
            currentShaderProgram.Compile();
            currentShaderProgram.Use();
            InitPrimitives();

            _inited = true;
        }

        public override void Start()
        {
            if (!_inited) throw new System.Exception("Renderer does not initialized");
            if (_started) throw new System.Exception("Rendering alredy running");
            RenderThread = Thread.CurrentThread;
            _started = true;

            while (!Glfw.WindowShouldClose(_window))
            {
                RenderFrame();
            }
            Engine.instance.Stop();
        }

        public unsafe override void DrawObject(Object obj)
        {
            (obj as IGLRenderable).texture.Use(0);
            glBindVertexArray((obj as IGLRenderable).VAO);
            Matrix4x4 trs = Matrix4x4.TRS(Vector3.One, Quaternion.Identity, obj.transform.position);
            Matrix4x4 t = Matrix4x4.Translate( new Vector3(0.5f, 0, 0));
            currentShaderProgram.Set<Matrix4x4>("modelMatrix", trs);
            glDrawArrays(GL_TRIANGLES, 0, (obj as IGLRenderable).verticesCount);
        }

        private void RenderFrame()
        {
            glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            glClear(GL_COLOR_BUFFER_BIT);

            foreach(Object obj in objects)
            {
                DrawObject(obj);
            }

            Glfw.PollEvents();
            Glfw.SwapBuffers(_window);
        }
        private void CreateWindow()
        {
            _window = Glfw.CreateWindow(WindowWidth, WindowHeight, WindowTitle, GLFW.Monitor.None, Window.None);
            Glfw.MakeContextCurrent(_window);
            Import(Glfw.GetProcAddress);
        }
        public override void Stop()
        {
            Glfw.SetWindowShouldClose(_window, true);
            Glfw.Terminate();
        }
        private void InitPrimitives()
        {
            Object.Triangle = new Object();
            (Object.Triangle as IGLRenderable).vertices = new float[]
            {
                -0.5f, -0.5f, 0.0f, 0.0f, 0.0f,
                 0.5f, -0.5f, 0.0f, 1.0f, 0.0f,
                 0.0f,  0.5f, 0.0f, 0.5f, 1.0f
            };
            (Object.Triangle as IGLRenderable).verticesCount = 3;
            GLObjectBuilder.CreateGLObject(Object.Triangle);
        }

        public override void AddToRender(Object obj)
        {
            objects.Add(obj);
        }
        public override void RemoveFromRender(Object obj)
        {
            objects.Remove(obj);
        }
    }
}