using System;

namespace Mod.DungPham.KoiOctiiu957
{
	// Token: 0x02000111 RID: 273
	public class AutoPean : IActionListener, IChatable
	{
		// Token: 0x06000B72 RID: 2930 RVA: 0x00009441 File Offset: 0x00007641
		public static AutoPean getInstance()
		{
			if (AutoPean._Instance == null)
			{
				AutoPean._Instance = new AutoPean();
			}
			return AutoPean._Instance;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x000A70A0 File Offset: 0x000A52A0
		public static void Update()
		{
			if (AutoPean.isAutoRequestPean)
			{
				AutoPean.RequestPean();
			}
			if (AutoPean.isAutoDonatePean && GameCanvas.gameTick % 40 == 0)
			{
				AutoPean.DonatePean();
			}
			if (AutoPean.isAutoHarvestPean)
			{
				AutoPean.HarvestPean();
			}
			if (!global::Char.myCharz().meDead)
			{
				AutoPean.AutoUsePean();
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x000A70EC File Offset: 0x000A52EC
		public void onChatFromMe(string text, string to)
		{
			if (ChatTextField.gI().tfChat.getText() == null || ChatTextField.gI().tfChat.getText().Equals(string.Empty) || text.Equals(string.Empty) || text == null)
			{
				ChatTextField.gI().isShow = false;
				AutoPean.ResetChatTextField();
				return;
			}
			if (ChatTextField.gI().strChat.Equals(AutoPean.inputHPPercent[0]))
			{
				try
				{
					int num = int.Parse(ChatTextField.gI().tfChat.getText());
					if (num >= 100)
					{
						num = 99;
					}
					if (num <= 0)
					{
						num = 1;
					}
					AutoPean.minimumHPPercent = num;
					GameScr.info1.addInfo("Auto pean khi HP dưới: " + num + "%", 0);
					if (AutoPean.isSaveData)
					{
						Rms.saveRMSInt("AutoPeanPercentHP", AutoPean.minimumHPPercent);
					}
				}
				catch
				{
					GameScr.info1.addInfo("%HP không hợp lệ, vui lòng nhập lại", 0);
				}
				AutoPean.ResetChatTextField();
				return;
			}
			if (ChatTextField.gI().strChat.Equals(AutoPean.inputMPPercent[0]))
			{
				try
				{
					int num2 = int.Parse(ChatTextField.gI().tfChat.getText());
					if (num2 >= 100)
					{
						num2 = 99;
					}
					if (num2 <= 0)
					{
						num2 = 1;
					}
					AutoPean.minimumMPPercent = num2;
					GameScr.info1.addInfo("Auto pean khi MP dưới: " + num2 + "%", 0);
					if (AutoPean.isSaveData)
					{
						Rms.saveRMSInt("AutoPeanPercentMP", AutoPean.minimumMPPercent);
					}
				}
				catch
				{
					GameScr.info1.addInfo("%MP không hợp lệ, vui lòng nhập lại", 0);
				}
				AutoPean.ResetChatTextField();
				return;
			}
			if (ChatTextField.gI().strChat.Equals(AutoPean.inputHP[0]))
			{
				try
				{
					int num3 = AutoPean.minimumHP = int.Parse(ChatTextField.gI().tfChat.getText());
					if (AutoPean.isSaveData)
					{
						Rms.saveRMSString("AutoPeanHP", AutoPean.minimumHP.ToString());
					}
					GameScr.info1.addInfo("Auto pean khi HP dưới: " + NinjaUtil.getMoneys((long)num3) + "HP", 0);
				}
				catch
				{
					GameScr.info1.addInfo("HP không hợp lệ, vui lòng nhập lại", 0);
				}
				AutoPean.ResetChatTextField();
				return;
			}
			if (ChatTextField.gI().strChat.Equals(AutoPean.inputMP[0]))
			{
				try
				{
					int num4 = AutoPean.minimumMP = int.Parse(ChatTextField.gI().tfChat.getText());
					if (AutoPean.isSaveData)
					{
						Rms.saveRMSString("AutoPeanMP", AutoPean.minimumMP.ToString());
					}
					GameScr.info1.addInfo("Auto pean khi MP dưới: " + NinjaUtil.getMoneys((long)num4) + "MP", 0);
				}
				catch
				{
					GameScr.info1.addInfo("MP không hợp lệ, vui lòng nhập lại", 0);
				}
				AutoPean.ResetChatTextField();
				return;
			}
			Service.gI().chat(text);
			ChatTextField.gI().isShow = false;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x000045ED File Offset: 0x000027ED
		public void onCancelChat()
		{
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x000A73C8 File Offset: 0x000A55C8
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 1:
				AutoPean.isAutoRequestPean = !AutoPean.isAutoRequestPean;
				GameScr.info1.addInfo("Xin Đậu\n" + (AutoPean.isAutoRequestPean ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				if (AutoPean.isSaveData)
				{
					Rms.saveRMSInt("AutoPeanIsAutoRequestPean", AutoPean.isAutoRequestPean ? 1 : 0);
					return;
				}
				break;
			case 2:
				AutoPean.isAutoDonatePean = !AutoPean.isAutoDonatePean;
				GameScr.info1.addInfo("Cho Đậu\n" + (AutoPean.isAutoDonatePean ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				if (AutoPean.isSaveData)
				{
					Rms.saveRMSInt("AutoPeanIsAutoSendPean", AutoPean.isAutoDonatePean ? 1 : 0);
					return;
				}
				break;
			case 3:
				AutoPean.isAutoHarvestPean = !AutoPean.isAutoHarvestPean;
				GameScr.info1.addInfo("Thu Đậu\n" + (AutoPean.isAutoHarvestPean ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				if (AutoPean.isSaveData)
				{
					Rms.saveRMSInt("AutoPeanIsAutoHarvestPean", AutoPean.isAutoHarvestPean ? 1 : 0);
					return;
				}
				break;
			case 4:
				if (AutoPean.minimumHP != 0)
				{
					AutoPean.minimumHP = 0;
					GameScr.info1.addInfo("Auto đậu: 0HP", 0);
					if (AutoPean.isSaveData)
					{
						Rms.saveRMSString("AutoPeanHP", AutoPean.minimumHP.ToString());
						return;
					}
				}
				else if (AutoPean.minimumHP == 0)
				{
					ChatTextField.gI().strChat = AutoPean.inputHP[0];
					ChatTextField.gI().tfChat.name = AutoPean.inputHP[1];
					ChatTextField.gI().startChat2(AutoPean.getInstance(), string.Empty);
					return;
				}
				break;
			case 5:
				if (AutoPean.minimumHPPercent != 0)
				{
					AutoPean.minimumHPPercent = 0;
					GameScr.info1.addInfo("Auto đậu: 0% HP", 0);
					if (AutoPean.isSaveData)
					{
						Rms.saveRMSInt("AutoPeanPercentHP", AutoPean.minimumHPPercent);
						return;
					}
				}
				else if (AutoPean.minimumHPPercent == 0)
				{
					ChatTextField.gI().strChat = AutoPean.inputHPPercent[0];
					ChatTextField.gI().tfChat.name = AutoPean.inputHPPercent[1];
					ChatTextField.gI().startChat2(AutoPean.getInstance(), string.Empty);
					return;
				}
				break;
			case 6:
				if (AutoPean.minimumMP != 0)
				{
					AutoPean.minimumMP = 0;
					GameScr.info1.addInfo("Auto đậu: 0MP", 0);
					if (AutoPean.isSaveData)
					{
						Rms.saveRMSString("AutoPeanMP", AutoPean.minimumMP.ToString());
						return;
					}
				}
				else if (AutoPean.minimumMP == 0)
				{
					ChatTextField.gI().strChat = AutoPean.inputMP[0];
					ChatTextField.gI().tfChat.name = AutoPean.inputMP[1];
					ChatTextField.gI().startChat2(AutoPean.getInstance(), string.Empty);
					return;
				}
				break;
			case 7:
				if (AutoPean.minimumMPPercent != 0)
				{
					AutoPean.minimumMPPercent = 0;
					GameScr.info1.addInfo("Auto đậu: 0% MP", 0);
					if (AutoPean.isSaveData)
					{
						Rms.saveRMSInt("AutoPeanPercentMP", AutoPean.minimumMPPercent);
						return;
					}
				}
				else if (AutoPean.minimumMPPercent == 0)
				{
					ChatTextField.gI().strChat = AutoPean.inputMPPercent[0];
					ChatTextField.gI().tfChat.name = AutoPean.inputMPPercent[1];
					ChatTextField.gI().startChat2(AutoPean.getInstance(), string.Empty);
					return;
				}
				break;
			case 8:
				AutoPean.isSaveData = !AutoPean.isSaveData;
				GameScr.info1.addInfo("Lưu Cài Đặt\n" + (AutoPean.isSaveData ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				Rms.saveRMSInt("AutoPeanIsSaveRms", AutoPean.isSaveData ? 1 : 0);
				if (AutoPean.isSaveData)
				{
					AutoPean.SaveData();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x000A7754 File Offset: 0x000A5954
		public static void ShowMenu()
		{
			AutoPean.LoadData();
			AutoPeanDlg dlg = new AutoPeanDlg();
			dlg.show();
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x000A78E4 File Offset: 0x000A5AE4
		private static void LoadData()
		{
			AutoPean.isSaveData = (Rms.loadRMSInt("AutoPeanIsSaveRms") == 1);
			if (AutoPean.isSaveData)
			{
				AutoPean.isAutoRequestPean = (Rms.loadRMSInt("AutoPeanIsAutoRequestPean") == 1);
				AutoPean.isAutoDonatePean = (Rms.loadRMSInt("AutoPeanIsAutoSendPean") == 1);
				AutoPean.isAutoHarvestPean = (Rms.loadRMSInt("AutoPeanIsAutoHarvestPean") == 1);
				if (Rms.loadRMSInt("AutoPeanPercentHP") == -1)
				{
					AutoPean.minimumHPPercent = 0;
				}
				else
				{
					AutoPean.minimumHPPercent = Rms.loadRMSInt("AutoPeanPercentHP");
				}
				if (Rms.loadRMSInt("AutoPeanPercentMP") == -1)
				{
					AutoPean.minimumMPPercent = 0;
				}
				else
				{
					AutoPean.minimumMPPercent = Rms.loadRMSInt("AutoPeanPercentMP");
				}
				if (Rms.loadRMSString("AutoPeanHP") == null)
				{
					AutoPean.minimumHP = 0;
				}
				else
				{
					AutoPean.minimumHP = int.Parse(Rms.loadRMSString("AutoPeanHP"));
				}
				if (Rms.loadRMSString("AutoPeanMP") == null)
				{
					AutoPean.minimumMP = 0;
					return;
				}
				AutoPean.minimumMP = int.Parse(Rms.loadRMSString("AutoPeanMP"));
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x000A79DC File Offset: 0x000A5BDC
		private static void SaveData()
		{
			Rms.saveRMSInt("AutoPeanIsAutoRequestPean", AutoPean.isAutoRequestPean ? 1 : 0);
			Rms.saveRMSInt("AutoPeanIsAutoSendPean", AutoPean.isAutoDonatePean ? 1 : 0);
			Rms.saveRMSInt("AutoPeanIsAutoHarvestPean", AutoPean.isAutoHarvestPean ? 1 : 0);
			Rms.saveRMSString("AutoPeanHP", AutoPean.minimumHP.ToString());
			Rms.saveRMSInt("AutoPeanPercentHP", AutoPean.minimumHPPercent);
			Rms.saveRMSString("AutoPeanMP", AutoPean.minimumMP.ToString());
			Rms.saveRMSInt("AutoPeanPercentMP", AutoPean.minimumMPPercent);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00009276 File Offset: 0x00007476
		private static void ResetChatTextField()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00009459 File Offset: 0x00007659
		private static void RequestPean()
		{
			if (mSystem.currentTimeMillis() - AutoPean.lastTimeRequestedPean >= 301000L)
			{
				AutoPean.lastTimeRequestedPean = mSystem.currentTimeMillis();
				Service.gI().clanMessage(1, "", -1);
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x000A7A70 File Offset: 0x000A5C70
		private static void DonatePean()
		{
			for (int i = 0; i < ClanMessage.vMessage.size(); i++)
			{
				ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(i);
				if (clanMessage.maxCap != 0 && clanMessage.playerName != global::Char.myCharz().cName && clanMessage.recieve != clanMessage.maxCap)
				{
					Service.gI().clanDonate(clanMessage.id);
					return;
				}
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x000A7AE4 File Offset: 0x000A5CE4
		private static void HarvestPean()
		{
			if (TileMap.mapID != 21 && TileMap.mapID != 22 && TileMap.mapID != 23)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < global::Char.myCharz().arrItemBox.Length; i++)
			{
				if (global::Char.myCharz().arrItemBox[i] != null && global::Char.myCharz().arrItemBox[i].template.type == 6)
				{
					num += global::Char.myCharz().arrItemBox[i].quantity;
				}
			}
			if (num < 20 && GameCanvas.gameTick % 120 == 0)
			{
				for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
				{
					if (global::Char.myCharz().arrItemBag[j] != null && global::Char.myCharz().arrItemBag[j].template.type == 6)
					{
						Service.gI().getItem(1, (sbyte)j);
						break;
					}
				}
			}
			if (GameScr.gI().magicTree.currPeas > 0 && (GameScr.hpPotion < 10 || num < 20) && GameCanvas.gameTick % 200 == 0)
			{
				Service.gI().openMenu(4);
				Service.gI().menu(4, 0, 0);
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x000A7C04 File Offset: 0x000A5E04
		private static void AutoUsePean()
		{
			if (GameScr.hpPotion > 0)
			{
				if (AutoPean.minimumHPPercent != 0 && AutoPean.isMyHPLowerThan(AutoPean.minimumHP, AutoPean.minimumHPPercent) && GameCanvas.gameTick % 40 == 0)
				{
					GameScr.gI().doUseHP();
					return;
				}
				if (AutoPean.minimumMPPercent != 0 && AutoPean.isMyMPLowerThan(AutoPean.minimumMP, AutoPean.minimumMPPercent) && GameCanvas.gameTick % 40 == 0)
				{
					GameScr.gI().doUseHP();
				}
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00009489 File Offset: 0x00007689
		public static int MyHPPercent()
		{
			return (int)(global::Char.myCharz().cHP * 100L / global::Char.myCharz().cHPFull);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x000094A5 File Offset: 0x000076A5
		public static int MyMPPercent()
		{
			return (int)(global::Char.myCharz().cMP * 100L / global::Char.myCharz().cMPFull);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000094C1 File Offset: 0x000076C1
		private static bool isMyHPLowerThan(int minHP, int minHPPercent)
		{
			return global::Char.myCharz().cHP > 0L && (AutoPean.MyHPPercent() <= minHPPercent || global::Char.myCharz().cHP < (long)minHP);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000094EB File Offset: 0x000076EB
		private static bool isMyMPLowerThan(int minMP, int minMPPercent)
		{
			return global::Char.myCharz().cHP > 0L && (AutoPean.MyMPPercent() <= minMPPercent || global::Char.myCharz().cMP < (long)minMP);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x000A7C74 File Offset: 0x000A5E74
		static AutoPean()
		{
			AutoPean.LoadData();
		}

		// Token: 0x0400162C RID: 5676
		public static AutoPean _Instance;

		// Token: 0x0400162D RID: 5677
		public static bool isAutoRequestPean;

		// Token: 0x0400162E RID: 5678
		private static long lastTimeRequestedPean;

		// Token: 0x0400162F RID: 5679
		public static bool isAutoDonatePean;

		// Token: 0x04001630 RID: 5680
		public static bool isAutoHarvestPean;

		// Token: 0x04001631 RID: 5681
		public static int minimumHPPercent;

		// Token: 0x04001632 RID: 5682
		public static int minimumMPPercent;

		// Token: 0x04001633 RID: 5683
		public static int minimumHP;

		// Token: 0x04001634 RID: 5684
		public static int minimumMP;

		// Token: 0x04001635 RID: 5685
		public static bool isSaveData;

		// Token: 0x04001636 RID: 5686
		private static string[] inputHPPercent = new string[]
		{
			"Nhập %HP Pean",
			"%HP"
		};

		// Token: 0x04001637 RID: 5687
		private static string[] inputMPPercent = new string[]
		{
			"Nhập %MP Pean",
			"%MP"
		};

		// Token: 0x04001638 RID: 5688
		private static string[] inputHP = new string[]
		{
			"Nhập HP Pean",
			"HP"
		};

		// Token: 0x04001639 RID: 5689
		private static string[] inputMP = new string[]
		{
			"Nhập MP Pean",
			"MP"
		};
	}
}
