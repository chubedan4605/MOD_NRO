using System;
using System.Collections.Generic;

namespace Mod.DungPham.KoiOctiiu957
{
	// Token: 0x020000FD RID: 253
	public class AutoSkill : IActionListener, IChatable
	{
		// Token: 0x06000B16 RID: 2838 RVA: 0x0000925E File Offset: 0x0000745E
		public static AutoSkill getInstance()
		{
			if (AutoSkill._Instance == null)
			{
				AutoSkill._Instance = new AutoSkill();
			}
			return AutoSkill._Instance;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000A3A1C File Offset: 0x000A1C1C
		public static void Update()
		{
			if (AutoSkill.isAutoReviveSelf)
			{
				AutoSkill.AutoReviveSelf();
			}
			if (AutoSkill.isAutoSendAttack)
			{
				AutoSkill.AutoSendAttack();
			}
			if (AutoSkill.isTrainPet)
			{
				AutoSkill.AutoSkillForPet();
			}
			if (!global::Char.myCharz().meDead)
			{
				for (int i = 0; i < GameScr.keySkill.Length; i++)
				{
					if (AutoSkill.isAutoUseSkills[i])
					{
						AutoSkill.AutoUseSkill(i);
					}
				}
			}
			if (AutoSkill.isLoadKeySkill && GameCanvas.gameTick % 20 == 0)
			{
				AutoSkill.isLoadKeySkill = false;
				AutoSkill.LoadKeySkills();
			}
			if (AutoSkill.isAutoChangeFocus)
			{
				AutoSkill.AutoChangeFocus();
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x000A3A98 File Offset: 0x000A1C98
		public void onChatFromMe(string text, string to)
		{
			if (ChatTextField.gI().tfChat.getText() != null && !ChatTextField.gI().tfChat.getText().Equals(string.Empty) && !text.Equals(string.Empty) && text != null)
			{
				if (ChatTextField.gI().strChat.Equals(AutoSkill.inputDelay[0]))
				{
					try
					{
						long num = long.Parse(ChatTextField.gI().tfChat.getText());
						AutoSkill.timeAutoSkills[AutoSkill.indexSkillAuto] = num;
						AutoSkill.isAutoUseSkills[AutoSkill.indexSkillAuto] = true;
						AutoSkill.lastTimeUseSkill[AutoSkill.indexSkillAuto] = mSystem.currentTimeMillis() - 999999999L;
						GameScr.info1.addInfo(string.Concat(new string[]
						{
							"Auto ",
							GameScr.keySkill[AutoSkill.indexSkillAuto].template.name,
							": ",
							NinjaUtil.getMoneys(num),
							" mili giây"
						}), 0);
					}
					catch
					{
						GameScr.info1.addInfo("Vui Lòng Nhập Lại Delay!", 0);
					}
					AutoSkill.ResetChatTextField();
					return;
				}
			}
			else
			{
				ChatTextField.gI().isShow = false;
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x000045ED File Offset: 0x000027ED
		public void onCancelChat()
		{
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x000A3BBC File Offset: 0x000A1DBC
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 1:
				AutoSkill.isAutoSendAttack = !AutoSkill.isAutoSendAttack;
				GameScr.info1.addInfo("Tự Đánh\n" + (AutoSkill.isAutoSendAttack ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				if (AutoSkill.isAutoSendAttack)
				{
					AutoSkill.isAutoChangeFocus = false;
					return;
				}
				break;
			case 2:
				AutoSkill.isTrainPet = !AutoSkill.isTrainPet;
				GameScr.info1.addInfo("Đánh Khi Đệ Cần\n" + (AutoSkill.isTrainPet ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 3:
			{
				MyVector myVector = new MyVector();
				for (int i = 0; i < GameScr.keySkill.Length; i++)
				{
					myVector.addElement(new Command(((GameScr.keySkill[i] != null) ? GameScr.keySkill[i].template.name : "null") + "\n[" + (i + 1).ToString() + "]\n", AutoSkill.getInstance(), 10, i));
				}
				GameCanvas.menu.startAt(myVector, 3);
				return;
			}
			case 4:
				AutoSkill.isAutoShield = !AutoSkill.isAutoShield;
				GameScr.info1.addInfo("Auto Khiên Pro\n" + (AutoSkill.isAutoShield ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 5:
				AutoSkill.isSaveData = true;
				Rms.saveRMSInt("AutoSkillIsSaveRms", 1);
				GameScr.gI().saveKeySkillToRMS();
				GameScr.gI().saveonScreenSkillToRMS();
				GameScr.info1.addInfo("Đã Lưu Thứ Tự Skill Hiện Tại!", 0);
				return;
			case 6:
				AutoSkill.isAutoChangeFocus = !AutoSkill.isAutoChangeFocus;
				GameScr.info1.addInfo("Đánh Chuyển Mục Tiêu\n" + (AutoSkill.isAutoChangeFocus ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				if (AutoSkill.isAutoChangeFocus)
				{
					AutoSkill.isAutoSendAttack = false;
					return;
				}
				break;
			case 7:
				AutoSkill.listTargetAutoChangeFocus.Clear();
				GameScr.info1.addInfo("Đã Xóa Danh Sách Đánh Chuyển Mục Tiêu!", 0);
				return;
			case 8:
				if (global::Char.myCharz().charFocus != null)
				{
					AutoSkill.listTargetAutoChangeFocus.Remove(global::Char.myCharz().charFocus);
					GameScr.info1.addInfo(string.Concat(new string[]
					{
						"Đã Xóa ",
						global::Char.myCharz().charFocus.cName,
						" [",
						global::Char.myCharz().charFocus.charID.ToString(),
						"]"
					}), 0);
					return;
				}
				break;
			case 9:
				if (global::Char.myCharz().charFocus != null)
				{
					AutoSkill.listTargetAutoChangeFocus.Add(global::Char.myCharz().charFocus);
					GameScr.info1.addInfo(string.Concat(new string[]
					{
						"Đã Thêm ",
						global::Char.myCharz().charFocus.cName,
						" [",
						global::Char.myCharz().charFocus.charID.ToString(),
						"]"
					}), 0);
					return;
				}
				break;
			case 10:
				AutoSkill.ShowMenuAutoSkill((int)p);
				return;
			case 11:
			{
				int num = (int)p;
				AutoSkill.isAutoUseSkills[num] = !AutoSkill.isAutoUseSkills[num];
				if (AutoSkill.isAutoUseSkills[num])
				{
					AutoSkill.timeAutoSkills[num] = -1L;
					AutoSkill.lastTimeUseSkill[num] = mSystem.currentTimeMillis() - 999999999L;
				}
				GameScr.info1.addInfo("Auto " + GameScr.keySkill[num].template.name + (AutoSkill.isAutoUseSkills[num] ? (": " + NinjaUtil.getMoneys(AutoSkill.timeAutoSkills[num]) + " mili giây") : "\n[STATUS: OFF]"), 0);
				return;
			}
			case 12:
				ChatTextField.gI().strChat = AutoSkill.inputDelay[0];
				ChatTextField.gI().tfChat.name = AutoSkill.inputDelay[1];
				ChatTextField.gI().startChat2(AutoSkill.getInstance(), string.Empty);
				AutoSkill.indexSkillAuto = (int)p;
				return;
			case 13:
			{
				int num2 = (int)p;
				AutoSkill.ToggleFreezeSkill(GameScr.keySkill[num2]);
				return;
			}
			case 14:
				AutoSkill.isAutoReviveSelf = !AutoSkill.isAutoReviveSelf;
				GameScr.info1.addInfo("Auto Hồi Sinh Bản Thân\n" + (AutoSkill.isAutoReviveSelf ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x000A3FB4 File Offset: 0x000A21B4
		public static void ShowMenu()
		{
			AutoSkill.LoadData();
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Auto Hồi Sinh\nBản Thân\n" + (AutoSkill.isAutoReviveSelf ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoSkill.getInstance(), 14, null));
			myVector.addElement(new Command("Tự Đánh\n" + (AutoSkill.isAutoSendAttack ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoSkill.getInstance(), 1, null));
			myVector.addElement(new Command("Đánh Khi Đệ Cần\n" + (AutoSkill.isTrainPet ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoSkill.getInstance(), 2, null));
			myVector.addElement(new Command(GameScr.keySkill.Length.ToString() + " Ô Kỹ Năng", AutoSkill.getInstance(), 3, null));
			myVector.addElement(new Command("Lưu Cài Đặt\n" + (AutoSkill.isSaveData ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoSkill.getInstance(), 5, null));
			myVector.addElement(new Command("Đánh Chuyển Mục Tiêu\n" + (AutoSkill.isAutoChangeFocus ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoSkill.getInstance(), 6, null));
			if (AutoSkill.listTargetAutoChangeFocus.Count > 0)
			{
				myVector.addElement(new Command("Clear Danh Sách Chuyển Mục Tiêu", AutoSkill.getInstance(), 7, null));
			}
			if (global::Char.myCharz().charFocus != null)
			{
				if (AutoSkill.listTargetAutoChangeFocus.Contains(global::Char.myCharz().charFocus))
				{
					myVector.addElement(new Command("Xóa Khỏi Danh Sách Chuyển Mục Tiêu", AutoSkill.getInstance(), 8, null));
				}
				else
				{
					myVector.addElement(new Command("Thêm Vào Danh Sách Chuyển Mục Tiêu", AutoSkill.getInstance(), 9, null));
				}
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000A4138 File Offset: 0x000A2338
		private static void ShowMenuAutoSkill(int skillIndex)
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Auto Sử Dụng\n" + (AutoSkill.isAutoUseSkills[skillIndex] ? ("[" + NinjaUtil.getMoneys(AutoSkill.timeAutoSkills[skillIndex]) + " mili giây]") : "[STATUS: OFF]"), AutoSkill.getInstance(), 11, skillIndex));
			myVector.addElement(new Command("Nhập Delay\n[mili giây]", AutoSkill.getInstance(), 12, skillIndex));
			myVector.addElement(new Command("Đóng Băng\n" + GameScr.keySkill[skillIndex].template.name, AutoSkill.getInstance(), 13, skillIndex));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00009276 File Offset: 0x00007476
		private static void ResetChatTextField()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000045ED File Offset: 0x000027ED
		private static void LoadData()
		{
			AutoSkill.isSaveData = (Rms.loadRMSInt("AutoSkillIsSaveRms") == 1);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x000045ED File Offset: 0x000027ED
		private static void smethod_6()
		{
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x000A41F4 File Offset: 0x000A23F4
		private static void LoadKeySkills()
		{
			bool flag = false;
			for (int i = 0; i < GameScr.keySkill.Length; i++)
			{
				if (GameScr.keySkill[i] != null)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				return;
			}
			for (int i = 0; i < global::Char.myCharz().nClass.skillTemplates.Length; i++)
			{
				SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[i];
				Skill skill = global::Char.myCharz().getSkill(skillTemplate);
				if (skill != null)
				{
					GameScr.keySkill[i] = skill;
				}
			}
			GameScr.gI().saveKeySkillToRMS();
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000A4250 File Offset: 0x000A2450
		
		public static void AutoReviveSelf()
		{
			if (global::Char.myCharz() == null) return;
			if (global::Char.myCharz().cgender != 1) return;
			if (global::Char.myCharz().meDead || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5 || global::Char.myCharz().cHP <= 0L) return;

			Skill skillRevive = global::Char.myCharz().getSkill(new SkillTemplate { id = 7 });
			if (skillRevive != null)
			{
				if (mSystem.currentTimeMillis() - skillRevive.lastTimeUseThisSkill > (long)(skillRevive.coolDown + 100))
				{
					if (global::Char.myCharz().cMP >= (long)skillRevive.manaUse)
					{
						int currentSkillId = (global::Char.myCharz().myskill != null) ? (int)global::Char.myCharz().myskill.template.id : -1;
						Service.gI().selectSkill(7);
						Service.gI().sendPlayerAttack(new MyVector(), MainMod.GetMyVectorMe(), -1);
						if (currentSkillId != -1)
						{
							Service.gI().selectSkill(currentSkillId);
						}
						skillRevive.lastTimeUseThisSkill = mSystem.currentTimeMillis();
					}
				}
			}
		}

		public static void AutoSendAttack()
		{
			if (!global::Char.myCharz().meDead && global::Char.myCharz().cHP > 0L && global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5 && global::Char.myCharz().myskill.template.type != 3 && global::Char.myCharz().myskill.template.id != 10 && global::Char.myCharz().myskill.template.id != 11 && (!global::Char.myCharz().myskill.paintCanNotUseSkill || GameCanvas.panel.isShow))
			{
				int mySkillIndex = AutoSkill.GetMySkillIndex();
				if (mSystem.currentTimeMillis() - AutoSkill.lastTimeSendAttack[mySkillIndex] > AutoSkill.GetCoolDown(global::Char.myCharz().myskill))
				{
					if (GameScr.gI().isMeCanAttackMob(global::Char.myCharz().mobFocus))
					{
						global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
						AutoSkill.SendAttackToMobFocus();
						AutoSkill.lastTimeSendAttack[mySkillIndex] = mSystem.currentTimeMillis();
						return;
					}
					if (global::Char.myCharz().charFocus != null && AutoSkill.isMeCanAttackChar(global::Char.myCharz().charFocus) && (double)global::Math.abs(global::Char.myCharz().charFocus.cx - global::Char.myCharz().cx) < (double)global::Char.myCharz().myskill.dx * 1.7)
					{
						global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
						AutoSkill.SendAttackToCharFocus();
						AutoSkill.lastTimeSendAttack[mySkillIndex] = mSystem.currentTimeMillis();
					}
				}
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x000A43F0 File Offset: 0x000A25F0
		private static void AutoSkillForPet()
		{
			if (!AutoSkill.isPetAskedForUseSkill || GameScr.vMob.size() == 0 || global::Char.myCharz().myskill.template.type == 3)
			{
				return;
			}
			Mob mobFocus = (Mob)GameScr.vMob.elementAt(0);
			int num = 0;
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				int num2 = global::Math.abs(global::Char.myCharz().cx - mob.x);
				int num3 = global::Math.abs(global::Char.myCharz().cy - mob.y);
				int num4 = num2 * num2 + num3 * num3;
				if (num < num4)
				{
					num = num4;
					mobFocus = mob;
				}
			}
			global::Char.myCharz().mobFocus = mobFocus;
			int mySkillIndex = AutoSkill.GetMySkillIndex();
			if (mSystem.currentTimeMillis() - AutoSkill.lastTimeSendAttack[mySkillIndex] > (long)(global::Char.myCharz().myskill.coolDown + 100) && GameScr.gI().isMeCanAttackMob(global::Char.myCharz().mobFocus))
			{
				AutoSkill.SendAttackToMobFocus();
				AutoSkill.lastTimeSendAttack[mySkillIndex] = mSystem.currentTimeMillis();
				AutoSkill.isPetAskedForUseSkill = false;
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x000A4508 File Offset: 0x000A2708
				private static void AutoUseSkill(int skillIndex)
		{
			if (TileMap.mapID != 21 && TileMap.mapID != 22 && TileMap.mapID != 23)
			{
				if (skillIndex >= GameScr.keySkill.Length)
				{
					skillIndex = GameScr.keySkill.Length - 1;
				}
				if (skillIndex < 0)
				{
					skillIndex = 0;
				}
				if (GameScr.keySkill[skillIndex] != null && !GameScr.keySkill[skillIndex].paintCanNotUseSkill)
				{
					if (mSystem.currentTimeMillis() - GameScr.keySkill[skillIndex].lastTimeUseThisSkill < (long)GameScr.keySkill[skillIndex].coolDown)
					{
						return;
					}
					if (GameScr.keySkill[skillIndex].coolDown == 0)
					{
						AutoSkill.timeAutoSkills[skillIndex] = 500L;
					}
					if (AutoSkill.isMeHasEnoughMP(GameScr.keySkill[skillIndex]) && !GameScr.gI().isCharging() && mSystem.currentTimeMillis() - AutoSkill.lastTimeAutoUseSkill > 500L)
					{
						if (AutoSkill.timeAutoSkills[skillIndex] == -1L)
						{
							AutoSkill.timeAutoSkills[skillIndex] = ((GameScr.keySkill[skillIndex].coolDown > 0) ? ((long)(GameScr.keySkill[skillIndex].coolDown + 100)) : 500L);
						}
						if (mSystem.currentTimeMillis() - AutoSkill.lastTimeUseSkill[skillIndex] > AutoSkill.timeAutoSkills[skillIndex])
						{
							long num = mSystem.currentTimeMillis();
							AutoSkill.lastTimeUseSkill[skillIndex] = num;
							AutoSkill.lastTimeAutoUseSkill = num;
							if (GameScr.keySkill[skillIndex].template.isUseAlone() || GameScr.keySkill[skillIndex].template.isSkillSpec())
							{
								GameScr.gI().doSelectSkill(GameScr.keySkill[skillIndex], true);
								GameScr.gI().doSelectSkill(GameScr.keySkill[skillIndex], true);
							}
							else
							{
								GameScr.gI().doUseSkill(GameScr.keySkill[skillIndex], true);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x000A463C File Offset: 0x000A283C
		public static bool isMeCanAttackChar(global::Char ch)
		{
			bool result;
			if (TileMap.mapID == 113)
			{
				result = (ch != null && global::Char.myCharz().myskill != null && (ch.cTypePk == 5 || ch.cTypePk == 3));
			}
			else
			{
				result = (ch != null && global::Char.myCharz().myskill != null && ((ch.statusMe != 14 && ch.statusMe != 5 && global::Char.myCharz().myskill.template.type != 2 && ((global::Char.myCharz().cFlag == 8 && ch.cFlag != 0) || (global::Char.myCharz().cFlag != 0 && ch.cFlag == 8) || (global::Char.myCharz().cFlag != ch.cFlag && global::Char.myCharz().cFlag != 0 && ch.cFlag != 0) || (ch.cTypePk == 3 && global::Char.myCharz().cTypePk == 3) || global::Char.myCharz().cTypePk == 5 || ch.cTypePk == 5 || (global::Char.myCharz().cTypePk == 1 && ch.cTypePk == 1) || (global::Char.myCharz().cTypePk == 4 && ch.cTypePk == 4))) || (global::Char.myCharz().myskill.template.type == 2 && ch.cTypePk != 5)));
			}
			return result;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x000A47A8 File Offset: 0x000A29A8
		private static bool isMeHasEnoughMP(Skill skillToUse)
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

		// Token: 0x06000B26 RID: 2854 RVA: 0x000A4818 File Offset: 0x000A2A18
		private static void SendAttackToCharFocus()
		{
			try
			{
				MyVector myVector = new MyVector();
				myVector.addElement(global::Char.myCharz().charFocus);
				Service.gI().sendPlayerAttack(new MyVector(), myVector, 2);
			}
			catch
			{
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x000A4864 File Offset: 0x000A2A64
		private static void SendAttackToMobFocus()
		{
			try
			{
				MyVector myVector = new MyVector();
				myVector.addElement(global::Char.myCharz().mobFocus);
				Service.gI().sendPlayerAttack(myVector, new MyVector(), 1);
			}
			catch
			{
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000A48B0 File Offset: 0x000A2AB0
		private static long GetCoolDown(Skill skill)
		{
			long result;
			if (skill.template.id != 20 && skill.template.id != 22 && skill.template.id != 7 && skill.template.id != 18 && skill.template.id != 23)
			{
				result = (long)(skill.coolDown + 100);
			}
			else
			{
				result = (long)skill.coolDown + 500L;
			}
			return result;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x000A4924 File Offset: 0x000A2B24
		private static int GetMySkillIndex()
		{
			for (int i = 0; i < GameScr.keySkill.Length; i++)
			{
				if (GameScr.keySkill[i] == global::Char.myCharz().myskill)
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000A495C File Offset: 0x000A2B5C
		private static void AutoChangeFocus()
		{
			if (AutoSkill.listTargetAutoChangeFocus.Count == 0)
			{
				GameScr.info1.addInfo("Danh sách chuyển mục tiêu trống!", 0);
				AutoSkill.isAutoChangeFocus = false;
				return;
			}
			if (!global::Char.myCharz().meDead && global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5 && global::Char.myCharz().myskill.template.type != 3 && global::Char.myCharz().myskill.template.id != 10 && global::Char.myCharz().myskill.template.id != 11 && !global::Char.myCharz().myskill.paintCanNotUseSkill)
			{
				AutoSkill.cooldownAutoChangeFocus = AutoSkill.GetCooldownAutoChangeFocus(global::Char.myCharz().myskill);
				if (AutoSkill.targetIndex >= AutoSkill.listTargetAutoChangeFocus.Count)
				{
					AutoSkill.targetIndex = 0;
				}
				if (mSystem.currentTimeMillis() - AutoSkill.lastTimeChangeFocus >= AutoSkill.cooldownAutoChangeFocus)
				{
					AutoSkill.lastTimeChangeFocus = mSystem.currentTimeMillis();
					global::Char.myCharz().charFocus = GameScr.findCharInMap(AutoSkill.listTargetAutoChangeFocus[AutoSkill.targetIndex].charID);
					AutoSkill.targetIndex++;
					if (AutoSkill.targetIndex >= AutoSkill.listTargetAutoChangeFocus.Count)
					{
						AutoSkill.targetIndex = 0;
					}
					if (global::Char.myCharz().charFocus != null && AutoSkill.isMeCanAttackChar(global::Char.myCharz().charFocus) && (double)global::Math.abs(global::Char.myCharz().charFocus.cx - global::Char.myCharz().cx) < (double)global::Char.myCharz().myskill.dx * 1.5)
					{
						global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
						AutoSkill.SendAttackToCharFocus();
					}
				}
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x000A4B24 File Offset: 0x000A2D24
		private static long GetCooldownAutoChangeFocus(Skill skill)
		{
			long result;
			if (skill.coolDown <= 500)
			{
				result = 1000L;
			}
			else
			{
				result = (long)((double)skill.coolDown * 1.2 + 200.0);
			}
			return result;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000045ED File Offset: 0x000027ED
		private static void smethod_0()
		{
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x000A4B68 File Offset: 0x000A2D68
		static AutoSkill()
		{
			AutoSkill.LoadData();
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000A4BD8 File Offset: 0x000A2DD8
		public static void FreezeSelectedSkill()
		{
			int mySkillIndex = AutoSkill.GetMySkillIndex();
			AutoSkill.ToggleFreezeSkill(GameScr.keySkill[mySkillIndex]);
		}

		private static void ToggleFreezeSkill(Skill skill)
		{
			if (skill == null)
			{
				return;
			}
			sbyte id = skill.template.id;
			if (AutoSkill.frozenOriginal.ContainsKey(id))
			{
				int[] original = AutoSkill.frozenOriginal[id];
				skill.coolDown = original[0];
				skill.manaUse = original[1];
				AutoSkill.frozenOriginal.Remove(id);
				GameScr.info1.addInfo("Hủy Đóng Băng\n" + skill.template.name, 0);
			}
			else
			{
				AutoSkill.frozenOriginal[id] = new int[] { skill.coolDown, skill.manaUse };
				skill.coolDown = 0;
				skill.manaUse = 0;
				skill.lastTimeUseThisSkill = 0L;
				GameScr.info1.addInfo("Đóng Băng\n" + skill.template.name, 0);
			}
		}

		// Token: 0x040015BF RID: 5567
		private static AutoSkill _Instance;

		// Token: 0x040015C0 RID: 5568
		public static bool isLoadKeySkill = true;

		// Token: 0x040015C1 RID: 5569
		public static bool isAutoReviveSelf;
		public static bool isAutoSendAttack;

		// Token: 0x040015C2 RID: 5570
		private static long[] lastTimeSendAttack = new long[10];

		// Token: 0x040015C3 RID: 5571
		public static bool isTrainPet;

		// Token: 0x040015C4 RID: 5572
		public static bool isPetAskedForUseSkill;

		// Token: 0x040015C5 RID: 5573
		public static bool[] isAutoUseSkills = new bool[10];

		// Token: 0x040015C6 RID: 5574
		private static long[] lastTimeUseSkill = new long[10];

		// Token: 0x040015C7 RID: 5575
		private static long[] timeAutoSkills = new long[10];

		// Token: 0x040015C8 RID: 5576
		private static int indexSkillAuto;

		// Token: 0x040015C9 RID: 5577
		private static bool isAutoChangeFocus;

		// Token: 0x040015CA RID: 5578
		private static long cooldownAutoChangeFocus;

		// Token: 0x040015CB RID: 5579
		private static long lastTimeChangeFocus;

		// Token: 0x040015CC RID: 5580
		private static List<global::Char> listTargetAutoChangeFocus = new List<global::Char>();

		// Token: 0x040015CD RID: 5581
		private static int targetIndex;

		// Token: 0x040015CE RID: 5582
		private static bool isAutoShield;

		// Token: 0x040015CF RID: 5583
		private static string[] inputDelay = new string[]
		{
			"Nhập delay",
			"mili giây"
		};

		// Token: 0x040015D0 RID: 5584
		private static bool isSaveData;

		// Token: 0x040015D1 RID: 5585
		private static long lastTimeAutoUseSkill;

		private static Dictionary<sbyte, int[]> frozenOriginal = new Dictionary<sbyte, int[]>();
	}
}
