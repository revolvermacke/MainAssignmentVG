namespace Business.Helpers;

public static class UniqueIDGenerator
{
    public static string GenerateUniqueId() => Guid.NewGuid().ToString();
}
