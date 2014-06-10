using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Ubiq2.Graphics
{
    public interface IGL
    {
        void MatrixMode(MatrixMode mode);
        void ReadBuffer(ReadBufferMode mode);
        void Clear(ClearBufferMask mask);
        void ClearColor(float r, float g, float b, float a);
        void Begin(PrimitiveType type);
        void End();
        void Color3(float r, float g, float b);
        void Vertex2(float x, float y);
        void LoadIdentity();
        void Ortho(double left, double right, double bottom, double top, double near, double far);
        void Viewport(int x, int y, int width, int height);
        void Enable(EnableCap enableCap);
        void BindTexture(TextureTarget textureTarget, int textureId);
        void TexParameter(TextureTarget textureTarget, TextureParameterName parameterName, int param);
        void TexImage2D(TextureTarget target, int level, PixelInternalFormat internalFormat, int width, int height, int border, PixelFormat format, PixelType type, IntPtr pixels);
        void TexCoord2(float x, float y);
        void BlendFunc(BlendingFactorSrc src, BlendingFactorDest dest);
        void PushMatrix();
        void PopMatrix();
        void Translate(float x, float y, float z);
        void Translate(Vector3d translation);
        void Scale(Vector3d scale);
        int GenTexture();
    }
}
