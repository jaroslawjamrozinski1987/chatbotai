namespace ChatbotAi.Core.Services.Abstractions;

public interface IItemStorage
{
    void Set<T>(string key, T item);
    T Get<T>(string key);
}
