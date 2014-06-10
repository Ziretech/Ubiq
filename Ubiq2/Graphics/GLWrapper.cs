using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Ubiq2.Graphics
{
    public class GLWrapper : IGL
    {
        public void MatrixMode(MatrixMode mode)
        {
            GL.MatrixMode(mode);
        }

        public void ReadBuffer(ReadBufferMode mode)
        {
            GL.ReadBuffer(mode);
        }

        public void Clear(ClearBufferMask mask)
        {
            GL.Clear(mask);
        }

        public void ClearColor(float r, float g, float b, float a)
        {
            GL.ClearColor(r, g, b, a);
        }

        public void Begin(PrimitiveType type)
        {
            GL.Begin(type);
        }

        public void End()
        {
            GL.End();
        }

        public void Color3(float r, float g, float b)
        {
            GL.Color3(r, g, b);
        }

        public void Vertex2(float x, float y)
        {
            GL.Vertex2(x, y);
        }

        public void LoadIdentity()
        {
            GL.LoadIdentity();
        }

        public void Ortho(double left, double right, double bottom, double top, double near, double far)
        {
            GL.Ortho(left, right, bottom, top, near, far);
        }

        public void Viewport(int x, int y, int width, int height)
        {
            GL.Viewport(x, y, width, height);
        }

        public void Enable(EnableCap enableCap)
        {
            GL.Enable(enableCap);
        }

        public void BindTexture(TextureTarget textureTarget, int textureId)
        {
            GL.BindTexture(textureTarget, textureId);
        }

        public void TexParameter(TextureTarget textureTarget, TextureParameterName parameterName, int param)
        {
            GL.TexParameter(textureTarget, parameterName, param);
        }

        public void TexImage2D(TextureTarget target, int level, PixelInternalFormat internalFormat, int width, int height, int border,
            PixelFormat format, PixelType type, IntPtr pixels)
        {
            GL.TexImage2D(target, level, internalFormat, width, height, border, format, type, pixels);
        }

        public void TexCoord2(float x, float y)
        {
            GL.TexCoord2(x, y);
        }

        public void BlendFunc(BlendingFactorSrc src, BlendingFactorDest dest)
        {
            GL.BlendFunc(src, dest);
        }

        public void PushMatrix()
        {
            GL.PushMatrix();
        }

        public void PopMatrix()
        {
            GL.PopMatrix();
        }

        public void Translate(float x, float y, float z)
        {
            GL.Translate(x, y, z);
        }

        public void Translate(Vector3d translation)
        {
            GL.Translate(translation);
        }

        public void Scale(Vector3d scale)
        {
            GL.Scale(scale);
        }

        public int GenTexture()
        {
            return GL.GenTexture();
        }
    }
}
