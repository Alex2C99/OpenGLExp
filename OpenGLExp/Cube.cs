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
        private Matrix4 orientation;
        private Matrix4 location;
        private Matrix4 scale;
        private readonly CubeModel model;
        private readonly bool ownModel;

        public Cube(Vector3 lc, Vector3 axis, float angle, float sc=1)
        {
            orientation = Matrix4.CreateFromAxisAngle(axis,angle);
            location = Matrix4.CreateTranslation(lc);
            scale = Matrix4.CreateScale(sc);
            model = new CubeModel();
            ownModel = true;
        }
        
        public Cube(CubeModel mdl, Vector3 lc, Vector3 axis, float angle, float sc=1)
        {
            orientation = Matrix4.CreateFromAxisAngle(axis,angle);
            location = Matrix4.CreateTranslation(lc);
            scale = Matrix4.CreateScale(sc);
            model = mdl;
            ownModel = false;
        }

        public void Render(Matrix4 persp)
        {        	
        	model.Draw(persp, orientation*location);
        }
        
        public void Rotate(Vector3 axis, float angle)
        {
        	orientation = orientation*Matrix4.CreateFromAxisAngle(axis,angle);
        }

        #region IDisposable implementation

        public void Dispose()
        {
			if(ownModel && null!=model)
	            model.Dispose();
        }

        #endregion
    }
}
