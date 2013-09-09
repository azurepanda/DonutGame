using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    class FloorTile : Sprite
    {
        private Image imagetile;

        public FloorTile(Point loc, int key, Image tile)
        {
            this.Key = key;
            this.Size = new System.Drawing.SizeF(128, 128);
            this.Location = loc;
            this.Friction = 0.0F;
            this.Velocity = new PointF(0, 0);
            this.imagetile = tile;
        }

        public override Image GetImage()
        {
            return imagetile;
        }

        public override bool IsCircleShape()
        {
            return false;
        }

        public override void UpdateSelf(Level l, InputState s, InputState sp)
        {
        }

        public Image ImageTile
        {
            get
            {
                return imagetile;
            }
            set
            {
                imagetile = value;
            }
        }
    }
}
