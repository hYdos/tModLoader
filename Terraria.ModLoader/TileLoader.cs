﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Map;
using Terraria.ObjectData;

namespace Terraria.ModLoader {
public static class TileLoader
{
    //make Terraria.ObjectData.TileObjectData._data internal
    //make all static Terraria.ObjectData.TileObjectData.StyleName fields public
    //make Terraria.ObjectData.TileObjectData.LinkedAlternates public
    //at end of Terraria.ObjectData.TileObjectData.Initialize remove TileObjectData.readOnlyData = true;
    //at beginning of Terraria.WorldGen.PlaceTile remove type too high check
    //at beginning of Terraria.WorldGen.PlaceObject remove type too high check
    //in Terraria.WorldGen.Convert remove type too high checks
    //in Terraria.WorldGen.StartRoomCheck change 419 to WorldGen.houseTile.Length
    //at end of Terraria.WorldGen.KillWall remove type too high check
    //in Terraria.Player change adjTile and oldAdjTile size to TileLoader.TileCount()
    //in Terraria.Player.AdjTiles change 419 to adjTile.Length

    //in Terraria.IO.WorldFile.SaveFileFormatHeader set initial num to TileLoader.TileCount
    private static int nextTile = TileID.Count;
    internal static readonly IDictionary<int, ModTile> tiles = new Dictionary<int, ModTile>();
    private static int vanillaChairCount = TileID.Sets.RoomNeeds.CountsAsChair.Length;
    private static int vanillaTableCount = TileID.Sets.RoomNeeds.CountsAsTable.Length;
    private static int vanillaTorchCount = TileID.Sets.RoomNeeds.CountsAsTorch.Length;
    private static int vanillaDoorCount = TileID.Sets.RoomNeeds.CountsAsDoor.Length;

    internal static int ReserveTileID()
    {
        int reserveID = nextTile;
        nextTile++;
        return reserveID;
    }

    internal static int TileCount()
    {
        return nextTile;
    }

    public static ModTile GetTile(int type)
    {
        if(tiles.ContainsKey(type))
        {
            return tiles[type];
        }
        else
        {
            return null;
        }
    }

    private static void Resize2DArray<T>(ref T[,] array, int newSize)
    {
        int dim1 = array.GetLength(0);
        int dim2 = array.GetLength(1);
        T[,] newArray = new T[newSize, dim2];
        for(int j = 0; j < newSize && j < dim1; j++)
        {
            for(int k = 0; k < dim2; k++)
            {
                newArray[j, k] = array[j, k];
            }
        }
        array = newArray;
    }

