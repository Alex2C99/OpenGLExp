/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 06.10.2016
 * Time: 13:14
 */
using System;
using OpenTK.Graphics.OpenGL;

namespace GLCapsule
{
    /// <summary>
    /// Description of ShaderProgram.
    /// </summary>
    public class ShaderProgram : GLObject
    {
        public ShaderProgram()
        {
            Int32 handle = GL.CreateProgram();
            if(0==handle)
                throw new GLCapsuleException("Program creation error");
            Release = () => {Int32 h = this.Handle; GL.DeleteProgram(h); };
        }
        
        public ShaderProgram(Shader[] shaders)
            : this()
        {
            foreach(Shader sh in shaders)
                AddShader(sh);
        }
        
        public void AddShader(Shader shader)
        {
            GL.AttachShader(this.Handle,shader.Handle);
        }
        
        public void Link()
        {
            GL.LinkProgram(this.Handle);
            string log = GL.GetProgramInfoLog(this.Handle);
            Int32 status = 1;
            GL.GetProgram(this.Handle,GetProgramParameterName.LinkStatus, out status);
            if(1!=status)
            {
                this.Dispose();
                throw new GLCapsuleException("Program linking error: "+log);
            }
        }
        
        public void Use()
        {
            GL.UseProgram(this.Handle);
        }
    }
}
