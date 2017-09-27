/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 11.10.2016
 * Time: 10:04
 */
using System;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GLCapsule
{
    /// <summary>
    /// Description of Vertex.
    /// </summary>
    
//    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        private readonly Vector3 xyz;
        private readonly Vector2 uv;
        private readonly Vector3 norm;

        public Vertex(Vector3 _xyz, Vector2 _uv, Vector3 _norm)
        {
            xyz = _xyz;
            uv = _uv;
            norm = _norm;
        }

        public static int Size { get { return(3 + 2 + 3) * sizeof(float); } }
        
        public static VertexAttribute[] Attributes 
        {
            get 
            {
                return new [] { new VertexAttribute
                              {
                                  Name = "vPos",
                                  Size = 3,
                                  Type = VertexAttribPointerType.Float,
                                  Stride = Vertex.Size,
                                  Offset = 0,
                                  Norm = false
                              },
                              new VertexAttribute 
                              {
                                  Name = "vUV",
                                  Size = 2,
                                  Type = VertexAttribPointerType.Float,
                                  Stride = Vertex.Size,
                                  Offset = 3*sizeof(float),
                                  Norm = false
                              },
                              new VertexAttribute
                              {
                                  Name = "vNorm",
                                  Size = 3,
                                  Type = VertexAttribPointerType.Float,
                                  Stride = Vertex.Size,
                                  Offset = 5*sizeof(float),
                                  Norm = false
                              }
                };
            }
        }
        
        public Vector3 Xyz { get {return xyz;} }
        public Vector2 Uv { get {return uv;} }
        public Vector3 Norm { get {return norm;} }
    }
}
