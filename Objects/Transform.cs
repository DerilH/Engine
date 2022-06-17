using System.Numerics;
using GlmNet;
namespace UrsaEngine
{
    public class Transform
    {
        public Vector3 position {get; set;} = Vector3.Zero;
        public Vector3 scale {get; set;} = Vector3.One;
        public Quaternion rotation {get; set;} = Quaternion.Identity;
        public Matrix4x4 modelMatrix {get 
        {
            Matrix4x4 trs = Matrix4x4.CreateScale(scale);
            trs = trs * Matrix4x4.CreateFromQuaternion(rotation);
            trs = trs * Matrix4x4.CreateTranslation(position);            
            return trs;
        }}
        public Matrix4x4 worldMatrix {get;} = Matrix4x4.Identity;
        public Transform(){}
    }
}