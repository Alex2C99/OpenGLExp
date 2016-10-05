/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 05.10.2016
 * Time: 10:28
 */
using System;
using GLCapsule;

namespace OpenGLExp
{
    /// <summary>
    /// Description of CubeModel.
    /// </summary>
    public class CubeModel
    {
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
              
        public CubeModel()
        {
            using(var vao = new VertexArray())
            {
                vao.Bind();
                using(var vbo = new VertexBuffer(data))
                {
                    
                }
            }
        }
    }
}
