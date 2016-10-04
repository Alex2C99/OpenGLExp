/*
 * Created by SharpDevelop.
 * User: alex
 * Date: 18.09.2016
 * Time: 20:57
 */
using System;
using NLog;
using GLCapsule;

namespace OpenGLExp
{
	class OGLProgram
	{
		static readonly Logger logger = LogManager.GetCurrentClassLogger();
		
		public static void Main(string[] args)
		{
			try
			{
				logger.Debug("App started");
				using(var mainWin = new MainWindow())
				{
				    logger.Debug("Main loop start");
				    mainWin.Run(30,0);
                    logger.Debug("Main loop stop");
				}
			}
			catch(Exception ex)
			{
				logger.Error(ex,"Not handled exception through main");
			}
		}
	}
}
