﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Game
{
    class GameControl : Control
    {
        private LinearGradientBrush lgb;
        private Timer timer;
        private Level currentLevel;
        private PointF camera;
        private InputState inputstate;
        private InputState inputstateP;
        public Image[,] imageset;
        public bool running;
        public float loaded;
        public int lives;

        public GameControl()
        {
            InitializeComponent();
        }
        public void InitializeComponent()
        {
            DoubleBuffered = true;

            loaded = 0;
            running = false;
            lives = 5;
            
            Width = 1024;
            Height = 768;
            MinimumSize = Size;
            MaximumSize = Size;
            camera = new PointF(0, 0);

            lgb = new LinearGradientBrush(new Point(0, 0), new Point(0, Height), Color.FromArgb(221, 236, 255), Color.FromArgb(101, 178, 244));
            
            Paint += GameControl_Paint;
            Click += GameControl_Click;
            MouseDown += GameControl_MouseDown;
            MouseUp += GameControl_MouseUp;
            KeyDown += GameControl_KeyDown;
            KeyUp += GameControl_KeyUp;
            MouseMove += GameControl_MouseMove;
            PreviewKeyDown += GameControl_PreviewKeyDown;

            inputstate = new InputState();
            inputstateP = new InputState();

            timer = new Timer();
            timer.Tick += timer_Tick;
            timer.Interval = 30;
            timer.Enabled = true; 
        }

        private void LoadTileSet()
        {
            Console.WriteLine("Loading");
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    imageset[x, y] = Sprite.GetTile(Properties.Resources.tilesheet, new Point(x, y), new Size(128, 128));
                    loaded += (100f / 256f);
                }
            }
            Console.WriteLine("Loaded");
        }

        public void StartGame()
        {
            loaded = 0;
            imageset = new Image[16, 16];
            LoadTileSet();

            currentLevel = LoadLevel(Properties.Resources.level1, this, imageset);
            running = true;
        }

        private static Level LoadLevel(string data, GameControl gc, Image[,] iset)
        {
            data = data.Replace("\r", "");
            Level level = new Level(gc);
            int row = 0;
            int key = 0;
            foreach (String s in data.Split("\n".ToCharArray()))
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (s.ToCharArray()[i] == '1')
                    {
                        level.AddSprite(new FloorTile(new Point(i * 128, row * 128), key, iset[0, 0]));
                    }
                    if (s.ToCharArray()[i] == '2')
                    {
                        level.AddSprite(new FloorTile(new Point(i * 128, row * 128), key, iset[1, 0]));
                    }
                    if (s.ToCharArray()[i] == '3')
                    {
                        level.AddSprite(new FloorTile(new Point(i * 128, row * 128), key, iset[2, 0]));
                    }
                    if (s.ToCharArray()[i] == '4')
                    {
                        level.AddSprite(new FloorTile(new Point(i * 128, row * 128), key, iset[3, 0]));
                    }
                    if (s.ToCharArray()[i] == '5')
                    {
                        level.AddSprite(new FloorTile(new Point(i * 128, row * 128), key, iset[4, 0]));
                    }
                    if (s.ToCharArray()[i] == '6')
                    {
                        level.AddSprite(new FloorTile(new Point(i * 128, row * 128), key, iset[5, 0]));
                    }
                    if (s.ToCharArray()[i] == '7')
                    {
                        level.AddSprite(new FloorTile(new Point(i * 128, row * 128), key, iset[6, 0]));
                    }
                    if (s.ToCharArray()[i] == '8')
                    {
                        level.AddSprite(new FloorTile(new Point(i * 128, row * 128), key, iset[7, 0]));
                    }
                    if (s.ToCharArray()[i] == '9')
                    {
                        level.AddSprite(new FloorTile(new Point(i * 128, row * 128), key, iset[8, 0]));
                    }
                    if (s.ToCharArray()[i] == 'Q')
                    {
                        level.AddSprite(new FloorTile(new Point(i * 128, row * 128), key, iset[0, 8]));
                    }
                    if (s.ToCharArray()[i] == 'W')
                    {
                        level.AddSprite(new FloorTile(new Point(i * 128, row * 128), key, iset[1, 8]));
                    }
                    if (s.ToCharArray()[i] == 'E')
                    {
                        level.AddSprite(new FloorTile(new Point(i * 128, row * 128), key, iset[2, 8]));
                    }
                    if (s.ToCharArray()[i] == 'R')
                    {
                        level.AddSprite(new FloorTile(new Point(i * 128, row * 128), key, iset[3, 8]));
                    }
                    if (s.ToCharArray()[i] == 'p')
                    {
                        level.AddSprite(new Donut(new Point(i * 128, row * 128), key));
                    }
                    key++;
                }
                row++;
            }
            return level;
        }

        void GameControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        void GameControl_MouseMove(object sender, MouseEventArgs e)
        {
            inputstate.MousePos = new PointF(e.X, e.Y);
        }

        void GameControl_KeyUp(object sender, KeyEventArgs e)
        {
            inputstate.SetKeyUp(e.KeyCode);
        }

        void GameControl_KeyDown(object sender, KeyEventArgs e)
        {
            inputstate.SetKeyDown(e.KeyCode);
        }

        void GameControl_MouseUp(object sender, MouseEventArgs e)
        {
            inputstate.MousePos = new PointF(e.X, e.Y);
            inputstate.UnsetMouseButtons(e.Button);
        }

        void GameControl_MouseDown(object sender, MouseEventArgs e)
        {
            inputstate.MousePos = new PointF(e.X, e.Y);
            inputstate.SetMouseButtons(e.Button);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return true;
        }

        public PointF Camera
        {
            get
            {
                return camera;
            }
            set
            {
                camera = value;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (running)
            {
                Matrix m = new Matrix();
                m.Translate(camera.X, camera.Y);
                inputstate.CameraMatrix = m;
                foreach (Sprite s in currentLevel.GetSpriteList())
                {
                    s.Update(currentLevel, inputstate, inputstateP);
                }
                Invalidate();
                inputstateP = (InputState)inputstate.Clone();
            }
            else
            {
                Invalidate();
            }
        }

        void GameControl_Click(object sender, EventArgs e)
        {
            Focus();
        }

        void GameControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillRectangle(lgb, 0, 0, Width, Height);
            if (running)
            {
                e.Graphics.TranslateTransform(camera.X, camera.Y);
                foreach (Sprite s in currentLevel.GetSpriteList())
                {
                    if (new Rectangle(-256 - (int)Camera.X, -256 - (int)Camera.Y, Width + 512, Height + 512).Contains(new Rectangle((int)s.X, (int)s.Y, (int)s.Width, (int)s.Height + 128)))
                    {
                        using (ImageAttributes wrapMode = new ImageAttributes())
                        {
                            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                            //e.Graphics.DrawImage(s.GetImage(), s.Bounds, 0, 0, s.Width, s.Height, GraphicsUnit.Pixel, wrapMode));
                            e.Graphics.DrawImage(s.GetImage(), Rectangle.Round(s.Bounds), 0, 0, s.Width, s.Height, GraphicsUnit.Pixel, wrapMode);
                        }
                    }
                }
                e.Graphics.ResetTransform();
                
                for(int i = 0; i < lives; i++)
                {
                    e.Graphics.DrawImage(Properties.Resources.donut,64 + i * 80, 64, 64, 64);
                }
            }
            else
            {
                Pen p = new Pen(Color.Black, 5);
                p.LineJoin = LineJoin.Bevel;

                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128,0,0,0)), Width / 2 - 236, Height / 2 - 108, 512, 256);
                e.Graphics.FillRectangle(Brushes.Silver, Width / 2 - 256, Height / 2 - 128, 512, 256);
                e.Graphics.DrawRectangle(p, Width / 2 - 256, Height / 2 - 128, 512, 256);

                RectangleF prog = new RectangleF(Width / 2 - 224, Height / 2 - 24, 448, 48);

                e.Graphics.FillRectangle(Brushes.Gray, prog);
                e.Graphics.FillRectangle(Brushes.LimeGreen, prog.X,prog.Y,prog.Width * (loaded / 100),prog.Height);
                e.Graphics.DrawRectangle(p, Rectangle.Round(prog));
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString("Loading...", new Font("Verdana", 24), Brushes.Black, prog, sf);
            }
        }

        public static bool IsIntersected(PointF circle, float radius, RectangleF rectangle)
        {
            PointF rectangleCenter = new PointF((rectangle.X + rectangle.Width / 2), (rectangle.Y + rectangle.Height / 2));

            float w = rectangle.Width / 2;
            float h = rectangle.Height / 2;

            float dx = Math.Abs(circle.X - rectangleCenter.X);
            float dy = Math.Abs(circle.Y - rectangleCenter.Y);

            if (dx > (radius + w) || dy > (radius + h))
            {
                return false;
            }

            PointF circleDistance = new PointF(Math.Abs(circle.X - rectangle.X - w), Math.Abs(circle.Y - rectangle.Y - h));

            if (circleDistance.X <= w)
            {
                return true;
            }

            if (circleDistance.Y <= h)
            {
                return true;
            }

            double cornerDistanceSq = Math.Pow(circleDistance.X - w, 2) + Math.Pow(circleDistance.Y - h, 2);

            return cornerDistanceSq <= (Math.Pow(radius, 2));
        }
    }
}
