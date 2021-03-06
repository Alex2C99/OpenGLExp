﻿/*
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
    public struct VertexAttribute
    {       
        public string Name { get; set; }
        public Int32 Size { get; set; }
        public VertexAttribPointerType Type { get; set; } 
        public bool Norm { get; set; }
        public Int32 Stride { get; set; }
        public Int32 Offset { get; set; }         
    }
}
