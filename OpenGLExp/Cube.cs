/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 05.10.2016
 * Time: 10:28
 */
using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLExp
{
    /// <summary>
    /// Description of Cube.
    /// </summary>
    public class Cube : IDisposable
    {
        private Vector3 location;
        private Quaternion position;
        private double scale;
        private readonly CubeModel model;

        public Cube(Vector3 lc, Vector3 axis, float angle, float sc=1)
        {
            location = lc;
            scale = sc;
            position = Quaternion.FromAxisAngle(axis,angle);
            model = new CubeModel();
        }
        
        public void Render(Matrix4 persp)
        {
            model.Draw(persp, Matrix4.CreateFromQuaternion(position));
        }
        
        public void Rotate(Quaternion r)
        {
            position *= r;
        }

        public void Rotate(Vector3 axis, float angle)
        {
            Rotate(Quaternion.FromAxisAngle(axis,angle));
        }

        #region IDisposable implementation

        public void Dispose()
        {
			if(null!=model)
	            model.Dispose();
        }

        #endregion
    }
}
