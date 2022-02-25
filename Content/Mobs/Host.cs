using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using MirosTestMod.Content.Projectiles;
using Terraria.Audio;

namespace MirosTestMod.Content.Mobs
{
	public class Host : ModNPC
	{
		private float aggrorange = 450f;
		private enum ActionState
		{
			Asleep,
			Stand,
			PreShoot,
			Shoot,
		}

		public ref float AI_State => ref NPC.ai[0];
		public ref float AI_Timer => ref NPC.ai[1];

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flutter Slime"); // Automatic from .lang files
			Main.npcFrameCount[NPC.type] = 4; // make sure to set this for your modnpcs.
		}

		public override void SetDefaults()
		{
			NPC.width = 60; // The width of the npc's hitbox (in pixels)
			NPC.height = 100; // The height of the npc's hitbox (in pixels)
			NPC.aiStyle = -1; // This npc has a completely unique AI, so we set this to -1. The default aiStyle 0 will face the player, which might conflict with custom AI code.
			NPC.damage = 10; // The amount of damage that this npc deals
			NPC.defense = 5; // The amount of defense that this npc has
			NPC.lifeMax = 50; // The amount of health that this npc has
			NPC.HitSound = SoundID.NPCHit1; // The sound the NPC will make when being hit.
			NPC.DeathSound = SoundID.NPCDeath1; // The sound the NPC will make when it dies.
			NPC.value = 500f; // How many copper coins the NPC will drop when killed.
			NPC.stepSpeed = 0;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			// we would like this npc to spawn in the overworld.
			return SpawnCondition.Crimson.Chance * 0.1f;
		}

		public override void AI()
		{
			// The npc starts in the asleep state, waiting for a player to enter range
			switch (AI_State)
			{
				case (float)ActionState.Asleep:
					FallAsleep();
					break;
				case (float)ActionState.Stand:
					Stand();
					break;
				case (float)ActionState.PreShoot:
					PreShoot();
					break;
				case (float)ActionState.Shoot:
					Shoot();
					break;
			}
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.spriteDirection = NPC.direction;

			switch (AI_State)
			{
				case (float)ActionState.Asleep:
					// npc.frame.Y is the goto way of changing animation frames. npc.frame starts from the top left corner in pixel coordinates, so keep that in mind.
					NPC.frame.Y = (int)ActionState.Asleep * frameHeight;
					break;
				case (float)ActionState.Stand:
					// npc.frame.Y is the goto way of changing animation frames. npc.frame starts from the top left corner in pixel coordinates, so keep that in mind.
					NPC.frame.Y = (int)ActionState.Stand * frameHeight;
					break;
				case (float)ActionState.PreShoot:
					// npc.frame.Y is the goto way of changing animation frames. npc.frame starts from the top left corner in pixel coordinates, so keep that in mind.
					NPC.frame.Y = (int)ActionState.PreShoot * frameHeight;
					break;
				case (float)ActionState.Shoot:
					// npc.frame.Y is the goto way of changing animation frames. npc.frame starts from the top left corner in pixel coordinates, so keep that in mind.
					NPC.frame.Y = (int)ActionState.Shoot * frameHeight;
					break;
			}
		}


		private void FallAsleep()
		{
			NPC.dontTakeDamage = true;
			NPC.TargetClosest(true);

			// Now we check the make sure the target is still valid and within our specified notice range (500)
			if (NPC.HasValidTarget && Main.player[NPC.target].Distance(NPC.Center) <= 300f)
			{
				// Since we have a target in range, we change to the Notice state. (and zero out the Timer for good measure)
				AI_State = (float)ActionState.PreShoot;
				AI_Timer = 0;
			}
		}

		private void Stand()
		{
			if (Main.player[NPC.target].Distance(NPC.Center) <= aggrorange)
			{
				AI_Timer++;

				if (AI_Timer >= 20)
				{
					AI_State = (float)ActionState.PreShoot;
					AI_Timer = 0;
				}
			}
			else
			{
				AI_Timer++;
				NPC.TargetClosest(true);

				if (!NPC.HasValidTarget || Main.player[NPC.target].Distance(NPC.Center) > aggrorange)
				{
					if (AI_Timer > 20)
					{
						AI_State = (float)ActionState.Asleep;
						AI_Timer = 0;
					}
				}
			}
		}

		private void PreShoot()
		{
			NPC.dontTakeDamage = false;
			AI_Timer++;


			if (!NPC.HasValidTarget || Main.player[NPC.target].Distance(NPC.Center) > aggrorange)
			{
				if (AI_Timer > 20)
				{
					AI_State = (float)ActionState.Stand;
					AI_Timer = 0;
				}
			}

			if (AI_Timer > 80)
			{
				AI_State = (float)ActionState.Shoot;
				SoundEngine.PlaySound(SoundID.DD2_BetsyScream, NPC.position);
				AI_Timer = 0;
			}
		}

		private void Shoot()
		{
			NPC.dontTakeDamage = false;
			AI_Timer++;


			if (!NPC.HasValidTarget || Main.player[NPC.target].Distance(NPC.Center) > aggrorange || AI_Timer > 30)
			{
				if (AI_Timer > 10)
				{
					AI_State = (float)ActionState.PreShoot;
					AI_Timer = 0;
				}
			}

			if (AI_Timer == 20)
			{
				Vector2 position = NPC.position + new Vector2(0,NPC.height/3);

				int type = ModContent.ProjectileType<HostProj>();
				int damage = NPC.damage;
				Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), position, (Main.player[NPC.target].position - position) * 0.02f, type, damage, 0.1f, Main.myPlayer);
			}

		}
	}
}
