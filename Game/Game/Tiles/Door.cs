using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    class Door : Sprite
    {
        private Image imagetileB;
        private Image imagetileT;

        public Door(Point loc, int key, Image tileBottom, Image tileTop)
        {
            this.Key = key;
            this.Size = new System.Drawing.SizeF(128, 256);
            this.Location = loc;
            this.Friction = 0.0F;
            this.Velocity = new PointF(0, 0);
            this.imagetileB = tileBottom;
            this.imagetileT = tileTop;
        }
       
        public override Image GetImage()
        {
            Bitmap bmp = new Bitmap(128, 256);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.DrawImageUnscaled(imagetileT, 0, 0);
            gfx.DrawImageUnscaled(imagetileB, 0, 128);                    
            return bmp;         
        }

        public override bool IsCircleShape()
        {
            return false;
        }

        public override void UpdateSelf(Level l, InputState s, InputState sp)
        {
        }
    }
}
