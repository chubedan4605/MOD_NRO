using System;
using System.Collections.Generic;

namespace Mod.DungPham.KoiOctiiu957
{
	// Token: 0x020000FF RID: 255
	public class AutoPick : IActionListener, IChatable
	{
		// Token: 0x06000B41 RID: 2881 RVA: 0x00009370 File Offset: 0x00007570
		public static AutoPick getInstance()
		{
			if (AutoPick._Instance == null)
			{
				AutoPick._Instance = new AutoPick();
			}
			return AutoPick._Instance;
		}

		private static int lastLoadedCharId = -1;

		private static string GetAccountPrefix()
		{
			global::Char c = global::Char.myCharz();
			if (c != null && c.charID > 0)
			{
				return "koi_pick_" + c.charID.ToString() + "_";
			}
			return "koi_pick_";
		}

		private static void SaveSettings()
		{
			string pickStr = "";
			for (int i = 0; i < AutoPick.listItemAutoPick.Count; i++)
			{
				pickStr += AutoPick.listItemAutoPick[i].ToString();
				if (i < AutoPick.listItemAutoPick.Count - 1)
				{
					pickStr += " ";
				}
			}
			Rms.saveRMSString(AutoPick.GetAccountPrefix() + "auto_pick_list", pickStr);

			string ignoreStr = "";
			for (int j = 0; j < AutoPick.listItemIgnorePick.Count; j++)
			{
				ignoreStr += AutoPick.listItemIgnorePick[j].ToString();
				if (j < AutoPick.listItemIgnorePick.Count - 1)
				{
					ignoreStr += " ";
				}
			}
			Rms.saveRMSString(AutoPick.GetAccountPrefix() + "ignore_pick_list", ignoreStr);
		}

