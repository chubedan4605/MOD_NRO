using System;
using System.Collections.Generic;
using Assets.src.g;

namespace Mod.DungPham.KoiOctiiu957
{
	public class AutoBoss : IActionListener, IChatable
	{
		private static AutoBoss _Instance;

		public static AutoBoss getInstance()
		{
			if (_Instance == null)
			{
				_Instance = new AutoBoss();
			}
			return _Instance;
		}

		public static bool isAutoAttackBoss = false;
		public static List<string> listBossTargets = new List<string>();
		private static long lastTimeAttackBoss;
		private static long lastTimeTeleportBoss;

		public static string[] inputBossName = new string[]
		{
			"Nhập tên Boss",
			"Tên Boss"
		};

		static AutoBoss()
		{
			LoadData();
		}

		public static void LoadData()
		{
			try
			{
				string saved = Rms.loadRMSString("AutoBossTargetList");
				if (!string.IsNullOrEmpty(saved))
				{
					listBossTargets.Clear();
					string[] names = saved.Split(',');
					foreach (string name in names)
					{
						string trimmed = name.Trim();
						if (!string.IsNullOrEmpty(trimmed) && !listBossTargets.Contains(trimmed))
						{
							listBossTargets.Add(trimmed);
						}
					}
				}
			}
			catch { }
		}

		public static void SaveData()
		{
			try
			{
				string saved = string.Join(",", listBossTargets.ToArray());
				Rms.saveRMSString("AutoBossTargetList", saved);
			}
			catch { }
		}

		public static void Update()
		{
			if (!isAutoAttackBoss)
			{
				return;
			}

			global::Char me = global::Char.myCharz();
			if (me == null || me.meDead || me.cHP <= 0L || me.statusMe == 14 || me.statusMe == 5)
			{
				return;
			}

			// 1. Tìm Boss Char trong map
			global::Char targetCharBoss = FindTargetCharBoss();
			if (targetCharBoss != null)
			{
				me.charFocus = targetCharBoss;
				StickAndAttackChar(me, targetCharBoss);
				return;
			}

			// 2. Nếu không có Char Boss, tìm Mob Boss trong map
			Mob targetMobBoss = FindTargetMobBoss();
			if (targetMobBoss != null)
			{
				me.mobFocus = targetMobBoss;
				StickAndAttackMob(me, targetMobBoss);
				return;
			}
			else if (me.mobFocus != null && (me.mobFocus.isMobMe || IsMobEgg(me.mobFocus)))
			{
				me.mobFocus = null;
			}
		}

		private static global::Char FindTargetCharBoss()
		{
			if (GameScr.vCharInMap == null) return null;

			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char c = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (c == null || c.meDead || c.cHP <= 0L || c.statusMe == 14 || c.statusMe == 5 || c.isPet || c.isMiniPet)
				{
					continue;
				}

				if (!MainMod.isBoss(c))
				{
					continue;
				}

				if (c.cName != null && c.cName.ToLower().Contains("alien"))
				{
					continue;
				}

				if (listBossTargets.Count == 0)
				{
					return c;
				}

				for (int j = 0; j < listBossTargets.Count; j++)
				{
					if (!string.IsNullOrEmpty(listBossTargets[j]) && c.cName != null && c.cName.ToLower().Contains(listBossTargets[j].ToLower()))
					{
						return c;
					}
				}
			}
			return null;
		}

