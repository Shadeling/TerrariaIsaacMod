using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Audio;

namespace MirosTestMod.Content.Items.Accessories
{
	//[AutoloadEquip(EquipType.Front)]
	internal class Purity: ModItem
    {

		private int rand = 0;
		private int lastHP = 0;
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Every time you take damage randomly switches between:\n"
								+ "+25% damage, +50% movement speed, +15% crit chance\n"
								+ "15 armor reduction or stronger knockback");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.rare = ItemRarityID.Orange;
			Item.accessory = true;
			Item.SetNameOverride("Purity");
			Item.vanity = true;

			//Main.item[Item.type]= 5;
		}

        public override void UpdateVanity(Player player)
        {
            //base.UpdateVanity(player);
			//player.
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if(lastHP > player.statLife)
            {
				rand = Main.rand.Next(0, 5);
				lastHP = player.statLife;
            }
            else
            {
				lastHP = player.statLife;
			}

			choose(rand, player);

		}


		private void choose(int r, Player player)
        {
            switch (r)
            {
				case 0:
					player.GetDamage(DamageClass.Generic) *= 1.25f;
					//Item
					break;
				case 1:
					player.GetCritChance(DamageClass.Generic) += 15;
					break;
				case 2:
					player.moveSpeed *= 1.5f;
					break;
				case 3:
					player.GetKnockback(DamageClass.Generic) *= 3f;
					break;
				case 4:
					player.armorPenetration += 15;
					break;
			}
        }

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.PixieDust, 25)
				.AddIngredient(ItemID.Moonglow, 5)
				.AddTile(TileID.DemonAltar)
				.Register();
		}

	}
}
