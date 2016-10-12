/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 11.10.2016
 * Time: 10:04
 */
using System;
using System.Runtime.InteropServices;
using OpenTK;

namespace GLCapsule
{
    /// <summary>
    /// Description of Vertex.
    /// </summary>
    
//    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public const int Size = (3 + 2 + 3) * 4;  

        private readonly Vector3 xyz;
        private readonly Vector2 uv;
        private readonly Vector3 norm;

        public Vertex(Vector3 _xyz, Vector2 _uv, Vector3 _norm)
        {
            xyz = _xyz;
            uv = _uv;
            norm = _norm;
        }
        
        public Vector3 Xyz { get {return xyz;} }
        public Vector2 Uv { get {return uv;} }
        public Vector3 Norm { get {return norm;} }
    }
}
