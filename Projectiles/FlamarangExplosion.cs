namespace HellRework.Projectiles
{
    public class FlamarangExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 7;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 60 * 3);
        }

        const int time = 20;

        public override void SetDefaults()
        {
            Projectile.timeLeft = time;
            Projectile.width = 70;
            Projectile.height = 70;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()   
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > (float)time / Main.projFrames[Type])
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
            }


        }
    }
}
