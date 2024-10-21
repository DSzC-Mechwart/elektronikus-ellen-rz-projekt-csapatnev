using Avalonia.Media;

namespace AvaUtils;

public class ColorUtils {
    public static SolidColorBrush HexBrush(string hex) {
        return new SolidColorBrush(Color.Parse(hex));
    }
}