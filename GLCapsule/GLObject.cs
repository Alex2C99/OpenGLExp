/*
 * Created by SharpDevelop.
 * User: alex
 * Date: 18.09.2016
 * Time: 20:58
 */
using System;
using System.Collections.Generic;

namespace GLCapsule
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class GLObject : IDisposable
	{
	    protected delegate void GLResourceRelease();

        protected GLResourceRelease Release
        {
            get;
            set;
        }
	    
        public GLObject()
		{
            Release = () => { return; };
		}
		
    #region IDisposable implementation
        public void Dispose()
        {
            if(null!=Release)
                Release();
            GC.SuppressFinalize(this);
        }
    #endregion
	}
}