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
        public VertexArray()
        {
            Int32 handle;
            GL.GenVertexArrays(1,out handle);
            if(ErrorCode.NoError != GL.GetError())
                throw new GLCapsuleException("Vertex array creation error");
            this.Handle = handle;
            Release = () => { Int32 h = this.Handle; GL.DeleteVertexArrays(1, ref h); };
        }
        
        public void AddBuffer(VertexBuffer buf, ShaderProgram prog, params VertexAttribute[] attrs)
        {
            this.Bind();
            buf.Bind();
            foreach(VertexAttribute a in attrs)
            {
                Int32 idx = GL.GetAttribLocation(prog.Handle,a.Name);
                GL.VertexAttribPointer(idx,a.Size,a.Type,a.Norm,a.Stride,a.Offset);
                GL.EnableVertexAttribArray(idx);
            }
            buf.Unbind();
            this.Unbind();
        }
        
        public void Bind()
        {
            GL.BindVertexArray(this.Handle);
        }
        
        public void Unbind()
        {
            GL.BindVertexArray(0);
        }
    }
}
