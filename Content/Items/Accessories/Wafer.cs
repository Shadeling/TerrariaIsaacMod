using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace MirosTestMod.Content.Items.Accessories
{
    internal class Wafer : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Reduse damage taken by 50%,\n"
							 + "but reduse damage dealt by 50%\n");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.rare = ItemRarityID.Orange;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Generic) *= 0.50f;

			// GetCrit, similarly to GetDamage, returns a reference to the specified damage class' crit chance.
			// In this case, we're adding 10% crit chance, but only for the melee DamageClass (as such, only melee weapons will receive this bonus).
			player.endurance = 1f - (0.5f * (1f - player.endurance));
		}


		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.DirtBlock, 100)
				.AddTile(TileID.DemonAltar)
				.Register();
		}
	}

}
