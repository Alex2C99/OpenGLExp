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
        
        private CubeModel model;
        private Cube cube1;
        private Cube cube2;
        private Matrix4 perspective;
        
        bool cubeRotating;
        bool firstMouseMove;
        
        public MainWindow(int w, int h)
            : base(w,h)
        {
        	Point winCenter = this.PointToScreen(new Point(this.Width/2, this.Height/2));
        	OpenTK.Input.Mouse.SetPosition(winCenter.X,winCenter.Y);
        	
        	cubeRotating = true;
        	firstMouseMove = true;
            keys = new Dictionary<Key, KeyAction>();
            keys[Key.Escape] = e => this.Dispose();
            keys[Key.F12] = e => this.WindowState = (int)(WindowState.Fullscreen) - this.WindowState;
            keys[Key.Space] = e => cubeRotating = !cubeRotating;
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Title = "Hello OpenTK!";
            model = new CubeModel();
            cube1 = new Cube(model, new Vector3(-1.1f,0f,-10f),new Vector3(0,1,0),0);
            cube2 = new Cube(model, new Vector3(2f,1f,-25f),new Vector3(-0.2f,-0.3f,-0.5f),0);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            GL.ClearColor(Color.CornflowerBlue);
        }
        
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            cube1.Render(perspective);
            cube2.Render(perspective);
            this.SwapBuffers();
        }
        
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            if(cubeRotating)
            	cube1.Rotate(new Vector3(1f,-0.5f,0f),MathHelper.Pi/180);
            cube2.Rotate(new Vector3(-1f,-0.1f,-1f),MathHelper.Pi/180);
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
            perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver6, (float)this.Width / this.Height, 1.0f, 100.0f);
        }

		protected override void OnMouseMove(MouseMoveEventArgs e)
		{
			if(firstMouseMove)
			{
				firstMouseMove = false;
				return;
			}
			if (!cubeRotating) 
			{
				Vector2 move = new Vector2((float)e.YDelta / 200, (float)e.XDelta / 200);
				Vector3 axis = new Vector3(move.X, move.Y, 0);
				if (0 != axis.Length)
					cube1.Rotate(axis, move.Length);
			}
		}
        
        public override void Dispose()
        {
            if(null!=cube1)
                cube1.Dispose();
            if(null!=cube2)
                cube2.Dispose();
            if(null!=model)
            	model.Dispose();
            base.Dispose();
        }
    }
}
