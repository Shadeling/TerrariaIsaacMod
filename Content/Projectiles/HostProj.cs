using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MirosTestMod.Content.Projectiles
{
    internal class HostProj : ModProjectile
    {

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Host Projectile");
		}

		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 20;
			Projectile.alpha = 255;
			Projectile.timeLeft = 900;
			Projectile.penetrate = -1;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
			Projectile.netImportant = true;
			Projectile.light = 0.5f;
			Projectile.aiStyle = 1;
			//Projectile.

			AIType = ProjectileID.Bullet;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.Kill();
			return false;
		}




	}

}
