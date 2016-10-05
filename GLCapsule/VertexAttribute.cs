/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 05.10.2016
 * Time: 14:15
 */
using System;
using OpenTK.Graphics.OpenGL;

namespace GLCapsule
{
    /// <summary>
    /// Description of VertexAttribute.
    /// </summary>
    public class VertexAttribute
    {
        public VertexAttribute()
        {
        }
        
        public string Name { get; set; }
        public UInt32 Size { get; set; }
        public VertexAttribPointerType Type { get; set; } 
        public bool Norm { get; set; }
        public UInt32 Stride { get; set; }
        public UInt32 Offset { get; set; }         
    }
}
