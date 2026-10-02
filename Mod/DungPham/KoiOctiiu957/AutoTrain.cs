using System;
using System.Collections.Generic;

namespace Mod.DungPham.KoiOctiiu957
{
	// Token: 0x020000FE RID: 254
	public class AutoTrain : IActionListener, IChatable
	{
		// Token: 0x06000B30 RID: 2864 RVA: 0x000092A6 File Offset: 0x000074A6
		public static AutoTrain getInstance()
		{
			if (AutoTrain._Instance == null)
			{
				AutoTrain._Instance = new AutoTrain();
			}
			return AutoTrain._Instance;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x000A4C2C File Offset: 0x000A2E2C
		public void onChatFromMe(string text, string to)
		{
			if (ChatTextField.gI().tfChat.getText() != null && !ChatTextField.gI().tfChat.getText().Equals(string.Empty) && !text.Equals(string.Empty) && text != null)
			{
				if (ChatTextField.gI().strChat.Equals(AutoTrain.inputMPPercentGoHome[0]))
				{
					try
					{
						int num = AutoTrain.minimumMPGoHome = int.Parse(ChatTextField.gI().tfChat.getText());
						GameScr.info1.addInfo("Về Nhà Khi MP Dưới\n[" + num.ToString() + "%]", 0);
					}
					catch
					{
						GameScr.info1.addInfo("%MP Không Hợp Lệ, Vui Lòng Nhập Lại", 0);
					}
					AutoTrain.ResetChatTextField();
					return;
				}
			}
			else
			{
				ChatTextField.gI().isShow = false;
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x000045ED File Offset: 0x000027ED
		public void onCancelChat()
		{
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000A4D04 File Offset: 0x000A2F04
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 1:
			{
				int num = (int)p;
				AutoTrain.listMobIds.Clear();
				for (int i = 0; i < GameScr.vMob.size(); i++)
				{
					Mob mob = (Mob)GameScr.vMob.elementAt(i);
					if (!mob.isMobMe && mob.templateId == num)
					{
						AutoTrain.listMobIds.Add(mob.mobId);
					}
				}
				AutoTrain.TurnOnAutoTrain();
				return;
			}
			case 2:
				AutoTrain.listMobIds.Clear();
				for (int j = 0; j < GameScr.vMob.size(); j++)
				{
					Mob mob2 = (Mob)GameScr.vMob.elementAt(j);
					if (!mob2.isMobMe)
					{
						AutoTrain.listMobIds.Add(mob2.mobId);
					}
				}
				AutoTrain.TurnOnAutoTrain();
				return;
			case 3:
				AutoTrain.TurnOnAutoTrain();
				return;
			case 4:
				AutoTrain.isAvoidSuperMob = !AutoTrain.isAvoidSuperMob;
				GameScr.info1.addInfo("Né Siêu Quái\n" + (AutoTrain.isAvoidSuperMob ? "[STATUS: OFF]" : "[STATUS: ON]"), 0);
				return;
			case 5:
				AutoTrain.ShowMenuGoback();
				return;
			case 6:
				AutoTrain.listMobIds.Clear();
				AutoTrain.isAutoTrain = false;
				AutoTrain.isLockAir = false;
				AutoTrain.lockAirY = 0;
				AutoTrain.currentLockedTarget = null;
				GameScr.info1.addInfo("Đã Clear Danh Sách Train!", 0);
				return;
			case 7:
				if (global::Char.myCharz().mobFocus == null)
				{
					GameScr.info1.addInfo("Vui Lòng Chọn Quái!", 0);
				}
				if (global::Char.myCharz().mobFocus != null)
				{
					AutoTrain.listMobIds.Add(global::Char.myCharz().mobFocus.mobId);
					GameScr.info1.addInfo("Đã Thêm Quái: " + global::Char.myCharz().mobFocus.mobId.ToString(), 0);
					return;
				}
				break;
			case 8:
				if (AutoTrain.isAutoTrain)
				{
					AutoTrain.isAutoTrain = false;
					AutoTrain.isLockAir = false;
					AutoTrain.lockAirY = 0;
					AutoTrain.currentLockedTarget = null;
					global::Char.myCharz().mobFocus = null;
					GameScr.isAutoPlay = false;
					GameScr.info1.addInfo("Auto Train\n[STATUS: OFF]", 0);
				}
				else
				{
					if (AutoTrain.listMobIds.Count == 0)
					{
						for (int m = 0; m < GameScr.vMob.size(); m++)
						{
							Mob mob3 = (Mob)GameScr.vMob.elementAt(m);
							if (mob3 != null && !mob3.isMobMe)
							{
								AutoTrain.listMobIds.Add(mob3.mobId);
							}
						}
					}
					AutoTrain.isAutoTrain = true;
					GameScr.isAutoPlay = true;
					GameScr.info1.addInfo("Auto Train\n[STATUS: ON]", 0);
				}
				return;
			case 9:
				if (AutoTrain.isGoBack)
				{
					AutoTrain.isGoBack = false;
					GameScr.info1.addInfo("Goback\n[STATUS: OFF]", 0);
					return;
				}
				if (!AutoTrain.isGoBack)
				{
					AutoTrain.isGobackCoordinate = false;
					AutoTrain.isGoBack = true;
					AutoTrain.gobackMapID = TileMap.mapID;
					AutoTrain.gobackZoneID = TileMap.zoneID;
					GameScr.info1.addInfo(string.Concat(new string[]
					{
						"Goback\n[",
						TileMap.mapNames[AutoTrain.gobackMapID],
						"]\n[",
						AutoTrain.gobackZoneID.ToString(),
						"]"
					}), 0);
					return;
				}
				break;
			case 10:
				if (AutoTrain.isGoBack)
				{
					AutoTrain.isGoBack = false;
					GameScr.info1.addInfo("Goback\n[STATUS: OFF]", 0);
					return;
				}
				if (!AutoTrain.isGoBack)
				{
					AutoTrain.isGobackCoordinate = true;
					AutoTrain.isGoBack = true;
					AutoTrain.gobackMapID = TileMap.mapID;
					AutoTrain.gobackZoneID = TileMap.zoneID;
					AutoTrain.gobackX = global::Char.myCharz().cx;
					AutoTrain.gobackY = global::Char.myCharz().cy;
					GameScr.info1.addInfo(string.Concat(new string[]
					{
						"Goback Tọa Độ\n[",
						AutoTrain.gobackX.ToString(),
						"-",
						AutoTrain.gobackY.ToString(),
						"]"
					}), 0);
					return;
				}
				break;
			case 11:
				ChatTextField.gI().strChat = AutoTrain.inputMPPercentGoHome[0];
				ChatTextField.gI().tfChat.name = AutoTrain.inputMPPercentGoHome[1];
				ChatTextField.gI().startChat2(AutoTrain.getInstance(), string.Empty);
				return;
			case 13:
				AutoTrain.isAvoidBossInZone = !AutoTrain.isAvoidBossInZone;
				Rms.saveRMSInt("isAvoidBossInZone", AutoTrain.isAvoidBossInZone ? 1 : 0);
				GameScr.info1.addInfo("Né Boss Đổi Khu\n" + (AutoTrain.isAvoidBossInZone ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 14:
				AutoTrain.ShowMenuSelectSkills();
				return;
			case 15:
				AutoTrain.isDisableSkill12Anim = !AutoTrain.isDisableSkill12Anim;
				Rms.saveRMSInt("isDisableSkill12Anim", AutoTrain.isDisableSkill12Anim ? 1 : 0);
				GameScr.info1.addInfo("Tắt Hoạt Ảnh Đánh/Chưởng\n" + (AutoTrain.isDisableSkill12Anim ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 16:
				AutoTrain.isAutoChangeZoneWhenNoMobs = !AutoTrain.isAutoChangeZoneWhenNoMobs;
				Rms.saveRMSInt("isAutoChangeZoneWhenNoMobs", AutoTrain.isAutoChangeZoneWhenNoMobs ? 1 : 0);
				GameScr.info1.addInfo("Đổi Khu Khi Hết Quái\n" + (AutoTrain.isAutoChangeZoneWhenNoMobs ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 140:
				AutoTrain.listSelectedSkillTemplateIds.Clear();
				for (int k = 0; k < GameScr.keySkill.Length; k++)
				{
					if (GameScr.keySkill[k] != null && GameScr.keySkill[k].template != null)
					{
						int tId = (int)GameScr.keySkill[k].template.id;
						if (!AutoTrain.listSelectedSkillTemplateIds.Contains(tId))
						{
							AutoTrain.listSelectedSkillTemplateIds.Add(tId);
						}
					}
				}
				AutoTrain.SaveSkillData();
				GameScr.info1.addInfo("Đã Bật Tất Cả Chiêu Up!", 0);
				AutoTrain.ShowMenuSelectSkills();
				return;
			case 141:
				AutoTrain.listSelectedSkillTemplateIds.Clear();
				AutoTrain.listSelectedSkillTemplateIds.Add(-999);
				AutoTrain.SaveSkillData();
				GameScr.info1.addInfo("Đã Tắt Tất Cả Chiêu Up!", 0);
				AutoTrain.ShowMenuSelectSkills();
				return;
			case 142:
			{
				int templateId = (int)p;
				if (AutoTrain.listSelectedSkillTemplateIds.Contains(-999))
				{
					AutoTrain.listSelectedSkillTemplateIds.Remove(-999);
				}
				if (AutoTrain.listSelectedSkillTemplateIds.Count == 0)
				{
					for (int l = 0; l < GameScr.keySkill.Length; l++)
					{
						if (GameScr.keySkill[l] != null && GameScr.keySkill[l].template != null)
						{
							int tId2 = (int)GameScr.keySkill[l].template.id;
							if (!AutoTrain.listSelectedSkillTemplateIds.Contains(tId2))
							{
								AutoTrain.listSelectedSkillTemplateIds.Add(tId2);
							}
						}
					}
				}
				if (AutoTrain.listSelectedSkillTemplateIds.Contains(templateId))
				{
					AutoTrain.listSelectedSkillTemplateIds.Remove(templateId);
					if (AutoTrain.listSelectedSkillTemplateIds.Count == 0)
					{
						AutoTrain.listSelectedSkillTemplateIds.Add(-999);
					}
					GameScr.info1.addInfo("Đã Tắt Chiêu Này!", 0);
				}
				else
				{
					AutoTrain.listSelectedSkillTemplateIds.Add(templateId);
					GameScr.info1.addInfo("Đã Bật Chiêu Này!", 0);
				}
				AutoTrain.SaveSkillData();
				AutoTrain.ShowMenuSelectSkills();
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000A5068 File Offset: 0x000A3268
		public static void ShowMenu()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Auto Train\n" + (AutoTrain.isAutoTrain ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoTrain.getInstance(), 8, null));
			List<Mob> list = new List<Mob>();
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				if (!mob.isMobMe)
				{
					bool flag = false;
					for (int j = 0; j < list.Count; j++)
					{
						if (mob.templateId == list[j].templateId)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						list.Add(mob);
						myVector.addElement(new Command(string.Concat(new string[]
						{
							"Tàn Sát\n",
							mob.getTemplate().name,
							"\n[",
							NinjaUtil.getMoneys(mob.maxHp),
							"HP]"
						}), AutoTrain.getInstance(), 1, mob.templateId));
					}
				}
			}
			myVector.addElement(new Command("Tàn Sát Tất Cả", AutoTrain.getInstance(), 2, null));
			myVector.addElement(new Command("Tàn Sát Theo Vị Trí", AutoTrain.getInstance(), 3, null));
			myVector.addElement(new Command("Chọn Chiêu Up\n[" + AutoTrain.GetSelectedSkillsCount().ToString() + " Chiêu]", AutoTrain.getInstance(), 14, null));
			myVector.addElement(new Command("Tắt Hoạt Ảnh Đánh/Chưởng\n" + (AutoTrain.isDisableSkill12Anim ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoTrain.getInstance(), 15, null));
			myVector.addElement(new Command("Né Boss Đổi Khu\n" + (AutoTrain.isAvoidBossInZone ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoTrain.getInstance(), 13, null));
			myVector.addElement(new Command("Đổi Khu Khi Hết Quái\n" + (AutoTrain.isAutoChangeZoneWhenNoMobs ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoTrain.getInstance(), 16, null));
			myVector.addElement(new Command("Né Siêu Quái\n" + (AutoTrain.isAvoidSuperMob ? "[STATUS: OFF]" : "[STATUS: ON]"), AutoTrain.getInstance(), 4, null));
			myVector.addElement(new Command("Goback", AutoTrain.getInstance(), 5, null));
			myVector.addElement(new Command("Clear Danh Sách Train", AutoTrain.getInstance(), 6, null));
			if (global::Char.myCharz().mobFocus != null)
			{
				myVector.addElement(new Command(string.Concat(new string[]
				{
					"Thêm\n[",
					global::Char.myCharz().mobFocus.getTemplate().name,
					"]\n[",
					global::Char.myCharz().mobFocus.mobId.ToString(),
					"]"
				}), AutoTrain.getInstance(), 7, null));
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x000A527C File Offset: 0x000A347C
		private static void ShowMenuGoback()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Goback\n" + (AutoTrain.isGoBack ? string.Concat(new string[]
			{
				"[",
				TileMap.mapNames[AutoTrain.gobackMapID],
				"]\n[",
				AutoTrain.gobackZoneID.ToString(),
				"]"
			}) : "[STATUS: OFF]"), AutoTrain.getInstance(), 9, null));
			myVector.addElement(new Command("Goback Tọa Độ\n" + ((!AutoTrain.isGoBack || !AutoTrain.isGobackCoordinate) ? "[STATUS: OFF]" : string.Concat(new string[]
			{
				"[",
				AutoTrain.gobackX.ToString(),
				"-",
				AutoTrain.gobackY.ToString(),
				"]"
			})), AutoTrain.getInstance(), 10, null));
			myVector.addElement(new Command("Về Nhà Khi MP Dưới\n[" + AutoTrain.minimumMPGoHome.ToString() + "%]", AutoTrain.getInstance(), 11, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00009276 File Offset: 0x00007476
		private static void ResetChatTextField()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000A53A0 File Offset: 0x000A35A0
		private static void TeleportTo(int x, int y)
		{
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			global::Char.myCharz().cxSend = -1;
			global::Char.myCharz().cySend = -1;
			Service.gI().charMove();
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000092BE File Offset: 0x000074BE
		private static bool isMeCanAttack(Mob mob)
		{
			return GameScr.canAutoPlay || !mob.checkIsBoss() || (mob.checkIsBoss() && AutoTrain.isAvoidSuperMob);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x000092E0 File Offset: 0x000074E0
		private static bool isMeOutOfMP()
		{
			return global::Char.myCharz().cMP < global::Char.myCharz().cMPFull * (long)AutoTrain.minimumMPGoHome / 100L;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00009303 File Offset: 0x00007503
		private static void TurnOnAutoTrain()
		{
			if (AutoTrain.listMobIds.Count == 0)
			{
				GameScr.info1.addInfo("Danh Sách Tàn Sát Trống!", 0);
				AutoTrain.isAutoTrain = false;
				return;
			}
			AutoTrain.isAutoTrain = true;
			GameScr.isAutoPlay = true;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00009343 File Offset: 0x00007543
		static AutoTrain()
		{
			AutoTrain.minimumMPGoHome = 5;
			AutoTrain.inputMPPercentGoHome = new string[]
			{
				"Nhập %MP",
				"%MP"
			};
			AutoTrain.isAvoidBossInZone = (Rms.loadRMSInt("isAvoidBossInZone") == 1);
			int savedAnim = Rms.loadRMSInt("isDisableSkill12Anim");
			AutoTrain.isDisableSkill12Anim = (savedAnim != 0);
			int savedChangeZone = Rms.loadRMSInt("isAutoChangeZoneWhenNoMobs");
			AutoTrain.isAutoChangeZoneWhenNoMobs = (savedChangeZone != 0);
			AutoTrain.LoadSkillData();
		}

		public static bool isDisableSkill12Anim = true;

		public static bool isLockAir = false;

		public static int lockAirY = 0;

		public static bool isAutoChangeZoneWhenNoMobs = true;

		public static long lastTimeChangeZoneNoMobs;

		public static long timeNoMobsDetected;

		public static long lastTimeZoneChanged;

		public static int lastKnownZoneId = -1;

		public static int lastKnownMapId = -1;

		public static List<int> listSelectedSkillTemplateIds = new List<int>();

		public static void LoadSkillData()
		{
			try
			{
				string saved = Rms.loadRMSString("AutoTrainSelectedSkills");
				AutoTrain.listSelectedSkillTemplateIds.Clear();
				if (!string.IsNullOrEmpty(saved))
				{
					string[] items = saved.Split(',');
					foreach (string item in items)
					{
						int id;
						if (int.TryParse(item.Trim(), out id))
						{
							if (!AutoTrain.listSelectedSkillTemplateIds.Contains(id))
							{
								AutoTrain.listSelectedSkillTemplateIds.Add(id);
							}
						}
					}
				}
			}
			catch { }
		}

		public static void SaveSkillData()
		{
			try
			{
				string saved = string.Join(",", AutoTrain.listSelectedSkillTemplateIds.ConvertAll<string>(delegate(int i) { return i.ToString(); }).ToArray());
				Rms.saveRMSString("AutoTrainSelectedSkills", saved);
			}
			catch { }
		}

		public static bool IsSkillSelected(Skill skill)
		{
			if (skill == null || skill.template == null) return false;
			if (AutoTrain.listSelectedSkillTemplateIds.Contains(-999)) return false;
			if (AutoTrain.listSelectedSkillTemplateIds.Count == 0) return true;
			return AutoTrain.listSelectedSkillTemplateIds.Contains((int)skill.template.id);
		}

		public static bool IsSkill1OrSkill2(Skill skill)
		{
			if (!AutoTrain.isDisableSkill12Anim) return false;
			if (skill == null || skill.template == null) return false;
			int id = (int)skill.template.id;
			return id >= 0 && id <= 5;
		}

		public static void PerformSkillWithoutAnimation(global::Char me, Skill skill, Mob targetMob)
		{
			if (me == null || skill == null || skill.template == null || targetMob == null)
			{
				return;
			}
			if (targetMob.hp <= 0L || targetMob.status == 0 || targetMob.status == 1)
			{
				return;
			}
			long now = mSystem.currentTimeMillis();
			long cd = (skill.coolDown > 0) ? (long)skill.coolDown : 200L;
			cd += 20L; // Ping buffer
			if (now - skill.lastTimeUseThisSkill < cd)
			{
				return;
			}

			int mana = (skill.template.manaUseType == 2) ? 1 : ((skill.template.manaUseType == 1) ? ((int)((long)skill.manaUse * me.cMPFull / 100L)) : skill.manaUse);
			if (me.cMP < (long)mana)
			{
				return;
			}

			skill.lastTimeUseThisSkill = now;
			skill.paintCanNotUseSkill = false;

			if (skill.template.manaUseType == 2)
			{
				me.cMP = 1L;
			}
			else if (skill.template.manaUseType != 1)
			{
				me.cMP -= (long)skill.manaUse;
			}
			else
			{
				me.cMP -= (long)skill.manaUse * me.cMPFull / 100L;
			}
			if (me.cMP < 0L)
			{
				me.cMP = 0L;
			}

			me.cStamina--;
			GameScr.gI().isInjureMp = true;
			GameScr.gI().twMp = 0L;

			me.skillPaint = null;
			me.skillPaintRandomPaint = null;
			me.dart = null;
			me.arr = null;

			if (GameScr.sks != null && (int)skill.skillId >= 0 && (int)skill.skillId < GameScr.sks.Length)
			{
				SkillPaint sp = GameScr.sks[(int)skill.skillId];
				if (sp != null && sp.effectHappenOnMob > 0 && GameScr.efs != null && sp.effectHappenOnMob - 1 < GameScr.efs.Length)
				{
					me.effPaints = new EffectPaint[1];
					me.effPaints[0] = new EffectPaint();
					me.effPaints[0].effCharPaint = GameScr.efs[sp.effectHappenOnMob - 1];
					me.effPaints[0].eMob = targetMob;
				}
			}

			MyVector myVector = new MyVector();
			myVector.addElement(targetMob);
			Service.gI().sendPlayerAttack(myVector, new MyVector(), 1);
		}

		public static int GetSelectedSkillsCount()
		{
			if (AutoTrain.listSelectedSkillTemplateIds.Contains(-999)) return 0;
			if (AutoTrain.listSelectedSkillTemplateIds.Count == 0)
			{
				int count = 0;
				for (int i = 0; i < GameScr.keySkill.Length; i++)
				{
					if (GameScr.keySkill[i] != null) count++;
				}
				return count;
			}
			int countSelected = 0;
			for (int i = 0; i < GameScr.keySkill.Length; i++)
			{
				if (GameScr.keySkill[i] != null && AutoTrain.listSelectedSkillTemplateIds.Contains((int)GameScr.keySkill[i].template.id))
				{
					countSelected++;
				}
			}
			return countSelected;
		}

		public static void ShowMenuSelectSkills()
		{
			SkillSelectDlg dlg = new SkillSelectDlg();
			dlg.show();
		}

		public static bool IsHarmlessBoss(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			string lower = name.ToLower().Trim();
			string[] harmlessKeywords = new string[]
			{
				"ăn trộm", "an trom", "tên trộm", "ten trom", "kẻ trộm", "ke trom", "trộm",
				"ở dơ", "o do",
				"xinbato", "xin ba to",
				"đường tăng", "duong tang", "đường tam tạng", "duong tam tang",
				"héc quyn", "hẹc quynh", "héc-quyn", "hec quyn", "hecquyn", "sói héc", "sói hẹc",
				"ngộ không", "ngo khong", "bát giới", "bat gioi", "sa tăng", "sa tang", "bạch long", "bach long",
				"thỏ trắng", "tho trang", "thỏ đại ca", "tho dai ca", "thỏ xám", "thỏ ngọc",
				"trọng tài", "trong tai", "bò mộng", "bo mong",
				"tàu pảy pảy", "tau pay pay", "pảy pảy", "pay pay",
				"người tuyết", "nguoi tuyet", "tuần lộc", "tuan loc", "ông già noel", "ong gia noel",
				"bí ngô", "bi ngo", "bóng ma", "bong ma", "bong bóng", "ma trơi"
			};
			for (int i = 0; i < harmlessKeywords.Length; i++)
			{
				if (lower.Contains(harmlessKeywords[i]))
				{
					return true;
				}
			}
			return false;
		}

		public static bool HasBossInCurrentZone(out string bossName)
		{
			bossName = string.Empty;
			if (GameScr.vCharInMap != null)
			{
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					global::Char c = (global::Char)GameScr.vCharInMap.elementAt(i);
					if (c != null && !c.meDead && c.cHP > 0L && c.statusMe != 14 && c.statusMe != 5 && !c.isPet && !c.isMiniPet && MainMod.isBoss(c))
					{
						if (!IsHarmlessBoss(c.cName))
						{
							bossName = c.cName;
							return true;
						}
					}
				}
			}
			if (GameScr.vMob != null)
			{
				for (int j = 0; j < GameScr.vMob.size(); j++)
				{
					Mob mob = (Mob)GameScr.vMob.elementAt(j);
					if (mob != null && mob.hp > 0L && mob.status != 0 && mob.status != 1)
					{
						if (mob is Assets.src.g.BigBoss || mob is BigBoss2 || mob is BachTuoc || mob is NewBoss)
						{
							string name = (mob.getTemplate() != null) ? mob.getTemplate().name : "Boss";
							if (!IsHarmlessBoss(name))
							{
								bossName = name;
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		public static void AvoidBossChangeZone(string bossName)
		{
			if (global::Char.myCharz().meDead || global::Char.ischangingMap || Controller.isStopReadMessage)
			{
				return;
			}
			if (mSystem.currentTimeMillis() - AutoTrain.lastTimeChangeZoneAvoidBoss < 2000L)
			{
				return;
			}
			AutoTrain.lastTimeChangeZoneAvoidBoss = mSystem.currentTimeMillis();
			global::Char.myCharz().mobFocus = null;
			global::Char.myCharz().charFocus = null;
			global::Char.myCharz().currentMovePoint = null;

			int nextZone = TileMap.zoneID + 1;
			int maxZone = (GameScr.gI().zones != null && GameScr.gI().zones.Length > 0) ? GameScr.gI().zones.Length : 20;
			if (nextZone >= maxZone)
			{
				nextZone = 0;
			}
			Service.gI().requestChangeZone(nextZone, -1);
			if (AutoTrain.isGoBack)
			{
				AutoTrain.gobackZoneID = nextZone;
			}
			GameScr.info1.addInfo("Phát hiện Boss [" + bossName + "]!\nNé sang Khu " + nextZone, 0);
		}

		public static bool HasAliveTargetMobs()
		{
			if (GameScr.vMob == null || GameScr.vMob.size() == 0)
			{
				return false;
			}
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				if (mob == null || mob.isMobMe || mob.hp <= 0L || mob.status == 0 || mob.status == 1)
				{
					continue;
				}
				if (AutoTrain.listMobIds.Count > 0 && !AutoTrain.listMobIds.Contains(mob.mobId))
				{
					continue;
				}
				if (!AutoTrain.isMeCanAttack(mob))
				{
					continue;
				}
				if (mob.x <= 0 || mob.y <= 0)
				{
					continue;
				}
				return true;
			}
			return false;
		}

		public static void CheckAndChangeZoneWhenNoMobs()
		{
			if (!AutoTrain.isAutoTrain || !AutoTrain.isAutoChangeZoneWhenNoMobs)
			{
				AutoTrain.timeNoMobsDetected = 0L;
				return;
			}
			global::Char me = global::Char.myCharz();
			if (me == null || me.meDead || me.cHP <= 0L || me.statusMe == 14 || me.statusMe == 5)
			{
				AutoTrain.timeNoMobsDetected = 0L;
				return;
			}
			if (global::Char.ischangingMap || global::Char.isLoadingMap || Controller.isStopReadMessage)
			{
				AutoTrain.timeNoMobsDetected = 0L;
				return;
			}
			if (TileMap.isOfflineMap() || TileMap.mapPhuBang())
			{
				AutoTrain.timeNoMobsDetected = 0L;
				return;
			}
			if (GameScr.gI().zones != null && GameScr.gI().zones.Length <= 1)
			{
				AutoTrain.timeNoMobsDetected = 0L;
				return;
			}
			if (GameScr.vMob == null || GameScr.vMob.size() == 0)
			{
				AutoTrain.timeNoMobsDetected = 0L;
				return;
			}

			// Kiểm tra map có quái mục tiêu thuộc listMobIds không
			if (AutoTrain.listMobIds.Count > 0)
			{
				bool hasAnyTargetInMap = false;
				for (int i = 0; i < GameScr.vMob.size(); i++)
				{
					Mob m = (Mob)GameScr.vMob.elementAt(i);
					if (m != null && !m.isMobMe && AutoTrain.listMobIds.Contains(m.mobId))
					{
						hasAnyTargetInMap = true;
						break;
					}
				}
				if (!hasAnyTargetInMap)
				{
					AutoTrain.timeNoMobsDetected = 0L;
					return;
				}
			}

			long now = mSystem.currentTimeMillis();

			// Mới đổi khu / đổi map: chờ ít nhất 1.5s để server load quái xong
			if (TileMap.zoneID != AutoTrain.lastKnownZoneId || TileMap.mapID != AutoTrain.lastKnownMapId)
			{
				AutoTrain.lastKnownZoneId = TileMap.zoneID;
				AutoTrain.lastKnownMapId = TileMap.mapID;
				AutoTrain.lastTimeZoneChanged = now;
				AutoTrain.timeNoMobsDetected = 0L;
				return;
			}
			if (now - AutoTrain.lastTimeZoneChanged < 1500L)
			{
				return;
			}

			// Nếu vẫn còn quái sống trong khu, reset thời gian
			if (AutoTrain.HasAliveTargetMobs())
			{
				AutoTrain.timeNoMobsDetected = 0L;
				return;
			}

			// Ghi nhận mốc thời gian phát hiện hết quái
			if (AutoTrain.timeNoMobsDetected == 0L)
			{
				AutoTrain.timeNoMobsDetected = now;
			}

			// Chờ 600ms không có quái (để nhặt item và xác nhận quái chưa hồi sinh)
			if (now - AutoTrain.timeNoMobsDetected >= 600L)
			{
				if (now - AutoTrain.lastTimeChangeZoneNoMobs >= 3000L)
				{
					AutoTrain.ChangeZoneWhenNoMobs();
				}
			}
		}

		public static void ChangeZoneWhenNoMobs()
		{
			global::Char me = global::Char.myCharz();
			if (me == null || me.meDead || me.cHP <= 0L || me.statusMe == 14 || me.statusMe == 5)
			{
				return;
			}
			if (global::Char.ischangingMap || global::Char.isLoadingMap || Controller.isStopReadMessage)
			{
				return;
			}
			long now = mSystem.currentTimeMillis();
			if (now - AutoTrain.lastTimeChangeZoneNoMobs < 3000L)
			{
				return;
			}
			AutoTrain.lastTimeChangeZoneNoMobs = now;
			AutoTrain.timeNoMobsDetected = 0L;

			me.mobFocus = null;
			me.charFocus = null;
			me.currentMovePoint = null;
			AutoTrain.isLockAir = false;
			AutoTrain.lockAirY = 0;

			int maxZone = (GameScr.gI().zones != null && GameScr.gI().zones.Length > 0) ? GameScr.gI().zones.Length : 20;
			int nextZone = (TileMap.zoneID + 1) % maxZone;

			if (GameScr.gI().zones != null && GameScr.gI().zones.Length > 0 && GameScr.gI().numPlayer != null && GameScr.gI().maxPlayer != null && GameScr.gI().pts != null)
			{
				for (int i = 1; i < GameScr.gI().zones.Length; i++)
				{
					int candidate = (TileMap.zoneID + i) % GameScr.gI().zones.Length;
					if (candidate < GameScr.gI().numPlayer.Length && candidate < GameScr.gI().maxPlayer.Length && candidate < GameScr.gI().pts.Length)
					{
						if (GameScr.gI().numPlayer[candidate] < GameScr.gI().maxPlayer[candidate] && GameScr.gI().pts[candidate] != 1)
						{
							nextZone = candidate;
							break;
						}
					}
				}
			}

			if (nextZone == TileMap.zoneID)
			{
				nextZone = (TileMap.zoneID + 1) % maxZone;
			}

			Service.gI().requestChangeZone(nextZone, -1);
			if (AutoTrain.isGoBack)
			{
				AutoTrain.gobackZoneID = nextZone;
			}
			GameScr.info1.addInfo("Hết quái! Đổi sang Khu " + nextZone, 0);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000A5410 File Offset: 0x000A3610
		public static void UseGrape()
		{
			for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
			{
				Item item = global::Char.myCharz().arrItemBag[i];
				if (item != null && item.template.id == 212)
				{
					Service.gI().useItem(0, 1, (sbyte)item.indexUI, -1);
					return;
				}
			}
			for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
			{
				Item item2 = global::Char.myCharz().arrItemBag[j];
				if (item2 != null && item2.template.id == 211)
				{
					Service.gI().useItem(0, 1, (sbyte)item2.indexUI, -1);
					return;
				}
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000A54BC File Offset: 0x000A36BC
		public static void Update()
		{
			if (AutoTrain.isAutoTrain || (GameScr.isAutoPlay && GameScr.canAutoPlay))
			{
				if (AutoTrain.isAvoidBossInZone)
				{
					string bossName;
					if (AutoTrain.HasBossInCurrentZone(out bossName))
					{
						AutoTrain.AvoidBossChangeZone(bossName);
						return;
					}
				}
				AutoTrain.DoIt();
			}
			if (global::Char.myCharz().cStamina <= 5 && GameCanvas.gameTick % 100 == 0)
			{
				AutoTrain.UseGrape();
			}
			if (!AutoTrain.isGoBack)
			{
				return;
			}
			if (global::Char.myCharz().meDead && GameCanvas.gameTick % 100 == 0)
			{
				Service.gI().returnTownFromDead();
			}
			if (AutoTrain.isMeOutOfMP())
			{
				int num = 21 + global::Char.myCharz().cgender;
				if (TileMap.mapID != num)
				{
					GameScr.isAutoPlay = false;
					global::Char.myCharz().mobFocus = null;
					if (GameCanvas.gameTick % 50 == 0)
					{
						AutoMap.StartRunToMapId(num);
						return;
					}
				}
			}
			else
			{
				if (AutoTrain.isMeOutOfMP())
				{
					return;
				}
				if (TileMap.mapID != AutoTrain.gobackMapID)
				{
					GameScr.isAutoPlay = false;
					AutoMap.StartRunToMapId(AutoTrain.gobackMapID);
				}
				if (TileMap.mapID == AutoTrain.gobackMapID)
				{
					if (!AutoTrain.isGobackCoordinate && GameCanvas.gameTick % 100 == 0)
					{
						GameScr.isAutoPlay = true;
					}
					if (TileMap.zoneID != AutoTrain.gobackZoneID && !global::Char.ischangingMap && !Controller.isStopReadMessage && GameCanvas.gameTick % 100 == 0)
					{
						Service.gI().requestChangeZone(AutoTrain.gobackZoneID, -1);
					}
					if (AutoTrain.isGobackCoordinate && (global::Char.myCharz().cx != AutoTrain.gobackX || global::Char.myCharz().cy != AutoTrain.gobackY) && GameCanvas.gameTick % 100 == 0)
					{
						AutoTrain.TeleportTo(AutoTrain.gobackX, AutoTrain.gobackY);
					}
				}
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000A5638 File Offset: 0x000A3838
		private static Mob currentLockedTarget;

		private static bool IsValidTarget(Mob mob)
		{
			if (mob == null || mob.isMobMe || mob.hp <= 0L || mob.status == 0 || mob.status == 1 || mob.x <= 0 || mob.y <= 0)
			{
				return false;
			}
			if (!AutoTrain.isMeCanAttack(mob))
			{
				return false;
			}
			if (AutoTrain.listMobIds.Count > 0 && !AutoTrain.listMobIds.Contains(mob.mobId))
			{
				return false;
			}
			bool isMobInMap = false;
			if (GameScr.vMob != null)
			{
				for (int i = 0; i < GameScr.vMob.size(); i++)
				{
					if (GameScr.vMob.elementAt(i) == mob)
					{
						isMobInMap = true;
						break;
					}
				}
			}
			return isMobInMap;
		}

		private static Mob GetNextMob()
		{
			if (GameScr.vMob == null || GameScr.vMob.size() == 0)
			{
				AutoTrain.currentLockedTarget = null;
				return null;
			}
			global::Char me = global::Char.myCharz();
			if (me == null)
			{
				AutoTrain.currentLockedTarget = null;
				return null;
			}

			// TARGET LOCKING: Nếu đang khóa mục tiêu hợp lệ -> giữ nguyên, không đổi
			if (AutoTrain.currentLockedTarget != null && AutoTrain.IsValidTarget(AutoTrain.currentLockedTarget))
			{
				return AutoTrain.currentLockedTarget;
			}
			AutoTrain.currentLockedTarget = null;

			// Tìm mục tiêu mới: ưu tiên quái CHƯA AI ĐÁNH (full HP) + GẦN NHẤT
			Mob bestFullHpMob = null;
			int bestFullHpDist = int.MaxValue;
			Mob bestNearestMob = null;
			int bestNearestDist = int.MaxValue;

			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				if (!AutoTrain.IsValidTarget(mob))
				{
					continue;
				}
				int dist = Res.abs(me.cx - mob.x) + Res.abs(me.cy - mob.y);

				// Nhóm 1: Quái đầy máu (chưa ai đánh) - ưu tiên tuyệt đối
				if (mob.hp >= mob.maxHp)
				{
					if (dist < bestFullHpDist)
					{
						bestFullHpDist = dist;
						bestFullHpMob = mob;
					}
				}

				// Nhóm 2: Quái gần nhất (fallback)
				if (dist < bestNearestDist)
				{
					bestNearestDist = dist;
					bestNearestMob = mob;
				}
			}

			// Ưu tiên quái full HP gần nhất; nếu không có thì chọn quái gần nhất bất kỳ
			AutoTrain.currentLockedTarget = (bestFullHpMob != null) ? bestFullHpMob : bestNearestMob;
			return AutoTrain.currentLockedTarget;
		}

		private static long lastTimeAttackMob;
		private static long lastTimeTeleportMob;
		private static Skill lastSelectedSkill;

		// Token: 0x06000B40 RID: 2880 RVA: 0x000A573C File Offset: 0x000A393C
		private static void DoIt()
		{
			global::Char me = global::Char.myCharz();
			if (me == null || me.meDead || me.cHP <= 0L || me.statusMe == 14 || me.statusMe == 5)
			{
				AutoTrain.isLockAir = false;
				AutoTrain.lockAirY = 0;
				return;
			}
			if (!Session_ME.gI().isConnected() || global::Char.ischangingMap)
			{
				AutoTrain.isLockAir = false;
				AutoTrain.lockAirY = 0;
				if (me != null)
				{
					me.mobFocus = null;
				}
				AutoTrain.currentLockedTarget = null;
				return;
			}
			if (AutoTrain.listMobIds.Count == 0)
			{
				if (mSystem.currentTimeMillis() - AutoTrain.lastTimeAddNewMob > 5000L)
				{
					AutoTrain.lastTimeAddNewMob = mSystem.currentTimeMillis();
					GameScr.info1.addInfo("Danh Sách Tàn Sát Trống!", 0);
				}
				AutoTrain.isAutoTrain = false;
				AutoTrain.isLockAir = false;
				AutoTrain.lockAirY = 0;
				return;
			}
			// Xóa target cũ nếu không hợp lệ
			if (me.mobFocus != null && !AutoTrain.IsValidTarget(me.mobFocus))
			{
				me.mobFocus = null;
				AutoTrain.currentLockedTarget = null;
				AutoTrain.isLockAir = false;
				AutoTrain.lockAirY = 0;
			}
			if (me.mobFocus == null)
			{
				if (!GameScr.canAutoPlay && AutoPick.isAutoPick)
				{
					AutoPick.FocusToNearestItem();
					if (me.itemFocus != null)
					{
						AutoPick.PickIt();
						AutoPick.FocusToNearestItem();
					}
				}
				else
				{
					me.itemFocus = null;
				}
				if (me.itemFocus == null)
				{
					me.mobFocus = AutoTrain.GetNextMob();
				}
			}
			if (me.mobFocus == null)
			{
				AutoTrain.isLockAir = false;
				AutoTrain.lockAirY = 0;
				if (AutoTrain.isAutoChangeZoneWhenNoMobs)
				{
					AutoTrain.CheckAndChangeZoneWhenNoMobs();
				}
				return;
			}
			Mob targetMob = me.mobFocus;
			if (!AutoTrain.IsValidTarget(targetMob))
			{
				me.mobFocus = null;
				AutoTrain.currentLockedTarget = null;
				AutoTrain.isLockAir = false;
				AutoTrain.lockAirY = 0;
				if (AutoTrain.isAutoChangeZoneWhenNoMobs)
				{
					AutoTrain.CheckAndChangeZoneWhenNoMobs();
				}
				return;
			}

			AutoTrain.timeNoMobsDetected = 0L;

			// Tìm skill sẵn sàng sử dụng (ưu tiên skill cooldown cao nhất đã hồi xong)
			Skill skill = null;
			long now = mSystem.currentTimeMillis();
			for (int i = 0; i < GameScr.keySkill.Length; i++)
			{
				Skill s = GameScr.keySkill[i];
				if (s != null && s.template != null && (s.template.isAttackSkill() || s.template.isSkillSpec()) && AutoTrain.IsSkillSelected(s))
				{
					long sCoolDown = (s.coolDown > 0) ? (long)s.coolDown : 200L;
					sCoolDown += 150L; // Ping buffer
					if (now - s.lastTimeUseThisSkill >= sCoolDown)
					{
						s.paintCanNotUseSkill = false;
						int num = (int)((s.template.manaUseType == 2) ? 1L : ((s.template.manaUseType == 1) ? ((long)s.manaUse * me.cMPFull / 100L) : ((long)s.manaUse)));
						if (me.cMP >= (long)num)
						{
							if (skill == null)
							{
								skill = s;
							}
							else if (skill.coolDown < s.coolDown)
							{
								skill = s;
							}
						}
					}
				}
			}
			if (skill == null && me.myskill != null && me.myskill.template != null && me.myskill.template.isAttackSkill())
			{
				long sCoolDown = (me.myskill.coolDown > 0) ? (long)me.myskill.coolDown : 200L;
				sCoolDown += 150L; // Ping buffer
				if (now - me.myskill.lastTimeUseThisSkill >= sCoolDown)
				{
					me.myskill.paintCanNotUseSkill = false;
					skill = me.myskill;
				}
			}

			if (skill == null)
			{
				return; // Chưa có skill sẵn sàng, chờ cooldown
			}

			bool isMelee = (skill.dx <= 60);
			int maxRangeX = isMelee ? 35 : ((skill.dx > 0) ? skill.dx : 100);
			int maxRangeY = isMelee ? 35 : ((skill.dy > 0) ? skill.dy : 60);

			int distX = Res.abs(me.cx - targetMob.x);
			int distY = Res.abs(me.cy - targetMob.y);

			// Di chuyển tới vị trí đánh nếu ngoài tầm
			if (distX > maxRangeX || distY > maxRangeY)
			{
				if (now - AutoTrain.lastTimeTeleportMob < 80L)
				{
					return; // Throttle để không spam quá nhiều gói di chuyển
				}
				AutoTrain.lastTimeTeleportMob = now;

				int targetX = targetMob.x;
				int targetY = targetMob.y;

				if (isMelee)
				{
					int offsetX = (me.cx < targetMob.x) ? -24 : 24;
					targetX = targetMob.x + offsetX;
				}
				else
				{
					int offsetX = (me.cx < targetMob.x) ? -(maxRangeX - 30) : (maxRangeX - 30);
					targetX = targetMob.x + offsetX;
				}

				// Kiểm tra có thể đánh từ mặt đất không
				int groundY = AutoMap.GetYGround(targetMob.x);
				bool canHitFromGround = (groundY > 0 && Res.abs(groundY - targetMob.y) <= (maxRangeY - 10));

				if (canHitFromGround)
				{
					targetY = groundY;
					AutoTrain.isLockAir = false;
					AutoTrain.lockAirY = 0;
				}
				else
				{
					targetY = targetMob.y;
					AutoTrain.isLockAir = true;
					AutoTrain.lockAirY = targetY;
				}

				// Di chuyển: dùng 1 gói charMove duy nhất cho khoảng cách ngắn, TeleportTo cho xa
				if (distX > 200 || distY > 200)
				{
					AutoMap.TeleportTo(targetX, targetY);
				}
				else
				{
					me.currentMovePoint = null;
					me.cx = targetX;
					me.cy = targetY;
					me.cxSend = -1;
					me.cySend = -1;
					Service.gI().charMove();
				}

				me.cdir = (me.cx <= targetMob.x) ? 1 : -1;
				me.cvy = 0;
				me.cvx = 0;
				me.delayFall = 0;
				if (AutoTrain.isLockAir)
				{
					me.statusMe = 4;
					me.cf = 8;
				}
				else if (canHitFromGround)
				{
					me.statusMe = 1;
				}

				// KHÔNG RETURN: Tiếp tục tấn công ngay trong cùng frame sau khi di chuyển
				// Cập nhật lại khoảng cách sau khi dịch chuyển
				distX = Res.abs(me.cx - targetMob.x);
				distY = Res.abs(me.cy - targetMob.y);

				// Nếu sau khi dịch chuyển vẫn ngoài tầm (trường hợp hiếm), thì mới return
				if (distX > maxRangeX + 10 || distY > maxRangeY + 10)
				{
					return;
				}
			}

			// === ĐANG TRONG TẦM ĐÁNH ===

			// Quay mặt về phía quái
			me.cdir = (me.cx <= targetMob.x) ? 1 : -1;

			// Khóa trên không nếu cần
			int groundYHere = AutoMap.GetYGround(me.cx);
			bool canHitFromGroundHere = (groundYHere > 0 && Res.abs(groundYHere - targetMob.y) <= (maxRangeY - 10));
			if (!canHitFromGroundHere)
			{
				AutoTrain.isLockAir = true;
				if (AutoTrain.lockAirY <= 0)
				{
					AutoTrain.lockAirY = me.cy;
				}
				me.statusMe = 4;
				me.cvy = 0;
				me.cvx = 0;
				me.delayFall = 0;
			}
			else
			{
				AutoTrain.isLockAir = false;
				AutoTrain.lockAirY = 0;
			}

			// Đổi skill nếu cần (gửi packet selectSkill)
			if (me.myskill != skill)
			{
				me.myskill = skill;
				AutoTrain.lastSelectedSkill = skill;
				GameScr.gI().lastSkill = skill;
				Service.gI().selectSkill((int)skill.template.id);
			}

			// TẤN CÔNG NGAY LẬP TỨC
			if (AutoTrain.IsSkill1OrSkill2(skill))
			{
				AutoTrain.PerformSkillWithoutAnimation(me, skill, targetMob);
			}
			else if (me.skillPaint == null)
			{
				if (skill.template.isSkillSpec())
				{
					me.sendNewAttack((short)skill.template.id);
				}
				else
				{
					GameScr.gI().doFire(false, true);
				}
			}
		}

		// Token: 0x040015D2 RID: 5586
		private static AutoTrain _Instance;

		// Token: 0x040015D3 RID: 5587
		private static bool isAvoidSuperMob;

		// Token: 0x040015D4 RID: 5588
		private static bool isGoBack;

		// Token: 0x040015D5 RID: 5589
		private static bool isGobackCoordinate;

		// Token: 0x040015D6 RID: 5590
		private static int gobackX;

		// Token: 0x040015D7 RID: 5591
		private static int gobackY;

		// Token: 0x040015D8 RID: 5592
		private static int gobackMapID;

		// Token: 0x040015D9 RID: 5593
		private static int gobackZoneID;

		// Token: 0x040015DA RID: 5594
		public static bool isAutoTrain;

		public static bool isAvoidBossInZone;

		public static long lastTimeChangeZoneAvoidBoss;

		// Token: 0x040015DB RID: 5595
		private static int minimumMPGoHome;

		// Token: 0x040015DC RID: 5596
		private static string[] inputMPPercentGoHome;

		// Token: 0x040015DD RID: 5597
		public static List<int> listMobIds = new List<int>();

		// Token: 0x040015DE RID: 5598
		public static long lastTimeAddNewMob;

		// Token: 0x040015DF RID: 5599
		private static long lastTimeTeleportToMob;
	}
}
