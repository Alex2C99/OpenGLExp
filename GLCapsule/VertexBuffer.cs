/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 05.10.2016
 * Time: 11:04
 */
using System;
using OpenTK.Graphics.OpenGL;

namespace GLCapsule
{
    /// <summary>
    /// Description of VertexBuffer.
    /// </summary>
    public class VertexBuffer : GLObject
    {
        private UInt32 handle;
      
        public VertexBuffer(Double[] data)
        {
            GL.GenBuffers(1,out handle);
            Release = () => GL.DeleteBuffers(1, ref handle);
            this.Bind();
            GL.BufferData(BufferTarget.ArrayBuffer,sizeof(Double)*data.Length,data,BufferUsageHint.StaticDraw);
            this.Unbind();
        }
        
        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer,handle);
        }
        
        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer,0);
        }
    }
}