    internal static void ResizeArrays()
    {
        Array.Resize(ref Main.tileSetsLoaded, nextTile);
        for(int k = TileID.Count; k < nextTile; k++)
        {
            Main.tileSetsLoaded[k] = true;
        }
        Resize2DArray(ref Main.tileAltTexture, nextTile);
        Resize2DArray(ref Main.tileAltTextureInit, nextTile);
        Resize2DArray(ref Main.tileAltTextureDrawn, nextTile);
        Array.Resize(ref Main.tileTexture, nextTile);
        Array.Resize(ref Main.tileLighted, nextTile);
        Array.Resize(ref Main.tileMergeDirt, nextTile);
        Array.Resize(ref Main.tileCut, nextTile);
        Array.Resize(ref Main.tileAlch, nextTile);
        Array.Resize(ref Main.tileShine, nextTile);
        Array.Resize(ref Main.tileShine2, nextTile);
        Array.Resize(ref Main.tileStone, nextTile);
        Array.Resize(ref Main.tileAxe, nextTile);
        Array.Resize(ref Main.tileHammer, nextTile);
        Array.Resize(ref Main.tileWaterDeath, nextTile);
        Array.Resize(ref Main.tileLavaDeath, nextTile);
        Array.Resize(ref Main.tileTable, nextTile);
        Array.Resize(ref Main.tileBlockLight, nextTile);
        Array.Resize(ref Main.tileNoSunLight, nextTile);
        Array.Resize(ref Main.tileDungeon, nextTile);
        Array.Resize(ref Main.tileSpelunker, nextTile);
        Array.Resize(ref Main.tileSolidTop, nextTile);
        Array.Resize(ref Main.tileSolid, nextTile);
        Array.Resize(ref Main.tileBouncy, nextTile);
        Array.Resize(ref Main.tileValue, nextTile);
        Array.Resize(ref Main.tileLargeFrames, nextTile);
        Array.Resize(ref Main.tileRope, nextTile);
        Array.Resize(ref Main.tileBrick, nextTile);
        Array.Resize(ref Main.tileMoss, nextTile);
        Array.Resize(ref Main.tileNoAttach, nextTile);
        Array.Resize(ref Main.tileNoFail, nextTile);
        Array.Resize(ref Main.tileObsidianKill, nextTile);
        Array.Resize(ref Main.tileFrameImportant, nextTile);
        Array.Resize(ref Main.tilePile, nextTile);
        Array.Resize(ref Main.tileBlendAll, nextTile);
        Array.Resize(ref Main.tileGlowMask, nextTile);
        Array.Resize(ref Main.tileContainer, nextTile);
        Array.Resize(ref Main.tileSign, nextTile);
        Array.Resize(ref Main.tileMerge, nextTile);
        for(int k = 0; k < nextTile; k++) //oh dear
        {
            Array.Resize(ref Main.tileMerge[k], nextTile);
        }
        Array.Resize(ref Main.tileSand, nextTile);
        Array.Resize(ref Main.tileFlame, nextTile);
        Array.Resize(ref Main.tileFrame, nextTile);
        Array.Resize(ref Main.tileFrameCounter, nextTile);
        Array.Resize(ref WorldGen.tileCounts, nextTile);
        Array.Resize(ref WorldGen.houseTile, nextTile);
        Array.Resize(ref MapHelper.tileLookup, nextTile);
        Array.Resize(ref TileID.Sets.Conversion.Grass, nextTile);
        Array.Resize(ref TileID.Sets.Conversion.Stone, nextTile);
        Array.Resize(ref TileID.Sets.Conversion.Ice, nextTile);
        Array.Resize(ref TileID.Sets.Conversion.Sand, nextTile);
        Array.Resize(ref TileID.Sets.Conversion.HardenedSand, nextTile);
        Array.Resize(ref TileID.Sets.Conversion.Sandstone, nextTile);
        Array.Resize(ref TileID.Sets.Conversion.Thorn, nextTile);
        Array.Resize(ref TileID.Sets.Conversion.Moss, nextTile);
        Array.Resize(ref TileID.Sets.AllTiles, nextTile);
        for(int k = TileID.Count; k < nextTile; k++)
        {
            TileID.Sets.AllTiles[k] = true;
        }
        Array.Resize(ref TileID.Sets.Mud, nextTile);
        Array.Resize(ref TileID.Sets.Snow, nextTile);
        Array.Resize(ref TileID.Sets.Ices, nextTile);
        Array.Resize(ref TileID.Sets.IcesSlush, nextTile);
        Array.Resize(ref TileID.Sets.IcesSnow, nextTile);
        Array.Resize(ref TileID.Sets.GrassSpecial, nextTile);
        Array.Resize(ref TileID.Sets.JungleSpecial, nextTile);
        Array.Resize(ref TileID.Sets.HellSpecial, nextTile);
        Array.Resize(ref TileID.Sets.Leaves, nextTile);
        Array.Resize(ref TileID.Sets.GeneralPlacementTiles, nextTile);
        Array.Resize(ref TileID.Sets.CanBeClearedDuringGeneration, nextTile);
        Array.Resize(ref TileID.Sets.Corrupt, nextTile);
        Array.Resize(ref TileID.Sets.Hallow, nextTile);
        Array.Resize(ref TileID.Sets.Crimson, nextTile);
        Array.Resize(ref TileID.Sets.BlocksStairs, nextTile);
        Array.Resize(ref TileID.Sets.BlocksStairsAbove, nextTile);
        Array.Resize(ref TileID.Sets.NotReallySolid, nextTile);
        Array.Resize(ref TileID.Sets.ChecksForMerge, nextTile);
        Array.Resize(ref TileID.Sets.FramesOnKillWall, nextTile);
        Array.Resize(ref TileID.Sets.AvoidedByNPCs, nextTile);
        Array.Resize(ref TileID.Sets.InteractibleByNPCs, nextTile);
        Array.Resize(ref TileID.Sets.HousingWalls, nextTile);
        Array.Resize(ref TileID.Sets.BreakableWhenPlacing, nextTile);
        Array.Resize(ref TileID.Sets.TouchDamageVines, nextTile);
        Array.Resize(ref TileID.Sets.TouchDamageSands, nextTile);
        Array.Resize(ref TileID.Sets.TouchDamageHot, nextTile);
        Array.Resize(ref TileID.Sets.TouchDamageOther, nextTile);
        Array.Resize(ref TileID.Sets.Falling, nextTile);
        Array.Resize(ref TileID.Sets.Ore, nextTile);
        while(TileObjectData._data.Count < nextTile)
        {
            TileObjectData._data.Add(null);
        }
    }

