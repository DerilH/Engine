using System;
using System.IO;
using GLFW;
using GLFW.Game;
using UrsaEngine;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine.instance.Init(RenderingLib.OpenGL, "Test", 600, 600);
            Engine.instance.Run();
        }
    }
}