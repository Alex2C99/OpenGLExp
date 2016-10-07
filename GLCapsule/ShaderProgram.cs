/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 06.10.2016
 * Time: 13:14
 */
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace GLCapsule
{
    /// <summary>
    /// Description of ShaderProgram.
    /// </summary>
    public class ShaderProgram : GLObject
    {
        List<Shader> shaders;
        
        public ShaderProgram()
        {
            Int32 handle = GL.CreateProgram();
            if(!GL.IsProgram(handle))
                throw new GLCapsuleException("Program creation error");
            this.Handle = handle;
            Release = () => {Int32 h = this.Handle; GL.DeleteProgram(h); };
            shaders = new List<Shader>();
        }
        
        public void AddShader(ShaderType type, string source)
        {
            var shader = new Shader(type,source);
            GL.AttachShader(this.Handle,shader.Handle);
            shaders.Add(shader);
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
            foreach(var sh in shaders)
            {
                GL.DetachShader(this.Handle,sh.Handle);
            }
        }
        
        public void Use()
        {
            GL.UseProgram(this.Handle);
        }

        public void Unuse()
        {
            GL.UseProgram(0);
        }
    }
}
