/*
 * PacketType.cs
 * 
 * Copyright © 2011-2012 by Sijmen Schoon and Adam Hellberg.
 * 
 * This file is part of Sharpcraft.
 * 
 * Sharpcraft is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Sharpcraft is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Sharpcraft.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * Disclaimer: Sharpcraft is in no way affiliated with Mojang AB and/or
 * any of its employees and/or licensors.
 * Sijmen Schoon and Adam Hellberg do not take responsibility for
 * any harm caused, direct or indirect, to your Minecraft account
 * via the use of Sharpcraft.
 * 
 * "Minecraft" is a trademark of Mojang AB.
 */

namespace Sharpcraft.Networking.Enums
{
	/// <summary>
	/// All the different packet types.
	/// </summary>
	/// <remarks>http://wiki.vg/Protocol</remarks>
	public enum PacketType
	{
		KeepAlive					= 0x00,
		LoginRequest				= 0x01,
		Handshake					= 0x02,
		ChatMessage					= 0x03,
		TimeUpdate					= 0x04,
		EntityEquipment				= 0x05,
		SpawnPosition				= 0x06,
		UseEntity					= 0x07,
		UpdateHealth				= 0x08,
		Respawn						= 0x09,
		Player						= 0x0A,
		PlayerPosition				= 0x0B,
		PlayerLook					= 0x0C,
		PlayerPositionAndLook		= 0x0D,
		PlayerDigging				= 0x0E,
		PlayerBlockPlacement		= 0x0F,
		HoldingChange				= 0x10,
		UseBed						= 0x11,
		Animation					= 0x12,
		EntityAction				= 0x13,
		NamedEntitySpawn			= 0x14,
		PickupSpawn					= 0x15,
		CollectItem					= 0x16,
		AddObjectVehicle			= 0x17,
		MobSpawn					= 0x18,
		EntityPainting				= 0x19,
		ExperienceOrb				= 0x1A,
		EntityVelocity				= 0x1C,
		DestroyEntity				= 0x1D,
		Entity						= 0x1E,
		EntityRelativeMove			= 0x1F,
		EntityLook					= 0x20,
		EntityLookAndRelativeMove	= 0x21,
		EntityTeleport				= 0x22,
		EntityHeadLook				= 0x23,
		EntityStatus				= 0x26,
		AttachEntity				= 0x27,
		EntityMetadata				= 0x28,
		EntityEffect				= 0x29,
		RemoveEntityEffect			= 0x2A,
		Experience					= 0x2B,
		PreChunk					= 0x32,
		MapChunk					= 0x33,
		MultiBlockChange			= 0x34,
		BlockChange					= 0x35,
		BlockAction					= 0x36,
		Explosion					= 0x3C,
		SoundParticleEffect			= 0x3D,
		NewInvalidState				= 0x46,
		Thunderbolt					= 0x47,
		OpenWindow					= 0x64,
		CloseWindow					= 0x65,
		WindowClick					= 0x66,
		SetSlot						= 0x67,
		WindowItems					= 0x68,
		UpdateWindowProperty		= 0x69,
		Transaction					= 0x6A,
		CreativeInventoryAction		= 0x6B,
		EnchantItem					= 0x6C,
		UpdateSign					= 0x82,
		ItemData					= 0x83,
		UpdateTileEntity			= 0x84,
		IncrementStatistic			= 0xC8,
		PlayerListItem				= 0xC9,
		PluginMessage				= 0xFA,
		ServerListPing				= 0xFE,
		DisconnectKick				= 0xFF
	}
}
