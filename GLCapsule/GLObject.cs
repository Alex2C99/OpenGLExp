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
		private bool disposed;

	    protected delegate void GLResourceRelease();

        protected GLResourceRelease Release
        {
            get;
            set;
        }
	    
        public GLObject()
		{
            Release = () => { return; };
			disposed = false;
		}

		protected void virtual Dispose(bool disposing)
		{
			if(disposed)
				return;
			if(disposing)
			{
	            if(null!=Release)
    	            Release();
			}
			disposed = true;
		}
		
    #region IDisposable implementation
        public void Dispose()
        {
			Dispose(true);
      	    GC.SuppressFinalize(this);
        }
    #endregion
	}
}