/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 04.10.2016
 * Time: 14:01
 */
using System;
using OpenTK.Graphics.OpenGL;

namespace GLCapsule
{
    /// <summary>
    /// Description of VertexArray.
    /// </summary>
    public class VertexArray : GLObject
    {
        private UInt32 handle;
        
        public VertexArray()
        {
            GL.GenVertexArrays(1,out handle);
            Release = () => GL.DeleteVertexArrays(1, ref handle);
        }
        
        public void AddBuffer(VertexBuffer buf, params VertexAttribute[] attrs)
        {
            this.Bind();
            buf.Bind()
            foreach(VertexAttribute a in attrs)
            {
///                UInt32 idx = GL.GetAttribLocation()
///                GL.VertexAttribIPointer();
///                GL.EnableVertexAttribArray();
            }
            buf.Unbind();
            this.Unbind();
        }
        
        public void Bind()
        {
            GL.BindVertexArray(handle);
        }
        
        public void Unbind()
        {
            GL.BindVertexArray(0);
        }
    }
}
