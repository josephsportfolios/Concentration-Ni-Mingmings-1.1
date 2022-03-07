/* This program is the main program for the
 * game, Concentration of Mingmings.
 */

// Initialize the frameworks.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace ConcentrationOfMingMings
{
    public class Game1 : Game
    {

        // Initialize audio-visual cues.
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Song song;

        Texture2D targetSprite; // Initiaalize Catya 
        Texture2D targetChoice; // Initialize choice boxes
        Texture2D targetFrame; // Initialize background
        Texture2D targetLargeChoice; // Initialize opening choice 
        
        // Initialize lives
        Texture2D threeLives;
        Texture2D twoLives;
        Texture2D oneLife;

        //  Initialize fonts.
        SpriteFont comicSans;
        SpriteFont comicSansLarge;

        // Initialize randomizer
        Random random = new Random();

        // Initialize initial question
        string questionPresented = "1.What is the most commonly used concentration unit in\nChemistry?";
        string correctChoice = "Molarity";
        string aChoice = "Molality";
        string bChoice = "Molarity";
        string cChoice = "Percent by Mass";
        string dChoice = "Mole Fraction";

        // List of questions 
        // Format: {Question, Correct, A, B, C, D}
        string[,] questions = new string[16, 6]
        {
            {"1.What is the most commonly used concentration unit in\nChemistry?", "B. Molarity", "A. Molality", "B. Molarity", "C. Percent by Mass", "D. Mole Fraction" },
            {"2.The formula of this concentration is defined as the\nratio of number of moles of 1 component over the number\nof moles of all components present?", "D. Mole Fraction", "A. Molality", "B. Molarity", "C. Percent by Mass", "D. Mole Fraction"},
            {"3.A gas leak in a nearby factory was found to contain\n3.45 moles of nitrogen, 0.689 moles of sulfur, and 2.74\nmoles of carbon. Calculate the mole fraction of each gas.", "A. 0.50, 0.10, 0.40", "A. 0.50, 0.10, 0.40", "B. 0.50, 0.27, 0.15", "C. 0.46, 0.12, 0.40", "D. 0.46, 0.15, 0.12"},
            {"4.A sample of 0.726 g of salt (NaCl) is dissolved in\n33.5 g of water. What is the percent by mass of NaCl \nin the solution?", "C. 2.12%", "A. 3.36%", "B. 2.45%", "C. 2.12%", "D. 9.38%"},
            {"5.It is the amount of solute present in a given amount\nof solution", "B. Concentration", "A. Mass of Solute", "B. Concentration", "C. Ratio of Moles", "D. Molality"},
            {"6.There is approximately 23.62 g  of calcium (Ca) in\n 132.67 g of CaCl2 solution. Calculate the percent by \nmass of calcium in this solution.", "C. 17.8%", "A. 15.3%", "B. 22.4%", "C. 17.8%", "D. 16.3%"},
            {"7.The following are mixed into a solution- 505 grams \nethanol (C2H5OH), 215 grams methanol (CH3OH), and\n415 grams water (H2O). The solvent is:", "D. H2O", "A. C2H5OH", "B. CH3OH", "C. A and D", "D. H2O"},
            {"8.What Concentration Unit represents the symbol 'X'?", "D. Mole Fraction", "A. Molality", "B. Molarity", "C. Percent By Mass", "D. Mole Fraction"},
            {"9.Calculate the molarity of a solution made from \n54.6 g HCl in 750 mL.", "A. 72.8%", "A. 72.8%", "B. 75.6%", "C. 71.7%", "D. 79.0%"},
            {"10.What is the molarity when 25.0 g of the compound \nNaCl (molar mass=58.44 g/mole) is placed in 85.0 mL of \ntotal solution?", "C. 5.03 M", "A. 6.21 M", "B. 6.73 M", "C. 5.03 M", "D. 3.45 M"},
            {"11.What is the molality of a solution containing 14.32\nmoles of urea in 320 g of water?", "D. 44.75%", "A. 52.12%", "B. 23.78%", "C. 43.99%", "D. 44.75%"},
            {"12.What is the molarity of an 85.0-mL methanol (C6H5OH) \nsolution containing 1.77 g of methanol?", "B. 0.221 M", "A. 0.321 M", "B. 0.221 M", "C. 0.344 M", "D. 0.545 M"},
            {"13.A very saturated solution means an abundant amount \nof ______ present", "D. Solute", "A. Mixture", "B. Solvent", "C. Concentration", "D. Solute"},
            {"14.How many moles of solute are contained in 4.0 L of \na 2.6 M solution?", "A. 10.4 moles", "A. 10.4 moles", "B. 13.6 moles", "C. 7.4 moles", "D. 13.4 moles"},
            {"15.If the percent for a solute is 18% and the mass of \nthe solution is 450 g, what is the mass of solute in the\nsolution?", "A. 81%", "A. 81%", "B. 25%", "C. 72%", "D. 43%"},
            {"Question", "Correct", "A", "B", "C", "D" }
        }; 

        string storyPresented = "Dalee, the Universe's most advanced society, is run by a group of\n" +
                "cats called the Mingmings. The Mingmings are a special species\n" +
                "some kind of magic called Peecans in their DNA to create daily\n" +
                "innovations that further the power of their civilization.";
        
        // Format: {Key, Storyline}
        string[,] storyline = new string[5, 2]
        {
            {"a", "Word of the Mingming's greatness spread across the universe and\n" +
                "upon hearing about this, Hatdoc's - Dalee's rival civilization\n" +
                "envious and greedy scientists vowed to steal the magic from the\n" +
                "cats' DNA. Wanting to experiment on these creatures, the\n" +
                "scientsts kidnapped the entire clan of Mingmings. All but one." },
            {"b", "Catya, the only Mingmings left behind, is tasked to save her\n" +
                "friends from the evil scientists, and defeat their boss Joslop.\n" +
                "She is on a mission to track down the missing Mingmings by\n" +
                "examining the chemical bonds that have been left like crumbs\n" +
                "during the kidnapping. Each question consists of a concentration\n" +
                "related question that Catya must solve in order to get closer\n" +
                "to completing the mission." },
            {"c", "Move the cat using the arrow keys.\n" +
                "Press A/B/C/D to select your answer.\n" +
                "The goal of the game is to save the cats by selecting the correct\n" +
                "answer. You only have three lives, so make it count! If you feel\n" +
                "that you are about to lose, hearts will spawn across the game!\n" +
                "Get those hearts to get another life. Good luck on your mission!" },
            {"d", "Storyline 4" },
            {"e", "Storyline 5" }
        }; // "d" and "e" are added for buffers

        
        int questionNumber = 0; // Initialize question number
        int frameNumber = 0; // Initialize story frame number
        int score = 0; // Initialize score 
        int lives = 3; // Initialize lives
        bool started = false; // Determines if the game has started 
        bool storylineRun = false; // Determines if the storyline is running
        bool endGame = false; // Determines if the endgame is running
        string stringHolder = "A"; // Initialize string holder
        

        Vector2 targetPosition = new Vector2(300, 100); // Initialize Catya's position.
        Vector2 heartPosition; // Initialize the hearts' position

        // Initialize keyboard states
        KeyboardState keyState;
        KeyboardState newKeyState;
        KeyboardState prevKeyState;


        // Starts main loop
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // Sets screen width and height
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1100;
            _graphics.PreferredBackBufferHeight = 700;

            // Initializes heart position.
            heartPosition = new Vector2(random.Next(100, 1000), random.Next(100, 600));
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice); // Starts sprite.
            targetSprite = Content.Load<Texture2D>("Catya (1)"); // Loads Catya.
            targetLargeChoice = Content.Load<Texture2D>("Choice4"); // Loads opening choice.
            targetChoice = Content.Load<Texture2D>("Choice3"); // Loads choice boxes.
            comicSans = Content.Load<SpriteFont>("File"); // Loads small font.
            comicSansLarge = Content.Load<SpriteFont>("FontLarge"); // Loads big font.
            targetFrame = Content.Load<Texture2D>("Frame 4"); // Loads background.
            oneLife = Content.Load<Texture2D>("Lives"); // Loads 1 life.
            twoLives = Content.Load<Texture2D>("Lives 2"); // Loads 2 lives.
            threeLives = Content.Load<Texture2D>("Lives 3"); // Loads 3 lives.

            // Loads sound effect
            song = Content.Load<Song>("correct"); 
            MediaPlayer.Play(song);
        }


        protected override void Update(GameTime gameTime)
        {
            // Exit mechanism
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // Changes the position when Up, Down, Right, and Left buttons are pressed.
            if (keyState.IsKeyDown(Keys.Right))
            {
                targetPosition.X += 10;
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                targetPosition.X -= 10;
            }
            if (keyState.IsKeyDown(Keys.Up))
            {
                targetPosition.Y -= 10;
            }
            if (keyState.IsKeyDown(Keys.Down))
            {
                targetPosition.Y += 10;
            }

            // check if S is pressed and if yes, move through questions
            prevKeyState = keyState;
            keyState = Keyboard.GetState();
            var keys = keyState.GetPressedKeys();

            if (keys.Length > 0)
            {
                var keyValue = keys[0].ToString();
                stringHolder += keyValue;
            }

            if ((stringHolder[stringHolder.Length - 1] == 'S' || stringHolder[stringHolder.Length - 1] == 's') && started == false && storylineRun == false)
            {
                MediaPlayer.Play(song);
                storylineRun = true; // Sets the storyline to true in order to move through the storyline
            }

            // When T is pressed, the storyline moves.
            if (storylineRun == true && started == false && keyState.IsKeyDown(Keys.T) && prevKeyState.IsKeyUp(Keys.T) && questionNumber < 16)
            {
                MediaPlayer.Play(song);
                storyPresented = storyline[frameNumber, 1];
                frameNumber++;
            }

            // Initializes the question and the choices.
            questionPresented = questions[questionNumber, 0];
            correctChoice = questions[questionNumber, 1];
            aChoice = questions[questionNumber, 2];
            bChoice = questions[questionNumber, 3];
            cChoice = questions[questionNumber, 4];
            dChoice = questions[questionNumber, 5];

            // Logic when A is pressed.
            if (keyState.IsKeyDown(Keys.A) && prevKeyState.IsKeyUp(Keys.A) && questionNumber < 16 && started == true && endGame == false)
            {
                System.Diagnostics.Debug.WriteLine("A is pressed");
                if (correctChoice == aChoice) 
                {
                    MediaPlayer.Play(song);
                    score++;
                    questionNumber++;
                }
                else
                {
                    MediaPlayer.Play(song);
                    lives--;
                    questionNumber++;
                }
            }

            // Logic when B is pressed
            if (keyState.IsKeyDown(Keys.B) && prevKeyState.IsKeyUp(Keys.B) && questionNumber < 16 && started == true && endGame == false)
            {
                System.Diagnostics.Debug.WriteLine("B is pressed");
                if (correctChoice == bChoice)
                {
                    MediaPlayer.Play(song);
                    score++;
                    questionNumber++;
                }
                else
                {
                    MediaPlayer.Play(song);
                    lives--;
                    questionNumber++;
                }
            }

            // Logic when C is pressed.
            if (keyState.IsKeyDown(Keys.C) && prevKeyState.IsKeyUp(Keys.C) && questionNumber < 16 && started == true && endGame == false)
            {
                System.Diagnostics.Debug.WriteLine("C is pressed");
                if (correctChoice == cChoice)
                {
                    MediaPlayer.Play(song);
                    score++;
                    questionNumber++;
                }
                else
                {
                    MediaPlayer.Play(song);
                    lives--;
                    questionNumber++;
                }
            }

            // Logic when D is pressed.
            if (keyState.IsKeyDown(Keys.D) && prevKeyState.IsKeyUp(Keys.D) && questionNumber < 16 && started == true && endGame == false)
            {
                System.Diagnostics.Debug.WriteLine("D is pressed");
                if (correctChoice == dChoice)
                {
                    MediaPlayer.Play(song);
                    score++;
                    questionNumber++;
                }
                else
                {
                    MediaPlayer.Play(song);
                    lives--;
                    questionNumber++;
                }
            }

            // Detects collision when a heart is spawned. 
            // A heart is spawne when there is only 1 life left. The life count increases when 
            // Catya gets the heart.
            if (lives == 1 && Collision() && questionNumber < 16 && started == true && endGame == false)
            {
                heartPosition.X = random.Next(100, 1000);
                heartPosition.Y = random.Next(100, 600);
                if (!(lives > 3))
                {
                    MediaPlayer.Play(song);
                    lives++;
                }
            }

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White); // Clears the screen

            // Logic for opening Screen
            if (started == false && storylineRun == false)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(targetFrame, new Vector2(0, 0), Color.White);

                // Draws rainbow cats.
                _spriteBatch.Draw(targetSprite, new Vector2(targetPosition.X, targetPosition.Y), Color.White);
                _spriteBatch.Draw(targetSprite, new Vector2(targetPosition.X + 70, targetPosition.Y), Color.Yellow);
                _spriteBatch.Draw(targetSprite, new Vector2(targetPosition.X + 140, targetPosition.Y), Color.Orange);
                _spriteBatch.Draw(targetSprite, new Vector2(targetPosition.X + 210, targetPosition.Y), Color.Red);
                _spriteBatch.Draw(targetSprite, new Vector2(targetPosition.X + 280, targetPosition.Y), Color.Violet);
                _spriteBatch.Draw(targetSprite, new Vector2(targetPosition.X + 350, targetPosition.Y), Color.Blue);
                _spriteBatch.Draw(targetSprite, new Vector2(targetPosition.X + 420, targetPosition.Y), Color.Green);
                
                // Draws Press S to start!
                _spriteBatch.Draw(targetChoice, new Vector2(350, 388), Color.Red);
                _spriteBatch.DrawString(comicSans, "Press S to start!", new Vector2(420, 488), Color.Black);

                // Draws Title Concentration ni Mingming
                _spriteBatch.Draw(targetLargeChoice, new Vector2(100, 150), Color.White);
                _spriteBatch.DrawString(comicSansLarge, "Concentration\n ni Mingming", new Vector2(170, 225), Color.Black);
                System.Diagnostics.Debug.Write(targetPosition.X);
                System.Diagnostics.Debug.WriteLine(targetPosition.Y);

                _spriteBatch.End();
            }

            // Logic for storyline
            if (started == false && storylineRun == true)
            {
                GraphicsDevice.Clear(Color.White);
                _spriteBatch.Begin();
                _spriteBatch.Draw(targetFrame, new Vector2(0, 0), Color.White);
                _spriteBatch.DrawString(comicSans, storyPresented, new Vector2(30, 30), Color.Black); // Draws storyline.
                _spriteBatch.DrawString(comicSans, "Press T to\n move on!", new Vector2(250, 450), Color.Black); // WHen T is pressed, the story moves on.
                
                // When the frames equal to 4, the game starts.
                if (frameNumber == 4)
                {
                    started = true;
                }
                _spriteBatch.End();
            }

            // Logic for main game.
            if (started == true && storylineRun == true)
            {
                GraphicsDevice.Clear(Color.White);
                _spriteBatch.Begin();
                _spriteBatch.Draw(targetFrame, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(targetSprite, new Vector2(targetPosition.X, targetPosition.Y), Color.White); // Draws Catya
                
                // Draws choice boxes
                _spriteBatch.Draw(targetChoice, new Vector2(100, 350), Color.White);
                _spriteBatch.Draw(targetChoice, new Vector2(100, 450), Color.White);
                _spriteBatch.Draw(targetChoice, new Vector2(600, 350), Color.White);
                _spriteBatch.Draw(targetChoice, new Vector2(600, 450), Color.White);

                // Draws questions.
                _spriteBatch.DrawString(comicSans, questionPresented, new Vector2(100, 100), Color.Black);

                // Draws choices.
                _spriteBatch.DrawString(comicSans, aChoice, new Vector2(150, 450), Color.Black);
                _spriteBatch.DrawString(comicSans, bChoice, new Vector2(150, 550), Color.Black);
                _spriteBatch.DrawString(comicSans, cChoice, new Vector2(650, 450), Color.Black);
                _spriteBatch.DrawString(comicSans, dChoice, new Vector2(650, 550), Color.Black);
                _spriteBatch.DrawString(comicSans, "Score: " + score.ToString(), new Vector2(100, 40), Color.Black); // Display scores

                // Logic for life system.
                if (lives >= 3)
                {
                    _spriteBatch.Draw(threeLives, new Vector2(800, 40), Color.White);
                }
                if (lives == 2)
                {
                    _spriteBatch.Draw(twoLives, new Vector2(800, 40), Color.White);
                }
                if (lives == 1)
                {
                    _spriteBatch.Draw(oneLife, new Vector2(800, 40), Color.White);
                    _spriteBatch.Draw(oneLife, new Vector2(heartPosition.X, heartPosition.Y), Color.White); // Only spawn a heart if life is equal to one.
                }

                // If the questions are used up or the life is equal to 0, the game ends.
                if (questionNumber == 15 || lives == 0)
                {
                    endGame = true;
                }

                _spriteBatch.End();
            }

            // Logic for end screen.
            if (endGame == true)
            {
                GraphicsDevice.Clear(Color.White);
                _spriteBatch.Begin();
                _spriteBatch.Draw(targetFrame, new Vector2(0, 0), Color.White);

                // Draws the losing screen.
                if (lives == 0)
                {
                    _spriteBatch.Draw(targetLargeChoice, new Vector2(100, 150), Color.White);
                    _spriteBatch.DrawString(comicSansLarge, "Try again!", new Vector2(275, 250), Color.Black);
                    _spriteBatch.Draw(targetChoice, new Vector2(350, 350), Color.Red);
                    _spriteBatch.DrawString(comicSans, "Press E to exit!", new Vector2(425, 450), Color.Black);
                    _spriteBatch.Draw(targetChoice, new Vector2(350, 450), Color.Green);
                    _spriteBatch.DrawString(comicSans, "Press H to go home!", new Vector2(400, 550), Color.Black);
                }

                // Draws the winning screen.
                if (lives != 0)
                {
                    _spriteBatch.Draw(targetLargeChoice, new Vector2(100, 150), Color.White);
                    _spriteBatch.DrawString(comicSansLarge, "You saved \n" + score.ToString() + "  cats!", new Vector2(300, 225), Color.Black); // Displays the score.
                    _spriteBatch.Draw(targetChoice, new Vector2(350, 350), Color.Red);
                    _spriteBatch.DrawString(comicSans, "Press E to exit!", new Vector2(425, 450), Color.Black);
                    _spriteBatch.Draw(targetChoice, new Vector2(350, 450), Color.Green);
                    _spriteBatch.DrawString(comicSans, "Press H to go home!", new Vector2(400, 550), Color.Black);
                }
                _spriteBatch.End();

                // When E is pressed, the game exits.
                if (keyState.IsKeyDown(Keys.E) && prevKeyState.IsKeyUp(Keys.E))
                {
                    Exit();
                }

                // When H is pressed the game restarts and goes to the homescreen.
                if (keyState.IsKeyDown(Keys.H) && prevKeyState.IsKeyUp(Keys.H))
                {
                    MediaPlayer.Play(song);

                    // Reinitialize the values.
                    questionPresented = "1.What is the most commonly used concentration unit in\nChemistry?";
                    storyPresented = "Dalee, the Universe's most advanced society, is run by a group of\n" +
                            "cats called the Mingmings. The Mingmings are a special species\n" +
                            "some kind of magic called Peecans in their DNA to create daily\n" +
                            "innovations that further the power of their civilization.";
                    correctChoice = "Molality";
                    aChoice = "Molality";
                    bChoice = "Molarity";
                    cChoice = "Percent by Mass";
                    dChoice = "Mole Fraction";
                    questionNumber = 0;
                    frameNumber = 0;
                    score = 0;
                    lives = 3;
                    started = false;
                    storylineRun = false;
                    endGame = false;
                    stringHolder = "A";
                }
            }
            base.Draw(gameTime);
        }

        // Detects collisions.
        protected bool Collision()
        {
            Rectangle heartRectangle = new Rectangle((int)heartPosition.X, (int)heartPosition.Y, 45 - 2, 45 - 11); // Draws a rectangle around the heart.
            Rectangle catyaRectangle = new Rectangle((int)targetPosition.X, (int)targetPosition.Y, 100 - 1, 100 - 20); // Draws a rectangle around Catya.
            return heartRectangle.Intersects(catyaRectangle); // Detects collision between the two.
        }
    }
} 