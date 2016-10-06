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
#version 450 core
void main(void)
{
   gl_Position = ;
}
        ";

        private static readonly string FRAG_SHADER = @"
#version 450 core
void main(void)
{
   gl_Position = ;
}
        ";
        
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
              
        private VertexArray vao;
        private ShaderProgram shaderProgram;
        
        public CubeModel()
        {
            vao = new VertexArray();
            using(var vbo = new VertexBuffer(data))
            {
                vao.AddBuffer(vbo, 
                              new VertexAttribute 
                              {
                                  Name = "glVertex",
                                  Size = 3,
                                  Type = VertexAttribPointerType.Double,
                                  Stride = sizeof(Double)*3,
                                  Offset = 0
                              },
                              new VertexAttribute 
                              {
                                  Name = "glColor",
                                  Size = 3,
                                  Type = VertexAttribPointerType.Double,
                                  Stride = sizeof(Double)*3,
                                  Offset = sizeof(Double)*3,
                              } 
                             );
            }
            Shader sh = new Shader(ShaderType.VertexShader,VERT_SHADER);
            shaderProgram = new ShaderProgram();
        }

        #region IDisposable implementation

        public void Dispose()
        {
            if(null!=vao)
                vao.Dispose();
        }

        #endregion
    }
}
