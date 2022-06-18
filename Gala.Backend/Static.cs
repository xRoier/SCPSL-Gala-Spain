namespace Gala.Backend;

public class Static
{
    public Static() => Random = new Random();
    public readonly string Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    public Random Random;
}