    internal static void Unload()
    {
        tiles.Clear();
        nextTile = TileID.Count;
        Array.Resize(ref TileID.Sets.RoomNeeds.CountsAsChair, vanillaChairCount);
        Array.Resize(ref TileID.Sets.RoomNeeds.CountsAsTable, vanillaTableCount);
        Array.Resize(ref TileID.Sets.RoomNeeds.CountsAsTorch, vanillaTorchCount);
        Array.Resize(ref TileID.Sets.RoomNeeds.CountsAsDoor, vanillaDoorCount);
        while(TileObjectData._data.Count > TileID.Count)
        {
            TileObjectData._data.RemoveAt(TileObjectData._data.Count - 1);
        }
    }

    //add to beginning of Terraria.IO.WorldFile.SaveWorldTiles
    internal static void WriteTable(BinaryWriter writer)
    {
        HashSet<ushort> tiles = new HashSet<ushort>();
        for(int x = 0; x < Main.maxTilesX; x++)
        {
            for(int y = 0; y < Main.maxTilesY; y++)
            {
                ushort type = Main.tile[x, y].type;
                if(type >= TileID.Count)
                {
                    tiles.Add(type);
                }
            }
        }
        if(tiles.Count == 0)
        {
            return;
        }
        for(int k = 0; k < 13; k++)
        {
            writer.Write((byte)255);
        }
        writer.Write((ushort)tiles.Count);
        foreach(ushort type in tiles)
        {
            writer.Write(type);
            ModTile tile = GetTile(type);
            writer.Write(tile.mod.Name);
            writer.Write(tile.Name);
        }
    }

    //add to beginning of Terraria.IO.WorldFile.LoadWorldTiles
    //  IDictionary<int, int> modTiles = TileLoader.ReadTable(reader);
    //in Terraria.IO.WorldFile.ValidateWorld after baseStream.Position = (long)array2[1]; add
    //  TileLoader.ReadTable(fileIO);
    internal static IDictionary<int, int> ReadTable(BinaryReader reader)
    {
        IDictionary<int, int> table = new Dictionary<int, int>();
        long startPos = reader.BaseStream.Position;
        for(int k = 0; k < 13; k++)
        {
            if(reader.ReadByte() != (byte)255)
            {
                reader.BaseStream.Seek(startPos, SeekOrigin.Begin);
                return table;
            }
        }
        ushort count = reader.ReadUInt16();
        for(ushort k = 0; k < count; k++)
        {
            ushort type = reader.ReadUInt16();
            string modName = reader.ReadString();
            string tileName = reader.ReadString();
            Mod mod = ModLoader.GetMod(modName);
            if (mod == null)
            {
                table[(int)type] = 0;
            }
            else
            {
                table[(int)type] = mod.TileType(tileName);
            }
        }
        return table;
    }

