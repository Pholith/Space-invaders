namespace SpaceInvaders.GameObjects
{
    internal interface IHitable
    {

        /// <summary>
        /// Apply damage on a hittable object
        /// </summary>
        /// <param name="damage"></param>
        /// <returns> true if the object is destroyed </returns>
        bool OnHit(Laser laser);
    }
}