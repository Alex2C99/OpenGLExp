/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 05.10.2016
 * Time: 10:28
 */
using System;
using GLCapsule;
using OpenTK.Graphics.OpenGL;

namespace OpenGLExp
{
    /// <summary>
    /// Description of CubeModel.
    /// </summary>
    public class CubeModel : IDisposable
    {
        private static readonly string VERT_SHADER = @"
#version 130

in vec3 vPos;
in vec3 vCol;
out vec4 vs_color;

void main()
{
   vs_color = vec4(vCol,1.0);
   gl_Position = vec4(vPos, 1.0);
}";

        private static readonly string FRAG_SHADER = @"
#version 130

in vec4 vs_color;
out vec4 fs_color;

void main()
{
   fs_color = vs_color;
}";
        
        private static readonly Double[] data = {
//           x   y   z   r  g  b
            -1, -1,  1,  1, 0, 0,
             1, -1,  1,  0, 1, 0,
             1,  1,  1,  0, 0, 1,
            -1,  1,  1,  1, 1, 1,

             1, -1,  1,  0, 1, 1,
             1, -1, -1,  0, 1, 1,
             1,  1, -1,  1, 1, 0,
             1,  1,  1,  1, 0, 1,

             1, -1, -1,  1, 0, 0,
            -1, -1, -1,  0, 1, 0,
            -1,  1, -1,  0, 0, 1,
             1,  1, -1,  1, 1, 1,

            -1, -1, -1,  0, 1, 1,
            -1, -1,  1,  0, 1, 1,
            -1,  1,  1,  1, 1, 0,
            -1,  1, -1,  1, 0, 1,

            -1,  1,  1,  1, 0, 0,
             1,  1,  1,  0, 1, 0,
             1,  1, -1,  0, 0, 1,
            -1,  1, -1,  1, 1, 1,
            
            -1, -1, -1,  0, 1, 1,
             1, -1, -1,  0, 1, 1,
             1,  -1, 1,  1, 1, 0,
            -1,  -1, 1,  1, 0, 1           
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
                                  Type = VertexAttribPointerType.Double,
                                  Stride = sizeof(Double)*3,
                                  Offset = 0,
                                  Norm = false
                              },
                              new VertexAttribute 
                              {
                                  Name = "vCol",
                                  Size = 3,
                                  Type = VertexAttribPointerType.Double,
                                  Stride = sizeof(Double)*3,
                                  Offset = sizeof(Double)*3,
                                  Norm = false
                              } 
                             );
            }
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
