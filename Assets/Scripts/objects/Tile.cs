using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Game;

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
        Obstacle,
    }

    public static void DoAction(string name) => DoAction(GetTileType(name));

    public static void DoAction(TileType type)
    {
        switch (type)
        {
            case TileType.Empty:
                {
                    GameController.current.game.CurrentState = GameState.GameOver;
                    break;
                }
            case TileType.Burning:
                {
                    GameController.current.game.fuelRate = 10;
                    break;
                }
            case TileType.Supplies:
                {
                    GameController.current.game.fuel = 50;
                    break;
                }
            case TileType.Boost:
                {
                    GameController.current.game.GameSpeedMultiplier = 2.25f;
                    break;
                }
            case TileType.Sticky:
                {
                    GameController.current.game.GameSpeedMultiplier = 1f;
                    break;
                }
            case TileType.Obstacle:
                {
                    GameController.current.game.CurrentState = GameState.GameOver;
                    break;
                }
            default:
                {
                    // revert to normal
                    GameController.current.game.fuelRate = 1;
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
        if (name.ContainsInsensitive("obstacle"))
            return TileType.Obstacle;
        return TileType.Normal; // norm or other
    }

}
