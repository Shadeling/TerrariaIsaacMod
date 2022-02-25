using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace MirosTestMod.Content.Items.Accessories
{
	public class Agaric : ModItem
	{
		public override void SetStaticDefaults()
		{
				Tooltip.SetDefault("5% increased all damage\n"
								 + "5% increased crit chance\n"
								 + "20% increased move speed\n"
								 + "+ 1 HP regen + 50 health + 5 defense\n");

				CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
				Item.width = 40;
				Item.height = 40;
				Item.rare = ItemRarityID.Orange;
				Item.accessory = true;
				Item.lifeRegen = 1;
				Item.defense = 5;
				Item.SetNameOverride("Magic Mushroom");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
				// GetDamage returns a reference to the specified damage class' damage StatModifier.
				// Since it doesn't return a value, but a reference to it, you can freely modify it with +-*/ operators.
				// Modifier is a structure that separately holds float additive and multiplicative modifiers.
				// When Modifier is applied to a value, its additive modifiers are applied before multiplicative ones.

				// In this case, we're multiplying by 1.20f, which will mean a 20% damage increase after every additive modifier (and a number of multiplicative modifiers) are applied.
				// Since we're using DamageClass.Generic, this bonus applies to ALL damage the player deals.
			player.GetDamage(DamageClass.Generic) *= 1.05f;

				// GetCrit, similarly to GetDamage, returns a reference to the specified damage class' crit chance.
				// In this case, we're adding 10% crit chance, but only for the melee DamageClass (as such, only melee weapons will receive this bonus).
			player.GetCritChance(DamageClass.Generic) += 5;

			player.moveSpeed *= 1.2f;
			player.statLifeMax2 += 50;

		}


		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.GlowingMushroom, 500)
				.AddIngredient(ItemID.SoulofFright, 5)
				.AddIngredient(ItemID.SoulofMight, 5)
				.AddIngredient(ItemID.SoulofSight, 5)
				.AddIngredient(ItemID.LifeFruit, 1)
				.AddTile(TileID.DemonAltar)
				.Register();
		}
	}
}
