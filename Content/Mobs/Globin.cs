using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using MirosTestMod.Content.Projectiles;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;

namespace MirosTestMod.Content.Mobs
{
    internal class Globin : ModNPC
    {
		private int walkState = 0;
		private int damage = 20;
		private int ressurects = 0;
		private enum ActionState
		{
			Walk,
			Dead
		}

		public ref float AI_State => ref NPC.ai[0];
		public ref float AI_Timer => ref NPC.ai[1];

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Globin"); // Automatic from .lang files
			Main.npcFrameCount[NPC.type] = 15; // make sure to set this for your modnpcs.

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{ // Influences how the NPC looks in the Bestiary
				Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
			};
		}

		public override void SetDefaults()
		{
			NPC.width = 31; // The width of the npc's hitbox (in pixels)
			NPC.height = 35; // The height of the npc's hitbox (in pixels)
			NPC.aiStyle = -1; // This npc has a completely unique AI, so we set this to -1. The default aiStyle 0 will face the player, which might conflict with custom AI code.
			NPC.damage = damage; // The amount of damage that this npc deals
			NPC.defense = 5; // The amount of defense that this npc has
			NPC.lifeMax = 200; // The amount of health that this npc has
			NPC.HitSound = SoundID.NPCHit1; // The sound the NPC will make when being hit.
			NPC.DeathSound = SoundID.NPCDeath1; // The sound the NPC will make when it dies.
			NPC.value = 500f; // How many copper coins the NPC will drop when killed.
			NPC.knockBackResist = 0.5f;
			AnimationType = NPCID.Zombie;
			AIType = NPCID.Zombie;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			//npcLoot.Add(ItemDropRule.Common(ItemID.Shackle, 50)); // Drop shackles with a 1 out of 50 chance.
			//	npcLoot.Add(ItemDropRule.Common(ItemID.ZombieArm, 250)); // Drop zombie arm with a 1 out of 250 chance.	
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			// we would like this npc to spawn in the overworld.
			return SpawnCondition.OverworldNight.Chance * 0.1f;
		}

		public override void AI()
		{
			// The npc starts in the asleep state, waiting for a player to enter range
			switch (AI_State)
			{
				case (float)ActionState.Walk:
					Walk();
					break;
				case (float)ActionState.Dead:
					Dead();
					break;
			}
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.spriteDirection = -NPC.direction;

			switch (AI_State)
			{
				case (float)ActionState.Walk:
					// npc.frame.Y is the goto way of changing animation frames. npc.frame starts from the top left corner in pixel coordinates, so keep that in mind.
					if (walkState > 13) walkState = 0;
					NPC.frame.Y = walkState * frameHeight;
					walkState++;
					break;
				case (float)ActionState.Dead:
					NPC.frame.Y = 14 * frameHeight;
					break;
			}
		}

		private void Walk()
		{
			AI_Timer++;
			if(Main.player[NPC.target].position.Y < NPC.position.Y-1f && AI_Timer>60)
            {
				var direction = (Main.player[NPC.target].position.X - NPC.position.X) > 0 ? 3 : -3;
				NPC.velocity = new Vector2(direction, -8);
				AI_Timer = 0;
			}


			if ((NPC.life < NPC.lifeMax / 2) && ressurects<4)
            {
				AI_State = (float)ActionState.Dead;
				//AIType = 0;
				AI_Timer = 0;
				ressurects++;
			}
		}

		private void Dead()
		{
			AI_Timer+=1;
			NPC.velocity = new Vector2(0, 0);

			if (AI_Timer > 240)
            {
				NPC.life = (int)(NPC.lifeMax*0.75);
				AI_State = (float)ActionState.Walk;
				AI_Timer = 0;
			}
		}

	}
}
