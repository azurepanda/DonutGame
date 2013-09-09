using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Game 
{
    class Level
    {
        private ConcurrentDictionary<int, Sprite> spriteList;
        private GameControl owner;

        public Level(GameControl gc)
        {
            spriteList = new ConcurrentDictionary<int, Sprite>();
            owner = gc;
        }

        public void AddSprite(Sprite s)
        {
            s.Level = this;
            spriteList.TryAdd(s.Key, s);
        }

        public void RemoveSprite(Sprite s)
        {
           
            spriteList.TryRemove(s.Key, out s);
        }

        public Sprite[] GetSpriteList()
        {
            return spriteList.Values.ToArray();
        }

        public GameControl Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }
    }
}
