﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace Terraria.ModLoader {
public class GlobalTile
{
    public Mod mod
    {
        get;
        internal set;
    }

    public void AddToArray(ref int[] array, int type)
    {
        Array.Resize(ref array, array.Length + 1);
        array[array.Length - 1] = type;
    }

    public virtual void SetDefaults() { }

    public virtual bool KillSound(int i, int j, int type)
    {
        return true;
    }

    public virtual void NumDust(int i, int j, int type, bool fail, ref int num) { }

    public virtual bool CreateDust(int i, int j, int type, ref int dustType)
    {
        return true;
    }

    public virtual void DropCritterChance(int i, int j, int type, ref int wormChance, ref int grassHopperChance, ref int jungleGrubChance) { }

    public virtual bool Drop(int i, int j, int type)
    {
        return true;
    }

    public virtual void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem) { }

    public virtual void ModifyLight(int i, int j, int type, ref float r, ref float g, ref float b) { }

    public virtual void SetSpriteEffects(int i, int j, int type, ref SpriteEffects spriteEffects) { }

    public virtual bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
    {
        return true;
    }

    public virtual void PostDraw(int i, int j, int type, SpriteBatch spriteBatch) { }
}}
