using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace MirosTestMod.Content.Items.Accessories
{
    internal class Crown_of_light : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("+100% all damage if you have >95% hp\n");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.rare = ItemRarityID.Orange;
			Item.accessory = true;
			Item.SetNameOverride("Crown of Light");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			// GetDamage returns a reference to the specified damage class' damage StatModifier.
			// Since it doesn't return a value, but a reference to it, you can freely modify it with +-*/ operators.
			// Modifier is a structure that separately holds float additive and multiplicative modifiers.
			// When Modifier is applied to a value, its additive modifiers are applied before multiplicative ones.

			// In this case, we're multiplying by 1.20f, which will mean a 20% damage increase after every additive modifier (and a number of multiplicative modifiers) are applied.
			// Since we're using DamageClass.Generic, this bonus applies to ALL damage the player deals.
			if(player.statLife > player.statLifeMax2*0.95f)
            {
				player.GetDamage(DamageClass.Generic) *= 2f;
			}
		}


		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.SoulofLight, 20)
				.AddIngredient(ItemID.PlatinumCrown, 1)
				.AddTile(TileID.DemonAltar)
				.Register();
		}

	}
}
