using Terraria.Audio;

namespace HellRework.Tiles
{
    class Corestone : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileSpelunker[Type] = false;
            Main.tileMerge[Type][TileID.Stone] = true;
            Main.tileMerge[TileID.Stone][Type] = true;
            Main.tileMerge[Type][TileID.Mud] = true;
            Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileMerge[Type][TileID.Ash] = true;
            Main.tileMerge[TileID.Ash][Type] = true;
            Main.tileMerge[Type][TileID.Hellstone] = true;
            Main.tileMerge[TileID.Hellstone][Type] = true;
            Main.tileMerge[Type][TileID.FleshBlock] = true;
            Main.tileMerge[TileID.FleshBlock][Type] = true;

            SoundStyle style = new SoundStyle("HellRework/Sounds/Effects/StoneHit2")
            {
                PitchVariance = 0.3f
            };
            style.Pitch += 0f;
            HitSound = style;

            MinPick = 65;
            MineResist = 2f;
            DustType = DustID.Stone;

            AddMapEntry(new Color(69, 40, 60));
        }
    }
}
