using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Audio;

namespace MirosTestMod.Content.Items.Accessories
{
    internal class Dead_Cat: ModItem
    {
		private float Time = 0;
		private bool cooldown = false;
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("If your health drops below 20%\n"
								+ "you instantly restore all health(cooldown 180 sec)\n");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 64;
			Item.height = 64;
			Item.rare = ItemRarityID.Orange;
			Item.accessory = true;
			Item.SetNameOverride("Dead Cat");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			Time++;

            if ((player.statLife < player.statLifeMax2 * 0.2f) && cooldown==false)
            {
				player.statLife = player.statLifeMax2;
				cooldown = true;
				Time = 0;
				for(int i=0; i<100; i++)
                {
					var rand1 = Main.rand.Next(-50, 50);
					var rand2 = Main.rand.Next(-50,50);
					Dust.NewDust(player.position + new Vector2(rand1, rand2), 30, 30, ModContent.DustType<Dusts.Sparkle>());
				}
				SoundEngine.PlaySound(SoundID.Pixie, player.position);
			}

			if(Time > 60 * 180) //180 sec cooldown
            {
				cooldown = false;
				Time = 0;
			}

		}


		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Silk, 25)
				.AddIngredient(ItemID.Hay, 25)
				.AddIngredient(ItemID.Bone, 25)
				.AddTile(TileID.DemonAltar)
				.Register();
		}

	}
}
