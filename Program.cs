using System.IO;
using GLFW;
using GLFW.Game;
using UrsaEngine;
using System.Numerics;
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
                newObj.transform.position = new Vector3(0.5f,0,0);
                newObj.transform.scale = new Vector3(0.5f);
                newObj.transform.rotation = Quaternion.CreateFromAxisAngle(new Vector3(0,0,1), UrsaEngine.Math.UMathF.Radians(10));

                Object newObj2 = Object.Instantiate(Object.Triangle);
                newObj2.transform.position = new Vector3(-0.5f,0,0);
                newObj2.transform.scale = new Vector3(0.2f);
                newObj2.transform.rotation = Quaternion.CreateFromAxisAngle(new Vector3(0,0,1), UrsaEngine.Math.UMathF.Radians(10));

            };
            Engine.instance.Run();
        }
    }
}