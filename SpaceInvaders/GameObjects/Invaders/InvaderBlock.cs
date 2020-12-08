using System;
using System.Collections.Generic;

namespace SpaceInvaders.GameObjects.Invaders
{
    /// <summary>
    /// An InvaderBlock is a list of Invader, it controls them and bind them together to react like a "block"
    /// </summary>
    /// <seealso cref="GameObject" /> 
    /// <seealso cref="Invader" />
    class InvaderBlock : GameObject
    {
        public InvaderBlock()
        {
            int size = 45;
            int unitType = 0;
            for (int i = 0; i < 5; i++)
            {
                unitType = Game.game.random.Next(2, 9);
                for (int j = 0; j < 9; j++)
                {
                    units.Add(new Invader(new Vector2(j * size, i * size), unitType));
                }
            }
        }
        
        List<Invader> units = new List<Invader>();

        public override void Update(Game gameInstance, double deltaT)
        {
            base.Update(gameInstance, deltaT);

            foreach (Invader unit in units)
            {
                if (unit.IsOnBorder())
                {
                    foreach (var unitB in units)
                    {
                        unitB.SwitchDirection();
                    }
                    break;
                }
            }

        }
    }
}