		public static bool IsMobEgg(Mob mob)
		{
			if (mob == null)
			{
				return false;
			}
			if (mob.isMobMe)
			{
				return true;
			}
			global::Char myChar = global::Char.myCharz();
			if (myChar != null && myChar.mobMe == mob)
			{
				return true;
			}
			if (GameScr.vCharInMap != null)
			{
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					global::Char c = (global::Char)GameScr.vCharInMap.elementAt(i);
					if (c != null && (c.mobMe == mob || (mob.mobId == c.charID && (mob.templateId == 25 || mob.templateId == 26 || mob.templateId == 27))))
					{
						return true;
					}
				}
			}
			return false;
		}

		public static bool IsRegularMob(Mob mob)
		{
			if (mob == null || IsMobEgg(mob))
			{
				return true;
			}
			if (mob is BigBoss || mob is BigBoss2 || mob is BachTuoc || mob is NewBoss)
			{
				return false;
			}

			int id = mob.templateId;
			// Quái thường luyện tập / up sức mạnh (không phải Boss)
			if ((id >= 0 && id <= 27) || // Mộc nhân -> Akkuman
				(id >= 28 && id <= 38) || // Thằn lằn bay 2 -> Robot thép
				(id >= 43 && id <= 66) || // Thằn lằn xanh -> Tai tím (bao gồm Khỉ lông đen/vàng, Xên con 1-8)
				id == 69 || // Da xanh
				(id >= 73 && id <= 76) || // Kawazu, Kinkarn, Arbee, Cỗ máy hủy diệt
				(id >= 78 && id <= 81) || // Khỉ lông xanh, Taburine đỏ, Cabira, Tobi
				(id >= 86 && id <= 91) || // Ếch mắt đỏ -> Quỷ vàng
				id == 94 || id == 95 || id == 96) // Máy đo sức mạnh, Caéc M, Quái mới
			{
				return true;
			}

			string name = (mob.getTemplate() != null && mob.getTemplate().name != null) ? mob.getTemplate().name.ToLower() : "";
			if (name.Contains("alien") || name.Contains("heo") || name.Contains("khủng long") || name.Contains("lợn") ||
				name.Contains("quỷ đất") || name.Contains("thằn lằn") || name.Contains("phi long") || name.Contains("quỷ bay") ||
				name.Contains("ốc sên") || name.Contains("bulon") || name.Contains("ukulele") || name.Contains("quỷ mập") ||
				name.Contains("xên con") || name.Contains("khỉ lông") || name.Contains("mộc nhân") || name.Contains("sói xám") ||
				name.Contains("robot bay") || name.Contains("robot thép") || name.Contains("cá sấu") || name.Contains("dơi da xanh") ||
				name.Contains("quỷ chim") || name.Contains("lính đầu trọc") || name.Contains("lính tai dài") || name.Contains("lính vũ trụ"))
			{
				return true;
			}

			return false;
		}

		public static bool IsBossMob(Mob mob)
		{
			if (mob == null || mob.isMobMe || IsMobEgg(mob) || IsRegularMob(mob))
			{
				return false;
			}

			if (mob is BigBoss || mob is BigBoss2 || mob is BachTuoc || mob is NewBoss)
			{
				return true;
			}

			int id = mob.templateId;
			// Danh sách Mob Boss chính thức trong NRO:
			// 39: Nappa, 40: Soldier, 41: Appule, 42: Raspberry
			// 67: Abo, 68: Kado
			// 70: Hirudegarn, 71: Vua Bạch Tuộc, 72: Rôbốt bảo vệ, 77: Gấu tướng cướp
			// 82: Voi Chín Ngà, 83: Gà Chín Cựa, 84: Ngựa Chín Mao, 85: Piano
			// 92: Godzilla, 93: Kong
			if ((id >= 39 && id <= 42) ||
				id == 67 || id == 68 ||
				id == 70 || id == 71 || id == 72 || id == 77 ||
				id == 82 || id == 83 || id == 84 || id == 85 ||
				id == 92 || id == 93)
			{
				return true;
			}

			if (mob.isBoss)
			{
				return true;
			}

			return false;
		}

		private static Mob FindTargetMobBoss()
		{
			if (GameScr.vMob == null) return null;

			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				if (mob == null || mob.hp <= 0L || mob.status == 0 || mob.status == 1 || mob.isMobMe || IsMobEgg(mob))
				{
					continue;
				}

				if (IsRegularMob(mob))
				{
					continue;
				}

				if (!IsBossMob(mob))
				{
					continue;
				}

				string mobName = (mob.getTemplate() != null && mob.getTemplate().name != null) ? mob.getTemplate().name : "";

				if (listBossTargets.Count == 0)
				{
					return mob;
				}

				for (int j = 0; j < listBossTargets.Count; j++)
				{
					if (!string.IsNullOrEmpty(listBossTargets[j]) && mobName.ToLower().Contains(listBossTargets[j].ToLower()))
					{
						return mob;
					}
				}
			}
			return null;
		}

		private static void StickAndAttackChar(global::Char me, global::Char boss)
		{
			int maxRangeX = 35;
			int maxRangeY = 35;
			if (me.myskill != null && me.myskill.template != null)
			{
				bool isMelee = (me.myskill.template.type == 1);
				maxRangeX = isMelee ? 35 : ((me.myskill.dx > 0) ? me.myskill.dx : 60);
				maxRangeY = isMelee ? 35 : ((me.myskill.dy > 0) ? me.myskill.dy : 60);
			}

			int distX = Res.abs(me.cx - boss.cx);
			int distY = Res.abs(me.cy - boss.cy);

			// Bám sát Boss: Nếu cách xa hơn maxRange thì dịch chuyển ngay tới cạnh Boss
			if (distX > maxRangeX || distY > maxRangeY)
			{
				if (mSystem.currentTimeMillis() - lastTimeTeleportBoss > 150L)
				{
					lastTimeTeleportBoss = mSystem.currentTimeMillis();
					
					int targetX = boss.cx;
					int targetY = boss.cy;

					int groundY = AutoMap.GetYGround(targetX);
					bool canHitFromGround = (groundY > 0 && Res.abs(groundY - boss.cy) <= (maxRangeY - 10));

					if (canHitFromGround)
					{
						targetY = groundY;
						AutoTrain.isLockAir = false;
						AutoTrain.lockAirY = 0;
					}
					else
					{
						targetY = boss.cy;
						AutoTrain.isLockAir = true;
						AutoTrain.lockAirY = targetY;
					}

					if (distX > 200 || distY > 200)
					{
						AutoMap.TeleportTo(targetX, targetY);
					}
					else
					{
						me.cx = targetX;
						me.cy = targetY;
						me.statusMe = 3;
						me.cxSend = -1;
						me.cySend = -1;
						Service.gI().charMove();
					}
					
					if (AutoTrain.isLockAir)
					{
						me.statusMe = 4;
						me.cf = 8;
					}
					else if (canHitFromGround)
					{
						me.statusMe = 1;
					}
				}
			}
			else
			{
				int groundYHere = AutoMap.GetYGround(me.cx);
				bool canHitFromGroundHere = (groundYHere > 0 && Res.abs(groundYHere - boss.cy) <= (maxRangeY - 10));
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
					if (me.skillPaint == null)
					{
						me.cf = 8;
					}
				}
				else
				{
					AutoTrain.isLockAir = false;
				}
			}

			// Tấn công Boss
			if (me.myskill != null)
			{
				long coolDown = (long)me.myskill.coolDown;
				if (coolDown < 300) coolDown = 300;
				if (mSystem.currentTimeMillis() - lastTimeAttackBoss > coolDown)
				{
					lastTimeAttackBoss = mSystem.currentTimeMillis();
					me.myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
					MyVector myVector = new MyVector();
					myVector.addElement(boss);
					Service.gI().sendPlayerAttack(new MyVector(), myVector, 2);
				}
			}
		}

		private static void StickAndAttackMob(global::Char me, Mob boss)
		{
			int maxRangeX = 35;
			int maxRangeY = 35;
			if (me.myskill != null && me.myskill.template != null)
			{
				bool isMelee = (me.myskill.template.type == 1);
				maxRangeX = isMelee ? 35 : ((me.myskill.dx > 0) ? me.myskill.dx : 60);
				maxRangeY = isMelee ? 35 : ((me.myskill.dy > 0) ? me.myskill.dy : 60);
			}

			int distX = Res.abs(me.cx - boss.x);
			int distY = Res.abs(me.cy - boss.y);

			// Bám sát Mob Boss: Nếu cách xa hơn maxRange thì dịch chuyển ngay tới cạnh Boss
			if (distX > maxRangeX || distY > maxRangeY)
			{
				if (mSystem.currentTimeMillis() - lastTimeTeleportBoss > 150L)
				{
					lastTimeTeleportBoss = mSystem.currentTimeMillis();
					
					int targetX = boss.x;
					int targetY = boss.y;

					int groundY = AutoMap.GetYGround(targetX);
					bool canHitFromGround = (groundY > 0 && Res.abs(groundY - boss.y) <= (maxRangeY - 10));

					if (canHitFromGround)
					{
						targetY = groundY;
						AutoTrain.isLockAir = false;
						AutoTrain.lockAirY = 0;
					}
					else
					{
						targetY = boss.y;
						AutoTrain.isLockAir = true;
						AutoTrain.lockAirY = targetY;
					}

					if (distX > 200 || distY > 200)
					{
						AutoMap.TeleportTo(targetX, targetY);
					}
					else
					{
						me.cx = targetX;
						me.cy = targetY;
						me.statusMe = 3;
						me.cxSend = -1;
						me.cySend = -1;
						Service.gI().charMove();
					}
					
					if (AutoTrain.isLockAir)
					{
						me.statusMe = 4;
						me.cf = 8;
					}
					else if (canHitFromGround)
					{
						me.statusMe = 1;
					}
				}
			}
			else
			{
				int groundYHere = AutoMap.GetYGround(me.cx);
				bool canHitFromGroundHere = (groundYHere > 0 && Res.abs(groundYHere - boss.y) <= (maxRangeY - 10));
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
					if (me.skillPaint == null)
					{
						me.cf = 8;
					}
				}
				else
				{
					AutoTrain.isLockAir = false;
				}
			}

			// Tấn công Mob Boss
			if (me.myskill != null)
			{
				long coolDown = (long)me.myskill.coolDown;
				if (coolDown < 300) coolDown = 300;
				if (mSystem.currentTimeMillis() - lastTimeAttackBoss > coolDown)
				{
					lastTimeAttackBoss = mSystem.currentTimeMillis();
					me.myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
					MyVector myVector = new MyVector();
					myVector.addElement(boss);
					Service.gI().sendPlayerAttack(myVector, new MyVector(), 1);
				}
			}
		}

		public void onChatFromMe(string text, string to)
		{
			if (ChatTextField.gI().tfChat.getText() != null && !ChatTextField.gI().tfChat.getText().Equals(string.Empty) && !text.Equals(string.Empty) && text != null)
			{
				if (ChatTextField.gI().strChat.Equals(inputBossName[0]))
				{
					string bossName = ChatTextField.gI().tfChat.getText().Trim();
					if (!string.IsNullOrEmpty(bossName))
					{
						AddBossTarget(bossName);
					}
					ResetChatTextField();
					return;
				}
			}
			else
			{
				ChatTextField.gI().isShow = false;
			}
		}

		public void onCancelChat()
		{
		}

		public static void AddBossTarget(string bossName)
		{
			if (!listBossTargets.Contains(bossName))
			{
				listBossTargets.Add(bossName);
				SaveData();
				GameScr.info1.addInfo("Đã thêm Boss: " + bossName, 0);
			}
			else
			{
				GameScr.info1.addInfo("Boss " + bossName + " đã có trong danh sách", 0);
			}
		}

		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 1:
				isAutoAttackBoss = !isAutoAttackBoss;
				GameScr.info1.addInfo("Auto Đánh Boss\n" + (isAutoAttackBoss ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 2:
				ChatTextField.gI().strChat = inputBossName[0];
				ChatTextField.gI().tfChat.name = inputBossName[1];
				ChatTextField.gI().startChat2(getInstance(), string.Empty);
				return;
			case 3:
				global::Char me = global::Char.myCharz();
				if (me != null && me.charFocus != null && !string.IsNullOrEmpty(me.charFocus.cName))
				{
					AddBossTarget(me.charFocus.cName);
				}
				else if (me != null && me.mobFocus != null && me.mobFocus.getTemplate() != null)
				{
					if (IsMobEgg(me.mobFocus))
					{
						GameScr.info1.addInfo("Không thể thêm quái đẻ trứng Namec!", 0);
						return;
					}
					AddBossTarget(me.mobFocus.getTemplate().name);
				}
				else
				{
					GameScr.info1.addInfo("Chưa chọn mục tiêu!", 0);
				}
				return;
			case 4:
				ShowMenuListBoss();
				return;
			case 5:
				listBossTargets.Clear();
				SaveData();
				GameScr.info1.addInfo("Đã xóa hết DS Boss!\n(Auto đánh mọi Boss)", 0);
				return;
			case 6:
				string clip = UnityEngine.GUIUtility.systemCopyBuffer;
				if (!string.IsNullOrEmpty(clip))
				{
					clip = clip.Trim();
					AddBossTarget(clip);
				}
				else
				{
					GameScr.info1.addInfo("Clipboard trống!", 0);
				}
				return;
			case 10:
				if (p is string)
				{
					string name = (string)p;
					listBossTargets.Remove(name);
					SaveData();
					GameScr.info1.addInfo("Đã xóa Boss: " + name, 0);
				}
				return;
			default:
				return;
			}
		}

		public static void ShowMenu()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Auto Đánh Boss [Z]\n" + (isAutoAttackBoss ? "[STATUS: ON]" : "[STATUS: OFF]"), getInstance(), 1, null));
			myVector.addElement(new Command("Nhập Tên Boss", getInstance(), 2, null));
			myVector.addElement(new Command("Dán Boss Từ Clipboard", getInstance(), 6, null));
			myVector.addElement(new Command("Thêm Boss Đang Chọn", getInstance(), 3, null));
			myVector.addElement(new Command("DS Boss Mục Tiêu (" + listBossTargets.Count + ")", getInstance(), 4, null));
			if (listBossTargets.Count > 0)
			{
				myVector.addElement(new Command("Xóa Hết DS Boss", getInstance(), 5, null));
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		private static void ShowMenuListBoss()
		{
			MyVector myVector = new MyVector();
			if (listBossTargets.Count == 0)
			{
				GameScr.info1.addInfo("DS Boss trống (Đang đánh mọi Boss)", 0);
				return;
			}
			for (int i = 0; i < listBossTargets.Count; i++)
			{
				string name = listBossTargets[i];
				myVector.addElement(new Command("Xóa: " + name, getInstance(), 10, name));
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		private static void ResetChatTextField()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}
	}
}
