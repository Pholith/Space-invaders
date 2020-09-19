using SpaceInvaders.GameObjects.Invaders;
using System.Linq;

namespace SpaceInvaders.GameModes
{
    class NormalMode : GameMode
    {
        public NormalMode()
        {
        }
        public override void Init()
        {
            base.Init();
            new InvaderBlock();

        }

        public override bool CheckEnd()
        {           
            if (Game.game.gameObjects.All((GameObject obj) => !(obj is Invader)))
            {
                Win = true;
                Ended = true;
            }
            return base.CheckEnd();
        }
    }
}
