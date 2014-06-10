using OpenTK;
using OpenTK.Graphics.OpenGL;
using Ubiq2.GameLogic;

namespace Ubiq2.Graphics
{
    public class Quad
    {
        public MapPosition Position { get; set; }
        public double FragmentPositionX { get; set; }
        public double FragmentPositionY { get; set; }

        public Quad()
        {
            Position = new MapPosition(0, 0);
        }

        public void Render(IGL gl)
        {
            DrawTile(gl, new Vector3d(FragmentPositionX, FragmentPositionY, 0.0), new Vector3d(Position.X, Position.Y, 0.0));
        }

        private void DrawTile(IGL gl, Vector3d textureFragmentPosition, Vector3d position)
        {
            gl.MatrixMode(MatrixMode.Texture);
            gl.PushMatrix();

            gl.Translate(textureFragmentPosition);

            gl.MatrixMode(MatrixMode.Modelview);
            gl.PushMatrix();
            gl.Translate(position);

            DrawQuad(gl);

            gl.PopMatrix();

            gl.MatrixMode(MatrixMode.Texture);
            gl.PopMatrix();

            gl.MatrixMode(MatrixMode.Modelview);
        }

        private void DrawQuad(IGL gl)
        {
            gl.Begin(PrimitiveType.Quads);
            SetQuadVertex(gl, 0.0f, 0.0f);
            SetQuadVertex(gl, 1.0f, 0.0f);
            SetQuadVertex(gl, 1.0f, 1.0f);
            SetQuadVertex(gl, 0.0f, 1.0f);
            gl.End();
        }

        private void SetQuadVertex(IGL gl, float x, float y)
        {
            gl.TexCoord2(x, y);
            gl.Vertex2(x, y);
        }
    }
}
