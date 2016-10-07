/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 04.10.2016
 * Time: 10:14
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenGLExp
{
    /// <summary>
    /// Description of MainWindow.
    /// </summary>
    public class MainWindow : GameWindow
    {
        private delegate void KeyAction(KeyboardKeyEventArgs e);
        private readonly Dictionary<Key,KeyAction> keys;
        
        private Cube cube1;
        private Matrix4 perspective;
        
        public MainWindow(int w, int h)
            : base(w,h)
        {
            keys = new Dictionary<Key, KeyAction>();
            keys[Key.Escape] = e => this.Dispose();
            keys[Key.F12] = e => this.WindowState = (int)(WindowState.Fullscreen) - this.WindowState;
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Title = "Hello OpenTK!";
            perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, 16f / 9, 0.1f, 10.0f);
            cube1 = new Cube(new Vector3(1,1,1),new Vector3(1,0,0),MathHelper.Pi/4);
        }
        
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.ClearColor(Color.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            cube1.Render(perspective);
            this.SwapBuffers();
        }
        
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
//            System.Threading.Thread.Sleep(500);
        }
        
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if(keys.ContainsKey(e.Key))
               keys[e.Key](e);
            else
               base.OnKeyDown(e);
        }
        
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, this.Width, this.Height);
        }

        public override void Dispose()
        {
            if(null!=cube1)
                cube1.Dispose();
            base.Dispose();
        }
    }
}