    //in Terraria.IO.WorldFile.LoadWorldTiles replace tile.type = (ushort)num2; with
    //  tile.type = TileLoader.ReadTileType(num2, modTiles);
    //in Terraria.IO.WorldFile.LoadWorldTiles after if else with importance array add
    //  num2 = (int)tile.type;
    internal static ushort ReadTileType(int type, IDictionary<int, int> table)
    {
        if(table.ContainsKey(type))
        {
            type = table[type];
        }
        return (ushort)type;
    }

    //in Terraria.WorldGen.TileFrame after if else chain inside frameImportant if statement before return add
    //  else { TileLoader.CheckModTile(i, j, num); }
    //in Terraria.TileObject.CanPlace add optional checkStay parameter as false to end
    //  and add && !checkStay to if statement that sets flag4
    internal static void CheckModTile(int i, int j, int type)
    {
        if(WorldGen.destroyObject)
        {
            return;
        }
        int style = 0;
        TileObjectData tileData = TileObjectData.GetTileData(type, style, 0);
        if(tileData == null)
        {
            return;
        }
        int frameX = Main.tile[i, j].frameX;
        int frameY = Main.tile[i, j].frameY;
        int partFrameX = frameX % tileData.CoordinateFullWidth;
        int partFrameY = frameY % tileData.CoordinateFullHeight;
        int partX = partFrameX / (tileData.CoordinateWidth + tileData.CoordinatePadding);
        int partY = 0;
        int remainingFrameY = partFrameY;
        while(remainingFrameY > 0)
        {
            remainingFrameY -= tileData.CoordinateHeights[partY] + tileData.CoordinatePadding;
            partY++;
        }
        i -= partX;
        j -= partY;
        int originX = i + tileData.Origin.X;
        int originY = j + tileData.Origin.Y;
        TileObject objectData;
        bool partiallyDestroyed = false;
        for(int x = i; x < i + tileData.Width; x++)
        {
            for(int y = j; y < j + tileData.Height; y++)
            {
                if(!Main.tile[x, y].active() || Main.tile[x, y].type != type)
                {
                    partiallyDestroyed = true;
                    break;
                }
            }
            if(partiallyDestroyed)
            {
                break;
            }
        }
        if (partiallyDestroyed || !TileObject.CanPlace(originX, originY, type, style, 0, out objectData, true, true))
        {
            WorldGen.destroyObject = true;
            for (int x = i; x < i + tileData.Width; x++)
            {
                for (int y = j; y < j + tileData.Height; y++)
                {
                    if (Main.tile[x, y].type == type && Main.tile[x, y].active())
                    {
                        WorldGen.KillTile(x, y, false, false, false);
                    }
                }
            }
            KillMultiTile(i, j, frameX - partFrameX, frameY - partFrameY, type);
            WorldGen.destroyObject = false;
            for (int x = i - 1; x < i + tileData.Width + 2; x++)
            {
                for (int y = j - 1; y < j + tileData.Height + 2; y++)
                {
                    WorldGen.TileFrame(x, y, false, false);
                }
            }
        }
    }

    //in Terraria.ObjectData.TileObject data make the following public:
    //  newTile, newSubTile, newAlternate, addTile, addSubTile, addAlternate
    internal static void SetDefaults(ModTile tile)
    {
        tile.SetDefaults();
        if(Main.tileLavaDeath[tile.Type])
        {
            Main.tileObsidianKill[tile.Type] = true;
        }
        if(Main.tileSolid[tile.Type])
        {
            Main.tileNoSunLight[tile.Type] = true;
        }
    }

    //in Terraria.WorldGen.KillTile inside if (!effectOnly && !WorldGen.stopDrops) for playing sounds
    //  add if(!TileLoader.KillSound(i, j, tile.type)) { } to beginning of if/else chain and turn first if into else if
    internal static bool KillSound(int i, int j, int type)
    {
        foreach(Mod mod in ModLoader.mods.Values)
        {
            if(mod.globalTile != null && !mod.globalTile.KillSound(i, j, type))
            {
                return false;
            }
        }
        ModTile modTile = GetTile(type);
        if(modTile != null)
        {
            if(!modTile.KillSound(i, j))
            {
                return false;
            }
            Main.PlaySound(modTile.soundType, i * 16, j * 16, modTile.soundStyle);
            return false;
        }
        return true;
    }

