/*
 * Created by SharpDevelop.
 * User: alex
 * Date: 09.10.2016
 * Time: 14:46
 */
using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace GLCapsule
{
	/// <summary>
	/// Description of Texture.
	/// </summary>
	public class Texture : GLObject
	{
		
		public Texture(string fn)
		{
			this.Handle = GL.GenTexture();
            if(ErrorCode.NoError!=GL.GetError())
				throw new GLCapsuleException("Texture creation error");
			Release = () => GL.DeleteTexture(this.Handle);
			var bmp = new Bitmap(fn);
			this.Bind();
			var data = bmp.LockBits(new Rectangle(0,0,bmp.Width,bmp.Height),
			                               ImageLockMode.ReadOnly,
			                               System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			GL.TexImage2D(TextureTarget.Texture2D,
			              0,
			              PixelInternalFormat.Rgba,
			              data.Width,
			              data.Height,
			              0,
            			  OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
            			  PixelType.UnsignedByte,
            			  data.Scan0);
			bmp.UnlockBits(data);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
			this.Unbind();
		}
		
		public void Bind()
		{
			GL.BindTexture(TextureTarget.Texture2D,this.Handle);
		}
		
		public void Unbind()
		{
			GL.BindTexture(TextureTarget.Texture2D,0);
		}
	}
}
