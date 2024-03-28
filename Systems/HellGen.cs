using Terraria.WorldBuilding;
using System.Collections.Generic;
using System;
using Terraria.GameContent.Generation;
using Terraria.IO;

namespace HellRework.Systems
{
    class HellGen : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            if (tasks.Find(x => x.Name == "Underworld", out GenPass a, out int index))
            {
                a.Disable();

                tasks.Insert(index + 1, new PassLegacy("Reworked Underworld", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    progress.Message = "Reforging the Underworld";

                    UnderworldTerrain();
                }));
            }

            if (tasks.Find(x => x.Name == "Hellforge", out GenPass b))
            {
                b.Disable();
            }
        }

        public void UnderworldTerrain()
        {
            FastNoiseLite hellstoneNoise = new(WorldGen.genRand.Next(3000));
            FastNoiseLite roofNoise1 = new(WorldGen.genRand.Next(3000));
            FastNoiseLite roofNoise2 = new(WorldGen.genRand.Next(3000));
            FastNoiseLite terrainNoise1 = new(WorldGen.genRand.Next(3000));
            FastNoiseLite terrainNoise2 = new(WorldGen.genRand.Next(3000));
            FastNoiseLite caveNoise1 = new(WorldGen.genRand.Next(3000));
            FastNoiseLite coreStoneNoise1 = new(WorldGen.genRand.Next(3000));
            FastNoiseLite coreStoneNoise2 = new(WorldGen.genRand.Next(3000));
            FastNoiseLite fleshNoise = new(WorldGen.genRand.Next(3000));


            for (int j = Main.UnderworldLayer; j < Main.maxTilesY; j++)
            {
                for (int i = 0; i < Main.maxTilesX; i++)
                {
                    float terrainOffset = 55f * terrainNoise2.GetNoise(i / 3f, j / 20f);

                    //Whether or not *bottom* terrain should generate here
                    bool generateTile =
                        (j > (Main.maxTilesY - 75f) + terrainNoise1.GetNoise(i / 1.5f, j * 2f) * 50f + terrainOffset) &&
                        (caveNoise1.GetNoise(i, j) < 0.5f);

                    int tileType = TileID.Ash;

                    float lavaHeight = MathHelper.Lerp(Main.UnderworldLayer, Main.maxTilesY, 0.65f);

                    if (j > lavaHeight + 5f * coreStoneNoise1.GetNoise(i * 4f, j * 4f) - 5f || coreStoneNoise2.GetNoise(i * 1.5f, j * 1.5f) > 0.5f)
                    {
                        tileType = ModContent.TileType<Tiles.Corestone>();
                    }

                    if (j > lavaHeight + 5f * coreStoneNoise1.GetNoise(i * 4f, j * 4f) - 5f && fleshNoise.GetNoise(i * 2f, j * 1f) > 0.5f)
                    {
                        tileType = TileID.FleshBlock;
                    }

                    if (hellstoneNoise.GetNoise(i * 4f, j * 4f) > MathHelper.Lerp(1f, 0.65f, (float)(j - Main.UnderworldLayer) / (Main.maxTilesY - Main.UnderworldLayer)))
                    {
                        tileType = TileID.Hellstone;
                    }

                    if (generateTile)
                    {
                        WorldGen.PlaceTile(i, j, tileType, true, true);
                    }
                    else
                    {
                        bool seamlessTop = j <= Main.UnderworldLayer + 15f + 7.5f * roofNoise2.GetNoise(i * 2f, j);

                        if ((j > Main.UnderworldLayer + 40f + 40f * roofNoise1.GetNoise(i / 2f, j / 3f) + terrainOffset && 
                            !seamlessTop) || //Makes sure that the top remains seamless looking
                            (caveNoise1.GetNoise(i, j) > 0.5f)) //Generate caves within the roof of the underworld
                        {
                            WorldGen.KillTile(i, j, false, false, true);
                        }
                        else if (!WorldGen.TileEmpty(i, j) && Main.tileSolid[WorldGen.TileType(i, j)] && 
                            !seamlessTop) //So that normal vanilla tiles don't have a hard cutoff
                        {
                            WorldGen.PlaceTile(i, j, tileType, true, true);
                        }

                        if (j > lavaHeight)
                        {
                            WorldGen.PlaceLiquid(i, j, (byte)LiquidID.Lava, 255);
                        }
                    }
                }
            }
        }
    }
}
