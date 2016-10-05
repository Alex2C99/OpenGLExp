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
