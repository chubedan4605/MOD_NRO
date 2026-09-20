using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Mod.DungPham.KoiOctiiu957
{
	// Token: 0x020000EF RID: 239
	public class AutoMap : IActionListener
	{
		// Token: 0x06000AEA RID: 2794 RVA: 0x00009135 File Offset: 0x00007335
		public static AutoMap getInstance()
		{
			if (AutoMap._Instance == null)
			{
				AutoMap._Instance = new AutoMap();
			}
			return AutoMap._Instance;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000A21EC File Offset: 0x000A03EC
		public static void Update()
		{
			long num = mSystem.currentTimeMillis();
			if (global::Char.myCharz().meDead)
			{
				AutoMap.lastWaitTime = num + 1000L;
			}
			if (TileMap.mapID == AutoMap.IdMapEnd)
			{
				AutoMap.FinishXmap();
				return;
			}
			bool flag = false;
			if (TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23)
			{
				if (AutoMap.isEatChicken)
				{
					for (int i = 0; i < GameScr.vItemMap.size(); i++)
					{
						ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
						if ((itemMap.playerId == global::Char.myCharz().charID || itemMap.playerId == -1) && itemMap.template.id == 74)
						{
							flag = true;
							global::Char.myCharz().itemFocus = itemMap;
							if (num - AutoMap.lastWaitTime > 600L)
							{
								AutoMap.lastWaitTime = num;
								Service.gI().pickItem(global::Char.myCharz().itemFocus.itemMapID);
								return;
							}
						}
					}
				}
				if (AutoMap.isXmaping && AutoMap.isHarvestPean && GameScr.hpPotion < 10 && GameScr.gI().magicTree.currPeas > 0 && num - AutoMap.lastWaitTime > 500L)
				{
					AutoMap.lastWaitTime = num;
					Service.gI().openMenu(4);
					Service.gI().menu(4, 0, 0);
				}
			}
			if (AutoMap.isXmaping && !flag && !global::Char.isLoadingMap && num - AutoMap.lastWaitTime > 250L && GameCanvas.gameTick % 4 == 0)
			{
				bool flag2 = true;
				if (AutoMap.isFutureMap(AutoMap.IdMapEnd))
				{
					if (flag2 && TileMap.mapID == 27 && GameScr.findNPCInMap(38) == null)
					{
						flag2 = false;
						AutoMap.UpdateXmap(28);
					}
					if (flag2 && TileMap.mapID == 29 && GameScr.findNPCInMap(38) == null)
					{
						flag2 = false;
						AutoMap.UpdateXmap(28);
					}
					if (flag2 && TileMap.mapID == 28 && GameScr.findNPCInMap(38) == null)
					{
						flag2 = false;
						if (global::Char.myCharz().cx < TileMap.pxw / 2)
						{
							AutoMap.UpdateXmap(29);
						}
						else
						{
							AutoMap.UpdateXmap(27);
						}
					}
				}
				if (flag2)
				{
					AutoMap.UpdateXmap(AutoMap.IdMapEnd);
				}
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000A2410 File Offset: 0x000A0610
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 1:
				AutoMap.ShowPlanetMenu();
				return;
			case 2:
				AutoMap.isEatChicken = !AutoMap.isEatChicken;
				GameScr.info1.addInfo("Ăn Đùi Gà\n" + (AutoMap.isEatChicken ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				if (AutoMap.isSaveData)
				{
					Rms.saveRMSInt("AutoMapIsEatChicken", AutoMap.isEatChicken ? 1 : 0);
					return;
				}
				break;
			case 3:
				AutoMap.isHarvestPean = !AutoMap.isHarvestPean;
				GameScr.info1.addInfo("Thu Đậu\n" + (AutoMap.isHarvestPean ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				if (AutoMap.isSaveData)
				{
					Rms.saveRMSInt("AutoMapIsHarvestPean", AutoMap.isHarvestPean ? 1 : 0);
					return;
				}
				break;
			case 4:
				AutoMap.isUseCapsule = !AutoMap.isUseCapsule;
				GameScr.info1.addInfo("Sử Dụng Capsule\n" + (AutoMap.isUseCapsule ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				if (AutoMap.isSaveData)
				{
					Rms.saveRMSInt("AutoMapIsUseCsb", AutoMap.isUseCapsule ? 1 : 0);
					return;
				}
				break;
			case 5:
				AutoMap.isSaveData = !AutoMap.isSaveData;
				GameScr.info1.addInfo("Lưu Cài Đặt Auto Map\n" + (AutoMap.isSaveData ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				Rms.saveRMSInt("AutoMapIsSaveRms", AutoMap.isSaveData ? 1 : 0);
				if (AutoMap.isSaveData)
				{
					AutoMap.SaveData();
					return;
				}
				break;
			case 6:
				AutoMap.ShowMapsMenu((int[])p);
				return;
			case 7:
				AutoMap.isXmaping = true;
				AutoMap.IdMapEnd = (int)p;
				GameScr.info1.addInfo("Go to " + TileMap.mapNames[AutoMap.IdMapEnd], 0);
				return;
			case 8:
				AutoMap.ShowAdjacentMapsMenu();
				return;
			case 888:
				if (p is AutoMap.AdjacentMapInfo info)
				{
					info.GotoMap();
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000A25DC File Offset: 0x000A07DC
		public static void ShowMenu()
		{
			AutoMap.LoadData();
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Load Map", AutoMap.getInstance(), 1, null));
			myVector.addElement(new Command("Map Liền Kề", AutoMap.getInstance(), 8, null));
			myVector.addElement(new Command("Ăn Đùi Gà\n" + (AutoMap.isEatChicken ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoMap.getInstance(), 2, null));
			myVector.addElement(new Command("Thu Đậu\n" + (AutoMap.isHarvestPean ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoMap.getInstance(), 3, null));
			myVector.addElement(new Command("Sử Dụng Capsule\n" + (AutoMap.isUseCapsule ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoMap.getInstance(), 4, null));
			myVector.addElement(new Command("Lưu Cài Đặt\n" + (AutoMap.isSaveData ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoMap.getInstance(), 5, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x000A26D4 File Offset: 0x000A08D4
		private static void ShowPlanetMenu()
		{
			MyVector myVector = new MyVector();
			foreach (KeyValuePair<string, int[]> keyValuePair in AutoMap.planetDictionary)
			{
				myVector.addElement(new Command(keyValuePair.Key, AutoMap.getInstance(), 6, keyValuePair.Value));
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000A2750 File Offset: 0x000A0950
		private static void ShowMapsMenu(int[] mapIDs)
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < mapIDs.Length; i++)
			{
				if ((global::Char.myCharz().cgender != 0 || (mapIDs[i] != 22 && mapIDs[i] != 23)) && (global::Char.myCharz().cgender != 1 || (mapIDs[i] != 21 && mapIDs[i] != 23)) && (global::Char.myCharz().cgender != 2 || (mapIDs[i] != 21 && mapIDs[i] != 22)))
				{
					myVector.addElement(new Command(AutoMap.GetMapName(mapIDs[i]), AutoMap.getInstance(), 7, mapIDs[i]));
				}
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		public class AdjacentMapInfo
		{
			public string KeyName;
			public int MapID = -1;
			public string MapName = "";
			public int Position = -1;
			public Waypoint Waypoint;
			public AutoMap.NextMap NextMap;

			public string GetCoordString()
			{
				if (this.Waypoint != null)
				{
					int wpX = (int)(this.Waypoint.minX + this.Waypoint.maxX) / 2;
					int wpY = (int)(this.Waypoint.minY + this.Waypoint.maxY) / 2;
					return string.Format(" ({0}, {1})", wpX, wpY);
				}
				return "";
			}

			public void GotoMap()
			{
				if (this.NextMap != null)
				{
					this.NextMap.GotoMap();
				}
				else if (this.Position >= 0)
				{
					AutoMap.LoadMap(this.Position);
				}
				else if (this.Waypoint != null)
				{
					AutoMap.EnterWaypointDirect(this.Waypoint);
				}
			}
		}

		public static List<AutoMap.AdjacentMapInfo> GetAdjacentMaps()
		{
			List<AutoMap.AdjacentMapInfo> list = new List<AutoMap.AdjacentMapInfo>();
			if (TileMap.vGo != null && TileMap.vGo.size() > 0)
			{
				List<Waypoint> list2 = new List<Waypoint>();
				for (int i = 0; i < TileMap.vGo.size(); i++)
				{
					Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
					if (waypoint != null)
					{
						list2.Add(waypoint);
					}
				}
				if (list2.Count > 0)
				{
					list2.Sort((Waypoint w1, Waypoint w2) => ((int)(w1.minX + w1.maxX)).CompareTo((int)(w2.minX + w2.maxX)));
					list.Add(AutoMap.CreateAdjacentMapInfo("Trái (J)", 0, list2[0]));
					if (list2.Count >= 2)
					{
						list.Add(AutoMap.CreateAdjacentMapInfo("Phải (L)", 1, list2[list2.Count - 1]));
					}
					if (list2.Count >= 3)
					{
						list.Add(AutoMap.CreateAdjacentMapInfo("Giữa (K)", 2, list2[list2.Count / 2]));
					}
				}
			}
			if (AutoMap.linkMaps != null && AutoMap.linkMaps.ContainsKey(TileMap.mapID))
			{
				List<AutoMap.NextMap> list3 = AutoMap.linkMaps[TileMap.mapID];
				if (list3 != null)
				{
					foreach (AutoMap.NextMap nextMap in list3)
					{
						if (nextMap != null && nextMap.Npc != -1)
						{
							AutoMap.AdjacentMapInfo adjacentMapInfo = new AutoMap.AdjacentMapInfo();
							string text = "NPC";
							if (Npc.arrNpcTemplate != null && nextMap.Npc >= 0 && nextMap.Npc < Npc.arrNpcTemplate.Length && Npc.arrNpcTemplate[nextMap.Npc] != null)
							{
								text = "NPC " + Npc.arrNpcTemplate[nextMap.Npc].name;
							}
							adjacentMapInfo.KeyName = text;
							adjacentMapInfo.MapID = nextMap.MapID;
							adjacentMapInfo.MapName = ((TileMap.mapNames != null && nextMap.MapID >= 0 && nextMap.MapID < TileMap.mapNames.Length) ? TileMap.mapNames[nextMap.MapID] : ("Map " + nextMap.MapID.ToString()));
							adjacentMapInfo.Position = -1;
							adjacentMapInfo.NextMap = nextMap;
							list.Add(adjacentMapInfo);
						}
					}
				}
			}
			return list;
		}

		private static AutoMap.AdjacentMapInfo CreateAdjacentMapInfo(string keyName, int pos, Waypoint wp)
		{
			AutoMap.AdjacentMapInfo adjacentMapInfo = new AutoMap.AdjacentMapInfo();
			adjacentMapInfo.KeyName = keyName;
			adjacentMapInfo.Position = pos;
			adjacentMapInfo.Waypoint = wp;
			string text = "";
			if (wp != null && wp.popup != null && wp.popup.says != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < wp.popup.says.Length; i++)
				{
					stringBuilder.Append(wp.popup.says[i]).Append(" ");
				}
				text = stringBuilder.ToString().Trim();
			}
			adjacentMapInfo.MapName = text;
			adjacentMapInfo.MapID = AutoMap.GetMapIDByName(text);
			return adjacentMapInfo;
		}

		public static int GetMapIDByName(string mapName)
		{
			if (string.IsNullOrEmpty(mapName) || TileMap.mapNames == null)
			{
				return -1;
			}
			for (int i = 0; i < TileMap.mapNames.Length; i++)
			{
				if (TileMap.mapNames[i] != null && TileMap.mapNames[i].Equals(mapName, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		public static void ShowAdjacentMapsMenu()
		{
			List<AutoMap.AdjacentMapInfo> adjacentMaps = AutoMap.GetAdjacentMaps();
			if (adjacentMaps.Count == 0)
			{
				GameScr.info1.addInfo("Không có map liền kề nào!", 0);
				return;
			}
			MyVector myVector = new MyVector();
			for (int i = 0; i < adjacentMaps.Count; i++)
			{
				AutoMap.AdjacentMapInfo adjacentMapInfo = adjacentMaps[i];
				string text = (adjacentMapInfo.MapID >= 0) ? ("[" + adjacentMapInfo.MapID.ToString() + "] ") : "";
				string caption = adjacentMapInfo.KeyName + ": " + text + adjacentMapInfo.MapName + adjacentMapInfo.GetCoordString();
				myVector.addElement(new Command(caption, AutoMap.getInstance(), 888, adjacentMapInfo));
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		public static void EnterWaypointDirect(Waypoint waypoint)
		{
			if (waypoint == null)
			{
				return;
			}
			int num = (int)(waypoint.minX + waypoint.maxX) / 2;
			if (num < 15)
			{
				num = 15;
			}
			if (num > TileMap.pxw - 15)
			{
				num = TileMap.pxw - 15;
			}
			int num2 = AutoMap.GetBestWaypointY(waypoint, global::Char.myCharz().cy);
			if (waypoint.popup != null)
			{
				waypoint.popup.isPaint = true;
			}
			for (int k = 0; k < PopUp.vPopups.size(); k++)
			{
				PopUp popUp = (PopUp)PopUp.vPopups.elementAt(k);
				if (popUp != null)
				{
					popUp.isPaint = true;
				}
			}
			AutoMap.TeleportTo(num, num2);
			AutoMap.MarkActionSent();
			if (waypoint.isOffline || TileMap.isTrainingMap())
			{
				Service.gI().getMapOffline();
			}
			else
			{
				Service.gI().requestChangeMap();
			}
			global::Char.isLockKey = true;
			global::Char.ischangingMap = true;
			GameCanvas.timeLoading = 0;
			GameCanvas.TIMEOUT = mSystem.currentTimeMillis();
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			InfoDlg.showWait();
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0000914D File Offset: 0x0000734D
		public static void StartRunToMapId(int mapID)
		{
			AutoMap.isXmaping = true;
			AutoMap.IdMapEnd = mapID;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0000915B File Offset: 0x0000735B
		public static void FinishXmap()
		{
			AutoMap.isXmaping = false;
			AutoMap.isUsingCapsule = false;
			AutoMap.isOpeningPanel = false;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000A27F0 File Offset: 0x000A09F0
		public static void UpdateXmap(int mapID)
		{
			if (AutoMap.linkMaps.ContainsKey(84))
			{
				AutoMap.linkMaps.Remove(84);
			}
			AutoMap.linkMaps.Add(84, new List<AutoMap.NextMap>());
			AutoMap.linkMaps[84].Add(new AutoMap.NextMap(24 + global::Char.myCharz().cgender, 10, 0));
			int[] array = AutoMap.FindWay(mapID);
			if (array == null)
			{
				GameScr.info1.addInfo("Không thể tìm thấy đường đi", 0);
				return;
			}
			if (AutoMap.isUseCapsule)
			{
				if (!AutoMap.isUsingCapsule && array.Length > 3)
				{
					for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
					{
						Item item = global::Char.myCharz().arrItemBag[i];
						if (item != null && (item.template.id == 194 || (item.template.id == 193 && item.quantity > 10)))
						{
							AutoMap.isUsingCapsule = true;
							AutoMap.isOpeningPanel = false;
							AutoMap.lastTimeOpenedPanel = mSystem.currentTimeMillis();
							AutoMap.MarkActionSent();
							GameCanvas.panel.mapNames = null;
							Service.gI().useItem(0, 1, -1, item.template.id);
							return;
						}
					}
				}
				if (AutoMap.isUsingCapsule && !AutoMap.isOpeningPanel && (GameCanvas.panel.mapNames == null || mSystem.currentTimeMillis() - AutoMap.lastTimeOpenedPanel < 500L))
				{
					return;
				}
				if (AutoMap.isUsingCapsule && !AutoMap.isOpeningPanel)
				{
					for (int j = array.Length - 1; j >= 2; j--)
					{
						for (int k = 0; k < GameCanvas.panel.mapNames.Length; k++)
						{
							if (GameCanvas.panel.mapNames[k].Contains(TileMap.mapNames[array[j]]))
							{
								AutoMap.isOpeningPanel = true;
								AutoMap.MarkActionSent();
								Service.gI().requestMapSelect(k);
								return;
							}
						}
					}
					AutoMap.isOpeningPanel = true;
				}
			}
			if (TileMap.mapID == array[0] && !global::Char.ischangingMap && !Controller.isStopReadMessage)
			{
				AutoMap.MarkActionSent();
				AutoMap.Goto(array[1]);
			}
		}

		private static void MarkActionSent()
		{
			AutoMap.lastWaitTime = mSystem.currentTimeMillis();
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0000916F File Offset: 0x0000736F
		public static void LoadMapLeft()
		{
			AutoMap.LoadMap(0);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00009177 File Offset: 0x00007377
		public static void LoadMapCenter()
		{
			AutoMap.LoadMap(2);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0000917F File Offset: 0x0000737F
		public static void LoadMapRight()
		{
			AutoMap.LoadMap(1);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000A29D8 File Offset: 0x000A0BD8
		private static void LoadData()
		{
			AutoMap.isSaveData = (Rms.loadRMSInt("AutoMapIsSaveRms") == 1);
			if (AutoMap.isSaveData)
			{
				if (Rms.loadRMSInt("AutoMapIsEatChicken") == -1)
				{
					AutoMap.isEatChicken = true;
				}
				else
				{
					AutoMap.isEatChicken = (Rms.loadRMSInt("AutoMapIsEatChicken") == 1);
				}
				if (Rms.loadRMSInt("AutoMapIsUseCsb") == -1)
				{
					AutoMap.isUseCapsule = true;
				}
				else
				{
					AutoMap.isUseCapsule = (Rms.loadRMSInt("AutoMapIsUseCsb") == 1);
				}
				AutoMap.isHarvestPean = (Rms.loadRMSInt("AutoMapIsHarvestPean") == 1);
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000A2A60 File Offset: 0x000A0C60
		private static void SaveData()
		{
			Rms.saveRMSInt("AutoMapIsEatChicken", AutoMap.isEatChicken ? 1 : 0);
			Rms.saveRMSInt("AutoMapIsHarvestPean", AutoMap.isHarvestPean ? 1 : 0);
			Rms.saveRMSInt("AutoMapIsUseCsb", AutoMap.isUseCapsule ? 1 : 0);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x000A2AAC File Offset: 0x000A0CAC
		private static void LoadLinkMapsXmap()
		{
			AutoMap.AddLinkMapsXmap(new int[]
			{
				0,
				21
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				1,
				47
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				47,
				111
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				2,
				24
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				5,
				29
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				7,
				22
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				9,
				25
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				13,
				33
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				14,
				23
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				16,
				26
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				20,
				37
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				39,
				21
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				40,
				22
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				41,
				23
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				109,
				105
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				109,
				106
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				106,
				107
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				108,
				105
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				80,
				105
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				3,
				27,
				28,
				29,
				30
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				11,
				31,
				32,
				33,
				34
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				17,
				35,
				36,
				37,
				38
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				109,
				108,
				107,
				110,
				106
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				47,
				46,
				45,
				48
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				131,
				132,
				133
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				42,
				0,
				1,
				2,
				3,
				4,
				5,
				6
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				43,
				7,
				8,
				9,
				11,
				12,
				13,
				10
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				52,
				44,
				14,
				15,
				16,
				17,
				18,
				20,
				19
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				53,
				58,
				59,
				60,
				61,
				62,
				55,
				56,
				54,
				57
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				68,
				69,
				70,
				71,
				72,
				64,
				65,
				63,
				66,
				67,
				73,
				74,
				75,
				76,
				77,
				81,
				82,
				83,
				79,
				80
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				102,
				92,
				93,
				94,
				96,
				97,
				98,
				99,
				100,
				103
			});
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000A2D4C File Offset: 0x000A0F4C
		private static void LoadNPCLinkMapsXmap()
		{
			AutoMap.AddNPCLinkMapsXmap(19, 68, 12, 1);
			AutoMap.AddNPCLinkMapsXmap(19, 109, 12, 0);
			AutoMap.AddNPCLinkMapsXmap(24, 25, 10, 0);
			AutoMap.AddNPCLinkMapsXmap(24, 26, 10, 1);
			AutoMap.AddNPCLinkMapsXmap(24, 84, 10, 2);
			AutoMap.AddNPCLinkMapsXmap(25, 24, 11, 0);
			AutoMap.AddNPCLinkMapsXmap(25, 26, 11, 1);
			AutoMap.AddNPCLinkMapsXmap(25, 84, 11, 2);
			AutoMap.AddNPCLinkMapsXmap(26, 24, 12, 0);
			AutoMap.AddNPCLinkMapsXmap(26, 25, 12, 1);
			AutoMap.AddNPCLinkMapsXmap(26, 84, 12, 2);
			AutoMap.AddNPCLinkMapsXmap(27, 102, 38, 1);
			AutoMap.AddNPCLinkMapsXmap(27, 53, 25, 0);
			AutoMap.AddNPCLinkMapsXmap(28, 102, 38, 1);
			AutoMap.AddNPCLinkMapsXmap(29, 102, 38, 1);
			AutoMap.AddNPCLinkMapsXmap(45, 46, 19, 3);
			AutoMap.AddNPCLinkMapsXmap(52, 127, 44, 0);
			AutoMap.AddNPCLinkMapsXmap(52, 129, 23, 3);
			AutoMap.AddNPCLinkMapsXmap(52, 113, 23, 2);
			AutoMap.AddNPCLinkMapsXmap(68, 19, 12, 0);
			AutoMap.AddNPCLinkMapsXmap(80, 131, 60, 0);
			AutoMap.AddNPCLinkMapsXmap(102, 27, 38, 1);
			AutoMap.AddNPCLinkMapsXmap(113, 52, 22, 4);
			AutoMap.AddNPCLinkMapsXmap(127, 52, 44, 2);
			AutoMap.AddNPCLinkMapsXmap(129, 52, 23, 3);
			AutoMap.AddNPCLinkMapsXmap(131, 80, 60, 1);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x000A2EA0 File Offset: 0x000A10A0
		private static void AddPlanetXmap()
		{
			AutoMap.planetDictionary.Add("Trái đất", AutoMap.idMapsTraiDat);
			AutoMap.planetDictionary.Add("Namếc", AutoMap.idMapsNamek);
			AutoMap.planetDictionary.Add("Xayda", AutoMap.idMapsXayda);
			AutoMap.planetDictionary.Add("Fide", AutoMap.idMapsNappa);
			AutoMap.planetDictionary.Add("Tương lai", AutoMap.idMapsTuongLai);
			AutoMap.planetDictionary.Add("Cold", AutoMap.idMapsCold);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x000A2F28 File Offset: 0x000A1128
		private static void AddLinkMapsXmap(params int[] link)
		{
			for (int i = 0; i < link.Length; i++)
			{
				if (!AutoMap.linkMaps.ContainsKey(link[i]))
				{
					AutoMap.linkMaps.Add(link[i], new List<AutoMap.NextMap>());
				}
				if (i != 0)
				{
					AutoMap.linkMaps[link[i]].Add(new AutoMap.NextMap(link[i - 1], -1, -1));
				}
				if (i != link.Length - 1)
				{
					AutoMap.linkMaps[link[i]].Add(new AutoMap.NextMap(link[i + 1], -1, -1));
				}
			}
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00009187 File Offset: 0x00007387
		private static void AddNPCLinkMapsXmap(int currentMapID, int nextMapID, int npcID, int select)
		{
			if (!AutoMap.linkMaps.ContainsKey(currentMapID))
			{
				AutoMap.linkMaps.Add(currentMapID, new List<AutoMap.NextMap>());
			}
			AutoMap.linkMaps[currentMapID].Add(new AutoMap.NextMap(nextMapID, npcID, select));
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000A2FAC File Offset: 0x000A11AC
		private static void Goto(int mapID)
		{
			foreach (AutoMap.NextMap nextMap in AutoMap.linkMaps[TileMap.mapID])
			{
				if (nextMap.MapID == mapID)
				{
					nextMap.GotoMap();
					return;
				}
			}
			GameScr.info1.addInfo("Không thể thực hiện", 0);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000091BE File Offset: 0x000073BE
		private static int[] FindWay(int mapID)
		{
			return AutoMap.FindWay(mapID, new int[]
			{
				TileMap.mapID
			});
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000A3024 File Offset: 0x000A1224
		private static int[] FindWay(int mapIDEnd, int[] mapIDs)
		{
			List<int[]> list = new List<int[]>();
			List<int> list2 = new List<int>();
			list2.AddRange(mapIDs);
			foreach (AutoMap.NextMap nextMap in AutoMap.linkMaps[mapIDs[mapIDs.Length - 1]])
			{
				if (mapIDEnd == nextMap.MapID)
				{
					list2.Add(mapIDEnd);
					return list2.ToArray();
				}
				if (!list2.Contains(nextMap.MapID))
				{
					int[] array = AutoMap.FindWay(mapIDEnd, new List<int>(list2)
					{
						nextMap.MapID
					}.ToArray());
					if (array != null)
					{
						list.Add(array);
					}
				}
			}
			int num = 9999;
			int[] result = null;
			foreach (int[] array2 in list)
			{
				if (!AutoMap.hasWayGoFutureAndBack(array2) && (global::Char.myCharz().taskMaint.taskId > 30 || !AutoMap.hasWayGoToColdMap(array2)) && array2.Length < num)
				{
					num = array2.Length;
					result = array2;
				}
			}
			return result;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x000A3164 File Offset: 0x000A1364
		private static bool hasWayGoFutureAndBack(int[] ways)
		{
			for (int i = 1; i < ways.Length - 1; i++)
			{
				if (ways[i] == 102 && ways[i + 1] == 24 && (ways[i - 1] == 27 || ways[i - 1] == 28 || ways[i - 1] == 29))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000A31B0 File Offset: 0x000A13B0
		private static bool hasWayGoToColdMap(int[] ways)
		{
			for (int i = 0; i < ways.Length; i++)
			{
				if (ways[i] >= 105 && ways[i] <= 110)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000A31DC File Offset: 0x000A13DC
		private static string GetMapName(int mapID)
		{
			int num = mapID;
			string result;
			if (num != 113)
			{
				if (num != 129)
				{
					result = TileMap.mapNames[mapID] + "\n[" + mapID.ToString() + "]";
				}
				else
				{
					result = TileMap.mapNames[mapID] + " 23\n[" + mapID.ToString() + "]";
				}
			}
			else
			{
				result = string.Concat(new object[]
				{
					"Siêu hạng\n[",
					mapID,
					"]"
				});
			}
			return result;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000A3260 File Offset: 0x000A1460
		private static void LoadWaypointsInMap()
		{
			AutoMap.ResetSavedWaypoints();
			int num = TileMap.vGo.size();
			if (num != 2)
			{
				for (int i = 0; i < num; i++)
				{
					Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
					if (waypoint.maxX < 60)
					{
						AutoMap.wayPointMapLeft[0] = (int)(waypoint.minX + 15);
						AutoMap.wayPointMapLeft[1] = (int)waypoint.maxY;
					}
					else if ((int)waypoint.maxX > TileMap.pxw - 60)
					{
						AutoMap.wayPointMapRight[0] = (int)(waypoint.maxX - 15);
						AutoMap.wayPointMapRight[1] = (int)waypoint.maxY;
					}
					else
					{
						AutoMap.wayPointMapCenter[0] = (int)(waypoint.minX + 15);
						AutoMap.wayPointMapCenter[1] = (int)waypoint.maxY;
					}
				}
				return;
			}
			Waypoint waypoint2 = (Waypoint)TileMap.vGo.elementAt(0);
			Waypoint waypoint3 = (Waypoint)TileMap.vGo.elementAt(1);
			if ((waypoint2.maxX < 60 && waypoint3.maxX < 60) || ((int)waypoint2.minX > TileMap.pxw - 60 && (int)waypoint3.minX > TileMap.pxw - 60))
			{
				AutoMap.wayPointMapLeft[0] = (int)(waypoint2.minX + 15);
				AutoMap.wayPointMapLeft[1] = (int)waypoint2.maxY;
				AutoMap.wayPointMapRight[0] = (int)(waypoint3.maxX - 15);
				AutoMap.wayPointMapRight[1] = (int)waypoint3.maxY;
				return;
			}
			if (waypoint2.maxX < waypoint3.maxX)
			{
				AutoMap.wayPointMapLeft[0] = (int)(waypoint2.minX + 15);
				AutoMap.wayPointMapLeft[1] = (int)waypoint2.maxY;
				AutoMap.wayPointMapRight[0] = (int)(waypoint3.maxX - 15);
				AutoMap.wayPointMapRight[1] = (int)waypoint3.maxY;
				return;
			}
			AutoMap.wayPointMapLeft[0] = (int)(waypoint3.minX + 15);
			AutoMap.wayPointMapLeft[1] = (int)waypoint3.maxY;
			AutoMap.wayPointMapRight[0] = (int)(waypoint2.maxX - 15);
			AutoMap.wayPointMapRight[1] = (int)waypoint2.maxY;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000A343C File Offset: 0x000A163C
		private static int GetYGround(int x)
		{
			int limitY = (TileMap.pxh > 0) ? TileMap.pxh : 1000;
			for (int y = 24; y < limitY; y += 24)
			{
				if (TileMap.tileTypeAt(x, y, 2))
				{
					if (y % 24 != 0)
					{
						y -= y % 24;
					}
					return y;
				}
			}
			return -1;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000A3478 File Offset: 0x000A1678
		public static int GetBestWaypointY(Waypoint wp, int currentY)
		{
			if (wp == null)
			{
				return currentY;
			}
			return (int)(wp.minY + wp.maxY) / 2;
		}

		public static void TeleportTo(int targetX, int targetY)
		{
			global::Char me = global::Char.myCharz();
			if (me == null)
			{
				return;
			}

			int startX = me.cx;
			int startY = me.cy;
			int stepSize = 150;

			me.cvy = 0;
			me.cvx = 0;

			int diffX = targetX - startX;
			int diffY = targetY - startY;
			int distX = Res.abs(diffX);
			int distY = Res.abs(diffY);

			// Nếu khoảng cách rất ngắn (< 100px), dịch chuyển trực tiếp
			if (distX < 100 && distY < 100)
			{
				me.cx = targetX;
				me.cy = targetY;
				me.statusMe = (TileMap.tileTypeAt(targetX / 24, targetY / 24) == 0) ? 3 : 1;
				me.cxSend = -1;
				me.cySend = -1;
				Service.gI().charMove();
				return;
			}

			// Tính tầm bay cao an toàn (flyY) vượt mọi vật cản/ngọn đồi ở giữa map
			int minPointY = (startY < targetY) ? startY : targetY;
			int flyY = minPointY - 100;
			if (flyY < 60)
			{
				flyY = 60;
			}
			if (flyY > minPointY)
			{
				flyY = minPointY;
			}

			// Giai đoạn 1: Bay Y lên tầm cao an toàn flyY tại vị trí X khởi đầu
			if (startY > flyY)
			{
				int diffY1 = flyY - startY;
				int stepsY1 = (Res.abs(diffY1) + stepSize - 1) / stepSize;
				if (stepsY1 < 1) stepsY1 = 1;
				for (int i = 1; i <= stepsY1; i++)
				{
					int curY = startY + diffY1 * i / stepsY1;
					me.cx = startX;
					me.cy = curY;
					me.statusMe = 3;
					me.cxSend = -1;
					me.cySend = -1;
					Service.gI().charMove();
				}
			}

			// Giai đoạn 2: Băng ngang X qua không trung trên tầm cao flyY (vượt mọi đồi núi/vật cản)
			if (distX > 0)
			{
				int stepsX = (distX + stepSize - 1) / stepSize;
				if (stepsX < 1) stepsX = 1;
				for (int i = 1; i <= stepsX; i++)
				{
					int curX = startX + diffX * i / stepsX;
					me.cx = curX;
					me.cy = flyY;
					me.statusMe = 3;
					me.cxSend = -1;
					me.cySend = -1;
					Service.gI().charMove();
				}
			}

			// Giai đoạn 3: Hạ Y từ flyY xuống targetY tại vị trí targetX
			if (targetY != flyY)
			{
				int diffY2 = targetY - flyY;
				int stepsY2 = (Res.abs(diffY2) + stepSize - 1) / stepSize;
				if (stepsY2 < 1) stepsY2 = 1;
				for (int i = 1; i <= stepsY2; i++)
				{
					int curY = flyY + diffY2 * i / stepsY2;
					me.cx = targetX;
					me.cy = curY;
					me.statusMe = 3;
					me.cxSend = -1;
					me.cySend = -1;
					Service.gI().charMove();
				}
			}

			// Chốt tọa độ đích cuối cùng
			me.cx = targetX;
			me.cy = targetY;
			me.statusMe = (TileMap.tileTypeAt(targetX / 24, targetY / 24) == 0) ? 3 : 1;
			me.cxSend = -1;
			me.cySend = -1;
			Service.gI().charMove();
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x000091D4 File Offset: 0x000073D4
		private static void ResetSavedWaypoints()
		{
			AutoMap.wayPointMapLeft = new int[2];
			AutoMap.wayPointMapCenter = new int[2];
			AutoMap.wayPointMapRight = new int[2];
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000091F7 File Offset: 0x000073F7
		private static bool isNRDMap(int mapID)
		{
			return mapID >= 85 && mapID <= 91;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x000A3510 File Offset: 0x000A1710
		private static bool isFutureMap(int mapID)
		{
			for (int i = 0; i < AutoMap.idMapsTuongLai.Length; i++)
			{
				if (AutoMap.idMapsTuongLai[i] == mapID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00009208 File Offset: 0x00007408
		private static bool isNRD(ItemMap mapID)
		{
			return mapID.template.id >= 372 && mapID.template.id <= 378;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000A353C File Offset: 0x000A173C
		public static void LoadMap(int position)
		{
			if (AutoMap.isNRDMap(TileMap.mapID))
			{
				AutoMap.TeleportInNRDMap(position);
				return;
			}

			global::Char me = global::Char.myCharz();
			if (me.meDead || me.statusMe == 14 || me.statusMe == 5 || me.cHP <= 0L)
			{
				GameScr.info1.addInfo("Bạn đang kiệt sức!", 0);
				return;
			}

			// Đảm bảo không bị chặn charMove và các kiểm tra map
			global::Char.ischangingMap = false;
			global::Char.isLoadingMap = false;
			global::Char.isLockKey = false;
			AutoMap.isAutoChangeMap = true;

			int wpCount = TileMap.vGo.size();
			if (wpCount == 0)
			{
				GameScr.info1.addInfo("Map này không có điểm chuyển map!", 0);
				return;
			}

			List<Waypoint> listWp = new List<Waypoint>();
			for (int i = 0; i < wpCount; i++)
			{
				Waypoint wp = (Waypoint)TileMap.vGo.elementAt(i);
				if (wp != null)
				{
					listWp.Add(wp);
				}
			}

			if (listWp.Count == 0)
			{
				GameScr.info1.addInfo("Map này không có điểm chuyển map!", 0);
				return;
			}

			// Sắp xếp các Waypoint theo tọa độ X trung tâm từ trái qua phải
			listWp.Sort((w1, w2) => ((int)(w1.minX + w1.maxX)).CompareTo((int)(w2.minX + w2.maxX)));

			Waypoint targetWp = null;
			if (position == 0) // Trái (J)
			{
				targetWp = listWp[0];
			}
			else if (position == 1) // Phải (L)
			{
				targetWp = listWp[listWp.Count - 1];
			}
			else if (position == 2) // Giữa (K)
			{
				if (listWp.Count <= 2)
				{
					int midMapX = TileMap.pxw / 2;
					Waypoint closest = listWp[0];
					int minDiff = Res.abs((int)(closest.minX + closest.maxX) / 2 - midMapX);
					for (int j = 1; j < listWp.Count; j++)
					{
						int diff = Res.abs((int)(listWp[j].minX + listWp[j].maxX) / 2 - midMapX);
						if (diff < minDiff)
						{
							minDiff = diff;
							closest = listWp[j];
						}
					}
					targetWp = closest;
				}
				else
				{
					targetWp = listWp[listWp.Count / 2];
				}
			}

			if (targetWp == null)
			{
				targetWp = listWp[0];
			}

			// Tính toán tọa độ X an toàn: chính giữa Waypoint
			int targetX = (int)(targetWp.minX + targetWp.maxX) / 2;

			// Giữ targetX trong giới hạn an toàn của bản đồ (tránh va chạm mép map < 15px hoặc > pxw - 15px)
			if (targetX < 15)
			{
				targetX = 15;
			}
			if (targetX > TileMap.pxw - 15)
			{
				targetX = TileMap.pxw - 15;
			}

			// Đảm bảo targetX luôn nằm trọn trong Waypoint
			if (targetX < (int)targetWp.minX)
			{
				targetX = (int)targetWp.minX + 2;
			}
			if (targetX > (int)targetWp.maxX)
			{
				targetX = (int)targetWp.maxX - 2;
			}

			// Tọa độ Y chuẩn xác: Giữ nguyên Y nếu đã nằm trong Waypoint, hoặc lấy tâm Y để tránh vượt khung khi gồ ghề
			int targetY = AutoMap.GetBestWaypointY(targetWp, me.cy);

			// Kích hoạt PopUp của Waypoint để isInEnterOnlinePoint() / isInEnterOfflinePoint() trả về hợp lệ
			if (targetWp.popup != null)
			{
				targetWp.popup.isPaint = true;
			}
			for (int k = 0; k < PopUp.vPopups.size(); k++)
			{
				PopUp popUp = (PopUp)PopUp.vPopups.elementAt(k);
				if (popUp != null)
				{
					popUp.isPaint = true;
				}
			}

			string posName = (position == 0) ? "Trái (J)" : ((position == 1) ? "Phải (L)" : "Giữa (K)");
			string cmdName = (targetWp.isOffline || TileMap.isTrainingMap()) ? "getMapOffline (cmd -33)" : "requestChangeMap (cmd -23)";
			Debug.Log(string.Format("[AutoMap] Bấm {0} | MapID={1} | Waypoint: X[{2}..{3}] Y[{4}..{5}] | From: ({6}, {7}) -> Target: ({8}, {9}) | Gói tin: {10}",
				posName, TileMap.mapID, targetWp.minX, targetWp.maxX, targetWp.minY, targetWp.maxY, me.cx, me.cy, targetX, targetY, cmdName));
			GameScr.info1.addInfo(string.Format("Qua map [{0}] -> ({1}, {2})", posName, targetX, targetY), 0);

			// Dọn dẹp hành động và tiêu điểm để không bị ngắt
			GameScr.gI().auto = 0;
			me.currentMovePoint = null;
			me.charFocus = null;
			me.mobFocus = null;
			me.npcFocus = null;
			me.itemFocus = null;

			// Dịch chuyển từng bước an toàn và gửi vị trí mới lên Server
			AutoMap.TeleportTo(targetX, targetY);

			// Gửi gói tin chuyển map tương ứng (Offline hoặc Online)
			AutoMap.MarkActionSent();
			if (targetWp.isOffline || TileMap.isTrainingMap())
			{
				Service.gI().getMapOffline();
			}
			else
			{
				Service.gI().requestChangeMap();
			}

			global::Char.isLockKey = true;
			global::Char.ischangingMap = true;
			GameCanvas.timeLoading = 0;
			GameCanvas.TIMEOUT = mSystem.currentTimeMillis();
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			InfoDlg.showWait();
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000A3684 File Offset: 0x000A1884
		private static void TeleportInNRDMap(int position)
		{
			if (position == 0)
			{
				AutoMap.TeleportTo(60, AutoMap.GetYGround(60));
				return;
			}
			if (position != 2)
			{
				AutoMap.TeleportTo(TileMap.pxw - 60, AutoMap.GetYGround(TileMap.pxw - 60));
				return;
			}
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.template.npcTemplateId >= 30 && npc.template.npcTemplateId <= 36)
				{
					global::Char.myCharz().npcFocus = npc;
					AutoMap.TeleportTo(npc.cx, npc.cy - 3);
					return;
				}
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000A3728 File Offset: 0x000A1928
		static AutoMap()
		{
			AutoMap.LoadLinkMapsXmap();
			AutoMap.LoadNPCLinkMapsXmap();
			AutoMap.AddPlanetXmap();
			AutoMap.LoadData();
		}

		// Token: 0x04001594 RID: 5524
		public static AutoMap _Instance;

		// Token: 0x04001595 RID: 5525
		private static Dictionary<int, List<AutoMap.NextMap>> linkMaps = new Dictionary<int, List<AutoMap.NextMap>>();

		// Token: 0x04001596 RID: 5526
		private static Dictionary<string, int[]> planetDictionary = new Dictionary<string, int[]>();

		// Token: 0x04001597 RID: 5527
		public static bool isXmaping;

		// Token: 0x04001598 RID: 5528
		public static int IdMapEnd;

		// Token: 0x04001599 RID: 5529
		private static int[] wayPointMapLeft;

		// Token: 0x0400159A RID: 5530
		private static int[] wayPointMapCenter;

		// Token: 0x0400159B RID: 5531
		private static int[] wayPointMapRight;

		// Token: 0x0400159C RID: 5532
		private static bool isEatChicken = true;

		// Token: 0x0400159D RID: 5533
		private static bool isHarvestPean;

		// Token: 0x0400159E RID: 5534
		private static bool isUseCapsule = true;

		// Token: 0x0400159F RID: 5535
		private static bool isUsingCapsule;

		// Token: 0x040015A0 RID: 5536
		private static bool isOpeningPanel;

		// Token: 0x040015A1 RID: 5537
		private static long lastTimeOpenedPanel;

		// Token: 0x040015A2 RID: 5538
		private static bool isSaveData;

		// Token: 0x040015A3 RID: 5539
		private static long lastWaitTime;

		// Token: 0x040015A4 RID: 5540
		private static int[] idMapsNamek = new int[]
		{
			43,
			22,
			7,
			8,
			9,
			11,
			12,
			13,
			10,
			31,
			32,
			33,
			34,
			43,
			25
		};

		// Token: 0x040015A5 RID: 5541
		private static int[] idMapsXayda = new int[]
		{
			44,
			23,
			14,
			15,
			16,
			17,
			18,
			20,
			19,
			35,
			36,
			37,
			38,
			52,
			44,
			26,
			84,
			113,
			127,
			129
		};

		// Token: 0x040015A6 RID: 5542
		private static int[] idMapsTraiDat = new int[]
		{
			42,
			21,
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			27,
			28,
			29,
			30,
			47,
			42,
			24,
			46,
			45,
			48,
			53,
			58,
			59,
			60,
			61,
			62,
			55,
			56,
			54,
			57
		};

		// Token: 0x040015A7 RID: 5543
		private static int[] idMapsTuongLai = new int[]
		{
			102,
			92,
			93,
			94,
			96,
			97,
			98,
			99,
			100,
			103
		};

		// Token: 0x040015A8 RID: 5544
		private static int[] idMapsCold = new int[]
		{
			109,
			108,
			107,
			110,
			106,
			105
		};

		// Token: 0x040015A9 RID: 5545
		private static int[] idMapsNappa = new int[]
		{
			68,
			69,
			70,
			71,
			72,
			64,
			65,
			63,
			66,
			67,
			73,
			74,
			75,
			76,
			77,
			81,
			82,
			83,
			79,
			80,
			131,
			132,
			133
		};

		// Token: 0x040015AA RID: 5546
		public static bool isAutoChangeMap;

		// Token: 0x020000F0 RID: 240
		public class NextMap
		{
			// Token: 0x06000B0E RID: 2830 RVA: 0x00009233 File Offset: 0x00007433
			public NextMap(int mapID, int npcID, int index)
			{
				this.MapID = mapID;
				this.Npc = npcID;
				this.Index = index;
			}

			// Token: 0x06000B0F RID: 2831 RVA: 0x000A37F4 File Offset: 0x000A19F4
			public void GotoMap()
			{
				if (this.Index == -1 && this.Npc == -1)
				{
					Waypoint wayPoint = this.GetWayPoint();
					if (wayPoint != null)
					{
						this.Enter(wayPoint);
						return;
					}
				}
				else if (this.Npc != -1 && this.Index != -1)
				{
					AutoMap.MarkActionSent();
					Service.gI().openMenu(this.Npc);
					Service.gI().confirmMenu(0, (sbyte)this.Index);
				}
			}

			// Token: 0x06000B10 RID: 2832 RVA: 0x000A385C File Offset: 0x000A1A5C
			public Waypoint GetWayPoint()
			{
				for (int i = 0; i < TileMap.vGo.size(); i++)
				{
					Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
					if (this.GetMapName().Equals(this.GetMapName(waypoint.popup)))
					{
						return waypoint;
					}
				}
				return null;
			}

			// Token: 0x06000B11 RID: 2833 RVA: 0x00009250 File Offset: 0x00007450
			public string GetMapName()
			{
				return TileMap.mapNames[this.MapID];
			}

			// Token: 0x06000B12 RID: 2834 RVA: 0x000A38AC File Offset: 0x000A1AAC
			public void Enter(Waypoint waypoint)
			{
				int num = (waypoint.maxX < 60) ? 15 : (((int)waypoint.minX <= TileMap.pxw - 60) ? ((int)((waypoint.minX + waypoint.maxX) / 2)) : (TileMap.pxw - 15));
				int maxY = AutoMap.GetBestWaypointY(waypoint, global::Char.myCharz().cy);
				if (waypoint.popup != null)
				{
					waypoint.popup.isPaint = true;
				}
				for (int k = 0; k < PopUp.vPopups.size(); k++)
				{
					PopUp popUp = (PopUp)PopUp.vPopups.elementAt(k);
					if (popUp != null)
					{
						popUp.isPaint = true;
					}
				}
				if (num == -1 || maxY == -1)
				{
					GameScr.info1.addInfo("Có lỗi xảy ra", 0);
					return;
				}
				this.TeleportTo(num, maxY);
				if (waypoint.isOffline)
				{
					AutoMap.MarkActionSent();
					Service.gI().getMapOffline();
					return;
				}
				AutoMap.MarkActionSent();
				Service.gI().requestChangeMap();
			}

			// Token: 0x06000B13 RID: 2835 RVA: 0x000A3938 File Offset: 0x000A1B38
			public string GetMapName(PopUp popup)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < popup.says.Length; i++)
				{
					stringBuilder.Append(popup.says[i]);
					stringBuilder.Append(" ");
				}
				return stringBuilder.ToString().Trim();
			}

			// Token: 0x06000B14 RID: 2836 RVA: 0x000A3984 File Offset: 0x000A1B84
			public void TeleportTo(int x, int y)
			{
				AutoMap.TeleportTo(x, y);
			}

			// Token: 0x040015AB RID: 5547
			public int MapID;

			// Token: 0x040015AC RID: 5548
			public int Npc;

			// Token: 0x040015AD RID: 5549
			public int Index;
		}
	}
}
