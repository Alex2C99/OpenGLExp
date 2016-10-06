/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 05.10.2016
 * Time: 10:28
 */
using System;
using OpenTK;

namespace OpenGLExp
{
    /// <summary>
    /// Description of Cube.
    /// </summary>
    public class Cube : IDisposable
    {
        private Vector3d location;
        private Quaterniond position;
        private double scale;
        private readonly CubeModel model;

        public Cube(Vector3d lc, Vector3d axis, double angle, double sc=1)
        {
            location = lc;
            scale = sc;
            position = Quaterniond.FromAxisAngle(axis,angle);
            model = new CubeModel();
        }
        
        public void rotate(Quaterniond r)
        {
            position *= r;
        }

        public void rotate(Vector3d axis, double angle)
        {
            rotate(Quaterniond.FromAxisAngle(axis,angle));
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
