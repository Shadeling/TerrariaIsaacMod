using Terraria;
using Terraria.ModLoader;

namespace MirosTestMod.Content.Dusts
{
	public class Sparkle : ModDust
	{
		public override void OnSpawn(Dust dust) {
			dust.velocity *= 2f; // Multiply the dust's start velocity by 0.4, slowing it down
			dust.noGravity = true; // Makes the dust have no gravity.
			dust.noLight = false; // Makes the dust emit no light.
			dust.scale *= 2f; // Multiplies the dust's initial scale by 1.5.
		}

		public override bool Update(Dust dust) { // Calls every frame the dust is active
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.15f;
			dust.scale *= 0.99f;

			float light = 0.35f * dust.scale;

			Lighting.AddLight(dust.position, light, light, light);

			if (dust.scale < 0.2f) {
				dust.active = false;
			}
				
			return false; // Return false to prevent vanilla behavior.
		}
	}
}
