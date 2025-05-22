public enum OrangeType { TOP_LEFT, TOP_RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT }

public class Orange : CellItem
{
    public OrangeType Type;
    public string GetPrefabName()
    {
        return Type switch
        {
            OrangeType.TOP_LEFT => PrefabName.ORANGE_TOP_LEFT.ToString(),
            OrangeType.TOP_RIGHT => PrefabName.ORANGE_TOP_RIGHT.ToString(),
            OrangeType.BOTTOM_LEFT => PrefabName.ORANGE_BOTTOM_LEFT.ToString(),
            OrangeType.BOTTOM_RIGHT => PrefabName.ORANGE_BOTTOM_RIGHT.ToString(),
            _ => PrefabName.ORANGE_TOP_LEFT.ToString() // mặc định
        };
    }
}