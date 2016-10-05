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
    public class CubeModel : IDisposable
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
              
        private VertexArray vao;
        
        public CubeModel()
        {
            vao = new VertexArray();
            vao.Bind();
            using(var vbo = new VertexBuffer(data))
            {

            }
            vao.Unbind();
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
