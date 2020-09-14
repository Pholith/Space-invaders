using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Utils
{
    /// <summary>
    /// Objects with this interface (SpawnerManager and Gameobject) can works with TimedActions the same way Game works with gameobjects
    /// </summary>
    interface IActionsManager
    {
        TimedAction AddNewAction(TimedAction action);

    }
}
