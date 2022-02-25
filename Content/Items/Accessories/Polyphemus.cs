using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace MirosTestMod.Content.Items.Accessories
{
    internal class Polyphemus : ModItem
    {
		private float Time = 0;
		private bool hasImmune = false;
		private float multiply = 3f;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Multiply all non summon damage by 300%\n"
							 + "but every 6 seconds you cant attack for 3 seconds\n"
							 + "do not work if you have Curse Immunity :)\n");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 50;
			Item.height = 50;
			Item.rare = ItemRarityID.Orange;
			Item.accessory = true;
			Item.SetNameOverride("Magic Mushroom");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			// GetDamage returns a reference to the specified damage class' damage StatModifier.
			// Since it doesn't return a value, but a reference to it, you can freely modify it with +-*/ operators.
			// Modifier is a structure that separately holds float additive and multiplicative modifiers.
			// When Modifier is applied to a value, its additive modifiers are applied before multiplicative ones.
			if (!hasImmune) { 
				player.GetDamage(DamageClass.Magic) *= multiply;
				player.GetDamage(DamageClass.Ranged) *= multiply;
				player.GetDamage(DamageClass.Melee) *= multiply;
				player.GetDamage(DamageClass.Throwing) *= multiply;
			}
			else {
				player.GetDamage(DamageClass.Magic) /= multiply;
				player.GetDamage(DamageClass.Ranged) /= multiply;
				player.GetDamage(DamageClass.Melee) /= multiply;
				player.GetDamage(DamageClass.Throwing) /= multiply;
			}

			Time++;

			if(Time > 6 * 60)
            {
				Time = 0;
				player.AddBuff(23, 3*60);
                if (player.HasBuff(23))
                {
					hasImmune = false;

				}
				else hasImmune = true;
			}

		}


		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Lens, 10)
				.AddIngredient(ItemID.EoCShield, 1)
				.AddTile(TileID.DemonAltar)
				.Register();
		}
	}
}
