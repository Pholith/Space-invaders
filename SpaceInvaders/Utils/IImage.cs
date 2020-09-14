using System.Drawing;

namespace SpaceInvaders.Utils
{
    /// <summary>
    /// Interface to automaticly use images in the gameobject class on some gameobjects that have images
    /// </summary>
    interface IImage
    {
        Bitmap GetImage();
    }
}
