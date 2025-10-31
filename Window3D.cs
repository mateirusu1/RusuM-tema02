using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ConsoleApp3
{
    // windowul grafic 3D
    class Window3D : GameWindow
    {

        private KeyboardState previousKeyboard;
        private MouseState previousMouse;
        private readonly Randomizer rando;
        private readonly Axes ax;
        private readonly Grid grid;
        private readonly Camera3DIsometric cam;
        private bool displayMarker;
        private ulong updatesCounter;
        private ulong framesCounter;
        private readonly Cube c1;

        private List<CubeDown> rainofobjects;

        private readonly Color DEFAULT_BKG_COLOR = Color.FromArgb(49, 50, 51);


        public Window3D() : base(1280, 768, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;
            Title = "Rusu Sebastian, 3133a";

            // inits
            rando = new Randomizer();
            ax = new Axes();
            grid = new Grid();
            cam = new Camera3DIsometric();
            c1 = new Cube();

            rainofobjects = new List<CubeDown>();

            DisplayHelp();
            displayMarker = false;
            updatesCounter = 0;
            framesCounter = 0;
        }


        //metoda apelata la initializarea ferestrei grafice
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }


        // metoda apelata la redimensionarea ferestrei grafice
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // set background color
            GL.ClearColor(DEFAULT_BKG_COLOR);

            // set viewport
            GL.Viewport(0, 0, this.Width, this.Height);

            // set perspective
            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 1024);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiva);

            // set the eye
            cam.SetCamera();
        }


        // metoda apelata periodic pentru update logica aplicatiei
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            updatesCounter++;

            if (displayMarker)
            {
                TimeStampIt("update", updatesCounter.ToString());
            }

            KeyboardState currentKeyboard = Keyboard.GetState();
            MouseState currentMouse = Mouse.GetState();

            
            if (currentKeyboard[Key.T] && !previousKeyboard[Key.T])
            {
                c1.ToggleVisibility();
            }

            if (currentKeyboard[Key.Escape])
            {
                Exit();
            }

            if (currentKeyboard[Key.H] && !previousKeyboard[Key.H])
            {
                DisplayHelp();
            }

            if (currentKeyboard[Key.R] && !previousKeyboard[Key.R])
            {
                GL.ClearColor(DEFAULT_BKG_COLOR);
                ax.Show();
                grid.Show();
            }

            if (currentKeyboard[Key.K] && !previousKeyboard[Key.K])
            {
                ax.ToggleVisibility();
            }


            if (currentKeyboard[Key.B] && !previousKeyboard[Key.B])
            {
                GL.ClearColor(rando.RandomColor());
            }

            if (currentKeyboard[Key.V] && !previousKeyboard[Key.V])
            {
                grid.ToggleVisibility();
            }


            // controlul camerei
            if (currentKeyboard[Key.W])
            {
                cam.MoveForward();
            }
            if (currentKeyboard[Key.S])
            {
                cam.MoveBackward();
            }
            if (currentKeyboard[Key.A])
            {
                cam.MoveLeft();
            }
            if (currentKeyboard[Key.D])
            {
                cam.MoveRight();
            }
            if (currentKeyboard[Key.Q])
            {
                cam.MoveUp();
            }
            if (currentKeyboard[Key.E])
            {
                cam.MoveDown();
            }


            if (currentKeyboard[Key.L] && !previousKeyboard[Key.L])
            {
                displayMarker = !displayMarker;
            }
            


            if (currentMouse[MouseButton.Left] && !previousMouse[MouseButton.Left])
            {
                rainofobjects.Add(new CubeDown());
            }

            if (currentMouse[MouseButton.Right] && !previousMouse[MouseButton.Right])
            {
                rainofobjects.Clear();
            }


            if (currentKeyboard[Key.G] && !previousKeyboard[Key.G])
            {
                foreach(CubeDown obj in rainofobjects)
                {
                    obj.ToggleGravity();
                }
            }


            if (currentKeyboard[Key.F] && !previousKeyboard[Key.F])
                cam.SetFarCamera();

            if (currentKeyboard[Key.C] && !previousKeyboard[Key.C])
                cam.SetCloseCamera();

            foreach (CubeDown obj in rainofobjects)
            {
                obj.UpdatePosition();
            }

            previousKeyboard = currentKeyboard;
            previousMouse = currentMouse;

        }

        // metoda apelata periodic pentru desenarea in fereastra grafica
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            framesCounter++;

            if (displayMarker)
            {
                TimeStampIt("render", framesCounter.ToString());
            }

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);


            grid.Draw();
            ax.Draw();
    
               
            foreach(CubeDown obj in rainofobjects)
            {
                obj.Draw(); 
            }
           

            SwapBuffers();
        }


        // metoda care afiseaza meniul de comenzi in consola
        private void DisplayHelp()
        {
            Console.WriteLine("\n      MENIU Rusu Sebastian 3133b");
            Console.WriteLine(" (H) - meniul");
            Console.WriteLine(" (ESC) - parasire aplicatie");
            Console.WriteLine(" (K) - schimbare vizibilitate sistem de axe");
            Console.WriteLine(" (R) - resteaza scena la valori implicite");
            Console.WriteLine(" (B) - schimbare culoare de fundal");
            Console.WriteLine(" (V) - schimbare vizibilitate linii");
            Console.WriteLine(" (T) - schimbare vizibilitate cub");
            Console.WriteLine(" (W,A,S,D) - deplasare camera (izometric)");
            Console.WriteLine(" (Left Click) - generarea unui cub care va fi atras de gravitatie si va ajunge pe grid");
            Console.WriteLine(" (Right Click) - curata obiectele");
            Console.WriteLine(" (G) - dezactivare gravitatie pentru toate cuburile aflate in cadere");
            Console.WriteLine(" (F) - schimbare camera <departe>");
            Console.WriteLine(" (C) - schimbare camera <aproape>");
        }

        private void TimeStampIt(String source, String counter)
        {
            String dt = DateTime.Now.ToString("hh:mm:ss.ffff");
            Console.WriteLine("     TSTAMP from <" + source + "> on iteration <" + counter + ">: " + dt);
        }

    }
}
