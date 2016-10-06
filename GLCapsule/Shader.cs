/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 06.10.2016
 * Time: 13:13
 */
using System;
using OpenTK.Graphics.OpenGL;

namespace GLCapsule
{
    /// <summary>
    /// Description of Shader.
    /// </summary>
    public class Shader : GLObject
    {
        public Shader(ShaderType type, string source)
        {
            Int32 handle = GL.CreateShader(type);
            if(0==handle)
                throw new GLCapsuleException("Shader creation error");
            Release = () => {Int32 h = this.Handle; GL.DeleteShader(h); };
            GL.ShaderSource(this.Handle, source);
            GL.CompileShader(this.Handle);
            Int32 status = 1;
            string log = GL.GetShaderInfoLog(this.Handle);
            GL.GetShader(this.Handle, ShaderParameter.CompileStatus, out status);
            if(1!=status)
            {
                this.Dispose();
                throw new GLCapsuleException("Shader compilation error: "+log);
            }
        }
        
    }
}
