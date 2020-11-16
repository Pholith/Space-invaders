using SpaceInvaders.GameModes;
using SpaceInvaders.GameObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public class Game
    {

        #region GameObjects management
        /// <summary>
        /// Set of all game objects currently in the game
        /// </summary>
        public HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set of new game objects scheduled for addition to the game
        /// </summary>
        public HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Schedule a new object for addition in the game.
        /// The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject)
        {
            pendingNewGameObjects.Add(gameObject);
        }
        #endregion

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;

        /// <summary>
        /// GameMode choiced in the menu
        /// </summary>
        public GameMode Mode { get; private set; }

        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();


        public int Score { get; private set; }
        public void AddScore(int score) => Score += score;


        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

        public Random random { get; private set; }
        /// <summary>
        /// Some pens
        /// </summary>
        private static Pen blackPen = new Pen(Color.Black, 4);
        private static Pen whitePen = new Pen(Color.White, 3);

        public static readonly Color backgroundColor = Color.Black;
        public static readonly Color foregroundColor = Color.White;

        /// <summary>
        /// A shared simple font
        /// </summary>
        public static Font defaultFont = new Font(FontFamily.GenericSansSerif, 16, FontStyle.Regular);
        #endregion


        #region constructors

        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// 
        /// <returns></returns>
        public static Game CreateGame(Size gameSize, GameMode mode)
        {
            game = new Game(gameSize, mode);
            return game;
        }

        private Game(Size gameSize, GameMode mode)
        {
            this.gameSize = gameSize;
            Mode = mode;
            random = new Random();
        }

        #endregion

        #region methods

        /// <summary>
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
        }


        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {

            foreach (GameObject gameObject in gameObjects)
                gameObject.Draw(this, g);

            if (paused) DrawTextSquare(g, "paused", 20, 10);
            if (Mode.Ended) DrawTextSquare(g, Mode.Win ? "Win !" : "Game Over", 38, 15);
            Mode.Draw(g);
        }

        private void DrawTextSquare(Graphics g, string text, int fontSize, int padding)
        {
            Font f = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Regular);
            SizeF textSize = g.MeasureString(text, f);
            Vecteur2D pausePosition = new Vecteur2D(gameSize.Width / 2 - textSize.Width / 2, gameSize.Height / 2 - textSize.Height / 2);
            Rectangle rect = new Rectangle((int)pausePosition.X - padding, (int)pausePosition.Y - padding, (int)textSize.Width + padding * 2, (int)textSize.Height + padding * 2);

            g.FillRectangle(whitePen.Brush, rect);
            g.DrawRectangle(blackPen, rect);
            g.DrawString(text, f, blackPen.Brush, (float)pausePosition.X, (float)pausePosition.Y);
        }


        /// <summary>
        /// Init game
        /// </summary>
        public void InitGame()
        {
            Score = 0;
            gameObjects.Clear();
            pendingNewGameObjects.Clear();
            Mode.Init();
        }

        private bool paused = false;
        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {
            // Check for pause
            if (keyPressed.Contains(Keys.P))
            {
                paused = !paused;
                ReleaseKey(Keys.P);
            }
            if (paused) return;

            if (game.random.NextDouble() < 0.01)
            {
                new StarParticle(new Vecteur2D(game.random.Next(0, game.gameSize.Width), 0));
            }

            #region Objects managment
            // add new game objects
            foreach (var obj in pendingNewGameObjects)
            {
                obj.Init(this);
            }
            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();

            // update each game object
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(this, deltaT);
            }

            // remove dead objects
            gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());
            #endregion

            Mode.Update(deltaT);
            if (Mode.Ended)
            {
                if (keyPressed.Contains(Keys.Space))
                {
                    InitGame();
                }
                return;
            }

        }
        #endregion

    }
}
