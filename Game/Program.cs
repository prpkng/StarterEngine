using Microsoft.Xna.Framework;

Console.WriteLine("Hello, World!");

using (Game game = new Game())
{
    var g = new GraphicsDeviceManager(game);
    game.Run();
}