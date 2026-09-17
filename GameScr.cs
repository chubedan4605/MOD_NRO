using System;
using Assets.src.g;
using Mod.DungPham.KoiOctiiu957;

// Token: 0x020000AA RID: 170
public class GameScr : mScreen, IChatable
{
	// Token: 0x0600071B RID: 1819 RVA: 0x000611CC File Offset: 0x0005F3CC
	public GameScr()
	{
		if (GameCanvas.w == 128 || GameCanvas.h <= 208)
		{
			GameScr.indexSize = 20;
		}
		this.cmdback = new Command(string.Empty, 11021);
		this.cmdMenu = new Command("menu", 11000);
		this.cmdFocus = new Command(string.Empty, 11001);
		this.cmdMenu.img = GameScr.imgMenu;
		this.cmdMenu.w = mGraphics.getImageWidth(this.cmdMenu.img) + 20;
		this.cmdMenu.isPlaySoundButton = false;
		this.cmdFocus.img = GameScr.imgFocus;
		if (GameCanvas.isTouch)
		{
			this.cmdMenu.x = 0;
			this.cmdMenu.y = 50;
			this.cmdFocus = null;
		}
		else
		{
			this.cmdMenu.x = 0;
			this.cmdMenu.y = GameScr.gH - 30;
			this.cmdFocus.x = GameScr.gW - 32;
			this.cmdFocus.y = GameScr.gH - 32;
		}
		this.right = this.cmdFocus;
		GameScr.isPaintRada = 1;
		if (GameCanvas.isTouch)
		{
			GameScr.isHaveSelectSkill = true;
		}
		this.cmdDoiCo = new Command("Đổi cờ", GameCanvas.gI(), 100001, null);
		this.cmdLogOut = new Command("Logout", GameCanvas.gI(), 100002, null);
		this.cmdChatTheGioi = new Command("chat world", GameCanvas.gI(), 100003, null);
		this.cmdshowInfo = new Command("InfoLog", GameCanvas.gI(), 100004, null);
		this.cmdDoiCo.setType();
		this.cmdLogOut.setType();
		this.cmdChatTheGioi.setType();
		this.cmdshowInfo.setType();
		this.cmdChatTheGioi.x = GameCanvas.w - this.cmdChatTheGioi.w;
		this.cmdshowInfo.x = GameCanvas.w - this.cmdshowInfo.w;
		this.cmdLogOut.x = GameCanvas.w - this.cmdLogOut.w;
		this.cmdDoiCo.x = GameCanvas.w - this.cmdDoiCo.w;
		this.cmdChatTheGioi.y = this.cmdChatTheGioi.h + mFont.tahoma_7_white.getHeight();
		this.cmdshowInfo.y = this.cmdChatTheGioi.h * 2 + mFont.tahoma_7_white.getHeight();
		this.cmdLogOut.y = this.cmdChatTheGioi.h * 3 + mFont.tahoma_7_white.getHeight();
		this.cmdDoiCo.y = this.cmdChatTheGioi.h * 4 + mFont.tahoma_7_white.getHeight();
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x00061538 File Offset: 0x0005F738
	public static void loadBg()
	{
		GameScr.fra_PVE_Bar_0 = new FrameImage(mSystem.loadImage("/mainImage/i_pve_bar_0.png"), 6, 15);
		GameScr.fra_PVE_Bar_1 = new FrameImage(mSystem.loadImage("/mainImage/i_pve_bar_1.png"), 38, 21);
		GameScr.imgVS = mSystem.loadImage("/mainImage/i_vs.png");
		GameScr.imgBall = mSystem.loadImage("/mainImage/i_charlife.png");
		GameScr.imgHP_NEW = mSystem.loadImage("/mainImage/i_hp.png");
		GameScr.imgKhung = mSystem.loadImage("/mainImage/i_khung.png");
		GameScr.imgLbtn = GameCanvas.loadImage("/mainImage/myTexture2dbtnl.png");
		GameScr.imgLbtnFocus = GameCanvas.loadImage("/mainImage/myTexture2dbtnlf.png");
		GameScr.imgLbtn2 = GameCanvas.loadImage("/mainImage/myTexture2dbtnl2.png");
		GameScr.imgLbtnFocus2 = GameCanvas.loadImage("/mainImage/myTexture2dbtnlf2.png");
		GameScr.imgPanel = GameCanvas.loadImage("/mainImage/myTexture2dpanel.png");
		GameScr.imgPanel2 = GameCanvas.loadImage("/mainImage/panel2.png");
		GameScr.imgHP = GameCanvas.loadImage("/mainImage/myTexture2dHP.png");
		GameScr.imgSP = GameCanvas.loadImage("/mainImage/SP.png");
		GameScr.imgHPLost = GameCanvas.loadImage("/mainImage/myTexture2dhpLost.png");
		GameScr.imgMPLost = GameCanvas.loadImage("/mainImage/myTexture2dmpLost.png");
		GameScr.imgMP = GameCanvas.loadImage("/mainImage/myTexture2dMP.png");
		GameScr.imgSkill = GameCanvas.loadImage("/mainImage/myTexture2dskill.png");
		GameScr.imgSkill2 = GameCanvas.loadImage("/mainImage/myTexture2dskill2.png");
		GameScr.imgMenu = GameCanvas.loadImage("/mainImage/myTexture2dmenu.png");
		GameScr.imgFocus = GameCanvas.loadImage("/mainImage/myTexture2dfocus.png");
		GameScr.imgHP_tm_do = GameCanvas.loadImage("/mainImage/tm-do.png");
		GameScr.imgHP_tm_vang = GameCanvas.loadImage("/mainImage/tm-vang.png");
		GameScr.imgHP_tm_xam = GameCanvas.loadImage("/mainImage/tm-xam.png");
		GameScr.imgHP_tm_xanh = GameCanvas.loadImage("/mainImage/tm-xanh.png");
		GameScr.imgChatPC = GameCanvas.loadImage("/pc/chat.png");
		GameScr.imgChatsPC2 = GameCanvas.loadImage("/pc/chat2.png");
		if (GameCanvas.isTouch)
		{
			GameScr.imgArrow = GameCanvas.loadImage("/mainImage/myTexture2darrow.png");
			GameScr.imgArrow2 = GameCanvas.loadImage("/mainImage/myTexture2darrow2.png");
			GameScr.imgChat = GameCanvas.loadImage("/mainImage/myTexture2dchat.png");
			GameScr.imgChat2 = GameCanvas.loadImage("/mainImage/myTexture2dchat2.png");
			GameScr.imgFocus2 = GameCanvas.loadImage("/mainImage/myTexture2dfocus2.png");
			GameScr.imgHP1 = GameCanvas.loadImage("/mainImage/myTexture2dPea0.png");
			GameScr.imgHP2 = GameCanvas.loadImage("/mainImage/myTexture2dPea1.png");
			GameScr.imgAnalog1 = GameCanvas.loadImage("/mainImage/myTexture2danalog1.png");
			GameScr.imgAnalog2 = GameCanvas.loadImage("/mainImage/myTexture2danalog2.png");
			GameScr.imgHP3 = GameCanvas.loadImage("/mainImage/myTexture2dPea2.png");
			GameScr.imgHP4 = GameCanvas.loadImage("/mainImage/myTexture2dPea3.png");
			GameScr.imgFire0 = GameCanvas.loadImage("/mainImage/myTexture2dfirebtn0.png");
			GameScr.imgFire1 = GameCanvas.loadImage("/mainImage/myTexture2dfirebtn1.png");
		}
		GameScr.imgNR1 = GameCanvas.loadImage("/mainImage/myTexture2dPea_0.png");
		GameScr.imgNR2 = GameCanvas.loadImage("/mainImage/myTexture2dPea_1.png");
		GameScr.imgNR3 = GameCanvas.loadImage("/mainImage/myTexture2dPea_2.png");
		GameScr.imgNR4 = GameCanvas.loadImage("/mainImage/myTexture2dPea_3.png");
		GameScr.flyTextX = new int[5];
		GameScr.flyTextY = new int[5];
		GameScr.flyTextDx = new int[5];
		GameScr.flyTextDy = new int[5];
		GameScr.flyTextState = new int[5];
		GameScr.flyTextString = new string[5];
		GameScr.flyTextYTo = new int[5];
		GameScr.flyTime = new int[5];
		GameScr.flyTextColor = new int[8];
		for (int i = 0; i < 5; i++)
		{
			GameScr.flyTextState[i] = -1;
		}
		sbyte[] array = Rms.loadRMS("NRdataVersion");
		sbyte[] array2 = Rms.loadRMS("NRmapVersion");
		sbyte[] array3 = Rms.loadRMS("NRskillVersion");
		sbyte[] array4 = Rms.loadRMS("NRitemVersion");
		if (array != null)
		{
			GameScr.vcData = array[0];
		}
		if (array2 != null)
		{
			GameScr.vcMap = array2[0];
		}
		if (array3 != null)
		{
			GameScr.vcSkill = array3[0];
		}
		if (array4 != null)
		{
			GameScr.vcItem = array4[0];
		}
		GameScr.imgNut = GameCanvas.loadImage("/mainImage/myTexture2dnut.png");
		GameScr.imgNutF = GameCanvas.loadImage("/mainImage/myTexture2dnutF.png");
		MobCapcha.init();
		GameScr.isAnalog = ((Rms.loadRMSInt("analog") != 1) ? 0 : 1);
		GameScr.gamePad = new GamePad();
		GameScr.arrow = GameCanvas.loadImage("/mainImage/myTexture2darrow3.png");
		GameScr.imgTrans = GameCanvas.loadImage("/bg/trans.png");
		GameScr.imgRoomStat = GameCanvas.loadImage("/mainImage/myTexture2dstat.png");
		GameScr.frBarPow0 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor20.png");
		GameScr.frBarPow1 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor21.png");
		GameScr.frBarPow2 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor22.png");
		GameScr.frBarPow20 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor00.png");
		GameScr.frBarPow21 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor01.png");
		GameScr.frBarPow22 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor02.png");
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x000076D9 File Offset: 0x000058D9
	public void initSelectChar()
	{
		this.readPart();
		SmallImage.init();
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x000619A8 File Offset: 0x0005FBA8
	public static void paintOngMauPercent(Image img0, Image img1, Image img2, float x, float y, int size, float pixelPercent, mGraphics g)
	{
		int clipX = g.getClipX();
		int clipY = g.getClipY();
		int clipWidth = g.getClipWidth();
		int clipHeight = g.getClipHeight();
		g.setClip((int)x, (int)y, (int)pixelPercent, 13);
		int num = size / 15 - 2;
		for (int i = 0; i < num; i++)
		{
			g.drawImage(img1, x + (float)((i + 1) * 15), y, 0);
		}
		g.drawImage(img0, x, y, 0);
		g.drawImage(img1, x + (float)size - 30f, y, 0);
		g.drawImage(img2, x + (float)size - 15f, y, 0);
		g.setClip(clipX, clipY, clipWidth, clipHeight);
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x000076E6 File Offset: 0x000058E6
	public void initTraining()
	{
		if (CreateCharScr.isCreateChar)
		{
			CreateCharScr.isCreateChar = false;
			this.right = null;
		}
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x000076FF File Offset: 0x000058FF
	public bool isMapDocNhan()
	{
		return TileMap.mapID >= 53 && TileMap.mapID <= 62;
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x0000771C File Offset: 0x0000591C
	public bool isMapFize()
	{
		return TileMap.mapID >= 63;
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x00061A60 File Offset: 0x0005FC60
	public override void switchToMe()
	{
		GameScr.vChatVip.removeAllElements();
		ServerListScreen.isWait = false;
		if (BackgroudEffect.isHaveRain())
		{
			SoundMn.gI().rain();
		}
		LoginScr.isContinueToLogin = false;
		global::Char.isLoadingMap = false;
		if (!GameScr.isPaintOther)
		{
			Service.gI().finishLoadMap();
		}
		if (TileMap.isTrainingMap())
		{
			this.initTraining();
		}
		GameScr.info1.isUpdate = true;
		GameScr.info2.isUpdate = true;
		this.resetButton();
		GameScr.isLoadAllData = true;
		GameScr.isPaintOther = false;
		base.switchToMe();
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x00061AF0 File Offset: 0x0005FCF0
	public static int getMaxExp(int level)
	{
		int num = 0;
		for (int i = 0; i <= level; i++)
		{
			num += (int)GameScr.exps[i];
		}
		return num;
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00061B20 File Offset: 0x0005FD20
	public static void resetAllvector()
	{
		GameScr.vCharInMap.removeAllElements();
		Teleport.vTeleport.removeAllElements();
		GameScr.vItemMap.removeAllElements();
		Effect2.vEffect2.removeAllElements();
		Effect2.vAnimateEffect.removeAllElements();
		Effect2.vEffect2Outside.removeAllElements();
		Effect2.vEffectFeet.removeAllElements();
		Effect2.vEffect3.removeAllElements();
		GameScr.vMobAttack.removeAllElements();
		GameScr.vMob.removeAllElements();
		GameScr.vNpc.removeAllElements();
		global::Char.myCharz().vMovePoints.removeAllElements();
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x000045ED File Offset: 0x000027ED
	public void loadSkillShortcut()
	{
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x00061BAC File Offset: 0x0005FDAC
	public void onOSkill(sbyte[] oSkillID)
	{
		Cout.println("GET onScreenSkill!");
		GameScr.onScreenSkill = new Skill[10];
		if (AutoSkill.isSaveData)
		{
			sbyte[] savedSkillIDs = this.loadSkillShortcutFromRMS(StaticObj.SAVE_OSKILL);
			if (savedSkillIDs != null)
			{
				oSkillID = savedSkillIDs;
			}
		}
		if (oSkillID == null)
		{
			this.loadDefaultonScreenSkill();
		}
		else
		{
			for (int i = 0; i < oSkillID.Length; i++)
			{
				for (int j = 0; j < global::Char.myCharz().vSkillFight.size(); j++)
				{
					Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(j);
					if ((int)skill.template.id == (int)oSkillID[i])
					{
						GameScr.onScreenSkill[i] = skill;
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x00061C4C File Offset: 0x0005FE4C
	public void onKSkill(sbyte[] kSkillID)
	{
		Cout.println("GET KEYSKILL!");
		GameScr.keySkill = new Skill[10];
		if (AutoSkill.isSaveData)
		{
			sbyte[] savedSkillIDs = this.loadSkillShortcutFromRMS(StaticObj.SAVE_KEYKILL);
			if (savedSkillIDs != null)
			{
				kSkillID = savedSkillIDs;
			}
		}
		if (kSkillID == null)
		{
			this.loadDefaultKeySkill();
			return;
		}
		GameScr.info1.addInfo("load OnkSKill", 0);
		for (int i = 0; i < kSkillID.Length; i++)
		{
			for (int j = 0; j < global::Char.myCharz().vSkillFight.size(); j++)
			{
				Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(j);
				if (skill.template.id == kSkillID[i])
				{
					GameScr.keySkill[i] = skill;
					break;
				}
			}
		}
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x00061CE4 File Offset: 0x0005FEE4
	public void onCSkill(sbyte[] cSkillID)
	{
		Cout.println("GET CURRENTSKILL!");
		if (cSkillID == null || cSkillID.Length == 0)
		{
			if (global::Char.myCharz().vSkillFight.size() > 0)
			{
				global::Char.myCharz().myskill = (Skill)global::Char.myCharz().vSkillFight.elementAt(0);
			}
		}
		else
		{
			for (int i = 0; i < global::Char.myCharz().vSkillFight.size(); i++)
			{
				Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(i);
				if ((int)skill.template.id == (int)cSkillID[0])
				{
					global::Char.myCharz().myskill = skill;
					break;
				}
			}
		}
		if (global::Char.myCharz().myskill != null)
		{
			Service.gI().selectSkill((int)global::Char.myCharz().myskill.template.id);
			this.saveRMSCurrentSkill(global::Char.myCharz().myskill.template.id);
		}
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x00061DE8 File Offset: 0x0005FFE8
	private void loadDefaultonScreenSkill()
	{
		Cout.println("LOAD DEFAULT ONmScreen SKILL");
		int num = 0;
		while (num < GameScr.onScreenSkill.Length && num < global::Char.myCharz().vSkillFight.size())
		{
			Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(num);
			GameScr.onScreenSkill[num] = skill;
			num++;
		}
		if (global::Char.myCharz().cgender == 0 && GameScr.onScreenSkill.Length > 5)
		{
			Skill skill2 = GameScr.onScreenSkill[3];
			GameScr.onScreenSkill[3] = GameScr.onScreenSkill[5];
			GameScr.onScreenSkill[5] = skill2;
		}
		this.saveonScreenSkillToRMS();
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x00061E7C File Offset: 0x0006007C
	private void loadDefaultKeySkill()
	{
		Cout.println("LOAD DEFAULT KEY SKILL");
		for (int i = 0; i < GameScr.keySkill.Length; i++)
		{
			if (i >= global::Char.myCharz().vSkillFight.size())
			{
				break;
			}
			Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(i);
			GameScr.keySkill[i] = skill;
		}
		this.saveKeySkillToRMS();
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x00061EEC File Offset: 0x000600EC
	public void doSetOnScreenSkill(SkillTemplate skillTemplate)
	{
		Skill skill = global::Char.myCharz().getSkill(skillTemplate);
		MyVector myVector = new MyVector();
		for (int i = 0; i < 10; i++)
		{
			object[] p = new object[]
			{
				skill,
				i + string.Empty
			};
			Command command = new Command(mResources.into_place + (i + 1), 11120, p);
			Skill skill2 = GameScr.onScreenSkill[i];
			if (skill2 != null)
			{
				command.isDisplay = true;
			}
			myVector.addElement(command);
		}
		GameCanvas.menu.startAt(myVector, 0);
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x00061F88 File Offset: 0x00060188
	public void doSetKeySkill(SkillTemplate skillTemplate)
	{
		Cout.println("DO SET KEY SKILL");
		Skill skill = global::Char.myCharz().getSkill(skillTemplate);
		string[] array = (!TField.isQwerty) ? mResources.key_skill : mResources.key_skill_qwerty;
		MyVector myVector = new MyVector();
		for (int i = 0; i < 10; i++)
		{
			object[] p = new object[]
			{
				skill,
				i + string.Empty
			};
			myVector.addElement(new Command(array[i], 11121, p));
		}
		GameCanvas.menu.startAt(myVector, 0);
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x00062020 File Offset: 0x00060220
	public void saveonScreenSkillToRMS()
	{
		sbyte[] array = new sbyte[GameScr.onScreenSkill.Length];
		for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
		{
			if (GameScr.onScreenSkill[i] == null)
			{
				array[i] = -1;
			}
			else
			{
				array[i] = GameScr.onScreenSkill[i].template.id;
			}
		}
		this.saveSkillShortcutToRMS(StaticObj.SAVE_OSKILL, array);
		Service.gI().changeOnKeyScr(array);
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x00062088 File Offset: 0x00060288
	public void saveKeySkillToRMS()
	{
		sbyte[] array = new sbyte[GameScr.keySkill.Length];
		for (int i = 0; i < GameScr.keySkill.Length; i++)
		{
			if (GameScr.keySkill[i] == null)
			{
				array[i] = -1;
			}
			else
			{
				array[i] = GameScr.keySkill[i].template.id;
			}
		}
		this.saveSkillShortcutToRMS(StaticObj.SAVE_KEYKILL, array);
		Service.gI().changeOnKeyScr(array);
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x000045ED File Offset: 0x000027ED
	public void saveRMSCurrentSkill(sbyte id)
	{
	}

	private string getSkillShortcutRmsKey(string baseKey)
	{
		global::Char c = global::Char.myCharz();
		if (c == null || c.charID <= 0)
		{
			return baseKey;
		}
		return baseKey + "_" + c.charID.ToString();
	}

	private sbyte[] loadSkillShortcutFromRMS(string baseKey)
	{
		sbyte[] array = Rms.loadRMS(this.getSkillShortcutRmsKey(baseKey));
		if (array == null || array.Length == 0)
		{
			return null;
		}
		return array;
	}

	private void saveSkillShortcutToRMS(string baseKey, sbyte[] skillIDs)
	{
		if (skillIDs == null || skillIDs.Length == 0)
		{
			return;
		}
		Rms.saveRMS(this.getSkillShortcutRmsKey(baseKey), skillIDs);
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x000620F0 File Offset: 0x000602F0
	public void addSkillShortcut(Skill skill)
	{
		Cout.println("ADD SKILL SHORTCUT TO SKILL " + skill.template.id);
		for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
		{
			if (GameScr.onScreenSkill[i] == null)
			{
				GameScr.onScreenSkill[i] = skill;
				break;
			}
		}
		for (int j = 0; j < GameScr.keySkill.Length; j++)
		{
			if (GameScr.keySkill[j] == null)
			{
				GameScr.keySkill[j] = skill;
				break;
			}
		}
		if (global::Char.myCharz().myskill == null)
		{
			global::Char.myCharz().myskill = skill;
		}
		this.saveKeySkillToRMS();
		this.saveonScreenSkillToRMS();
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x000621A4 File Offset: 0x000603A4
	public bool isBagFull()
	{
		for (int i = global::Char.myCharz().arrItemBag.Length - 1; i >= 0; i--)
		{
			if (global::Char.myCharz().arrItemBag[i] == null)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x0000772D File Offset: 0x0000592D
	public void createConfirm(string[] menu, Npc npc)
	{
		this.resetButton();
		this.isLockKey = true;
		this.left = new Command(menu[0], 130011, npc);
		this.right = new Command(menu[1], 130012, npc);
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x000621E4 File Offset: 0x000603E4
	public void createMenu(string[] menu, Npc npc)
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < menu.Length; i++)
		{
			if (TileMap.mapID == 84 && npc != null && npc.template != null && npc.template.npcTemplateId == 21 && menu[i] != null && menu[i].Trim().Equals(mResources.UPGRADE))
			{
				continue;
			}
			myVector.addElement(new Command(menu[i], 11057, new object[] { npc, i }));
		}
		GameCanvas.menu.startAt(myVector, 2);
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x0006222C File Offset: 0x0006042C
	public void readPart()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_part"));
			int num = (int)dataInputStream.readShort();
			GameScr.parts = new Part[num];
			for (int i = 0; i < num; i++)
			{
				int type = (int)dataInputStream.readByte();
				GameScr.parts[i] = new Part(type);
				for (int j = 0; j < GameScr.parts[i].pi.Length; j++)
				{
					GameScr.parts[i].pi[j] = new PartImage();
					GameScr.parts[i].pi[j].id = dataInputStream.readShort();
					GameScr.parts[i].pi[j].dx = dataInputStream.readByte();
					GameScr.parts[i].pi[j].dy = dataInputStream.readByte();
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("LOI TAI readPart " + ex.ToString());
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex2)
			{
				Res.outz2("LOI TAI readPart 2" + ex2.StackTrace);
			}
		}
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x0006237C File Offset: 0x0006057C
	public void readEfect()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_effect"));
			int num = (int)dataInputStream.readShort();
			GameScr.efs = new EffectCharPaint[num];
			for (int i = 0; i < num; i++)
			{
				GameScr.efs[i] = new EffectCharPaint();
				GameScr.efs[i].idEf = (int)dataInputStream.readShort();
				GameScr.efs[i].arrEfInfo = new EffectInfoPaint[(int)dataInputStream.readByte()];
				for (int j = 0; j < GameScr.efs[i].arrEfInfo.Length; j++)
				{
					GameScr.efs[i].arrEfInfo[j] = new EffectInfoPaint();
					GameScr.efs[i].arrEfInfo[j].idImg = (int)dataInputStream.readShort();
					GameScr.efs[i].arrEfInfo[j].dx = (int)dataInputStream.readByte();
					GameScr.efs[i].arrEfInfo[j].dy = (int)dataInputStream.readByte();
				}
			}
		}
		catch (Exception ex)
		{
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex2)
			{
				Cout.LogError("Loi ham Eff: " + ex2.ToString());
			}
		}
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x000624D4 File Offset: 0x000606D4
	public void readArrow()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_arrow"));
			int num = (int)dataInputStream.readShort();
			GameScr.arrs = new Arrowpaint[num];
			for (int i = 0; i < num; i++)
			{
				GameScr.arrs[i] = new Arrowpaint();
				GameScr.arrs[i].id = (int)dataInputStream.readShort();
				GameScr.arrs[i].imgId[0] = (int)dataInputStream.readShort();
				GameScr.arrs[i].imgId[1] = (int)dataInputStream.readShort();
				GameScr.arrs[i].imgId[2] = (int)dataInputStream.readShort();
			}
		}
		catch (Exception ex)
		{
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex2)
			{
				Cout.LogError("Loi ham readArrow: " + ex2.ToString());
			}
		}
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x000625D0 File Offset: 0x000607D0
	public void readDart()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_dart"));
			int num = (int)dataInputStream.readShort();
			GameScr.darts = new DartInfo[num];
			for (int i = 0; i < num; i++)
			{
				GameScr.darts[i] = new DartInfo();
				GameScr.darts[i].id = dataInputStream.readShort();
				GameScr.darts[i].nUpdate = dataInputStream.readShort();
				GameScr.darts[i].va = (int)(dataInputStream.readShort() * 256);
				GameScr.darts[i].xdPercent = dataInputStream.readShort();
				int num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].tail = new short[num2];
				for (int j = 0; j < num2; j++)
				{
					GameScr.darts[i].tail[j] = dataInputStream.readShort();
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].tailBorder = new short[num2];
				for (int k = 0; k < num2; k++)
				{
					GameScr.darts[i].tailBorder[k] = dataInputStream.readShort();
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].xd1 = new short[num2];
				for (int l = 0; l < num2; l++)
				{
					GameScr.darts[i].xd1[l] = dataInputStream.readShort();
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].xd2 = new short[num2];
				for (int m = 0; m < num2; m++)
				{
					GameScr.darts[i].xd2[m] = dataInputStream.readShort();
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].head = new short[num2][];
				for (int n = 0; n < num2; n++)
				{
					short num3 = dataInputStream.readShort();
					GameScr.darts[i].head[n] = new short[(int)num3];
					for (int num4 = 0; num4 < (int)num3; num4++)
					{
						GameScr.darts[i].head[n][num4] = dataInputStream.readShort();
					}
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].headBorder = new short[num2][];
				for (int num5 = 0; num5 < num2; num5++)
				{
					short num6 = dataInputStream.readShort();
					GameScr.darts[i].headBorder[num5] = new short[(int)num6];
					for (int num7 = 0; num7 < (int)num6; num7++)
					{
						GameScr.darts[i].headBorder[num5][num7] = dataInputStream.readShort();
					}
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham ReadDart: " + ex.ToString());
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex2)
			{
				Cout.LogError("Loi ham reaaDart: " + ex2.ToString());
			}
		}
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0006290C File Offset: 0x00060B0C
	public void readSkill()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_skill"));
			int num = (int)dataInputStream.readShort();
			int num2 = Skills.skills.size();
			GameScr.sks = new SkillPaint[num2];
			for (int i = 0; i < num; i++)
			{
				short num3 = dataInputStream.readShort();
				if (num3 == 1111)
				{
					num3 = (short)(num - 1);
				}
				GameScr.sks[(int)num3] = new SkillPaint();
				GameScr.sks[(int)num3].id = (int)num3;
				GameScr.sks[(int)num3].effectHappenOnMob = (int)dataInputStream.readShort();
				if (GameScr.sks[(int)num3].effectHappenOnMob <= 0)
				{
					GameScr.sks[(int)num3].effectHappenOnMob = 80;
				}
				GameScr.sks[(int)num3].numEff = (int)dataInputStream.readByte();
				GameScr.sks[(int)num3].skillStand = new SkillInfoPaint[(int)dataInputStream.readByte()];
				for (int j = 0; j < GameScr.sks[(int)num3].skillStand.Length; j++)
				{
					GameScr.sks[(int)num3].skillStand[j] = new SkillInfoPaint();
					GameScr.sks[(int)num3].skillStand[j].status = (int)dataInputStream.readByte();
					GameScr.sks[(int)num3].skillStand[j].effS0Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].e0dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].e0dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].effS1Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].e1dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].e1dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].effS2Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].e2dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].e2dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].arrowId = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].adx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].ady = (int)dataInputStream.readShort();
				}
				GameScr.sks[(int)num3].skillfly = new SkillInfoPaint[(int)dataInputStream.readByte()];
				for (int k = 0; k < GameScr.sks[(int)num3].skillfly.Length; k++)
				{
					GameScr.sks[(int)num3].skillfly[k] = new SkillInfoPaint();
					GameScr.sks[(int)num3].skillfly[k].status = (int)dataInputStream.readByte();
					GameScr.sks[(int)num3].skillfly[k].effS0Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].e0dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].e0dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].effS1Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].e1dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].e1dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].effS2Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].e2dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].e2dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].arrowId = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].adx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].ady = (int)dataInputStream.readShort();
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham readSkill: " + ex.ToString());
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex2)
			{
				Cout.LogError("Loi ham readskill: " + ex2.ToString());
			}
		}
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x00062DD4 File Offset: 0x00060FD4
	public void readOk()
	{
		try
		{
			Res.outz(string.Concat(new object[]
			{
				"<readOk><vsData<",
				GameScr.vsData,
				"==",
				GameScr.vcData
			}));
			Res.outz(string.Concat(new object[]
			{
				"<readOk><vsMap<",
				GameScr.vsMap,
				"==",
				GameScr.vcMap
			}));
			Res.outz(string.Concat(new object[]
			{
				"<readOk><vsSkill<",
				GameScr.vsSkill,
				"==",
				GameScr.vcSkill
			}));
			Res.outz(string.Concat(new object[]
			{
				"<readOk><vsItem<",
				GameScr.vsItem,
				"==",
				GameScr.vcItem
			}));
			if ((int)GameScr.vsData == (int)GameScr.vcData && (int)GameScr.vsMap == (int)GameScr.vcMap && (int)GameScr.vsSkill == (int)GameScr.vcSkill && (int)GameScr.vsItem == (int)GameScr.vcItem)
			{
				Res.outz(string.Concat(new object[]
				{
					GameScr.vsData,
					",",
					GameScr.vsMap,
					",",
					GameScr.vsSkill,
					",",
					GameScr.vsItem
				}));
				GameScr.gI().readDart();
				GameScr.gI().readEfect();
				GameScr.gI().readArrow();
				GameScr.gI().readSkill();
				Service.gI().clientOk();
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham readOk: " + ex.ToString());
		}
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x00007764 File Offset: 0x00005964
	public static GameScr gI()
	{
		if (GameScr.instance == null)
		{
			GameScr.instance = new GameScr();
		}
		return GameScr.instance;
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x0000777F File Offset: 0x0000597F
	public static void clearGameScr()
	{
		GameScr.instance = null;
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x00007787 File Offset: 0x00005987
	public void loadGameScr()
	{
		GameScr.loadSplash();
		Res.init();
		this.loadInforBar();
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x00062FDC File Offset: 0x000611DC
	public void doMenuInforMe()
	{
		GameScr.scrMain.clear();
		GameScr.scrInfo.clear();
		GameScr.isViewNext = false;
		this.cmdBag = new Command(mResources.MENUME[0], 1100011);
		this.cmdSkill = new Command(mResources.MENUME[1], 1100012);
		this.cmdTiemnang = new Command(mResources.MENUME[2], 1100013);
		this.cmdInfo = new Command(mResources.MENUME[3], 1100014);
		this.cmdtrangbi = new Command(mResources.MENUME[4], 1100015);
		MyVector myVector = new MyVector();
		myVector.addElement(this.cmdBag);
		myVector.addElement(this.cmdSkill);
		myVector.addElement(this.cmdTiemnang);
		myVector.addElement(this.cmdInfo);
		myVector.addElement(this.cmdtrangbi);
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x000630C4 File Offset: 0x000612C4
	public void doMenusynthesis()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(mResources.SYNTHESIS[0], 110002));
		myVector.addElement(new Command(mResources.SYNTHESIS[1], 1100032));
		myVector.addElement(new Command(mResources.SYNTHESIS[2], 1100033));
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x00063128 File Offset: 0x00061328
	public static void loadCamera(bool fullmScreen, int cx, int cy)
	{
		GameScr.gW = GameCanvas.w;
		GameScr.cmdBarH = 39;
		GameScr.gH = GameCanvas.h;
		GameScr.cmdBarW = GameScr.gW;
		GameScr.cmdBarX = 0;
		GameScr.cmdBarY = GameCanvas.h - Paint.hTab - GameScr.cmdBarH;
		GameScr.girlHPBarY = 0;
		GameScr.csPadMaxH = GameCanvas.h / 6;
		if (GameScr.csPadMaxH < 48)
		{
			GameScr.csPadMaxH = 48;
		}
		GameScr.gW2 = GameScr.gW >> 1;
		GameScr.gH2 = GameScr.gH >> 1;
		GameScr.gW3 = GameScr.gW / 3;
		GameScr.gH3 = GameScr.gH / 3;
		GameScr.gW23 = GameScr.gH - 120;
		GameScr.gH23 = GameScr.gH * 2 / 3;
		GameScr.gW34 = 3 * GameScr.gW / 4;
		GameScr.gH34 = 3 * GameScr.gH / 4;
		GameScr.gW6 = GameScr.gW / 6;
		GameScr.gH6 = GameScr.gH / 6;
		GameScr.gssw = GameScr.gW / (int)TileMap.size + 2;
		GameScr.gssh = GameScr.gH / (int)TileMap.size + 2;
		if (GameScr.gW % 24 != 0)
		{
			GameScr.gssw++;
		}
		GameScr.cmxLim = (TileMap.tmw - 1) * (int)TileMap.size - GameScr.gW;
		GameScr.cmyLim = (TileMap.tmh - 1) * (int)TileMap.size - GameScr.gH;
		if (cx == -1 && cy == -1)
		{
			GameScr.cmx = (GameScr.cmtoX = global::Char.myCharz().cx - GameScr.gW2 + GameScr.gW6 * global::Char.myCharz().cdir);
			GameScr.cmy = (GameScr.cmtoY = global::Char.myCharz().cy - GameScr.gH23);
		}
		else
		{
			GameScr.cmx = (GameScr.cmtoX = cx - GameScr.gW23 + GameScr.gW6 * global::Char.myCharz().cdir);
			GameScr.cmy = (GameScr.cmtoY = cy - GameScr.gH23);
		}
		GameScr.firstY = GameScr.cmy;
		if (GameScr.cmx < 24)
		{
			GameScr.cmx = (GameScr.cmtoX = 24);
		}
		if (GameScr.cmx > GameScr.cmxLim)
		{
			GameScr.cmx = (GameScr.cmtoX = GameScr.cmxLim);
		}
		if (GameScr.cmy < 0)
		{
			GameScr.cmy = (GameScr.cmtoY = 0);
		}
		if (GameScr.cmy > GameScr.cmyLim)
		{
			GameScr.cmy = (GameScr.cmtoY = GameScr.cmyLim);
		}
		GameScr.gssx = GameScr.cmx / (int)TileMap.size - 1;
		if (GameScr.gssx < 0)
		{
			GameScr.gssx = 0;
		}
		GameScr.gssy = GameScr.cmy / (int)TileMap.size;
		GameScr.gssxe = GameScr.gssx + GameScr.gssw;
		GameScr.gssye = GameScr.gssy + GameScr.gssh;
		if (GameScr.gssy < 0)
		{
			GameScr.gssy = 0;
		}
		if (GameScr.gssye > TileMap.tmh - 1)
		{
			GameScr.gssye = TileMap.tmh - 1;
		}
		TileMap.countx = (GameScr.gssxe - GameScr.gssx) * 4;
		if (TileMap.countx > TileMap.tmw)
		{
			TileMap.countx = TileMap.tmw;
		}
		TileMap.county = (GameScr.gssye - GameScr.gssy) * 4;
		if (TileMap.county > TileMap.tmh)
		{
			TileMap.county = TileMap.tmh;
		}
		TileMap.gssx = (global::Char.myCharz().cx - 2 * GameScr.gW) / (int)TileMap.size;
		if (TileMap.gssx < 0)
		{
			TileMap.gssx = 0;
		}
		TileMap.gssxe = TileMap.gssx + TileMap.countx;
		if (TileMap.gssxe > TileMap.tmw)
		{
			TileMap.gssxe = TileMap.tmw;
		}
		TileMap.gssy = (global::Char.myCharz().cy - 2 * GameScr.gH) / (int)TileMap.size;
		if (TileMap.gssy < 0)
		{
			TileMap.gssy = 0;
		}
		TileMap.gssye = TileMap.gssy + TileMap.county;
		if (TileMap.gssye > TileMap.tmh)
		{
			TileMap.gssye = TileMap.tmh;
		}
		ChatTextField.gI().parentScreen = GameScr.instance;
		ChatTextField.gI().tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
		ChatTextField.gI().initChatTextField();
		if (GameCanvas.isTouch)
		{
			GameScr.yTouchBar = GameScr.gH - 88;
			GameScr.xC = GameScr.gW - 40;
			GameScr.yC = 2;
			if (GameCanvas.w <= 240)
			{
				GameScr.xC = GameScr.gW - 35;
				GameScr.yC = 5;
			}
			GameScr.xF = GameScr.gW - 55;
			GameScr.yF = GameScr.yTouchBar + 35;
			GameScr.xTG = GameScr.gW - 37;
			GameScr.yTG = GameScr.yTouchBar - 1;
			if (GameCanvas.w >= 450)
			{
				GameScr.yTG -= 12;
				GameScr.yHP -= 7;
				GameScr.xF -= 10;
				GameScr.yF -= 5;
				GameScr.xTG -= 10;
			}
		}
		GameScr.setSkillBarPosition();
		GameScr.disXC = ((GameCanvas.w <= 200) ? 30 : 40);
		if (Rms.loadRMSInt("viewchat") == -1)
		{
			GameCanvas.panel.isViewChatServer = true;
		}
		else
		{
			GameCanvas.panel.isViewChatServer = (Rms.loadRMSInt("viewchat") == 1);
		}
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x0006368C File Offset: 0x0006188C
	public static void setSkillBarPosition()
	{
		Skill[] array = (!GameCanvas.isTouch) ? GameScr.keySkill : GameScr.onScreenSkill;
		GameScr.xS = new int[array.Length];
		GameScr.yS = new int[array.Length];
		if (GameCanvas.isTouchControlSmallScreen && GameScr.isUseTouch)
		{
			GameScr.xSkill = 23;
			GameScr.ySkill = 52;
			GameScr.padSkill = 5;
			for (int i = 0; i < GameScr.xS.Length; i++)
			{
				GameScr.xS[i] = i * (25 + GameScr.padSkill);
				GameScr.yS[i] = GameScr.ySkill;
				if (GameScr.xS.Length > 5 && i >= GameScr.xS.Length / 2)
				{
					GameScr.xS[i] = (i - GameScr.xS.Length / 2) * (25 + GameScr.padSkill);
					GameScr.yS[i] = GameScr.ySkill - 32;
				}
			}
			GameScr.xHP = array.Length * (25 + GameScr.padSkill);
			GameScr.yHP = GameScr.ySkill;
		}
		else
		{
			GameScr.wSkill = 30;
			if (GameCanvas.w <= 320)
			{
				GameScr.ySkill = GameScr.gH - GameScr.wSkill - 6;
				GameScr.xSkill = GameScr.gW2 - array.Length * GameScr.wSkill / 2 - 25;
			}
			else
			{
				GameScr.wSkill = 40;
				GameScr.xSkill = 10;
				GameScr.ySkill = GameCanvas.h - GameScr.wSkill + 7;
			}
			for (int j = 0; j < GameScr.xS.Length; j++)
			{
				GameScr.xS[j] = j * GameScr.wSkill;
				GameScr.yS[j] = GameScr.ySkill;
				if (GameScr.xS.Length > 5 && j >= GameScr.xS.Length / 2)
				{
					GameScr.xS[j] = (j - GameScr.xS.Length / 2) * GameScr.wSkill;
					GameScr.yS[j] = GameScr.ySkill - 32;
				}
			}
			GameScr.xHP = array.Length * GameScr.wSkill;
			GameScr.yHP = GameScr.ySkill;
		}
		if (GameCanvas.isTouch)
		{
			GameScr.xSkill = 17;
			GameScr.ySkill = GameCanvas.h - 40;
			if (GameScr.gamePad.isSmallGamePad && GameScr.isAnalog == 1)
			{
				GameScr.xHP = array.Length * GameScr.wSkill;
				GameScr.yHP = GameScr.ySkill;
			}
			else
			{
				GameScr.xHP = GameCanvas.w - 45;
				GameScr.yHP = GameCanvas.h - 45;
			}
			GameScr.setTouchBtn();
			for (int k = 0; k < GameScr.xS.Length; k++)
			{
				GameScr.xS[k] = k * GameScr.wSkill;
				GameScr.yS[k] = GameScr.ySkill;
				if (GameScr.xS.Length > 5 && k >= GameScr.xS.Length / 2)
				{
					GameScr.xS[k] = (k - GameScr.xS.Length / 2) * GameScr.wSkill;
					GameScr.yS[k] = GameScr.ySkill - 32;
				}
			}
		}
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x0006396C File Offset: 0x00061B6C
	private static void updateCamera()
	{
		if (GameScr.isPaintOther)
		{
			return;
		}
		if (GameScr.cmx != GameScr.cmtoX || GameScr.cmy != GameScr.cmtoY)
		{
			GameScr.cmvx = GameScr.cmtoX - GameScr.cmx << 2;
			GameScr.cmvy = GameScr.cmtoY - GameScr.cmy << 2;
			GameScr.cmdx += GameScr.cmvx;
			GameScr.cmx += GameScr.cmdx >> 4;
			GameScr.cmdx &= 15;
			GameScr.cmdy += GameScr.cmvy;
			GameScr.cmy += GameScr.cmdy >> 4;
			GameScr.cmdy &= 15;
			if (GameScr.cmx < 24)
			{
				GameScr.cmx = 24;
			}
			if (GameScr.cmx > GameScr.cmxLim)
			{
				GameScr.cmx = GameScr.cmxLim;
			}
			if (GameScr.cmy < 0)
			{
				GameScr.cmy = 0;
			}
			if (GameScr.cmy > GameScr.cmyLim)
			{
				GameScr.cmy = GameScr.cmyLim;
			}
		}
		GameScr.gssx = GameScr.cmx / (int)TileMap.size - 1;
		if (GameScr.gssx < 0)
		{
			GameScr.gssx = 0;
		}
		GameScr.gssy = GameScr.cmy / (int)TileMap.size;
		GameScr.gssxe = GameScr.gssx + GameScr.gssw;
		GameScr.gssye = GameScr.gssy + GameScr.gssh;
		if (GameScr.gssy < 0)
		{
			GameScr.gssy = 0;
		}
		if (GameScr.gssye > TileMap.tmh - 1)
		{
			GameScr.gssye = TileMap.tmh - 1;
		}
		TileMap.gssx = (global::Char.myCharz().cx - 2 * GameScr.gW) / (int)TileMap.size;
		if (TileMap.gssx < 0)
		{
			TileMap.gssx = 0;
		}
		TileMap.gssxe = TileMap.gssx + TileMap.countx;
		if (TileMap.gssxe > TileMap.tmw)
		{
			TileMap.gssxe = TileMap.tmw;
			TileMap.gssx = TileMap.gssxe - TileMap.countx;
		}
		TileMap.gssy = (global::Char.myCharz().cy - 2 * GameScr.gH) / (int)TileMap.size;
		if (TileMap.gssy < 0)
		{
			TileMap.gssy = 0;
		}
		TileMap.gssye = TileMap.gssy + TileMap.county;
		if (TileMap.gssye > TileMap.tmh)
		{
			TileMap.gssye = TileMap.tmh;
			TileMap.gssy = TileMap.gssye - TileMap.county;
		}
		GameScr.scrMain.updatecm();
		GameScr.scrInfo.updatecm();
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x00063BE4 File Offset: 0x00061DE4
	public bool testAct()
	{
		sbyte b = 2;
		while ((int)b < 9)
		{
			if (GameCanvas.keyHold[(int)b])
			{
				return false;
			}
			b = (sbyte)((int)b + 2);
		}
		return true;
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x00063C18 File Offset: 0x00061E18
	public void clanInvite(string strInvite, int clanID, int code)
	{
		ClanObject clanObject = new ClanObject();
		clanObject.code = code;
		clanObject.clanID = clanID;
		this.startYesNoPopUp(strInvite, new Command(mResources.YES, 12002, clanObject), new Command(mResources.NO, 12003, clanObject));
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x00063C60 File Offset: 0x00061E60
	public void playerMenu(global::Char c)
	{
		this.auto = 0;
		GameCanvas.clearKeyHold();
		if (global::Char.myCharz().charFocus.charID < 0)
		{
			return;
		}
		if (global::Char.myCharz().charID < 0)
		{
			return;
		}
		MyVector vPlayerMenu = GameCanvas.panel.vPlayerMenu;
		if (vPlayerMenu.size() > 0)
		{
			return;
		}
		if (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId > 1)
		{
			vPlayerMenu.addElement(new Command(mResources.make_friend, 11112, global::Char.myCharz().charFocus));
			vPlayerMenu.addElement(new Command(mResources.trade, 11113, global::Char.myCharz().charFocus));
		}
		if (global::Char.myCharz().clan != null && (int)global::Char.myCharz().role < 2 && global::Char.myCharz().charFocus.clanID == -1)
		{
			vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[4], 110391));
		}
		if (global::Char.myCharz().charFocus.statusMe != 14 && global::Char.myCharz().charFocus.statusMe != 5)
		{
			if (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId >= 14)
			{
				vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[0], 2003));
			}
		}
		else if (global::Char.myCharz().myskill.template.type == 4)
		{
		}
		if (global::Char.myCharz().clan != null && global::Char.myCharz().clan.ID == global::Char.myCharz().charFocus.clanID && global::Char.myCharz().charFocus.statusMe != 14 && global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId >= 14)
		{
			vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[1], 2004));
		}
		int num = global::Char.myCharz().nClass.skillTemplates.Length;
		for (int i = 0; i < num; i++)
		{
			SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[i];
			Skill skill = global::Char.myCharz().getSkill(skillTemplate);
			if (skill != null && skillTemplate.isBuffToPlayer() && skill.point >= 1)
			{
				vPlayerMenu.addElement(new Command(skillTemplate.name, 12004, skill));
			}
		}
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x00063EEC File Offset: 0x000620EC
	public bool isAttack()
	{
		if (this.checkClickToBotton(global::Char.myCharz().charFocus))
		{
			return false;
		}
		if (this.checkClickToBotton(global::Char.myCharz().mobFocus))
		{
			return false;
		}
		if (this.checkClickToBotton(global::Char.myCharz().npcFocus))
		{
			return false;
		}
		if (ChatTextField.gI().isShow)
		{
			return false;
		}
		if (InfoDlg.isLock || global::Char.myCharz().isLockAttack || global::Char.isLockKey)
		{
			return false;
		}
		if (global::Char.myCharz().myskill != null && (int)global::Char.myCharz().myskill.template.id == 6 && global::Char.myCharz().itemFocus != null)
		{
			this.pickItem();
			return false;
		}
		if (global::Char.myCharz().myskill != null && global::Char.myCharz().myskill.template.type == 2 && global::Char.myCharz().npcFocus == null && (int)global::Char.myCharz().myskill.template.id != 6)
		{
			return this.checkSkillValid();
		}
		if (global::Char.myCharz().skillPaint != null || (global::Char.myCharz().mobFocus == null && global::Char.myCharz().npcFocus == null && global::Char.myCharz().charFocus == null && global::Char.myCharz().itemFocus == null))
		{
			return false;
		}
		if (global::Char.myCharz().mobFocus != null)
		{
			if (global::Char.myCharz().mobFocus.isBigBoss() && global::Char.myCharz().mobFocus.status == 4)
			{
				global::Char.myCharz().mobFocus = null;
				global::Char.myCharz().currentMovePoint = null;
			}
			GameScr.isAutoPlay = true;
			if (!this.isMeCanAttackMob(global::Char.myCharz().mobFocus))
			{
				Res.outz("can not attack");
				return false;
			}
			if (this.mobCapcha != null)
			{
				return false;
			}
			if (global::Char.myCharz().myskill == null)
			{
				return false;
			}
			if (global::Char.myCharz().isSelectingSkillUseAlone())
			{
				return false;
			}
			int num = -1;
			int num2 = Res.abs(global::Char.myCharz().cx - GameScr.cmx) * mGraphics.zoomLevel;
			if (global::Char.myCharz().charFocus != null)
			{
				num = Res.abs(global::Char.myCharz().cx - global::Char.myCharz().charFocus.cx) * mGraphics.zoomLevel;
			}
			else if (global::Char.myCharz().mobFocus != null)
			{
				num = Res.abs(global::Char.myCharz().cx - global::Char.myCharz().mobFocus.x) * mGraphics.zoomLevel;
			}
			if ((global::Char.myCharz().mobFocus.status == 1 || global::Char.myCharz().mobFocus.status == 0 || global::Char.myCharz().myskill.template.type == 4 || num == -1 || num > num2) && global::Char.myCharz().myskill.template.type == 4)
			{
				if (global::Char.myCharz().mobFocus.x < global::Char.myCharz().cx)
				{
					global::Char.myCharz().cdir = -1;
				}
				else
				{
					global::Char.myCharz().cdir = 1;
				}
				this.doSelectSkill(global::Char.myCharz().myskill, true);
			}
			if (!this.checkSkillValid())
			{
				return false;
			}
			if (global::Char.myCharz().cx < global::Char.myCharz().mobFocus.getX())
			{
				global::Char.myCharz().cdir = 1;
			}
			else
			{
				global::Char.myCharz().cdir = -1;
			}
			int num3 = global::Math.abs(global::Char.myCharz().cx - global::Char.myCharz().mobFocus.getX());
			int num4 = global::Math.abs(global::Char.myCharz().cy - global::Char.myCharz().mobFocus.getY());
			global::Char.myCharz().cvx = 0;
			if (num3 > global::Char.myCharz().myskill.dx || num4 > global::Char.myCharz().myskill.dy)
			{
				bool flag = false;
				if (global::Char.myCharz().mobFocus is BigBoss || global::Char.myCharz().mobFocus is BigBoss2)
				{
					flag = true;
				}
				int num5 = (global::Char.myCharz().myskill.dx - ((!flag) ? 20 : 50)) * ((global::Char.myCharz().cx <= global::Char.myCharz().mobFocus.getX()) ? -1 : 1);
				if (num3 <= global::Char.myCharz().myskill.dx)
				{
					num5 = 0;
				}
				global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().mobFocus.getX() + num5, global::Char.myCharz().mobFocus.getY());
				global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return false;
			}
			if ((int)global::Char.myCharz().myskill.template.id == 20)
			{
				return true;
			}
			if (num4 > num3 && Res.abs(global::Char.myCharz().cy - global::Char.myCharz().mobFocus.getY()) > 30 && (int)global::Char.myCharz().mobFocus.getTemplate().type == 4)
			{
				global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().cx + global::Char.myCharz().cdir, global::Char.myCharz().mobFocus.getY());
				global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return false;
			}
			int num6 = 20;
			bool flag2 = false;
			if (global::Char.myCharz().mobFocus is BigBoss || global::Char.myCharz().mobFocus is BigBoss2)
			{
				flag2 = true;
			}
			if (global::Char.myCharz().myskill.dx > 100)
			{
				num6 = 60;
				if (num3 < 20)
				{
					global::Char.myCharz().createShadow(global::Char.myCharz().cx, global::Char.myCharz().cy, 10);
				}
			}
			bool flag3 = false;
			if ((TileMap.tileTypeAtPixel(global::Char.myCharz().cx, global::Char.myCharz().cy + 3) & 2) == 2)
			{
				int num7 = (global::Char.myCharz().cx <= global::Char.myCharz().mobFocus.getX()) ? -1 : 1;
				if ((TileMap.tileTypeAtPixel(global::Char.myCharz().mobFocus.getX() + num6 * num7, global::Char.myCharz().cy + 3) & 2) != 2)
				{
					flag3 = true;
				}
			}
			if (num3 <= num6 && !flag3)
			{
				if (global::Char.myCharz().cx > global::Char.myCharz().mobFocus.getX())
				{
					int num8 = global::Char.myCharz().mobFocus.getX() + num6 + ((!flag2) ? 0 : 30);
					int i = global::Char.myCharz().mobFocus.getX();
					bool flag4 = false;
					while (i < num8)
					{
						if (TileMap.tileTypeAtPixel(i, global::Char.myCharz().cy + 3) == 8 || TileMap.tileTypeAtPixel(i, global::Char.myCharz().cy + 3) == 4)
						{
							flag4 = true;
							break;
						}
						i += 24;
					}
					if (flag4)
					{
						global::Char.myCharz().cx = i - 24;
					}
					else
					{
						global::Char.myCharz().cx = num8;
					}
					global::Char.myCharz().cdir = -1;
				}
				else
				{
					int num9 = global::Char.myCharz().mobFocus.getX() - num6 - ((!flag2) ? 0 : 30);
					int j = global::Char.myCharz().mobFocus.getX();
					bool flag5 = false;
					while (j > num9)
					{
						if (TileMap.tileTypeAtPixel(j, global::Char.myCharz().cy + 3) == 8 || TileMap.tileTypeAtPixel(j, global::Char.myCharz().cy + 3) == 4)
						{
							flag5 = true;
							break;
						}
						j -= 24;
					}
					if (flag5)
					{
						global::Char.myCharz().cx = j + 24;
					}
					else
					{
						global::Char.myCharz().cx = num9;
					}
					global::Char.myCharz().cdir = 1;
				}
				Service.gI().charMove();
			}
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			return true;
		}
		else if (global::Char.myCharz().npcFocus != null)
		{
			if (global::Char.myCharz().npcFocus.isHide)
			{
				return false;
			}
			if (global::Char.myCharz().cx < global::Char.myCharz().npcFocus.cx)
			{
				global::Char.myCharz().cdir = 1;
			}
			else
			{
				global::Char.myCharz().cdir = -1;
			}
			if (global::Char.myCharz().cx < global::Char.myCharz().npcFocus.cx)
			{
				global::Char.myCharz().npcFocus.cdir = -1;
			}
			else
			{
				global::Char.myCharz().npcFocus.cdir = 1;
			}
			int num10 = global::Math.abs(global::Char.myCharz().cx - global::Char.myCharz().npcFocus.cx);
			int num11 = global::Math.abs(global::Char.myCharz().cy - global::Char.myCharz().npcFocus.cy);
			if (num11 > 40)
			{
				global::Char.myCharz().cy = global::Char.myCharz().npcFocus.cy - 40;
			}
			if (num10 < 60)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				if (this.tMenuDelay == 0)
				{
					if (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId == 0)
					{
						if (global::Char.myCharz().taskMaint.index < 4 && global::Char.myCharz().npcFocus.template.npcTemplateId == 4)
						{
							return false;
						}
						if (global::Char.myCharz().taskMaint.index < 3 && global::Char.myCharz().npcFocus.template.npcTemplateId == 3)
						{
							return false;
						}
					}
					this.tMenuDelay = 50;
					InfoDlg.showWait();
					Service.gI().charMove();
					Service.gI().openMenu(global::Char.myCharz().npcFocus.template.npcTemplateId);
				}
			}
			else
			{
				int num12 = (20 + Res.r.nextInt(20)) * ((global::Char.myCharz().cx <= global::Char.myCharz().npcFocus.cx) ? -1 : 1);
				global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().npcFocus.cx + num12, global::Char.myCharz().cy);
				global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
			}
			return false;
		}
		else if (global::Char.myCharz().charFocus != null)
		{
			if (this.mobCapcha != null)
			{
				return false;
			}
			if (global::Char.myCharz().cx < global::Char.myCharz().charFocus.cx)
			{
				global::Char.myCharz().cdir = 1;
			}
			else
			{
				global::Char.myCharz().cdir = -1;
			}
			int num13 = global::Math.abs(global::Char.myCharz().cx - global::Char.myCharz().charFocus.cx);
			int num14 = global::Math.abs(global::Char.myCharz().cy - global::Char.myCharz().charFocus.cy);
			if (!global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus) && !global::Char.myCharz().isSelectingSkillBuffToPlayer())
			{
				if (num13 < 60 && num14 < 40)
				{
					this.playerMenu(global::Char.myCharz().charFocus);
					if (!GameCanvas.isTouch && global::Char.myCharz().charFocus.charID >= 0 && TileMap.mapID != 51 && TileMap.mapID != 52 && this.popUpYesNo == null)
					{
						GameCanvas.panel.setTypePlayerMenu(global::Char.myCharz().charFocus);
						GameCanvas.panel.show();
						Service.gI().getPlayerMenu(global::Char.myCharz().charFocus.charID);
						Service.gI().messagePlayerMenu(global::Char.myCharz().charFocus.charID);
					}
				}
				else
				{
					int num15 = (20 + Res.r.nextInt(20)) * ((global::Char.myCharz().cx <= global::Char.myCharz().charFocus.cx) ? -1 : 1);
					global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().charFocus.cx + num15, global::Char.myCharz().charFocus.cy);
					global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
				}
				return false;
			}
			if (global::Char.myCharz().myskill == null)
			{
				return false;
			}
			if (!this.checkSkillValid())
			{
				return false;
			}
			if (global::Char.myCharz().cx < global::Char.myCharz().charFocus.cx)
			{
				global::Char.myCharz().cdir = 1;
			}
			else
			{
				global::Char.myCharz().cdir = -1;
			}
			global::Char.myCharz().cvx = 0;
			if (num13 > global::Char.myCharz().myskill.dx || num14 > global::Char.myCharz().myskill.dy)
			{
				int num16 = (global::Char.myCharz().myskill.dx - 20) * ((global::Char.myCharz().cx <= global::Char.myCharz().charFocus.cx) ? -1 : 1);
				if (num13 <= global::Char.myCharz().myskill.dx)
				{
					num16 = 0;
				}
				global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().charFocus.cx + num16, global::Char.myCharz().charFocus.cy);
				global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return false;
			}
			if ((int)global::Char.myCharz().myskill.template.id == 20)
			{
				return true;
			}
			int num17 = 20;
			if (global::Char.myCharz().myskill.dx > 60)
			{
				num17 = 60;
				if (num13 < 20)
				{
					global::Char.myCharz().createShadow(global::Char.myCharz().cx, global::Char.myCharz().cy, 10);
				}
			}
			bool flag6 = false;
			if ((TileMap.tileTypeAtPixel(global::Char.myCharz().cx, global::Char.myCharz().cy + 3) & 2) == 2)
			{
				int num18 = (global::Char.myCharz().cx <= global::Char.myCharz().charFocus.cx) ? -1 : 1;
				if ((TileMap.tileTypeAtPixel(global::Char.myCharz().charFocus.cx + num17 * num18, global::Char.myCharz().cy + 3) & 2) != 2)
				{
					flag6 = true;
				}
			}
			if (num13 <= num17 && !flag6)
			{
				if (global::Char.myCharz().cx > global::Char.myCharz().charFocus.cx)
				{
					global::Char.myCharz().cx = global::Char.myCharz().charFocus.cx + num17;
					global::Char.myCharz().cdir = -1;
				}
				else
				{
					global::Char.myCharz().cx = global::Char.myCharz().charFocus.cx - num17;
					global::Char.myCharz().cdir = 1;
				}
				Service.gI().charMove();
			}
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			return true;
		}
		else
		{
			if (global::Char.myCharz().itemFocus != null)
			{
				this.pickItem();
				return false;
			}
			return true;
		}
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x00064E80 File Offset: 0x00063080
	public bool isMeCanAttackMob(Mob m)
	{
		if (m == null)
		{
			return false;
		}
		if ((int)global::Char.myCharz().cTypePk == 5)
		{
			return true;
		}
		if (global::Char.myCharz().isAttacPlayerStatus() && !m.isMobMe)
		{
			return false;
		}
		if (global::Char.myCharz().mobMe != null && m.Equals(global::Char.myCharz().mobMe))
		{
			return false;
		}
		global::Char @char = GameScr.findCharInMap(m.mobId);
		return @char == null || (int)@char.cTypePk == 5 || global::Char.myCharz().isMeCanAttackOtherPlayer(@char);
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x00064F20 File Offset: 0x00063120
	private bool checkSkillValid()
	{
		if (global::Char.myCharz().myskill != null && ((global::Char.myCharz().myskill.template.manaUseType != 1 && global::Char.myCharz().cMP < (long)global::Char.myCharz().myskill.manaUse) || (global::Char.myCharz().myskill.template.manaUseType == 1 && global::Char.myCharz().cMP < global::Char.myCharz().cMPFull * (long)global::Char.myCharz().myskill.manaUse / 100L)))
		{
			GameScr.info1.addInfo(mResources.NOT_ENOUGH_MP, 0);
			this.auto = 0;
			return false;
		}
		if (global::Char.myCharz().myskill == null || (global::Char.myCharz().myskill.template.maxPoint > 0 && global::Char.myCharz().myskill.point == 0))
		{
			GameCanvas.startOKDlg(mResources.SKILL_FAIL);
			return false;
		}
		return true;
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x00065020 File Offset: 0x00063220
	private bool checkSkillValid2()
	{
		return (global::Char.myCharz().myskill == null || ((global::Char.myCharz().myskill.template.manaUseType == 1 || global::Char.myCharz().cMP >= (long)global::Char.myCharz().myskill.manaUse) && (global::Char.myCharz().myskill.template.manaUseType != 1 || global::Char.myCharz().cMP >= global::Char.myCharz().cMPFull * (long)global::Char.myCharz().myskill.manaUse / 100L))) && global::Char.myCharz().myskill != null && (global::Char.myCharz().myskill.template.maxPoint <= 0 || global::Char.myCharz().myskill.point != 0);
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x00065100 File Offset: 0x00063300
	public void resetButton()
	{
		GameCanvas.menu.showMenu = false;
		ChatTextField.gI().close();
		ChatTextField.gI().center = null;
		this.isLockKey = false;
		this.typeTrade = 0;
		GameScr.indexMenu = 0;
		GameScr.indexSelect = 0;
		this.indexItemUse = -1;
		GameScr.indexRow = -1;
		GameScr.indexRowMax = 0;
		GameScr.indexTitle = 0;
		this.typeTrade = (this.typeTradeOrder = 0);
		mSystem.endKey();
		if (global::Char.myCharz().cHP <= 0L || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5)
		{
			if (global::Char.myCharz().meDead)
			{
				this.cmdDead = new Command(mResources.DIES[0], 11038);
				this.center = this.cmdDead;
				global::Char.myCharz().cHP = 0L;
			}
			GameScr.isHaveSelectSkill = false;
		}
		else
		{
			GameScr.isHaveSelectSkill = true;
		}
		GameScr.scrMain.clear();
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x00007799 File Offset: 0x00005999
	public override void keyPress(int keyCode)
	{
		base.keyPress(keyCode);
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x00065200 File Offset: 0x00063400
	public override void updateKey()
	{
		if (Controller.isStopReadMessage || global::Char.myCharz().isTeleport || global::Char.myCharz().isPaintNewSkill)
		{
			return;
		}
		if (InfoDlg.isLock)
		{
			return;
		}
		if (GameCanvas.isTouch && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu)
		{
			this.updateKeyTouchControl();
		}
		this.checkAuto();
		GameCanvas.debug("F2", 0);
		if (ChatPopup.currChatPopup != null)
		{
			Command cmdNextLine = ChatPopup.currChatPopup.cmdNextLine;
			if ((GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(cmdNextLine)) && cmdNextLine != null)
			{
				GameCanvas.isPointerJustRelease = false;
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				mScreen.keyTouch = -1;
				if (cmdNextLine != null)
				{
					cmdNextLine.performAction();
				}
			}
		}
		else if (!ChatTextField.gI().isShow)
		{
			if ((GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.left)) && this.left != null)
			{
				GameCanvas.isPointerJustRelease = false;
				GameCanvas.isPointerClick = false;
				GameCanvas.keyPressed[12] = false;
				mScreen.keyTouch = -1;
				if (this.left != null)
				{
					this.left.performAction();
				}
			}
			if ((GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.right)) && this.right != null)
			{
				GameCanvas.isPointerJustRelease = false;
				GameCanvas.isPointerClick = false;
				GameCanvas.keyPressed[13] = false;
				mScreen.keyTouch = -1;
				if (this.right != null)
				{
					this.right.performAction();
				}
			}
			if ((GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.center)) && this.center != null)
			{
				GameCanvas.isPointerJustRelease = false;
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				mScreen.keyTouch = -1;
				if (this.center != null)
				{
					this.center.performAction();
				}
			}
		}
		else
		{
			if (ChatTextField.gI().left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(ChatTextField.gI().left)) && ChatTextField.gI().left != null)
			{
				ChatTextField.gI().left.performAction();
			}
			if (ChatTextField.gI().right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(ChatTextField.gI().right)) && ChatTextField.gI().right != null)
			{
				ChatTextField.gI().right.performAction();
			}
			if (ChatTextField.gI().center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(ChatTextField.gI().center)) && ChatTextField.gI().center != null)
			{
				ChatTextField.gI().center.performAction();
			}
		}
		GameCanvas.debug("F6", 0);
		this.updateKeyAlert();
		GameCanvas.debug("F7", 0);
		if (global::Char.myCharz().currentMovePoint != null)
		{
			for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
			{
				if (GameCanvas.keyPressed[i])
				{
					global::Char.myCharz().currentMovePoint = null;
					break;
				}
			}
		}
		GameCanvas.debug("F8", 0);
		if (ChatTextField.gI().isShow && GameCanvas.keyAsciiPress != 0)
		{
			ChatTextField.gI().keyPressed(GameCanvas.keyAsciiPress);
			GameCanvas.keyAsciiPress = 0;
			return;
		}
		if (this.isLockKey)
		{
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			return;
		}
		if (GameCanvas.menu.showMenu || this.isOpenUI() || global::Char.isLockKey)
		{
			return;
		}
		if (GameCanvas.keyPressed[10])
		{
			GameCanvas.keyPressed[10] = false;
			this.doUseHP();
			GameCanvas.clearKeyPressed();
		}
		if (GameCanvas.keyPressed[11] && this.mobCapcha == null)
		{
			if (this.popUpYesNo != null)
			{
				this.popUpYesNo.cmdYes.performAction();
			}
			else if (GameScr.info2.info.info != null && GameScr.info2.info.info.charInfo != null)
			{
				GameCanvas.panel.setTypeMessage();
				GameCanvas.panel.show();
			}
			GameCanvas.keyPressed[11] = false;
			GameCanvas.clearKeyPressed();
		}
		if (GameCanvas.keyAsciiPress != 0 && TField.isQwerty && GameCanvas.keyAsciiPress == 32)
		{
			this.doUseHP();
			GameCanvas.keyAsciiPress = 0;
			GameCanvas.clearKeyPressed();
		}
		if (GameCanvas.keyAsciiPress != 0 && this.mobCapcha == null && TField.isQwerty && GameCanvas.keyAsciiPress == 121)
		{
			if (this.popUpYesNo != null)
			{
				this.popUpYesNo.cmdYes.performAction();
				GameCanvas.keyAsciiPress = 0;
				GameCanvas.clearKeyPressed();
			}
			else if (GameScr.info2.info.info != null && GameScr.info2.info.info.charInfo != null)
			{
				GameCanvas.panel.setTypeMessage();
				GameCanvas.panel.show();
				GameCanvas.keyAsciiPress = 0;
				GameCanvas.clearKeyPressed();
			}
		}
		if (GameCanvas.keyPressed[10] && this.mobCapcha == null)
		{
			GameCanvas.keyPressed[10] = false;
			GameScr.info2.doClick(10);
			GameCanvas.clearKeyPressed();
		}
		this.checkDrag();
		if (!global::Char.myCharz().isFlyAndCharge)
		{
			this.checkClick();
		}
		if (global::Char.myCharz().cmdMenu != null && global::Char.myCharz().cmdMenu.isPointerPressInside())
		{
			global::Char.myCharz().cmdMenu.performAction();
		}
		if (global::Char.myCharz().skillPaint != null)
		{
			return;
		}
		if (GameCanvas.keyAsciiPress != 0)
		{
			if (this.mobCapcha == null)
			{
				if (TField.isQwerty)
				{
					if (GameCanvas.keyPressed[1])
					{
						if (GameScr.keySkill[0] != null)
						{
							this.doSelectSkill(GameScr.keySkill[0], true);
						}
					}
					else if (GameCanvas.keyPressed[2])
					{
						if (GameScr.keySkill[1] != null)
						{
							this.doSelectSkill(GameScr.keySkill[1], true);
						}
					}
					else if (GameCanvas.keyPressed[3])
					{
						if (GameScr.keySkill[2] != null)
						{
							this.doSelectSkill(GameScr.keySkill[2], true);
						}
					}
					else if (GameCanvas.keyPressed[4])
					{
						if (GameScr.keySkill[3] != null)
						{
							this.doSelectSkill(GameScr.keySkill[3], true);
						}
					}
					else if (GameCanvas.keyPressed[5])
					{
						if (GameScr.keySkill[4] != null)
						{
							this.doSelectSkill(GameScr.keySkill[4], true);
						}
					}
					else if (GameCanvas.keyPressed[6])
					{
						if (GameScr.keySkill[5] != null)
						{
							this.doSelectSkill(GameScr.keySkill[5], true);
						}
					}
					else if (GameCanvas.keyPressed[7])
					{
						if (GameScr.keySkill[6] != null)
						{
							this.doSelectSkill(GameScr.keySkill[6], true);
						}
					}
					else if (GameCanvas.keyPressed[8])
					{
						if (GameScr.keySkill[7] != null)
						{
							this.doSelectSkill(GameScr.keySkill[7], true);
						}
					}
					else if (GameCanvas.keyPressed[9])
					{
						if (GameScr.keySkill[8] != null)
						{
							this.doSelectSkill(GameScr.keySkill[8], true);
						}
					}
					else if (GameCanvas.keyPressed[0])
					{
						if (GameScr.keySkill[9] != null)
						{
							this.doSelectSkill(GameScr.keySkill[9], true);
						}
					}
					else if (GameCanvas.keyAsciiPress == 114)
					{
						ChatTextField.gI().startChat(this, string.Empty);
					}
					else if (!MainMod.UpdateKey(GameCanvas.keyAsciiPress))
					{
					}
				}
				else if (!GameCanvas.isMoveNumberPad)
				{
					ChatTextField.gI().startChat(GameCanvas.keyAsciiPress, this, string.Empty);
				}
				else if (GameCanvas.keyAsciiPress == 55)
				{
					if (GameScr.keySkill[0] != null)
					{
						this.doSelectSkill(GameScr.keySkill[0], true);
					}
				}
				else if (GameCanvas.keyAsciiPress == 56)
				{
					if (GameScr.keySkill[1] != null)
					{
						this.doSelectSkill(GameScr.keySkill[1], true);
					}
				}
				else if (GameCanvas.keyAsciiPress == 57)
				{
					if (GameScr.keySkill[(!Main.isPC) ? 2 : 21] != null)
					{
						this.doSelectSkill(GameScr.keySkill[2], true);
					}
				}
				else if (GameCanvas.keyAsciiPress == 48)
				{
					ChatTextField.gI().startChat(this, string.Empty);
				}
			}
			else
			{
				char[] array = this.keyInput.ToCharArray();
				MyVector myVector = new MyVector();
				for (int j = 0; j < array.Length; j++)
				{
					myVector.addElement(array[j].ToString() + string.Empty);
				}
				myVector.removeElementAt(0);
				string text = ((char)GameCanvas.keyAsciiPress).ToString() + string.Empty;
				if (text.Equals(string.Empty) || text == null || text.Equals("\n"))
				{
					text = "-";
				}
				myVector.insertElementAt(text, myVector.size());
				this.keyInput = string.Empty;
				for (int k = 0; k < myVector.size(); k++)
				{
					this.keyInput += ((string)myVector.elementAt(k)).ToUpper();
				}
				Service.gI().mobCapcha((char)GameCanvas.keyAsciiPress);
			}
			GameCanvas.keyAsciiPress = 0;
		}
		if (global::Char.myCharz().statusMe == 1)
		{
			GameCanvas.debug("F10", 0);
			if (!this.doSeleckSkillFlag)
			{
				if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
					this.doFire(false, false);
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
				{
					if (!global::Char.myCharz().isLockMove)
					{
						this.setCharJump(0);
					}
				}
				else if (GameCanvas.keyHold[1] && this.mobCapcha == null)
				{
					if (!Main.isPC)
					{
						global::Char.myCharz().cdir = -1;
						if (!global::Char.myCharz().isLockMove)
						{
							this.setCharJump(-4);
						}
					}
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 5 : 25] && this.mobCapcha == null)
				{
					if (!Main.isPC)
					{
						global::Char.myCharz().cdir = 1;
						if (!global::Char.myCharz().isLockMove)
						{
							this.setCharJump(4);
						}
					}
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
				{
					GameScr.isAutoPlay = false;
					global::Char.myCharz().isAttack = false;
					if (global::Char.myCharz().cdir == 1)
					{
						global::Char.myCharz().cdir = -1;
					}
					else if (!global::Char.myCharz().isLockMove)
					{
						if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0)
						{
							Service.gI().charMove();
						}
						global::Char.myCharz().statusMe = 2;
						global::Char.myCharz().cvx = -global::Char.myCharz().cspeed;
					}
					global::Char.myCharz().holder = false;
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
				{
					GameScr.isAutoPlay = false;
					global::Char.myCharz().isAttack = false;
					if (global::Char.myCharz().cdir == -1)
					{
						global::Char.myCharz().cdir = 1;
					}
					else if (!global::Char.myCharz().isLockMove)
					{
						if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0)
						{
							Service.gI().charMove();
						}
						global::Char.myCharz().statusMe = 2;
						global::Char.myCharz().cvx = global::Char.myCharz().cspeed;
					}
					global::Char.myCharz().holder = false;
				}
			}
		}
		else if (global::Char.myCharz().statusMe == 2)
		{
			GameCanvas.debug("F11", 0);
			if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				this.doFire(false, true);
			}
			else if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
			{
				if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0)
				{
					Service.gI().charMove();
				}
				global::Char.myCharz().cvy = -10;
				global::Char.myCharz().statusMe = 3;
				global::Char.myCharz().cp1 = 0;
			}
			else if (GameCanvas.keyHold[1] && this.mobCapcha == null)
			{
				if (Main.isPC)
				{
					if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0)
					{
						Service.gI().charMove();
					}
					global::Char.myCharz().cdir = -1;
					global::Char.myCharz().cvy = -10;
					global::Char.myCharz().cvx = -4;
					global::Char.myCharz().statusMe = 3;
					global::Char.myCharz().cp1 = 0;
				}
			}
			else if (GameCanvas.keyHold[3] && this.mobCapcha == null)
			{
				if (!Main.isPC)
				{
					if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0)
					{
						Service.gI().charMove();
					}
					global::Char.myCharz().cdir = 1;
					global::Char.myCharz().cvy = -10;
					global::Char.myCharz().cvx = 4;
					global::Char.myCharz().statusMe = 3;
					global::Char.myCharz().cp1 = 0;
				}
			}
			else if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
			{
				GameScr.isAutoPlay = false;
				if (global::Char.myCharz().cdir == 1)
				{
					global::Char.myCharz().cdir = -1;
				}
				else
				{
					global::Char.myCharz().cvx = -global::Char.myCharz().cspeed + global::Char.myCharz().cBonusSpeed;
				}
			}
			else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
			{
				GameScr.isAutoPlay = false;
				if (global::Char.myCharz().cdir == -1)
				{
					global::Char.myCharz().cdir = 1;
				}
				else
				{
					global::Char.myCharz().cvx = global::Char.myCharz().cspeed + global::Char.myCharz().cBonusSpeed;
				}
			}
		}
		else if (global::Char.myCharz().statusMe == 3)
		{
			GameScr.isAutoPlay = false;
			GameCanvas.debug("F12", 0);
			if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				this.doFire(false, true);
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] || (GameCanvas.keyHold[1] && this.mobCapcha == null))
			{
				if (global::Char.myCharz().cdir == 1)
				{
					global::Char.myCharz().cdir = -1;
				}
				else
				{
					global::Char.myCharz().cvx = -global::Char.myCharz().cspeed;
				}
			}
			else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] || (GameCanvas.keyHold[3] && this.mobCapcha == null))
			{
				if (global::Char.myCharz().cdir == -1)
				{
					global::Char.myCharz().cdir = 1;
				}
				else
				{
					global::Char.myCharz().cvx = global::Char.myCharz().cspeed;
				}
			}
			if ((GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] || ((GameCanvas.keyHold[1] || GameCanvas.keyHold[3]) && this.mobCapcha == null)) && global::Char.myCharz().canFly && global::Char.myCharz().cMP > 0L && global::Char.myCharz().cp1 < 8 && global::Char.myCharz().cvy > -4)
			{
				global::Char.myCharz().cp1++;
				global::Char.myCharz().cvy = -7;
			}
		}
		else if (global::Char.myCharz().statusMe == 4)
		{
			GameCanvas.debug("F13", 0);
			if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				this.doFire(false, true);
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] && global::Char.myCharz().cMP > 0L && global::Char.myCharz().canFly)
			{
				GameScr.isAutoPlay = false;
				if ((global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0) && (Res.abs(global::Char.myCharz().cx - global::Char.myCharz().cxSend) > 96 || Res.abs(global::Char.myCharz().cy - global::Char.myCharz().cySend) > 24))
				{
					Service.gI().charMove();
				}
				global::Char.myCharz().cvy = -10;
				global::Char.myCharz().statusMe = 3;
				global::Char.myCharz().cp1 = 0;
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
			{
				GameScr.isAutoPlay = false;
				if (global::Char.myCharz().cdir == 1)
				{
					global::Char.myCharz().cdir = -1;
				}
				else
				{
					global::Char.myCharz().cp1++;
					global::Char.myCharz().cvx = -global::Char.myCharz().cspeed;
					if (global::Char.myCharz().cp1 > 5 && global::Char.myCharz().cvy > 6)
					{
						global::Char.myCharz().statusMe = 10;
						global::Char.myCharz().cp1 = 0;
						global::Char.myCharz().cvy = 0;
					}
				}
			}
			else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
			{
				GameScr.isAutoPlay = false;
				if (global::Char.myCharz().cdir == -1)
				{
					global::Char.myCharz().cdir = 1;
				}
				else
				{
					global::Char.myCharz().cp1++;
					global::Char.myCharz().cvx = global::Char.myCharz().cspeed;
					if (global::Char.myCharz().cp1 > 5 && global::Char.myCharz().cvy > 6)
					{
						global::Char.myCharz().statusMe = 10;
						global::Char.myCharz().cp1 = 0;
						global::Char.myCharz().cvy = 0;
					}
				}
			}
		}
		else if (global::Char.myCharz().statusMe == 10)
		{
			GameCanvas.debug("F14", 0);
			if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				this.doFire(false, true);
			}
			if (global::Char.myCharz().canFly && global::Char.myCharz().cMP > 0L)
			{
				if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
				{
					GameScr.isAutoPlay = false;
					if ((global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0) && (Res.abs(global::Char.myCharz().cx - global::Char.myCharz().cxSend) > 96 || Res.abs(global::Char.myCharz().cy - global::Char.myCharz().cySend) > 24))
					{
						Service.gI().charMove();
					}
					global::Char.myCharz().cvy = -10;
					global::Char.myCharz().statusMe = 3;
					global::Char.myCharz().cp1 = 0;
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
				{
					GameScr.isAutoPlay = false;
					if (global::Char.myCharz().cdir == 1)
					{
						global::Char.myCharz().cdir = -1;
					}
					else
					{
						global::Char.myCharz().cvx = -(global::Char.myCharz().cspeed + 1);
					}
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
				{
					if (global::Char.myCharz().cdir == -1)
					{
						global::Char.myCharz().cdir = 1;
					}
					else
					{
						global::Char.myCharz().cvx = global::Char.myCharz().cspeed + 1;
					}
				}
			}
		}
		else if (global::Char.myCharz().statusMe == 7)
		{
			GameCanvas.debug("F15", 0);
			if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
			{
				GameScr.isAutoPlay = false;
				if (global::Char.myCharz().cdir == 1)
				{
					global::Char.myCharz().cdir = -1;
				}
				else
				{
					global::Char.myCharz().cvx = -global::Char.myCharz().cspeed + 2;
				}
			}
			else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
			{
				GameScr.isAutoPlay = false;
				if (global::Char.myCharz().cdir == -1)
				{
					global::Char.myCharz().cdir = 1;
				}
				else
				{
					global::Char.myCharz().cvx = global::Char.myCharz().cspeed - 2;
				}
			}
		}
		GameCanvas.debug("F17", 0);
		if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] && GameCanvas.keyAsciiPress != 56)
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
			global::Char.myCharz().delayFall = 0;
		}
		if (GameCanvas.keyPressed[10])
		{
			GameCanvas.keyPressed[10] = false;
			this.doUseHP();
		}
		GameCanvas.debug("F20", 0);
		GameCanvas.clearKeyPressed();
		GameCanvas.debug("F23", 0);
		this.doSeleckSkillFlag = false;
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x00004E4B File Offset: 0x0000304B
	public bool isVsMap()
	{
		return true;
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x000666E8 File Offset: 0x000648E8
	private void checkDrag()
	{
		if (GameScr.isAnalog == 1)
		{
			return;
		}
		if (GameScr.gamePad.disableCheckDrag())
		{
			return;
		}
		global::Char.myCharz().cmtoChar = true;
		if (GameScr.isUseTouch)
		{
			return;
		}
		if (GameCanvas.isPointerJustDown)
		{
			GameCanvas.isPointerJustDown = false;
			this.isPointerDowning = true;
			this.ptDownTime = 0;
			this.ptLastDownX = (this.ptFirstDownX = GameCanvas.px);
			this.ptLastDownY = (this.ptFirstDownY = GameCanvas.py);
		}
		if (this.isPointerDowning)
		{
			int num = GameCanvas.px - this.ptLastDownX;
			int num2 = GameCanvas.py - this.ptLastDownY;
			if (!this.isChangingCameraMode && (Res.abs(GameCanvas.px - this.ptFirstDownX) > 15 || Res.abs(GameCanvas.py - this.ptFirstDownY) > 15))
			{
				this.isChangingCameraMode = true;
			}
			this.ptLastDownX = GameCanvas.px;
			this.ptLastDownY = GameCanvas.py;
			this.ptDownTime++;
			if (this.isChangingCameraMode)
			{
				global::Char.myCharz().cmtoChar = false;
				GameScr.cmx -= num;
				GameScr.cmy -= num2;
				if (GameScr.cmx < 24)
				{
					int num3 = (24 - GameScr.cmx) / 3;
					if (num3 != 0)
					{
						GameScr.cmx += num - num / num3;
					}
				}
				if (GameScr.cmx < ((!this.isVsMap()) ? 0 : 24))
				{
					GameScr.cmx = ((!this.isVsMap()) ? 0 : 24);
				}
				if (GameScr.cmx > GameScr.cmxLim)
				{
					int num4 = (GameScr.cmx - GameScr.cmxLim) / 3;
					if (num4 != 0)
					{
						GameScr.cmx += num - num / num4;
					}
				}
				if (GameScr.cmx > GameScr.cmxLim + ((!this.isVsMap()) ? 24 : 0))
				{
					GameScr.cmx = GameScr.cmxLim + ((!this.isVsMap()) ? 24 : 0);
				}
				if (GameScr.cmy < 0)
				{
					int num5 = -GameScr.cmy / 3;
					if (num5 != 0)
					{
						GameScr.cmy += num2 - num2 / num5;
					}
				}
				if (GameScr.cmy < -((!this.isVsMap()) ? 24 : 0))
				{
					GameScr.cmy = -((!this.isVsMap()) ? 24 : 0);
				}
				if (GameScr.cmy > GameScr.cmyLim)
				{
					GameScr.cmy = GameScr.cmyLim;
				}
				GameScr.cmtoX = GameScr.cmx;
				GameScr.cmtoY = GameScr.cmy;
			}
		}
		if (this.isPointerDowning && GameCanvas.isPointerJustRelease)
		{
			this.isPointerDowning = false;
			this.isChangingCameraMode = false;
			if (Res.abs(GameCanvas.px - this.ptFirstDownX) > 15 || Res.abs(GameCanvas.py - this.ptFirstDownY) > 15)
			{
				GameCanvas.isPointerJustRelease = false;
			}
		}
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x000669EC File Offset: 0x00064BEC
	private void checkClick()
	{
		if (this.isCharging())
		{
			return;
		}
		if (this.popUpYesNo != null && this.popUpYesNo.cmdYes != null && this.popUpYesNo.cmdYes.isPointerPressInside())
		{
			this.popUpYesNo.cmdYes.performAction();
			return;
		}
		if (this.checkClickToCapcha())
		{
			return;
		}
		long num = mSystem.currentTimeMillis();
		if (this.lastSingleClick != 0L)
		{
			this.lastSingleClick = 0L;
			GameCanvas.isPointerJustDown = false;
			if (!this.disableSingleClick)
			{
				this.checkSingleClick();
				GameCanvas.isPointerJustRelease = false;
				this.isWaitingDoubleClick = true;
				this.timeStartDblClick = mSystem.currentTimeMillis();
			}
		}
		if (this.isWaitingDoubleClick)
		{
			this.timeEndDblClick = mSystem.currentTimeMillis();
			if (this.timeEndDblClick - this.timeStartDblClick < 300L && GameCanvas.isPointerJustRelease)
			{
				this.isWaitingDoubleClick = false;
				this.checkDoubleClick();
			}
		}
		if (GameCanvas.isPointerJustRelease)
		{
			this.disableSingleClick = this.checkSingleClickEarly();
			this.lastSingleClick = num;
			this.lastClickCMX = GameScr.cmx;
			this.lastClickCMY = GameScr.cmy;
			GameCanvas.isPointerJustRelease = false;
		}
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x00066B1C File Offset: 0x00064D1C
	private IMapObject findClickToItem(int px, int py)
	{
		IMapObject mapObject = null;
		int num = 0;
		int num2 = 30;
		MyVector[] array = new MyVector[]
		{
			GameScr.vMob,
			GameScr.vNpc,
			GameScr.vItemMap,
			GameScr.vCharInMap
		};
		for (int i = 0; i < array.Length; i++)
		{
			for (int j = 0; j < array[i].size(); j++)
			{
				IMapObject mapObject2 = (IMapObject)array[i].elementAt(j);
				if (!mapObject2.isInvisible())
				{
					if (mapObject2 is Mob)
					{
						Mob mob = (Mob)mapObject2;
						if (mob.isMobMe && mob.Equals(global::Char.myCharz().mobMe))
						{
							goto IL_139;
						}
					}
					int x = mapObject2.getX();
					int y = mapObject2.getY();
					int w = mapObject2.getW();
					int h = mapObject2.getH();
					if (this.inRectangle(px, py, x - w / 2 - num2, y - h - num2, w + num2 * 2, h + num2 * 2))
					{
						if (mapObject == null)
						{
							mapObject = mapObject2;
							num = Res.abs(px - x) + Res.abs(py - y);
							if (i == 1)
							{
								return mapObject;
							}
						}
						else
						{
							int num3 = Res.abs(px - x) + Res.abs(py - y);
							if (num3 < num)
							{
								mapObject = mapObject2;
								num = num3;
							}
						}
					}
				}
				IL_139:;
			}
		}
		return mapObject;
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x00066C8C File Offset: 0x00064E8C
	private Mob findClickToMOB(int px, int py)
	{
		int num = 30;
		Mob mob = null;
		int num2 = 0;
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob2 = (Mob)GameScr.vMob.elementAt(i);
			if (!mob2.isInvisible())
			{
				if (mob2 != null)
				{
					Mob mob3 = mob2;
					if (mob3.isMobMe && mob3.Equals(global::Char.myCharz().mobMe))
					{
						goto IL_F5;
					}
				}
				int x = mob2.getX();
				int y = mob2.getY();
				int w = mob2.getW();
				int h = mob2.getH();
				if (this.inRectangle(px, py, x - w / 2 - num, y - h - num, w + num * 2, h + num * 2))
				{
					if (mob == null)
					{
						mob = mob2;
						num2 = Res.abs(px - x) + Res.abs(py - y);
					}
					else
					{
						int num3 = Res.abs(px - x) + Res.abs(py - y);
						if (num3 < num2)
						{
							mob = mob2;
							num2 = num3;
						}
					}
				}
			}
			IL_F5:;
		}
		return mob;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x000077A2 File Offset: 0x000059A2
	private bool inRectangle(int xClick, int yClick, int x, int y, int w, int h)
	{
		return xClick >= x && xClick <= x + w && yClick >= y && yClick <= y + h;
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x00066DA4 File Offset: 0x00064FA4
	private bool checkSingleClickEarly()
	{
		int num = GameCanvas.px + GameScr.cmx;
		int num2 = GameCanvas.py + GameScr.cmy;
		global::Char.myCharz().cancelAttack();
		IMapObject mapObject = this.findClickToItem(num, num2);
		if (mapObject == null)
		{
			return false;
		}
		if (global::Char.myCharz().isAttacPlayerStatus() && global::Char.myCharz().charFocus != null && !mapObject.Equals(global::Char.myCharz().charFocus))
		{
			if (!mapObject.Equals(global::Char.myCharz().charFocus.mobMe))
			{
				if (mapObject is global::Char)
				{
					global::Char @char = (global::Char)mapObject;
					if ((int)@char.cTypePk != 5 && !@char.isAttacPlayerStatus())
					{
						this.checkClickMoveTo(num, num2, 2);
						return false;
					}
				}
			}
		}
		if (global::Char.myCharz().mobFocus == mapObject || global::Char.myCharz().itemFocus == mapObject)
		{
			this.doDoubleClickToObj(mapObject);
			return true;
		}
		if (TileMap.mapID == 51 && mapObject.Equals(global::Char.myCharz().npcFocus))
		{
			this.checkClickMoveTo(num, num2, 3);
			return false;
		}
		if (global::Char.myCharz().skillPaint != null || global::Char.myCharz().arr != null || global::Char.myCharz().dart != null || global::Char.myCharz().skillInfoPaint() != null)
		{
			return false;
		}
		global::Char.myCharz().focusManualTo(mapObject);
		mapObject.stopMoving();
		return false;
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x00066F18 File Offset: 0x00065118
	private void checkDoubleClick()
	{
		int num = GameCanvas.px + this.lastClickCMX;
		int num2 = GameCanvas.py + this.lastClickCMY;
		int cy = global::Char.myCharz().cy;
		if (this.isLockKey)
		{
			return;
		}
		IMapObject mapObject = this.findClickToItem(num, num2);
		if (mapObject != null)
		{
			if (mapObject is Mob && !this.isMeCanAttackMob((Mob)mapObject))
			{
				this.checkClickMoveTo(num, num2, 4);
				return;
			}
			if (this.checkClickToBotton(mapObject))
			{
				return;
			}
			if (!mapObject.Equals(global::Char.myCharz().npcFocus) && this.mobCapcha != null)
			{
				return;
			}
			if (global::Char.myCharz().isAttacPlayerStatus() && global::Char.myCharz().charFocus != null && !mapObject.Equals(global::Char.myCharz().charFocus))
			{
				if (!mapObject.Equals(global::Char.myCharz().charFocus.mobMe))
				{
					if (mapObject is global::Char)
					{
						global::Char @char = (global::Char)mapObject;
						if ((int)@char.cTypePk != 5 && !@char.isAttacPlayerStatus())
						{
							this.checkClickMoveTo(num, num2, 5);
							return;
						}
					}
				}
			}
			if (TileMap.mapID == 51 && mapObject.Equals(global::Char.myCharz().npcFocus))
			{
				this.checkClickMoveTo(num, num2, 6);
				return;
			}
			this.doDoubleClickToObj(mapObject);
			return;
		}
		else
		{
			if (this.checkClickToPopup(num, num2))
			{
				return;
			}
			if (this.checkClipTopChatPopUp(num, num2))
			{
				return;
			}
			if (Main.isPC)
			{
				return;
			}
			this.checkClickMoveTo(num, num2, 7);
			return;
		}
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x000670AC File Offset: 0x000652AC
	private bool checkClickToBotton(IMapObject Object)
	{
		if (Object == null)
		{
			return false;
		}
		int i = Object.getY();
		int num = global::Char.myCharz().cy;
		if (i < num)
		{
			while (i < num)
			{
				num -= 5;
				if (TileMap.tileTypeAt(global::Char.myCharz().cx, num, 8192))
				{
					this.auto = 0;
					global::Char.myCharz().cancelAttack();
					global::Char.myCharz().currentMovePoint = null;
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x00067124 File Offset: 0x00065324
	public void doDoubleClickToObj(IMapObject obj)
	{
		if (!obj.Equals(global::Char.myCharz().npcFocus) && this.mobCapcha != null)
		{
			return;
		}
		if (this.checkClickToBotton(obj))
		{
			return;
		}
		this.checkEffToObj(obj, false);
		global::Char.myCharz().cancelAttack();
		global::Char.myCharz().currentMovePoint = null;
		global::Char.myCharz().cvx = (global::Char.myCharz().cvy = 0);
		obj.stopMoving();
		this.auto = 10;
		this.doFire(false, true);
		this.clickToX = obj.getX();
		this.clickToY = obj.getY();
		this.clickOnTileTop = false;
		this.clickMoving = true;
		this.clickMovingRed = true;
		this.clickMovingTimeOut = 20;
		this.clickMovingP1 = 30;
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x000671E0 File Offset: 0x000653E0
	private void checkSingleClick()
	{
		int xClick = GameCanvas.px + this.lastClickCMX;
		int yClick = GameCanvas.py + this.lastClickCMY;
		if (this.isLockKey)
		{
			return;
		}
		if (this.checkClickToPopup(xClick, yClick))
		{
			return;
		}
		if (this.checkClipTopChatPopUp(xClick, yClick))
		{
			return;
		}
		this.checkClickMoveTo(xClick, yClick, 0);
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x00067238 File Offset: 0x00065438
	private bool checkClipTopChatPopUp(int xClick, int yClick)
	{
		if (this.Equals(GameScr.info2) && GameScr.gI().popUpYesNo != null)
		{
			return false;
		}
		if (GameScr.info2.info.info != null && GameScr.info2.info.info.charInfo != null)
		{
			int x = Res.abs(GameScr.info2.cmx) + GameScr.info2.info.X - 40;
			int y = Res.abs(GameScr.info2.cmy) + GameScr.info2.info.Y;
			if (this.inRectangle(xClick - GameScr.cmx, yClick - GameScr.cmy, x, y, 200, GameScr.info2.info.H))
			{
				GameScr.info2.doClick(10);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00067318 File Offset: 0x00065518
	private bool checkClickToPopup(int xClick, int yClick)
	{
		for (int i = 0; i < PopUp.vPopups.size(); i++)
		{
			PopUp popUp = (PopUp)PopUp.vPopups.elementAt(i);
			if (this.inRectangle(xClick, yClick, popUp.cx, popUp.cy, popUp.cw, popUp.ch))
			{
				if (popUp.cy <= 24 && TileMap.isInAirMap() && (int)global::Char.myCharz().cTypePk != 0)
				{
					return false;
				}
				if (popUp.isPaint)
				{
					popUp.doClick(10);
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x000673B8 File Offset: 0x000655B8
	private void checkClickMoveTo(int xClick, int yClick, int index)
	{
		if (GameScr.gamePad.disableClickMove())
		{
			return;
		}
		global::Char.myCharz().cancelAttack();
		if (xClick < TileMap.pxw && xClick > TileMap.pxw - 32)
		{
			global::Char.myCharz().currentMovePoint = new MovePoint(TileMap.pxw, yClick);
			return;
		}
		if (xClick < 32 && xClick > 0)
		{
			global::Char.myCharz().currentMovePoint = new MovePoint(0, yClick);
			return;
		}
		if (xClick < TileMap.pxw && xClick > TileMap.pxw - 48)
		{
			global::Char.myCharz().currentMovePoint = new MovePoint(TileMap.pxw, yClick);
			return;
		}
		if (xClick < 48 && xClick > 0)
		{
			global::Char.myCharz().currentMovePoint = new MovePoint(0, yClick);
			return;
		}
		this.clickToX = xClick;
		this.clickToY = yClick;
		this.clickOnTileTop = false;
		global::Char.myCharz().delayFall = 0;
		int num = (!global::Char.myCharz().canFly || global::Char.myCharz().cMP <= 0L) ? 1000 : 0;
		if (this.clickToY > global::Char.myCharz().cy && Res.abs(this.clickToX - global::Char.myCharz().cx) < 12)
		{
			return;
		}
		for (int i = 0; i < 60 + num; i += 24)
		{
			if (this.clickToY + i >= TileMap.pxh - 24)
			{
				break;
			}
			if (TileMap.tileTypeAt(this.clickToX, this.clickToY + i, 2))
			{
				this.clickToY = TileMap.tileYofPixel(this.clickToY + i);
				this.clickOnTileTop = true;
				break;
			}
		}
		for (int j = 0; j < 40 + num; j += 24)
		{
			if (TileMap.tileTypeAt(this.clickToX, this.clickToY - j, 2))
			{
				this.clickToY = TileMap.tileYofPixel(this.clickToY - j);
				this.clickOnTileTop = true;
				break;
			}
		}
		this.clickMoving = true;
		this.clickMovingRed = false;
		this.clickMovingP1 = ((!this.clickOnTileTop) ? 30 : ((yClick >= this.clickToY) ? this.clickToY : yClick));
		global::Char.myCharz().delayFall = 0;
		if (!this.clickOnTileTop && this.clickToY < global::Char.myCharz().cy - 50)
		{
			global::Char.myCharz().delayFall = 20;
		}
		this.clickMovingTimeOut = 30;
		this.auto = 0;
		if (global::Char.myCharz().holder)
		{
			global::Char.myCharz().removeHoleEff();
		}
		global::Char.myCharz().currentMovePoint = new MovePoint(this.clickToX, this.clickToY);
		global::Char.myCharz().cdir = ((global::Char.myCharz().cx - global::Char.myCharz().currentMovePoint.xEnd <= 0) ? 1 : -1);
		global::Char.myCharz().endMovePointCommand = null;
		GameScr.isAutoPlay = false;
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x000676B4 File Offset: 0x000658B4
	private void checkAuto()
	{
		long num = mSystem.currentTimeMillis();
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] || GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] || GameCanvas.keyPressed[1] || GameCanvas.keyPressed[3])
		{
			this.auto = 0;
			GameScr.isAutoPlay = false;
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] && !this.isPaintPopup())
		{
			if (this.auto == 0)
			{
				if (num - this.lastFire < 800L && this.checkSkillValid2() && (global::Char.myCharz().mobFocus != null || (global::Char.myCharz().charFocus != null && global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus))))
				{
					Res.outz("toi day");
					this.auto = 10;
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				}
			}
			else
			{
				this.auto = 0;
				GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = false);
			}
			this.lastFire = num;
		}
		if (GameCanvas.gameTick % 5 == 0 && this.auto > 0 && global::Char.myCharz().currentMovePoint == null)
		{
			if (global::Char.myCharz().myskill != null && (global::Char.myCharz().myskill.template.isUseAlone() || global::Char.myCharz().myskill.paintCanNotUseSkill))
			{
				return;
			}
			if ((global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.status != 1 && global::Char.myCharz().mobFocus.status != 0 && global::Char.myCharz().charFocus == null) || (global::Char.myCharz().charFocus != null && global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus)))
			{
				if (global::Char.myCharz().myskill.paintCanNotUseSkill)
				{
					return;
				}
				this.doFire(false, true);
			}
		}
		if (this.auto > 1)
		{
			this.auto--;
		}
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x00067948 File Offset: 0x00065B48
	public void doUseHP()
	{
		if (global::Char.myCharz().stone)
		{
			return;
		}
		if (global::Char.myCharz().blindEff)
		{
			return;
		}
		if (global::Char.myCharz().holdEffID > 0)
		{
			return;
		}
		long num = mSystem.currentTimeMillis();
		if (num - this.lastUsePotion < 10000L)
		{
			return;
		}
		if (!global::Char.myCharz().doUsePotion())
		{
			GameScr.info1.addInfo(mResources.HP_EMPTY, 0);
		}
		else
		{
			ServerEffect.addServerEffect(11, global::Char.myCharz(), 5);
			ServerEffect.addServerEffect(104, global::Char.myCharz(), 4);
			this.lastUsePotion = num;
			SoundMn.gI().eatPeans();
		}
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x000679F0 File Offset: 0x00065BF0
	public void activeSuperPower(int x, int y)
	{
		if (!this.isSuperPower)
		{
			SoundMn.gI().bigeExlode();
			this.isSuperPower = true;
			this.tPower = 0;
			this.dxPower = 0;
			this.xPower = x - GameScr.cmx;
			this.yPower = y - GameScr.cmy;
		}
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x00067A44 File Offset: 0x00065C44
	public void activeRongThanEff(bool isMe)
	{
		this.activeRongThan = true;
		this.isUseFreez = true;
		this.isMeCallRongThan = true;
		if (isMe)
		{
			Effect me = new Effect(20, global::Char.myCharz().cx, global::Char.myCharz().cy - 77, 2, 8, 1);
			EffecMn.addEff(me);
		}
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x000077CB File Offset: 0x000059CB
	public void hideRongThanEff()
	{
		this.activeRongThan = false;
		this.isUseFreez = true;
		this.isMeCallRongThan = false;
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x000077E2 File Offset: 0x000059E2
	public void doiMauTroi()
	{
		this.isRongThanXuatHien = true;
		this.mautroi = mGraphics.blendColor(0.4f, 0, GameCanvas.colorTop[GameCanvas.colorTop.Length - 1]);
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x00067A94 File Offset: 0x00065C94
	public void callRongThan(int x, int y)
	{
		Res.outz(string.Concat(new object[]
		{
			"VE RONG THAN O VI TRI x= ",
			x,
			" y=",
			y
		}));
		this.doiMauTroi();
		Effect me = new Effect((!this.isRongNamek) ? 17 : 25, x, y - 77, 2, -1, 1);
		EffecMn.addEff(me);
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x0000780B File Offset: 0x00005A0B
	public void hideRongThan()
	{
		this.isRongThanXuatHien = false;
		EffecMn.removeEff(17);
		if (this.isRongNamek)
		{
			this.isRongNamek = false;
			EffecMn.removeEff(25);
		}
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x00067B04 File Offset: 0x00065D04
	private void autoPlay()
	{
		if (this.timeSkill > 0)
		{
			this.timeSkill--;
		}
		if (!GameScr.canAutoPlay || GameScr.isChangeZone || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5 || global::Char.myCharz().isCharge || global::Char.myCharz().isFlyAndCharge || global::Char.myCharz().isUseChargeSkill())
		{
			return;
		}
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			((Mob)GameScr.vMob.elementAt(i)).isDontMove = true;
		}
		bool flag = false;
		for (int j = 0; j < GameScr.vMob.size(); j++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(j);
			if (mob.status != 0 && mob.status != 1)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			return;
		}
		bool flag2 = false;
		for (int k = 0; k < global::Char.myCharz().arrItemBag.Length; k++)
		{
			Item item = global::Char.myCharz().arrItemBag[k];
			if (item != null && item.template.type == 6)
			{
				flag2 = true;
				break;
			}
		}
		if (!flag2 && GameCanvas.gameTick % 150 == 0)
		{
			Service.gI().requestPean();
		}
		if (global::Char.myCharz().cHP <= global::Char.myCharz().cHPFull * 20L / 100L || global::Char.myCharz().cMP <= global::Char.myCharz().cMPFull * 20L / 100L)
		{
			this.doUseHP();
		}
		if (global::Char.myCharz().mobFocus != null && (global::Char.myCharz().mobFocus == null || !global::Char.myCharz().mobFocus.isMobMe))
		{
			if (global::Char.myCharz().mobFocus.hp <= 0L || global::Char.myCharz().mobFocus.status == 1 || global::Char.myCharz().mobFocus.status == 0)
			{
				global::Char.myCharz().mobFocus = null;
			}
		}
		else
		{
			for (int l = 0; l < GameScr.vMob.size(); l++)
			{
				Mob mob2 = (Mob)GameScr.vMob.elementAt(l);
				if (mob2.status != 0 && mob2.status != 1 && mob2.hp > 0L && !mob2.isMobMe)
				{
					global::Char.myCharz().cx = mob2.x;
					global::Char.myCharz().cy = mob2.y;
					global::Char.myCharz().mobFocus = mob2;
					Service.gI().charMove();
					Res.outz("focus 1 con bossssssssssssssssssssssssssssssssssssssssssssssssss");
					break;
				}
			}
		}
		if (global::Char.myCharz().mobFocus == null || this.timeSkill != 0 || (global::Char.myCharz().skillInfoPaint() != null && global::Char.myCharz().indexSkill < global::Char.myCharz().skillInfoPaint().Length && global::Char.myCharz().dart != null && global::Char.myCharz().arr != null))
		{
			return;
		}
		Skill skill = null;
		if (GameCanvas.isTouch)
		{
			for (int m = 0; m < GameScr.onScreenSkill.Length; m++)
			{
				if (GameScr.keySkill[m] != null && !GameScr.keySkill[m].paintCanNotUseSkill && GameScr.keySkill[m].template.id != 10 && GameScr.keySkill[m].template.id != 11 && GameScr.keySkill[m].template.id != 14 && GameScr.keySkill[m].template.id != 23 && GameScr.keySkill[m].template.id != 7 && GameScr.keySkill[m].template.id != 3 && GameScr.keySkill[m].template.id != 1 && GameScr.keySkill[m].template.id != 5 && GameScr.keySkill[m].template.id != 20 && GameScr.keySkill[m].template.id != 22 && GameScr.keySkill[m].template.id != 18 && (global::Char.myCharz().cgender != 1 || (global::Char.myCharz().cgender == 1 && (global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[5]) == null || (global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[5]) != null && GameScr.keySkill[m].template.id != 2)))) && global::Char.myCharz().skillInfoPaint() == null && !GameScr.onScreenSkill[m].template.isSkillSpec())
				{
					int num = (GameScr.onScreenSkill[m].template.manaUseType == 2) ? 1 : ((GameScr.onScreenSkill[m].template.manaUseType == 1) ? ((int)((long)GameScr.onScreenSkill[m].manaUse * global::Char.myCharz().cMPFull / 100L)) : GameScr.onScreenSkill[m].manaUse);
					if (global::Char.myCharz().cMP >= (long)num)
					{
						if (skill == null)
						{
							skill = GameScr.onScreenSkill[m];
						}
						else if (skill.coolDown < GameScr.onScreenSkill[m].coolDown)
						{
							skill = GameScr.onScreenSkill[m];
						}
					}
				}
			}
			if (skill != null)
			{
				this.doSelectSkill(skill, true);
				this.doDoubleClickToObj(global::Char.myCharz().mobFocus);
			}
			return;
		}
		for (int n = 0; n < GameScr.keySkill.Length; n++)
		{
			if (GameScr.keySkill[n] != null && !GameScr.keySkill[n].paintCanNotUseSkill && GameScr.keySkill[n].template.id != 10 && GameScr.keySkill[n].template.id != 11 && GameScr.keySkill[n].template.id != 14 && GameScr.keySkill[n].template.id != 23 && GameScr.keySkill[n].template.id != 7 && global::Char.myCharz().skillInfoPaint() == null)
			{
				int num2 = (GameScr.keySkill[n].template.manaUseType == 2) ? 1 : ((GameScr.keySkill[n].template.manaUseType == 1) ? ((int)((long)GameScr.keySkill[n].manaUse * global::Char.myCharz().cMPFull / 100L)) : GameScr.keySkill[n].manaUse);
				if (global::Char.myCharz().cMP >= (long)num2)
				{
					if (skill == null)
					{
						skill = GameScr.keySkill[n];
					}
					else if (skill.coolDown < GameScr.keySkill[n].coolDown)
					{
						skill = GameScr.keySkill[n];
					}
				}
			}
		}
		if (skill != null)
		{
			this.doSelectSkill(skill, true);
			this.doDoubleClickToObj(global::Char.myCharz().mobFocus);
		}
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x00068200 File Offset: 0x00066400
	private void doFire(bool isFireByShortCut, bool skipWaypoint)
	{
		GameScr.tam++;
		Waypoint waypoint = global::Char.myCharz().isInEnterOfflinePoint();
		Waypoint waypoint2 = global::Char.myCharz().isInEnterOnlinePoint();
		if (!skipWaypoint && waypoint != null && (global::Char.myCharz().mobFocus == null || (global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.templateId == 0)))
		{
			waypoint.popup.command.performAction();
		}
		else if (!skipWaypoint && waypoint2 != null && (global::Char.myCharz().mobFocus == null || (global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.templateId == 0)))
		{
			waypoint2.popup.command.performAction();
		}
		else
		{
			if (TileMap.mapID == 51 && global::Char.myCharz().npcFocus != null)
			{
				return;
			}
			if (global::Char.myCharz().statusMe != 14)
			{
				global::Char.myCharz().cvx = (global::Char.myCharz().cvy = 0);
				if (global::Char.myCharz().isSelectingSkillUseAlone() && global::Char.myCharz().focusToAttack())
				{
					if (this.checkSkillValid())
					{
						global::Char.myCharz().currentFireByShortcut = isFireByShortCut;
						global::Char.myCharz().useSkillNotFocus();
					}
				}
				else if (this.isAttack())
				{
					if (global::Char.myCharz().isUseChargeSkill() && global::Char.myCharz().focusToAttack())
					{
						if (this.checkSkillValid())
						{
							global::Char.myCharz().currentFireByShortcut = isFireByShortCut;
							global::Char.myCharz().sendUseChargeSkill();
						}
						else
						{
							global::Char.myCharz().stopUseChargeSkill();
						}
					}
					else
					{
						bool flag = TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2);
						global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], (!flag) ? 1 : 0);
						if (flag)
						{
							global::Char.myCharz().delayFall = 20;
						}
						global::Char.myCharz().currentFireByShortcut = isFireByShortCut;
					}
				}
				if (global::Char.myCharz().isSelectingSkillBuffToPlayer())
				{
					this.auto = 0;
				}
			}
		}
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x00068438 File Offset: 0x00066638
	private void askToPick()
	{
		Npc npc = new Npc(5, 0, -100, 100, 5, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
		string nhatvatpham = mResources.nhatvatpham;
		string[] menu = new string[]
		{
			mResources.YES,
			mResources.NO
		};
		npc.idItem = 673;
		GameScr.gI().createMenu(menu, npc);
		ChatPopup.addChatPopupWithIcon(nhatvatpham, 100000, npc, 5820);
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x000684B0 File Offset: 0x000666B0
	private void pickItem()
	{
		if (global::Char.myCharz().itemFocus != null)
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
				}
				else
				{
					this.askToPick();
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

	// Token: 0x06000766 RID: 1894 RVA: 0x000685F4 File Offset: 0x000667F4
	public bool isCharging()
	{
		return global::Char.myCharz().isFlyAndCharge || global::Char.myCharz().isUseSkillAfterCharge || global::Char.myCharz().isStandAndCharge || global::Char.myCharz().isWaitMonkey || this.isSuperPower || global::Char.myCharz().isFreez;
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x0006865C File Offset: 0x0006685C
	public void doSelectSkill(Skill skill, bool isShortcut)
	{
		if (global::Char.myCharz().isCreateDark)
		{
			return;
		}
		if (this.isCharging())
		{
			return;
		}
		if (global::Char.myCharz().taskMaint.taskId <= 1)
		{
			return;
		}
		global::Char.myCharz().myskill = skill;
		if (this.lastSkill != skill && this.lastSkill != null)
		{
			Service.gI().selectSkill((int)skill.template.id);
			this.saveRMSCurrentSkill(skill.template.id);
			this.resetButton();
			this.lastSkill = skill;
			this.selectedIndexSkill = -1;
			GameScr.gI().auto = 0;
			return;
		}
		if (global::Char.myCharz().isUseSkillSpec())
		{
			Res.outz(">>>use skill spec: " + skill.template.id);
			global::Char.myCharz().sendNewAttack((short)skill.template.id);
			this.saveRMSCurrentSkill(skill.template.id);
			this.resetButton();
			this.lastSkill = skill;
			this.selectedIndexSkill = -1;
			GameScr.gI().auto = 0;
			return;
		}
		if (global::Char.myCharz().isSelectingSkillUseAlone())
		{
			Res.outz("use skill not focus");
			this.doUseSkillNotFocus(skill);
			this.lastSkill = skill;
			return;
		}
		this.selectedIndexSkill = -1;
		if (skill != null)
		{
			Res.outz("only select skill");
			if (this.lastSkill != skill)
			{
				Service.gI().selectSkill((int)skill.template.id);
				this.saveRMSCurrentSkill(skill.template.id);
				this.resetButton();
			}
			if (global::Char.myCharz().charFocus == null && global::Char.myCharz().isSelectingSkillBuffToPlayer())
			{
				return;
			}
			if (global::Char.myCharz().focusToAttack())
			{
				this.doFire(isShortcut, true);
				this.doSeleckSkillFlag = true;
			}
			this.lastSkill = skill;
		}
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x00068838 File Offset: 0x00066A38
	public void doUseSkill(Skill skill, bool isShortcut)
	{
		if ((TileMap.mapID == 112 || TileMap.mapID == 113) && (int)global::Char.myCharz().cTypePk == 0)
		{
			return;
		}
		if (skill != null && skill.template.isUseAlone())
		{
			Res.outz("HERE");
			this.doUseSkillNotFocus(skill);
			return;
		}
		this.selectedIndexSkill = -1;
		if (skill != null)
		{
			Service.gI().selectSkill((int)skill.template.id);
			this.saveRMSCurrentSkill(skill.template.id);
			this.resetButton();
			global::Char.myCharz().myskill = skill;
			this.doFire(isShortcut, true);
		}
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x000688DC File Offset: 0x00066ADC
	public void doUseSkillNotFocus(Skill skill)
	{
		if ((TileMap.mapID == 112 || TileMap.mapID == 113) && (int)global::Char.myCharz().cTypePk == 0)
		{
			return;
		}
		if (this.checkSkillValid())
		{
			this.selectedIndexSkill = -1;
			if (skill != null)
			{
				Service.gI().selectSkill((int)skill.template.id);
				this.saveRMSCurrentSkill(skill.template.id);
				this.resetButton();
				global::Char.myCharz().myskill = skill;
				global::Char.myCharz().useSkillNotFocus();
				global::Char.myCharz().currentFireByShortcut = true;
				this.auto = 0;
			}
		}
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x00068980 File Offset: 0x00066B80
	public void sortSkill()
	{
		for (int i = 0; i < global::Char.myCharz().vSkillFight.size() - 1; i++)
		{
			Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(i);
			for (int j = i + 1; j < global::Char.myCharz().vSkillFight.size(); j++)
			{
				Skill skill2 = (Skill)global::Char.myCharz().vSkillFight.elementAt(j);
				if ((int)skill2.template.id < (int)skill.template.id)
				{
					Skill skill3 = skill2;
					skill2 = skill;
					skill = skill3;
					global::Char.myCharz().vSkillFight.setElementAt(skill, i);
					global::Char.myCharz().vSkillFight.setElementAt(skill2, j);
				}
			}
		}
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x00068A44 File Offset: 0x00066C44
	public void updateKeyTouchCapcha()
	{
		if (this.isNotPaintTouchControl())
		{
			return;
		}
		for (int i = 0; i < this.strCapcha.Length; i++)
		{
			this.keyCapcha[i] = -1;
			if (GameCanvas.isTouchControl)
			{
				int num = (GameCanvas.w - this.strCapcha.Length * GameScr.disXC) / 2;
				int w = this.strCapcha.Length * GameScr.disXC;
				int y = GameCanvas.h - 40;
				int h = GameScr.disXC;
				if (GameCanvas.isPointerHoldIn(num, y, w, h))
				{
					int num2 = (GameCanvas.px - num) / GameScr.disXC;
					if (i == num2)
					{
						this.keyCapcha[i] = 1;
					}
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease && i == num2)
					{
						char[] array = this.keyInput.ToCharArray();
						MyVector myVector = new MyVector();
						for (int j = 0; j < array.Length; j++)
						{
							myVector.addElement(array[j] + string.Empty);
						}
						myVector.removeElementAt(0);
						myVector.insertElementAt(this.strCapcha[i] + string.Empty, myVector.size());
						this.keyInput = string.Empty;
						for (int k = 0; k < myVector.size(); k++)
						{
							this.keyInput += ((string)myVector.elementAt(k)).ToUpper();
						}
						Service.gI().mobCapcha(this.strCapcha[i]);
					}
				}
			}
		}
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x00068BF0 File Offset: 0x00066DF0
	public bool checkClickToCapcha()
	{
		if (this.mobCapcha == null)
		{
			return false;
		}
		int x = (GameCanvas.w - 5 * GameScr.disXC) / 2;
		int w = 5 * GameScr.disXC;
		int y = GameCanvas.h - 40;
		int h = GameScr.disXC;
		return GameCanvas.isPointerHoldIn(x, y, w, h);
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x00068C44 File Offset: 0x00066E44
	public void checkMouseChat()
	{
		if (GameCanvas.isMouseFocus(GameScr.xC, GameScr.yC, 34, 34))
		{
			if (!TileMap.isOfflineMap())
			{
				mScreen.keyMouse = 15;
			}
		}
		else if (GameCanvas.isMouseFocus(GameScr.xHP, GameScr.yHP, 40, 40))
		{
			if (global::Char.myCharz().statusMe != 14)
			{
				mScreen.keyMouse = 10;
			}
		}
		else if (GameCanvas.isMouseFocus(GameScr.xF, GameScr.yF, 40, 40))
		{
			if (global::Char.myCharz().statusMe != 14)
			{
				mScreen.keyMouse = 5;
			}
		}
		else if (this.cmdMenu != null && GameCanvas.isMouseFocus(this.cmdMenu.x, this.cmdMenu.y, this.cmdMenu.w / 2, this.cmdMenu.h))
		{
			mScreen.keyMouse = 1;
		}
		else
		{
			mScreen.keyMouse = -1;
		}
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x00068D3C File Offset: 0x00066F3C
	private void updateKeyTouchControl()
	{
		if (this.isNotPaintTouchControl())
		{
			return;
		}
		if (MainMod.isShowCharsInMap)
		{
			if (MainMod.isMeInNRDMap())
			{
				for (int i = 0; i < MainMod.listCharsInMap.Count; i++)
				{
					if (GameCanvas.isPointerHoldIn(GameCanvas.w - MainMod.widthRect, 35 + (MainMod.heightRect + 1) * i, MainMod.widthRect, MainMod.heightRect))
					{
						GameCanvas.isPointerJustDown = false;
						this.isPointerDowning = false;
						if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
						{
							global::Char @char = MainMod.listCharsInMap[i];
							if (global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.cName == @char.cName)
							{
								MainMod.TeleportTo(global::Char.myCharz().charFocus.cx, global::Char.myCharz().charFocus.cy);
							}
							else
							{
								global::Char.myCharz().mobFocus = null;
								global::Char.myCharz().npcFocus = null;
								global::Char.myCharz().itemFocus = null;
								global::Char.myCharz().charFocus = null;
								global::Char.myCharz().charFocus = @char;
							}
							global::Char.myCharz().currentMovePoint = null;
							GameCanvas.clearAllPointerEvent();
							return;
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < MainMod.listCharsInMap.Count; j++)
				{
					if (GameCanvas.isPointerHoldIn(GameCanvas.w - MainMod.widthRect, 95 + (MainMod.heightRect + 1) * j, MainMod.widthRect, MainMod.heightRect))
					{
						GameCanvas.isPointerJustDown = false;
						this.isPointerDowning = false;
						if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
						{
							global::Char char2 = MainMod.listCharsInMap[j];
							if (global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.cName == char2.cName)
							{
								MainMod.TeleportTo(global::Char.myCharz().charFocus.cx, global::Char.myCharz().charFocus.cy);
							}
							else
							{
								global::Char.myCharz().mobFocus = null;
								global::Char.myCharz().npcFocus = null;
								global::Char.myCharz().itemFocus = null;
								global::Char.myCharz().charFocus = null;
								global::Char.myCharz().charFocus = char2;
							}
							global::Char.myCharz().currentMovePoint = null;
							GameCanvas.clearAllPointerEvent();
							return;
						}
					}
				}
			}
		}
		mScreen.keyTouch = -1;
		if (GameCanvas.isTouchControl)
		{
			if (GameCanvas.isPointerHoldIn(0, 0, 60, 50) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				if (global::Char.myCharz().cmdMenu != null)
				{
					global::Char.myCharz().cmdMenu.performAction();
				}
				global::Char.myCharz().currentMovePoint = null;
				GameCanvas.clearAllPointerEvent();
				this.flareFindFocus = true;
				this.flareTime = 5;
				return;
			}
			if (Main.isPC)
			{
				this.checkMouseChat();
			}
			if (!TileMap.isOfflineMap() && GameCanvas.isPointerHoldIn(GameScr.xC, GameScr.yC, 34, 34))
			{
				mScreen.keyTouch = 15;
				GameCanvas.isPointerJustDown = false;
				this.isPointerDowning = false;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					ChatTextField.gI().startChat(this, string.Empty);
					SoundMn.gI().buttonClick();
					global::Char.myCharz().currentMovePoint = null;
					GameCanvas.clearAllPointerEvent();
					return;
				}
			}
			if (global::Char.myCharz().cmdMenu != null && GameCanvas.isPointerHoldIn(global::Char.myCharz().cmdMenu.x - 17, global::Char.myCharz().cmdMenu.y - 17, 34, 34))
			{
				mScreen.keyTouch = 20;
				GameCanvas.isPointerJustDown = false;
				this.isPointerDowning = false;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					GameCanvas.clearAllPointerEvent();
					global::Char.myCharz().cmdMenu.performAction();
					return;
				}
			}
			this.updateGamePad();
			if (((GameScr.isAnalog != 0) ? GameCanvas.isPointerHoldIn(GameScr.xHP, GameScr.yHP + 10, 34, 34) : GameCanvas.isPointerHoldIn(GameScr.xHP, GameScr.yHP + 10, 40, 40)) && global::Char.myCharz().statusMe != 14 && this.mobCapcha == null)
			{
				mScreen.keyTouch = 10;
				GameCanvas.isPointerJustDown = false;
				this.isPointerDowning = false;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					GameCanvas.keyPressed[10] = true;
					GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
				}
			}
			if (((GameScr.isAnalog != 0) ? GameCanvas.isPointerHoldIn(GameScr.xHP + 5, GameScr.yHP - 6 - 34 + 10, 34, 34) : GameCanvas.isPointerHoldIn(GameScr.xHP + 5, GameScr.yHP - 6 - 40 + 10, 40, 40)) && global::Char.myCharz().statusMe != 14 && this.mobCapcha == null)
			{
				if (GameScr.isPickNgocRong)
				{
					mScreen.keyTouch = 14;
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						GameCanvas.keyPressed[14] = true;
						GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
						GameScr.isPickNgocRong = false;
						Service.gI().useItem(-1, -1, -1, -1);
					}
				}
				else if (GameScr.isudungCapsun4)
				{
					mScreen.keyTouch = 14;
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						GameCanvas.keyPressed[14] = true;
						GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
						for (int k = 0; k < global::Char.myCharz().arrItemBag.Length; k++)
						{
							Item item = global::Char.myCharz().arrItemBag[k];
							if (item != null)
							{
								Res.err("find " + item.template.id);
								if (item.template.id == 194)
								{
									GameScr.isudungCapsun4 = (item.quantity > 0);
									if (GameScr.isudungCapsun4)
									{
										Service.gI().useItem(0, 1, (sbyte)k, -1);
										break;
									}
								}
							}
						}
					}
				}
				else if (GameScr.isudungCapsun3)
				{
					mScreen.keyTouch = 14;
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						GameCanvas.keyPressed[14] = true;
						GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
						for (int l = 0; l < global::Char.myCharz().arrItemBag.Length; l++)
						{
							Item item2 = global::Char.myCharz().arrItemBag[l];
							if (item2 != null && item2.template.id == 193)
							{
								GameScr.isudungCapsun3 = (item2.quantity > 0);
								if (GameScr.isudungCapsun3)
								{
									Service.gI().useItem(0, 1, (sbyte)l, -1);
									break;
								}
							}
						}
					}
				}
			}
		}
		if (this.mobCapcha != null)
		{
			this.updateKeyTouchCapcha();
		}
		else if (GameScr.isHaveSelectSkill)
		{
			if (this.isCharging())
			{
				return;
			}
			this.keyTouchSkill = -1;
			bool flag = false;
			if (GameScr.onScreenSkill.Length > 5 && (GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12, GameScr.yS[0] - GameScr.wSkill / 2 + 12, 5 * GameScr.wSkill, GameScr.wSkill) || GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[5] - GameScr.wSkill / 2 + 12, GameScr.yS[5] - GameScr.wSkill / 2 + 12, 5 * GameScr.wSkill, GameScr.wSkill)))
			{
				flag = true;
			}
			if (flag || GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12, GameScr.yS[0] - GameScr.wSkill / 2 + 12, 5 * GameScr.wSkill, GameScr.wSkill) || (!GameCanvas.isTouchControl && GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12, GameScr.yS[0] - GameScr.wSkill / 2 + 12, GameScr.wSkill, GameScr.onScreenSkill.Length * GameScr.wSkill)))
			{
				GameCanvas.isPointerJustDown = false;
				this.isPointerDowning = false;
				int num = (GameCanvas.pxLast - (GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12)) / GameScr.wSkill;
				if (flag && GameCanvas.pyLast < GameScr.yS[0])
				{
					num += 5;
				}
				this.keyTouchSkill = num;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
					this.selectedIndexSkill = num;
					if (GameScr.indexSelect < 0)
					{
						GameScr.indexSelect = 0;
					}
					if (!Main.isPC)
					{
						if (this.selectedIndexSkill > GameScr.onScreenSkill.Length - 1)
						{
							this.selectedIndexSkill = GameScr.onScreenSkill.Length - 1;
						}
					}
					else if (this.selectedIndexSkill > GameScr.keySkill.Length - 1)
					{
						this.selectedIndexSkill = GameScr.keySkill.Length - 1;
					}
					Skill skill;
					if (!Main.isPC)
					{
						skill = GameScr.onScreenSkill[this.selectedIndexSkill];
					}
					else
					{
						skill = GameScr.keySkill[this.selectedIndexSkill];
					}
					if (skill != null)
					{
						this.doSelectSkill(skill, true);
					}
				}
			}
		}
		if (GameCanvas.isPointerJustRelease)
		{
			if (GameCanvas.keyHold[1] || (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] || GameCanvas.keyHold[3]) || GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] || GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
			{
				GameCanvas.isPointerJustRelease = false;
			}
			GameCanvas.keyHold[1] = false;
			GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = false;
			GameCanvas.keyHold[3] = false;
			GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = false;
			GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = false;
		}
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x00007834 File Offset: 0x00005A34
	public void setCharJumpAtt()
	{
		global::Char.myCharz().cvy = -10;
		global::Char.myCharz().statusMe = 3;
		global::Char.myCharz().cp1 = 0;
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x000696B4 File Offset: 0x000678B4
	public void setCharJump(int cvx)
	{
		if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0)
		{
			Service.gI().charMove();
		}
		global::Char.myCharz().cvy = -10;
		global::Char.myCharz().cvx = cvx;
		global::Char.myCharz().statusMe = 3;
		global::Char.myCharz().cp1 = 0;
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x0006972C File Offset: 0x0006792C
	public void updateOpen()
	{
		if (!this.isstarOpen)
		{
			return;
		}
		if (this.moveUp > -3)
		{
			this.moveUp -= 4;
		}
		else
		{
			this.moveUp = -2;
		}
		if (this.moveDow < GameCanvas.h + 3)
		{
			this.moveDow += 4;
		}
		else
		{
			this.moveDow = GameCanvas.h + 2;
		}
		if (this.moveUp <= -2 && this.moveDow >= GameCanvas.h + 2)
		{
			this.isstarOpen = false;
		}
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x000045ED File Offset: 0x000027ED
	public void initCreateCommand()
	{
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x000045ED File Offset: 0x000027ED
	public void checkCharFocus()
	{
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x000697C8 File Offset: 0x000679C8
	public void updateXoSo()
	{
		if (this.tShow != 0)
		{
			GameScr.currXS = mSystem.currentTimeMillis();
			if (GameScr.currXS - GameScr.lastXS > 1000L)
			{
				GameScr.lastXS = mSystem.currentTimeMillis();
				GameScr.secondXS++;
			}
			if (GameScr.secondXS > 20)
			{
				for (int i = 0; i < this.winnumber.Length; i++)
				{
					this.randomNumber[i] = this.winnumber[i];
				}
				this.tShow--;
				if (this.tShow == 0)
				{
					this.yourNumber = string.Empty;
					GameScr.info1.addInfo(this.strFinish, 0);
					GameScr.secondXS = 0;
				}
				return;
			}
			if (this.moveIndex > this.winnumber.Length - 1)
			{
				this.tShow--;
				if (this.tShow == 0)
				{
					this.yourNumber = string.Empty;
					GameScr.info1.addInfo(this.strFinish, 0);
				}
				return;
			}
			if (this.moveIndex < this.randomNumber.Length)
			{
				if (this.tMove[this.moveIndex] == 15)
				{
					if (this.randomNumber[this.moveIndex] == this.winnumber[this.moveIndex] - 1)
					{
						this.delayMove[this.moveIndex] = 10;
					}
					if (this.randomNumber[this.moveIndex] == this.winnumber[this.moveIndex])
					{
						this.tMove[this.moveIndex] = -1;
						this.moveIndex++;
					}
				}
				else if (GameCanvas.gameTick % 5 == 0)
				{
					this.tMove[this.moveIndex]++;
				}
			}
			for (int j = 0; j < this.winnumber.Length; j++)
			{
				if (this.tMove[j] != -1)
				{
					this.moveCount[j]++;
					if (this.moveCount[j] > this.tMove[j] + this.delayMove[j])
					{
						this.moveCount[j] = 0;
						this.randomNumber[j]++;
						if (this.randomNumber[j] >= 10)
						{
							this.randomNumber[j] = 0;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x00069A14 File Offset: 0x00067C14
	public override void update()
	{
		MainMod.Update();
		if (GameCanvas.keyPressed[16])
		{
			GameCanvas.keyPressed[16] = false;
			global::Char.myCharz().findNextFocusByKey();
		}
		if (GameCanvas.keyPressed[13] && !GameCanvas.panel.isShow)
		{
			GameCanvas.keyPressed[13] = false;
			global::Char.myCharz().findNextFocusByKey();
		}
		if (GameCanvas.keyPressed[17])
		{
			GameCanvas.keyPressed[17] = false;
			global::Char.myCharz().searchItem();
			if (global::Char.myCharz().itemFocus != null)
			{
				this.pickItem();
			}
		}
		if (GameCanvas.gameTick % 100 == 0 && TileMap.mapID == 137)
		{
			GameScr.shock_scr = 30;
		}
		this.updateXoSo();
		mSystem.checkAdComlete();
		try
		{
			if (LoginScr.isContinueToLogin)
			{
				LoginScr.isContinueToLogin = false;
			}
			if (GameScr.tickMove == 1)
			{
				GameScr.lastTick = mSystem.currentTimeMillis();
			}
			if (GameScr.tickMove == 100)
			{
				GameScr.tickMove = 0;
				GameScr.currTick = mSystem.currentTimeMillis();
				int second = (int)(GameScr.currTick - GameScr.lastTick) / 1000;
				Service.gI().checkMMove(second);
			}
			if (GameScr.lockTick > 0)
			{
				GameScr.lockTick--;
				if (GameScr.lockTick == 0)
				{
					Controller.isStopReadMessage = false;
				}
			}
			this.checkCharFocus();
			GameCanvas.debug("E1", 0);
			GameScr.updateCamera();
			GameCanvas.debug("E2", 0);
			ChatTextField.gI().update();
			GameCanvas.debug("E3", 0);
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				((global::Char)GameScr.vCharInMap.elementAt(i)).update();
			}
			for (int j = 0; j < Teleport.vTeleport.size(); j++)
			{
				((Teleport)Teleport.vTeleport.elementAt(j)).update();
			}
			global::Char.myCharz().update();
			int statusMe = global::Char.myCharz().statusMe;
			if (this.popUpYesNo != null)
			{
				this.popUpYesNo.update();
			}
			EffecMn.update();
			GameCanvas.debug("E5x", 0);
			for (int k = 0; k < GameScr.vMob.size(); k++)
			{
				((Mob)GameScr.vMob.elementAt(k)).update();
			}
			GameCanvas.debug("E6", 0);
			for (int l = 0; l < GameScr.vNpc.size(); l++)
			{
				((Npc)GameScr.vNpc.elementAt(l)).update();
			}
			this.nSkill = GameScr.onScreenSkill.Length;
			for (int m = GameScr.onScreenSkill.Length - 1; m >= 0; m--)
			{
				if (GameScr.onScreenSkill[m] != null)
				{
					this.nSkill = m + 1;
					break;
				}
				this.nSkill--;
			}
			GameScr.setSkillBarPosition();
			GameCanvas.debug("E7", 0);
			GameCanvas.gI().updateDust();
			GameCanvas.debug("E8", 0);
			GameScr.updateFlyText();
			PopUp.updateAll();
			GameScr.updateSplash();
			this.updateSS();
			GameCanvas.updateBG();
			GameCanvas.debug("E9", 0);
			this.updateClickToArrow();
			GameCanvas.debug("E10", 0);
			for (int n = 0; n < GameScr.vItemMap.size(); n++)
			{
				((ItemMap)GameScr.vItemMap.elementAt(n)).update();
			}
			GameCanvas.debug("E11", 0);
			GameCanvas.debug("E13", 0);
			for (int num = Effect2.vRemoveEffect2.size() - 1; num >= 0; num--)
			{
				Effect2.vEffect2.removeElement(Effect2.vRemoveEffect2.elementAt(num));
				Effect2.vRemoveEffect2.removeElementAt(num);
			}
			for (int num2 = 0; num2 < Effect2.vEffect2.size(); num2++)
			{
				((Effect2)Effect2.vEffect2.elementAt(num2)).update();
			}
			for (int num3 = 0; num3 < Effect2.vEffect2Outside.size(); num3++)
			{
				((Effect2)Effect2.vEffect2Outside.elementAt(num3)).update();
			}
			for (int num4 = 0; num4 < Effect2.vAnimateEffect.size(); num4++)
			{
				((Effect2)Effect2.vAnimateEffect.elementAt(num4)).update();
			}
			for (int num5 = 0; num5 < Effect2.vEffectFeet.size(); num5++)
			{
				((Effect2)Effect2.vEffectFeet.elementAt(num5)).update();
			}
			for (int num6 = 0; num6 < Effect2.vEffect3.size(); num6++)
			{
				((Effect2)Effect2.vEffect3.elementAt(num6)).update();
			}
			BackgroudEffect.updateEff();
			GameScr.info1.update();
			GameScr.info2.update();
			GameCanvas.debug("E15", 0);
			if (GameScr.currentCharViewInfo != null && !GameScr.currentCharViewInfo.Equals(global::Char.myCharz()))
			{
				GameScr.currentCharViewInfo.update();
			}
			this.runArrow++;
			if (this.runArrow > 3)
			{
				this.runArrow = 0;
			}
			if (this.isInjureHp)
			{
				this.twHp += 1L;
				if (this.twHp == 20L)
				{
					this.twHp = 0L;
					this.isInjureHp = false;
				}
			}
			else if (this.dHP > global::Char.myCharz().cHP)
			{
				long num7 = this.dHP - global::Char.myCharz().cHP >> 1;
				if (num7 < 1L)
				{
					num7 = 1L;
				}
				this.dHP -= num7;
			}
			else
			{
				this.dHP = global::Char.myCharz().cHP;
			}
			if (this.isInjureMp)
			{
				this.twMp += 1L;
				if (this.twMp == 20L)
				{
					this.twMp = 0L;
					this.isInjureMp = false;
				}
			}
			else if (this.dMP > global::Char.myCharz().cMP)
			{
				long num8 = this.dMP - global::Char.myCharz().cMP >> 1;
				if (num8 < 1L)
				{
					num8 = 1L;
				}
				this.dMP -= num8;
			}
			else
			{
				this.dMP = global::Char.myCharz().cMP;
			}
			if (this.tMenuDelay > 0)
			{
				this.tMenuDelay--;
			}
			if (this.isRongThanMenu())
			{
				int num9 = 100;
				while (this.yR - num9 < GameScr.cmy)
				{
					GameScr.cmy--;
				}
			}
			for (int num10 = 0; num10 < global::Char.vItemTime.size(); num10++)
			{
				((ItemTime)global::Char.vItemTime.elementAt(num10)).update();
			}
			for (int num11 = 0; num11 < GameScr.textTime.size(); num11++)
			{
				((ItemTime)GameScr.textTime.elementAt(num11)).update();
			}
			this.updateChatVip();
		}
		catch (Exception)
		{
		}
		if (GameCanvas.gameTick % 4000 == 1000)
		{
			GameScr.checkRemoveImage();
		}
		EffectManager.update();
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x000045ED File Offset: 0x000027ED
	public void updateKeyChatPopUp()
	{
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x00007858 File Offset: 0x00005A58
	public bool isRongThanMenu()
	{
		return this.isMeCallRongThan;
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x0006A0C4 File Offset: 0x000682C4
	public void paintEffect(mGraphics g)
	{
		for (int i = 0; i < Effect2.vEffect2.size(); i++)
		{
			Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
			if (effect != null && !(effect is ChatPopup))
			{
				effect.paint(g);
			}
		}
		if (!GameCanvas.lowGraphic)
		{
			for (int i = 0; i < Effect2.vAnimateEffect.size(); i++)
			{
				Effect2 effect2 = (Effect2)Effect2.vAnimateEffect.elementAt(i);
				effect2.paint(g);
			}
		}
		for (int i = 0; i < Effect2.vEffect2Outside.size(); i++)
		{
			Effect2 effect3 = (Effect2)Effect2.vEffect2Outside.elementAt(i);
			effect3.paint(g);
		}
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x0006A188 File Offset: 0x00068388
	public void paintBgItem(mGraphics g, int layer)
	{
		for (int i = 0; i < TileMap.vCurrItem.size(); i++)
		{
			BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(i);
			if (bgItem.idImage != -1 && (int)bgItem.layer == layer)
			{
				bgItem.paint(g);
			}
		}
		if (TileMap.mapID == 48 && layer == 3 && GameCanvas.bgW != null && GameCanvas.bgW[0] != 0)
		{
			for (int j = 0; j < TileMap.pxw / GameCanvas.bgW[0] + 1; j++)
			{
				g.drawImage(GameCanvas.imgBG[0], j * GameCanvas.bgW[0], TileMap.pxh - GameCanvas.bgH[0] - 70, 0);
			}
		}
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x00007868 File Offset: 0x00005A68
	public void paintBlackSky(mGraphics g)
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		g.fillTrans(GameScr.imgTrans, 0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x0006A254 File Offset: 0x00068454
	public void paintCapcha(mGraphics g)
	{
		MobCapcha.paint(g, global::Char.myCharz().cx, global::Char.myCharz().cy);
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		if (GameCanvas.menu.showMenu)
		{
			return;
		}
		if (GameCanvas.panel.isShow)
		{
			return;
		}
		if (ChatPopup.currChatPopup != null)
		{
			return;
		}
		if (GameCanvas.isTouch)
		{
			for (int i = 0; i < this.strCapcha.Length; i++)
			{
				int x = (GameCanvas.w - this.strCapcha.Length * GameScr.disXC) / 2 + i * GameScr.disXC + GameScr.disXC / 2;
				if (this.keyCapcha[i] == -1)
				{
					g.drawImage(GameScr.imgNut, x, GameCanvas.h - 25, 3);
					mFont.tahoma_7b_dark.drawString(g, this.strCapcha[i] + string.Empty, x, GameCanvas.h - 30, 2);
				}
				else
				{
					g.drawImage(GameScr.imgNutF, x, GameCanvas.h - 25, 3);
					mFont.tahoma_7b_green2.drawString(g, this.strCapcha[i] + string.Empty, x, GameCanvas.h - 30, 2);
				}
			}
		}
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x0006A3A8 File Offset: 0x000685A8
	public override void paint(mGraphics g)
	{
		if (global::Char.isLoadingMap)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			return;
		}
		GameScr.countEff = 0;
		if (GameScr.isPaint)
		{
			GameCanvas.debug("PA1", 1);
			if (this.isFreez || (this.isUseFreez && ChatPopup.currChatPopup == null))
			{
				this.dem++;
				if ((this.dem < 30 && this.dem >= 0 && GameCanvas.gameTick % 4 == 0) || (this.dem >= 30 && this.dem <= 50 && GameCanvas.gameTick % 3 == 0) || this.dem > 50)
				{
					int num = this.dem;
					if (this.isUseFreez)
					{
						this.isUseFreez = false;
						this.dem = 0;
						if (this.activeRongThan)
						{
							this.callRongThan(this.xR, this.yR);
						}
						else
						{
							this.hideRongThan();
						}
					}
					this.paintInfoBar(g);
					g.translate(-GameScr.cmx, -GameScr.cmy);
					g.translate(0, GameCanvas.transY);
					global::Char.myCharz().paint(g);
					mSystem.paintFlyText(g);
					GameScr.resetTranslate(g);
					this.paintSelectedSkill(g);
				}
			}
			GameCanvas.debug("PA2", 1);
			GameCanvas.paintBGGameScr(g);
			if ((this.isRongThanXuatHien || this.isFireWorks) && TileMap.bgID != 3)
			{
				this.paintBlackSky(g);
			}
			GameCanvas.debug("PA3", 1);
			if (GameScr.shock_scr > 0)
			{
				g.translate(-GameScr.cmx + GameScr.shock_x[GameScr.shock_scr % GameScr.shock_x.Length], -GameScr.cmy + GameScr.shock_y[GameScr.shock_scr % GameScr.shock_y.Length]);
				GameScr.shock_scr--;
			}
			else
			{
				g.translate(-GameScr.cmx, -GameScr.cmy);
			}
			if (this.isSuperPower)
			{
				int tx = (GameCanvas.gameTick % 3 != 0) ? -3 : 3;
				g.translate(tx, 0);
			}
			BackgroudEffect.paintBehindTileAll(g);
			EffecMn.paintLayer1(g);
			TileMap.paintTilemap(g);
			TileMap.paintOutTilemap(g);
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (@char.isMabuHold && TileMap.mapID == 128)
				{
					@char.paintHeadWithXY(g, @char.cx, @char.cy, 0);
				}
			}
			if (global::Char.myCharz().isMabuHold && TileMap.mapID == 128)
			{
				global::Char.myCharz().paintHeadWithXY(g, global::Char.myCharz().cx, global::Char.myCharz().cy, 0);
			}
			if (global::Char.myCharz().cmdMenu != null && GameCanvas.isTouch)
			{
				if (mScreen.keyTouch == 20)
				{
					g.drawImage(GameScr.imgChat2, global::Char.myCharz().cmdMenu.x + GameScr.cmx, global::Char.myCharz().cmdMenu.y + GameScr.cmy, mGraphics.HCENTER | mGraphics.VCENTER);
				}
				else
				{
					g.drawImage(GameScr.imgChat, global::Char.myCharz().cmdMenu.x + GameScr.cmx, global::Char.myCharz().cmdMenu.y + GameScr.cmy, mGraphics.HCENTER | mGraphics.VCENTER);
				}
			}
			GameCanvas.debug("PA4", 1);
			GameCanvas.debug("PA5", 1);
			for (int j = 0; j < Effect2.vEffectFeet.size(); j++)
			{
				((Effect2)Effect2.vEffectFeet.elementAt(j)).paint(g);
			}
			for (int k = 0; k < Teleport.vTeleport.size(); k++)
			{
				((Teleport)Teleport.vTeleport.elementAt(k)).paintHole(g);
			}
			for (int l = 0; l < GameScr.vNpc.size(); l++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(l);
				if (npc.cHP > 0L)
				{
					npc.paintShadow(g);
				}
			}
			for (int m = 0; m < GameScr.vNpc.size(); m++)
			{
				((Npc)GameScr.vNpc.elementAt(m)).paint(g);
			}
			g.translate(0, GameCanvas.transY);
			GameCanvas.debug("PA7", 1);
			GameCanvas.debug("PA8", 1);
			for (int n = 0; n < GameScr.vCharInMap.size(); n++)
			{
				global::Char char2 = null;
				try
				{
					char2 = (global::Char)GameScr.vCharInMap.elementAt(n);
				}
				catch (Exception ex)
				{
					Cout.LogError("Loi ham paint char gamesc: " + ex.ToString());
				}
				if (char2 != null && (!GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop()) && char2.isShadown)
				{
					char2.paintShadow(g);
				}
			}
			global::Char.myCharz().paintShadow(g);
			for (int num2 = 0; num2 < GameScr.vMob.size(); num2++)
			{
				((Mob)GameScr.vMob.elementAt(num2)).paint(g);
			}
			for (int num3 = 0; num3 < Teleport.vTeleport.size(); num3++)
			{
				((Teleport)Teleport.vTeleport.elementAt(num3)).paint(g);
			}
			for (int num4 = 0; num4 < GameScr.vCharInMap.size(); num4++)
			{
				global::Char char3 = null;
				try
				{
					char3 = (global::Char)GameScr.vCharInMap.elementAt(num4);
				}
				catch (Exception)
				{
				}
				if (char3 != null && (!GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop()))
				{
					char3.paint(g);
				}
			}
			global::Char.myCharz().paint(g);
			if (global::Char.myCharz().skillPaint != null && global::Char.myCharz().skillInfoPaint() != null && global::Char.myCharz().indexSkill < global::Char.myCharz().skillInfoPaint().Length)
			{
				global::Char.myCharz().paintCharWithSkill(g);
				global::Char.myCharz().paintMount2(g);
			}
			for (int num5 = 0; num5 < GameScr.vCharInMap.size(); num5++)
			{
				global::Char char4 = null;
				try
				{
					char4 = (global::Char)GameScr.vCharInMap.elementAt(num5);
				}
				catch (Exception ex2)
				{
					Cout.LogError("Loi ham paint char gamescr: " + ex2.ToString());
				}
				if (char4 != null && (!GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop()))
				{
					if (char4.skillPaint != null && char4.skillInfoPaint() != null && char4.indexSkill < char4.skillInfoPaint().Length)
					{
						char4.paintCharWithSkill(g);
						char4.paintMount2(g);
					}
					char4.paintEffect(g);
				}
			}
			for (int num6 = 0; num6 < GameScr.vItemMap.size(); num6++)
			{
				((ItemMap)GameScr.vItemMap.elementAt(num6)).paint(g);
			}
			g.translate(0, -GameCanvas.transY);
			GameCanvas.debug("PA9", 1);
			GameScr.paintSplash(g);
			GameCanvas.debug("PA10", 1);
			GameCanvas.debug("PA11", 1);
			GameCanvas.debug("PA13", 1);
			this.paintEffect(g);
			for (int num7 = 0; num7 < GameScr.vNpc.size(); num7++)
			{
				((Npc)GameScr.vNpc.elementAt(num7)).paintName(g);
			}
			for (int num8 = 0; num8 < GameScr.vNpc.size(); num8++)
			{
				Npc npc2 = (Npc)GameScr.vNpc.elementAt(num8);
				if (npc2.chatInfo != null && npc2 != null)
				{
					npc2.chatInfo.paint(g, npc2.cx, npc2.cy - npc2.ch - GameCanvas.transY, npc2.cdir);
				}
			}
			for (int num9 = 0; num9 < GameScr.vCharInMap.size(); num9++)
			{
				global::Char char5 = null;
				try
				{
					char5 = (global::Char)GameScr.vCharInMap.elementAt(num9);
				}
				catch (Exception)
				{
				}
				if (char5 != null && char5.chatInfo != null)
				{
					char5.chatInfo.paint(g, char5.cx, char5.cy - char5.ch, char5.cdir);
				}
			}
			if (global::Char.myCharz().chatInfo != null)
			{
				global::Char.myCharz().chatInfo.paint(g, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, global::Char.myCharz().cdir);
			}
			PopUp.paintAll(g);
			if (TileMap.mapID == 120)
			{
				if (this.percentMabu != 100)
				{
					int w = (int)this.percentMabu * mGraphics.getImageWidth(GameScr.imgHPLost) / 100;
					sbyte b = this.percentMabu;
					g.drawImage(GameScr.imgHPLost, TileMap.pxw / 2 - mGraphics.getImageWidth(GameScr.imgHPLost) / 2, 220, 0);
					g.setClip(TileMap.pxw / 2 - mGraphics.getImageWidth(GameScr.imgHPLost) / 2, 220, w, 10);
					g.drawImage(GameScr.imgHP, TileMap.pxw / 2 - mGraphics.getImageWidth(GameScr.imgHPLost) / 2, 220, 0);
					g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				}
				if (this.mabuEff)
				{
					this.tMabuEff++;
					if (GameCanvas.gameTick % 3 == 0)
					{
						EffecMn.addEff(new Effect(19, Res.random(TileMap.pxw / 2 - 50, TileMap.pxw / 2 + 50), 340, 2, 1, -1));
					}
					if (GameCanvas.gameTick % 15 == 0)
					{
						EffecMn.addEff(new Effect(18, Res.random(TileMap.pxw / 2 - 5, TileMap.pxw / 2 + 5), Res.random(300, 320), 2, 1, -1));
					}
					if (this.tMabuEff == 100)
					{
						this.activeSuperPower(TileMap.pxw / 2, 300);
					}
					if (this.tMabuEff == 110)
					{
						this.tMabuEff = 0;
						this.mabuEff = false;
					}
				}
			}
			bool flag = true;
			for (int num10 = 0; num10 < BackgroudEffect.vBgEffect.size(); num10++)
			{
				if (((BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(num10)).typeEff == 0)
				{
					flag = false;
					break;
				}
			}
			if (mGraphics.zoomLevel <= 1 || Main.isIpod || Main.isIphone4)
			{
				flag = false;
			}
			if (flag && !this.isRongThanXuatHien)
			{
				int num11 = TileMap.pxw / (mGraphics.getImageWidth(TileMap.imgLight) + 50);
				if (num11 <= 0)
				{
					num11 = 1;
				}
				if (TileMap.tileID != 28)
				{
					for (int num12 = 0; num12 < num11; num12++)
					{
						int num13 = 100 + num12 * (mGraphics.getImageWidth(TileMap.imgLight) + 50) - GameScr.cmx / 2;
						int num14 = -20;
						int imageWidth = mGraphics.getImageWidth(TileMap.imgLight);
						if (num13 + imageWidth >= GameScr.cmx && num13 <= GameScr.cmx + GameCanvas.w && num14 + mGraphics.getImageHeight(TileMap.imgLight) >= GameScr.cmy)
						{
							int num15 = GameScr.cmy;
							int h = GameCanvas.h;
						}
					}
				}
			}
			mSystem.paintFlyText(g);
			GameCanvas.debug("PA14", 1);
			GameCanvas.debug("PA15", 1);
			GameCanvas.debug("PA16", 1);
			this.paintArrowPointToNPC(g);
			GameCanvas.debug("PA17", 1);
			if (!GameScr.isPaintOther && GameScr.isPaintRada == 1 && !GameCanvas.panel.isShow)
			{
				this.paintInfoBar(g);
			}
			GameScr.resetTranslate(g);
			if (!GameScr.isPaintOther)
			{
				MainMod.Paint(g);
				GameCanvas.debug("PA21", 1);
				GameCanvas.debug("PA18", 1);
				g.translate(-g.getTranslateX(), -g.getTranslateY());
				if ((TileMap.mapID == 128 || TileMap.mapID == 127) && GameScr.mabuPercent != 0)
				{
					int num16 = 30;
					int num17 = 200;
					g.setColor(0);
					g.fillRect(num16 - 27, num17 - 112, 54, 8);
					g.setColor(16711680);
					g.setClip(num16 - 25, num17 - 110, (int)GameScr.mabuPercent, 4);
					g.fillRect(num16 - 25, num17 - 110, 50, 4);
					g.setClip(0, 0, 3000, 3000);
					mFont.tahoma_7b_white.drawString(g, "Mabu", num16, num17 - 112 + 10, 2, mFont.tahoma_7b_dark);
				}
				if (global::Char.myCharz().isFusion)
				{
					global::Char.myCharz().tFusion++;
					int num18 = GameCanvas.gameTick % 3;
					if (global::Char.myCharz().tFusion >= 100)
					{
						global::Char.myCharz().fusionComplete();
					}
				}
				for (int num19 = 0; num19 < GameScr.vCharInMap.size(); num19++)
				{
					global::Char char6 = null;
					try
					{
						char6 = (global::Char)GameScr.vCharInMap.elementAt(num19);
					}
					catch (Exception)
					{
					}
					if (char6 != null && char6.isFusion && global::Char.isCharInScreen(char6))
					{
						char6.tFusion++;
						if (GameCanvas.gameTick % 3 == 0)
						{
							g.setColor(16777215);
							g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
						}
						if (char6.tFusion >= 100)
						{
							char6.fusionComplete();
						}
					}
				}
				GameCanvas.paintz.paintTabSoft(g);
				GameCanvas.debug("PA19", 1);
				GameCanvas.debug("PA20", 1);
				GameScr.resetTranslate(g);
				this.paintSelectedSkill(g);
				GameCanvas.debug("PA22", 1);
				GameScr.resetTranslate(g);
				if (GameCanvas.isTouch && GameCanvas.isTouchControl)
				{
					this.paintTouchControl(g);
				}
				GameScr.resetTranslate(g);
				this.paintChatVip(g);
				if (!GameCanvas.panel.isShow && GameCanvas.currentDialog == null && ChatPopup.currChatPopup == null && ChatPopup.serverChatPopUp == null && GameCanvas.currentScreen.Equals(GameScr.instance))
				{
					base.paint(g);
					if (mScreen.keyMouse == 1 && this.cmdMenu != null)
					{
						g.drawImage(ItemMap.imageFlare, this.cmdMenu.x + 7, this.cmdMenu.y + 15, 3);
					}
				}
				GameScr.resetTranslate(g);
				int num20 = 100 + ((global::Char.vItemTime.size() != 0) ? (GameScr.textTime.size() * 12) : 0);
				if (global::Char.myCharz().clan != null)
				{
					int num21 = 0;
					int num22 = 0;
					int num23 = (GameCanvas.h - 100 - 60) / 12;
					for (int num24 = 0; num24 < GameScr.vCharInMap.size(); num24++)
					{
						global::Char char7 = (global::Char)GameScr.vCharInMap.elementAt(num24);
						if (char7.clanID != -1 && char7.clanID == global::Char.myCharz().clan.ID)
						{
							if (char7.isOutX() && char7.cx < global::Char.myCharz().cx)
							{
								int num25 = num23;
								if (global::Char.vItemTime.size() != 0)
								{
									num25 -= GameScr.textTime.size();
								}
								if (num21 <= num25)
								{
									mFont.tahoma_7_green.drawString(g, char7.cName, 20, num20 - 12 + num21 * 12, mFont.LEFT, mFont.tahoma_7_grey);
									char7.paintHp(g, 10, num20 + num21 * 12 - 5);
									num21++;
								}
							}
							else if (char7.isOutX() && char7.cx > global::Char.myCharz().cx && num22 <= num23)
							{
								mFont.tahoma_7_green.drawString(g, char7.cName, GameCanvas.w - 25, num20 - 12 + num22 * 12, mFont.RIGHT, mFont.tahoma_7_grey);
								char7.paintHp(g, GameCanvas.w - 15, num20 + num22 * 12 - 5);
								num22++;
							}
						}
					}
				}
				ChatTextField.gI().paint(g);
				if (GameScr.isNewClanMessage && !GameCanvas.panel.isShow && GameCanvas.gameTick % 4 == 0)
				{
					g.drawImage(ItemMap.imageFlare, this.cmdMenu.x + 15, this.cmdMenu.y + 30, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				if (this.isSuperPower)
				{
					this.dxPower += 5;
					if (this.tPower >= 0)
					{
						this.tPower += this.dxPower;
					}
					Res.outz("x power= " + this.xPower.ToString());
					if (this.tPower < 0)
					{
						this.tPower--;
						if (this.tPower == -20)
						{
							this.isSuperPower = false;
							this.tPower = 0;
							this.dxPower = 0;
						}
					}
					else if ((this.xPower - this.tPower > 0 || this.tPower < TileMap.pxw) && this.tPower > 0)
					{
						g.setColor(16777215);
						if (!GameCanvas.lowGraphic)
						{
							g.fillArg(0, 0, GameCanvas.w, GameCanvas.h, 0, 0);
						}
						else
						{
							g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
						}
					}
					else
					{
						this.tPower = -1;
					}
				}
				for (int num26 = 0; num26 < global::Char.vItemTime.size(); num26++)
				{
					((ItemTime)global::Char.vItemTime.elementAt(num26)).paint(g, this.cmdMenu.x + 32 + num26 * 24, 55);
				}
				for (int num27 = 0; num27 < GameScr.textTime.size(); num27++)
				{
					ItemTime itemTime = (ItemTime)GameScr.textTime.elementAt(num27);
					if (itemTime.isPaint_coolDownBar)
					{
						itemTime.paintText(g, this.cmdMenu.x + ((global::Char.vItemTime.size() == 0) ? 25 : 5), ((global::Char.vItemTime.size() == 0) ? 45 : 90) + num27 * 12);
					}
				}
				this.paintXoSo(g);
				if (mResources.language == 1)
				{
					long second = mSystem.currentTimeMillis() + GameScr.deltaTime;
					mFont.tahoma_7b_white.drawString(g, NinjaUtil.getDate2(second), 10, GameCanvas.h - 65, 0, mFont.tahoma_7b_dark);
				}
				if (!this.yourNumber.Equals(string.Empty))
				{
					for (int num28 = 0; num28 < this.strPaint.Length; num28++)
					{
						mFont.tahoma_7b_white.drawString(g, this.strPaint[num28], 5, 85 + num28 * 18, 0, mFont.tahoma_7b_dark);
					}
				}
			}
			int num29 = 0;
			int num30 = GameCanvas.hw;
			if (num30 > 200)
			{
				num30 = 200;
			}
			this.paintPhuBanBar(g, num29 + GameCanvas.w / 2, 0, num30);
			EffectManager.hiEffects.paintAll(g);
		}
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x0006B5C4 File Offset: 0x000697C4
	private void paintXoSo(mGraphics g)
	{
		if (this.tShow != 0)
		{
			string text = string.Empty;
			for (int i = 0; i < this.winnumber.Length; i++)
			{
				text = text + this.randomNumber[i] + " ";
			}
			PopUp.paintPopUp(g, 20, 45, 95, 35, 16777215, false);
			mFont.tahoma_7b_dark.drawString(g, mResources.kquaVongQuay, 68, 50, 2);
			mFont.tahoma_7b_dark.drawString(g, text + string.Empty, 68, 65, 2);
		}
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x0006B658 File Offset: 0x00069858
	private void checkEffToObj(IMapObject obj, bool isnew)
	{
		if (obj != null)
		{
			if (this.tDoubleDelay > 0)
			{
				return;
			}
			this.tDoubleDelay = 10;
			int x = obj.getX();
			int num = Res.abs(global::Char.myCharz().cx - x);
			int loopCount;
			if (num <= 80)
			{
				loopCount = 1;
			}
			else if (num > 80 && num <= 200)
			{
				loopCount = 2;
			}
			else if (num > 200 && num <= 400)
			{
				loopCount = 3;
			}
			else
			{
				loopCount = 4;
			}
			if (!isnew)
			{
				if (obj.Equals(global::Char.myCharz().mobFocus) || (obj.Equals(global::Char.myCharz().charFocus) && global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus)))
				{
					ServerEffect.addServerEffect(135, obj.getX(), obj.getY(), loopCount);
				}
				else if (obj.Equals(global::Char.myCharz().npcFocus) || obj.Equals(global::Char.myCharz().itemFocus) || obj.Equals(global::Char.myCharz().charFocus))
				{
					ServerEffect.addServerEffect(136, obj.getX(), obj.getY(), loopCount);
				}
			}
			else
			{
				ServerEffect.addServerEffect(136, obj.getX(), obj.getY(), loopCount);
			}
		}
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x0006B7BC File Offset: 0x000699BC
	private void updateClickToArrow()
	{
		if (this.tDoubleDelay > 0)
		{
			this.tDoubleDelay--;
		}
		if (this.clickMoving)
		{
			this.clickMoving = false;
			IMapObject mapObject = this.findClickToItem(this.clickToX, this.clickToY);
			if (mapObject == null || (mapObject != null && mapObject.Equals(global::Char.myCharz().npcFocus) && TileMap.mapID == 51))
			{
				ServerEffect.addServerEffect(134, this.clickToX, this.clickToY + GameCanvas.transY / 2, 3);
			}
		}
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x0006B854 File Offset: 0x00069A54
	private void paintWaypointArrow(mGraphics g)
	{
		int num = 10;
		Task taskMaint = global::Char.myCharz().taskMaint;
		if (taskMaint != null && taskMaint.taskId == 0 && ((taskMaint.index != 1 && taskMaint.index < 6) || taskMaint.index == 0))
		{
			return;
		}
		for (int i = 0; i < TileMap.vGo.size(); i++)
		{
			Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
			if (waypoint.minY == 0 || (int)waypoint.maxY >= TileMap.pxh - 24)
			{
				if ((int)waypoint.maxY <= TileMap.pxh / 2)
				{
					int x = (int)(waypoint.minX + (waypoint.maxX - waypoint.minX) / 2);
					int y = (int)(waypoint.minY + (waypoint.maxY - waypoint.minY) / 2) + this.runArrow;
					if (GameCanvas.isTouch)
					{
						y = (int)(waypoint.maxY + (waypoint.maxY - waypoint.minY)) + this.runArrow + num;
					}
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 6, x, y, StaticObj.VCENTER_HCENTER);
				}
				else if ((int)waypoint.minY >= TileMap.pxh / 2)
				{
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 4, (int)(waypoint.minX + (waypoint.maxX - waypoint.minX) / 2), (int)(waypoint.minY - 12) - this.runArrow, StaticObj.VCENTER_HCENTER);
				}
			}
			else if (waypoint.minX >= 0 && waypoint.minX < 24)
			{
				if (!GameCanvas.isTouch)
				{
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 2, (int)(waypoint.maxX + 12) + this.runArrow, (int)(waypoint.maxY - 12), StaticObj.VCENTER_HCENTER);
				}
				else
				{
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 2, (int)(waypoint.maxX + 12) + this.runArrow, (int)(waypoint.maxY - 32), StaticObj.VCENTER_HCENTER);
				}
			}
			else if ((int)waypoint.minX <= TileMap.tmw * 24 && (int)waypoint.minX >= TileMap.tmw * 24 - 48)
			{
				if (!GameCanvas.isTouch)
				{
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 0, (int)(waypoint.minX - 12) - this.runArrow, (int)(waypoint.maxY - 12), StaticObj.VCENTER_HCENTER);
				}
				else
				{
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 0, (int)(waypoint.minX - 12) - this.runArrow, (int)(waypoint.maxY - 32), StaticObj.VCENTER_HCENTER);
				}
			}
			else
			{
				g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 4, (int)(waypoint.minX + (waypoint.maxX - waypoint.minX) / 2), (int)(waypoint.maxY - 48) - this.runArrow, StaticObj.VCENTER_HCENTER);
			}
		}
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x0006BB60 File Offset: 0x00069D60
	public static Npc findNPCInMap(short id)
	{
		for (int i = 0; i < GameScr.vNpc.size(); i++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(i);
			if (npc.template.npcTemplateId == (int)id)
			{
				return npc;
			}
		}
		return null;
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x0006BBB0 File Offset: 0x00069DB0
	public static global::Char findCharInMap(int charId)
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char.charID == charId)
			{
				return @char;
			}
		}
		return null;
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x0000788C File Offset: 0x00005A8C
	public static Mob findMobInMap(sbyte mobIndex)
	{
		return (Mob)GameScr.vMob.elementAt((int)mobIndex);
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x0006BBF8 File Offset: 0x00069DF8
	public static Mob findMobInMap(int mobId)
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob.mobId == mobId)
			{
				return mob;
			}
		}
		return null;
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x0006BC40 File Offset: 0x00069E40
	public static Npc getNpcTask()
	{
		for (int i = 0; i < GameScr.vNpc.size(); i++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(i);
			if (npc.template.npcTemplateId == (int)GameScr.getTaskNpcId())
			{
				return npc;
			}
		}
		return null;
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x0006BC94 File Offset: 0x00069E94
	private void paintArrowPointToNPC(mGraphics g)
	{
		try
		{
			if (ChatPopup.currChatPopup == null)
			{
				int num = (int)GameScr.getTaskNpcId();
				if (num != -1)
				{
					Npc npc = null;
					for (int i = 0; i < GameScr.vNpc.size(); i++)
					{
						Npc npc2 = (Npc)GameScr.vNpc.elementAt(i);
						if (npc2.template.npcTemplateId == num)
						{
							if (npc == null)
							{
								npc = npc2;
							}
							else if (Res.abs(npc2.cx - global::Char.myCharz().cx) < Res.abs(npc.cx - global::Char.myCharz().cx))
							{
								npc = npc2;
							}
						}
					}
					if (npc != null && npc.statusMe != 15)
					{
						if (npc.cx <= GameScr.cmx || npc.cx >= GameScr.cmx + GameScr.gW || npc.cy <= GameScr.cmy || npc.cy >= GameScr.cmy + GameScr.gH)
						{
							if (GameCanvas.gameTick % 10 >= 5)
							{
								int num2 = npc.cx - global::Char.myCharz().cx;
								int num3 = npc.cy - global::Char.myCharz().cy;
								int x = 0;
								int y = 0;
								int arg = 0;
								if (num2 > 0 && num3 >= 0)
								{
									if (Res.abs(num2) >= Res.abs(num3))
									{
										x = GameScr.gW - 10;
										y = GameScr.gH / 2 + 30;
										if (GameCanvas.isTouch)
										{
											y = GameScr.gH / 2 + 10;
										}
										arg = 0;
									}
									else
									{
										x = GameScr.gW / 2;
										y = GameScr.gH - 10;
										arg = 5;
									}
								}
								else if (num2 >= 0 && num3 < 0)
								{
									if (Res.abs(num2) >= Res.abs(num3))
									{
										x = GameScr.gW - 10;
										y = GameScr.gH / 2 + 30;
										if (GameCanvas.isTouch)
										{
											y = GameScr.gH / 2 + 10;
										}
										arg = 0;
									}
									else
									{
										x = GameScr.gW / 2;
										y = 10;
										arg = 6;
									}
								}
								if (num2 < 0 && num3 >= 0)
								{
									if (Res.abs(num2) >= Res.abs(num3))
									{
										x = 10;
										y = GameScr.gH / 2 + 30;
										if (GameCanvas.isTouch)
										{
											y = GameScr.gH / 2 + 10;
										}
										arg = 3;
									}
									else
									{
										x = GameScr.gW / 2;
										y = GameScr.gH - 10;
										arg = 5;
									}
								}
								else if (num2 <= 0 && num3 < 0)
								{
									if (Res.abs(num2) >= Res.abs(num3))
									{
										x = 10;
										y = GameScr.gH / 2 + 30;
										if (GameCanvas.isTouch)
										{
											y = GameScr.gH / 2 + 10;
										}
										arg = 3;
									}
									else
									{
										x = GameScr.gW / 2;
										y = 10;
										arg = 6;
									}
								}
								GameScr.resetTranslate(g);
								g.drawRegion(GameScr.arrow, 0, 0, 13, 16, arg, x, y, StaticObj.VCENTER_HCENTER);
							}
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham arrow to npc: " + ex.ToString());
		}
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x0000789F File Offset: 0x00005A9F
	public static void resetTranslate(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, -200, GameCanvas.w, 200 + GameCanvas.h);
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x0006BFF4 File Offset: 0x0006A1F4
	private void paintTouchControl(mGraphics g)
	{
		if (this.isNotPaintTouchControl())
		{
			return;
		}
		GameScr.resetTranslate(g);
		if (!TileMap.isOfflineMap() && !this.isVS())
		{
			if (mScreen.keyTouch == 15 || mScreen.keyMouse == 15)
			{
				g.drawImage((!Main.isPC) ? GameScr.imgChat2 : GameScr.imgChatsPC2, GameScr.xC + 17, GameScr.yC + 17 + mGraphics.addYWhenOpenKeyBoard, mGraphics.HCENTER | mGraphics.VCENTER);
			}
			else
			{
				g.drawImage((!Main.isPC) ? GameScr.imgChat : GameScr.imgChatPC, GameScr.xC + 17, GameScr.yC + 17 + mGraphics.addYWhenOpenKeyBoard, mGraphics.HCENTER | mGraphics.VCENTER);
			}
		}
		if (!GameScr.isUseTouch)
		{
			return;
		}
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x0006C0D0 File Offset: 0x0006A2D0
	public void paintImageBarRight(mGraphics g, global::Char c)
	{
		int num = (int)(c.cHP * GameScr.hpBarW / c.cHPFull);
		int num2 = (int)(c.cMP * (long)GameScr.mpBarW / c.cMPFull);
		int num3 = (int)(this.dHP * GameScr.hpBarW / c.cHPFull);
		int num4 = (int)(this.dMP * (long)GameScr.mpBarW / c.cMPFull);
		g.setClip(GameCanvas.w / 2 + 58 - mGraphics.getImageWidth(GameScr.imgPanel), 0, 95, 100);
		g.drawRegion(GameScr.imgPanel, 0, 0, mGraphics.getImageWidth(GameScr.imgPanel), mGraphics.getImageHeight(GameScr.imgPanel), 2, GameCanvas.w / 2 + 60, 0, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip((int)((long)(GameCanvas.w / 2 + 60 - 83) - GameScr.hpBarW + GameScr.hpBarW - (long)num3), 5, num3, 10);
		g.drawImage(GameScr.imgHPLost, GameCanvas.w / 2 + 60 - 83, 5, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.setClip((int)((long)(GameCanvas.w / 2 + 60 - 83) - GameScr.hpBarW + GameScr.hpBarW - (long)num), 5, num, 10);
		g.drawImage(GameScr.imgHP, GameCanvas.w / 2 + 60 - 83, 5, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.setClip((int)((long)(GameCanvas.w / 2 + 60 - 83 - GameScr.mpBarW) + GameScr.hpBarW - (long)num4), 20, num4, 6);
		g.drawImage(GameScr.imgMPLost, GameCanvas.w / 2 + 60 - 83, 20, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.setClip((int)((long)(GameCanvas.w / 2 + 60 - 83 - GameScr.mpBarW) + GameScr.hpBarW - (long)num2), 20, num2, 6);
		g.drawImage(GameScr.imgMP, GameCanvas.w / 2 + 60 - 83, 20, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		int xHP = GameCanvas.w / 2 + 60 - 83 - (int)(GameScr.hpBarW / 2L);
		int xMP = GameCanvas.w / 2 + 60 - 83 - GameScr.mpBarW / 2;
		mFont.paintDamageWhite(g, NinjaUtil.getMoneys(c.cHP), xHP, 4, mFont.CENTER, 13);
		mFont.paintDamageYellow(g, NinjaUtil.getMoneys(c.cMP), xMP, 17, mFont.CENTER, 13);
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x0006C308 File Offset: 0x0006A508
	private void paintImageBar(mGraphics g, bool isLeft, global::Char c)
	{
		if (c == null)
		{
			return;
		}
		int num;
		int num2;
		int num3;
		int num4;
		if (c.charID == global::Char.myCharz().charID)
		{
			num = (int)(this.dHP * GameScr.hpBarW / c.cHPFull);
			num2 = (int)(this.dMP * (long)GameScr.mpBarW / c.cMPFull);
			num3 = (int)(c.cHP * GameScr.hpBarW / c.cHPFull);
			num4 = (int)(c.cMP * (long)GameScr.mpBarW / c.cMPFull);
		}
		else
		{
			num = (int)(c.dHP * GameScr.hpBarW / c.cHPFull);
			num2 = c.perCentMp * GameScr.mpBarW / 100;
			num3 = (int)(c.cHP * GameScr.hpBarW / c.cHPFull);
			num4 = c.perCentMp * GameScr.mpBarW / 100;
		}
		if (global::Char.myCharz().secondPower > 0)
		{
			int w = (int)global::Char.myCharz().powerPoint * GameScr.spBarW / (int)global::Char.myCharz().maxPowerPoint;
			g.drawImage(GameScr.imgPanel2, 58, 29, 0);
			g.setClip(83, 31, w, 10);
			g.drawImage(GameScr.imgSP, 83, 31, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			mFont.tahoma_7_white.drawString(g, string.Concat(new object[]
			{
				global::Char.myCharz().strInfo,
				":",
				global::Char.myCharz().powerPoint,
				"/",
				global::Char.myCharz().maxPowerPoint
			}), 115, 29, 2);
		}
		if (c.charID != global::Char.myCharz().charID)
		{
			g.setClip(mGraphics.getImageWidth(GameScr.imgPanel) - 95, 0, 95, 100);
		}
		g.drawImage(GameScr.imgPanel, 0, 0, 0);
		if (isLeft)
		{
			g.setClip(83, 5, num, 10);
		}
		else
		{
			g.setClip((int)(83L + GameScr.hpBarW - (long)num), 5, num, 10);
		}
		g.drawImage(GameScr.imgHPLost, 83, 5, 0);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		if (isLeft)
		{
			g.setClip(83, 5, num3, 10);
		}
		else
		{
			g.setClip((int)(83L + GameScr.hpBarW - (long)num3), 5, num3, 10);
		}
		g.drawImage(GameScr.imgHP, 83, 5, 0);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		if (isLeft)
		{
			g.setClip(83, 20, num2, 6);
		}
		else
		{
			g.setClip(83 + GameScr.mpBarW - num2, 20, num2, 6);
		}
		g.drawImage(GameScr.imgMPLost, 83, 20, 0);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		if (isLeft)
		{
			g.setClip(83, 20, num2, 6);
		}
		else
		{
			g.setClip(83 + GameScr.mpBarW - num4, 20, num4, 6);
		}
		g.drawImage(GameScr.imgMP, 83, 20, 0);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		if (global::Char.myCharz().cMP == 0L && GameCanvas.gameTick % 10 > 5)
		{
			g.setClip(83, 20, 2, 6);
			g.drawImage(GameScr.imgMPLost, 83, 20, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		}
		int xHP = 83 + (int)(GameScr.hpBarW / 2L);
		int xMP = 83 + GameScr.mpBarW / 2;
		mFont.paintDamageWhite(g, NinjaUtil.getMoneys(c.cHP), xHP, 4, mFont.CENTER, 13);
		mFont.paintDamageYellow(g, NinjaUtil.getMoneys(c.cMP), xMP, 17, mFont.CENTER, 13);
		
		if (c.charID == global::Char.myCharz().charID)
		{
			int fps = (int)System.Math.Round((double)(1f / UnityEngine.Time.smoothDeltaTime * UnityEngine.Time.timeScale), 0);
			int ping = Mod.DungPham.KoiOctiiu957.MainMod.ping;
			int yPos = (global::Char.myCharz().secondPower > 0) ? 52 : 38;
			mFont.tahoma_7_red.drawString(g, "FPS: " + fps + " - " + ping + " ms", 80, yPos, 0);
		}
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x000045ED File Offset: 0x000027ED
	public void getInjure()
	{
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x0006C668 File Offset: 0x0006A868
	public void starVS()
	{
		this.curr = (this.last = mSystem.currentTimeMillis());
		this.secondVS = 180;
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x0006C694 File Offset: 0x0006A894
	private global::Char findCharVS1()
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if ((int)@char.cTypePk != 0)
			{
				return @char;
			}
		}
		return null;
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x0006C6DC File Offset: 0x0006A8DC
	private global::Char findCharVS2()
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if ((int)@char.cTypePk != 0 && @char != this.findCharVS1())
			{
				return @char;
			}
		}
		return null;
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x0006C730 File Offset: 0x0006A930
	private void paintInfoBar(mGraphics g)
	{
		GameScr.resetTranslate(g);
		if (TileMap.mapID == 130 && this.findCharVS1() != null && this.findCharVS2() != null)
		{
			g.translate(GameCanvas.w / 2 - 62, 0);
			this.paintImageBar(g, true, this.findCharVS1());
			g.translate(-(GameCanvas.w / 2 - 65), 0);
			this.paintImageBarRight(g, this.findCharVS2());
			this.findCharVS1().paintHeadWithXY(g, 137, 25, 0);
			this.findCharVS2().paintHeadWithXY(g, GameCanvas.w - 15 - 122, 25, 2);
		}
		else if (this.isVS() && global::Char.myCharz().charFocus != null)
		{
			g.translate(GameCanvas.w / 2 - 62, 0);
			this.paintImageBar(g, true, global::Char.myCharz().charFocus);
			g.translate(-(GameCanvas.w / 2 - 65), 0);
			this.paintImageBarRight(g, global::Char.myCharz());
			global::Char.myCharz().paintHeadWithXY(g, 137, 25, 0);
			global::Char.myCharz().charFocus.paintHeadWithXY(g, GameCanvas.w - 15 - 122, 25, 2);
		}
		else if (GameScr.ispaintPhubangBar() && GameScr.isSmallScr())
		{
			GameScr.paintHPBar_NEW(g, 1, 1, global::Char.myCharz());
		}
		else
		{
			this.paintImageBar(g, true, global::Char.myCharz());
			if (global::Char.myCharz().isInEnterOfflinePoint() != null || global::Char.myCharz().isInEnterOnlinePoint() != null)
			{
				mFont.tahoma_7_green2.drawString(g, mResources.enter, this.imgScrW / 2, 8 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
			}
			else if (global::Char.myCharz().mobFocus != null)
			{
				if (global::Char.myCharz().mobFocus.getTemplate() != null)
				{
					mFont.tahoma_7b_green2.drawString(g, global::Char.myCharz().mobFocus.getTemplate().name, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				}
				if (global::Char.myCharz().mobFocus.templateId != 0)
				{
					mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys(global::Char.myCharz().mobFocus.hp) + string.Empty, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				}
			}
			else if (global::Char.myCharz().npcFocus != null)
			{
				mFont.tahoma_7b_green2.drawString(g, global::Char.myCharz().npcFocus.template.name, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				if (global::Char.myCharz().npcFocus.template.npcTemplateId == 4)
				{
					mFont.tahoma_7b_green2.drawString(g, GameScr.gI().magicTree.currPeas + "/" + GameScr.gI().magicTree.maxPeas, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				}
			}
			else if (global::Char.myCharz().charFocus != null)
			{
				mFont.tahoma_7b_green2.drawString(g, global::Char.myCharz().charFocus.cName, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys(global::Char.myCharz().charFocus.cHP) + string.Empty, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
			}
			else
			{
				mFont.tahoma_7b_green2.drawString(g, global::Char.myCharz().cName, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys(global::Char.myCharz().cPower) + string.Empty, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
			}
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		if (this.isVS() && this.secondVS > 0)
		{
			this.curr = mSystem.currentTimeMillis();
			if (this.curr - this.last >= 1000L)
			{
				this.last = mSystem.currentTimeMillis();
				this.secondVS--;
			}
			mFont.tahoma_7b_white.drawString(g, this.secondVS + string.Empty, GameCanvas.w / 2, 13, 2, mFont.tahoma_7b_dark);
		}
		if (this.flareFindFocus)
		{
			g.drawImage(ItemMap.imageFlare, 40, 35, mGraphics.BOTTOM | mGraphics.HCENTER);
			this.flareTime--;
			if (this.flareTime < 0)
			{
				this.flareTime = 0;
				this.flareFindFocus = false;
			}
		}
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x0006CC10 File Offset: 0x0006AE10
	public bool isVS()
	{
		return TileMap.isVoDaiMap() && ((int)global::Char.myCharz().cTypePk != 0 || (TileMap.mapID == 130 && this.findCharVS1() != null && this.findCharVS2() != null));
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x0006CC60 File Offset: 0x0006AE60
	private void paintSelectedSkill(mGraphics g)
	{
		if (this.mobCapcha != null)
		{
			this.paintCapcha(g);
			return;
		}
		if (GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || this.isPaintPopup() || GameCanvas.panel.isShow || global::Char.myCharz().taskMaint.taskId == 0 || ChatTextField.gI().isShow || GameCanvas.currentScreen == MoneyCharge.instance)
		{
			return;
		}
		long num = mSystem.currentTimeMillis() - this.lastUsePotion;
		int num2 = 0;
		if (num < 10000L)
		{
			num2 = (int)(num * 20L / 10000L);
		}
		if (!GameCanvas.isTouch)
		{
			g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgSkill : GameScr.imgSkill2, GameScr.xSkill + GameScr.xHP - 1, GameScr.yHP - 1, 0);
			SmallImage.drawSmallImage(g, 542, GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3, 0, 0);
			mFont.number_gray.drawString(g, string.Empty + GameScr.hpPotion, GameScr.xSkill + GameScr.xHP + 22, GameScr.yHP + 15, 1);
			if (num < 10000L)
			{
				g.setColor(2721889);
				num2 = (int)(num * 20L / 10000L);
				g.fillRect(GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3 + num2, 20, 20 - num2);
			}
		}
		else if (global::Char.myCharz().statusMe != 14)
		{
			if (GameScr.gamePad.isSmallGamePad)
			{
				if (GameScr.isAnalog != 1)
				{
					g.setColor(9670800);
					g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10 + 10, 22, 20);
					g.setColor(16777215);
					g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10 + ((num2 != 0) ? (20 - num2) : 0) + 10, 22, (num2 == 0) ? 20 : num2);
					g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgHP1 : GameScr.imgHP2, GameScr.xHP, GameScr.yHP + 10, 0);
					mFont.tahoma_7_red.drawString(g, string.Empty + GameScr.hpPotion, GameScr.xHP + 20, GameScr.yHP + 15 + 10, 2);
					if (GameScr.isPickNgocRong)
					{
						g.drawImage((mScreen.keyTouch != 14) ? GameScr.imgNR1 : GameScr.imgNR2, GameScr.xHP + 5, GameScr.yHP - 6 - 40 + 10, 0);
					}
				}
				else if (GameScr.isAnalog == 1)
				{
					int num3 = 10;
					g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgSkill : GameScr.imgSkill2, GameScr.xSkill + GameScr.xHP - 1, GameScr.yHP - 1 + num3, 0);
					SmallImage.drawSmallImage(g, 542, GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3 + num3, 0, 0);
					mFont.number_gray.drawString(g, string.Empty + GameScr.hpPotion, GameScr.xSkill + GameScr.xHP + 22, GameScr.yHP + 13 + num3, 1);
					if (num < 10000L)
					{
						g.setColor(2721889);
						num2 = (int)(num * 20L / 10000L);
						g.fillRect(GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3 + num2 + num3, 20, 20 - num2);
					}
					if (GameScr.isPickNgocRong)
					{
						g.drawImage((mScreen.keyTouch != 14) ? GameScr.imgNR3 : GameScr.imgNR4, GameScr.xHP + 20 + 5, GameScr.yHP + 20 - 6 - 40 + 10, mGraphics.HCENTER | mGraphics.VCENTER);
					}
				}
			}
			else if (GameScr.isAnalog != 1)
			{
				g.setColor(9670800);
				g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10 - 6, 22, 20);
				g.setColor(16777215);
				g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10 + ((num2 != 0) ? (20 - num2) : 0) - 6, 22, (num2 == 0) ? 20 : num2);
				g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgHP1 : GameScr.imgHP2, GameScr.xHP, GameScr.yHP - 6, 0);
				mFont.tahoma_7_red.drawString(g, string.Empty + GameScr.hpPotion, GameScr.xHP + 20, GameScr.yHP + 15 - 6, 2);
				if (GameScr.isPickNgocRong)
				{
					g.drawImage((mScreen.keyTouch != 14) ? GameScr.imgNR1 : GameScr.imgNR2, GameScr.xHP, GameScr.yHP - 6 - 40, 0);
				}
			}
			else
			{
				g.setColor(9670800);
				g.fillRect(GameScr.xHP + 10, GameScr.yHP + 10 - 6 + 10, 20, 18);
				g.setColor(16777215);
				g.fillRect(GameScr.xHP + 10, GameScr.yHP + 10 + ((num2 != 0) ? (20 - num2) : 0) - 6 + 10, 20, (num2 == 0) ? 18 : num2);
				g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgHP3 : GameScr.imgHP4, GameScr.xHP + 20, GameScr.yHP + 20 - 6 + 10, mGraphics.HCENTER | mGraphics.VCENTER);
				mFont.tahoma_7_red.drawString(g, string.Empty + GameScr.hpPotion, GameScr.xHP + 20, GameScr.yHP + 15 - 6 + 10, 2);
				if (GameScr.isPickNgocRong)
				{
					g.drawImage((mScreen.keyTouch != 14) ? GameScr.imgNR3 : GameScr.imgNR4, GameScr.xHP + 20 + 5, GameScr.yHP + 20 - 6 - 40 + 10, mGraphics.HCENTER | mGraphics.VCENTER);
				}
			}
		}
		if (GameScr.isHaveSelectSkill)
		{
			Skill[] array = Main.isPC ? GameScr.keySkill : ((!GameCanvas.isTouch) ? GameScr.keySkill : GameScr.onScreenSkill);
			int keyTouch = mScreen.keyTouch;
			if (!GameCanvas.isTouch)
			{
				g.setColor(11152401);
				g.fillRect(GameScr.xSkill + GameScr.xHP + 2, GameScr.yHP - 10 + 6, 20, 10);
				mFont.tahoma_7_white.drawString(g, "*", GameScr.xSkill + GameScr.xHP + 12, GameScr.yHP - 8 + 6, mFont.CENTER);
			}
			int num4 = Main.isPC ? array.Length : ((!GameCanvas.isTouch) ? array.Length : this.nSkill);
			for (int i = 0; i < num4; i++)
			{
				if (Main.isPC)
				{
					string[] array3;
					if (TField.isQwerty)
					{
						string[] array2 = new string[10];
						array2[0] = "1";
						array2[1] = "2";
						array2[2] = "3";
						array2[3] = "4";
						array2[4] = "5";
						array2[5] = "6";
						array2[6] = "7";
						array2[7] = "8";
						array2[8] = "9";
						array3 = array2;
						array2[9] = "0";
					}
					else
					{
						string[] array4 = new string[5];
						array4[0] = "7";
						array4[1] = "8";
						array4[2] = "9";
						array4[3] = "10";
						array3 = array4;
						array4[4] = "11";
					}
					string[] array5 = array3;
					int num5 = -13;
					if (mGraphics.zoomLevel == 1 && num4 > 5 && i < 5)
					{
						num5 = 27;
					}
					mFont.tahoma_7b_dark.drawString(g, array5[i], GameScr.xSkill + GameScr.xS[i] + 14, GameScr.yS[i] + num5, mFont.CENTER);
					mFont.tahoma_7b_white.drawString(g, array5[i], GameScr.xSkill + GameScr.xS[i] + 14, GameScr.yS[i] + num5 + 1, mFont.CENTER);
				}
				else if (!GameCanvas.isTouch)
				{
					string[] array7;
					if (TField.isQwerty)
					{
						string[] array6 = new string[5];
						array6[0] = "Q";
						array6[1] = "W";
						array6[2] = "E";
						array6[3] = "R";
						array7 = array6;
						array6[4] = "T";
					}
					else
					{
						string[] array8 = new string[5];
						array8[0] = "7";
						array8[1] = "8";
						array8[2] = "9";
						array8[3] = "1";
						array7 = array8;
						array8[4] = "3";
					}
					string[] array9 = array7;
					g.setColor(11152401);
					g.fillRect(GameScr.xSkill + GameScr.xS[i] + 2, GameScr.yS[i] - 10 + 8, 20, 10);
					mFont.tahoma_7_white.drawString(g, array9[i], GameScr.xSkill + GameScr.xS[i] + 12, GameScr.yS[i] - 10 + 6, mFont.CENTER);
				}
				Skill skill = array[i];
				if (skill != global::Char.myCharz().myskill)
				{
					g.drawImage(GameScr.imgSkill, GameScr.xSkill + GameScr.xS[i] - 1, GameScr.yS[i] - 1, 0);
				}
				if (skill != null)
				{
					if (skill == global::Char.myCharz().myskill)
					{
						g.drawImage(GameScr.imgSkill2, GameScr.xSkill + GameScr.xS[i] - 1, GameScr.yS[i] - 1, 0);
						if (GameCanvas.isTouch && !Main.isPC)
						{
							g.drawRegion(Mob.imgHP, 0, 12, 9, 6, 0, GameScr.xSkill + GameScr.xS[i] + 8, GameScr.yS[i] - 7, 0);
						}
					}
					skill.paint(GameScr.xSkill + GameScr.xS[i] + 13, GameScr.yS[i] + 13, g);
					if ((i == this.selectedIndexSkill && !this.isPaintUI() && GameCanvas.gameTick % 10 > 5) || i == this.keyTouchSkill)
					{
						g.drawImage(ItemMap.imageFlare, GameScr.xSkill + GameScr.xS[i] + 13, GameScr.yS[i] + 14, 3);
					}
					long num6 = (long)skill.coolDown - mSystem.currentTimeMillis() + skill.lastTimeUseThisSkill;
					string st = "";
					if (num6 > 0L)
					{
						if (num6 >= 1000L)
						{
							st = (num6 / 1000L).ToString();
						}
						else
						{
							int num7 = (int)(num6 / 100L);
							st = "0." + num7;
						}
					}
					mFont.tahoma_7b_white.drawString(g, st, GameScr.xSkill + GameScr.xS[i] + 14, GameScr.yS[i] + 8, mFont.CENTER, mFont.tahoma_7_red);
					if (AutoSkill.isAutoUseSkills != null && i < AutoSkill.isAutoUseSkills.Length && AutoSkill.isAutoUseSkills[i])
					{
						g.setColor(0);
						g.drawRect(GameScr.xSkill + GameScr.xS[i] + 2, GameScr.yS[i] + 2, 21, 21);
					}
				}
			}
		}
		this.paintGamePad(g);
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x0006D718 File Offset: 0x0006B918
	public void paintOpen(mGraphics g)
	{
		if (this.isstarOpen)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.fillRect(0, 0, GameCanvas.w, this.moveUp);
			g.setColor(10275899);
			g.fillRect(0, this.moveUp - 1, GameCanvas.w, 1);
			g.fillRect(0, this.moveDow + 1, GameCanvas.w, 1);
		}
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x0006D78C File Offset: 0x0006B98C
	public static void startFlyText(string flyString, int x, int y, int dx, int dy, int color)
	{
		if (!string.IsNullOrEmpty(flyString))
		{
			try
			{
				flyString = System.Text.RegularExpressions.Regex.Replace(flyString, @"\d+", delegate(System.Text.RegularExpressions.Match match)
				{
					long val;
					if (long.TryParse(match.Value, out val))
					{
						return NinjaUtil.getMoneys(val);
					}
					return match.Value;
				});
			}
			catch
			{
			}
		}
		int num = -1;
		for (int i = 0; i < 5; i++)
		{
			if (GameScr.flyTextState[i] == -1)
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			return;
		}
		GameScr.flyTextColor[num] = color;
		GameScr.flyTextString[num] = flyString;
		GameScr.flyTextX[num] = x;
		GameScr.flyTextY[num] = y;
		GameScr.flyTextDx[num] = dx;
		GameScr.flyTextDy[num] = ((dy >= 0) ? 5 : -5);
		GameScr.flyTextState[num] = 0;
		GameScr.flyTime[num] = 0;
		GameScr.flyTextYTo[num] = 10;
		for (int j = 0; j < 5; j++)
		{
			if (GameScr.flyTextState[j] != -1 && num != j && GameScr.flyTextDy[num] < 0 && Res.abs(GameScr.flyTextX[num] - GameScr.flyTextX[j]) <= 20 && GameScr.flyTextYTo[num] == GameScr.flyTextYTo[j])
			{
				GameScr.flyTextYTo[num] += 10;
			}
		}
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x0006D894 File Offset: 0x0006BA94
	public static void updateFlyText()
	{
		for (int i = 0; i < 5; i++)
		{
			if (GameScr.flyTextState[i] != -1)
			{
				if (GameScr.flyTextState[i] > GameScr.flyTextYTo[i])
				{
					GameScr.flyTime[i]++;
					if (GameScr.flyTime[i] == 25)
					{
						GameScr.flyTime[i] = 0;
						GameScr.flyTextState[i] = -1;
						GameScr.flyTextYTo[i] = 0;
						GameScr.flyTextDx[i] = 0;
						GameScr.flyTextX[i] = 0;
					}
				}
				else
				{
					GameScr.flyTextState[i] += Res.abs(GameScr.flyTextDy[i]);
					GameScr.flyTextX[i] += GameScr.flyTextDx[i];
					GameScr.flyTextY[i] += GameScr.flyTextDy[i];
				}
			}
		}
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x0006D968 File Offset: 0x0006BB68
	public static void loadSplash()
	{
		if (GameScr.imgSplash == null)
		{
			GameScr.imgSplash = new Image[3];
			for (int i = 0; i < 3; i++)
			{
				GameScr.imgSplash[i] = GameCanvas.loadImage("/e/sp" + i + ".png");
			}
		}
		GameScr.splashX = new int[2];
		GameScr.splashY = new int[2];
		GameScr.splashState = new int[2];
		GameScr.splashF = new int[2];
		GameScr.splashDir = new int[2];
		GameScr.splashState[0] = (GameScr.splashState[1] = -1);
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x0006DA08 File Offset: 0x0006BC08
	public static bool startSplash(int x, int y, int dir)
	{
		int num = (GameScr.splashState[0] != -1) ? 1 : 0;
		if (GameScr.splashState[num] != -1)
		{
			return false;
		}
		GameScr.splashState[num] = 0;
		GameScr.splashDir[num] = dir;
		GameScr.splashX[num] = x;
		GameScr.splashY[num] = y;
		return true;
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x0006DA5C File Offset: 0x0006BC5C
	public static void updateSplash()
	{
		for (int i = 0; i < 2; i++)
		{
			if (GameScr.splashState[i] != -1)
			{
				GameScr.splashState[i]++;
				GameScr.splashX[i] += GameScr.splashDir[i] << 2;
				GameScr.splashY[i]--;
				if (GameScr.splashState[i] >= 6)
				{
					GameScr.splashState[i] = -1;
				}
				else
				{
					GameScr.splashF[i] = (GameScr.splashState[i] >> 1) % 3;
				}
			}
		}
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x0006DAEC File Offset: 0x0006BCEC
	public static void paintSplash(mGraphics g)
	{
		for (int i = 0; i < 2; i++)
		{
			if (GameScr.splashState[i] != -1)
			{
				if (GameScr.splashDir[i] == 1)
				{
					g.drawImage(GameScr.imgSplash[GameScr.splashF[i]], GameScr.splashX[i], GameScr.splashY[i], 3);
				}
				else
				{
					g.drawRegion(GameScr.imgSplash[GameScr.splashF[i]], 0, 0, mGraphics.getImageWidth(GameScr.imgSplash[GameScr.splashF[i]]), mGraphics.getImageHeight(GameScr.imgSplash[GameScr.splashF[i]]), 2, GameScr.splashX[i], GameScr.splashY[i], 3);
				}
			}
		}
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x000078D1 File Offset: 0x00005AD1
	private void loadInforBar()
	{
		this.imgScrW = 84;
		GameScr.hpBarW = 66L;
		GameScr.mpBarW = 59;
		GameScr.hpBarX = 52;
		GameScr.hpBarY = 10;
		GameScr.spBarW = 61;
		GameScr.expBarW = GameScr.gW - 61;
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x0006DB98 File Offset: 0x0006BD98
	public void updateSS()
	{
		if (GameScr.indexMenu == -1)
		{
			return;
		}
		if (GameScr.cmySK != GameScr.cmtoYSK)
		{
			GameScr.cmvySK = GameScr.cmtoYSK - GameScr.cmySK << 2;
			GameScr.cmdySK += GameScr.cmvySK;
			GameScr.cmySK += GameScr.cmdySK >> 4;
			GameScr.cmdySK &= 15;
		}
		if (global::Math.abs(GameScr.cmtoYSK - GameScr.cmySK) < 15 && GameScr.cmySK < 0)
		{
			GameScr.cmtoYSK = 0;
		}
		if (global::Math.abs(GameScr.cmtoYSK - GameScr.cmySK) < 15 && GameScr.cmySK > GameScr.cmyLimSK)
		{
			GameScr.cmtoYSK = GameScr.cmyLimSK;
		}
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x0006DC5C File Offset: 0x0006BE5C
	public void updateKeyAlert()
	{
		if (!GameScr.isPaintAlert || GameCanvas.currentDialog != null)
		{
			return;
		}
		bool flag = false;
		if (GameCanvas.keyPressed[Key.NUM8])
		{
			GameScr.indexRow++;
			if (GameScr.indexRow >= this.texts.size())
			{
				GameScr.indexRow = 0;
			}
			flag = true;
		}
		else if (GameCanvas.keyPressed[Key.NUM2])
		{
			GameScr.indexRow--;
			if (GameScr.indexRow < 0)
			{
				GameScr.indexRow = this.texts.size() - 1;
			}
			flag = true;
		}
		if (flag)
		{
			GameScr.scrMain.moveTo(GameScr.indexRow * GameScr.scrMain.ITEM_SIZE);
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
		}
		if (GameCanvas.isTouch)
		{
			ScrollResult scrollResult = GameScr.scrMain.updateKey();
			if (scrollResult.isDowning || scrollResult.isFinish)
			{
				GameScr.indexRow = scrollResult.selected;
				flag = true;
			}
		}
		if (flag && GameScr.indexRow >= 0 && GameScr.indexRow < this.texts.size())
		{
			string text = (string)this.texts.elementAt(GameScr.indexRow);
			this.fnick = null;
			this.alertURL = null;
			this.center = null;
			ChatTextField.gI().center = null;
			int num;
			if ((num = text.IndexOf("http://")) >= 0)
			{
				Cout.println("currentLine: " + text);
				this.alertURL = text.Substring(num);
				this.center = new Command(mResources.open_link, 12000);
				if (!GameCanvas.isTouch)
				{
					ChatTextField.gI().center = new Command(mResources.open_link, null, 12000, null);
				}
			}
			else if (text.IndexOf("@") >= 0)
			{
				string text2 = text.Substring(2);
				text2 = text2.Trim();
				num = text2.IndexOf("@");
				string text3 = text2.Substring(num);
				int num2 = text3.IndexOf(" ");
				if (num2 <= 0)
				{
					num2 = num + text3.Length;
				}
				else
				{
					num2 += num;
				}
				this.fnick = text2.Substring(num + 1, num2);
				if (!this.fnick.Equals(string.Empty) && !this.fnick.Equals(global::Char.myCharz().cName))
				{
					this.center = new Command(mResources.SELECT, 12009, this.fnick);
					if (!GameCanvas.isTouch)
					{
						ChatTextField.gI().center = new Command(mResources.SELECT, null, 12009, this.fnick);
					}
				}
				else
				{
					this.fnick = null;
					this.center = null;
				}
			}
		}
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x0006DF2C File Offset: 0x0006C12C
	public bool isPaintPopup()
	{
		return GameScr.isPaintItemInfo || GameScr.isPaintInfoMe || GameScr.isPaintStore || GameScr.isPaintWeapon || GameScr.isPaintNonNam || GameScr.isPaintNonNu || GameScr.isPaintAoNam || GameScr.isPaintAoNu || GameScr.isPaintGangTayNam || GameScr.isPaintGangTayNu || GameScr.isPaintQuanNam || GameScr.isPaintQuanNu || GameScr.isPaintGiayNam || GameScr.isPaintGiayNu || GameScr.isPaintLien || GameScr.isPaintNhan || GameScr.isPaintNgocBoi || GameScr.isPaintPhu || GameScr.isPaintStack || GameScr.isPaintStackLock || GameScr.isPaintGrocery || GameScr.isPaintGroceryLock || GameScr.isPaintUpGrade || GameScr.isPaintConvert || GameScr.isPaintSplit || GameScr.isPaintUpPearl || GameScr.isPaintBox || GameScr.isPaintTrade || GameScr.isPaintAlert || GameScr.isPaintZone || GameScr.isPaintTeam || GameScr.isPaintClan || GameScr.isPaintFindTeam || GameScr.isPaintTask || GameScr.isPaintFriend || GameScr.isPaintEnemies || GameScr.isPaintCharInMap || GameScr.isPaintMessage;
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x0006E0B8 File Offset: 0x0006C2B8
	public bool isNotPaintTouchControl()
	{
		return (!GameCanvas.isTouchControl && GameCanvas.currentScreen == GameScr.gI()) || !GameCanvas.isTouch || ChatTextField.gI().isShow || InfoDlg.isShow || (GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || GameCanvas.panel.isShow || this.isPaintPopup());
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x0006E14C File Offset: 0x0006C34C
	public bool isPaintUI()
	{
		return GameScr.isPaintStore || GameScr.isPaintWeapon || GameScr.isPaintNonNam || GameScr.isPaintNonNu || GameScr.isPaintAoNam || GameScr.isPaintAoNu || GameScr.isPaintGangTayNam || GameScr.isPaintGangTayNu || GameScr.isPaintQuanNam || GameScr.isPaintQuanNu || GameScr.isPaintGiayNam || GameScr.isPaintGiayNu || GameScr.isPaintLien || GameScr.isPaintNhan || GameScr.isPaintNgocBoi || GameScr.isPaintPhu || GameScr.isPaintStack || GameScr.isPaintStackLock || GameScr.isPaintGrocery || GameScr.isPaintGroceryLock || GameScr.isPaintUpGrade || GameScr.isPaintConvert || GameScr.isPaintSplit || GameScr.isPaintUpPearl || GameScr.isPaintBox || GameScr.isPaintTrade;
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x0006E260 File Offset: 0x0006C460
	public bool isOpenUI()
	{
		return GameScr.isPaintItemInfo || GameScr.isPaintInfoMe || GameScr.isPaintStore || GameScr.isPaintNonNam || GameScr.isPaintNonNu || GameScr.isPaintAoNam || GameScr.isPaintAoNu || GameScr.isPaintGangTayNam || GameScr.isPaintGangTayNu || GameScr.isPaintQuanNam || GameScr.isPaintQuanNu || GameScr.isPaintGiayNam || GameScr.isPaintGiayNu || GameScr.isPaintLien || GameScr.isPaintNhan || GameScr.isPaintNgocBoi || GameScr.isPaintPhu || GameScr.isPaintWeapon || GameScr.isPaintStack || GameScr.isPaintStackLock || GameScr.isPaintGrocery || GameScr.isPaintGroceryLock || GameScr.isPaintUpGrade || GameScr.isPaintConvert || GameScr.isPaintUpPearl || GameScr.isPaintBox || GameScr.isPaintSplit || GameScr.isPaintTrade;
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x0006E388 File Offset: 0x0006C588
	public static void setPopupSize(int w, int h)
	{
		if (GameCanvas.w == 128 || GameCanvas.h <= 208)
		{
			w = 126;
			h = 160;
		}
		GameScr.indexTitle = 0;
		GameScr.popupW = w;
		GameScr.popupH = h;
		GameScr.popupX = GameScr.gW2 - w / 2;
		GameScr.popupY = GameScr.gH2 - h / 2;
		if (GameCanvas.isTouch && !GameScr.isPaintZone && !GameScr.isPaintTeam && !GameScr.isPaintClan && !GameScr.isPaintCharInMap && !GameScr.isPaintFindTeam && !GameScr.isPaintFriend && !GameScr.isPaintEnemies && !GameScr.isPaintTask && !GameScr.isPaintMessage)
		{
			if (GameCanvas.h <= 240)
			{
				GameScr.popupY -= 10;
			}
			if (GameCanvas.isTouch && !GameCanvas.isTouchControlSmallScreen && GameCanvas.currentScreen is GameScr)
			{
				GameScr.popupW = 310;
				GameScr.popupX = GameScr.gW / 2 - GameScr.popupW / 2;
				if (GameScr.isPaintInfoMe && GameScr.indexMenu > 0)
				{
					GameScr.popupW = w;
					GameScr.popupX = GameScr.gW2 - w / 2;
				}
			}
		}
		if (GameScr.popupY < -10)
		{
			GameScr.popupY = -10;
		}
		if (GameCanvas.h > 208 && GameScr.popupY < 0)
		{
			GameScr.popupY = 0;
		}
		if (GameCanvas.h == 208 && GameScr.popupY < 10)
		{
			GameScr.popupY = 10;
		}
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x0000790C File Offset: 0x00005B0C
	public static void loadImg()
	{
		TileMap.loadTileImage();
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x0006E52C File Offset: 0x0006C72C
	public void paintTitle(mGraphics g, string title, bool arrow)
	{
		int num = GameScr.gW / 2;
		g.setColor(Paint.COLORDARK);
		g.fillRoundRect(num - mFont.tahoma_8b.getWidth(title) / 2 - 12, GameScr.popupY + 4, mFont.tahoma_8b.getWidth(title) + 22, 24, 6, 6);
		if ((GameScr.indexTitle == 0 || GameCanvas.isTouch) && arrow)
		{
			SmallImage.drawSmallImage(g, 989, num - mFont.tahoma_8b.getWidth(title) / 2 - 15 - 7 - ((GameCanvas.gameTick % 8 > 3) ? 0 : 2), GameScr.popupY + 16, 2, StaticObj.VCENTER_HCENTER);
			SmallImage.drawSmallImage(g, 989, num + mFont.tahoma_8b.getWidth(title) / 2 + 15 + 5 + ((GameCanvas.gameTick % 8 > 3) ? 0 : 2), GameScr.popupY + 16, 0, StaticObj.VCENTER_HCENTER);
		}
		if (GameScr.indexTitle == 0)
		{
			g.setColor(Paint.COLORFOCUS);
		}
		else
		{
			g.setColor(Paint.COLORBORDER);
		}
		g.drawRoundRect(num - mFont.tahoma_8b.getWidth(title) / 2 - 12, GameScr.popupY + 4, mFont.tahoma_8b.getWidth(title) + 22, 24, 6, 6);
		mFont.tahoma_8b.drawString(g, title, num, GameScr.popupY + 9, 2);
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x0006E68C File Offset: 0x0006C88C
	public static int getTaskMapId()
	{
		int result;
		if (global::Char.myCharz().taskMaint == null)
		{
			result = -1;
		}
		else
		{
			result = GameScr.mapTasks[global::Char.myCharz().taskMaint.index];
		}
		return result;
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x0006E6C8 File Offset: 0x0006C8C8
	public static sbyte getTaskNpcId()
	{
		sbyte result = 0;
		if (global::Char.myCharz().taskMaint == null)
		{
			result = -1;
		}
		else if (global::Char.myCharz().taskMaint.index <= GameScr.tasks.Length - 1)
		{
			result = (sbyte)GameScr.tasks[global::Char.myCharz().taskMaint.index];
		}
		return result;
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x000045ED File Offset: 0x000027ED
	public void refreshTeam()
	{
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x0006E724 File Offset: 0x0006C924
	public void onChatFromMe(string text, string to)
	{
		Res.outz("CHAT");
		ChatTextField.gI().isShow = false;
		if (Mod.DungPham.KoiOctiiu957.MainMod.Chat(text))
		{
			return;
		}
		if (to.Equals(mResources.chat_player))
		{
			if (GameScr.info2.playerID == global::Char.myCharz().charID)
			{
				return;
			}
			Service.gI().chatPlayer(text, GameScr.info2.playerID);
			return;
		}
		else
		{
			if (text.Equals(string.Empty))
			{
				return;
			}
			Service.gI().chat(text);
			return;
		}
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x00007913 File Offset: 0x00005B13
	public void onCancelChat()
	{
		if (GameScr.isPaintMessage)
		{
			GameScr.isPaintMessage = false;
			ChatTextField.gI().center = null;
		}
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x0006E7B8 File Offset: 0x0006C9B8
	public void openWeb(string strLeft, string strRight, string url, string title, string str)
	{
		GameScr.isPaintAlert = true;
		this.isLockKey = true;
		GameScr.indexRow = 0;
		GameScr.setPopupSize(175, 200);
		this.textsTitle = title;
		this.texts = mFont.tahoma_7.splitFontVector(str, GameScr.popupW - 30);
		this.center = null;
		this.left = new Command(strLeft, 11068, url);
		this.right = new Command(strRight, 11069);
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x0006E834 File Offset: 0x0006CA34
	public void sendSms(string strLeft, string strRight, short port, string syntax, string title, string str)
	{
		GameScr.isPaintAlert = true;
		this.isLockKey = true;
		GameScr.indexRow = 0;
		GameScr.setPopupSize(175, 200);
		this.textsTitle = title;
		this.texts = mFont.tahoma_7.splitFontVector(str, GameScr.popupW - 30);
		this.center = null;
		MyVector myVector = new MyVector();
		myVector.addElement(string.Empty + port);
		myVector.addElement(syntax);
		this.left = new Command(strLeft, 11074);
		this.right = new Command(strRight, 11075);
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x00007930 File Offset: 0x00005B30
	public void actMenu()
	{
		GameCanvas.panel.setTypeMain();
		GameCanvas.panel.show();
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x0006E8D4 File Offset: 0x0006CAD4
	public void openUIZone(Message message)
	{
		if (GameScr.isChangeZone || (GameCanvas.panel != null && GameCanvas.panel.isChangeZone))
		{
			return;
		}
		InfoDlg.hide();
		try
		{
			sbyte b = message.reader().readByte();
			bool flag = this.zones == null || this.zones.Length != (int)b;
			if (flag)
			{
				this.zones = new int[(int)b];
				this.pts = new int[(int)b];
				this.numPlayer = new int[(int)b];
				this.maxPlayer = new int[(int)b];
				this.rank1 = new int[(int)b];
				this.rankName1 = new string[(int)b];
				this.rank2 = new int[(int)b];
				this.rankName2 = new string[(int)b];
			}
			for (int i = 0; i < this.zones.Length; i++)
			{
				this.zones[i] = (int)message.reader().readByte();
				this.pts[i] = (int)message.reader().readByte();
				this.numPlayer[i] = (int)message.reader().readByte();
				this.maxPlayer[i] = (int)message.reader().readByte();
				if (message.reader().readByte() == 1)
				{
					this.rankName1[i] = message.reader().readUTF();
					this.rank1[i] = message.reader().readInt();
					this.rankName2[i] = message.reader().readUTF();
					this.rank2[i] = message.reader().readInt();
				}
			}
			if (GameCanvas.panel.isShow && GameCanvas.panel.type == 3 && flag)
			{
				int num = (GameCanvas.panel.scroll != null) ? GameCanvas.panel.scroll.cmy : 0;
				GameCanvas.panel.setTabZone();
				if (GameCanvas.panel.scroll != null)
				{
					GameCanvas.panel.scroll.cmy = num;
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham OPEN UIZONE: " + ex.ToString());
		}
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x00007946 File Offset: 0x00005B46
	public void showViewInfo()
	{
		GameScr.indexMenu = 3;
		GameScr.isPaintInfoMe = true;
		GameScr.setPopupSize(175, 200);
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x0006EB04 File Offset: 0x0006CD04
	private void actDead()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(mResources.DIES[1], 110381));
		myVector.addElement(new Command(mResources.DIES[2], 110382));
		myVector.addElement(new Command(mResources.DIES[3], 110383));
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x00007963 File Offset: 0x00005B63
	public void startYesNoPopUp(string info, Command cmdYes, Command cmdNo)
	{
		this.popUpYesNo = new PopUpYesNo();
		this.popUpYesNo.setPopUp(info, cmdYes, cmdNo);
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x0006EB68 File Offset: 0x0006CD68
	public void player_vs_player(int playerId, int xu, string info, sbyte typePK)
	{
		global::Char @char = GameScr.findCharInMap(playerId);
		if (@char != null)
		{
			if ((int)typePK == 3)
			{
				this.startYesNoPopUp(info, new Command(mResources.OK, 2000, @char), new Command(mResources.CLOSE, 2009, @char));
			}
			if ((int)typePK == 4)
			{
				this.startYesNoPopUp(info, new Command(mResources.OK, 2005, @char), new Command(mResources.CLOSE, 2009, @char));
			}
		}
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x0006EBE4 File Offset: 0x0006CDE4
	public void giaodich(int playerID)
	{
		global::Char @char = GameScr.findCharInMap(playerID);
		if (@char != null)
		{
			this.startYesNoPopUp(@char.cName + mResources.want_to_trade, new Command(mResources.YES, 11114, @char), new Command(mResources.NO, 2009, @char));
		}
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x0006EC34 File Offset: 0x0006CE34
	public void getFlagImage(int charID, sbyte cflag)
	{
		if (GameScr.vFlag.size() == 0)
		{
			Service.gI().getFlag(2, cflag);
			Res.outz("getFlag1");
		}
		else if (charID == global::Char.myCharz().charID)
		{
			Res.outz("my cflag: isme");
			if (global::Char.myCharz().isGetFlagImage(cflag))
			{
				Res.outz("my cflag: true");
				for (int i = 0; i < GameScr.vFlag.size(); i++)
				{
					PKFlag pkflag = (PKFlag)GameScr.vFlag.elementAt(i);
					if (pkflag != null && (int)pkflag.cflag == (int)cflag)
					{
						Res.outz("my cflag: cflag==");
						global::Char.myCharz().flagImage = pkflag.IDimageFlag;
					}
				}
			}
			else if (!global::Char.myCharz().isGetFlagImage(cflag))
			{
				Res.outz("my cflag: false");
				Service.gI().getFlag(2, cflag);
			}
		}
		else
		{
			Res.outz("my cflag: not me");
			if (GameScr.findCharInMap(charID) != null)
			{
				if (GameScr.findCharInMap(charID).isGetFlagImage(cflag))
				{
					Res.outz("my cflag: true");
					for (int j = 0; j < GameScr.vFlag.size(); j++)
					{
						PKFlag pkflag2 = (PKFlag)GameScr.vFlag.elementAt(j);
						if (pkflag2 != null && (int)pkflag2.cflag == (int)cflag)
						{
							Res.outz("my cflag: cflag==");
							GameScr.findCharInMap(charID).flagImage = pkflag2.IDimageFlag;
						}
					}
				}
				else if (!GameScr.findCharInMap(charID).isGetFlagImage(cflag))
				{
					Res.outz("my cflag: false");
					Service.gI().getFlag(2, cflag);
				}
			}
		}
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x0006EDE0 File Offset: 0x0006CFE0
	public void actionPerform(int idAction, object p)
	{
		Cout.println("PERFORM WITH ID = " + idAction);
		switch (idAction)
		{
		case 2000:
			this.popUpYesNo = null;
			GameCanvas.endDlg();
			if ((global::Char)p == null)
			{
				Service.gI().player_vs_player(1, 3, -1);
				return;
			}
			Service.gI().player_vs_player(1, 3, ((global::Char)p).charID);
			Service.gI().charMove();
			break;
		case 2001:
			GameCanvas.endDlg();
			break;
		default:
			switch (idAction)
			{
			case 11111:
				if (global::Char.myCharz().charFocus == null)
				{
					return;
				}
				InfoDlg.showWait();
				if (GameCanvas.panel.vPlayerMenu.size() <= 0)
				{
					this.playerMenu(global::Char.myCharz().charFocus);
				}
				GameCanvas.panel.setTypePlayerMenu(global::Char.myCharz().charFocus);
				GameCanvas.panel.show();
				Service.gI().getPlayerMenu(global::Char.myCharz().charFocus.charID);
				Service.gI().messagePlayerMenu(global::Char.myCharz().charFocus.charID);
				break;
			case 11112:
			{
				global::Char @char = (global::Char)p;
				Service.gI().friend(1, @char.charID);
				break;
			}
			case 11113:
			{
				global::Char char2 = (global::Char)p;
				if (char2 != null)
				{
					Service.gI().giaodich(0, char2.charID, -1, -1);
				}
				break;
			}
			case 11114:
			{
				this.popUpYesNo = null;
				GameCanvas.endDlg();
				global::Char char3 = (global::Char)p;
				if (char3 == null)
				{
					return;
				}
				Service.gI().giaodich(1, char3.charID, -1, -1);
				break;
			}
			case 11115:
				if (global::Char.myCharz().charFocus == null)
				{
					return;
				}
				InfoDlg.showWait();
				Service.gI().playerMenuAction(global::Char.myCharz().charFocus.charID, (short)global::Char.myCharz().charFocus.menuSelect);
				break;
			default:
				switch (idAction)
				{
				case 12000:
					Service.gI().getClan(1, -1, null);
					break;
				case 12001:
					GameCanvas.endDlg();
					break;
				case 12002:
				{
					GameCanvas.endDlg();
					ClanObject clanObject = (ClanObject)p;
					Service.gI().clanInvite(1, -1, clanObject.clanID, clanObject.code);
					this.popUpYesNo = null;
					break;
				}
				case 12003:
				{
					ClanObject clanObject = (ClanObject)p;
					GameCanvas.endDlg();
					Service.gI().clanInvite(2, -1, clanObject.clanID, clanObject.code);
					this.popUpYesNo = null;
					break;
				}
				case 12004:
				{
					Skill skill = (Skill)p;
					this.doUseSkill(skill, true);
					global::Char.myCharz().saveLoadPreviousSkill();
					break;
				}
				case 12005:
					if (GameCanvas.serverScr == null)
					{
						GameCanvas.serverScr = new ServerScr();
					}
					GameCanvas.serverScr.switchToMe();
					GameCanvas.endDlg();
					break;
				case 12006:
					GameMidlet.instance.exit();
					break;
				default:
					switch (idAction)
					{
					case 11000:
						this.actMenu();
						break;
					case 11001:
						global::Char.myCharz().findNextFocusByKey();
						break;
					case 11002:
						GameCanvas.panel.hide();
						break;
					default:
						if (idAction != 1)
						{
							if (idAction != 2)
							{
								switch (idAction)
								{
								case 11057:
								{
									Effect2.vEffect2Outside.removeAllElements();
									Effect2.vEffect2.removeAllElements();
									object[] menuData = (object[])p;
									Npc npc = (Npc)menuData[0];
									int originalIndex = (int)menuData[1];
									if (npc.idItem == 0)
									{
										Service.gI().confirmMenu((short)npc.template.npcTemplateId, (sbyte)originalIndex);
									}
									else if (originalIndex == 0)
									{
										Service.gI().pickItem(npc.idItem);
									}
									break;
								}
								default:
									switch (idAction)
									{
									case 110001:
										GameCanvas.panel.setTypeMain();
										GameCanvas.panel.show();
										break;
									default:
										if (idAction != 110382)
										{
											if (idAction != 110383)
											{
												if (idAction != 8002)
												{
													if (idAction != 11038)
													{
														if (idAction != 11067)
														{
															if (idAction != 110391)
															{
																if (idAction == 888351)
																{
																	Service.gI().petStatus(5);
																	GameCanvas.endDlg();
																}
															}
															else
															{
																Service.gI().clanInvite(0, global::Char.myCharz().charFocus.charID, -1, -1);
															}
														}
														else if (TileMap.zoneID != GameScr.indexSelect)
														{
															Service.gI().requestChangeZone(GameScr.indexSelect, this.indexItemUse);
															InfoDlg.showWait();
														}
														else
														{
															GameScr.info1.addInfo(mResources.ZONE_HERE, 0);
														}
													}
													else
													{
														this.actDead();
													}
												}
												else
												{
													this.doFire(false, true);
													GameCanvas.clearKeyHold();
													GameCanvas.clearKeyPressed();
												}
											}
											else
											{
												Service.gI().wakeUpFromDead();
											}
										}
										else
										{
											Service.gI().returnTownFromDead();
										}
										break;
									case 110004:
										GameCanvas.menu.showMenu = false;
										break;
									}
									break;
								case 11059:
								{
									Skill skill2 = GameScr.onScreenSkill[this.selectedIndexSkill];
									this.doUseSkill(skill2, false);
									this.center = null;
									break;
								}
								}
							}
							else
							{
								GameCanvas.menu.showMenu = false;
							}
						}
						else
						{
							GameCanvas.endDlg();
						}
						break;
					}
					break;
				}
				break;
			case 11120:
			{
				object[] array = (object[])p;
				Skill skill3 = (Skill)array[0];
				int num = int.Parse((string)array[1]);
				for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
				{
					if (GameScr.onScreenSkill[i] == skill3)
					{
						GameScr.onScreenSkill[i] = null;
					}
				}
				GameScr.onScreenSkill[num] = skill3;
				break;
			}
			case 11121:
			{
				object[] array2 = (object[])p;
				Skill skill4 = (Skill)array2[0];
				int num2 = int.Parse((string)array2[1]);
				for (int j = 0; j < GameScr.keySkill.Length; j++)
				{
					if (GameScr.keySkill[j] == skill4)
					{
						GameScr.keySkill[j] = null;
					}
				}
				GameScr.keySkill[num2] = skill4;
				break;
			}
			}
			break;
		case 2003:
			GameCanvas.endDlg();
			InfoDlg.showWait();
			Service.gI().player_vs_player(0, 3, global::Char.myCharz().charFocus.charID);
			break;
		case 2004:
			GameCanvas.endDlg();
			Service.gI().player_vs_player(0, 4, global::Char.myCharz().charFocus.charID);
			break;
		case 2005:
			GameCanvas.endDlg();
			this.popUpYesNo = null;
			if ((global::Char)p == null)
			{
				Service.gI().player_vs_player(1, 4, -1);
				return;
			}
			Service.gI().player_vs_player(1, 4, ((global::Char)p).charID);
			break;
		case 2006:
			GameCanvas.endDlg();
			Service.gI().player_vs_player(2, 4, global::Char.myCharz().charFocus.charID);
			break;
		case 2007:
			GameCanvas.endDlg();
			GameMidlet.instance.exit();
			break;
		case 2009:
			this.popUpYesNo = null;
			break;
		}
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x0006F520 File Offset: 0x0006D720
	private static void setTouchBtn()
	{
		if (GameScr.isAnalog == 0)
		{
			return;
		}
		GameScr.xTG = (GameScr.xF = GameCanvas.w - 45);
		if (GameScr.gamePad.isLargeGamePad)
		{
			GameScr.xSkill = GameScr.gamePad.wZone + 20;
			GameScr.wSkill = 35;
			GameScr.xHP = GameScr.xF - 45;
		}
		else if (GameScr.gamePad.isMediumGamePad)
		{
			GameScr.xHP = GameScr.xF - 45;
		}
		GameScr.yF = GameCanvas.h - 45;
		GameScr.yTG = GameScr.yF - 45;
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x0006F5BC File Offset: 0x0006D7BC
	private void updateGamePad()
	{
		if (GameScr.isAnalog == 0)
		{
			return;
		}
		if (global::Char.myCharz().statusMe == 14)
		{
			return;
		}
		if (GameCanvas.isPointerHoldIn(GameScr.xF, GameScr.yF, 40, 40))
		{
			mScreen.keyTouch = 5;
			if (GameCanvas.isPointerJustRelease)
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = true;
				GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
			}
		}
		GameScr.gamePad.update();
		if (GameCanvas.isPointerHoldIn(GameScr.xTG, GameScr.yTG, 34, 34))
		{
			mScreen.keyTouch = 13;
			GameCanvas.isPointerJustDown = false;
			this.isPointerDowning = false;
			if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				global::Char.myCharz().findNextFocusByKey();
				GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
			}
		}
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x0006F6A0 File Offset: 0x0006D8A0
	private void paintGamePad(mGraphics g)
	{
		if (GameScr.isAnalog == 0)
		{
			return;
		}
		if (global::Char.myCharz().statusMe == 14)
		{
			return;
		}
		g.drawImage((mScreen.keyTouch != 5 && mScreen.keyMouse != 5) ? GameScr.imgFire0 : GameScr.imgFire1, GameScr.xF + 20, GameScr.yF + 20, mGraphics.HCENTER | mGraphics.VCENTER);
		GameScr.gamePad.paint(g);
		g.drawImage((mScreen.keyTouch != 13) ? GameScr.imgFocus : GameScr.imgFocus2, GameScr.xTG + 20, GameScr.yTG + 20, mGraphics.HCENTER | mGraphics.VCENTER);
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x0006F758 File Offset: 0x0006D958
	public void showWinNumber(string num, string finish)
	{
		this.winnumber = new int[num.Length];
		this.randomNumber = new int[num.Length];
		this.tMove = new int[num.Length];
		this.moveCount = new int[num.Length];
		this.delayMove = new int[num.Length];
		try
		{
			for (int i = 0; i < num.Length; i++)
			{
				this.winnumber[i] = (int)short.Parse(num[i].ToString());
				this.randomNumber[i] = Res.random(0, 11);
				this.tMove[i] = 1;
				this.delayMove[i] = 0;
			}
		}
		catch (Exception ex)
		{
		}
		this.tShow = 100;
		this.moveIndex = 0;
		this.strFinish = finish;
		GameScr.lastXS = (GameScr.currXS = mSystem.currentTimeMillis());
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x0006F858 File Offset: 0x0006DA58
	public void chatVip(string chatVip)
	{
		if (!this.startChat)
		{
			this.currChatWidth = mFont.tahoma_7b_yellowSmall.getWidth(chatVip);
			this.xChatVip = GameCanvas.w;
			this.startChat = true;
		}
		if (chatVip.StartsWith("!"))
		{
			chatVip = chatVip.Substring(1, chatVip.Length);
			this.isFireWorks = true;
		}
		if (chatVip.StartsWith("BOSS"))
		{
			MainMod.listBosses.Add(new Boss(chatVip));
			if (MainMod.listBosses.Count > 5)
			{
				MainMod.listBosses.RemoveAt(0);
			}
		}
		GameScr.vChatVip.addElement(chatVip);
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x0000797E File Offset: 0x00005B7E
	public void clearChatVip()
	{
		GameScr.vChatVip.removeAllElements();
		this.xChatVip = GameCanvas.w;
		this.startChat = false;
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x0006F8F4 File Offset: 0x0006DAF4
	public void paintChatVip(mGraphics g)
	{
		if (GameScr.vChatVip.size() == 0)
		{
			return;
		}
		if (!GameScr.isPaintChatVip)
		{
			return;
		}
		g.setClip(0, GameCanvas.h - 13, GameCanvas.w, 15);
		string st = (string)GameScr.vChatVip.elementAt(0);
		mFont.tahoma_7b_yellow.drawString(g, st, this.xChatVip, GameCanvas.h - 13, 0, mFont.tahoma_7b_dark);
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x0006F97C File Offset: 0x0006DB7C
	public void updateChatVip()
	{
		if (this.startChat)
		{
			this.xChatVip -= 2;
			if (this.xChatVip < -this.currChatWidth)
			{
				this.xChatVip = GameCanvas.w;
				GameScr.vChatVip.removeElementAt(0);
				if (GameScr.vChatVip.size() == 0)
				{
					this.isFireWorks = false;
					this.startChat = false;
					return;
				}
				this.currChatWidth = mFont.tahoma_7b_white.getWidth((string)GameScr.vChatVip.elementAt(0));
			}
		}
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x0000799C File Offset: 0x00005B9C
	public void showYourNumber(string strNum)
	{
		this.yourNumber = strNum;
		this.strPaint = mFont.tahoma_7.splitFontArray(this.yourNumber, 500);
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x000079C0 File Offset: 0x00005BC0
	public static void checkRemoveImage()
	{
		ImgByName.checkDelHash(ImgByName.hashImagePath, 10, false);
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x0006FA08 File Offset: 0x0006DC08
	public static void StartServerPopUp(string strMsg)
	{
		GameCanvas.endDlg();
		int avatar = 1139;
		ChatPopup.addBigMessage(strMsg, 100000, new Npc(-1, 0, 0, 0, 0, 0)
		{
			avatar = avatar
		});
		ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
		ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 35;
		ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x000079CF File Offset: 0x00005BCF
	public static bool ispaintPhubangBar()
	{
		return TileMap.mapPhuBang() && GameScr.phuban_Info.type_PB == 0;
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x0006FA90 File Offset: 0x0006DC90
	public void paintPhuBanBar(mGraphics g, int x, int y, int w)
	{
		if (GameScr.phuban_Info == null)
		{
			return;
		}
		if (!GameScr.isPaintOther && GameScr.isPaintRada == 1 && !GameCanvas.panel.isShow && GameScr.ispaintPhubangBar())
		{
			if (w < GameScr.fra_PVE_Bar_1.frameWidth + GameScr.fra_PVE_Bar_0.frameWidth * 4)
			{
				w = GameScr.fra_PVE_Bar_1.frameWidth + GameScr.fra_PVE_Bar_0.frameWidth * 4;
			}
			if (x > GameCanvas.w - w / 2)
			{
				x = GameCanvas.w - w / 2;
			}
			if (x < mGraphics.getImageWidth(GameScr.imgKhung) + w / 2 + 10)
			{
				x = mGraphics.getImageWidth(GameScr.imgKhung) + w / 2 + 10;
			}
			int frameHeight = GameScr.fra_PVE_Bar_0.frameHeight;
			int num = y + frameHeight + mGraphics.getImageHeight(GameScr.imgBall) / 2 + 2;
			int frameWidth = GameScr.fra_PVE_Bar_1.frameWidth;
			int num2 = w / 2 - frameWidth / 2;
			int num3 = x - w / 2;
			int num4 = x + frameWidth / 2;
			int y2 = y + 3;
			int num5 = num2 - GameScr.fra_PVE_Bar_0.frameWidth;
			int num6 = num5 / GameScr.fra_PVE_Bar_0.frameWidth;
			if (num5 % GameScr.fra_PVE_Bar_0.frameWidth > 0)
			{
				num6++;
			}
			for (int i = 0; i < num6; i++)
			{
				if (i < num6 - 1)
				{
					GameScr.fra_PVE_Bar_0.drawFrame(1, num3 + GameScr.fra_PVE_Bar_0.frameWidth + i * GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
				}
				else
				{
					GameScr.fra_PVE_Bar_0.drawFrame(1, num3 + num5, y2, 0, 0, g);
				}
				if (i < num6 - 1)
				{
					GameScr.fra_PVE_Bar_0.drawFrame(1, num4 + i * GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
				}
				else
				{
					GameScr.fra_PVE_Bar_0.drawFrame(1, num4 + num5 - GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
				}
			}
			GameScr.fra_PVE_Bar_0.drawFrame(0, num3, y2, 2, 0, g);
			GameScr.fra_PVE_Bar_0.drawFrame(0, num4 + num5, y2, 0, 0, g);
			if (GameScr.phuban_Info.pointTeam1 > 0)
			{
				int idx = 2;
				int idx2 = 3;
				if (GameScr.phuban_Info.color_1 == 4)
				{
					idx = 4;
					idx2 = 5;
				}
				int num7 = GameScr.phuban_Info.pointTeam1 * num2 / GameScr.phuban_Info.maxPoint;
				if (num7 < 0)
				{
					num7 = 0;
				}
				if (num7 > num2)
				{
					num7 = num2;
				}
				g.setClip(num3 + num2 - num7, y2, num7, frameHeight);
				for (int j = 0; j < num6; j++)
				{
					if (j < num6 - 1)
					{
						GameScr.fra_PVE_Bar_0.drawFrame(idx2, num3 + GameScr.fra_PVE_Bar_0.frameWidth + j * GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
					}
					else
					{
						GameScr.fra_PVE_Bar_0.drawFrame(idx2, num3 + num5, y2, 0, 0, g);
					}
				}
				GameScr.fra_PVE_Bar_0.drawFrame(idx, num3, y2, 2, 0, g);
				GameCanvas.resetTrans(g);
			}
			if (GameScr.phuban_Info.pointTeam2 > 0)
			{
				int idx3 = 2;
				int idx4 = 3;
				if (GameScr.phuban_Info.color_2 == 4)
				{
					idx3 = 4;
					idx4 = 5;
				}
				int num8 = GameScr.phuban_Info.pointTeam2 * num2 / GameScr.phuban_Info.maxPoint;
				if (num8 < 0)
				{
					num8 = 0;
				}
				if (num8 > num2)
				{
					num8 = num2;
				}
				g.setClip(num4, y2, num8, frameHeight);
				for (int k = 0; k < num6; k++)
				{
					if (k < num6 - 1)
					{
						GameScr.fra_PVE_Bar_0.drawFrame(idx4, num4 + k * GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
					}
					else
					{
						GameScr.fra_PVE_Bar_0.drawFrame(idx4, num4 + num5 - GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
					}
				}
				GameScr.fra_PVE_Bar_0.drawFrame(idx3, num4 + num5, y2, 0, 0, g);
				GameCanvas.resetTrans(g);
			}
			GameScr.fra_PVE_Bar_1.drawFrame(0, x - frameWidth / 2, y, 0, 0, g);
			string timeCountDown = mSystem.getTimeCountDown(GameScr.phuban_Info.timeStart, (int)GameScr.phuban_Info.timeSecond, true, false);
			mFont.tahoma_7b_yellow.drawString(g, timeCountDown, x + 1, y + GameScr.fra_PVE_Bar_1.frameHeight / 2 - mFont.tahoma_7b_green2.getHeight() / 2, 2);
			Panel.setTextColor(GameScr.phuban_Info.color_1, 1).drawString(g, GameScr.phuban_Info.nameTeam1, x - 5, num + 5, 1);
			Panel.setTextColor(GameScr.phuban_Info.color_2, 1).drawString(g, GameScr.phuban_Info.nameTeam2, x + 5, num + 5, 0);
			if (GameScr.phuban_Info.type_PB != 0)
			{
				int y3 = y + frameHeight / 2 - 2;
				mFont.bigNumber_While.drawString(g, string.Empty + GameScr.phuban_Info.pointTeam1, num3 + num2 / 2, y3, 2);
				mFont.bigNumber_While.drawString(g, string.Empty + GameScr.phuban_Info.pointTeam2, num4 + num2 / 2, y3, 2);
			}
			g.drawImage(GameScr.imgVS, x, y + GameScr.fra_PVE_Bar_1.frameHeight + 2, 3);
			if (GameScr.phuban_Info.type_PB == 0)
			{
				GameScr.paintChienTruong_Life(g, GameScr.phuban_Info.maxLife, GameScr.phuban_Info.color_1, GameScr.phuban_Info.lifeTeam1, x - 13, GameScr.phuban_Info.color_2, GameScr.phuban_Info.lifeTeam2, x + 13, num);
			}
		}
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x00070014 File Offset: 0x0006E214
	public static void paintChienTruong_Life(mGraphics g, int maxLife, int cl1, int lifeTeam1, int x1, int cl2, int lifeTeam2, int x2, int y)
	{
		if (GameScr.imgBall != null)
		{
			int num = mGraphics.getImageHeight(GameScr.imgBall) / 2;
			for (int i = 0; i < maxLife; i++)
			{
				int num2 = 0;
				if (i < lifeTeam1)
				{
					num2 = 1;
				}
				g.drawRegion(GameScr.imgBall, 0, num2 * num, mGraphics.getImageWidth(GameScr.imgBall), num, 0, x1 - i * (num + 1), y, mGraphics.VCENTER | mGraphics.HCENTER);
			}
			for (int j = 0; j < maxLife; j++)
			{
				int num3 = 0;
				if (j < lifeTeam2)
				{
					num3 = 1;
				}
				g.drawRegion(GameScr.imgBall, 0, num3 * num, mGraphics.getImageWidth(GameScr.imgBall), num, 0, x2 + j * (num + 1), y, mGraphics.VCENTER | mGraphics.HCENTER);
			}
		}
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x000700D8 File Offset: 0x0006E2D8
	public static void paintHPBar_NEW(mGraphics g, int x, int y, global::Char c)
	{
		g.drawImage(GameScr.imgKhung, x, y, 0);
		int x2 = x + 3;
		int num = y + 19;
		int width = GameScr.imgHP_NEW.getWidth();
		int num2 = GameScr.imgHP_NEW.getHeight() / 2;
		int num3 = (int)(c.cHP * (long)width / c.cHPFull);
		if (num3 <= 0)
		{
			num3 = 1;
		}
		else if (num3 > width)
		{
			num3 = width;
		}
		g.drawRegion(GameScr.imgHP_NEW, 0, num2, num3, num2, 0, x2, num, 0);
		int num4 = (int)(c.cMP * (long)width / c.cMPFull);
		if (num4 <= 0)
		{
			num4 = 1;
		}
		else if (num4 > width)
		{
			num4 = width;
		}
		g.drawRegion(GameScr.imgHP_NEW, 0, 0, num4, num2, 0, x2, num + 6, 0);
		int x3 = x + GameScr.imgKhung.getWidth() / 2 + 1;
		int y2 = num + 13;
		mFont.tahoma_7_green2.drawString(g, c.cName, x3, y + 4, 2);
		if (c.mobFocus != null)
		{
			if (c.mobFocus.getTemplate() != null)
			{
				mFont.tahoma_7_green2.drawString(g, c.mobFocus.getTemplate().name, x3, y2, 2);
			}
		}
		else if (c.npcFocus != null)
		{
			mFont.tahoma_7_green2.drawString(g, c.npcFocus.template.name, x3, y2, 2);
		}
		else if (c.charFocus != null)
		{
			mFont.tahoma_7_green2.drawString(g, c.charFocus.cName, x3, y2, 2);
		}
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x00070264 File Offset: 0x0006E464
	public static void addEffectEnd(int type, int subtype, int typePaint, int x, int y, int levelPaint, int dir, short timeRemove, Point[] listObj)
	{
		Effect_End eff = new Effect_End(type, subtype, typePaint, x, y, levelPaint, dir, timeRemove, listObj);
		GameScr.addEffect2Vector(eff);
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x0007028C File Offset: 0x0006E48C
	public static void addEffectEnd_Target(int type, int subtype, int typePaint, global::Char charUse, Point target, int levelPaint, short timeRemove, short range)
	{
		Effect_End eff = new Effect_End(type, subtype, typePaint, charUse.clone(), target, levelPaint, timeRemove, range);
		GameScr.addEffect2Vector(eff);
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x000079ED File Offset: 0x00005BED
	public static void addEffect2Vector(Effect_End eff)
	{
		if (eff.levelPaint == 0)
		{
			EffectManager.addHiEffect(eff);
			return;
		}
		if (eff.levelPaint == 1)
		{
			EffectManager.addMidEffects(eff);
			return;
		}
		if (eff.levelPaint == 2)
		{
			EffectManager.addMid_2Effects(eff);
			return;
		}
		EffectManager.addLowEffect(eff);
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x000702B8 File Offset: 0x0006E4B8
	public static bool setIsInScreen(int x, int y, int wOne, int hOne)
	{
		return x >= GameScr.cmx - wOne && x <= GameScr.cmx + GameCanvas.w + wOne && y >= GameScr.cmy - hOne && y <= GameScr.cmy + GameCanvas.h + hOne * 3 / 2;
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x00007A24 File Offset: 0x00005C24
	public static bool isSmallScr()
	{
		return GameCanvas.w <= 320;
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x0007030C File Offset: 0x0006E50C
	private void paint_xp_bar(mGraphics g)
	{
		return;
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x000703A4 File Offset: 0x0006E5A4
	private void paint_ios_bg(mGraphics g)
	{
		if (mSystem.clientType != 5)
		{
			return;
		}
		if (GameScr.imgBgIOS != null)
		{
			g.setColor(16777215);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			g.drawImage(GameScr.imgBgIOS, GameCanvas.w / 2, GameCanvas.h / 2, mGraphics.VCENTER | mGraphics.HCENTER);
		}
		else
		{
			int num = (TileMap.bgID % 2 != 0) ? 1 : 2;
			GameScr.imgBgIOS = GameCanvas.loadImage("/bg/bg_ios_" + num + ".png");
		}
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x00070440 File Offset: 0x0006E640
	public void paint_CT(mGraphics g, int x, int y, int w)
	{
		w = 194;
		w = 182;
		w = 170;
		int num = 66;
		int num2 = 11;
		if (x > GameCanvas.w - w / 2)
		{
			x = GameCanvas.w - w / 2;
		}
		if (x < mGraphics.getImageWidth(GameScr.imgKhung) + w / 2 + 10)
		{
			x = mGraphics.getImageWidth(GameScr.imgKhung) + w / 2 + 10;
		}
		int frameHeight = GameScr.fra_PVE_Bar_0.frameHeight;
		int num3 = y + frameHeight + mGraphics.getImageHeight(GameScr.imgBall) / 2 + 2;
		int frameWidth = GameScr.fra_PVE_Bar_1.frameWidth;
		int num4 = w / 2 - frameWidth / 2;
		int num5 = x - w / 2 + 3;
		int num6 = x + frameWidth / 2;
		int num7 = y + 3;
		int num8 = num4 - GameScr.fra_PVE_Bar_0.frameWidth;
		int num9 = num8 / GameScr.fra_PVE_Bar_0.frameWidth;
		if (num8 % GameScr.fra_PVE_Bar_0.frameWidth > 0)
		{
			num9++;
		}
		for (int i = 0; i < num9; i++)
		{
			if (i < num9 - 1)
			{
				g.drawRegion(GameScr.img_ct_bar_0, 0, 15, mGraphics.getImageWidth(GameScr.img_ct_bar_0), 15, 2, num5 + GameScr.fra_PVE_Bar_0.frameWidth + i * GameScr.fra_PVE_Bar_0.frameWidth, num7, mGraphics.TOP | mGraphics.LEFT, true);
			}
			else
			{
				g.drawRegion(GameScr.img_ct_bar_0, 0, 15, mGraphics.getImageWidth(GameScr.img_ct_bar_0), 15, 2, num5 + num8, num7, mGraphics.TOP | mGraphics.LEFT, true);
			}
			if (i < num9 - 1)
			{
				g.drawRegion(GameScr.img_ct_bar_0, 0, 15, mGraphics.getImageWidth(GameScr.img_ct_bar_0), 15, 2, num6 + i * GameScr.fra_PVE_Bar_0.frameWidth, num7, mGraphics.TOP | mGraphics.LEFT, true);
			}
			else
			{
				g.drawRegion(GameScr.img_ct_bar_0, 0, 15, mGraphics.getImageWidth(GameScr.img_ct_bar_0), 15, 2, num6 + num8 - GameScr.fra_PVE_Bar_0.frameWidth, num7, mGraphics.TOP | mGraphics.LEFT, true);
			}
		}
		GameScr.fra_PVE_Bar_0.drawFrame(0, num5, num7, 2, 0, g);
		GameScr.fra_PVE_Bar_0.drawFrame(0, num6 + num8, num7, 0, 0, g);
		int num10 = GameScr.nCT_TeamA * 100 / (GameScr.nCT_nBoyBaller / 2) * num / 100;
		if (num10 > 0)
		{
			if (num10 < 6)
			{
				num10 = 6;
			}
			g.setClip(num5, num7, num10, 15);
		}
		if (GameScr.nCT_TeamA > 0)
		{
			for (int j = 0; j < num2; j++)
			{
				if (j == 0)
				{
					g.drawRegion(GameScr.img_ct_bar_0, 0, 60, mGraphics.getImageWidth(GameScr.img_ct_bar_0), 15, 2, num5, num7, mGraphics.TOP | mGraphics.LEFT, true);
				}
				else
				{
					g.drawRegion(GameScr.img_ct_bar_0, 0, 75, mGraphics.getImageWidth(GameScr.img_ct_bar_0), 15, 2, num5 + j * 6, num7, mGraphics.TOP | mGraphics.LEFT, true);
				}
			}
		}
		GameCanvas.resetTrans(g);
		int num11 = GameScr.nCT_TeamB * 100 / (GameScr.nCT_nBoyBaller / 2) * num / 100;
		if (num - (num - num11) > 0)
		{
			if (num11 < 6)
			{
				num11 = 6;
			}
			g.setClip(num6 + num - num11, num7, num - (num - num11), 15);
		}
		if (GameScr.nCT_TeamB > 0)
		{
			for (int k = 0; k < num2; k++)
			{
				if (k == 0)
				{
					g.drawRegion(GameScr.img_ct_bar_0, 0, 30, mGraphics.getImageWidth(GameScr.img_ct_bar_0), 15, 0, num6 + num8, num7, mGraphics.TOP | mGraphics.LEFT, true);
				}
				else
				{
					g.drawRegion(GameScr.img_ct_bar_0, 0, 45, mGraphics.getImageWidth(GameScr.img_ct_bar_0), 15, 0, num6 + num8 - k * 6, num7, mGraphics.TOP | mGraphics.LEFT, true);
				}
			}
		}
		GameCanvas.resetTrans(g);
		GameScr.fra_PVE_Bar_1.drawFrame(0, x - frameWidth / 2 + 1, y, 0, 0, g);
		string st = NinjaUtil.getTime((int)((GameScr.nCT_timeBallte - mSystem.currentTimeMillis()) / 1000L)) + string.Empty;
		mFont.tahoma_7b_yellow.drawString(g, st, num5 + w / 2 - 2, y + 5, 2);
		mFont.tahoma_7_grey.drawString(g, "Tầng " + GameScr.nCT_floor, num5 + w / 2 - 3, y + GameScr.fra_PVE_Bar_1.frameHeight, mFont.CENTER);
		int width = mFont.tahoma_7b_red.getWidth(GameScr.nCT_TeamA + string.Empty);
		mFont.tahoma_7b_blue.drawString(g, GameScr.nCT_TeamA + string.Empty, x - frameWidth / 2 - width, num7 + GameScr.fra_PVE_Bar_1.frameHeight, 0);
		SmallImage.drawSmallImage(g, 2325, x - frameWidth / 2 - width - 15, num7 + GameScr.fra_PVE_Bar_1.frameHeight, 2, mGraphics.TOP | mGraphics.LEFT);
		width = mFont.tahoma_7b_red.getWidth(GameScr.nCT_TeamB + string.Empty);
		mFont.tahoma_7b_red.drawString(g, GameScr.nCT_TeamB + string.Empty, x + frameWidth / 2, num7 + GameScr.fra_PVE_Bar_1.frameHeight, 0);
		SmallImage.drawSmallImage(g, 2323, x + frameWidth / 2 + width + 3, num7 + GameScr.fra_PVE_Bar_1.frameHeight, 0, mGraphics.TOP | mGraphics.LEFT);
		this.paint_board_CT(g, GameCanvas.w - mFont.tahoma_7b_dark.getWidth("#01 AAAAAAAAAA"), 40);
		GameCanvas.resetTrans(g);
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x000709D0 File Offset: 0x0006EBD0
	private void paint_board_CT(mGraphics g, int x, int y)
	{
		if (!GameScr.is_Paint_boardCT_Expand)
		{
			string s = "#01 nnnnnnnnnnnn";
			int width = mFont.tahoma_7.getWidth(s);
			int num = GameCanvas.w - width - 20;
			for (int i = 0; i < GameScr.nTop; i++)
			{
				mFont mFont = mFont.tahoma_7_white;
				if (i == 0)
				{
					mFont = mFont.tahoma_7_red;
				}
				else if (i == 1)
				{
					mFont = mFont.tahoma_7_yellow;
				}
				else if (i == 2)
				{
					mFont = mFont.tahoma_7_blue;
				}
				if (i == GameScr.nTop - 1)
				{
					mFont = mFont.tahoma_7_green;
				}
				string[] array = Res.split((string)GameScr.res_CT.elementAt(i), "|", 0);
				int[] array2 = new int[]
				{
					0,
					18
				};
				for (int j = 0; j < 2; j++)
				{
					mFont.drawString(g, array[j], num + array2[j], y + i * mFont.tahoma_7.getHeight(), 0, mFont.tahoma_7);
				}
			}
			GameCanvas.resetTrans(g);
			GameScr.xRect = num;
			GameScr.yRect = y;
			GameScr.wRect = width + 10;
			GameScr.hRect = mFont.tahoma_7b_dark.getHeight() * 6;
		}
		else
		{
			string s2 = "#01 namec1000000 0001   00000";
			int[] array3 = new int[]
			{
				0,
				18,
				80,
				101
			};
			int width2 = mFont.tahoma_7.getWidth(s2);
			int num2 = GameCanvas.w - width2 - 20;
			for (int k = 0; k < GameScr.nTop; k++)
			{
				string[] array4 = Res.split((string)GameScr.res_CT.elementAt(k), "|", 0);
				mFont mFont2 = mFont.tahoma_7_white;
				if (k == 0)
				{
					mFont2 = mFont.tahoma_7_red;
				}
				else if (k == 1)
				{
					mFont2 = mFont.tahoma_7_yellow;
				}
				else if (k == 2)
				{
					mFont2 = mFont.tahoma_7_blue;
				}
				if (k == GameScr.nTop - 1)
				{
					mFont2 = mFont.tahoma_7_green;
				}
				int y2 = k * mFont.tahoma_7_white.getHeight() + y;
				for (int l = 0; l < array3.Length; l++)
				{
					mFont2.drawString(g, array4[l], num2 + array3[l], y2, 0, mFont.tahoma_7);
				}
			}
			GameScr.xRect = num2;
			GameScr.yRect = y;
			GameScr.wRect = width2 + 10;
			GameScr.hRect = mFont.tahoma_7b_dark.getHeight() * 6;
		}
		GameCanvas.resetTrans(g);
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x00070C48 File Offset: 0x0006EE48
	private void paintHPCT(mGraphics g, int x, int y, global::Char c)
	{
		g.drawImage(GameScr.imgKhung, x, y, 0);
		int x2 = x + 3;
		int num = y + 19;
		int width = GameScr.imgHP_NEW.getWidth();
		int num2 = GameScr.imgHP_NEW.getHeight() / 2;
		int num3 = (int)(c.cHP * (long)width / c.cHPFull);
		if (num3 > 0)
		{
			if (num3 > width)
			{
			}
		}
		g.drawRegion(GameScr.imgHP_NEW, 0, num2, 80, num2, 0, x2, num, 0);
		int num4 = (int)(c.cMP * (long)width / c.cMPFull);
		if (num4 > 0)
		{
			if (num4 > width)
			{
			}
		}
		g.drawRegion(GameScr.imgHP_NEW, 0, 0, 80, num2, 0, x2, num + 6, 0);
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x00071018 File Offset: 0x0006F218
	public static void RemovefindCharInMap(int charId)
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			if (((global::Char)GameScr.vCharInMap.elementAt(i)).charID == charId)
			{
				GameScr.vCharInMap.removeElementAt(i);
				i--;
			}
		}
	}

	public static bool isItemYardrat(Item item)
	{
		if (item == null || item.template == null)
		{
			return false;
		}
		if (item.template.id == 591 || item.template.id == 911)
		{
			return true;
		}
		if (item.template.name != null && (item.template.name.Contains("Yardrat") || item.template.name.Contains("yardrat")))
		{
			return true;
		}
		return false;
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x00071064 File Offset: 0x0006F264
	public static int findYardrat()
	{
		Item[] arrItemBag = global::Char.myCharz().arrItemBag;
		if (arrItemBag == null) return -1;
		for (int i = 0; i < arrItemBag.Length; i++)
		{
			if (GameScr.isItemYardrat(arrItemBag[i]))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x000710C4 File Offset: 0x0006F2C4
	public static void myTele(int CharID)
	{
		Item[] arrItemBody = global::Char.myCharz().arrItemBody;
		if (arrItemBody != null && arrItemBody.Length > 5 && GameScr.isItemYardrat(arrItemBody[5]))
		{
			GameScr.info1.addInfo("Đang mặc Yardrat", 0);
			Service.gI().gotoPlayer(CharID);
			return;
		}
		int num = GameScr.findYardrat();
		if (num == -1)
		{
			GameScr.info1.addInfo("Không có cải trang Yardrat!", 0);
			return;
		}
		if (arrItemBody == null || arrItemBody.Length <= 5 || arrItemBody[5] == null)
		{
			GameScr.info1.addInfo("Mặc Yardrat", 0);
			Service.gI().getItem(4, (sbyte)num);
			Service.gI().gotoPlayer(CharID);
			Service.gI().getItem(5, 5);
			return;
		}
		GameScr.info1.addInfo("Đổi Yardrat", 0);
		Service.gI().getItem(4, (sbyte)num);
		Service.gI().gotoPlayer(CharID);
		Service.gI().getItem(4, (sbyte)num);
	}

	// Token: 0x04000D19 RID: 3353
	public bool isWaitingDoubleClick;

	// Token: 0x04000D1A RID: 3354
	public long timeStartDblClick;

	// Token: 0x04000D1B RID: 3355
	public long timeEndDblClick;

	// Token: 0x04000D1C RID: 3356
	public static bool isPaintOther = false;

	// Token: 0x04000D1D RID: 3357
	public static MyVector textTime = new MyVector(string.Empty);

	// Token: 0x04000D1E RID: 3358
	public static bool isLoadAllData = false;

	// Token: 0x04000D1F RID: 3359
	public static GameScr instance;

	// Token: 0x04000D20 RID: 3360
	public static int gW;

	// Token: 0x04000D21 RID: 3361
	public static int gH;

	// Token: 0x04000D22 RID: 3362
	public static int gW2;

	// Token: 0x04000D23 RID: 3363
	public static int gssw;

	// Token: 0x04000D24 RID: 3364
	public static int gssh;

	// Token: 0x04000D25 RID: 3365
	public static int gH34;

	// Token: 0x04000D26 RID: 3366
	public static int gW3;

	// Token: 0x04000D27 RID: 3367
	public static int gH3;

	// Token: 0x04000D28 RID: 3368
	public static int gH23;

	// Token: 0x04000D29 RID: 3369
	public static int gW23;

	// Token: 0x04000D2A RID: 3370
	public static int gH2;

	// Token: 0x04000D2B RID: 3371
	public static int csPadMaxH;

	// Token: 0x04000D2C RID: 3372
	public static int cmdBarH;

	// Token: 0x04000D2D RID: 3373
	public static int gW34;

	// Token: 0x04000D2E RID: 3374
	public static int gW6;

	// Token: 0x04000D2F RID: 3375
	public static int gH6;

	// Token: 0x04000D30 RID: 3376
	public static int cmx;

	// Token: 0x04000D31 RID: 3377
	public static int cmy;

	// Token: 0x04000D32 RID: 3378
	public static int cmdx;

	// Token: 0x04000D33 RID: 3379
	public static int cmdy;

	// Token: 0x04000D34 RID: 3380
	public static int cmvx;

	// Token: 0x04000D35 RID: 3381
	public static int cmvy;

	// Token: 0x04000D36 RID: 3382
	public static int cmtoX;

	// Token: 0x04000D37 RID: 3383
	public static int cmtoY;

	// Token: 0x04000D38 RID: 3384
	public static int cmxLim;

	// Token: 0x04000D39 RID: 3385
	public static int cmyLim;

	// Token: 0x04000D3A RID: 3386
	public static int gssx;

	// Token: 0x04000D3B RID: 3387
	public static int gssy;

	// Token: 0x04000D3C RID: 3388
	public static int gssxe;

	// Token: 0x04000D3D RID: 3389
	public static int gssye;

	// Token: 0x04000D3E RID: 3390
	public Command cmdback;

	// Token: 0x04000D3F RID: 3391
	public Command cmdBag;

	// Token: 0x04000D40 RID: 3392
	public Command cmdSkill;

	// Token: 0x04000D41 RID: 3393
	public Command cmdTiemnang;

	// Token: 0x04000D42 RID: 3394
	public Command cmdtrangbi;

	// Token: 0x04000D43 RID: 3395
	public Command cmdInfo;

	// Token: 0x04000D44 RID: 3396
	public Command cmdFocus;

	// Token: 0x04000D45 RID: 3397
	public Command cmdFire;

	// Token: 0x04000D46 RID: 3398
	public static int d;

	// Token: 0x04000D47 RID: 3399
	public static int hpPotion;

	// Token: 0x04000D48 RID: 3400
	public static SkillPaint[] sks;

	// Token: 0x04000D49 RID: 3401
	public static Arrowpaint[] arrs;

	// Token: 0x04000D4A RID: 3402
	public static DartInfo[] darts;

	// Token: 0x04000D4B RID: 3403
	public static Part[] parts;

	// Token: 0x04000D4C RID: 3404
	public static EffectCharPaint[] efs;

	// Token: 0x04000D4D RID: 3405
	public static int lockTick;

	// Token: 0x04000D4E RID: 3406
	private int moveUp;

	// Token: 0x04000D4F RID: 3407
	private int moveDow;

	// Token: 0x04000D50 RID: 3408
	private int idTypeTask;

	// Token: 0x04000D51 RID: 3409
	private bool isstarOpen;

	// Token: 0x04000D52 RID: 3410
	private bool isChangeSkill;

	// Token: 0x04000D53 RID: 3411
	public static MyVector vClan = new MyVector();

	// Token: 0x04000D54 RID: 3412
	public static MyVector vPtMap = new MyVector();

	// Token: 0x04000D55 RID: 3413
	public static MyVector vFriend = new MyVector();

	// Token: 0x04000D56 RID: 3414
	public static MyVector vEnemies = new MyVector();

	// Token: 0x04000D57 RID: 3415
	public static MyVector vCharInMap = new MyVector();

	// Token: 0x04000D58 RID: 3416
	public static MyVector vItemMap = new MyVector();

	// Token: 0x04000D59 RID: 3417
	public static MyVector vMobAttack = new MyVector();

	// Token: 0x04000D5A RID: 3418
	public static MyVector vSet = new MyVector();

	// Token: 0x04000D5B RID: 3419
	public static MyVector vMob = new MyVector();

	// Token: 0x04000D5C RID: 3420
	public static MyVector vNpc = new MyVector();

	// Token: 0x04000D5D RID: 3421
	public static MyVector vFlag = new MyVector();

	// Token: 0x04000D5E RID: 3422
	public static NClass[] nClasss;

	// Token: 0x04000D5F RID: 3423
	public static int indexSize = 28;

	// Token: 0x04000D60 RID: 3424
	public static int indexTitle = 0;

	// Token: 0x04000D61 RID: 3425
	public static int indexSelect = 0;

	// Token: 0x04000D62 RID: 3426
	public static int indexRow = -1;

	// Token: 0x04000D63 RID: 3427
	public static int indexRowMax;

	// Token: 0x04000D64 RID: 3428
	public static int indexMenu = 0;

	// Token: 0x04000D65 RID: 3429
	public Item itemFocus;

	// Token: 0x04000D66 RID: 3430
	public ItemOptionTemplate[] iOptionTemplates;

	// Token: 0x04000D67 RID: 3431
	public SkillOptionTemplate[] sOptionTemplates;

	// Token: 0x04000D68 RID: 3432
	private static Scroll scrInfo = new Scroll();

	// Token: 0x04000D69 RID: 3433
	public static Scroll scrMain = new Scroll();

	// Token: 0x04000D6A RID: 3434
	public static MyVector vItemUpGrade = new MyVector();

	// Token: 0x04000D6B RID: 3435
	public static bool isTypeXu;

	// Token: 0x04000D6C RID: 3436
	public static bool isViewNext;

	// Token: 0x04000D6D RID: 3437
	public static bool isViewClanMemOnline = false;

	// Token: 0x04000D6E RID: 3438
	public static bool isViewClanInvite = true;

	// Token: 0x04000D6F RID: 3439
	public static bool isChop;

	// Token: 0x04000D70 RID: 3440
	public static string titleInputText = string.Empty;

	// Token: 0x04000D71 RID: 3441
	public static int tickMove;

	// Token: 0x04000D72 RID: 3442
	public static bool isPaintAlert = false;

	// Token: 0x04000D73 RID: 3443
	public static bool isPaintTask = false;

	// Token: 0x04000D74 RID: 3444
	public static bool isPaintTeam = false;

	// Token: 0x04000D75 RID: 3445
	public static bool isPaintFindTeam = false;

	// Token: 0x04000D76 RID: 3446
	public static bool isPaintFriend = false;

	// Token: 0x04000D77 RID: 3447
	public static bool isPaintEnemies = false;

	// Token: 0x04000D78 RID: 3448
	public static bool isPaintItemInfo = false;

	// Token: 0x04000D79 RID: 3449
	public static bool isHaveSelectSkill = false;

	// Token: 0x04000D7A RID: 3450
	public static bool isPaintSkill = false;

	// Token: 0x04000D7B RID: 3451
	public static bool isPaintInfoMe = false;

	// Token: 0x04000D7C RID: 3452
	public static bool isPaintStore = false;

	// Token: 0x04000D7D RID: 3453
	public static bool isPaintNonNam = false;

	// Token: 0x04000D7E RID: 3454
	public static bool isPaintNonNu = false;

	// Token: 0x04000D7F RID: 3455
	public static bool isPaintAoNam = false;

	// Token: 0x04000D80 RID: 3456
	public static bool isPaintAoNu = false;

	// Token: 0x04000D81 RID: 3457
	public static bool isPaintGangTayNam = false;

	// Token: 0x04000D82 RID: 3458
	public static bool isPaintGangTayNu = false;

	// Token: 0x04000D83 RID: 3459
	public static bool isPaintQuanNam = false;

	// Token: 0x04000D84 RID: 3460
	public static bool isPaintQuanNu = false;

	// Token: 0x04000D85 RID: 3461
	public static bool isPaintGiayNam = false;

	// Token: 0x04000D86 RID: 3462
	public static bool isPaintGiayNu = false;

	// Token: 0x04000D87 RID: 3463
	public static bool isPaintLien = false;

	// Token: 0x04000D88 RID: 3464
	public static bool isPaintNhan = false;

	// Token: 0x04000D89 RID: 3465
	public static bool isPaintNgocBoi = false;

	// Token: 0x04000D8A RID: 3466
	public static bool isPaintPhu = false;

	// Token: 0x04000D8B RID: 3467
	public static bool isPaintWeapon = false;

	// Token: 0x04000D8C RID: 3468
	public static bool isPaintStack = false;

	// Token: 0x04000D8D RID: 3469
	public static bool isPaintStackLock = false;

	// Token: 0x04000D8E RID: 3470
	public static bool isPaintGrocery = false;

	// Token: 0x04000D8F RID: 3471
	public static bool isPaintGroceryLock = false;

	// Token: 0x04000D90 RID: 3472
	public static bool isPaintUpGrade = false;

	// Token: 0x04000D91 RID: 3473
	public static bool isPaintConvert = false;

	// Token: 0x04000D92 RID: 3474
	public static bool isPaintUpGradeGold = false;

	// Token: 0x04000D93 RID: 3475
	public static bool isPaintUpPearl = false;

	// Token: 0x04000D94 RID: 3476
	public static bool isPaintBox = false;

	// Token: 0x04000D95 RID: 3477
	public static bool isPaintSplit = false;

	// Token: 0x04000D96 RID: 3478
	public static bool isPaintCharInMap = false;

	// Token: 0x04000D97 RID: 3479
	public static bool isPaintTrade = false;

	// Token: 0x04000D98 RID: 3480
	public static bool isPaintZone = false;

	// Token: 0x04000D99 RID: 3481
	public static bool isPaintMessage = false;

	// Token: 0x04000D9A RID: 3482
	public static bool isPaintClan = false;

	// Token: 0x04000D9B RID: 3483
	public static bool isRequestMember = false;

	// Token: 0x04000D9C RID: 3484
	public static global::Char currentCharViewInfo;

	// Token: 0x04000D9D RID: 3485
	public static long[] exps;

	// Token: 0x04000D9E RID: 3486
	public static int[] crystals;

	// Token: 0x04000D9F RID: 3487
	public static int[] upClothe;

	// Token: 0x04000DA0 RID: 3488
	public static int[] upAdorn;

	// Token: 0x04000DA1 RID: 3489
	public static int[] upWeapon;

	// Token: 0x04000DA2 RID: 3490
	public static int[] coinUpCrystals;

	// Token: 0x04000DA3 RID: 3491
	public static int[] coinUpClothes;

	// Token: 0x04000DA4 RID: 3492
	public static int[] coinUpAdorns;

	// Token: 0x04000DA5 RID: 3493
	public static int[] coinUpWeapons;

	// Token: 0x04000DA6 RID: 3494
	public static int[] maxPercents;

	// Token: 0x04000DA7 RID: 3495
	public static int[] goldUps;

	// Token: 0x04000DA8 RID: 3496
	public int tMenuDelay;

	// Token: 0x04000DA9 RID: 3497
	public int zoneCol = 6;

	// Token: 0x04000DAA RID: 3498
	public int[] zones;

	// Token: 0x04000DAB RID: 3499
	public int[] pts;

	// Token: 0x04000DAC RID: 3500
	public int[] numPlayer;

	// Token: 0x04000DAD RID: 3501
	public int[] maxPlayer;

	// Token: 0x04000DAE RID: 3502
	public int[] rank1;

	// Token: 0x04000DAF RID: 3503
	public int[] rank2;

	// Token: 0x04000DB0 RID: 3504
	public string[] rankName1;

	// Token: 0x04000DB1 RID: 3505
	public string[] rankName2;

	// Token: 0x04000DB2 RID: 3506
	public int typeTrade;

	// Token: 0x04000DB3 RID: 3507
	public int typeTradeOrder;

	// Token: 0x04000DB4 RID: 3508
	public int coinTrade;

	// Token: 0x04000DB5 RID: 3509
	public int coinTradeOrder;

	// Token: 0x04000DB6 RID: 3510
	public int timeTrade;

	// Token: 0x04000DB7 RID: 3511
	public int indexItemUse = -1;

	// Token: 0x04000DB8 RID: 3512
	public int cLastFocusID = -1;

	// Token: 0x04000DB9 RID: 3513
	public int cPreFocusID = -1;

	// Token: 0x04000DBA RID: 3514
	public bool isLockKey;

	// Token: 0x04000DBB RID: 3515
	public static int[] tasks;

	// Token: 0x04000DBC RID: 3516
	public static int[] mapTasks;

	// Token: 0x04000DBD RID: 3517
	public static Image imgRoomStat;

	// Token: 0x04000DBE RID: 3518
	public static Image frBarPow0;

	// Token: 0x04000DBF RID: 3519
	public static Image frBarPow1;

	// Token: 0x04000DC0 RID: 3520
	public static Image frBarPow2;

	// Token: 0x04000DC1 RID: 3521
	public static Image frBarPow20;

	// Token: 0x04000DC2 RID: 3522
	public static Image frBarPow21;

	// Token: 0x04000DC3 RID: 3523
	public static Image frBarPow22;

	// Token: 0x04000DC4 RID: 3524
	public MyVector texts;

	// Token: 0x04000DC5 RID: 3525
	public string textsTitle;

	// Token: 0x04000DC6 RID: 3526
	public static sbyte vcData;

	// Token: 0x04000DC7 RID: 3527
	public static sbyte vcMap;

	// Token: 0x04000DC8 RID: 3528
	public static sbyte vcSkill;

	// Token: 0x04000DC9 RID: 3529
	public static sbyte vcItem;

	// Token: 0x04000DCA RID: 3530
	public static sbyte vsData;

	// Token: 0x04000DCB RID: 3531
	public static sbyte vsMap;

	// Token: 0x04000DCC RID: 3532
	public static sbyte vsSkill;

	// Token: 0x04000DCD RID: 3533
	public static sbyte vsItem;

	// Token: 0x04000DCE RID: 3534
	public static sbyte vcTask;

	// Token: 0x04000DCF RID: 3535
	public static Image imgArrow;

	// Token: 0x04000DD0 RID: 3536
	public static Image imgArrow2;

	// Token: 0x04000DD1 RID: 3537
	public static Image imgChat;

	// Token: 0x04000DD2 RID: 3538
	public static Image imgChat2;

	// Token: 0x04000DD3 RID: 3539
	public static Image imgMenu;

	// Token: 0x04000DD4 RID: 3540
	public static Image imgFocus;

	// Token: 0x04000DD5 RID: 3541
	public static Image imgFocus2;

	// Token: 0x04000DD6 RID: 3542
	public static Image imgSkill;

	// Token: 0x04000DD7 RID: 3543
	public static Image imgSkill2;

	// Token: 0x04000DD8 RID: 3544
	public static Image imgHP1;

	// Token: 0x04000DD9 RID: 3545
	public static Image imgHP2;

	// Token: 0x04000DDA RID: 3546
	public static Image imgHP3;

	// Token: 0x04000DDB RID: 3547
	public static Image imgHP4;

	// Token: 0x04000DDC RID: 3548
	public static Image imgFire0;

	// Token: 0x04000DDD RID: 3549
	public static Image imgFire1;

	// Token: 0x04000DDE RID: 3550
	public static Image imgNR1;

	// Token: 0x04000DDF RID: 3551
	public static Image imgNR2;

	// Token: 0x04000DE0 RID: 3552
	public static Image imgNR3;

	// Token: 0x04000DE1 RID: 3553
	public static Image imgNR4;

	// Token: 0x04000DE2 RID: 3554
	public static Image imgLbtn;

	// Token: 0x04000DE3 RID: 3555
	public static Image imgLbtnFocus;

	// Token: 0x04000DE4 RID: 3556
	public static Image imgLbtn2;

	// Token: 0x04000DE5 RID: 3557
	public static Image imgLbtnFocus2;

	// Token: 0x04000DE6 RID: 3558
	public static Image imgAnalog1;

	// Token: 0x04000DE7 RID: 3559
	public static Image imgAnalog2;

	// Token: 0x04000DE8 RID: 3560
	public string tradeName = string.Empty;

	// Token: 0x04000DE9 RID: 3561
	public string tradeItemName = string.Empty;

	// Token: 0x04000DEA RID: 3562
	public int timeLengthMap;

	// Token: 0x04000DEB RID: 3563
	public int timeStartMap;

	// Token: 0x04000DEC RID: 3564
	public static sbyte typeViewInfo = 0;

	// Token: 0x04000DED RID: 3565
	public static sbyte typeActive = 0;

	// Token: 0x04000DEE RID: 3566
	public static InfoMe info1 = new InfoMe();

	// Token: 0x04000DEF RID: 3567
	public static InfoMe info2 = new InfoMe();

	// Token: 0x04000DF0 RID: 3568
	public static Image imgPanel;

	// Token: 0x04000DF1 RID: 3569
	public static Image imgPanel2;

	// Token: 0x04000DF2 RID: 3570
	public static Image imgHP;

	// Token: 0x04000DF3 RID: 3571
	public static Image imgMP;

	// Token: 0x04000DF4 RID: 3572
	public static Image imgSP;

	// Token: 0x04000DF5 RID: 3573
	public static Image imgHPLost;

	// Token: 0x04000DF6 RID: 3574
	public static Image imgMPLost;

	// Token: 0x04000DF7 RID: 3575
	public static Image imgHP_tm_do;

	// Token: 0x04000DF8 RID: 3576
	public static Image imgHP_tm_vang;

	// Token: 0x04000DF9 RID: 3577
	public static Image imgHP_tm_xam;

	// Token: 0x04000DFA RID: 3578
	public static Image imgHP_tm_xanh;

	// Token: 0x04000DFB RID: 3579
	public Mob mobCapcha;

	// Token: 0x04000DFC RID: 3580
	public MagicTree magicTree;

	// Token: 0x04000DFD RID: 3581
	private short l;

	// Token: 0x04000DFE RID: 3582
	public static int countEff;

	// Token: 0x04000DFF RID: 3583
	public static GamePad gamePad = new GamePad();

	// Token: 0x04000E00 RID: 3584
	public static Image imgChatPC;

	// Token: 0x04000E01 RID: 3585
	public static Image imgChatsPC2;

	// Token: 0x04000E02 RID: 3586
	public static int isAnalog = 0;

	// Token: 0x04000E03 RID: 3587
	public static Image img_ct_bar_0 = mSystem.loadImage("/mainImage/i_pve_bar_0.png");

	// Token: 0x04000E04 RID: 3588
	public static Image img_ct_bar_1 = mSystem.loadImage("/mainImage/i_pve_bar_1.png");

	// Token: 0x04000E05 RID: 3589
	public static bool isUseTouch;

	// Token: 0x04000E06 RID: 3590
	public Command cmdDoiCo;

	// Token: 0x04000E07 RID: 3591
	public Command cmdLogOut;

	// Token: 0x04000E08 RID: 3592
	public Command cmdChatTheGioi;

	// Token: 0x04000E09 RID: 3593
	public Command cmdshowInfo;

	// Token: 0x04000E0A RID: 3594
	private static Command[] cmdTestLogin = null;

	// Token: 0x04000E0B RID: 3595
	public const int numSkill = 10;

	// Token: 0x04000E0C RID: 3596
	public const int numSkill_2 = 5;

	// Token: 0x04000E0D RID: 3597
	public static Skill[] keySkill = new Skill[10];

	// Token: 0x04000E0E RID: 3598
	public static Skill[] onScreenSkill = new Skill[10];

	// Token: 0x04000E0F RID: 3599
	public Command cmdMenu;

	// Token: 0x04000E10 RID: 3600
	public static int firstY;

	// Token: 0x04000E11 RID: 3601
	public static int wSkill;

	// Token: 0x04000E12 RID: 3602
	public static long deltaTime;

	// Token: 0x04000E13 RID: 3603
	public bool isPointerDowning;

	// Token: 0x04000E14 RID: 3604
	public bool isChangingCameraMode;

	// Token: 0x04000E15 RID: 3605
	private int ptLastDownX;

	// Token: 0x04000E16 RID: 3606
	private int ptLastDownY;

	// Token: 0x04000E17 RID: 3607
	private int ptFirstDownX;

	// Token: 0x04000E18 RID: 3608
	private int ptFirstDownY;

	// Token: 0x04000E19 RID: 3609
	private int ptDownTime;

	// Token: 0x04000E1A RID: 3610
	private bool disableSingleClick;

	// Token: 0x04000E1B RID: 3611
	public long lastSingleClick;

	// Token: 0x04000E1C RID: 3612
	public bool clickMoving;

	// Token: 0x04000E1D RID: 3613
	public bool clickOnTileTop;

	// Token: 0x04000E1E RID: 3614
	public bool clickMovingRed;

	// Token: 0x04000E1F RID: 3615
	private int clickToX;

	// Token: 0x04000E20 RID: 3616
	private int clickToY;

	// Token: 0x04000E21 RID: 3617
	private int lastClickCMX;

	// Token: 0x04000E22 RID: 3618
	private int lastClickCMY;

	// Token: 0x04000E23 RID: 3619
	private int clickMovingP1;

	// Token: 0x04000E24 RID: 3620
	private int clickMovingTimeOut;

	// Token: 0x04000E25 RID: 3621
	private long lastMove;

	// Token: 0x04000E26 RID: 3622
	public static bool isNewClanMessage;

	// Token: 0x04000E27 RID: 3623
	private long lastFire;

	// Token: 0x04000E28 RID: 3624
	private long lastUsePotion;

	// Token: 0x04000E29 RID: 3625
	public int auto;

	// Token: 0x04000E2A RID: 3626
	public int dem;

	// Token: 0x04000E2B RID: 3627
	private string strTam = string.Empty;

	// Token: 0x04000E2C RID: 3628
	private int a;

	// Token: 0x04000E2D RID: 3629
	public bool isFreez;

	// Token: 0x04000E2E RID: 3630
	public bool isUseFreez;

	// Token: 0x04000E2F RID: 3631
	public static Image imgTrans;

	// Token: 0x04000E30 RID: 3632
	public bool isRongThanXuatHien;

	// Token: 0x04000E31 RID: 3633
	public bool isRongNamek;

	// Token: 0x04000E32 RID: 3634
	public bool isSuperPower;

	// Token: 0x04000E33 RID: 3635
	public int tPower;

	// Token: 0x04000E34 RID: 3636
	public int xPower;

	// Token: 0x04000E35 RID: 3637
	public int yPower;

	// Token: 0x04000E36 RID: 3638
	public int dxPower;

	// Token: 0x04000E37 RID: 3639
	public bool activeRongThan;

	// Token: 0x04000E38 RID: 3640
	public bool isMeCallRongThan;

	// Token: 0x04000E39 RID: 3641
	public int mautroi;

	// Token: 0x04000E3A RID: 3642
	public int mapRID;

	// Token: 0x04000E3B RID: 3643
	public int zoneRID;

	// Token: 0x04000E3C RID: 3644
	public int bgRID = -1;

	// Token: 0x04000E3D RID: 3645
	public static int tam = 0;

	// Token: 0x04000E3E RID: 3646
	public static bool isAutoPlay;

	// Token: 0x04000E3F RID: 3647
	public static bool canAutoPlay;

	// Token: 0x04000E40 RID: 3648
	public static bool isChangeZone;

	// Token: 0x04000E41 RID: 3649
	private int timeSkill;

	// Token: 0x04000E42 RID: 3650
	private int nSkill;

	// Token: 0x04000E43 RID: 3651
	private int selectedIndexSkill = -1;

	// Token: 0x04000E44 RID: 3652
	private Skill lastSkill;

	// Token: 0x04000E45 RID: 3653
	private bool doSeleckSkillFlag;

	// Token: 0x04000E46 RID: 3654
	public string strCapcha;

	// Token: 0x04000E47 RID: 3655
	private long longPress;

	// Token: 0x04000E48 RID: 3656
	private int move;

	// Token: 0x04000E49 RID: 3657
	public bool flareFindFocus;

	// Token: 0x04000E4A RID: 3658
	private int flareTime;

	// Token: 0x04000E4B RID: 3659
	public int keyTouchSkill = -1;

	// Token: 0x04000E4C RID: 3660
	private long lastSendUpdatePostion;

	// Token: 0x04000E4D RID: 3661
	public static long lastTick;

	// Token: 0x04000E4E RID: 3662
	public static long currTick;

	// Token: 0x04000E4F RID: 3663
	private int timeAuto;

	// Token: 0x04000E50 RID: 3664
	public static long lastXS;

	// Token: 0x04000E51 RID: 3665
	public static long currXS;

	// Token: 0x04000E52 RID: 3666
	public static int secondXS;

	// Token: 0x04000E53 RID: 3667
	public int runArrow;

	// Token: 0x04000E54 RID: 3668
	public static int isPaintRada;

	// Token: 0x04000E55 RID: 3669
	public static Image imgNut;

	// Token: 0x04000E56 RID: 3670
	public static Image imgNutF;

	// Token: 0x04000E57 RID: 3671
	public int[] keyCapcha;

	// Token: 0x04000E58 RID: 3672
	public static Image imgCapcha;

	// Token: 0x04000E59 RID: 3673
	public string keyInput;

	// Token: 0x04000E5A RID: 3674
	public static int disXC;

	// Token: 0x04000E5B RID: 3675
	public static bool isPaint = true;

	// Token: 0x04000E5C RID: 3676
	public static int shock_scr;

	// Token: 0x04000E5D RID: 3677
	private static int[] shock_x = new int[]
	{
		1,
		-1,
		1,
		-1
	};

	// Token: 0x04000E5E RID: 3678
	private static int[] shock_y = new int[]
	{
		1,
		-1,
		-1,
		1
	};

	// Token: 0x04000E5F RID: 3679
	private int tDoubleDelay;

	// Token: 0x04000E60 RID: 3680
	public static Image arrow;

	// Token: 0x04000E61 RID: 3681
	private static int yTouchBar;

	// Token: 0x04000E62 RID: 3682
	private static int xC;

	// Token: 0x04000E63 RID: 3683
	private static int yC;

	// Token: 0x04000E64 RID: 3684
	private static int xL;

	// Token: 0x04000E65 RID: 3685
	private static int yL;

	// Token: 0x04000E66 RID: 3686
	public int xR;

	// Token: 0x04000E67 RID: 3687
	public int yR;

	// Token: 0x04000E68 RID: 3688
	private static int xU;

	// Token: 0x04000E69 RID: 3689
	private static int yU;

	// Token: 0x04000E6A RID: 3690
	private static int xF;

	// Token: 0x04000E6B RID: 3691
	private static int yF;

	// Token: 0x04000E6C RID: 3692
	public static int xHP;

	// Token: 0x04000E6D RID: 3693
	public static int yHP;

	// Token: 0x04000E6E RID: 3694
	private static int xTG;

	// Token: 0x04000E6F RID: 3695
	private static int yTG;

	// Token: 0x04000E70 RID: 3696
	public static int[] xS;

	// Token: 0x04000E71 RID: 3697
	public static int[] yS;

	// Token: 0x04000E72 RID: 3698
	public static int xSkill;

	// Token: 0x04000E73 RID: 3699
	public static int ySkill;

	// Token: 0x04000E74 RID: 3700
	public static int padSkill;

	// Token: 0x04000E75 RID: 3701
	public long dMP;

	// Token: 0x04000E76 RID: 3702
	public long twMp;

	// Token: 0x04000E77 RID: 3703
	public bool isInjureMp;

	// Token: 0x04000E78 RID: 3704
	public long dHP;

	// Token: 0x04000E79 RID: 3705
	public long twHp;

	// Token: 0x04000E7A RID: 3706
	public bool isInjureHp;

	// Token: 0x04000E7B RID: 3707
	private long curr;

	// Token: 0x04000E7C RID: 3708
	private long last;

	// Token: 0x04000E7D RID: 3709
	private int secondVS;

	// Token: 0x04000E7E RID: 3710
	private int[] idVS = new int[]
	{
		-1,
		-1
	};

	// Token: 0x04000E7F RID: 3711
	public static string[] flyTextString;

	// Token: 0x04000E80 RID: 3712
	public static int[] flyTextX;

	// Token: 0x04000E81 RID: 3713
	public static int[] flyTextY;

	// Token: 0x04000E82 RID: 3714
	public static int[] flyTextYTo;

	// Token: 0x04000E83 RID: 3715
	public static int[] flyTextDx;

	// Token: 0x04000E84 RID: 3716
	public static int[] flyTextDy;

	// Token: 0x04000E85 RID: 3717
	public static int[] flyTextState;

	// Token: 0x04000E86 RID: 3718
	public static int[] flyTextColor;

	// Token: 0x04000E87 RID: 3719
	public static int[] flyTime;

	// Token: 0x04000E88 RID: 3720
	public static int[] splashX;

	// Token: 0x04000E89 RID: 3721
	public static int[] splashY;

	// Token: 0x04000E8A RID: 3722
	public static int[] splashState;

	// Token: 0x04000E8B RID: 3723
	public static int[] splashF;

	// Token: 0x04000E8C RID: 3724
	public static int[] splashDir;

	// Token: 0x04000E8D RID: 3725
	public static Image[] imgSplash;

	// Token: 0x04000E8E RID: 3726
	public static int cmdBarX;

	// Token: 0x04000E8F RID: 3727
	public static int cmdBarY;

	// Token: 0x04000E90 RID: 3728
	public static int cmdBarW;

	// Token: 0x04000E91 RID: 3729
	public static int cmdBarLeftW;

	// Token: 0x04000E92 RID: 3730
	public static int cmdBarRightW;

	// Token: 0x04000E93 RID: 3731
	public static int cmdBarCenterW;

	// Token: 0x04000E94 RID: 3732
	public static int hpBarX;

	// Token: 0x04000E95 RID: 3733
	public static int hpBarY;

	// Token: 0x04000E96 RID: 3734
	public static int spBarW;

	// Token: 0x04000E97 RID: 3735
	public static int mpBarW;

	// Token: 0x04000E98 RID: 3736
	public static int expBarW;

	// Token: 0x04000E99 RID: 3737
	public static int lvPosX;

	// Token: 0x04000E9A RID: 3738
	public static int moneyPosX;

	// Token: 0x04000E9B RID: 3739
	public static int hpBarH;

	// Token: 0x04000E9C RID: 3740
	public static int girlHPBarY;

	// Token: 0x04000E9D RID: 3741
	public static long hpBarW;

	// Token: 0x04000E9E RID: 3742
	public static Image[] imgCmdBar;

	// Token: 0x04000E9F RID: 3743
	private int imgScrW;

	// Token: 0x04000EA0 RID: 3744
	public static int popupY;

	// Token: 0x04000EA1 RID: 3745
	public static int popupX;

	// Token: 0x04000EA2 RID: 3746
	public static int isborderIndex;

	// Token: 0x04000EA3 RID: 3747
	public static int isselectedRow;

	// Token: 0x04000EA4 RID: 3748
	private static Image imgNolearn;

	// Token: 0x04000EA5 RID: 3749
	public int cmxp;

	// Token: 0x04000EA6 RID: 3750
	public int cmvxp;

	// Token: 0x04000EA7 RID: 3751
	public int cmdxp;

	// Token: 0x04000EA8 RID: 3752
	public int cmxLimp;

	// Token: 0x04000EA9 RID: 3753
	public int cmyLimp;

	// Token: 0x04000EAA RID: 3754
	public int cmyp;

	// Token: 0x04000EAB RID: 3755
	public int cmvyp;

	// Token: 0x04000EAC RID: 3756
	public int cmdyp;

	// Token: 0x04000EAD RID: 3757
	private int indexTiemNang;

	// Token: 0x04000EAE RID: 3758
	private string alertURL;

	// Token: 0x04000EAF RID: 3759
	private string fnick;

	// Token: 0x04000EB0 RID: 3760
	public static int xstart;

	// Token: 0x04000EB1 RID: 3761
	public static int ystart;

	// Token: 0x04000EB2 RID: 3762
	public static int popupW = 140;

	// Token: 0x04000EB3 RID: 3763
	public static int popupH = 160;

	// Token: 0x04000EB4 RID: 3764
	public static int cmySK;

	// Token: 0x04000EB5 RID: 3765
	public static int cmtoYSK;

	// Token: 0x04000EB6 RID: 3766
	public static int cmdySK;

	// Token: 0x04000EB7 RID: 3767
	public static int cmvySK;

	// Token: 0x04000EB8 RID: 3768
	public static int cmyLimSK;

	// Token: 0x04000EB9 RID: 3769
	public static int columns = 6;

	// Token: 0x04000EBA RID: 3770
	public static int rows;

	// Token: 0x04000EBB RID: 3771
	private int totalRowInfo;

	// Token: 0x04000EBC RID: 3772
	private int ypaintKill;

	// Token: 0x04000EBD RID: 3773
	private int ylimUp;

	// Token: 0x04000EBE RID: 3774
	private int ylimDow;

	// Token: 0x04000EBF RID: 3775
	private int yPaint;

	// Token: 0x04000EC0 RID: 3776
	public static int indexEff = 0;

	// Token: 0x04000EC1 RID: 3777
	public static EffectCharPaint effUpok;

	// Token: 0x04000EC2 RID: 3778
	public static int inforX;

	// Token: 0x04000EC3 RID: 3779
	public static int inforY;

	// Token: 0x04000EC4 RID: 3780
	public static int inforW;

	// Token: 0x04000EC5 RID: 3781
	public static int inforH;

	// Token: 0x04000EC6 RID: 3782
	public Command cmdDead;

	// Token: 0x04000EC7 RID: 3783
	public static bool notPaint = false;

	// Token: 0x04000EC8 RID: 3784
	public static bool isPing = false;

	// Token: 0x04000EC9 RID: 3785
	public static int INFO = 0;

	// Token: 0x04000ECA RID: 3786
	public static int STORE = 1;

	// Token: 0x04000ECB RID: 3787
	public static int ZONE = 2;

	// Token: 0x04000ECC RID: 3788
	public static int UPGRADE = 3;

	// Token: 0x04000ECD RID: 3789
	private int Hitem = 30;

	// Token: 0x04000ECE RID: 3790
	private int maxSizeRow = 5;

	// Token: 0x04000ECF RID: 3791
	private int isTranKyNang;

	// Token: 0x04000ED0 RID: 3792
	private bool isTran;

	// Token: 0x04000ED1 RID: 3793
	private int cmY_Old;

	// Token: 0x04000ED2 RID: 3794
	private int cmX_Old;

	// Token: 0x04000ED3 RID: 3795
	public PopUpYesNo popUpYesNo;

	// Token: 0x04000ED4 RID: 3796
	public static MyVector vChatVip = new MyVector();

	// Token: 0x04000ED5 RID: 3797
	public static int vBig;

	// Token: 0x04000ED6 RID: 3798
	public bool isFireWorks;

	// Token: 0x04000ED7 RID: 3799
	public int[] winnumber;

	// Token: 0x04000ED8 RID: 3800
	public int[] randomNumber;

	// Token: 0x04000ED9 RID: 3801
	public int[] tMove;

	// Token: 0x04000EDA RID: 3802
	public int[] moveCount;

	// Token: 0x04000EDB RID: 3803
	public int[] delayMove;

	// Token: 0x04000EDC RID: 3804
	public int moveIndex;

	// Token: 0x04000EDD RID: 3805
	private bool isWin;

	// Token: 0x04000EDE RID: 3806
	private string strFinish;

	// Token: 0x04000EDF RID: 3807
	private int tShow;

	// Token: 0x04000EE0 RID: 3808
	private int xChatVip;

	// Token: 0x04000EE1 RID: 3809
	private int currChatWidth;

	// Token: 0x04000EE2 RID: 3810
	private bool startChat;

	// Token: 0x04000EE3 RID: 3811
	public sbyte percentMabu;

	// Token: 0x04000EE4 RID: 3812
	public bool mabuEff;

	// Token: 0x04000EE5 RID: 3813
	public int tMabuEff;

	// Token: 0x04000EE6 RID: 3814
	public static bool isPaintChatVip;

	// Token: 0x04000EE7 RID: 3815
	public static sbyte mabuPercent;

	// Token: 0x04000EE8 RID: 3816
	public static sbyte isNewMember;

	// Token: 0x04000EE9 RID: 3817
	private string yourNumber = string.Empty;

	// Token: 0x04000EEA RID: 3818
	private string[] strPaint;

	// Token: 0x04000EEB RID: 3819
	public static Image imgHP_NEW;

	// Token: 0x04000EEC RID: 3820
	public static InfoPhuBan phuban_Info;

	// Token: 0x04000EED RID: 3821
	public static FrameImage fra_PVE_Bar_0;

	// Token: 0x04000EEE RID: 3822
	public static FrameImage fra_PVE_Bar_1;

	// Token: 0x04000EEF RID: 3823
	public static Image imgVS;

	// Token: 0x04000EF0 RID: 3824
	public static Image imgBall;

	// Token: 0x04000EF1 RID: 3825
	public static Image imgKhung;

	// Token: 0x04000EF2 RID: 3826
	public int countFrameSkill;

	// Token: 0x04000EF3 RID: 3827
	public static Image imgBgIOS;

	// Token: 0x04000EF4 RID: 3828
	public static int nCT_TeamB = 50;

	// Token: 0x04000EF5 RID: 3829
	public static int nCT_TeamA = 50;

	// Token: 0x04000EF6 RID: 3830
	public static long nCT_timeBallte;

	// Token: 0x04000EF7 RID: 3831
	public static string nCT_team;

	// Token: 0x04000EF8 RID: 3832
	public static int nCT_nBoyBaller = 100;

	// Token: 0x04000EF9 RID: 3833
	public static bool isPaint_CT;

	// Token: 0x04000EFA RID: 3834
	public static sbyte nCT_floor;

	// Token: 0x04000EFB RID: 3835
	public static bool is_Paint_boardCT_Expand;

	// Token: 0x04000EFC RID: 3836
	private static int xRect;

	// Token: 0x04000EFD RID: 3837
	private static int yRect;

	// Token: 0x04000EFE RID: 3838
	private static int wRect;

	// Token: 0x04000EFF RID: 3839
	private static int hRect;

	// Token: 0x04000F00 RID: 3840
	public static MyVector res_CT = new MyVector();

	// Token: 0x04000F01 RID: 3841
	public static int nTop = 1;

	// Token: 0x04000F02 RID: 3842
	public static bool isPickNgocRong = false;

	// Token: 0x04000F03 RID: 3843
	public static int nUSER_CT;

	// Token: 0x04000F04 RID: 3844
	public static int nUSER_MAX_CT;

	// Token: 0x04000F05 RID: 3845
	public static bool isudungCapsun;

	// Token: 0x04000F06 RID: 3846
	public static bool isudungCapsun4;

	// Token: 0x04000F07 RID: 3847
	public static bool isudungCapsun3;
}
