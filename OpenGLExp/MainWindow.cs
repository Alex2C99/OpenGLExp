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
using OpenTK.Graphics.OpenGL4;
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
            GL.ClearColor(Color.CornflowerBlue);
        }
        
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            this.SwapBuffers();
        }
        
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if(keys.ContainsKey(e.Key))
               keys[e.Key](e);
            else
               base.OnKeyDown(e);
        }
    }
}
