global using NUnit.Framework;
using casnake;

SnakeMath math = new SnakeMath();
ISnakeUI userInterface = new SnakeUIConsole();
SnakeMap SMap = new SnakeMap(20,40);
SnakeGame game = new SnakeGame(userInterface, SMap);

game.playGame();

