/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 11.10.2016
 * Time: 10:04
 */
using System;
using OpenTK;

namespace GLCapsule
{
    /// <summary>
    /// Description of Vertex.
    /// </summary>
    public struct Vertex
    {
        private readonly Vector3 xyz;
        private readonly Vector2 uv;
        private readonly Vector4 rgba;
        
        public Vertex(Vector3 _xyz, Vector2 _uv, Vector4 _rgba)
        {
            xyz = _xyz;
            uv = _uv;
            rgba = _rgba;
        }
        
        public Vector3 Xyz { get {return xyz;} }
        public Vector2 Uv { get {return uv;} }
        public Vector4 Rgba { get {return rgba;} }
    }
}
