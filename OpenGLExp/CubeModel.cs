/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 05.10.2016
 * Time: 10:28
 */
using System;
using GLCapsule;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLExp
{
    /// <summary>
    /// Description of CubeModel.
    /// </summary>
    public class CubeModel : IDisposable
    {
        private static readonly string VERT_SHADER = @"
#version 450 core

uniform mat4 proj;
uniform mat4 mdl;

attribute vec3 vPos;
attribute vec2 vUV;
attribute vec3 vNorm;
out vec4 vs_color;
out vec2 UV;

void main()
{
//   vs_color = vec4(vCol,1.0);
   UV = vUV;
   gl_Position = proj*mdl*vec4(vPos, 1.0);
}";

        private static readonly string FRAG_SHADER = @"
#version 450 core

uniform vec3 clrs[6];
uniform sampler2D myTextureSampler;

in vec2 UV;
in vec4 vs_color;
out vec4 fs_color;

void main()
{
   fs_color = texture2D( myTextureSampler, UV ); // vec4(clrs[gl_PrimitiveID],1.0);
}";
        
        private const int VERTEX_ATTR_COUNT = 8;
        
        private static Vertex[] data;
        
        private static UInt32[] indexes;
        
        private readonly Texture tex;
        private readonly VertexArray vao;
        private readonly ShaderProgram shaderProgram;
       
        public CubeModel()
        {
            vao = new VertexArray();
            shaderProgram = new ShaderProgram();
            shaderProgram.AddShader(ShaderType.VertexShader,VERT_SHADER);
            shaderProgram.AddShader(ShaderType.FragmentShader,FRAG_SHADER);
            shaderProgram.Link();
            tex = new Texture("fanera.jpg");
                   
            var of = new ObjFile("cube.3dobj");
            of.GetGeometry(out data, out indexes);
            
            using(var vbo = new VertexBuffer(data))
            {
                vao.AddBuffer(vbo, shaderProgram, Vertex.Attributes);
            }        
        }       
        
        public void Draw(Matrix4 persp, Matrix4 model)
        {
            vao.Bind();
            tex.Bind();
            shaderProgram.Use();
            Int32 psp = GL.GetUniformLocation(shaderProgram.Handle,"proj");
            Int32 mdl = GL.GetUniformLocation(shaderProgram.Handle,"mdl");
            Int32 clrs = GL.GetUniformLocation(shaderProgram.Handle,"clrs");
            Int32 tu = GL.GetUniformLocation(shaderProgram.Handle,"myTextureSampler");
            GL.UniformMatrix4(psp,false, ref persp);
            GL.UniformMatrix4(mdl,false, ref model);
            GL.Uniform1(tu,0);
//            GL.Uniform3(clrs,colors.Length,colors);
            GL.DrawElements(PrimitiveType.Triangles,indexes.Length,DrawElementsType.UnsignedInt,indexes);
            shaderProgram.Unuse();
            tex.Unbind();
            vao.Unbind();
        }
        
        #region IDisposable implementation
        public void Dispose()
        {
            if(null!=tex)
                tex.Dispose();
            if(null!=shaderProgram)
                shaderProgram.Dispose();
            if(null!=vao)
                vao.Dispose();
        }
        #endregion
    }
}
