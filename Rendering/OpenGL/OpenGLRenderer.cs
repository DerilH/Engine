using GLFW;
using GLFW.Game;
using UrsaEngine;
using OpenGL;
namespace UrsaEngine.Rendering.OpenGL
{
    internal class OpenGLRenderer : BaseRenderer
    {
        public static Window _window { get; private set; }
        private bool _started = false;

        public override event OnRenderDelegate? OnRender;

        public OpenGLRenderer(string WindowTitle, int WindowWidth, int WindowHeight) : base(WindowTitle, WindowHeight, WindowWidth)
        {
            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Doublebuffer, true);
            Glfw.WindowHint(Hint.Decorated, true);

            OnRender += () => { };
        }

        public override void Start()
        {
            if (_started) throw new System.Exception("Rendering alredy running");
            _started = true;
            RenderThread = Thread.CurrentThread;

            Window window = Glfw.CreateWindow(WindowWidth, WindowHeight, WindowTitle, GLFW.Monitor.None, Window.None);
            Glfw.MakeContextCurrent(window);

            GL.Import(Glfw.GetProcAddress);
            _window = window;
            uint program = CreateProgram();

            InitPrimitives();
            while (!Glfw.WindowShouldClose(window))
            {
                GL.glUseProgram(program);
                RenderFrame();
                OnRender.Invoke();
            }
            Engine.instance.Stop();
        }

        private void RenderFrame()
        {
            GL.glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.glClear(GL.GL_COLOR_BUFFER_BIT);

            GL.glBindVertexArray((Object.Triangle as IGLRenderable).VAO);
            GL.glDrawArrays(GL.GL_TRIANGLES, 0, 3);

            Glfw.PollEvents();
            Glfw.SwapBuffers(_window);
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
                -0.5f, -0.5f, 0.0f,
                 0.5f, -0.5f, 0.0f,
                 0.0f,  0.5f, 0.0f
            };
            GLObjectCreator.CreateObject(Object.Triangle);
        }

        private static uint CreateProgram()
        {
            var vertex = CreateShader(GL.GL_VERTEX_SHADER, @"#version 330 core
                                                    layout (location = 0) in vec3 pos;

                                                    void main()
                                                    {
                                                        gl_Position = vec4(pos.x, pos.y, pos.z, 1.0);
                                                    }");
            var fragment = CreateShader(GL.GL_FRAGMENT_SHADER, @"#version 330 core
                                                        out vec4 result;

                                                        uniform vec3 color;

                                                        void main()
                                                        {
                                                            result = vec4(color, 1.0);
                                                        } ");

            var program = GL.glCreateProgram();
            GL.glAttachShader(program, vertex);
            GL.glAttachShader(program, fragment);

            GL.glLinkProgram(program);

            GL.glDeleteShader(vertex);
            GL.glDeleteShader(fragment);

            GL.glUseProgram(program);
            return program;
        }
        private static uint CreateShader(int type, string source)
        {
            var shader = GL.glCreateShader(type);
            GL.glShaderSource(shader, source);
            GL.glCompileShader(shader);
            return shader;
        }

    }
}