    //in Terraria.WorldGen.KillTile before num14 (num dust iteration) is declared, add
    //  TileLoader.NumDust(i, j, tile.type, ref num13);
    internal static void NumDust(int i, int j, int type, bool fail, ref int numDust)
    {
        ModTile modTile = GetTile(type);
        if(modTile != null)
        {
            modTile.NumDust(i, j, fail, ref numDust);
        }
        foreach(Mod mod in ModLoader.mods.Values)
        {
            if(mod.globalTile != null)
            {
                mod.globalTile.NumDust(i, j, type, fail, ref numDust);
            }
        }
    }

    //in Terraria.WorldGen.KillTile replace if (num15 >= 0) with
    //  if(TileLoader.CreateDust(i, j, tile.type, ref num15) && num15 >= 0)
    internal static bool CreateDust(int i, int j, int type, ref int dustType)
    {
        foreach(Mod mod in ModLoader.mods.Values)
        {
            if(mod.globalTile != null && !mod.globalTile.CreateDust(i, j, type, ref dustType))
            {
                return false;
            }
        }
        ModTile modTile = GetTile(type);
        if(modTile != null)
        {
            return modTile.CreateDust(i, j, ref dustType);
        }
        return true;
    }

    //in Terraria.WorldGen.KillTile before if statement checking num43 call
    //  TileLoader.DropCritterChance(i, j, tile.type, ref num43, ref num44, ref num45);
    internal static void DropCritterChance(int i, int j, int type, ref int wormChance, ref int grassHopperChance, ref int jungleGrubChance)
    {
        ModTile modTile = GetTile(type);
        if(modTile != null)
        {
            modTile.DropCritterChance(i, j, ref wormChance, ref grassHopperChance, ref jungleGrubChance);
        }
        foreach(Mod mod in ModLoader.mods.Values)
        {
            if(mod.globalTile != null)
            {
                mod.globalTile.DropCritterChance(i, j, type, ref wormChance, ref grassHopperChance, ref jungleGrubChance);
            }
        }
    }

    //in Terraria.WorldGen.KillTile before if statements checking num49 and num50
    //  add bool vanillaDrop = TileLoader.Drop(i, j, tile.type);
    //  add "vanillaDrop && " to beginning of these if statements
    internal static bool Drop(int i, int j, int type)
    {
        foreach(Mod mod in ModLoader.mods.Values)
        {
            if(mod.globalTile != null && !mod.globalTile.Drop(i, j, type))
            {
                return false;
            }
        }
        ModTile modTile = GetTile(type);
        if(modTile != null)
        {
            if(!modTile.Drop(i, j))
            {
                return false;
            }
            if(modTile.drop > 0)
            {
                Item.NewItem(i * 16, j * 16, 16, 16, modTile.drop, 1, false, -1, false, false);
            }
            return false;
        }
        return true;
    }

    //in Terraria.WorldGen.KillTile before if (!effectOnly && !WorldGen.stopDrops) add
    //  TileLoader.KillTile(i, j, tile.type, ref fail, ref effectOnly, ref noItem);
    internal static void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        ModTile modTile = GetTile(type);
        if(modTile != null)
        {
            modTile.KillTile(i, j, ref fail, ref effectOnly, ref noItem);
        }
        foreach(Mod mod in ModLoader.mods.Values)
        {
            if(mod.globalTile != null)
            {
                mod.globalTile.KillTile(i, j, type, ref fail, ref effectOnly, ref noItem);
            }
        }
    }

    internal static void KillMultiTile(int i, int j, int frameX, int frameY, int type)
    {
        ModTile modTile = GetTile(type);
        if(modTile != null)
        {
            modTile.KillMultiTile(i, j, frameX, frameY);
        }
    }

