using System.Collections.Concurrent;

public class RecoveryCodeService
{
    private readonly ConcurrentDictionary<string, string> _codeStorage = new();

    // Armazena o código associado a um email
    public void SaveCode(string email, string code)
    {
        _codeStorage[email] = code;
    }

    // Tenta obter o código associado a um email
    public bool TryGetCode(string email, out string code)
    {
        return _codeStorage.TryGetValue(email, out code);
    }

    // Remove o código após a validação
    public void RemoveCode(string email)
    {
        _codeStorage.TryRemove(email, out _);
    }
}

