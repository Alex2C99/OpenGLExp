/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 07.10.2016
 * Time: 9:55
 */
using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace shaderexp
{
    class Program
    {
        
        private static readonly string VERT_SHADER = @"
#version 130

in vec3 vPos;
in vec3 vCol;
out vec4 vs_color;

void main()
{
   vs_color = vec4(vCol,1.0);
   gl_Position = vec4(vPos, 1.0);
}";

        private static readonly string FRAG_SHADER = @"
#version 130

in vec4 vs_color;
out vec4 fs_color;

void main()
{
   fs_color = vs_color;
}";
        
        public static void OnLoad(Object sender, EventArgs e)
        {
            Console.WriteLine("Loading started");
            Int32 shv = GL.CreateShader(ShaderType.VertexShader);
            Int32 shf = GL.CreateShader(ShaderType.FragmentShader);;
            Int32 prog = GL.CreateProgram();
            
            if(!GL.IsShader(shv))
            {
                Console.WriteLine("Vertex shader not created");
            }
            if(!GL.IsShader(shf))
            {
                Console.WriteLine("Fragment shader not created");
            }
            
            if(!GL.IsProgram(prog))
            {
                Console.WriteLine("Program not created");
            }
            
            GL.ShaderSource(shv,VERT_SHADER);
            GL.ShaderSource(shf,FRAG_SHADER);
            Int32 status;
            string log;
            GL.CompileShader(shv);
            status = 1;
            log = GL.GetShaderInfoLog(shv);
            GL.GetShader(shv, ShaderParameter.CompileStatus, out status);
            if(1!=status)
            {
                Console.WriteLine("Vertex shader compilation error: "+log);
            }
            GL.CompileShader(shf);
            status = 1;
            log = GL.GetShaderInfoLog(shf);
            GL.GetShader(shf, ShaderParameter.CompileStatus, out status);
            if(1!=status)
            {
                Console.WriteLine("Fragment shader compilation error: "+log);
            }
            
            GL.AttachShader(prog,shv);
            GL.AttachShader(prog,shf);
            GL.LinkProgram(prog);
            log = GL.GetProgramInfoLog(prog);
            status = 1;
            GL.GetProgram(prog,GetProgramParameterName.LinkStatus, out status);
            if(1!=status)
            {
                Console.WriteLine("Program linking error: "+log);
            }
            

            Console.WriteLine("Loading stopped");
        }
        
        public static void Main(string[] args)
        {
            using (var mainWin = new GameWindow(1024, 768)) 
            {
                mainWin.Load += OnLoad;
                mainWin.Run(30, 0);
            }
           
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}