using System;
using System.Collections.Generic;

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

		private static Mob FindTargetMobBoss()
		{
			if (GameScr.vMob == null) return null;

			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				if (mob == null || mob.hp <= 0L || mob.status == 0 || mob.status == 1)
				{
					continue;
				}

				bool isMobBoss = mob.isBoss || mob.levelBoss > 0;
				if (!isMobBoss)
				{
					continue;
				}

				if (listBossTargets.Count == 0)
				{
					return mob;
				}

				string mobName = (mob.getTemplate() != null) ? mob.getTemplate().name : "";
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
			int distX = Res.abs(me.cx - boss.cx);
			int distY = Res.abs(me.cy - boss.cy);

			// Bám sát Boss: Nếu cách xa hơn 30px thì dịch chuyển ngay tới cạnh Boss
			if (distX > 30 || distY > 30)
			{
				if (mSystem.currentTimeMillis() - lastTimeTeleportBoss > 150L)
				{
					lastTimeTeleportBoss = mSystem.currentTimeMillis();
					if (distX > 200 || distY > 200)
					{
						AutoMap.TeleportTo(boss.cx, boss.cy);
					}
					else
					{
						me.cx = boss.cx;
						me.cy = boss.cy;
						me.statusMe = 3;
						me.cxSend = -1;
						me.cySend = -1;
						Service.gI().charMove();
					}
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
			int distX = Res.abs(me.cx - boss.x);
			int distY = Res.abs(me.cy - boss.y);

			// Bám sát Mob Boss: Nếu cách xa hơn 30px thì dịch chuyển ngay tới cạnh Boss
			if (distX > 30 || distY > 30)
			{
				if (mSystem.currentTimeMillis() - lastTimeTeleportBoss > 150L)
				{
					lastTimeTeleportBoss = mSystem.currentTimeMillis();
					if (distX > 200 || distY > 200)
					{
						AutoMap.TeleportTo(boss.x, boss.y);
					}
					else
					{
						me.cx = boss.x;
						me.cy = boss.y;
						me.statusMe = 3;
						me.cxSend = -1;
						me.cySend = -1;
						Service.gI().charMove();
					}
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
			myVector.addElement(new Command("Auto Đánh Boss\n" + (isAutoAttackBoss ? "[STATUS: ON]" : "[STATUS: OFF]"), getInstance(), 1, null));
			myVector.addElement(new Command("Nhập Tên Boss", getInstance(), 2, null));
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
