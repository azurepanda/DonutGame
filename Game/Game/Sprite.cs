using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    abstract class Sprite
    {
        private RectangleF bound;
        private Level level;
        private PointF vel;
        private int key;
        private float friction;

        public PointF Location
        {
            get
            {
                return bound.Location;
            }
            set
            {
                bound.Location = value;
            }
        }

        public RectangleF Bounds
        {
            get
            {
                return bound;
            }
            set
            {
                bound = value;
            }
        }

        public float X
        {
            get
            {
                return bound.X;
            }
            set
            {
                bound.X = value;
            }
        }

        public float Y
        {
            get
            {
                return bound.Y;
            }
            set
            {
                bound.Y = value;
            }
        }

        public SizeF Size
        {
            get
            {
                return bound.Size;
            }
            set
            {
                bound.Size = value;
            }
        }

        public float Width
        {
            get
            {
                return bound.Width;
            }
            set
            {
                bound.Width = value;
            }
        }

        public float Height
        {
            get
            {
                return bound.Height;
            }
            set
            {
                bound.Height = value;
            }
        }

        public Level Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }

        public PointF Velocity
        {
            get
            {
                return vel;
            }
            set
            {
                vel = value;
            }
        }

        public float XVel
        {
            get
            {
                return vel.X;
            }
            set
            {
                vel.X = value;
            }
        }

        public float YVel
        {
            get
            {
                return vel.Y;
            }
            set
            {
                vel.Y = value;
            }
        }

        public float Friction
        {
            get
            {
                return friction;
            }
            set
            {
                friction = value;
            }
        }

        public int Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }

        public int CollisionState(Sprite s)
        {
            if (IsCircleShape())
            {
                if (!s.IsCircleShape())
                {
                    PointF centerP = new PointF((Bounds.Left + Bounds.Right) / 2, (Bounds.Top + Bounds.Bottom) / 2);
                    if (GameControl.IsIntersected(centerP, Width / 2, s.Bounds))
                    {
                        float leftdist = s.Bounds.Left - centerP.X;
                        float rightdist = centerP.X - s.Bounds.Right;
                        float topdist = s.Bounds.Top - centerP.Y;
                        float bottomdist = centerP.Y - s.Bounds.Bottom;

                        int ret = 4;
                        float highest = leftdist;

                        if (rightdist > highest)
                        {
                            highest = rightdist;
                            ret = 2;
                        }
                        if (topdist > highest)
                        {
                            highest = topdist;
                            ret = 1;
                        }
                        if (bottomdist > highest)
                        {
                            highest = bottomdist;
                            ret = 3;
                        }
                        
                        return ret;
                    }
                }
            }
            return 0;
        }

        public void Delete()
        {
            level.RemoveSprite(this);
        }

        public abstract Image GetImage();

        public abstract bool IsCircleShape();

        public abstract void UpdateSelf(Level l, InputState s, InputState sp);

        public void Update(Level l, InputState s, InputState sp)
        {
            vel.X *= friction;
            vel.Y *= friction;
            X += vel.X;
            Y += vel.Y;
            UpdateSelf(l, s, sp);
        }

        public static Image GetTile(Image i, Point p, Size s)
        {
            Bitmap b = new Bitmap(s.Width, s.Height);
            Graphics g = Graphics.FromImage(b);
            g.DrawImage(i, -(p.X*s.Width), -(p.Y*s.Height), i.Width, i.Height);
            return b;
        }
    }
}
