using System;

namespace Mod.DungPham.KoiOctiiu957
{
	public class AutoZone
	{
		public static bool isAuto = false;
		public static int targetZone = -1;
		public static int lastMapId = -1;
		public static long lastTimeRefresh = 0;
		public static long lastTimeRequestChange = 0;

		public static void Start(int zoneId)
		{
			isAuto = true;
			targetZone = zoneId;
			lastMapId = TileMap.mapID;
			lastTimeRefresh = 0;
			lastTimeRequestChange = 0;
			GameScr.info1.addInfo("Bắt đầu Auto vào khu " + zoneId + "...", 0);
		}

		public static void Stop()
		{
			if (isAuto)
			{
				isAuto = false;
				targetZone = -1;
				lastMapId = -1;
			}
		}

		public static void Toggle(int zoneId)
		{
			if (isAuto && targetZone == zoneId)
			{
				Stop();
				GameScr.info1.addInfo("Đã hủy Auto vào khu " + zoneId, 0);
			}
			else
			{
				Start(zoneId);
			}
		}

		public static void Update()
		{
			if (!isAuto || targetZone == -1)
			{
				return;
			}

			if (global::Char.myCharz() == null || global::Char.myCharz().cHP <= 0 || global::Char.myCharz().statusMe == 14)
			{
				Stop();
				return;
			}

			if (TileMap.mapID != lastMapId)
			{
				Stop();
				return;
			}

			if (TileMap.zoneID == targetZone)
			{
				GameScr.info1.addInfo("Đã vào khu " + targetZone + " thành công!", 0);
				Stop();
				if (GameCanvas.panel != null && GameCanvas.panel.isShow && GameCanvas.panel.type == 3)
				{
					GameCanvas.panel.hide();
				}
				return;
			}

			long now = mSystem.currentTimeMillis();

			// Check if target zone is now available
			if (GameScr.gI().zones != null && GameScr.gI().numPlayer != null && GameScr.gI().maxPlayer != null && GameScr.gI().pts != null)
			{
				int targetIdx = -1;
				for (int i = 0; i < GameScr.gI().zones.Length; i++)
				{
					if (GameScr.gI().zones[i] == targetZone)
					{
						targetIdx = i;
						break;
					}
				}

				if (targetIdx != -1 && targetIdx < GameScr.gI().numPlayer.Length && targetIdx < GameScr.gI().maxPlayer.Length && targetIdx < GameScr.gI().pts.Length)
				{
					bool isFree = (GameScr.gI().numPlayer[targetIdx] < GameScr.gI().maxPlayer[targetIdx]);
					if (isFree)
					{
						if (now - lastTimeRequestChange > 500)
						{
							lastTimeRequestChange = now;
							Service.gI().requestChangeZone(targetIdx, -1);
						}
					}
				}
			}

			// Periodically refresh zone list from server
			if (now - lastTimeRefresh > 600)
			{
				lastTimeRefresh = now;
				Service.gI().openUIZone();
			}
		}
	}
}
