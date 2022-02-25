
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MirosTestMod.Content.Items.Accessories
{
    internal class Dollar3 : ModItem
    {
		private NPC closestNPC = null;
		private int Time = 0;
		private int sec = 60;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Every 2 seconds you apply\n"
								+ "a random debuff to closest NPC");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.rare = ItemRarityID.Orange;
			Item.accessory = true;
			Item.SetNameOverride("3 Dollar Bill");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			// GetDamage returns a reference to the specified damage class' damage StatModifier.
			// Since it doesn't return a value, but a reference to it, you can freely modify it with +-*/ operators.
			// Modifier is a structure that separately holds float additive and multiplicative modifiers.
			// When Modifier is applied to a value, its additive modifiers are applied before multiplicative ones.

			// In this case, we're multiplying by 1.20f, which will mean a 20% damage increase after every additive modifier (and a number of multiplicative modifiers) are applied.
			// Since we're using DamageClass.Generic, this bonus applies to ALL damage the player deals.
			Time++;

			foreach(var np in Main.npc)
            {
				if(!np.townNPC && (closestNPC ==null || Vector2.Distance(player.Center, np.Center)< Vector2.Distance(player.Center, closestNPC.Center)) )
                {
					closestNPC = np;
				}
            }

            if (closestNPC != null && Time>120)
            {
				var r = Main.rand.Next(0,7);
				Time = 0;
                switch (r)
                {
					case 0:
						closestNPC.AddBuff(BuffID.OnFire, 10*sec);
						break;
					case 1:
						closestNPC.AddBuff(BuffID.Poisoned, 10 * sec);
						break;
					case 2:
						closestNPC.AddBuff(BuffID.ShadowFlame, 5 * sec);
						break;
					case 3:
						closestNPC.AddBuff(BuffID.BetsysCurse, 5 * sec);
						break;
					case 4:
						closestNPC.AddBuff(BuffID.Midas, 15 * sec);
						break;
					case 5:
						closestNPC.AddBuff(BuffID.CursedInferno, 5 * sec);
						break;
					case 6:
						closestNPC.AddBuff(BuffID.Frostburn, 7 * sec);
						break;
					case 7:
						closestNPC.AddBuff(BuffID.Venom, 3 * sec);
						break;
				}

			}
		}


		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Silk, 10)
				.AddIngredient(ItemID.Feather, 1)
				.AddIngredient(ItemID.BlackInk, 1)
				.AddTile(TileID.DemonAltar)
				.Register();
		}

	}
}
