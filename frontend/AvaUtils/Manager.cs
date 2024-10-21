using Avalonia.Data;

namespace AvaUtils;

public static class Manager {
    private static readonly Dictionary<string, Action<object>> _navSetters = new();
    private static readonly Dictionary<string, Optional<object>> _navCurrentPages = new();

    public static void RegisterNav(string key, Action<object> setter) {
        _navSetters[key] = setter;
    }

    public static void SetCurrentPage(string key, object currentPage) {
        if (!_navSetters.TryGetValue(key, out var setter)) return;
        setter.Invoke(currentPage);
        _navCurrentPages[key] = currentPage;
    }

    public static Optional<object> GetCurrentPage(string key) {
        return _navCurrentPages.TryGetValue(key, out var page) ? page : Optional<object>.Empty;
    }
}