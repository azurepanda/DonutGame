using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Game
{
    class Donut : Sprite
    {
        private float angle;
        private bool topC;
        private bool leftC;
        private bool bottomC;
        private bool rightC;
        private int jump;
        private float cam;

        public Donut(Point loc, int key)
        {
            this.Key = key;
            this.Size = new System.Drawing.SizeF(128, 128);
            this.angle = 0;
            this.Location = loc;
            this.Friction = 0.95F;
            this.Velocity = new PointF(0, 0);
            this.cam = 128;
        }
        
        public override Image GetImage()
        {
            return RotateImage(Properties.Resources.donut, angle);
        }

        public override bool IsCircleShape()
        {
            return true;
        }

        public override void UpdateSelf(Level l, InputState s, InputState sp)
        {
            angle = X * ((128 * (float) Math.PI)/360);
            if (s.KeyDown(Keys.Up))
            {
                jump = 10;
            }
            if (s.KeyDown(Keys.Right))
            {
                XVel += 1;
                //cam -= 10;
            }
            if (s.KeyDown(Keys.Left))
            {
                XVel -= 1;
                //cam += 10;
            }
            if (s.KeyDown(Keys.R))
            {
                X = 500;
                Y = 500;
                XVel = 0;
                YVel = 0;
            }
            if (s.KeyDown(Keys.Space))
            {
                //Friction = 0.99F;
                Friction = 1.01F;
            }
            else
            {
                Friction = 0.95F;
            }
            cam -= XVel;
            if (cam < 256) cam = 256;
            if (cam > (l.Owner.Width - 512)) cam = (l.Owner.Width - 512);
            l.Owner.Camera = new PointF(cam - X, 0);
            if (l.Owner.Camera.X > 0)
            {
                l.Owner.Camera = new PointF(0, 0);
            }

            topC = false;
            rightC = false;
            bottomC = false;
            leftC = false;

            foreach (Sprite t in l.GetSpriteList())
            {
                if (t != this)
                {
                    switch (CollisionState(t))
                    {
                        case 1:
                            topC = true;
                            break;
                        case 2:
                            rightC = true;
                            break;
                        case 3:
                            bottomC = true;
                            break;
                        case 4:
                            leftC = true;
                            break;
                    }
                }
            }
            if ((jump>0) && topC)
            {
                if (s.KeyDown(Keys.Space))
                {
                    YVel = -100;
                    Friction = 1.0F;
                }
                else
                {
                    YVel = -40;
                    jump = 0;
                }
            }
            if (jump > 0)
            {
                jump -= 1;
            }
            if (topC)
            {
                YVel = -(float)Math.Abs(YVel);
            }
            else
            {
                YVel += 2;
            }
            if (rightC)
            {
                XVel = (float)Math.Abs(XVel);
            }
            if (bottomC)
            {
                YVel = (float)Math.Abs(YVel);
            }
            if (leftC)
            {
                XVel = -(float)Math.Abs(XVel);
            }
        }

        public float Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
            }
        }

        private static Image RotateImage(Image img, float rotationAngle)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
            gfx.RotateTransform(rotationAngle);
            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.DrawImage(img, 0, 0, img.Width, img.Height);
            gfx.Dispose();
            return bmp;
        }
    }
}
