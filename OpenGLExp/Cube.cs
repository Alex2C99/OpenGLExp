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
        private Matrix4 modelView;
        private readonly CubeModel model;
        private readonly bool ownModel;

        public Cube(Vector3 lc, Vector3 axis, float angle, float sc=1)
        {
            modelView = Matrix4.CreateFromAxisAngle(axis,angle) * Matrix4.CreateTranslation(lc) * Matrix4.CreateScale(sc);;
            modelView.Normalize();
            model = new CubeModel();
            ownModel = true;
        }
        
        public Cube(CubeModel mdl, Vector3 lc, Vector3 axis, float angle, float sc=1)
        {
            modelView = Matrix4.CreateFromAxisAngle(axis,angle) * Matrix4.CreateTranslation(lc) * Matrix4.CreateScale(sc);;
            model = mdl;
            ownModel = false;
        }

        public void Render(Matrix4 persp)
        {        	
        	model.Draw(persp, modelView);
        }
        
        public void Rotate(Vector3 axis, float angle)
        {
        	modelView = Matrix4.CreateFromAxisAngle(axis,angle)*modelView;
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
