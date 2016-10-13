/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 13.10.2016
 * Time: 14:08
 */
using System;
using OpenTK.Graphics.OpenGL;

namespace GLCapsule
{
    /// <summary>
    /// Description of IndexBuffer.
    /// </summary>
    public class IndexBuffer : GLObject
    {
        public IndexBuffer(UInt32[] data)
        {
            this.Handle = GL.GenBuffer();
            if(ErrorCode.NoError!=GL.GetError())
                throw new GLCapsuleException("Buffer creation error");
            Release = () => { Int32 h = this.Handle; GL.DeleteBuffers(1, ref h); };
            this.Bind();
            GL.BufferData(BufferTarget.ElementArrayBuffer,sizeof(UInt32)*data.Length,data,BufferUsageHint.StaticDraw);
            this.Unbind();
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer,this.Handle);
        }
        
        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer,0);
        }
    }
}
