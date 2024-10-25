using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile
{
    public enum TileType
    {
        Normal,
        Empty,
        Burning,
        Supplies,
        Boost,
        Sticky,
    }

    public static void DoAction(string name) => DoAction(GetTileType(name));

    public static void DoAction(TileType type)
    {
        switch (type)
        {
            case TileType.Empty:
                {

                    break;
                }
            case TileType.Burning:
                {
                    Debug.Log("Do Burning Action");
                    break;
                }
            case TileType.Supplies:
                {
                    Debug.Log("Do Supplies Action");
                    break;
                }
            case TileType.Boost:
                {
                    Debug.Log("Do Boost Action");
                    break;
                }
            case TileType.Sticky:
                {
                    Debug.Log("Do Sticky Action");
                    break;
                }
            default:
                {
                    Debug.Log("Do Default Normal Action");
                    break;
                }
        }
    }

    public static TileType GetTileType(string name)
    {
        if (name.ContainsInsensitive("empty"))
            return TileType.Empty;
        if (name.ContainsInsensitive("burning"))
            return TileType.Burning;
        if (name.ContainsInsensitive("supply"))
            return TileType.Supplies;
        if (name.ContainsInsensitive("boost"))
            return TileType.Boost;
        if (name.ContainsInsensitive("sticky"))
            return TileType.Sticky;
        return TileType.Normal; // norm or other
    }

}
