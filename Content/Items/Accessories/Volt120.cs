
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MirosTestMod.Content.Projectiles;

namespace MirosTestMod.Content.Items.Accessories
{
    internal class Volt120 : ModItem
    {
		private int Time = 0;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Every seconds you shoot Meowmere\n"
								+ "into closest enemy");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 50;
			Item.height = 50;
			Item.rare = ItemRarityID.Orange;
			Item.accessory = true;
			Item.SetNameOverride("120 Volts");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			Time++;

			if(Time > 60)
            {
				Time = 0;
				foreach (var np in Main.npc)
				{
					if (!np.townNPC && Vector2.Distance(player.Center, np.Center) < 100 && np.damage>0)
					{
						Projectile.NewProjectile(player.GetProjectileSource_Accessory(Item), player.position, (np.position - player.position) * 0.2f, ProjectileID.Meowmere, 15, 0.1f, Main.myPlayer);
						//Projectile.NewProjectile(player.GetProjectileSource_Accessory(Item), player.position, (np.position - player.position) * 0.2f, ModContent.ProjectileType<Ligthning>(), 15, 0, Main.myPlayer);
						break;
					}
				}
			}
		}


		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Wood, 1)
				.AddIngredient(ItemID.StoneBlock, 1)
				.AddIngredient(ItemID.SandBlock, 1)
				.AddIngredient(ItemID.Coral, 1)
				.AddIngredient(ItemID.GraniteBlock, 1)
				.AddIngredient(ItemID.MarbleBlock, 1)
				.AddIngredient(ItemID.AshBlock, 1)
				.AddTile(TileID.DemonAltar)
				.Register();
		}

	}
}
