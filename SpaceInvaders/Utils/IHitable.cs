namespace SpaceInvaders.GameObjects
{
    internal interface IHitable
    {

        /// <summary>
        /// Apply damage on a hittable object
        /// </summary>
        /// <param name="damage"></param>
        void OnHit(Laser laser);
    }
}