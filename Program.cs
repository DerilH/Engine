using System.IO;
using GLFW;
using GLFW.Game;
using UrsaEngine;
using UrsaEngine.Math;
using Object = UrsaEngine.Object;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine.instance.Init(RenderingLib.OpenGL, "Test", 600, 600);
            Engine.instance.OnStart += () =>
            {
                Object newObj = Object.Instantiate(Object.Triangle);
                newObj.transform.position = new Vector3(0, 0, 20);
            };
            Engine.instance.Run();
        }
    }
}