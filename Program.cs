global using NUnit.Framework;

SnakeMath math = new SnakeMath();
SnakeUI userInterface = new SnakeUI();
SnakeGame game = new SnakeGame(20, 40, math, userInterface);

game.playGame();

