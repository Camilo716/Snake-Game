global using NUnit.Framework;
using casnake.Game;
using casnake.SnakeUI;

ISnakeUI userInterface = new SnakeUIConsole();
SnakeMap SMap = new SnakeMap(20, 40);
SnakeGame game = new SnakeGame(userInterface, SMap);

game.playGame();   