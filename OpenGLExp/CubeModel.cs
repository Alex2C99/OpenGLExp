/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 05.10.2016
 * Time: 10:28
 */
using System;
using GLCapsule;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLExp
{
    /// <summary>
    /// Description of CubeModel.
    /// </summary>
    public class CubeModel : IDisposable
    {
        private static readonly string VERT_SHADER = @"
#version 450 core

uniform mat4 proj;
uniform mat4 mdl;

attribute vec3 vPos;
attribute vec3 vCol;
out vec4 vs_color;

void main()
{
   vs_color = vec4(vCol,1.0);
   gl_Position = proj*mdl*vec4(vPos, 1.0);
}";

        private static readonly string FRAG_SHADER = @"
#version 450 core

in vec4 vs_color;
out vec4 fs_color;

void main()
{
   fs_color = vs_color;
}";
        
        private const int VERTEX_ATTR_COUNT = 8;
        
        private static readonly float[] data = {
//           x   y   z   r  g  b
            -1, -1, -1,  1, 0, 0,
            -1, -1,  1,  0, 1, 0,
             1, -1,  1,  0, 0, 1,
             1, -1, -1,  1, 1, 1,
            -1,  1, -1,  0, 1, 1,
            -1,  1,  1,  0, 1, 1,
             1,  1,  1,  1, 1, 0,
             1,  1, -1,  1, 0, 1,
        };
              
        private readonly VertexArray vao;
        private readonly ShaderProgram shaderProgram;
       
        public CubeModel()
        {
            vao = new VertexArray();
            shaderProgram = new ShaderProgram();
            shaderProgram.AddShader(ShaderType.VertexShader,VERT_SHADER);
            shaderProgram.AddShader(ShaderType.FragmentShader,FRAG_SHADER);
            shaderProgram.Link();
            
            using(var vbo = new VertexBuffer(data))
            {
                vao.AddBuffer(vbo, shaderProgram, 
                              new VertexAttribute 
                              {
                                  Name = "vPos",
                                  Size = 3,
                                  Type = VertexAttribPointerType.Float,
                                  Stride = sizeof(float)*6,
                                  Offset = 0,
                                  Norm = false
                              }
                              , new VertexAttribute
                              {
                                  Name = "vCol",
                                  Size = 3,
                                  Type = VertexAttribPointerType.Float,
                                  Stride = sizeof(float)*6,
                                  Offset = sizeof(float)*3,
                                  Norm = false
                              }
 
                             );
            }
           
        }       
        
        public void Draw(Matrix4 persp, Matrix4 model)
        {
            vao.Bind();
            shaderProgram.Use();
            Int32 psp = GL.GetUniformLocation(shaderProgram.Handle,"proj");
            Int32 mdl = GL.GetUniformLocation(shaderProgram.Handle,"mdl");
            GL.UniformMatrix4(psp,false, ref persp);
            GL.UniformMatrix4(mdl,false, ref model);
            GL.DrawArrays(PrimitiveType.Quads,0,VERTEX_ATTR_COUNT);
            shaderProgram.Unuse();
            vao.Unbind();
        }
        
        #region IDisposable implementation
        public void Dispose()
        {
            if(null!=shaderProgram)
                shaderProgram.Dispose();
            if(null!=vao)
                vao.Dispose();
        }
        #endregion
    }
}
