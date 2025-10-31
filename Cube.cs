//Rusu Sebastian 3133a;

using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    
    public class Cube
    {
        private bool newVisibility;

        private const int DIM = 10;
        public Cube()
        {
            newVisibility = false;
        }

        public void Hide()
        {
            newVisibility = false;
        }

        public void Show()
        {
            newVisibility = true;
        }
        public void ToggleVisibility()
        {
            newVisibility = !newVisibility;
        }
        public void DrawCube() // Functia care ne deseanaza cubul
        {
            if (newVisibility)
            {
                GL.Begin(PrimitiveType.Quads);

                GL.Color3(Color.Silver);
                GL.Vertex3(-1.0f*DIM, -1.0f * DIM, -1.0f * DIM);
                GL.Vertex3(-1.0f * DIM, 1.0f * DIM, -1.0f * DIM);
                GL.Vertex3(1.0f * DIM, 1.0f * DIM, -1.0f * DIM);
                GL.Vertex3(1.0f * DIM, -1.0f * DIM, -1.0f * DIM);

                GL.Color3(Color.Silver);
                GL.Vertex3(-1.0f * DIM, -1.0f * DIM, -1.0f * DIM);
                GL.Vertex3(1.0f * DIM, -1.0f * DIM, -1.0f * DIM) ;
                GL.Vertex3(1.0f * DIM, -1.0f * DIM, 1.0f * DIM);
                GL.Vertex3(-1.0f * DIM, -1.0f * DIM, 1.0f * DIM);

                GL.Color3(Color.Silver);

                GL.Vertex3(-1.0f * DIM, -1.0f * DIM, -1.0f * DIM);
                GL.Vertex3(-1.0f * DIM, -1.0f * DIM, 1.0f * DIM);
                GL.Vertex3(-1.0f * DIM, 1.0f * DIM, 1.0f * DIM) ;
                GL.Vertex3(-1.0f * DIM, 1.0f * DIM, -1.0f * DIM);

                GL.Color3(Color.Silver);
                GL.Vertex3(-1.0f * DIM, -1.0f * DIM, 1.0f * DIM);
                GL.Vertex3(1.0f * DIM, -1.0f * DIM, 1.0f * DIM);
                GL.Vertex3(1.0f * DIM, 1.0f * DIM, 1.0f * DIM);
                GL.Vertex3(-1.0f * DIM, 1.0f * DIM, 1.0f * DIM);

                GL.Color3(Color.Silver);
                GL.Vertex3(-1.0f * DIM, 1.0f * DIM, -1.0f * DIM);
                GL.Vertex3(-1.0f * DIM, 1.0f * DIM, 1.0f * DIM);
                GL.Vertex3(1.0f * DIM, 1.0f * DIM, 1.0f * DIM);
                GL.Vertex3(1.0f * DIM, 1.0f * DIM, -1.0f * DIM);

                GL.Color3(Color.Silver);
                GL.Vertex3(1.0f * DIM, -1.0f * DIM, -1.0f * DIM);
                GL.Vertex3(1.0f * DIM, 1.0f * DIM, -1.0f * DIM);
                GL.Vertex3(1.0f * DIM, 1.0f * DIM, 1.0f * DIM);
                GL.Vertex3(1.0f * DIM, -1.0f * DIM, 1.0f * DIM);

                GL.End();
            }   
        }
    }
}
