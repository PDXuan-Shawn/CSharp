public interface ICacheFactory
{
    ICacheService CreateCacheService();
    ILogger CreateLogger(); // Ví dụ, có thể có các phương thức khác liên quan đến hệ thống cache
}
