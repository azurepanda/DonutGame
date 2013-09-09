using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    class SpikeTile : Sprite
    {
        public SpikeTile(Point loc, int key)
        {
            this.Key = key;
            this.Size = new System.Drawing.SizeF(128, 128);
            this.Location = loc;
            this.Friction = 0.0F;
            this.Velocity = new PointF(0, 0);
        }

        public override Image GetImage()
        {
            return Properties.Resources.spiketile;
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
