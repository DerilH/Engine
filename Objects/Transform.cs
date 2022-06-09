using UrsaEngine.Math;
using GlmNet;
namespace UrsaEngine
{
    public class Transform
    {
        public Vector3 position {get; set;} = Vector3.Zero;
        public Vector3 scale {get; set;} = Vector3.Zero;
        public Vector3 rotation {get; set;} = Vector3.Zero;
        public Matrix4x4 modelMatrix {get;}
        public Matrix4x4 worldMatrix {get;}
        public Transform(){}
    }
}