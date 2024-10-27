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

    public static Dictionary<TileType, bool> IsSFXPlaying = new Dictionary<TileType, bool>
    {
        { TileType.Empty, false },
        { TileType.Burning, false },
        { TileType.Supplies, false },
        { TileType.Boost, false },
        { TileType.Sticky, false },
        { TileType.Obstacle, false },
        { TileType.Normal, false },
    };

    public static void DoAction(string name) => DoAction(GetTileType(name));

    public static void DoAction(TileType type)
    {
        IsSFXPlaying = SoundManager.current.IsTileSoundPlayingUpdate();
        switch (type)
        {
            case TileType.Empty:
                {
                    GameController.current.game.CurrentState = GameState.GameOver;
                    if (!IsSFXPlaying[TileType.Empty])
                    {
                        SoundManager.current.PlayFall();
                        IsSFXPlaying[TileType.Empty] = true;
                    }
                    break;
                }
            case TileType.Burning:
                {
                    GameController.current.game.fuelRate = 10;
                    if (!IsSFXPlaying[TileType.Burning])
                    {
                        SoundManager.current.PlayBurning();
                        IsSFXPlaying[TileType.Burning] = true;
                    }
                    break;
                }
            case TileType.Supplies:
                {
                    GameController.current.game.fuel = 50;
                    if (!IsSFXPlaying[TileType.Supplies])
                    {
                        SoundManager.current.PlaySupply();
                        IsSFXPlaying[TileType.Supplies] = true;
                    }
                    break;
                }
            case TileType.Boost:
                {
                    GameController.current.game.GameSpeedMultiplier = 2.25f;
                    if (!IsSFXPlaying[TileType.Boost])
                    {
                        SoundManager.current.PlayBoost();
                        IsSFXPlaying[TileType.Boost] = true;
                    }
                    break;
                }
            case TileType.Sticky:
                {
                    GameController.current.game.GameSpeedMultiplier = 1f;
                    if (!IsSFXPlaying[TileType.Sticky])
                    {
                        SoundManager.current.PlaySticky();
                        IsSFXPlaying[TileType.Sticky] = true;
                    }
                    break;
                }
            case TileType.Obstacle:
                {
                    GameController.current.game.CurrentState = GameState.GameOver;
                    if (!IsSFXPlaying[TileType.Obstacle])
                    {
                        SoundManager.current.PlayCrash();
                        IsSFXPlaying[TileType.Obstacle] = true;
                    }
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
