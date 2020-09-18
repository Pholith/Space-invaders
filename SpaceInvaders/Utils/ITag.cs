﻿using SpaceInvaders.GameObjects;

namespace SpaceInvaders
{
    /// <summary>
    /// A tag allow lasers to only target enemy or the player
    /// </summary>
    public enum Tag
    {
        Invader,
        Player,
        Neutral
    }

    interface ITag
    {

        Tag GetTag();

    }
}