		private static void LoadSettings()
		{
			AutoPick.listItemAutoPick.Clear();
			string pickStr = Rms.loadRMSString(AutoPick.GetAccountPrefix() + "auto_pick_list");
			if (!string.IsNullOrEmpty(pickStr))
			{
				string[] array = pickStr.Split(' ');
				foreach (string s in array)
				{
					int item; if (int.TryParse(s, out item))
					{
						AutoPick.listItemAutoPick.Add(item);
					}
				}
			}

			AutoPick.listItemIgnorePick.Clear();
			string ignoreStr = Rms.loadRMSString(AutoPick.GetAccountPrefix() + "ignore_pick_list");
			if (!string.IsNullOrEmpty(ignoreStr))
			{
				string[] array2 = ignoreStr.Split(' ');
				foreach (string s in array2)
				{
					int item; if (int.TryParse(s, out item))
					{
						AutoPick.listItemIgnorePick.Add(item);
					}
				}
			}
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x000A5CC8 File Offset: 0x000A3EC8
		public static void Update()
		{
			global::Char @char = global::Char.myCharz();
			if (@char == null)
			{
				return;
			}
			if (@char.charID > 0 && @char.charID != AutoPick.lastLoadedCharId)
			{
				AutoPick.lastLoadedCharId = @char.charID;
				AutoPick.LoadSettings();
			}
			if ((!GameScr.isAutoPlay || (!GameScr.canAutoPlay && !AutoTrain.isAutoTrain)) && AutoPick.isAutoPick)
			{
				if (AutoPick.isNRDMap(TileMap.mapID))
				{
					try
					{
						for (int i = 0; i < GameScr.vItemMap.size(); i++)
						{
							ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
							int num = global::Math.abs(global::Char.myCharz().cx - itemMap.x);
							if ((itemMap.playerId == global::Char.myCharz().charID || itemMap.playerId == -1) && num <= 60 && itemMap != null && mSystem.currentTimeMillis() - AutoPick.lastTimePickedItem > 550L && AutoPick.isNRD(itemMap))
							{
								if (AutoPick.listItemIgnorePick.Contains((int)itemMap.template.id))
								{
									continue;
								}
								Service.gI().pickItem(itemMap.itemMapID);
								AutoPick.lastTimePickedItem = mSystem.currentTimeMillis();
								break;
							}
						}
					}
					catch (Exception)
					{
					}
				}
				if (!AutoPick.isNRDMap(TileMap.mapID))
				{
					AutoPick.FocusToNearestItem();
					if (global::Char.myCharz().itemFocus != null)
					{
						AutoPick.PickIt();
					}
				}
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000A5DE0 File Offset: 0x000A3FE0
		public void onChatFromMe(string text, string to)
		{
			if (ChatTextField.gI().tfChat.getText() != null && !ChatTextField.gI().tfChat.getText().Equals(string.Empty) && !text.Equals(string.Empty) && text != null)
			{
				if (ChatTextField.gI().strChat.Equals(AutoPick.inputMaximumPickDistance[0]))
				{
					try
					{
						int num = AutoPick.maximumPickDistance = int.Parse(ChatTextField.gI().tfChat.getText());
						GameScr.info1.addInfo("Khoảng Cách Nhặt: " + num.ToString(), 0);
					}
					catch
					{
						GameScr.info1.addInfo("Số Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					AutoPick.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(AutoPick.inputItemID[0]))
				{
					try
					{
						int item = int.Parse(ChatTextField.gI().tfChat.getText());
						AutoPick.listItemAutoPick.Add(item);
						AutoPick.SaveSettings();
						GameScr.info1.addInfo("Đã Thêm Item " + item.ToString(), 0);
					}
					catch
					{
						GameScr.info1.addInfo("Số Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					AutoPick.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(AutoPick.inputItemIDIgnore[0]))
				{
					try
					{
						int item = int.Parse(ChatTextField.gI().tfChat.getText());
						AutoPick.listItemIgnorePick.Add(item);
						AutoPick.SaveSettings();
						GameScr.info1.addInfo("Đã Thêm Vào DS Ko Nhặt " + item.ToString(), 0);
					}
					catch
					{
						GameScr.info1.addInfo("Số Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					AutoPick.ResetChatTextField();
					return;
				}
			}
			else
			{
				ChatTextField.gI().isShow = false;
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00009388 File Offset: 0x00007588
		public void onCancelChat()
		{
			if (GameScr.isPaintMessage)
			{
				GameScr.isPaintMessage = false;
				ChatTextField.gI().center = null;
			}
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x000A5F38 File Offset: 0x000A4138
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 1:
				AutoPick.isAutoPick = !AutoPick.isAutoPick;
				AutoPick.pickByList = 0;
				GameScr.info1.addInfo("Auto Nhặt\n" + (AutoPick.isAutoPick ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 2:
				AutoPick.isPickAll = !AutoPick.isPickAll;
				GameScr.info1.addInfo("Nhặt Tất Cả\n" + (AutoPick.isPickAll ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 3:
				AutoPick.isAutoPick = !AutoPick.isAutoPick;
				AutoPick.pickByList = 1;
				GameScr.info1.addInfo("Nhặt Theo Danh Sách\n" + (AutoPick.isAutoPick ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 4:
				AutoPick.isTeleportToItem = !AutoPick.isTeleportToItem;
				GameScr.info1.addInfo("Dịch Đến Item\n" + (AutoPick.isTeleportToItem ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 5:
				ChatTextField.gI().strChat = AutoPick.inputMaximumPickDistance[0];
				ChatTextField.gI().tfChat.name = AutoPick.inputMaximumPickDistance[1];
				ChatTextField.gI().startChat2(AutoPick.getInstance(), string.Empty);
				return;
			case 6:
				if (AutoPick.listItemAutoPick.Count == 0)
				{
					GameScr.info1.addInfo("Danh Sách Trống!", 0);
				}
				if (AutoPick.listItemAutoPick.Count > 0)
				{
					string text = "";
					for (int i = 0; i < AutoPick.listItemAutoPick.Count; i++)
					{
						text = text + AutoPick.listItemAutoPick[i].ToString() + " ";
					}
					GameScr.info1.addInfo(text, 0);
					return;
				}
				return;
			case 7:
				AutoPick.listItemAutoPick.Clear();
				AutoPick.SaveSettings();
				GameScr.info1.addInfo("Đã Clear Danh Sách Nhặt!", 0);
				return;
			case 8:
				ChatTextField.gI().strChat = AutoPick.inputItemID[0];
				ChatTextField.gI().tfChat.name = AutoPick.inputItemID[1];
				ChatTextField.gI().startChat2(AutoPick.getInstance(), string.Empty);
				return;
			case 9:
				AutoPick.listItemAutoPick.Add((int)global::Char.myCharz().itemFocus.template.id);
				AutoPick.SaveSettings();
				GameScr.info1.addInfo(string.Concat(new string[]
				{
					"Đã thêm ",
					global::Char.myCharz().itemFocus.template.name,
					" [",
					global::Char.myCharz().itemFocus.template.id.ToString(),
					"]"
				}), 0);
				return;
			case 10:
				if (AutoPick.listItemIgnorePick.Count == 0)
				{
					GameScr.info1.addInfo("Danh Sách Trống!", 0);
				}
				if (AutoPick.listItemIgnorePick.Count > 0)
				{
					string text = "";
					for (int i = 0; i < AutoPick.listItemIgnorePick.Count; i++)
					{
						text = text + AutoPick.listItemIgnorePick[i].ToString() + " ";
					}
					GameScr.info1.addInfo(text, 0);
					return;
				}
				return;
			case 11:
				AutoPick.listItemIgnorePick.Clear();
				AutoPick.SaveSettings();
				GameScr.info1.addInfo("Đã Clear Danh Sách Ko Nhặt!", 0);
				return;
			case 12:
				ChatTextField.gI().strChat = AutoPick.inputItemIDIgnore[0];
				ChatTextField.gI().tfChat.name = AutoPick.inputItemIDIgnore[1];
				ChatTextField.gI().startChat2(AutoPick.getInstance(), string.Empty);
				return;
			case 13:
				AutoPick.listItemIgnorePick.Add((int)global::Char.myCharz().itemFocus.template.id);
				AutoPick.SaveSettings();
				GameScr.info1.addInfo(string.Concat(new string[]
				{
					"Đã thêm ko nhặt ",
					global::Char.myCharz().itemFocus.template.name,
					" [",
					global::Char.myCharz().itemFocus.template.id.ToString(),
					"]"
				}), 0);
				return;
			case 14:
				AutoPick.ShowMenuPickConfig();
				return;
			case 15:
				AutoPick.ShowMenuIgnoreConfig();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x000A61DC File Offset: 0x000A43DC
		public static void ShowMenu()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Auto Nhặt\n" + ((!AutoPick.isAutoPick || AutoPick.pickByList != 0) ? "[STATUS: OFF]" : "[STATUS: ON]"), AutoPick.getInstance(), 1, null));
			myVector.addElement(new Command("Nhặt Tất Cả\n" + (AutoPick.isPickAll ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoPick.getInstance(), 2, null));
			myVector.addElement(new Command("Nhặt Theo Danh Sách\n" + ((!AutoPick.isAutoPick || AutoPick.pickByList != 1) ? "[STATUS: OFF]" : "[STATUS: ON]"), AutoPick.getInstance(), 3, null));
			myVector.addElement(new Command("Dịch Đến Item\n" + (AutoPick.isTeleportToItem ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoPick.getInstance(), 4, null));
			myVector.addElement(new Command("Khoảng Cách Nhặt\n[" + AutoPick.maximumPickDistance.ToString() + "]", AutoPick.getInstance(), 5, null));
			myVector.addElement(new Command("Cấu Hình Nhặt", AutoPick.getInstance(), 14, null));
			myVector.addElement(new Command("Cấu Hình Ko Nhặt", AutoPick.getInstance(), 15, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		public static void ShowMenuPickConfig()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Xem Danh Sách", AutoPick.getInstance(), 6, null));
			myVector.addElement(new Command("Clear Danh Sách", AutoPick.getInstance(), 7, null));
			myVector.addElement(new Command("Thêm ItemID", AutoPick.getInstance(), 8, null));
			if (global::Char.myCharz().itemFocus != null)
			{
				myVector.addElement(new Command(string.Concat(new string[]
				{
					"Thêm: ",
					global::Char.myCharz().itemFocus.template.name,
					" [",
					global::Char.myCharz().itemFocus.template.id.ToString(),
					"] "
				}), AutoPick.getInstance(), 9, null));
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		public static void ShowMenuIgnoreConfig()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Xem Danh Sách", AutoPick.getInstance(), 10, null));
			myVector.addElement(new Command("Clear Danh Sách", AutoPick.getInstance(), 11, null));
			myVector.addElement(new Command("Thêm ItemID", AutoPick.getInstance(), 12, null));
			if (global::Char.myCharz().itemFocus != null)
			{
				myVector.addElement(new Command(string.Concat(new string[]
				{
					"Thêm: ",
					global::Char.myCharz().itemFocus.template.name,
					" [",
					global::Char.myCharz().itemFocus.template.id.ToString(),
					"] "
				}), AutoPick.getInstance(), 13, null));
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00009276 File Offset: 0x00007476
		private static void ResetChatTextField()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x000045ED File Offset: 0x000027ED
		private static void smethod_0()
		{
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x000045ED File Offset: 0x000027ED
		private static void smethod_1()
		{
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x000A63AC File Offset: 0x000A45AC
		public static void FocusToNearestItem()
		{
			if (global::Char.myCharz().itemFocus == null)
			{
				for (int i = 0; i < GameScr.vItemMap.size(); i++)
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
					int num = global::Math.abs(global::Char.myCharz().cx - itemMap.x);
					int num2 = global::Math.abs(global::Char.myCharz().cy - itemMap.y);
					if (num <= AutoPick.maximumPickDistance && num2 <= AutoPick.maximumPickDistance && AutoPick.isPickIt(itemMap) && itemMap.template.id != 673)
					{
						global::Char.myCharz().itemFocus = itemMap;
						return;
					}
				}
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000A6454 File Offset: 0x000A4654
		public static void PickIt()
		{
			if (mSystem.currentTimeMillis() - AutoPick.lastTimePickedItem >= 550L && global::Char.myCharz().itemFocus != null)
			{
				if (AutoPick.listItemIgnorePick.Contains((int)global::Char.myCharz().itemFocus.template.id))
				{
					global::Char.myCharz().itemFocus = null;
					return;
				}
				if (AutoPick.isTeleportToItem && !global::Char.isLockKey)
				{
					AutoPick.TeleportTo(global::Char.myCharz().itemFocus.x, global::Char.myCharz().itemFocus.y);
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					if (global::Char.myCharz().itemFocus.template.id != 673)
					{
						Service.gI().pickItem(global::Char.myCharz().itemFocus.itemMapID);
						AutoPick.lastTimePickedItem = mSystem.currentTimeMillis();
						return;
					}
				}
				else
				{
					if (global::Char.myCharz().cx < global::Char.myCharz().itemFocus.x)
					{
						global::Char.myCharz().cdir = 1;
					}
					else
					{
						global::Char.myCharz().cdir = -1;
					}
					int num = global::Math.abs(global::Char.myCharz().cx - global::Char.myCharz().itemFocus.x);
					int num2 = global::Math.abs(global::Char.myCharz().cy - global::Char.myCharz().itemFocus.y);
					if (num <= 40 && num2 < 40)
					{
						GameCanvas.clearKeyHold();
						GameCanvas.clearKeyPressed();
						if (global::Char.myCharz().itemFocus.template.id != 673)
						{
							Service.gI().pickItem(global::Char.myCharz().itemFocus.itemMapID);
							AutoPick.lastTimePickedItem = mSystem.currentTimeMillis();
							return;
						}
					}
					else
					{
						global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().itemFocus.x, global::Char.myCharz().itemFocus.y);
						global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
						GameCanvas.clearKeyHold();
						GameCanvas.clearKeyPressed();
					}
				}
			}
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000A53A0 File Offset: 0x000A35A0
		private static void TeleportTo(int x, int y)
		{
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y + 1;
			Service.gI().charMove();
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000A6618 File Offset: 0x000A4818
		private static bool isPickIt(ItemMap item)
		{
			if (AutoPick.listItemIgnorePick.Contains((int)item.template.id))
			{
				return false;
			}
			bool result;
			if (AutoPick.isPickAll)
			{
				result = true;
			}
			else if (AutoPick.pickByList == 0)
			{
				result = (item.playerId == global::Char.myCharz().charID || item.playerId == -1);
			}
			else
			{
				result = (AutoPick.pickByList == 1 && AutoPick.listItemAutoPick.Contains((int)item.template.id) && (item.playerId == global::Char.myCharz().charID || item.playerId == -1));
			}
			return result;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x000091F7 File Offset: 0x000073F7
		private static bool isNRDMap(int mapID)
		{
			return mapID >= 85 && mapID <= 91;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00009208 File Offset: 0x00007408
		private static bool isNRD(ItemMap item)
		{
			return item.template.id >= 372 && item.template.id <= 378;
		}

		// Token: 0x040015E0 RID: 5600
		private static AutoPick _Instance;

		// Token: 0x040015E1 RID: 5601
		public static bool isAutoPick;

		// Token: 0x040015E2 RID: 5602
		public static long lastTimePickedItem;

		// Token: 0x040015E3 RID: 5603
		private static int maximumPickDistance = 50;

		// Token: 0x040015E4 RID: 5604
		private static bool isTeleportToItem;

		// Token: 0x040015E5 RID: 5605
		private static bool isPickAll;

		// Token: 0x040015E6 RID: 5606
		public static int pickByList;

		// Token: 0x040015E7 RID: 5607
		private static List<int> listItemAutoPick = new List<int>();

		private static List<int> listItemIgnorePick = new List<int>();

		// Token: 0x040015E8 RID: 5608
		private static string[] inputMaximumPickDistance = new string[]
		{
			"Nhập khoảng cách nhặt",
			"khoảng cách (>50)"
		};

		// Token: 0x040015E9 RID: 5609
		private static string[] inputItemID = new string[]
		{
			"Nhập ID của item",
			"ID"
		};

		private static string[] inputItemIDIgnore = new string[]
		{
			"Nhập ID vật phẩm không nhặt",
			"ID"
		};
	}
}

