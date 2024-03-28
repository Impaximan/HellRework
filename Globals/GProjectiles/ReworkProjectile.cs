namespace HellRework.Globals.GProjectiles
{
    public class ReworkProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile projectile)
        {
            switch (projectile.type)
            {
                case ProjectileID.Flamarang:
                    projectile.extraUpdates = 1;
                    break;
                default:
                    break;
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            switch (projectile.type)
            {
                case ProjectileID.Flamarang:
                    projectile.active = false;
                    SoundEngine.PlaySound(SoundID.DD2_GoblinBomb);
                    Projectile.NewProjectile(new EntitySource_OnHit(projectile, target, "Flamarang Hit"), projectile.Center, -projectile.velocity / 2, ModContent.ProjectileType<Projectiles.FlamarangExplosion>(), projectile.damage, projectile.knockBack, projectile.owner);
                    break;
                default:
                    break;
            }
        }
    }
}
