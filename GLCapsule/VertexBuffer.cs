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
        public VertexBuffer(float[] data)
        {
            Int32 handle;
            GL.GenBuffers(1,out handle);
            if(ErrorCode.NoError != GL.GetError())
                throw new GLCapsuleException("Buffer creation error");
            this.Handle = handle;
            Release = () => { Int32 h = this.Handle; GL.DeleteBuffers(1, ref h); };
            this.Bind();
            GL.BufferData(BufferTarget.ArrayBuffer,sizeof(float)*data.Length,data,BufferUsageHint.StreamDraw);
            this.Unbind();
        }
        
        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer,this.Handle);
        }
        
        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer,0);
        }
    }
}
