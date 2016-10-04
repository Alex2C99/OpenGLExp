/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 04.10.2016
 * Time: 10:14
 */
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace OpenGLExp
{
    /// <summary>
    /// Description of MainWindow.
    /// </summary>
    public class MainWindow : GameWindow
    {
        public MainWindow()
        {
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Title = "Hello OpenTK!";
            GL.ClearColor(Color.CornflowerBlue);
        }
        
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            this.SwapBuffers();
        }
    }
}
