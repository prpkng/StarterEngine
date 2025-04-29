using Core.Source;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

ConfigFile config;

try
{
    using var stream = TitleContainer.OpenStream("config.json");
    using var reader = new StreamReader(stream);
    Console.WriteLine(stream);
    config = JsonConvert.DeserializeObject<ConfigFile>(reader.ReadToEnd());
}
catch (Exception ex)
{
    Console.WriteLine($"Config file not available, creating it");
    using var file = File.CreateText($"{AppDomain.CurrentDomain.BaseDirectory}/config.json");
    var json = JsonConvert.SerializeObject(new ConfigFile(1280, 720), Formatting.Indented);
    config = new ConfigFile(1280, 720);
    file.Write(JsonConvert.SerializeObject(config, Formatting.Indented));
}


using var game = new StarterGame(config);
game.Run();