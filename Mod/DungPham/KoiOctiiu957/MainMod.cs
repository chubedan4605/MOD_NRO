using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Mod.DungPham.KoiOctiiu957
{
	// Token: 0x02000113 RID: 275
	public class MainMod : IActionListener, IChatable
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x0000953B File Offset: 0x0000773B
		public static MainMod getInstance()
		{
			if (MainMod._Instance == null)
			{
				MainMod._Instance = new MainMod();
			}
			return MainMod._Instance;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x000A8640 File Offset: 0x000A6840
		

		public static void Update()
		{
			if ((!MobCapcha.isAttack || !MobCapcha.explode) && GameScr.gI().mobCapcha != null)
			{
				if (!MainMod.isSlovingCapcha && GameCanvas.gameTick % 100 == 0)
				{
					MainMod.isSlovingCapcha = true;
					return;
				}
			}
			else
			{
				if (MainMod.isShowCharsInMap)
				{
					MainMod.listCharsInMap.Clear();
					for (int i = 0; i < GameScr.vCharInMap.size(); i++)
					{
						global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
						if (@char.cName != null && @char.cName != "" && !@char.isPet && !@char.isMiniPet && !@char.cName.StartsWith("#") && !@char.cName.StartsWith("$") && @char.cName != "Trọng tài")
						{
							MainMod.listCharsInMap.Add(@char);
						}
					}
				}
				bool flag = MainMod.isAutoEnterNRDMap;
				if (MainMod.isAutoRevive)
				{
					MainMod.Revive();
				}
				if (MainMod.isGMT)
				{
					MainMod.LockFocus();
				}
				if (MainMod.isConnectToAccountManager)
				{
					MainMod.ConnectToAccountManager();
				}
				AutoItem.Update();
				AutoPean.Update();
				AutoSkill.Update();
				AutoTrain.Update();
				AutoPick.Update();
				AutoMap.Update();
				AutoPoint.Update();
				AutoChat.Update();
				ModSkin.Update();

				try
				{
					if (global::Char.myCharz() != null)
					{
						MainMod.updateCharEff(global::Char.myCharz());
					}
					for (int i = 0; i < GameScr.vCharInMap.size(); i++)
					{
						global::Char c = (global::Char)GameScr.vCharInMap.elementAt(i);
						if (c != null)
						{
							MainMod.updateCharEff(c);
						}
					}
				}
				catch { }
				global::Char.myCharz().cspeed = MainMod.runSpeed;
			}
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x000A878C File Offset: 0x000A698C
		public static void Paint(mGraphics g)
		{
			MainMod.paintListBosses(g);
			if (MainMod.isShowCharsInMap)
			{
				MainMod.paintListCharsInMap(g);
			}
			int startY = (int)((float)GameCanvas.h * 0.30f);
			if (startY < 75)
			{
				startY = 75;
			}
			mFont.tahoma_7.drawString(g, string.Concat(new string[]
			{
				"Map: [",
				TileMap.mapID.ToString(),
				"] ",
				TileMap.mapNames[TileMap.mapID],
				" [",
				TileMap.zoneID.ToString(),
				"]"
			}), 25, startY, 0);
			startY += 10;
			mFont.tahoma_7.drawString(g, "Time: " + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), 25, startY, 0);
			startY += 10;
			mFont.tahoma_7.drawString(g, string.Concat(new string[]
			{
				"X: ",
				global::Char.myCharz().cx.ToString(),
				" - ",
				global::Char.myCharz().cy.ToString()
			}), 25, startY, 0);
			startY += 10;
				
			int num = startY;
			if (MainMod.isConnectToAccountManager)
			{
				mFont.tahoma_7.drawString(g, "Đã kết nối!", 25, num, 0);
				num += 10;
			}
			if (AutoSkill.isAutoSendAttack)
			{
				mFont.tahoma_7.drawString(g, "Tự đánh: on", 25, num, 0);
				num += 10;
			}
			if (MainMod.isAutoRevive)
			{
				mFont.tahoma_7.drawString(g, "Hồi sinh: on", 25, num, 0);
				num += 10;
			}
			if (AutoPick.isAutoPick)
			{
				mFont.tahoma_7.drawString(g, "Auto nhặt: on", 25, num, 0);
				num += 10;
			}
			if (MainMod.isGMT)
			{
				mFont.tahoma_7.drawString(g, "Khóa: " + MainMod.IDCharGMT.ToString(), 25, num, 0);
				num += 10;
			}
			if (MainMod.isAutoEnterNRDMap)
			{
				mFont.tahoma_7.drawString(g, "Đang auto nrd: " + MainMod.mapIdNRD.ToString() + "sk" + MainMod.zoneIdNRD.ToString(), 25, num, 0);
				num += 10;
			}
			if (MainMod.isUDTvaAntiMatsong)
			{
				mFont.tahoma_7.drawString(g, "Auto up đệ: on", 25, num, 0);
				num += 10;
			}
			if (MainMod.isLockMap)
			{
				mFont.tahoma_7.drawString(g, "Lock map: on", 25, num, 0);
				num += 10;
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x000A8A08 File Offset: 0x000A6C08
		public static void paintCharInfo(mGraphics g, global::Char ch)
		{
			int centerX = GameCanvas.w / 2;
			mFont.tahoma_7b_red.drawString(g, string.Concat(new string[]
			{
				ch.cName,
				" [",
				NinjaUtil.getMoneys(ch.cHP),
				"/",
				NinjaUtil.getMoneys(ch.cHPFull),
				"]"
			}), centerX, MainMod.infoStartY, 2);
			int num = MainMod.infoStartY + 10;
			int num2 = 10;
			if (ch.isNRD)
			{
				mFont.tahoma_7b_yellow.drawString(g, "NRD: Còn " + ch.timeNRD + " giây", centerX, num, 2);
				num += num2;
			}
			if (ch.isFreez)
			{
				mFont.tahoma_7b_red.drawString(g, "Bị TDHS: " + ch.freezSeconds + "s", centerX, num, 2);
				num += num2;
			}
			if (ch.protectEff)
			{
				long startTime = 0L;
				if (dictTimeShield.TryGetValue(ch.charID, out startTime))
				{
					int seconds = 46 - (int)((mSystem.currentTimeMillis() - startTime) / 1000L);
					if (seconds < 0) seconds = 0;
					mFont.tahoma_7b_blue.drawString(g, "Khiên: " + seconds + "s", centerX, num, 2);
					num += num2;
				}
			}
			if (ch.holdEffID != 0)
			{
				long startTime = 0L;
				if (dictTimeTie.TryGetValue(ch.charID, out startTime))
				{
					int seconds = 46 - (int)((mSystem.currentTimeMillis() - startTime) / 1000L);
					if (seconds < 0) seconds = 0;
					mFont.tahoma_7b_red.drawString(g, "Trói: " + seconds + "s", centerX, num, 2);
					num += num2;
				}
			}
			if (ch.sleepEff)
			{
				long startTime = 0L;
				if (dictTimeSleep.TryGetValue(ch.charID, out startTime))
				{
					int seconds = 12 - (int)((mSystem.currentTimeMillis() - startTime) / 1000L);
					if (seconds < 0) seconds = 0;
					mFont.tahoma_7b_yellow.drawString(g, "Thôi miên: " + seconds + "s", centerX, num, 2);
					num += num2;
				}
			}
			if ((int)ch.isMonkey == 1)
			{
				long startTime = 0L;
				if (dictTimeMonkey.TryGetValue(ch.charID, out startTime))
				{
					int seconds = 121 - (int)((mSystem.currentTimeMillis() - startTime) / 1000L);
					if (seconds < 0) seconds = 0;
					mFont.tahoma_7b_red.drawString(g, "Khỉ: " + seconds + "s", centerX, num, 2);
					num += num2;
				}
			}
			if (ch.huytSao)
			{
				long startTime = 0L;
				if (dictTimeWhistle.TryGetValue(ch.charID, out startTime))
				{
					int seconds = 31 - (int)((mSystem.currentTimeMillis() - startTime) / 1000L);
					if (seconds < 0) seconds = 0;
					mFont.tahoma_7b_blue.drawString(g, "Huýt sáo: " + seconds + "s", centerX, num, 2);
					num += num2;
				}
			}
			if (ch.stone)
			{
				long startTime = 0L;
				if (dictTimeStone.TryGetValue(ch.charID, out startTime))
				{
					int seconds = (int)((mSystem.currentTimeMillis() - startTime) / 1000L);
					mFont.tahoma_7b_yellow.drawString(g, "Hóa đá: " + seconds + "s", centerX, num, 2);
					num += num2;
				}
			}
			MainMod.infoStartY = num + 3;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x000045ED File Offset: 0x000027ED
		public static void smethod_15(mGraphics g, int x, int y)
		{
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x000A8AEC File Offset: 0x000A6CEC
		public static void paintListBosses(mGraphics g)
		{
			if (MainMod.isHuntingBoss && !MainMod.isMeInNRDMap())
			{
				int num = 42;
				for (int i = 0; i < MainMod.listBosses.Count; i++)
				{
					g.setColor(2721889, 0.5f);
					g.fillRect(GameCanvas.w - 23, num + 2, 21, 9);
					MainMod.listBosses[i].Paint(g, GameCanvas.w - 2, num, mFont.RIGHT);
					num += 10;
				}
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x000A8B68 File Offset: 0x000A6D68
		public void onChatFromMe(string text, string to)
		{
			if (ChatTextField.gI().tfChat.getText() != null && !ChatTextField.gI().tfChat.getText().Equals(string.Empty) && !text.Equals(string.Empty) && text != null)
			{
				if (ChatTextField.gI().strChat.Equals(MainMod.inputLockFocusCharID[0]))
				{
					try
					{
						int num = MainMod.charIDLock = int.Parse(ChatTextField.gI().tfChat.getText());
						MainMod.isLockFocus = true;
						GameScr.info1.addInfo("Đã Thêm: " + num.ToString(), 0);
					}
					catch
					{
						GameScr.info1.addInfo("CharID Không Hợp Lệ, Vui Lòng Nhập Lại", 0);
					}
					MainMod.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(MainMod.inputHPFusionDance[0]))
				{
					try
					{
						int num2 = MainMod.minimumHPFusionDance = int.Parse(ChatTextField.gI().tfChat.getText());
						GameScr.info1.addInfo("Hợp Thể Khi HP Dưới: " + Res.formatNumber2((long)num2), 0);
					}
					catch
					{
						GameScr.info1.addInfo("HP Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					MainMod.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(MainMod.inputCharID[0]))
				{
					try
					{
						int item = int.Parse(ChatTextField.gI().tfChat.getText());
						MainMod.listCharIDs.Add(item);
						GameScr.info1.addInfo("Đã Thêm: " + item.ToString(), 0);
					}
					catch
					{
						GameScr.info1.addInfo("CharID Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					MainMod.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(MainMod.inputHPLimit[0]))
				{
					try
					{
						int num3 = MainMod.HPLimit = int.Parse(ChatTextField.gI().tfChat.getText());
						GameScr.info1.addInfo("Limit: " + NinjaUtil.getMoneys((long)num3) + " HP", 0);
					}
					catch
					{
						GameScr.info1.addInfo("HP Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					MainMod.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(MainMod.inputHPChar[0]))
				{
					try
					{
						int num4 = MainMod.limitHPChar = int.Parse(ChatTextField.gI().tfChat.getText());
						GameScr.info1.addInfo("Limit: " + NinjaUtil.getMoneys((long)num4) + " HP", 0);
					}
					catch
					{
						GameScr.info1.addInfo("HP Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					MainMod.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(MainMod.inputHPPercentFusionDance[0]))
				{
					try
					{
						int num5 = int.Parse(ChatTextField.gI().tfChat.getText());
						if (num5 > 99)
						{
							num5 = 99;
						}
						MainMod.minumumHPPercentFusionDance = num5;
						GameScr.info1.addInfo("Hợp Thể Khi HP Dưới: " + num5.ToString() + "%", 0);
					}
					catch
					{
						GameScr.info1.addInfo("%HP Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
					}
					MainMod.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(MainMod.inputFPS[0]))
				{
					try
					{
						int fps = int.Parse(ChatTextField.gI().tfChat.getText());
						if (fps < 5) fps = 5;
						if (fps > 200) fps = 200;
						MainMod.targetFPS = fps;
						UnityEngine.Application.targetFrameRate = fps;
						Rms.saveRMSInt("targetFPS", fps);
						GameScr.info1.addInfo("Đã cài FPS: " + fps, 0);
					}
					catch { GameScr.info1.addInfo("FPS không hợp lệ!", 0); }
					MainMod.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(MainMod.inputDelayJump[0]))
				{
					try
					{
						int delay = int.Parse(ChatTextField.gI().tfChat.getText());
						if (delay < 50) delay = 50;
						MainMod.delayAutoJump = delay;
						Rms.saveRMSInt("delayAutoJump", delay);
						GameScr.info1.addInfo("Delay Nhảy: " + delay + "ms", 0);
					}
					catch { GameScr.info1.addInfo("Delay không hợp lệ!", 0); }
					MainMod.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(MainMod.inputDistanceJump[0]))
				{
					try
					{
						int dist = int.Parse(ChatTextField.gI().tfChat.getText());
						if (dist < 5) dist = 5;
						MainMod.distanceAutoJump = dist;
						GameScr.info1.addInfo("Khoảng Cách Nhảy: " + dist + "px", 0);
					}
					catch { GameScr.info1.addInfo("Khoảng cách không hợp lệ!", 0); }
					MainMod.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(MainMod.inputDelayMove[0]))
				{
					try
					{
						int delay = int.Parse(ChatTextField.gI().tfChat.getText());
						if (delay < 50) delay = 50;
						MainMod.delayAutoMove = delay;
						Rms.saveRMSInt("delayAutoMove", delay);
						GameScr.info1.addInfo("Delay Di Chuyển: " + delay + "ms", 0);
					}
					catch { GameScr.info1.addInfo("Delay không hợp lệ!", 0); }
					MainMod.ResetChatTextField();
					return;
				}
				if (ChatTextField.gI().strChat.Equals(MainMod.inputDistanceMove[0]))
				{
					try
					{
						int dist = int.Parse(ChatTextField.gI().tfChat.getText());
						if (dist < 5) dist = 5;
						MainMod.distanceAutoMove = dist;
						Rms.saveRMSInt("distanceAutoMove", dist);
						GameScr.info1.addInfo("Khoảng Cách Di Chuyển: " + dist + "px", 0);
					}
					catch { GameScr.info1.addInfo("Khoảng cách không hợp lệ!", 0); }
					MainMod.ResetChatTextField();
					return;
				}
			}
			else
			{
				ChatTextField.gI().isShow = false;
				MainMod.ResetChatTextField();
			}
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x000045ED File Offset: 0x000027ED
		public void onCancelChat()
		{
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x000A8EC0 File Offset: 0x000A70C0
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 10001:
				MainMod.isAutoLogin = !MainMod.isAutoLogin;
				GameScr.info1.addInfo("Auto Login " + (MainMod.isAutoLogin ? "[STATUS: ON] " : "[STATUS: OFF]"), 0);
				return;
			case 1:
				AutoMap.ShowMenu();
				return;
			case 2:
				AutoSkill.ShowMenu();
				return;
			case 3:
				AutoPean.ShowMenu();
				return;
			case 4:
				AutoPick.ShowMenu();
				return;
			case 5:
				AutoTrain.ShowMenu();
				return;
			case 6:
				AutoChat.ShowMenu();
				return;
			case 7:
				AutoPoint.ShowMenu();
				return;
			case 8:
				MainMod.ShowMenuMore();
				return;
			case 9:
				if (MainMod.minumumHPPercentFusionDance > 0)
				{
					MainMod.minumumHPPercentFusionDance = 0;
					GameScr.info1.addInfo("Hợp thể khi HP dưới: 0% HP", 0);
					return;
				}
				ChatTextField.gI().strChat = MainMod.inputHPPercentFusionDance[0];
				ChatTextField.gI().tfChat.name = MainMod.inputHPPercentFusionDance[1];
				ChatTextField.gI().startChat2(MainMod.getInstance(), string.Empty);
				return;
			case 10:
				if (MainMod.minimumHPFusionDance > 0)
				{
					MainMod.minimumHPFusionDance = 0;
					GameScr.info1.addInfo("Hợp thể khi HP dưới: 0", 0);
					return;
				}
				ChatTextField.gI().strChat = MainMod.inputHPFusionDance[0];
				ChatTextField.gI().tfChat.name = MainMod.inputHPFusionDance[1];
				ChatTextField.gI().startChat2(MainMod.getInstance(), string.Empty);
				return;
			case 11:
				MainMod.smethod_2();
				return;
			case 12:
				MainMod.isAutoLockControl = !MainMod.isAutoLockControl;
				GameScr.info1.addInfo("Auto Khống Chế\n" + (MainMod.isAutoLockControl ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 13:
				MainMod.isAutoTeleport = !MainMod.isAutoTeleport;
				GameScr.info1.addInfo("Auto Teleport\n" + (MainMod.isAutoTeleport ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 14:
				MainMod.smethod_3();
				return;
			case 15:
				ChatTextField.gI().strChat = MainMod.inputCharID[0];
				ChatTextField.gI().tfChat.name = MainMod.inputCharID[1];
				ChatTextField.gI().startChat2(MainMod.getInstance(), string.Empty);
				return;
			case 16:
			{
				int num = (int)p;
				if (num != 0)
				{
					MainMod.listCharIDs.Add(num);
					GameScr.info1.addInfo("Đã Thêm: " + num.ToString(), 0);
					return;
				}
				break;
			}
			case 17:
			{
				int num2 = (int)p;
				if (num2 != 0)
				{
					MainMod.listCharIDs.Remove(num2);
					GameScr.info1.addInfo("Đã Xóa: " + num2.ToString(), 0);
					return;
				}
				break;
			}
			case 18:
				MainMod.smethod_0();
				return;
			case 19:
				MainMod.isAutoAttackBoss = !MainMod.isAutoAttackBoss;
				GameScr.info1.addInfo("Tấn Công Boss\n" + (MainMod.isAutoAttackBoss ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 20:
				ChatTextField.gI().strChat = MainMod.inputHPLimit[0];
				ChatTextField.gI().tfChat.name = MainMod.inputHPLimit[1];
				ChatTextField.gI().startChat2(MainMod.getInstance(), string.Empty);
				return;
			case 21:
				MainMod.smethod_1();
				return;
			case 22:
				MainMod.isAutoAttackOtherChars = !MainMod.isAutoAttackOtherChars;
				GameScr.info1.addInfo("Tàn Sát Người\n" + (MainMod.isAutoAttackOtherChars ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 23:
				ChatTextField.gI().strChat = MainMod.inputHPChar[0];
				ChatTextField.gI().tfChat.name = MainMod.inputHPChar[1];
				ChatTextField.gI().startChat2(MainMod.getInstance(), string.Empty);
				return;
			case 24:
				GameScr.info1.addInfo("Tính năng chưa hoàn thiện, vui lòng chờ bản update!", 0);
				return;
			case 25:
				GameScr.info1.addInfo("Tính năng chưa hoàn thiện, vui lòng chờ bản update!", 0);
				return;
			case 26:
				GameScr.info1.addInfo("Tính năng chưa hoàn thiện, vui lòng chờ bản update!", 0);
				return;
			case 27:
				GameScr.info1.addInfo("Tính năng chưa hoàn thiện, vui lòng chờ bản update!", 0);
				return;
			case 28:
				MainMod.isPaintBackground = !MainMod.isPaintBackground;
				Rms.saveRMSInt("isPaintBgr", MainMod.isPaintBackground ? 1 : 0);
				return;
			case 29:
				MainMod.isHuntingBoss = !MainMod.isHuntingBoss;
				Rms.saveRMSInt("sanboss", MainMod.isHuntingBoss ? 1 : 0);
				return;
			case 30:
				MainMod.isShowCharsInMap = !MainMod.isShowCharsInMap;
				Rms.saveRMSInt("showchar", MainMod.isShowCharsInMap ? 1 : 0);
				return;
			case 31:
				MainMod.isReduceGraphics = !MainMod.isReduceGraphics;
				Rms.saveRMSInt("IsReduceGraphics", MainMod.isReduceGraphics ? 1 : 0);
				return;
			case 32:
				MainMod.isAutoT77 = !MainMod.isAutoT77;
				GameScr.info1.addInfo("Auto T77\n" + (MainMod.isAutoT77 ? "[STATUS: ON] " : "[STATUS: OFF]"), 0);
				return;
			case 33:
				MainMod.isAutoBomPicPoc = !MainMod.isAutoBomPicPoc;
				GameScr.info1.addInfo("Auto Bom\nPic Poc" + (MainMod.isAutoBomPicPoc ? "[STATUS: ON] " : "[STATUS: OFF]"), 0);
				return;
			case 34:
				MainMod.serverChat = !MainMod.serverChat;
				Rms.saveRMSInt("serverChat", MainMod.serverChat ? 1 : 0);
				GameScr.info1.addInfo("Server Chat\n" + (MainMod.serverChat ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 36:
				MainMod.ShowMenuDisplaySettings();
				return;
			case 37:
				ChatTextField.gI().strChat = MainMod.inputFPS[0];
				ChatTextField.gI().tfChat.name = MainMod.inputFPS[1];
				ChatTextField.gI().startChat2(MainMod.getInstance(), string.Empty);
				return;
			case 35:
				MainMod.ShowMenuPositionSettings();
				return;
			case 350:
				MainMod.ShowMenuAutoJump();
				return;
			case 360:
				MainMod.ShowMenuAutoMove();
				return;
			case 351:
				MainMod.isAutoJump = !MainMod.isAutoJump;
				Rms.saveRMSInt("isAutoJump", MainMod.isAutoJump ? 1 : 0);
				GameScr.info1.addInfo("Đã " + (MainMod.isAutoJump ? "bật" : "tắt") + " auto nhảy.", 0);
				if (MainMod.isAutoJump && global::Char.myCharz() != null)
				{
					MainMod.originJumpX = global::Char.myCharz().cx;
					MainMod.originJumpY = global::Char.myCharz().cy;
					new Thread(new ThreadStart(MainMod.AutoJumpThread)).Start();
				}
				return;
			case 352:
				ChatTextField.gI().strChat = MainMod.inputDelayJump[0];
				ChatTextField.gI().tfChat.name = MainMod.inputDelayJump[1];
				ChatTextField.gI().startChat2(MainMod.getInstance(), string.Empty);
				return;
			case 353:
				ChatTextField.gI().strChat = MainMod.inputDistanceJump[0];
				ChatTextField.gI().tfChat.name = MainMod.inputDistanceJump[1];
				ChatTextField.gI().startChat2(MainMod.getInstance(), string.Empty);
				return;
			case 361:
				MainMod.isAutoMove = !MainMod.isAutoMove;
				GameScr.info1.addInfo("Đã " + (MainMod.isAutoMove ? "bật" : "tắt") + " auto di chuyển.", 0);
				if (MainMod.isAutoMove && global::Char.myCharz() != null)
				{
					MainMod.originMoveX = global::Char.myCharz().cx;
					MainMod.originMoveY = global::Char.myCharz().cy;
					new Thread(new ThreadStart(MainMod.AutoMoveThread)).Start();
				}
				return;
			case 362:
				ChatTextField.gI().strChat = MainMod.inputDelayMove[0];
				ChatTextField.gI().tfChat.name = MainMod.inputDelayMove[1];
				ChatTextField.gI().startChat2(MainMod.getInstance(), string.Empty);
				return;
			case 363:
				ChatTextField.gI().strChat = MainMod.inputDistanceMove[0];
				ChatTextField.gI().tfChat.name = MainMod.inputDistanceMove[1];
				ChatTextField.gI().startChat2(MainMod.getInstance(), string.Empty);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000A9550 File Offset: 0x000A7750
		public static bool UpdateKey(int unused)
		{
			bool result;
			if (GameCanvas.keyAsciiPress == Hotkeys.A)
			{
				AutoSkill.isAutoSendAttack = !AutoSkill.isAutoSendAttack;
				GameScr.info1.addInfo("Tự Đánh\n" + (AutoSkill.isAutoSendAttack ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				result = true;
			}
			else if (GameCanvas.keyAsciiPress == Hotkeys.B)
			{
				Service.gI().friend(0, -1);
				result = true;
			}
			else if (GameCanvas.keyAsciiPress == Hotkeys.C)
			{
				for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
				{
					Item item = global::Char.myCharz().arrItemBag[i];
					if (item != null && (item.template.id == 194 || item.template.id == 193))
					{
						Service.gI().useItem(0, 1, (sbyte)item.indexUI, -1);
						break;
					}
				}
				result = true;
			}
			else if (GameCanvas.keyAsciiPress == Hotkeys.D)
			{
				AutoSkill.FreezeSelectedSkill();
				result = true;
			}
			else if (GameCanvas.keyAsciiPress == Hotkeys.E)
			{
				MainMod.isAutoRevive = !MainMod.isAutoRevive;
				GameScr.info1.addInfo("Auto Hồi Sinh\n" + (MainMod.isAutoRevive ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				result = true;
			}
			else if (GameCanvas.keyAsciiPress == Hotkeys.F)
			{
				bool isNhapThe = global::Char.myCharz().isNhapThe;
				int num = -1;
				foreach (Item item2 in global::Char.myCharz().arrItemBag)
				{
					if (item2 != null)
					{
						if (item2.template.id == 921)
						{
							num = 921;
						}
						if (item2.template.id == 454)
						{
							num = 454;
						}
						if (item2.template.id == 1884)
						{
							num = 1884;
						}
					}
				}
				if (num != -1)
				{
					MainMod.UseItem(num);
				}
				else
				{
					GameScr.info1.addInfo("Bạn không có bông tai!!", 0);
				}
				if (isNhapThe)
				{
					Service.gI().petStatus(3);
				}
				result = true;
			}
			else if (GameCanvas.keyAsciiPress == Hotkeys.G)
			{
				if (global::Char.myCharz().charFocus == null)
				{
					GameScr.info1.addInfo("Vui Lòng Chọn Mục Tiêu!", 0);
				}
				else
				{
					Service.gI().giaodich(0, global::Char.myCharz().charFocus.charID, -1, -1);
					GameScr.info1.addInfo("Đã Gửi Lời Mời Giao Dịch Đến: " + global::Char.myCharz().charFocus.cName, 0);
				}
				result = true;
			}
			else if (GameCanvas.keyAsciiPress == Hotkeys.I)
			{
				MainMod.isGMT = !MainMod.isGMT;
				MainMod.isgmtchar = false;
				MainMod.isgmtmob = false;
				GameScr.info1.addInfo("Đã " + (MainMod.isGMT ? "bật" : "tắt") + " auto giữ mục tiêu.", 0);
				if (global::Char.myCharz().charFocus != null)
				{
					MainMod.isgmtchar = true;
					MainMod.IDCharGMT = global::Char.myCharz().charFocus.charID;
				}
				if (global::Char.myCharz().mobFocus != null)
				{
					MainMod.isgmtmob = true;
					MainMod.IDMObGMT = global::Char.myCharz().mobFocus.mobId;
				}
				result = true;
			}
			else if (GameCanvas.keyAsciiPress == Hotkeys.J)
			{
				AutoMap.LoadMapLeft();
				result = true;
			}
			else if (GameCanvas.keyAsciiPress == Hotkeys.K)
			{
				AutoMap.LoadMapCenter();
				result = true;
			}
			else if (GameCanvas.keyAsciiPress == Hotkeys.L)
			{
				AutoMap.LoadMapRight();
				result = true;
			}
			else if (GameCanvas.keyAsciiPress == Hotkeys.M)
			{
				if (TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23)
				{
					return;
				}
				Service.gI().openUIZone();
				GameCanvas.panel.setTypeZone();
				GameCanvas.panel.show();
				result = true;
			}
			else if (GameCanvas.keyAsciiPress == Hotkeys.N)
			{
				if (MainMod.isMeInNRDMap())
				{
					AutoPick.isAutoPick = !AutoPick.isAutoPick;
					GameScr.info1.addInfo("Auto Nhặt\n" + (AutoPick.isAutoPick ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				}
				else
				{
					AutoPick.ShowMenu();
				}
				result = true;
			}
			else if (GameCanvas.keyAsciiPress == Hotkeys.H)
			{
				MainMod.isLockMap = !MainMod.isLockMap;
				GameScr.info1.addInfo("Lock Map\n" + (MainMod.isLockMap ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				result = true;
			}
			else
			{
				if (GameCanvas.keyAsciiPress == Hotkeys.O)
				{
					AutoItem.useSet(0);
					GameScr.info1.addInfo("Đã mặc set 1", 0);
					return true;
				}
				if (GameCanvas.keyAsciiPress == Hotkeys.P)
				{
					AutoItem.useSet(1);
					GameScr.info1.addInfo("Đã mặc set 2", 0);
					return true;
				}
				if (GameCanvas.keyAsciiPress == Hotkeys.T)
				{
					MainMod.UseItem(521);
					result = true;
				}
				else if (GameCanvas.keyAsciiPress == Hotkeys.X)
				{
					MainMod.ShowMenu();
					result = true;
				}
				else if (GameCanvas.keyAsciiPress == Hotkeys.U)
				{
					MainMod.buffMe();
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000A9A0C File Offset: 0x000A7C0C
		public static void ShowMenu()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Auto Map", MainMod.getInstance(), 1, null));
			myVector.addElement(new Command("Auto Skill", MainMod.getInstance(), 2, null));
			myVector.addElement(new Command("Auto Pean", MainMod.getInstance(), 3, null));
			myVector.addElement(new Command("Auto Pick", MainMod.getInstance(), 4, null));
			myVector.addElement(new Command("Auto Train", MainMod.getInstance(), 5, null));
			myVector.addElement(new Command("Cài Đặt\nHiển Thị", MainMod.getInstance(), 36, null));
			myVector.addElement(new Command("More", MainMod.getInstance(), 8, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x000A9AE4 File Offset: 0x000A7CE4
		public static void ShowMenuMore()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Auto Login\n" + (MainMod.isAutoLogin ? "[STATUS: ON] " : "[STATUS: OFF]"), MainMod.getInstance(), 10001, null));
			myVector.addElement(new Command("Auto Chat", MainMod.getInstance(), 6, null));
			myVector.addElement(new Command("Auto Point", MainMod.getInstance(), 7, null));
			myVector.addElement(new Command("Auto Vị Trí", MainMod.getInstance(), 35, null));
			myVector.addElement(new Command("Mod Skin\n" + ModSkin.GetMenuSummary(), ModSkin.getInstance(), 37, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		public static void ShowMenuPositionSettings()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Cài Đặt\nAuto Nhảy (Y)", MainMod.getInstance(), 350, null));
			myVector.addElement(new Command("Cài Đặt\nAuto Di Chuyển (X)", MainMod.getInstance(), 360, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		public static void ShowMenuDisplaySettings()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Background\n" + (MainMod.isPaintBackground ? "[STATUS: ON] " : "[STATUS: OFF]"), MainMod.getInstance(), 28, null));
			myVector.addElement(new Command("Giảm\nĐồ Họa\n" + (MainMod.isReduceGraphics ? "[STATUS: ON] " : "[STATUS: OFF]"), MainMod.getInstance(), 31, null));
			myVector.addElement(new Command("Thông Báo\nBoss\n" + (MainMod.isHuntingBoss ? "[STATUS: ON] " : "[STATUS: OFF]"), MainMod.getInstance(), 29, null));
			myVector.addElement(new Command("Danh Sách\nNgười Trong Map\n" + (MainMod.isShowCharsInMap ? "[STATUS: ON] " : "[STATUS: OFF]"), MainMod.getInstance(), 30, null));
			myVector.addElement(new Command("Server Chat\n" + (MainMod.serverChat ? "[STATUS: ON] " : "[STATUS: OFF]"), MainMod.getInstance(), 34, null));
			myVector.addElement(new Command("Cài Đặt\nFPS\n[" + MainMod.targetFPS + "]", MainMod.getInstance(), 37, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		public static void ShowMenuAutoJump()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Auto Nhảy (Y)\n" + (MainMod.isAutoJump ? "[STATUS: ON]" : "[STATUS: OFF]"), MainMod.getInstance(), 351, null));
			myVector.addElement(new Command("Delay Nhảy\n[" + MainMod.delayAutoJump + "ms]", MainMod.getInstance(), 352, null));
			myVector.addElement(new Command("Bước Nhảy\n[" + MainMod.distanceAutoJump + "px]", MainMod.getInstance(), 353, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		public static void ShowMenuAutoMove()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Auto Di Chuyển (X)\n" + (MainMod.isAutoMove ? "[STATUS: ON]" : "[STATUS: OFF]"), MainMod.getInstance(), 361, null));
			myVector.addElement(new Command("Delay Di Chuyển\n[" + MainMod.delayAutoMove + "ms]", MainMod.getInstance(), 362, null));
			myVector.addElement(new Command("Bước Di Chuyển\n[" + MainMod.distanceAutoMove + "px]", MainMod.getInstance(), 363, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x000045ED File Offset: 0x000027ED
		public static void smethod_0()
		{
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x000045ED File Offset: 0x000027ED
		public static void smethod_1()
		{
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x000045ED File Offset: 0x000027ED
		public static void smethod_2()
		{
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x000045ED File Offset: 0x000027ED
		public static void smethod_3()
		{
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00009276 File Offset: 0x00007476
		public static void ResetChatTextField()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
			ChatTextField.gI().parentScreen = GameScr.gI();
		}


		// Token: 0x06000BA5 RID: 2981 RVA: 0x000A9C54 File Offset: 0x000A7E54
		public static void UseItem(int templateId)
		{
			for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
			{
				Item item = global::Char.myCharz().arrItemBag[i];
				if (item != null && (int)item.template.id == templateId)
				{
					Service.gI().useItem(0, 1, (sbyte)item.indexUI, -1);
					return;
				}
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x000A53A0 File Offset: 0x000A35A0
		public static void TeleportTo(int x, int y)
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

		// Token: 0x06000BA7 RID: 2983 RVA: 0x000A343C File Offset: 0x000A163C
		public static int GetYGround(int x)
		{
			int num = 50;
			int i = 0;
			while (i < 30)
			{
				i++;
				num += 24;
				if (TileMap.tileTypeAt(x, num, 2))
				{
					if (num % 24 != 0)
					{
						num -= num % 24;
						break;
					}
					break;
				}
			}
			return num;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x000A9CAC File Offset: 0x000A7EAC
		static MainMod()
		{
			MainMod.isHuntingBoss = true;
			MainMod.listBosses = new List<Boss>();
			MainMod.LoadData();
			MainMod.listBackgroundImages = new List<Image>();
			MainMod.limitHPChar = -1;
			MainMod.inputHPChar = new string[]
			{
				"Nhập HP Char:",
				"HP"
			};
			MainMod.inputHPLimit = new string[]
			{
				"Nhập HP:",
				"HP"
			};
			MainMod.listCharIDs = new List<int>();
			MainMod.inputCharID = new string[]
			{
				"Nhập charID:",
				"charID"
			};
			MainMod.inputHPPercentFusionDance = new string[]
			{
				"Nhập %HP ",
				"%HP"
			};
			MainMod.inputHPFusionDance = new string[]
			{
				"Nhập HP",
				"HP"
			};
			MainMod.nameMapsNRD = new string[]
			{
				"Hành tinh M-2",
				"Hành tinh Polaris",
				"Hành tinh Cretaceous",
				"Hành tinh Monmaasu",
				"Hành tinh Rudeeze",
				"Hành tinh Gelbo",
				"Hành tinh Tigere"
			};
			MainMod.inputLockFocusCharID = new string[]
			{
				"Nhập charID lock",
				"charID"
			};
			MainMod.list_0 = new List<int>();
			MainMod.list_1 = new List<int>();
			MainMod.string_2 = "2.3.1 - 17/05/2023 00:00:00";
			MainMod.runSpeed = 8;
			ModSkin.Load();
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x000A9E40 File Offset: 0x000A8040
		public static void Revive()
		{
			if (global::Char.myCharz() != null && global::Char.myCharz().luong + global::Char.myCharz().luongKhoa > 0 && (global::Char.myCharz().meDead || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().cHP <= 0L) && GameCanvas.gameTick % 20 == 0)
			{
				Service.gI().wakeUpFromDead();
				global::Char.myCharz().meDead = false;
				global::Char.myCharz().statusMe = 1;
				global::Char.myCharz().cHP = global::Char.myCharz().cHPFull;
				global::Char.myCharz().cMP = global::Char.myCharz().cMPFull;
				global::Char @char = global::Char.myCharz();
				global::Char char2 = global::Char.myCharz();
				global::Char.myCharz().cp3 = 0;
				char2.cp2 = 0;
				@char.cp1 = 0;
				ServerEffect.addServerEffect(109, global::Char.myCharz(), 2);
				GameScr.gI().center = null;
				GameScr.isHaveSelectSkill = true;
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x000A9F1C File Offset: 0x000A811C
		public static void FocusTo(int charId)
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			}
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00009553 File Offset: 0x00007753
		public static bool isMeInNRDMap()
		{
			return TileMap.mapID >= 85 && TileMap.mapID <= 91;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x000045ED File Offset: 0x000027ED
		public static void smethod_4()
		{
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x000045ED File Offset: 0x000027ED
		public static void smethod_5()
		{
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0000956C File Offset: 0x0000776C
		public static bool smethod_6(int int_0)
		{
			return MainMod.list_0.Contains(int_0);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x000A9F50 File Offset: 0x000A8150
		public static void TeleportToFocus()
		{
			if (global::Char.myCharz().charFocus != null)
			{
				MainMod.TeleportTo(global::Char.myCharz().charFocus.cx, global::Char.myCharz().charFocus.cy);
				return;
			}
			if (global::Char.myCharz().itemFocus != null)
			{
				MainMod.TeleportTo(global::Char.myCharz().itemFocus.x, global::Char.myCharz().itemFocus.y);
				return;
			}
			if (global::Char.myCharz().mobFocus != null)
			{
				MainMod.TeleportTo(global::Char.myCharz().mobFocus.x, global::Char.myCharz().mobFocus.y);
				return;
			}
			if (global::Char.myCharz().npcFocus != null)
			{
				MainMod.TeleportTo(global::Char.myCharz().npcFocus.cx, global::Char.myCharz().npcFocus.cy - 3);
				return;
			}
			GameScr.info1.addInfo("Không Có Mục Tiêu!", 0);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000045ED File Offset: 0x000027ED
		public static void smethod_7()
		{
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000AA030 File Offset: 0x000A8230
		public static bool Chat(string text)
		{
			bool result;
			if (text.Equals(""))
			{
				result = false;
			}
			else if (text.Equals("copy"))
			{
				global::Char focus = global::Char.myCharz().charFocus;
				if (focus == null)
				{
					GameScr.info1.addInfo("Vui lòng chọn mục tiêu!", 0);
					return true;
				}
				if (focus.isPet || focus.isMiniPet || string.IsNullOrEmpty(focus.cName) || focus.cName.StartsWith("#") || focus.cName.StartsWith("$") || focus.cName == "Trọng tài")
				{
					GameScr.info1.addInfo("Chỉ copy người, không copy NPC/Pet!", 0);
					return true;
				}
				// Copy raw parts (head, body, leg, back) from the focus char and persist
				Mod.DungPham.KoiOctiiu957.ModSkin.ApplySkinParts(focus.headTemp >= 0 ? focus.headTemp : focus.head, focus.bodyTemp >= 0 ? focus.bodyTemp : focus.body, focus.legTemp >= 0 ? focus.legTemp : focus.leg);
				Mod.DungPham.KoiOctiiu957.ModSkin.Update();
				GameScr.info1.addInfo("Đã copy ngoại hình.", 0);
				return true;
			}
			else if (text.Equals("copy_pet"))
			{
				global::Char focus = global::Char.myCharz().charFocus;
				if (focus == null)
				{
					GameScr.info1.addInfo("Vui lòng chọn mục tiêu!", 0);
					return true;
				}
				if (focus.isPet || focus.isMiniPet || string.IsNullOrEmpty(focus.cName) || focus.cName.StartsWith("#") || focus.cName.StartsWith("$"))
				{
					GameScr.info1.addInfo("Vui lòng focus vào NGƯỜI CHƠI (chủ nhân của thú cưng)!", 0);
					return true;
				}
				global::Char targetPet = null;
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					global::Char c = (global::Char)GameScr.vCharInMap.elementAt(i);
					if (c.isMiniPet && c.cName == focus.cName)
					{
						targetPet = c;
						break;
					}
				}
				if (targetPet == null)
				{
					GameScr.info1.addInfo("Người này không có thú cưng!", 0);
					return true;
				}
				Mod.DungPham.KoiOctiiu957.ModSkin.ApplyPetSkinParts(targetPet.head, targetPet.body, targetPet.leg);
				Mod.DungPham.KoiOctiiu957.ModSkin.Update();
				GameScr.info1.addInfo("Đã copy ngoại hình thú cưng.", 0);
				return true;
			}
						else if (text.Equals("uncopy"))
			{
				Mod.DungPham.KoiOctiiu957.ModSkin.ClearCharSkinParts();
				GameScr.info1.addInfo("Đã khôi phục ngoại hình nhân vật.", 0);
				return true;
			}
			else if (text.Equals("uncopy_pet"))
			{
				Mod.DungPham.KoiOctiiu957.ModSkin.ClearPetSkinParts();
				GameScr.info1.addInfo("Đã khôi phục ngoại hình thú cưng.", 0);
				return true;
			}
			else if (text.StartsWith("k_"))
			{
				try
				{
					int zoneId = int.Parse(text.Split(new char[]
					{
						'_'
					})[1]);
					Service.gI().requestChangeZone(zoneId, -1);
				}
				catch
				{
				}
				result = true;
			}
			else if (text.StartsWith("s_"))
			{
				try
				{
					int num = MainMod.runSpeed = int.Parse(text.Split(new char[]
					{
						'_'
					})[1]);
					GameScr.info1.addInfo("Tốc Độ Di Chuyển: " + num.ToString(), 0);
				}
				catch
				{
				}
				result = true;
			}
			else if (text.Equals("bdkb"))
			{
				Service.gI().confirmMenu(13, 0);
				result = true;
			}
			else if (text.StartsWith("delay_"))
			{
				try
				{
					MainMod.delay = int.Parse(text.Split(new char[]
					{
						'_'
					})[1]);
				}
				catch
				{
				}
				result = true;
			}
			else if (text.StartsWith("nrd_"))
			{
				try
				{
					int num2 = int.Parse(text.Split(new char[]
					{
						'_'
					})[1]);
					int num3 = int.Parse(text.Split(new char[]
					{
						'_'
					})[2]);
					MainMod.mapIdNRD = num2;
					MainMod.zoneIdNRD = num3;
					GameScr.info1.addInfo("NRD: " + MainMod.mapIdNRD.ToString() + "sk" + MainMod.zoneIdNRD.ToString(), 0);
				}
				catch
				{
				}
				result = true;
			}
			else if (text.StartsWith("cheat_"))
			{
				try
				{
					float num4 = Time.timeScale = float.Parse(text.Split(new char[]
					{
						'_'
					})[1]);
					GameScr.info1.addInfo("Cheat: " + num4.ToString(), 0);
				}
				catch
				{
				}
				result = true;
			}
			else if (text.StartsWith("cheatf_"))
			{
				try
				{
					float num5 = Time.timeScale = float.Parse(text.Split(new char[]
					{
						'_'
					})[1]) / 10f;
					GameScr.info1.addInfo("Cheat: " + num5.ToString(), 0);
				}
				catch
				{
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000045ED File Offset: 0x000027ED
		public static void smethod_8()
		{
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00009489 File Offset: 0x00007689
		public static int MyHPPercent()
		{
			return (int)(global::Char.myCharz().cHP * 100L / global::Char.myCharz().cHPFull);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00009579 File Offset: 0x00007779
		public static bool isMyHPLowerThan(int percent)
		{
			return global::Char.myCharz().cHP > 0L && MainMod.MyHPPercent() < percent;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x000045ED File Offset: 0x000027ED
		public static void smethod_9()
		{
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000045ED File Offset: 0x000027ED
		public static void smethod_10()
		{
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000045ED File Offset: 0x000027ED
		public static void smethod_11()
		{
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00009593 File Offset: 0x00007793
		public static int smethod_12()
		{
			return 2000000000;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x000AA2B8 File Offset: 0x000A84B8
		public static string GetFlagName(int flagId)
		{
			string result;
			if (flagId != -1 && flagId != 0)
			{
				string text = "";
				switch (flagId)
				{
				case 1:
					text = "Cờ xanh";
					break;
				case 2:
					text = "Cờ đỏ";
					break;
				case 3:
					text = "Cờ tím";
					break;
				case 4:
					text = "Cờ vàng";
					break;
				case 5:
					text = "Cờ lục";
					break;
				case 6:
					text = "Cờ hồng";
					break;
				case 7:
					text = "Cờ cam";
					break;
				case 8:
					text = "Cờ đen";
					break;
				case 9:
					text = "Cờ Kaio";
					break;
				case 10:
					text = "Cờ Mabu";
					break;
				case 11:
					text = "Cờ xanh dương";
					break;
				}
				if (!text.Equals(""))
				{
					result = "(" + text + ") ";
				}
				else
				{
					result = text;
				}
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x000AA390 File Offset: 0x000A8590
		public static void LoadData()
		{
			MainMod.LoadFlagColor();

			MainMod.isHuntingBoss = (Rms.loadRMSInt("sanboss") == 1);
			MainMod.isPaintBackground = (Rms.loadRMSInt("isPaintBgr") == 1);
			MainMod.isShowCharsInMap = (Rms.loadRMSInt("showchar") == 1);
			MainMod.isReduceGraphics = (Rms.loadRMSInt("IsReduceGraphics") == 1);
			MainMod.serverChat = (Rms.loadRMSInt("serverChat") != 0); // Mặc định là ON, chỉ OFF khi RMS = 0
			MainMod.isAutoLogin = false; // Forced disabled on startup
			MainMod.isAutoJump = (Rms.loadRMSInt("isAutoJump") == 1);
			MainMod.delayAutoJump = Rms.loadRMSInt("delayAutoJump");
			if (MainMod.delayAutoJump <= 0) MainMod.delayAutoJump = 1000;
			MainMod.distanceAutoJump = Rms.loadRMSInt("distanceAutoJump");
			if (MainMod.distanceAutoJump <= 0) MainMod.distanceAutoJump = 50;
			MainMod.isAutoMove = false; // Forced disabled on startup
			MainMod.delayAutoMove = Rms.loadRMSInt("delayAutoMove");
			if (MainMod.delayAutoMove <= 0) MainMod.delayAutoMove = 1000;
			MainMod.distanceAutoMove = Rms.loadRMSInt("distanceAutoMove");
			if (MainMod.distanceAutoMove <= 0) MainMod.distanceAutoMove = 50;
			
			int savedFps = Rms.loadRMSInt("targetFPS");
			if (savedFps >= 5) {
				MainMod.targetFPS = savedFps;
			}
			UnityEngine.Application.targetFrameRate = MainMod.targetFPS;
			try
			{
				MainMod.APIKey = File.ReadAllText("Data\\keyAPI.ini");
				MainMod.APIServer = File.ReadAllText("Data\\serverAPI.ini");
			}
			catch
			{
			}
			new Thread(delegate()
			{
			}).Start();
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x000AA474 File Offset: 0x000A8674
		public static void LoadFlagColor()
		{
			MainMod.listFlagColor.Add(Color.black);
			MainMod.listFlagColor.Add(new Color(0f, 0.99609375f, 0.99609375f));
			MainMod.listFlagColor.Add(Color.red);
			MainMod.listFlagColor.Add(new Color(0.54296875f, 0f, 0.54296875f));
			MainMod.listFlagColor.Add(Color.yellow);
			MainMod.listFlagColor.Add(Color.green);
			MainMod.listFlagColor.Add(new Color(0.99609375f, 0.51171875f, 0.9765625f));
			MainMod.listFlagColor.Add(new Color(0.80078125f, 0.3984375f, 0f));
			MainMod.listFlagColor.Add(Color.gray);
			MainMod.listFlagColor.Add(Color.blue);
			MainMod.listFlagColor.Add(Color.red);
			MainMod.listFlagColor.Add(Color.blue);
			MainMod.listFlagColor.Add(Color.white);
			MainMod.listFlagColor.Add(Color.black);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x000AA590 File Offset: 0x000A8790
		public static void ConnectToAccountManager()
		{
			string path = Path.GetTempPath() + "koi occtiu957\\mod 222\\auto";
			if (mSystem.currentTimeMillis() - MainMod.lastTimeConnected >= 3500L && File.Exists(path))
			{
				string text = File.ReadAllText(path);
				new Thread(delegate()
				{
					MainMod.UseSkill(int.Parse(text));
				}).Start();
				MainMod.lastTimeConnected = mSystem.currentTimeMillis();
			}
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x000AA5F8 File Offset: 0x000A87F8
		public static bool isColdImmune(Item item)
		{
			int id = (int)item.template.id;
			return id == 450 || id == 630 || id == 631 || id == 632 || id == 878 || id == 879 || (id >= 386 && id <= 394);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x000AA658 File Offset: 0x000A8858
		public static void UseCapsule()
		{
			MainMod.isUsingCapsule = true;
			for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
			{
				Item item = global::Char.myCharz().arrItemBag[i];
				if (item != null && (item.template.id == 194 || item.template.id == 193))
				{
					Service.gI().useItem(0, 1, (sbyte)item.indexUI, -1);
					break;
				}
			}
			Thread.Sleep(500);
			Service.gI().requestMapSelect(0);
			Thread.Sleep(MainMod.delayAutoJump);
			MainMod.isUsingCapsule = false;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000AA6F4 File Offset: 0x000A88F4
		public static void UseSkill(int skillIndex)
		{
			if (!MainMod.isUsingSkill)
			{
				MainMod.isUsingSkill = true;
				if (global::Char.myCharz().myskill != GameScr.keySkill[skillIndex])
				{
					GameScr.gI().doSelectSkill(GameScr.keySkill[skillIndex], true);
					Thread.Sleep(200);
					GameScr.gI().doSelectSkill(GameScr.keySkill[skillIndex], true);
					MainMod.isUsingSkill = false;
					return;
				}
				GameScr.gI().doSelectSkill(GameScr.keySkill[skillIndex], true);
				MainMod.isUsingSkill = false;
			}
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000045ED File Offset: 0x000027ED
		public static void Login()
		{
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000AA770 File Offset: 0x000A8970
		public static void SolveCapcha()
		{
			MainMod.isSlovingCapcha = true;
			Thread.Sleep(MainMod.delayAutoJump);
			try
			{
				WebClient webClient = new WebClient();
				NameValueCollection nameValueCollection = new NameValueCollection();
				nameValueCollection["merchant_key"] = MainMod.APIKey;
				nameValueCollection["type"] = "19";
				nameValueCollection["image"] = Convert.ToBase64String(GameScr.imgCapcha.texture.EncodeToPNG());
				NameValueCollection data = nameValueCollection;
				Thread.Sleep(500);
				byte[] bytes = webClient.UploadValues(MainMod.APIServer, data);
				string @string = Encoding.Default.GetString(bytes);
				Thread.Sleep(500);
				if (@string.Contains("\"message\":\"success\"") && @string.Contains("\"success\":true"))
				{
					string text = @string.Split(new char[]
					{
						':'
					})[3].Split(new char[]
					{
						'"'
					})[1].Trim();
					Thread.Sleep(500);
					if (text.Length >= 4 && text.Length <= 7)
					{
						for (int i = 0; i < text.Length; i++)
						{
							Service.gI().mobCapcha(text[i]);
							Thread.Sleep(Res.random(500, 700));
						}
						Thread.Sleep(MainMod.delayAutoMove);
					}
				}
			}
			catch
			{
				Thread.Sleep(MainMod.delayAutoMove);
			}
			Thread.Sleep(MainMod.delayAutoJump);
			if (GameScr.gI().mobCapcha != null)
			{
				Thread.Sleep(MainMod.delayAutoMove);
			}
			MainMod.isSlovingCapcha = false;
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x000045ED File Offset: 0x000027ED
		public static void smethod_13()
		{
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x000A47A8 File Offset: 0x000A29A8
		public static bool isMeHasEnoughMP(Skill skillToUse)
		{
			bool result;
			if (skillToUse.template.manaUseType == 2)
			{
				result = true;
			}
			else if (skillToUse.template.manaUseType != 1)
			{
				result = (global::Char.myCharz().cMP >= (long)skillToUse.manaUse);
			}
			else
			{
				result = (global::Char.myCharz().cMP >= (long)skillToUse.manaUse * global::Char.myCharz().cMPFull / 100L);
			}
			return result;
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x000045ED File Offset: 0x000027ED
		public static void smethod_14()
		{
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x000045ED File Offset: 0x000027ED
		public static void smethod_16()
		{
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x000AA900 File Offset: 0x000A8B00
		public static string DecryptString(string str, string key)
		{
			byte[] array = Convert.FromBase64String(str);
			byte[] key2 = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(key));
			byte[] bytes = new TripleDESCryptoServiceProvider
			{
				Key = key2,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			}.CreateDecryptor().TransformFinalBlock(array, 0, array.Length);
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x000AA960 File Offset: 0x000A8B60
		public static bool isBoss(global::Char ch)
		{
			return ch.cName != null && ch.cName != "" && !ch.isPet && !ch.isMiniPet && char.IsUpper(char.Parse(ch.cName.Substring(0, 1))) && ch.cName != "Trọng tài" && !ch.cName.StartsWith("#") && !ch.cName.StartsWith("$");
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x000AA9EC File Offset: 0x000A8BEC
		public static void paintListCharsInMap(mGraphics g)
		{
			int num = MainMod.isMeInNRDMap() ? 35 : 95;
			MainMod.widthRect = 142;
			MainMod.heightRect = 9;
			global::Char @char = global::Char.myCharz();
			global::Char charFocus = @char.charFocus;
			int num2 = GameCanvas.w - MainMod.widthRect;
			MainMod.infoStartY = 62;
			for (int j = 0; j < MainMod.listCharsInMap.Count; j++)
			{
				global::Char c = MainMod.listCharsInMap[j];
				if (c != null && c.isNRD && !string.IsNullOrEmpty(c.cName) && !c.isPet && !c.isMiniPet && !c.cName.StartsWith("#") && !c.cName.StartsWith("$") && !(c.cName == "Trọng tài"))
				{
					MainMod.paintCharInfo(g, c);
				}
			}
			if (charFocus != null && !charFocus.isNRD && !string.IsNullOrEmpty(charFocus.cName) && !charFocus.isPet && !charFocus.isMiniPet && !charFocus.cName.StartsWith("#") && !charFocus.cName.StartsWith("$") && !(charFocus.cName == "Trọng tài"))
			{
				MainMod.paintCharInfo(g, charFocus);
			}
			for (int i = 0; i < MainMod.listCharsInMap.Count; i++)
			{
				global::Char char2 = MainMod.listCharsInMap[i];
				g.setColor(2721889, 0.5f);
				g.fillRect(num2, num + 2, MainMod.widthRect - 2, MainMod.heightRect);
				MainMod.paintPKFlag(g, char2, num);
				if (!string.IsNullOrEmpty(char2.cName) && !char2.isPet && !char2.isMiniPet && !char2.cName.StartsWith("#") && !char2.cName.StartsWith("$") && !(char2.cName == "Trọng tài"))
				{
					bool flag = MainMod.isBoss(char2);
					string arg = flag ? ("BOT: " + char2.cName + " [" + NinjaUtil.getMoneys(char2.cHP) + "]") : (char2.getGender() + ": " + char2.cName + " [" + NinjaUtil.getMoneys(char2.cHP) + "]");
					int x = num2 + 2;
					if (charFocus != null && charFocus == char2)
					{
						g.setColor(14155776);
						g.drawLine(@char.cx - GameScr.cmx, @char.cy - GameScr.cmy + 1, char2.cx - GameScr.cmx, char2.cy - GameScr.cmy);
						mFont.tahoma_7b_red.drawString(g, i + 1 + ". " + arg, x, num, 0);
					}
					else if (flag)
					{
						g.setColor(16383818);
						g.drawLine(@char.cx - GameScr.cmx, @char.cy - GameScr.cmy + 1, char2.cx - GameScr.cmx, char2.cy - GameScr.cmy);
						mFont.tahoma_7_red.drawString(g, i + 1 + ". " + arg, x, num, 0);
					}
					else if (char2.cHPFull > 100000000L && char2.cHP > 0L && MainMod.isMeInNRDMap() && !char2.isNRD)
					{
						mFont.tahoma_7b_red.drawString(g, i + 1 + ". " + arg, x, num, 0);
					}
					else if (char2.isNRD)
					{
						mFont.tahoma_7b_red.drawString(g, i + 1 + ". " + arg, x, num, 0);
					}
					else
					{
						mFont.tahoma_7.drawString(g, i + 1 + ". " + arg, x, num, 0);
					}
					num += MainMod.heightRect + 1;
				}
			}
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0000959A File Offset: 0x0000779A
		public static MyVector GetMyVectorMe()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(global::Char.myCharz());
			return myVector;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000AACFC File Offset: 0x000A8EFC
		public static bool canBuffMe(out Skill skillBuff)
		{
			skillBuff = global::Char.myCharz().getSkill(new SkillTemplate
			{
				id = MainMod.ID_SKILL_BUFF
			});
			bool result;
			if (skillBuff == null)
			{
				GameScr.info1.addInfo("Không tìm thấy skill Trị Thương", 0);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x000AAD40 File Offset: 0x000A8F40
		public static void buffMe()
		{
			Skill skill;
			if (MainMod.canBuffMe(out skill))
			{
				Service.gI().selectSkill((int)MainMod.ID_SKILL_BUFF);
				Service.gI().sendPlayerAttack(new MyVector(), MainMod.GetMyVectorMe(), -1);
				Service.gI().selectSkill((int)global::Char.myCharz().myskill.template.id);
				skill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
			}
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x000AADA4 File Offset: 0x000A8FA4
		public static void AttackMyself()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(global::Char.myCharz());
			Service.gI().sendPlayerAttack(new MyVector(), myVector, -1);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x000AADD4 File Offset: 0x000A8FD4
		public static void LockFocus()
		{
			if (!MainMod.isGMT)
			{
				return;
			}
			if (MainMod.isgmtchar)
			{
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
					if (@char.charID == MainMod.IDCharGMT)
					{
						global::Char.myCharz().charFocus = @char;
						global::Char.myCharz().mobFocus = null;
					}
				}
			}
			if (!MainMod.isgmtmob)
			{
				return;
			}
			for (int j = 0; j < GameScr.vMob.size(); j++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(j);
				if (mob.mobId == MainMod.IDMObGMT)
				{
					global::Char.myCharz().charFocus = null;
					global::Char.myCharz().mobFocus = mob;
				}
			}
		}

		public static void AutoJumpThread()
		{
			while (MainMod.isAutoJump)
			{
				try
				{
					if (global::Char.myCharz() != null)
					{
						// 1. Nhảy lên khoảng cách distanceAutoJump
						global::Char.myCharz().currentMovePoint = new MovePoint(MainMod.originJumpX, MainMod.originJumpY - MainMod.distanceAutoJump);
						Thread.Sleep(MainMod.delayAutoJump);
						if (!MainMod.isAutoJump) break;

						// 2. Rơi về lại đúng tọa độ gốc ban đầu
						global::Char.myCharz().currentMovePoint = new MovePoint(MainMod.originJumpX, MainMod.originJumpY);
						Thread.Sleep(MainMod.delayAutoJump);
					}
					else
					{
						Thread.Sleep(500);
					}
				}
				catch
				{
				}
			}
		}

		public static void AutoMoveThread()
		{
			while (MainMod.isAutoMove)
			{
				try
				{
					if (global::Char.myCharz() != null)
					{
						// 1. Di chuyển sang phải khoảng cách distanceAutoMove
						global::Char.myCharz().currentMovePoint = new MovePoint(MainMod.originMoveX + MainMod.distanceAutoMove, MainMod.originMoveY);
						Thread.Sleep(MainMod.delayAutoMove);
						if (!MainMod.isAutoMove) break;

						// 2. Quay về lại đúng tọa độ gốc ban đầu
						global::Char.myCharz().currentMovePoint = new MovePoint(MainMod.originMoveX, MainMod.originMoveY);
						Thread.Sleep(MainMod.delayAutoMove);
					}
					else
					{
						Thread.Sleep(500);
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000095AC File Offset: 0x000077AC
		public static void paintPKFlag(mGraphics g, global::Char ch, int rowTopY)
		{
			if (ch.cFlag == 0 || ch.cFlag == -1)
			{
				return;
			}
			int num = GameCanvas.w - MainMod.widthRect - 8;
			int num2 = rowTopY + 2 + (MainMod.heightRect - 7 >> 1);
			MainMod.drawFlagSquare(g, (int)ch.cFlag, num, num2);
		}

			public static void drawFlagSquare(mGraphics g, int cFlag, int x, int y)
		{
			if (cFlag != 0)
			{
				if (cFlag < 1 || cFlag > MainMod.listFlagColor.Count)
				{
					Cout.println("Invalid cFlag value: " + cFlag.ToString() + ", using default color");
					cFlag = 1;
				}
				Color color = MainMod.listFlagColor[cFlag];
				int num = (int)(color.r * 255f);
				int num2 = (int)(color.g * 255f);
				int num3 = (int)(color.b * 255f);
				int num4 = (num << 16) | (num2 << 8) | num3;
				float num5 = 0.7f;
				g.setColor(num4, num5);
				g.fillRect(x, y, 7, 7);
			}
		}
		// Token: 0x04001645 RID: 5701
		public static MainMod _Instance;

		// Token: 0x04001646 RID: 5702
		public static int int_0;

		// Token: 0x04001647 RID: 5703
		public static string account = "";

		// Token: 0x04001648 RID: 5704
		public static string password;

		// Token: 0x04001649 RID: 5705
		public static int int_1;

		// Token: 0x0400164A RID: 5706
		public static int int_2;

		// Token: 0x0400164B RID: 5707
		public static List<int> list_0;

		// Token: 0x0400164C RID: 5708
		public static List<int> list_1;

		// Token: 0x0400164D RID: 5709
		public static string string_2;

		// Token: 0x0400164E RID: 5710
		public static int runSpeed;

		// Token: 0x0400164F RID: 5711
		public static bool isAutoRevive;

		// Token: 0x04001650 RID: 5712
		public static bool isLockFocus;

		// Token: 0x04001651 RID: 5713
		public static int charIDLock;

		// Token: 0x04001652 RID: 5714
		public static string[] inputLockFocusCharID;

		// Token: 0x04001653 RID: 5715
		public static bool isConnectToAccountManager;

		// Token: 0x04001654 RID: 5716
		public static int zoneIdNRD;

		// Token: 0x04001655 RID: 5717
		public static int mapIdNRD;

		// Token: 0x04001656 RID: 5718
		public static bool isOpenMenuNPC;

		// Token: 0x04001657 RID: 5719
		public static bool isAutoEnterNRDMap;

		// Token: 0x04001658 RID: 5720
		public static string[] nameMapsNRD;

		// Token: 0x04001659 RID: 5721
		public static int int_4;

		// Token: 0x0400165A RID: 5722
		public static bool bool_1;

		// Token: 0x0400165B RID: 5723
		public static string[] inputHPPercentFusionDance;

		// Token: 0x0400165C RID: 5724
		public static string[] inputHPFusionDance;

		// Token: 0x0400165D RID: 5725
		public static int minumumHPPercentFusionDance;

		// Token: 0x0400165E RID: 5726
		public static bool isPaintBackground;

		// Token: 0x0400165F RID: 5727
		public static int minimumHPFusionDance;

		// Token: 0x04001660 RID: 5728
		public static long long_0;

		// Token: 0x04001661 RID: 5729
		public static List<int> listCharIDs;

		// Token: 0x04001662 RID: 5730
		public static string[] inputCharID;

		// Token: 0x04001663 RID: 5731
		public static bool isAutoLockControl;

		// Token: 0x04001664 RID: 5732
		public static bool isAutoTeleport;

		// Token: 0x04001665 RID: 5733
		public static long long_1;

		// Token: 0x04001666 RID: 5734
		public static long long_2;

		// Token: 0x04001667 RID: 5735
		public static bool isAutoAttackBoss;

		// Token: 0x04001668 RID: 5736
		public static int HPLimit;

		// Token: 0x04001669 RID: 5737
		public static string[] inputHPLimit;

		// Token: 0x0400166A RID: 5738
		public static long long_3;

		// Token: 0x0400166B RID: 5739
		public static bool isAutoAttackOtherChars;

		// Token: 0x0400166C RID: 5740
		public static int limitHPChar;

		// Token: 0x0400166D RID: 5741
		public static long long_4;

		// Token: 0x0400166E RID: 5742
		public static string[] inputHPChar;

		// Token: 0x0400166F RID: 5743
		public static bool isHuntingBoss;

		// Token: 0x04001670 RID: 5744
		public static List<Boss> listBosses;

		// Token: 0x04001671 RID: 5745
		public static Image logoServerListScreen;

		// Token: 0x04001672 RID: 5746
		public static Image logoGameScreen;

		// Token: 0x04001673 RID: 5747
		public static List<Image> listBackgroundImages;

		// Token: 0x04001674 RID: 5748
		public static List<Color> listFlagColor = new List<Color>();

		// Token: 0x04001675 RID: 5749
		public static int widthRect;

		// Token: 0x04001676 RID: 5750
		public static int heightRect;

		// Token: 0x04001677 RID: 5751
		public static List<global::Char> listCharsInMap = new List<global::Char>();

		// Token: 0x04001678 RID: 5752
		public static bool isShowCharsInMap = true;

		// Token: 0x04001679 RID: 5753
		public static string string_0 = "YT: DragonMoi";

		// Token: 0x0400167A RID: 5754
		public static bool isReduceGraphics = false;

		// Token: 0x0400167B RID: 5755
		public static bool isUsingSkill;

		// Token: 0x0400167C RID: 5756
		public static long lastTimeConnected;

		// Token: 0x0400167D RID: 5757
		public static bool isUsingCapsule;

		// Token: 0x0400167E RID: 5758
		public static string string_1;

		// Token: 0x0400167F RID: 5759
		public static int delay;

		// Token: 0x04001680 RID: 5760
		public static Image image_0;

		// Token: 0x04001681 RID: 5761
		public static int indexBackgroundImages;

		// Token: 0x04001682 RID: 5762
		public static long lastTimeChangeBackground;

		// Token: 0x04001683 RID: 5763
		public static string string_4;

		// Token: 0x04001684 RID: 5764
		public static string string_3;

		// Token: 0x04001685 RID: 5765
		public static int server;

		// Token: 0x04001686 RID: 5766
		public static string APIKey;

		// Token: 0x04001687 RID: 5767
		public static string APIServer;

		// Token: 0x04001688 RID: 5768
		public static bool isSlovingCapcha;

		// Token: 0x04001689 RID: 5769
		public static int int_3;

		// Token: 0x0400168A RID: 5770
		public static long long_5;

		// Token: 0x0400168B RID: 5771
		public static long long_6;

		// Token: 0x0400168C RID: 5772
		public static bool bool_0;

		// Token: 0x0400168D RID: 5773
		public static long long_7;

		// Token: 0x0400168E RID: 5774
		public static bool isAutoT77;

		// Token: 0x0400168F RID: 5775
		public static bool isAutoBomPicPoc;

		// Token: 0x04001690 RID: 5776
		public static sbyte ID_SKILL_BUFF = 7;

		// Token: 0x04001691 RID: 5777
		public static bool hs;

		// Token: 0x04001692 RID: 5778
		public static bool isUDTvaAntiMatsong;

		// Token: 0x04001693 RID: 5779
		public static long timeKillMe;

		// Token: 0x04001694 RID: 5780
		public static bool isGMT;

		// Token: 0x04001695 RID: 5781
		public static int IDCharGMT;

		// Token: 0x04001696 RID: 5782
		public static int IDMObGMT;

		// Token: 0x04001697 RID: 5783
		public static bool isgmtchar;

		// Token: 0x04001698 RID: 5784
		public static bool isgmtmob;

		// Token: 0x04001699 RID: 5785
		// Auto Nhảy (Y)
		public static bool isAutoJump = false;
		public static int originJumpX;
		public static int originJumpY;
		public static int distanceAutoJump = 50;
		public static int delayAutoJump = 1000;
		public static string[] inputDelayJump = new string[] { "Nhập Delay Nhảy (ms)", "Delay" };
		public static string[] inputDistanceJump = new string[] { "Nhập Khoảng Cách Nhảy (px)", "Khoảng cách" };

		// Auto Di Chuyển (X)
		public static bool isAutoMove = false;
		public static int originMoveX;
		public static int originMoveY;
		public static int distanceAutoMove = 50;
		public static int delayAutoMove = 3000;
		public static string[] inputDelayMove = new string[] { "Nhập Delay Di Chuyển (ms)", "Delay" };
		public static string[] inputDistanceMove = new string[] { "Nhập Khoảng Cách Di Chuyển (px)", "Khoảng cách" };

		public static int targetFPS = 60;

		public static int ping;
		public static string serverHost = "";
		public static int serverPort = 14445;
		public static bool isPingThreadRunning = false;

		public static void StartPingThread()
		{
			if (MainMod.isPingThreadRunning) return;
			MainMod.isPingThreadRunning = true;
			new Thread(() => {
				while (true)
				{
					try
					{
						if (!string.IsNullOrEmpty(MainMod.serverHost) && Session_ME.connected)
						{
							Stopwatch sw = new Stopwatch();
							sw.Start();
							using (TcpClient client = new TcpClient())
							{
								var result = client.BeginConnect(MainMod.serverHost, MainMod.serverPort, null, null);
								bool success = result.AsyncWaitHandle.WaitOne(1000);
								if (success)
								{
									client.EndConnect(result);
									sw.Stop();
									MainMod.ping = (int)sw.ElapsedMilliseconds;
								}
								else
								{
									MainMod.ping = -1;
								}
							}
						}
					}
					catch {
						MainMod.ping = -2;
					}
					Thread.Sleep(2000);
				}
			}).Start();
		}
		public static string[] inputFPS = new string[] { "Nhập mức FPS mong muốn", "FPS" };

		// Token: 0x0400169D RID: 5789
		public static bool serverChat = true;
		public static bool isAutoLogin;
		public static bool isDisconnecting;
		public static long timeDisconnect;
		public static void UpdateGlobal()
		{
			if (isAutoLogin && isDisconnecting)
			{
				if (mSystem.currentTimeMillis() - timeDisconnect > 5000L)
				{
					timeDisconnect = mSystem.currentTimeMillis();
					if (GameCanvas.currentScreen == GameCanvas.serverScreen)
					{
						GameCanvas.endDlg();
						GameCanvas.serverScreen.selectServer();
					}
					else if (GameCanvas.currentScreen == GameCanvas.loginScr)
					{
						GameCanvas.endDlg();
						GameCanvas.loginScr.doLogin();
						isDisconnecting = false;
					}
				}
			}
		}

		public static void updateCharEff(global::Char c)
		{
			if (c == null)
			{
				return;
			}
			if (c.protectEff)
			{
				if (!dictTimeShield.ContainsKey(c.charID)) dictTimeShield[c.charID] = mSystem.currentTimeMillis();
			}
			else if (dictTimeShield.ContainsKey(c.charID)) dictTimeShield.Remove(c.charID);

			if (c.holdEffID != 0)
			{
				if (!dictTimeTie.ContainsKey(c.charID)) dictTimeTie[c.charID] = mSystem.currentTimeMillis();
			}
			else if (dictTimeTie.ContainsKey(c.charID)) dictTimeTie.Remove(c.charID);

			if (c.sleepEff)
			{
				if (!dictTimeSleep.ContainsKey(c.charID)) dictTimeSleep[c.charID] = mSystem.currentTimeMillis();
			}
			else if (dictTimeSleep.ContainsKey(c.charID)) dictTimeSleep.Remove(c.charID);

			if ((int)c.isMonkey == 1)
			{
				if (!dictTimeMonkey.ContainsKey(c.charID)) dictTimeMonkey[c.charID] = mSystem.currentTimeMillis();
			}
			else if (dictTimeMonkey.ContainsKey(c.charID)) dictTimeMonkey.Remove(c.charID);
			
			if (c.huytSao)
			{
				if (!dictTimeWhistle.ContainsKey(c.charID)) dictTimeWhistle[c.charID] = mSystem.currentTimeMillis();
			}
			else if (dictTimeWhistle.ContainsKey(c.charID)) dictTimeWhistle.Remove(c.charID);

			if (c.stone)
			{
				if (!dictTimeStone.ContainsKey(c.charID)) dictTimeStone[c.charID] = mSystem.currentTimeMillis();
			}
			else if (dictTimeStone.ContainsKey(c.charID)) dictTimeStone.Remove(c.charID);
		}

		public static void clearMapData()
		{
			try
			{
				dictTimeShield.Clear();
				dictTimeTie.Clear();
				dictTimeSleep.Clear();
				dictTimeMonkey.Clear();
				dictTimeWhistle.Clear();
				dictTimeStone.Clear();
				listCharsInMap.Clear();
			}
			catch { }
		}

		// Token: 0x0400169E RID: 5790
		public static bool isLockMap = false;
		public static Dictionary<int, long> dictTimeShield = new Dictionary<int, long>();
		public static Dictionary<int, long> dictTimeTie = new Dictionary<int, long>();
		public static Dictionary<int, long> dictTimeSleep = new Dictionary<int, long>();
		public static Dictionary<int, long> dictTimeMonkey = new Dictionary<int, long>();
		public static Dictionary<int, long> dictTimeWhistle = new Dictionary<int, long>();
		public static Dictionary<int, long> dictTimeStone = new Dictionary<int, long>();
		public static int infoStartX = 0;
		public static int infoStartY = 62;

	}
}