    //in Terraria.Lighting.PreRenderPhase after label after if statement checking Main.tileLighted call
    //  TileLoader.ModifyLight(n, num17, tile.type, ref num18, ref num19, ref num20);
    internal static void ModifyLight(int i, int j, int type, ref float r, ref float g, ref float b)
    {
        if(!Main.tileLighted[type])
        {
            return;
        }
        ModTile modTile = GetTile(type);
        if(modTile != null)
        {
            modTile.ModifyLight(i, j, ref r, ref g, ref b);
        }
        foreach(Mod mod in ModLoader.mods.Values)
        {
            if(mod.globalTile != null)
            {
                mod.globalTile.ModifyLight(i, j, type, ref r, ref g, ref b);
            }
        }
    }

    //in Terraria.Main.DrawTiles after if statement setting effects call
    //  TileLoader.SetSpriteEffects(j, i, type, ref effects);
    internal static void SetSpriteEffects(int i, int j, int type, ref SpriteEffects spriteEffects)
    {
        ModTile modTile = GetTile(type);
        if(modTile != null)
        {
            modTile.SetSpriteEffects(i, j, ref spriteEffects);
        }
        foreach(Mod mod in ModLoader.mods.Values)
        {
            if(mod.globalTile != null)
            {
                mod.globalTile.SetSpriteEffects(i, j, type, ref spriteEffects);
            }
        }
    }

    //in Terraria.Main.DrawTiles after if statements setting num11 and num12 call
    //  TileLoader.SetDrawPositions(tile, ref num9, ref num11, ref num12);
    internal static void SetDrawPositions(Tile tile, ref int width, ref int offsetY, ref int height)
    {
        if(tile.type >= TileID.Count)
        {
            TileObjectData tileData = TileObjectData.GetTileData(tile.type, 0, 0);
            if(tileData != null)
            {
                int partFrameY = tile.frameY % tileData.CoordinateFullHeight;
                int partY = 0;
                while (partFrameY > 0)
                {
                    partFrameY -= tileData.CoordinateHeights[partY] + tileData.CoordinatePadding;
                    partY++;
                }
                width = tileData.CoordinateWidth;
                offsetY = tileData.DrawYOffset;
                height = tileData.CoordinateHeights[partY];
            }
        }
    }

    //in Terraria.Main.Update after vanilla tile animations call TileLoader.AnimateTiles();
    internal static void AnimateTiles()
    {
        foreach(ModTile modTile in tiles.Values)
        {
            modTile.AnimateTile(ref Main.tileFrame[modTile.Type], ref Main.tileFrameCounter[modTile.Type]);
        }
    }

    //in Terraria.Main.Draw after small if statements setting num15 call
    //  TileLoader.SetAnimationFrame(type, ref num15);
    internal static void SetAnimationFrame(int type, ref int frameY)
    {
        ModTile modTile = GetTile(type);
        if(modTile != null)
        {
            frameY = modTile.animationFrameHeight * Main.tileFrame[type];
        }
    }

    //in Terraria.Main.Draw after calling SetAnimationFrame call
    //  if(!TileLoader.PreDraw(j, i, type, Main.spriteBatch))
    //  { TileLoader.PostDraw(j, i, type, Main.spriteBatch); continue; }
    internal static bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
    {
        foreach(Mod mod in ModLoader.mods.Values)
        {
            if(mod.globalTile != null && !mod.globalTile.PreDraw(i, j, type, spriteBatch))
            {
                return false;
            }
        }
        ModTile modTile = GetTile(type);
        if(modTile != null && !modTile.PreDraw(i, j, spriteBatch))
        {
            return false;
        }
        return true;
    }

    //in Terraria.Main.Draw after if statement checking whether texture2D is null call
    //  TileLoader.PostDraw(j, i, type, Main.spriteBatch);
    internal static void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
    {
        ModTile modTile = GetTile(type);
        if(modTile != null)
        {
            modTile.PostDraw(i, j, spriteBatch);
        }
        foreach(Mod mod in ModLoader.mods.Values)
        {
            if(mod.globalTile != null)
            {
                mod.globalTile.PostDraw(i, j, type, spriteBatch);
            }
        }
    }
}}
