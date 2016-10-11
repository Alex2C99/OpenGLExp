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
    
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector3 Xyz;
        public Vector2 Uv;
        public Vector3 Norm;

        /*
        public Vertex(Vector3 _xyz, Vector2 _uv, Vector3 _norm)
        {
            xyz = _xyz;
            uv = _uv;
            norm = _norm;
        }
        
        public Vector3 Xyz { get {return xyz;} }
        public Vector2 Uv { get {return uv;} }
        public Vector3 Norm { get {return norm;} }
*/        
    }
}
