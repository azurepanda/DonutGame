using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Game
{
    class InputState : ICloneable
    {
        private bool[] keys;
        private PointF mousepos;
        private MouseButtons buttons;
        private Matrix matrix;

        public InputState()
        {
            keys = new bool[65536];
            mousepos = new PointF(0, 0);
            matrix = new Matrix();
        }

        public bool KeyDown(Keys k)
        {
            return keys[(int) k];
        }

        public void SetKeyDown(Keys k)
        {
            this.keys[(int) k] = true;
        }

        public void SetKeyUp(Keys k)
        {
            this.keys[(int)k] = false;
        }

        public bool MouseButtonDown(MouseButtons m)
        {
            return buttons.HasFlag(m);
        }

        public void SetMouseButtons(MouseButtons m)
        {
            buttons = (MouseButtons)((int) buttons + (int) m);
        }

        public void UnsetMouseButtons(MouseButtons m)
        {
            buttons = (MouseButtons)((int) buttons - (int) m);
        }

        public PointF MousePos
        {
            get
            {
                return mousepos;
            }
            set
            {
                mousepos = value;
            }
        }

        public PointF MousePosW
        {
            get
            {
                Matrix m = matrix.Clone();
                m.Invert();
                PointF[] points = new PointF[]{ mousepos };
                m.TransformPoints(points);
                return points[0];
            }
        }

        public float MouseX
        {
            get
            {
                return mousepos.X;
            }
            set
            {
                mousepos.X = value;
            }
        }

        public float MouseY
        {
            get
            {
                return mousepos.Y;
            }
            set
            {
                mousepos.Y = value;
            }
        }

        public float MouseXW
        {
            get
            {
                return MousePosW.X;
            }
        }

        public float MouseYW
        {
            get
            {
                return MousePosW.Y;
            }
        }

        public Matrix CameraMatrix
        {
            get
            {
                return matrix;
            }
            set
            {
                matrix = value;
            }
        }

        public Object Clone()
        {
            return (Object) MemberwiseClone();
        }
    }
}
