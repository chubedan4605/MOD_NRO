using System;
using Assets.src.e;
using Assets.src.g;
using UnityEngine;

// Token: 0x020000A1 RID: 161
public class Char : IMapObject
{
	// Token: 0x06000619 RID: 1561 RVA: 0x0004D02C File Offset: 0x0004B22C
	public Char()
	{
		this.statusMe = 6;
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x0004D290 File Offset: 0x0004B490
	public void applyCharLevelPercent()
	{
		try
		{
			long num = 1L;
			long num2 = 0L;
			int num3 = 0;
			for (int i = GameScr.exps.Length - 1; i >= 0; i--)
			{
				if (this.cPower >= GameScr.exps[i])
				{
					if (i == GameScr.exps.Length - 1)
					{
						num = 1L;
					}
					else
					{
						num = GameScr.exps[i + 1] - GameScr.exps[i];
					}
					num2 = this.cPower - GameScr.exps[i];
					num3 = i;
					break;
				}
			}
			this.clevel = num3;
			this.cLevelPercent = (long)((int)(num2 * 10000L / num));
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi char level percent: " + ex.ToString());
		}
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x00007051 File Offset: 0x00005251
	public int getdxSkill()
	{
		if (this.myskill != null)
		{
			return this.myskill.dx;
		}
		return 0;
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x0000706B File Offset: 0x0000526B
	public int getdySkill()
	{
		if (this.myskill != null)
		{
			return this.myskill.dy;
		}
		return 0;
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x0004D35C File Offset: 0x0004B55C
	public static void taskAction(bool isNextStep)
	{
		Task task = global::Char.myCharz().taskMaint;
		if (task.index > task.contentInfo.Length - 1)
		{
			task.index = task.contentInfo.Length - 1;
		}
		string text = task.contentInfo[task.index];
		if (text != null && !text.Equals(string.Empty))
		{
			if (text.StartsWith("#"))
			{
				text = NinjaUtil.replace(text, "#", string.Empty);
				Npc npc = new Npc(5, 0, -100, -100, 5, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
				npc.cx = (npc.cy = -100);
				npc.avatar = GameScr.info1.charId[global::Char.myCharz().cgender][2];
				npc.charID = 5;
				if (GameCanvas.currentScreen == GameScr.instance)
				{
					ChatPopup.addNextPopUpMultiLine(text, npc);
				}
			}
			else if (isNextStep)
			{
				GameScr.info1.addInfo(text, 0);
			}
		}
		GameScr.isHaveSelectSkill = true;
		Cout.println("TASKx " + global::Char.myCharz().taskMaint.taskId);
		if (global::Char.myCharz().taskMaint.taskId <= 2)
		{
			global::Char.myCharz().canFly = false;
		}
		else
		{
			global::Char.myCharz().canFly = true;
		}
		GameScr.gI().left = null;
		if (task.taskId == 0)
		{
			Hint.isViewMap = false;
			Hint.isViewPotential = false;
			GameScr.gI().right = null;
			GameScr.isHaveSelectSkill = false;
			GameScr.gI().left = null;
			if (task.index < 4)
			{
				MagicTree.isPaint = false;
				GameScr.isPaintRada = -1;
			}
			if (task.index == 4)
			{
				GameScr.isPaintRada = 1;
				MagicTree.isPaint = true;
			}
			if (task.index >= 5)
			{
				GameScr.gI().right = GameScr.gI().cmdFocus;
			}
		}
		if (task.taskId == 1)
		{
			GameScr.isHaveSelectSkill = true;
		}
		if (task.taskId >= 1)
		{
			GameScr.gI().right = GameScr.gI().cmdFocus;
			GameScr.gI().left = GameScr.gI().cmdMenu;
		}
		if (task.taskId >= 0)
		{
			Panel.isPaintMap = true;
		}
		else
		{
			Panel.isPaintMap = false;
		}
		if (task.taskId < 12)
		{
			GameCanvas.panel.mainTabName = mResources.mainTab1;
		}
		else
		{
			GameCanvas.panel.mainTabName = mResources.mainTab2;
		}
		GameCanvas.panel.tabName[0] = GameCanvas.panel.mainTabName;
		if (global::Char.myChar.taskMaint.taskId > 10)
		{
			Rms.saveRMSString("fake", "aa");
		}
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x0004D61C File Offset: 0x0004B81C
	public string getStrLevel()
	{
		string text = string.Concat(new object[]
		{
			this.strLevel[this.clevel],
			"+",
			this.cLevelPercent / 100L,
			".",
			this.cLevelPercent % 100L,
			"%"
		});
		if (text.Length > 23 && text.IndexOf("cấp ") >= 0)
		{
			text = Res.replace(text, "cấp ", "c");
		}
		return text;
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x00007085 File Offset: 0x00005285
	public int avatarz()
	{
		return this.getAvatar(this.head);
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x0004D6B4 File Offset: 0x0004B8B4
	public int getAvatar(int headId)
	{
		for (int i = 0; i < global::Char.idHead.Length; i++)
		{
			if (headId == (int)global::Char.idHead[i])
			{
				return (int)global::Char.idAvatar[i];
			}
		}
		return -1;
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x0004D6F0 File Offset: 0x0004B8F0
	public void setPowerInfo(string info, short p, short maxP, short sc)
	{
		this.powerPoint = p;
		this.strInfo = info;
		this.maxPowerPoint = maxP;
		this.secondPower = sc;
		this.lastS = (this.currS = mSystem.currentTimeMillis());
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x0004D730 File Offset: 0x0004B930
	public void addInfo(string info)
	{
		if (info != null && this.charID == -global::Char.myCharz().charID)
		{
			string lower = info.ToLower();
			if (lower.Contains("đánh") || lower.Contains("giúp") || lower.Contains("cứu") || lower.Contains("sư phụ") || lower.Contains("tiếp đi"))
			{
				Mod.DungPham.KoiOctiiu957.AutoSkill.isPetAskedForUseSkill = true;
			}
		}
		if (this.chatInfo == null)
		{
			this.chatInfo = new Info();
		}
		global::Char cInfo = null;
		this.chatInfo.addInfo(info, 0, cInfo, false);
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0004D764 File Offset: 0x0004B964
	public int getSys()
	{
		if (this.nClass.classId == 1 || this.nClass.classId == 2)
		{
			return 1;
		}
		if (this.nClass.classId == 3 || this.nClass.classId == 4)
		{
			return 2;
		}
		if (this.nClass.classId == 5 || this.nClass.classId == 6)
		{
			return 3;
		}
		return 0;
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x00007093 File Offset: 0x00005293
	public static global::Char myCharz()
	{
		if (global::Char.myChar == null)
		{
			global::Char.myChar = new global::Char();
			global::Char.myChar.me = true;
			global::Char.myChar.cmtoChar = true;
		}
		return global::Char.myChar;
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x000070C4 File Offset: 0x000052C4
	public static global::Char myPetz()
	{
		if (global::Char.myPet == null)
		{
			global::Char.myPet = new global::Char();
			global::Char.myPet.me = false;
		}
		return global::Char.myPet;
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x000070EA File Offset: 0x000052EA
	public static void clearMyChar()
	{
		global::Char.myChar = null;
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x0004D7E0 File Offset: 0x0004B9E0
	public void bagSort()
	{
		try
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < this.arrItemBag.Length; i++)
			{
				Item item = this.arrItemBag[i];
				if (item != null && item.template.isUpToUp && !item.isExpires)
				{
					myVector.addElement(item);
				}
			}
			for (int j = 0; j < myVector.size(); j++)
			{
				Item item2 = (Item)myVector.elementAt(j);
				if (item2 != null)
				{
					for (int k = j + 1; k < myVector.size(); k++)
					{
						Item item3 = (Item)myVector.elementAt(k);
						if (item3 != null && item2.template.Equals(item3.template) && item2.isLock == item3.isLock)
						{
							item2.quantity += item3.quantity;
							this.arrItemBag[item3.indexUI] = null;
							myVector.setElementAt(null, k);
						}
					}
				}
			}
			for (int l = 0; l < this.arrItemBag.Length; l++)
			{
				if (this.arrItemBag[l] != null)
				{
					for (int m = 0; m <= l; m++)
					{
						if (this.arrItemBag[m] == null)
						{
							this.arrItemBag[m] = this.arrItemBag[l];
							this.arrItemBag[m].indexUI = m;
							this.arrItemBag[l] = null;
							break;
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Cout.println("Char.bagSort()");
		}
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x0004D9A8 File Offset: 0x0004BBA8
	public void boxSort()
	{
		try
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < this.arrItemBox.Length; i++)
			{
				Item item = this.arrItemBox[i];
				if (item != null && item.template.isUpToUp && !item.isExpires)
				{
					myVector.addElement(item);
				}
			}
			for (int j = 0; j < myVector.size(); j++)
			{
				Item item2 = (Item)myVector.elementAt(j);
				if (item2 != null)
				{
					for (int k = j + 1; k < myVector.size(); k++)
					{
						Item item3 = (Item)myVector.elementAt(k);
						if (item3 != null && item2.template.Equals(item3.template) && item2.isLock == item3.isLock)
						{
							item2.quantity += item3.quantity;
							this.arrItemBox[item3.indexUI] = null;
							myVector.setElementAt(null, k);
						}
					}
				}
			}
			for (int l = 0; l < this.arrItemBox.Length; l++)
			{
				if (this.arrItemBox[l] != null)
				{
					for (int m = 0; m <= l; m++)
					{
						if (this.arrItemBox[m] == null)
						{
							this.arrItemBox[m] = this.arrItemBox[l];
							this.arrItemBox[m].indexUI = m;
							this.arrItemBox[l] = null;
							break;
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Cout.println("Char.boxSort()");
		}
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x0004DB70 File Offset: 0x0004BD70
	public void useItem(int indexUI)
	{
		Item item = this.arrItemBag[indexUI];
		if (item.isTypeBody())
		{
			item.isLock = true;
			item.typeUI = 5;
			Item item2 = this.arrItemBody[(int)item.template.type];
			this.arrItemBag[indexUI] = null;
			if (item2 != null)
			{
				item2.typeUI = 3;
				this.arrItemBody[(int)item.template.type] = null;
				item2.indexUI = indexUI;
				this.arrItemBag[indexUI] = item2;
			}
			item.indexUI = (int)item.template.type;
			this.arrItemBody[item.indexUI] = item;
			for (int i = 0; i < this.arrItemBody.Length; i++)
			{
				Item item3 = this.arrItemBody[i];
				if (item3 != null)
				{
					if ((int)item3.template.type == 0)
					{
						this.body = (int)item3.template.part;
					}
					else if ((int)item3.template.type == 1)
					{
						this.leg = (int)item3.template.part;
					}
				}
			}
		}
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x0004DC80 File Offset: 0x0004BE80
	public Skill getSkill(SkillTemplate skillTemplate)
	{
		for (int i = 0; i < this.vSkill.size(); i++)
		{
			if ((int)((Skill)this.vSkill.elementAt(i)).template.id == (int)skillTemplate.id)
			{
				return (Skill)this.vSkill.elementAt(i);
			}
		}
		return null;
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x0004DCE4 File Offset: 0x0004BEE4
	public Waypoint isInEnterOfflinePoint()
	{
		Task task = global::Char.myChar.taskMaint;
		if (task != null && task.taskId == 0 && task.index < 6)
		{
			return null;
		}
		int num = TileMap.vGo.size();
		sbyte b = 0;
		while ((int)b < num)
		{
			Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
			if (PopUp.vPopups.size() >= num)
			{
				PopUp popUp = (PopUp)PopUp.vPopups.elementAt((int)b);
				if (!popUp.isPaint)
				{
					return null;
				}
			}
			if (this.cx >= (int)waypoint.minX && this.cx <= (int)waypoint.maxX && this.cy >= (int)waypoint.minY && this.cy <= (int)waypoint.maxY && waypoint.isEnter && waypoint.isOffline)
			{
				return waypoint;
			}
			b = (sbyte)((int)b + 1);
		}
		return null;
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x0004DDDC File Offset: 0x0004BFDC
	public Waypoint isInEnterOnlinePoint()
	{
		Task task = global::Char.myChar.taskMaint;
		if (task != null && task.taskId == 0 && task.index < 6)
		{
			return null;
		}
		int num = TileMap.vGo.size();
		sbyte b = 0;
		while ((int)b < num)
		{
			Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
			if (PopUp.vPopups.size() >= num)
			{
				PopUp popUp = (PopUp)PopUp.vPopups.elementAt((int)b);
				if (!popUp.isPaint)
				{
					return null;
				}
			}
			if (this.cx >= (int)waypoint.minX && this.cx <= (int)waypoint.maxX && this.cy >= (int)waypoint.minY && this.cy <= (int)waypoint.maxY && waypoint.isEnter && !waypoint.isOffline)
			{
				return waypoint;
			}
			b = (sbyte)((int)b + 1);
		}
		return null;
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x0004DED4 File Offset: 0x0004C0D4
	public bool isInWaypoint()
	{
		if (TileMap.isInAirMap() && this.cy >= TileMap.pxh - 48)
		{
			return true;
		}
		if (this.isTeleport || this.isUsePlane)
		{
			return false;
		}
		int num = TileMap.vGo.size();
		sbyte b = 0;
		while ((int)b < num)
		{
			Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
			if ((TileMap.mapID == 47 || TileMap.isInAirMap()) && this.cy <= (int)(waypoint.minY + waypoint.maxY) && this.cx > (int)waypoint.minX && this.cx < (int)waypoint.maxX)
			{
				return !TileMap.isInAirMap() || (int)this.cTypePk == 0;
			}
			if (this.cx >= (int)waypoint.minX && this.cx <= (int)waypoint.maxX && this.cy >= (int)waypoint.minY && this.cy <= (int)waypoint.maxY && !waypoint.isEnter)
			{
				return true;
			}
			b = (sbyte)((int)b + 1);
		}
		return false;
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0004E00C File Offset: 0x0004C20C
	public bool isPunchKickSkill()
	{
		return this.skillPaint != null && ((this.skillPaint.id >= 0 && this.skillPaint.id <= 6) || (this.skillPaint.id >= 14 && this.skillPaint.id <= 20) || (this.skillPaint.id >= 28 && this.skillPaint.id <= 34) || (this.skillPaint.id >= 63 && this.skillPaint.id <= 69));
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0004E0C0 File Offset: 0x0004C2C0
	public void soundUpdate()
	{
		if (this.me && this.statusMe == 10 && this.cf == 8 && this.ty > 20 && GameCanvas.gameTick % 20 == 0)
		{
			SoundMn.gI().charFly();
		}
		if (this.skillPaint != null && this.skillInfoPaint() != null && this.indexSkill < this.skillInfoPaint().Length && this.isPunchKickSkill() && (this.me || (!this.me && this.cx >= GameScr.cmx && this.cx <= GameScr.cmx + GameCanvas.w)) && GameCanvas.gameTick % 5 == 0)
		{
			if (this.cf == 9 || this.cf == 10 || this.cf == 11)
			{
				SoundMn.gI().charPunch(true, (!this.me) ? 0.05f : 0.1f);
			}
			else
			{
				SoundMn.gI().charPunch(false, (!this.me) ? 0.05f : 0.1f);
			}
		}
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x000045ED File Offset: 0x000027ED
	public void updateChargeSkill()
	{
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x0004E208 File Offset: 0x0004C408
	public virtual void update()
	{
		if (this.isMafuba)
		{
			this.cf = 23;
			this.countMafuba += 1;
			if (this.countMafuba > 150)
			{
				this.isMafuba = false;
			}
			return;
		}
		this.countMafuba = 0;
		if (this.isHide)
		{
			return;
		}
		if (this.isMabuHold)
		{
			return;
		}
		if ((this.isCopy || this.clevel >= 14) && this.statusMe != 1)
		{
			int num = this.statusMe;
		}
		if (this.petFollow != null)
		{
			if (GameCanvas.gameTick % 3 == 0)
			{
				if (global::Char.myCharz().cdir == 1)
				{
					this.petFollow.cmtoX = this.cx - 20;
				}
				if (global::Char.myCharz().cdir == -1)
				{
					this.petFollow.cmtoX = this.cx + 20;
				}
				this.petFollow.cmtoY = this.cy - 40;
				if (this.petFollow.cmx > this.cx)
				{
					this.petFollow.dir = -1;
				}
				else
				{
					this.petFollow.dir = 1;
				}
				if (this.petFollow.cmtoX < 100)
				{
					this.petFollow.cmtoX = 100;
				}
				if (this.petFollow.cmtoX > TileMap.pxw - 100)
				{
					this.petFollow.cmtoX = TileMap.pxw - 100;
				}
			}
			this.petFollow.update();
		}
		if (!this.me && this.cHP <= 0L && this.clanID != -100 && this.statusMe != 14 && this.statusMe != 5)
		{
			this.startDie((short)this.cx, (short)this.cy);
		}
		if (this.isInjureHp)
		{
			this.twHp++;
			if (this.twHp == 20)
			{
				this.twHp = 0;
				this.isInjureHp = false;
			}
		}
		else if (this.dHP > this.cHP)
		{
			long num2 = this.dHP - this.cHP >> 1;
			if (num2 < 1L)
			{
				num2 = 1L;
			}
			this.dHP -= num2;
		}
		else
		{
			this.dHP = this.cHP;
		}
		if (this.secondPower != 0)
		{
			this.currS = mSystem.currentTimeMillis();
			if (this.currS - this.lastS >= 1000L)
			{
				this.lastS = mSystem.currentTimeMillis();
				this.secondPower -= 1;
			}
		}
		if (this.isPaintNewSkill)
		{
			if (GameCanvas.timeNow > this.timeReset_newSkill || this.statusMe == 14 || this.statusMe == 5)
			{
				this.timeReset_newSkill = 0L;
				this.isPaintNewSkill = false;
			}
			this.UpdSkillPaint_NEW();
			if (this.isShadown)
			{
				this.updateShadown();
			}
			return;
		}
		if (!this.me && GameScr.notPaint)
		{
			return;
		}
		if (this.sleepEff && GameCanvas.gameTick % 10 == 0)
		{
			EffecMn.addEff(new Effect(41, this.cx, this.cy, 3, 1, 1));
		}
		if (this.huytSao)
		{
			this.huytSao = false;
			EffecMn.addEff(new Effect(39, this.cx, this.cy, 3, 3, 1));
		}
		if (this.blindEff && GameCanvas.gameTick % 5 == 0)
		{
			ServerEffect.addServerEffect(113, this, 1);
		}
		if (this.protectEff)
		{
			int y = this.cH_new + 73;
			if (GameCanvas.gameTick % 5 == 0)
			{
				this.eProtect = new Effect(33, this.cx, y, 3, 3, 1);
			}
			if (this.eProtect != null)
			{
				this.eProtect.update();
				this.eProtect.x = this.cx;
				this.eProtect.y = y;
			}
		}
		if (this.danhHieuEff)
		{
			if (this.eDanhHieu == null)
			{
				string text = (string)GameCanvas.danhHieu.get(this.charID + string.Empty);
				if (text != null)
				{
					string[] array = Res.split(text.Trim(), ",", 0);
					short id = short.Parse(array[0]);
					short num3 = short.Parse(array[1]);
					this.eDanhHieu = new Effect((int)id, this.cx, this.cH_new + 73, 1, -1, -1);
					this.eDanhHieu.timeExist = (long)(num3 * 1000) + mSystem.currentTimeMillis();
				}
			}
			if (this.eDanhHieu != null)
			{
				this.eDanhHieu.update();
				this.eDanhHieu.x = this.cx;
				this.eDanhHieu.y = this.cH_new;
				if (this.eDanhHieu.timeExist <= mSystem.currentTimeMillis())
				{
					this.eDanhHieu = null;
					GameCanvas.danhHieu.remove(this.charID + string.Empty);
				}
			}
		}
		if (this.charFocus != null && this.charFocus.cy < 0)
		{
			this.charFocus = null;
		}
		if (this.isFusion)
		{
			this.tFusion++;
		}
		if (this.isNhapThe && GameCanvas.gameTick % 25 == 0)
		{
			ServerEffect.addServerEffect(114, this, 1);
		}
		if (this.isSetPos)
		{
			this.tpos++;
			if (this.tpos == 1)
			{
				this.tpos = 0;
				this.isSetPos = false;
				this.cx = (int)this.xPos;
				this.cy = (int)this.yPos;
				this.cp1 = (this.cp2 = (this.cp3 = 0));
				if (this.typePos == 1)
				{
					if (this.me)
					{
						this.cxSend = this.cx;
						this.cySend = this.cy;
					}
					this.currentMovePoint = null;
					this.telePortSkill = false;
					ServerEffect.addServerEffect(173, this.cx, this.cy, 1);
				}
				else
				{
					ServerEffect.addServerEffect(60, this.cx, this.cy, 1);
				}
				if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
				{
					this.statusMe = 1;
					return;
				}
				this.statusMe = 4;
			}
			return;
		}
		this.soundUpdate();
		if (this.stone)
		{
			return;
		}
		if (this.isFreez)
		{
			if (GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(113, this.cx, this.cy, 1);
			}
			this.cf = 23;
			long num4 = mSystem.currentTimeMillis();
			if (num4 - this.lastFreez >= 1000L)
			{
				this.freezSeconds--;
				this.lastFreez = num4;
				if (this.freezSeconds < 0)
				{
					this.isFreez = false;
					this.seconds = 0;
					if (this.me)
					{
						global::Char.myCharz().isLockMove = false;
						GameScr.gI().dem = 0;
						GameScr.gI().isFreez = false;
					}
				}
			}
			if (TileMap.tileTypeAt(this.cx / (int)TileMap.size, this.cy / (int)TileMap.size) == 0)
			{
				this.ty++;
				this.wt++;
				this.fy += (this.wy ? -1 : 1);
				if (this.wt == 10)
				{
					this.wt = 0;
					this.wy = !this.wy;
				}
			}
			return;
		}
		if (this.isWaitMonkey)
		{
			this.isLockMove = true;
			this.cf = 17;
			if (GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(154, this.cx, this.cy - 10, 2);
			}
			if (GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(1, this.cx, this.cy + 10, 1);
			}
			this.chargeCount++;
			if (this.chargeCount == 500)
			{
				this.isWaitMonkey = false;
				this.isLockMove = false;
			}
			return;
		}
		if (this.isStandAndCharge)
		{
			this.chargeCount++;
			bool flag = !TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2);
			this.updateEffect();
			this.updateSkillPaint();
			this.moveFast = null;
			this.currentMovePoint = null;
			this.cf = 17;
			if (flag && this.cgender != 2)
			{
				this.cf = 12;
			}
			if (this.cgender == 2)
			{
				if (TileMap.mapID == 170)
				{
					int num5 = GameCanvas.gameTick % 4;
					if (GameCanvas.gameTick % 2 == 0)
					{
						if (this.cdir == 1)
						{
							ServerEffect.addServerEffect(70, this.cx - 18, this.cy - this.ch / 2 + 8, 1);
							ServerEffect.addServerEffect(70, this.cx + 23, this.cy - this.ch / 2 + 15, 1);
						}
						else
						{
							ServerEffect.addServerEffect(70, this.cx + 18, this.cy - this.ch / 2 + 8, 1);
							ServerEffect.addServerEffect(70, this.cx - 23, this.cy - this.ch / 2 + 15, 1);
						}
					}
				}
				else
				{
					if (GameCanvas.gameTick % 3 == 0)
					{
						ServerEffect.addServerEffect(154, this.cx, this.cy - this.ch / 2 + 10, 1);
					}
					if (GameCanvas.gameTick % 5 == 0)
					{
						ServerEffect.addServerEffect(114, this.cx + Res.random(-20, 20), this.cy + Res.random(-20, 20), 1);
					}
				}
			}
			if (this.cgender == 1)
			{
				int num6 = GameCanvas.gameTick % 4;
				if (GameCanvas.gameTick % 2 == 0)
				{
					if (this.cdir == 1)
					{
						ServerEffect.addServerEffect(70, this.cx - 18, this.cy - this.ch / 2 + 8, 1);
						ServerEffect.addServerEffect(70, this.cx + 23, this.cy - this.ch / 2 + 15, 1);
					}
					else
					{
						ServerEffect.addServerEffect(70, this.cx + 18, this.cy - this.ch / 2 + 8, 1);
						ServerEffect.addServerEffect(70, this.cx - 23, this.cy - this.ch / 2 + 15, 1);
					}
				}
			}
			if (this.cgender == 0 && GameCanvas.gameTick % 2 == 0)
			{
				if (this.cdir == 1)
				{
					ServerEffect.addServerEffect(70, this.cx - 18, this.cy - this.ch / 2 + 8, 1);
					ServerEffect.addServerEffect(70, this.cx + 23, this.cy - this.ch / 2 + 15, 1);
				}
				else
				{
					ServerEffect.addServerEffect(70, this.cx + 18, this.cy - this.ch / 2 + 8, 1);
					ServerEffect.addServerEffect(70, this.cx - 23, this.cy - this.ch / 2 + 15, 1);
				}
			}
			this.cur = mSystem.currentTimeMillis();
			Res.outz(string.Concat(new object[]
			{
				"  7.5 gong namekLazer ",
				this.cName,
				"_",
				this.cgender
			}));
			if (this.cur - this.last > (long)this.seconds || this.cur - this.last > 10000L)
			{
				Res.outz("<*> 8  namekLazer gong xong " + this.cName);
				this.stopUseChargeSkill();
				this.isStandAndCharge = false;
				int skillId = (int)this.myskill.skillId;
				if (this.me)
				{
					if (this.cgender == 2)
					{
						Res.outz("<*> 9 [me] xay da xong  " + global::Char.myCharz().myskill.skillId);
						global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], flag ? 1 : 0);
					}
					if (this.cgender == 1)
					{
						Res.outz("<*> 9 [me] namec xong " + global::Char.myCharz().myskill.skillId);
						this.isCreateDark = true;
						global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], flag ? 1 : 0);
					}
					if (this.cgender == 0)
					{
						Res.outz("<*> 9 [me] namec xong " + global::Char.myCharz().myskill.skillId);
						global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], flag ? 1 : 0);
					}
					if (global::Char.myCharz().myskill.skillId >= 77 && global::Char.myCharz().myskill.skillId <= 83)
					{
						Service.gI().skill_not_focus(4);
					}
					skillId = (int)global::Char.myCharz().myskill.skillId;
				}
				else
				{
					if (this.cgender == 2)
					{
						this.setSkillPaint(GameScr.sks[this.skillTemplateId], flag ? 1 : 0);
						Res.outz("<*> 10 xay da xong 111   " + this.skillTemplateId);
					}
					if (this.cgender == 1)
					{
						this.setSkillPaint(GameScr.sks[this.skillTemplateId], flag ? 1 : 0);
						Res.outz("<*> 10 C_NAMEC xong 222   " + this.skillTemplateId);
					}
					if (this.cgender == 0)
					{
						this.setSkillPaint(GameScr.sks[this.skillTemplateId], flag ? 1 : 0);
						Res.outz("<*> 10  C_TRAIDAT xong 333   " + this.skillTemplateId);
					}
					skillId = this.skillTemplateId;
				}
				if (this.cgender == 2 && this.statusMe != 14 && this.statusMe != 5 && (skillId < 77 || skillId > 83))
				{
					GameScr.gI().activeSuperPower(this.cx, this.cy);
				}
				Res.outz("<*> 11 Hoàn thành skill not focus -  STAND");
			}
			this.chargeCount++;
			if (this.chargeCount == 500)
			{
				this.stopUseChargeSkill();
			}
			return;
		}
		if (this.isFlyAndCharge)
		{
			this.updateEffect();
			this.updateSkillPaint();
			this.moveFast = null;
			this.currentMovePoint = null;
			this.posDisY++;
			if (TileMap.tileTypeAt(this.cx, this.cy - this.ch, 8192))
			{
				this.stopUseChargeSkill();
				return;
			}
			if (this.posDisY == 20)
			{
				this.last = mSystem.currentTimeMillis();
			}
			if (this.posDisY <= 20)
			{
				if (this.statusMe != 14)
				{
					this.statusMe = 3;
				}
				this.cvy = -3;
				this.cy += this.cvy;
				this.cf = 7;
				return;
			}
			this.cur = mSystem.currentTimeMillis();
			if (this.cur - this.last <= (long)this.seconds && this.cur - this.last <= 10000L)
			{
				this.cf = 32;
				if (this.cgender == 0 && GameCanvas.gameTick % 3 == 0)
				{
					ServerEffect.addServerEffect(153, this.cx, this.cy - this.ch, 2);
				}
				if (TileMap.mapID == 170 && (this.cgender == 2 || this.cgender == 1) && GameCanvas.gameTick % 3 == 0)
				{
					ServerEffect.addServerEffect(153, this.cx, this.cy - this.ch, 2);
				}
				this.chargeCount++;
				if (this.chargeCount == 500)
				{
					this.stopUseChargeSkill();
				}
				return;
			}
			Res.outz("<*> 12 kết thúc skill  qua cau kinh khi \tFLY " + this.cName);
			this.isFlyAndCharge = false;
			if (this.me)
			{
				this.isCreateDark = true;
				bool flag2 = TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2);
				this.isUseSkillAfterCharge = true;
				this.setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], (!flag2) ? 1 : 0);
				return;
			}
			if (TileMap.mapID == 170)
			{
				this.isCreateDark = true;
				this.isUseSkillAfterCharge = true;
				bool flag3 = TileMap.tileTypeAt(this.cx, this.cy, 2);
				this.setSkillPaint(GameScr.sks[this.skillTemplateId], (!flag3) ? 1 : 0);
			}
			return;
		}
		else
		{
			if (this.me && GameCanvas.isTouch)
			{
				if (this.charFocus != null && this.charFocus.charID >= 0 && this.charFocus.cx > 100 && this.charFocus.cx < TileMap.pxw - 100 && this.isInEnterOnlinePoint() == null && this.isInEnterOfflinePoint() == null && !this.isAttacPlayerStatus() && TileMap.mapID != 51 && TileMap.mapID != 52 && GameCanvas.panel.vPlayerMenu.size() > 0 && GameScr.gI().popUpYesNo == null)
				{
					int num7 = global::Math.abs(this.cx - this.charFocus.cx);
					int num8 = global::Math.abs(this.cy - this.charFocus.cy);
					if (num7 < 60 && num8 < 40)
					{
						if (this.cmdMenu == null)
						{
							this.cmdMenu = new Command(mResources.MENU, 11111);
							this.cmdMenu.isPlaySoundButton = false;
						}
						this.cmdMenu.x = this.charFocus.cx - GameScr.cmx;
						this.cmdMenu.y = this.charFocus.cy - this.charFocus.ch - 30 - GameScr.cmy;
					}
					else
					{
						this.cmdMenu = null;
					}
				}
				else
				{
					this.cmdMenu = null;
				}
			}
			if (this.isShadown)
			{
				this.updateShadown();
			}
			if (this.isTeleport)
			{
				return;
			}
			if (this.chatInfo != null)
			{
				this.chatInfo.update();
			}
			if (this.shadowLife > 0)
			{
				this.shadowLife--;
			}
			if (this.resultTest > 0 && GameCanvas.gameTick % 2 == 0)
			{
				this.resultTest -= 1;
				if (this.resultTest == 30 || this.resultTest == 60)
				{
					this.resultTest = 0;
				}
			}
			this.updateSkillPaint();
			if (this.mobMe != null)
			{
				this.updateMobMe();
			}
			if (this.arr != null)
			{
				this.arr.update();
			}
			if (this.dart != null)
			{
				this.dart.update();
			}
			this.updateEffect();
			if (this.holdEffID != 0)
			{
				if (GameCanvas.gameTick % 5 == 0)
				{
					EffecMn.addEff(new Effect(32, this.cx, this.cy + 24, 3, 5, 1));
				}
				return;
			}
			if (this.blindEff)
			{
				return;
			}
			if (this.sleepEff)
			{
				return;
			}
			if (!this.holder)
			{
				if (this.cHP > 0L)
				{
					for (int i = 0; i < this.vEff.size(); i++)
					{
						EffectChar effectChar = (EffectChar)this.vEff.elementAt(i);
						if (effectChar.template.type == 0 || effectChar.template.type == 12)
						{
							if (GameCanvas.isEff1)
							{
								this.cHP += (long)effectChar.param;
								this.cMP += (long)effectChar.param;
							}
						}
						else if (effectChar.template.type == 4 || effectChar.template.type == 17)
						{
							if (GameCanvas.isEff1)
							{
								this.cHP += (long)effectChar.param;
							}
						}
						else if (effectChar.template.type == 13 && GameCanvas.isEff1)
						{
							this.cHP -= this.cHPFull * 3L / 100L;
							if (this.cHP < 1L)
							{
								this.cHP = 1L;
							}
						}
					}
					if (this.eff5BuffHp > 0 && GameCanvas.isEff2)
					{
						this.cHP += (long)this.eff5BuffHp;
					}
					if (this.eff5BuffMp > 0 && GameCanvas.isEff2)
					{
						this.cMP += (long)this.eff5BuffMp;
					}
					if (this.cHP > this.cHPFull)
					{
						this.cHP = this.cHPFull;
					}
					if (this.cMP > this.cMPFull)
					{
						this.cMP = this.cMPFull;
					}
				}
				if (this.cmtoChar)
				{
					GameScr.cmtoX = this.cx - GameScr.gW2;
					GameScr.cmtoY = this.cy - GameScr.gH23;
					if (!GameCanvas.isTouchControl)
					{
						GameScr.cmtoX += GameScr.gW6 * this.cdir;
					}
				}
				this.tick = (this.tick + 1) % 100;
				if (this.me)
				{
					if (this.charFocus != null && !GameScr.vCharInMap.contains(this.charFocus))
					{
						this.charFocus = null;
					}
					if (this.cx < 10)
					{
						this.cvx = 0;
						this.cx = 10;
					}
					else if (this.cx > TileMap.pxw - 10)
					{
						this.cx = TileMap.pxw - 10;
						this.cvx = 0;
					}
					if (!global::Char.ischangingMap && this.isInWaypoint())
					{
						if (Mod.DungPham.KoiOctiiu957.MainMod.isLockMap && !Mod.DungPham.KoiOctiiu957.AutoMap.isAutoChangeMap)
						{
							// Do not lock key or request change map
						}
						else
						{
							Service.gI().charMove();
							if (TileMap.isTrainingMap())
							{
								Service.gI().getMapOffline();
								global::Char.ischangingMap = true;
							}
							else
							{
								Service.gI().requestChangeMap();
							}
							global::Char.isLockKey = true;
							global::Char.ischangingMap = true;
							GameCanvas.clearKeyHold();
							GameCanvas.clearKeyPressed();
							InfoDlg.showWait();
							return;
						}
					}
					if (this.statusMe != 4 && Res.abs(this.cx - this.cxSend) + Res.abs(this.cy - this.cySend) >= 70 && this.cy - this.cySend <= 0 && this.me)
					{
						Service.gI().charMove();
					}
					if (this.isLockMove)
					{
						this.currentMovePoint = null;
					}
					if (this.currentMovePoint != null)
					{
						if (global::Char.abs(this.cx - this.currentMovePoint.xEnd) <= 16 && global::Char.abs(this.cy - this.currentMovePoint.yEnd) <= 16)
						{
							this.cx = (this.currentMovePoint.xEnd + this.cx) / 2;
							this.cy = this.currentMovePoint.yEnd;
							this.currentMovePoint = null;
							GameScr.instance.clickMoving = false;
							this.checkPerformEndMovePointAction();
							this.cvx = (this.cvy = 0);
							if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
							{
								this.statusMe = 1;
							}
							else
							{
								this.setCharFallFromJump();
							}
							Service.gI().charMove();
						}
						else
						{
							this.cdir = ((this.currentMovePoint.xEnd <= this.cx) ? -1 : 1);
							if (TileMap.tileTypeAt(this.cx, this.cy, 2))
							{
								this.statusMe = 2;
								if (this.currentMovePoint != null)
								{
									this.cvx = this.cspeed * this.cdir;
									this.cvy = 0;
								}
								if (global::Char.abs(this.cx - this.currentMovePoint.xEnd) <= 10)
								{
									if (this.currentMovePoint.yEnd > this.cy)
									{
										bool flag4 = false;
										sbyte b;
										if (this.cdir == 1)
										{
											b = 1;
										}
										else
										{
											b = -1;
										}
										for (int j = 0; j < 2; j++)
										{
											if (TileMap.tileTypeAt(this.currentMovePoint.xEnd + this.chw * (int)b, this.cy + this.chh * j, 2))
											{
												flag4 = true;
												break;
											}
										}
										if (flag4)
										{
											this.currentMovePoint = null;
											GameScr.instance.clickMoving = false;
											this.statusMe = 1;
											this.cvx = (this.cvy = 0);
											this.checkPerformEndMovePointAction();
										}
										else
										{
											SoundMn.gI().charJump();
											this.cx = this.currentMovePoint.xEnd;
											this.statusMe = 10;
											this.cvy = -5;
											this.cvx = 0;
											Res.outz("Jum lun");
										}
									}
									else
									{
										SoundMn.gI().charJump();
										this.cx = this.currentMovePoint.xEnd;
										this.statusMe = 10;
										this.cvy = -5;
										this.cvx = 0;
									}
								}
								if (this.cdir == 1)
								{
									if (TileMap.tileTypeAt(this.cx + this.chw, this.cy - this.chh, 4))
									{
										this.cvx = this.cspeed * this.cdir;
										this.statusMe = 10;
										this.cvy = -5;
									}
								}
								else if (TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - this.chh, 8))
								{
									this.cvx = this.cspeed * this.cdir;
									this.statusMe = 10;
									this.cvy = -5;
								}
							}
							else
							{
								if (this.currentMovePoint.yEnd < this.cy + 10)
								{
									this.statusMe = 10;
									this.cvy = -5;
									if (global::Char.abs(this.cy - this.currentMovePoint.yEnd) <= 10)
									{
										this.cy = this.currentMovePoint.yEnd;
										this.cvy = 0;
									}
									if (global::Char.abs(this.cx - this.currentMovePoint.xEnd) <= 10)
									{
										this.cvx = 0;
									}
									else
									{
										this.cvx = this.cspeed * this.cdir;
									}
								}
								else if (TileMap.tileTypeAt(this.cx, this.cy, 2))
								{
									this.currentMovePoint = null;
									GameScr.instance.clickMoving = false;
									this.statusMe = 1;
									this.cvx = (this.cvy = 0);
									this.checkPerformEndMovePointAction();
								}
								else
								{
									if (this.statusMe == 10 || this.statusMe == 2)
									{
										this.cvy = 0;
									}
									this.statusMe = 4;
								}
								if (this.currentMovePoint.yEnd > this.cy)
								{
									if (this.cdir == 1)
									{
										if (TileMap.tileTypeAt(this.cx + this.chw, this.cy - this.chh, 4))
										{
											this.cvx = (this.cvy = 0);
											this.statusMe = 4;
											this.currentMovePoint = null;
											GameScr.instance.clickMoving = false;
											this.checkPerformEndMovePointAction();
										}
									}
									else if (TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - this.chh, 8))
									{
										this.cvx = (this.cvy = 0);
										this.statusMe = 4;
										this.currentMovePoint = null;
										GameScr.instance.clickMoving = false;
										this.checkPerformEndMovePointAction();
									}
								}
							}
						}
					}
					this.searchFocus();
				}
				else
				{
					this.checkHideCharName();
					if (this.statusMe == 1 || this.statusMe == 6)
					{
						bool flag5 = false;
						if (this.currentMovePoint != null)
						{
							if (global::Char.abs(this.currentMovePoint.xEnd - this.cx) < 17 && global::Char.abs(this.currentMovePoint.yEnd - this.cy) < 25)
							{
								this.cx = this.currentMovePoint.xEnd;
								this.cy = this.currentMovePoint.yEnd;
								this.currentMovePoint = null;
								if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
								{
									this.statusMe = 1;
									this.cp3 = 0;
									GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
									GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
								}
								else
								{
									this.statusMe = 4;
									this.cvy = 0;
									this.cp1 = 0;
								}
								flag5 = true;
							}
							else if ((this.statusBeforeNothing == 10 || this.cf == 8) && this.vMovePoints.size() > 0)
							{
								flag5 = true;
							}
							else if (this.cy == this.currentMovePoint.yEnd)
							{
								if (this.cx != this.currentMovePoint.xEnd)
								{
									this.cx = (this.cx + this.currentMovePoint.xEnd) / 2;
									this.cf = GameCanvas.gameTick % 5 + 2;
								}
							}
							else if (this.cy < this.currentMovePoint.yEnd)
							{
								this.cf = 12;
								this.cx = (this.cx + this.currentMovePoint.xEnd) / 2;
								if (this.cvy < 0)
								{
									this.cvy = 0;
								}
								this.cy += this.cvy;
								if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
								{
									GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
									GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
								}
								this.cvy++;
								if (this.cvy > 16)
								{
									this.cy = (this.cy + this.currentMovePoint.yEnd) / 2;
								}
							}
							else
							{
								this.cf = 7;
								this.cx = (this.cx + this.currentMovePoint.xEnd) / 2;
								this.cy = (this.cy + this.currentMovePoint.yEnd) / 2;
							}
						}
						else
						{
							flag5 = true;
						}
						if (flag5 && this.vMovePoints.size() > 0)
						{
							this.currentMovePoint = (MovePoint)this.vMovePoints.firstElement();
							this.vMovePoints.removeElementAt(0);
							if (this.currentMovePoint.status == 2)
							{
								if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 12) & 2) != 2)
								{
									this.statusMe = 10;
									this.cp1 = 0;
									this.cp2 = 0;
									this.cvx = -(this.cx - this.currentMovePoint.xEnd) / 10;
									this.cvy = -(this.cy - this.currentMovePoint.yEnd) / 10;
									if (this.cx - this.currentMovePoint.xEnd > 0)
									{
										this.cdir = -1;
									}
									else if (this.cx - this.currentMovePoint.xEnd < 0)
									{
										this.cdir = 1;
									}
								}
								else
								{
									this.statusMe = 2;
									if (this.cx - this.currentMovePoint.xEnd > 0)
									{
										this.cdir = -1;
									}
									else if (this.cx - this.currentMovePoint.xEnd < 0)
									{
										this.cdir = 1;
									}
									this.cvx = this.cspeed * this.cdir;
									this.cvy = 0;
								}
							}
							else if (this.currentMovePoint.status == 3)
							{
								if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 23) & 2) != 2)
								{
									this.statusMe = 10;
									this.cp1 = 0;
									this.cp2 = 0;
									this.cvx = -(this.cx - this.currentMovePoint.xEnd) / 10;
									this.cvy = -(this.cy - this.currentMovePoint.yEnd) / 10;
									if (this.cx - this.currentMovePoint.xEnd > 0)
									{
										this.cdir = -1;
									}
									else if (this.cx - this.currentMovePoint.xEnd < 0)
									{
										this.cdir = 1;
									}
								}
								else
								{
									this.statusMe = 3;
									GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
									GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
									if (this.cx - this.currentMovePoint.xEnd > 0)
									{
										this.cdir = -1;
									}
									else if (this.cx - this.currentMovePoint.xEnd < 0)
									{
										this.cdir = 1;
									}
									this.cvx = global::Char.abs(this.cx - this.currentMovePoint.xEnd) / 10 * this.cdir;
									this.cvy = -10;
								}
							}
							else if (this.currentMovePoint.status == 4)
							{
								this.statusMe = 4;
								if (this.cx - this.currentMovePoint.xEnd > 0)
								{
									this.cdir = -1;
								}
								else if (this.cx - this.currentMovePoint.xEnd < 0)
								{
									this.cdir = 1;
								}
								this.cvx = global::Char.abs(this.cx - this.currentMovePoint.xEnd) / 9 * this.cdir;
								this.cvy = 0;
							}
							else
							{
								this.cx = this.currentMovePoint.xEnd;
								this.cy = this.currentMovePoint.yEnd;
								this.currentMovePoint = null;
							}
						}
					}
				}
				switch (this.statusMe)
				{
				case 1:
					this.updateCharStand();
					break;
				case 2:
					this.updateCharRun();
					break;
				case 3:
					this.updateCharJump();
					break;
				case 4:
					this.updateCharFall();
					break;
				case 5:
					this.updateCharDeadFly();
					break;
				case 6:
					if (this.isInjure <= 0)
					{
						this.cf = 0;
					}
					else if (this.statusBeforeNothing == 10)
					{
						this.cx += this.cvx;
					}
					else if (this.cf <= 1)
					{
						this.cp1++;
						if (this.cp1 > 6)
						{
							this.cf = 0;
						}
						else
						{
							this.cf = 1;
						}
						if (this.cp1 > 10)
						{
							this.cp1 = 0;
						}
					}
					if (this.cf != 7 && this.cf != 12 && (TileMap.tileTypeAtPixel(this.cx, this.cy + 1) & 2) != 2)
					{
						this.cvx = 0;
						this.cvy = 0;
						this.statusMe = 4;
						this.cf = 7;
					}
					if (!this.me)
					{
						this.cp3++;
						if (this.cp3 > 10)
						{
							if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 1) & 2) != 2)
							{
								this.cy += 5;
							}
							else
							{
								this.cf = 0;
							}
						}
						if (this.cp3 > 50)
						{
							this.cp3 = 0;
							this.currentMovePoint = null;
						}
					}
					break;
				case 9:
					this.updateCharAutoJump();
					break;
				case 10:
					this.updateCharFly();
					break;
				case 12:
					this.updateSkillStand();
					break;
				case 13:
					this.updateSkillFall();
					break;
				case 14:
					this.cp1++;
					if (this.cp1 > 30)
					{
						this.cp1 = 0;
					}
					if (this.cp1 % 15 < 5)
					{
						this.cf = 0;
					}
					else
					{
						this.cf = 1;
					}
					break;
				case 16:
					this.updateResetPoint();
					break;
				}
				if (this.isInjure > 0)
				{
					this.cf = 23;
					this.isInjure -= 1;
				}
				if (this.wdx != 0 || this.wdy != 0)
				{
					this.startDie(this.wdx, this.wdy);
					this.wdx = 0;
					this.wdy = 0;
				}
				if (this.moveFast != null)
				{
					if (this.moveFast[0] == 0)
					{
						short[] array2 = this.moveFast;
						int num9 = 0;
						int num10 = num9;
						array2[num10] += 1;
						ServerEffect.addServerEffect(60, this, 1);
					}
					else if (this.moveFast[0] < 10)
					{
						short[] array3 = this.moveFast;
						int num11 = 0;
						int num12 = num11;
						array3[num12] += 1;
					}
					else
					{
						this.cx = (int)this.moveFast[1];
						this.cy = (int)this.moveFast[2];
						this.moveFast = null;
						ServerEffect.addServerEffect(60, this, 1);
						if (this.me)
						{
							if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
							{
								this.statusMe = 4;
								global::Char.myCharz().setAutoSkillPaint(GameScr.sks[38], 1);
							}
							else
							{
								Service.gI().charMove();
								global::Char.myCharz().setAutoSkillPaint(GameScr.sks[38], 0);
							}
						}
					}
				}
				if (this.statusMe != 10)
				{
					this.fy = 0;
				}
				if (this.isCharge)
				{
					this.cf = 17;
					if (GameCanvas.gameTick % 4 == 0)
					{
						ServerEffect.addServerEffect(1, this.cx, this.cy + GameCanvas.transY, 1);
					}
					if (this.me)
					{
						long num13 = mSystem.currentTimeMillis();
						if (num13 - this.last >= 1000L)
						{
							Res.outz("%= " + this.myskill.damage);
							this.last = num13;
							this.cHP += this.cHPFull * (long)this.myskill.damage / 100L;
							this.cMP += this.cMPFull * (long)this.myskill.damage / 100L;
							if (this.cHP < this.cHPFull)
							{
								GameScr.startFlyText(string.Concat(new object[]
								{
									"+",
									this.cHPFull * (long)this.myskill.damage / 100L,
									" ",
									mResources.HP
								}), this.cx, this.cy - this.ch - 20, 0, -1, mFont.HP);
							}
							if (this.cMP < this.cMPFull)
							{
								GameScr.startFlyText(string.Concat(new object[]
								{
									"+",
									this.cMPFull * (long)this.myskill.damage / 100L,
									" ",
									mResources.KI
								}), this.cx, this.cy - this.ch - 20, 0, -2, mFont.MP);
							}
							Service.gI().skill_not_focus(2);
						}
					}
				}
				if (this.isFlyUp)
				{
					if (this.me)
					{
						global::Char.isLockKey = true;
						this.statusMe = 3;
						this.cvy = -8;
						if (this.cy <= TileMap.pxh - 240)
						{
							this.isFlyUp = false;
							global::Char.isLockKey = false;
							this.statusMe = 4;
						}
					}
					else
					{
						this.statusMe = 3;
						this.cvy = -8;
						if (this.cy <= TileMap.pxh - 240)
						{
							this.cvy = 0;
							this.isFlyUp = false;
							this.cvy = 0;
							this.statusMe = 1;
						}
					}
				}
				this.updateMount();
				this.updEffChar();
				this.updateEye();
				this.updateFHead();
				return;
			}
			if (this.charHold != null && (this.charHold.statusMe == 14 || this.charHold.statusMe == 5))
			{
				this.removeHoleEff();
			}
			if (this.mobHold != null && this.mobHold.status == 1)
			{
				this.removeHoleEff();
			}
			if (this.me && this.statusMe == 2 && this.currentMovePoint != null)
			{
				this.holder = false;
				this.charHold = null;
				this.mobHold = null;
			}
			if (TileMap.tileTypeAt(this.cx, this.cy, 2))
			{
				this.cf = 16;
				return;
			}
			this.cf = 31;
			return;
		}
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x00050894 File Offset: 0x0004EA94
	private void updateEffect()
	{
		if (this.effPaints != null)
		{
			for (int i = 0; i < this.effPaints.Length; i++)
			{
				if (this.effPaints[i] != null)
				{
					if (this.effPaints[i].eMob != null)
					{
						if (!this.effPaints[i].isFly)
						{
							this.effPaints[i].eMob.setInjure();
							this.effPaints[i].eMob.injureBy = this;
							if (this.me)
							{
								this.effPaints[i].eMob.hpInjure = global::Char.myCharz().cDamFull / 2L - global::Char.myCharz().cDamFull * (long)NinjaUtil.randomNumber(11) / 100L;
							}
							int num = this.effPaints[i].eMob.h >> 1;
							if (this.effPaints[i].eMob.isBigBoss())
							{
								num = this.effPaints[i].eMob.getY() + 20;
							}
							GameScr.startSplash(this.effPaints[i].eMob.x, this.effPaints[i].eMob.y - num, this.cdir);
							this.effPaints[i].isFly = true;
						}
					}
					else if (this.effPaints[i].eChar != null && !this.effPaints[i].isFly)
					{
						if (this.effPaints[i].eChar.charID >= 0)
						{
							this.effPaints[i].eChar.doInjure();
						}
						GameScr.startSplash(this.effPaints[i].eChar.cx, this.effPaints[i].eChar.cy - (this.effPaints[i].eChar.ch >> 1), this.cdir);
						this.effPaints[i].isFly = true;
					}
					this.effPaints[i].index++;
					if (this.effPaints[i].index >= this.effPaints[i].effCharPaint.arrEfInfo.Length)
					{
						this.effPaints[i] = null;
					}
				}
			}
		}
		if (this.indexEff >= 0 && this.eff != null && GameCanvas.gameTick % 2 == 0)
		{
			this.indexEff++;
			if (this.indexEff >= this.eff.arrEfInfo.Length)
			{
				this.indexEff = -1;
				this.eff = null;
			}
		}
		if (this.indexEffTask >= 0 && this.effTask != null && GameCanvas.gameTick % 2 == 0)
		{
			this.indexEffTask++;
			if (this.indexEffTask >= this.effTask.arrEfInfo.Length)
			{
				this.indexEffTask = -1;
				this.effTask = null;
			}
		}
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x00050B78 File Offset: 0x0004ED78
	private void checkPerformEndMovePointAction()
	{
		if (this.endMovePointCommand != null)
		{
			Command command = this.endMovePointCommand;
			this.endMovePointCommand = null;
			command.performAction();
		}
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x00050BA4 File Offset: 0x0004EDA4
	private void checkHideCharName()
	{
		if (GameCanvas.gameTick % 20 == 0 && this.charID >= 0)
		{
			this.paintName = true;
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = null;
				try
				{
					@char = (global::Char)GameScr.vCharInMap.elementAt(i);
				}
				catch (Exception ex)
				{
				}
				if (@char != null && !@char.Equals(this))
				{
					if ((@char.cy == this.cy && Res.abs(@char.cx - this.cx) < 35) || (this.cy - @char.cy < 32 && this.cy - @char.cy > 0 && Res.abs(@char.cx - this.cx) < 24))
					{
						this.paintName = false;
					}
				}
			}
			for (int j = 0; j < GameScr.vNpc.size(); j++)
			{
				Npc npc = null;
				try
				{
					npc = (Npc)GameScr.vNpc.elementAt(j);
				}
				catch (Exception ex2)
				{
				}
				if (npc != null)
				{
					if (npc.cy == this.cy && Res.abs(npc.cx - this.cx) < 24)
					{
						this.paintName = false;
					}
				}
			}
		}
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x00050D2C File Offset: 0x0004EF2C
	private void updateMobMe()
	{
		if (this.tMobMeBorn != 0)
		{
			this.tMobMeBorn--;
		}
		if (this.tMobMeBorn == 0)
		{
			this.mobMe.xFirst = ((this.cdir != 1) ? (this.cx + 30) : (this.cx - 30));
			this.mobMe.yFirst = this.cy - 60;
			int num = this.mobMe.xFirst - this.mobMe.x;
			int num2 = this.mobMe.yFirst - this.mobMe.y;
			this.mobMe.x += num / 4;
			this.mobMe.y += num2 / 4;
			this.mobMe.dir = this.cdir;
		}
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x00050E0C File Offset: 0x0004F00C
	private void updateSkillPaint()
	{
		if (this.statusMe == 14 || this.statusMe == 5)
		{
			return;
		}
		if (this.skillPaint != null && ((this.charFocus != null && this.isMeCanAttackOtherPlayer(this.charFocus) && this.charFocus.statusMe == 14) || (this.mobFocus != null && this.mobFocus.status == 0)))
		{
			if (!this.me)
			{
				if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
				{
					this.statusMe = 1;
				}
				else
				{
					this.statusMe = 6;
				}
				this.cp3 = 0;
			}
			this.indexSkill = 0;
			this.skillPaint = null;
			this.skillPaintRandomPaint = null;
			this.eff0 = (this.eff1 = (this.eff2 = null));
			this.i0 = (this.i1 = (this.i2 = 0));
			this.mobFocus = null;
			this.charFocus = null;
			this.effPaints = null;
			this.currentMovePoint = null;
			this.arr = null;
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
			{
				this.delayFall = 5;
			}
		}
		if (this.skillPaint != null && this.arr == null && this.skillInfoPaint() != null && this.indexSkill >= this.skillInfoPaint().Length)
		{
			if (!this.me)
			{
				if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
				{
					this.statusMe = 1;
				}
				else
				{
					this.statusMe = 6;
				}
				this.cp3 = 0;
			}
			this.indexSkill = 0;
			Res.outz("remove 2");
			this.skillPaint = null;
			this.skillPaintRandomPaint = null;
			this.eff0 = (this.eff1 = (this.eff2 = null));
			this.i0 = (this.i1 = (this.i2 = 0));
			this.arr = null;
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
			{
				this.delayFall = 5;
			}
		}
		SkillInfoPaint[] array = this.skillInfoPaint();
		if (array != null && this.indexSkill >= 0 && this.indexSkill <= array.Length - 1)
		{
			if (array[this.indexSkill].effS0Id != 0)
			{
				this.eff0 = GameScr.efs[array[this.indexSkill].effS0Id - 1];
				this.i0 = (this.dx0 = (this.dy0 = 0));
			}
			if (array[this.indexSkill].effS1Id != 0)
			{
				this.eff1 = GameScr.efs[array[this.indexSkill].effS1Id - 1];
				this.i1 = (this.dx1 = (this.dy1 = 0));
			}
			if (array[this.indexSkill].effS2Id != 0)
			{
				this.eff2 = GameScr.efs[array[this.indexSkill].effS2Id - 1];
				this.i2 = (this.dx2 = (this.dy2 = 0));
			}
			SkillInfoPaint[] array2 = array;
			int num = this.indexSkill;
			if (array2 != null && array2[num] != null && num >= 0 && num <= array2.Length - 1 && array2[num].arrowId != 0)
			{
				int arrowId = array2[num].arrowId;
				if (arrowId >= 100)
				{
					IMapObject mapObject2;
					if (this.mobFocus == null)
					{
						IMapObject mapObject = this.charFocus;
						mapObject2 = mapObject;
					}
					else
					{
						mapObject2 = this.mobFocus;
					}
					IMapObject mapObject3 = mapObject2;
					if (mapObject3 != null)
					{
						int num2 = Res.abs(mapObject3.getX() - this.cx);
						int num3 = Res.abs(mapObject3.getY() - this.cy);
						int num4;
						if (num2 > 4 * num3)
						{
							num4 = 0;
						}
						else
						{
							if (mapObject3.getY() < this.cy)
							{
								num4 = -3;
							}
							else
							{
								num4 = 3;
							}
							if (mapObject3 is BigBoss)
							{
								BigBoss bigBoss = (BigBoss)mapObject3;
								if (bigBoss.haftBody)
								{
									num4 = -20;
								}
							}
						}
						this.dart = new PlayerDart(this, arrowId - 100, this.skillPaintRandomPaint, this.cx + (array2[num].adx - 10) * this.cdir, this.cy + array2[num].ady + num4);
						if (this.myskill != null)
						{
							if ((int)this.myskill.template.id == 1)
							{
								SoundMn.gI().traidatKame();
							}
							else if ((int)this.myskill.template.id == 3)
							{
								SoundMn.gI().namekKame();
							}
							else if ((int)this.myskill.template.id == 5)
							{
								SoundMn.gI().xaydaKame();
							}
							else if ((int)this.myskill.template.id == 11)
							{
								SoundMn.gI().nameLazer();
							}
						}
					}
					else if (this.isFlyAndCharge || this.isUseSkillAfterCharge)
					{
						this.stopUseChargeSkill();
					}
				}
				else
				{
					Res.outz("g");
					this.arr = new Arrow(this, GameScr.arrs[arrowId - 1]);
					this.arr.life = 10;
					this.arr.ax = this.cx + array2[num].adx;
					this.arr.ay = this.cy + array2[num].ady;
				}
			}
			if ((this.mobFocus != null || (!this.me && this.charFocus != null) || (this.me && this.charFocus != null && (this.isMeCanAttackOtherPlayer(this.charFocus) || this.isSelectingSkillBuffToPlayer()) && this.arr == null && this.dart == null)) && this.indexSkill == array.Length - 1)
			{
				this.setAttack();
				if (this.me && this.myskill.template.isAttackSkill())
				{
					this.saveLoadPreviousSkill();
				}
			}
			if (!this.me)
			{
				IMapObject mapObject4 = null;
				if (this.mobFocus != null)
				{
					mapObject4 = this.mobFocus;
				}
				else if (this.charFocus != null)
				{
					mapObject4 = this.charFocus;
				}
				if (mapObject4 != null)
				{
					if (Res.abs(mapObject4.getX() - this.cx) < 10)
					{
						if (mapObject4.getX() > this.cx)
						{
							this.cx -= 10;
						}
						else
						{
							this.cx += 10;
						}
					}
					if (mapObject4.getX() > this.cx)
					{
						this.cdir = 1;
					}
					else
					{
						this.cdir = -1;
					}
				}
			}
		}
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x000045ED File Offset: 0x000027ED
	public void saveLoadPreviousSkill()
	{
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x000514F4 File Offset: 0x0004F6F4
	public void setResetPoint(int x, int y)
	{
		InfoDlg.hide();
		this.currentMovePoint = null;
		int num = this.cx - x;
		if (this.cy - y == 0)
		{
			this.cx = x;
			global::Char.ischangingMap = false;
			global::Char.isLockKey = false;
			return;
		}
		this.statusMe = 16;
		this.cp2 = x;
		this.cp3 = y;
		this.cp1 = 0;
		global::Char.myCharz().cxSend = x;
		global::Char.myCharz().cySend = y;
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x0005156C File Offset: 0x0004F76C
	private void updateCharDeadFly()
	{
		this.isFreez = false;
		if (this.isCharge)
		{
			this.isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		this.cp1++;
		this.cx += (this.cp2 - this.cx) / 4;
		if (this.cp1 > 7)
		{
			this.cy += (this.cp3 - this.cy) / 4;
		}
		else
		{
			this.cy += this.cp1 - 10;
		}
		if (Res.abs(this.cp2 - this.cx) < 4 && Res.abs(this.cp3 - this.cy) < 10)
		{
			this.cx = this.cp2;
			this.cy = this.cp3;
			this.statusMe = 14;
			if (this.me)
			{
				GameScr.gI().resetButton();
				Service.gI().charMove();
			}
		}
		this.cf = 23;
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x0005168C File Offset: 0x0004F88C
	private void updateResetPoint()
	{
		InfoDlg.hide();
		GameCanvas.clearAllPointerEvent();
		this.currentMovePoint = null;
		this.cp1++;
		this.cx += (this.cp2 - this.cx) / 4;
		if (this.cp1 > 7)
		{
			this.cy += (this.cp3 - this.cy) / 4;
		}
		else
		{
			this.cy += this.cp1 - 10;
		}
		if (Res.abs(this.cp2 - this.cx) < 4 && Res.abs(this.cp3 - this.cy) < 10)
		{
			this.cx = this.cp2;
			this.cy = this.cp3;
			this.statusMe = 1;
			this.cp3 = 0;
			global::Char.ischangingMap = false;
			Service.gI().charMove();
		}
		this.cf = 23;
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x000045ED File Offset: 0x000027ED
	public void updateSkillFall()
	{
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x00051788 File Offset: 0x0004F988
	public void updateSkillStand()
	{
		this.ty = 0;
		this.cp1++;
		if (this.cdir == 1)
		{
			if ((TileMap.tileTypeAtPixel(this.cx + this.chw, this.cy - this.chh) & 4) == 4)
			{
				this.cvx = 0;
			}
		}
		else if ((TileMap.tileTypeAtPixel(this.cx - this.chw, this.cy - this.chh) & 8) == 8)
		{
			this.cvx = 0;
		}
		if (this.cy > this.ch && TileMap.tileTypeAt(this.cx, this.cy - this.ch + 24, 8192))
		{
			if (!TileMap.tileTypeAt(this.cx, this.cy, 2))
			{
				this.statusMe = 4;
				this.cp1 = 0;
				this.cp2 = 0;
				this.cvy = 1;
			}
			else
			{
				this.cy = TileMap.tileYofPixel(this.cy);
			}
		}
		this.cx += this.cvx;
		this.cy += this.cvy;
		if (this.cy < 0)
		{
			this.cy = (this.cvy = 0);
		}
		if (this.cvy == 0)
		{
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
			{
				this.statusMe = 4;
				this.cvx = (this.cspeed >> 1) * this.cdir;
				this.cp1 = (this.cp2 = 0);
			}
		}
		else if (this.cvy < 0)
		{
			this.cvy++;
			if (this.cvy == 0)
			{
				this.cvy = 1;
			}
		}
		else
		{
			if (this.cvy < 20 && this.cp1 % 5 == 0)
			{
				this.cvy++;
			}
			if (this.cvy > 3)
			{
				this.cvy = 3;
			}
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 3) & 2) == 2 && this.cy <= TileMap.tileXofPixel(this.cy + 3))
			{
				this.cvx = (this.cvy = 0);
				this.cy = TileMap.tileXofPixel(this.cy + 3);
			}
		}
		if (this.cvx > 0)
		{
			this.cvx--;
		}
		else if (this.cvx < 0)
		{
			this.cvx++;
		}
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x00051A28 File Offset: 0x0004FC28
	public void updateCharAutoJump()
	{
		this.isFreez = false;
		if (this.isCharge)
		{
			this.isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		this.cx += this.cvx * this.cdir;
		this.cy += this.cvyJump;
		this.cvyJump++;
		if (this.cp1 == 0)
		{
			this.cf = 7;
		}
		else
		{
			this.cf = 23;
		}
		if (this.cvyJump == -3)
		{
			this.cf = 8;
		}
		else if (this.cvyJump == -2)
		{
			this.cf = 9;
		}
		else if (this.cvyJump == -1)
		{
			this.cf = 10;
		}
		else if (this.cvyJump == 0)
		{
			this.cf = 11;
		}
		if (this.cvyJump == 0)
		{
			this.statusMe = 6;
			this.cp3 = 0;
			((MovePoint)this.vMovePoints.firstElement()).status = 4;
			this.isJump = true;
			this.cp1 = 0;
			this.cvy = 1;
		}
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x00051B60 File Offset: 0x0004FD60
	public int getVx(int size, int dx, int dy)
	{
		if (dy > 0 && !TileMap.tileTypeAt(this.cx, this.cy, 2))
		{
			if (dx - dy <= 10)
			{
				return 5;
			}
			if (dx - dy <= 30)
			{
				return 6;
			}
			if (dx - dy <= 50)
			{
				return 7;
			}
			if (dx - dy <= 70)
			{
				return 8;
			}
		}
		if (dx <= 30)
		{
			return 4;
		}
		if (dx <= 160)
		{
			return 5;
		}
		if (dx <= 270)
		{
			return 6;
		}
		if (dx <= 320)
		{
			return 7;
		}
		return 8;
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x000070F2 File Offset: 0x000052F2
	public void hide()
	{
		this.isHide = true;
		EffecMn.addEff(new Effect(107, this.cx, this.cy + 25, 3, 15, 1));
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0000711A File Offset: 0x0000531A
	public void show()
	{
		this.isHide = false;
		EffecMn.addEff(new Effect(107, this.cx, this.cy + 25, 3, 10, 1));
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x00007142 File Offset: 0x00005342
	public int getVy(int size, int dx, int dy)
	{
		if (dy <= 10)
		{
			return 5;
		}
		if (dy <= 20)
		{
			return 6;
		}
		if (dy <= 30)
		{
			return 7;
		}
		if (dy <= 40)
		{
			return 8;
		}
		if (dy <= 50)
		{
			return 9;
		}
		return 10;
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x00051BF0 File Offset: 0x0004FDF0
	public int returnAct(int xFirst, int yFirst, int xEnd, int yEnd)
	{
		int num = xEnd - xFirst;
		int num2 = yEnd - yFirst;
		if (num == 0 && num2 == 0)
		{
			return 1;
		}
		if (num2 == 0 && yFirst % 24 == 0 && TileMap.tileTypeAt(xFirst, yFirst, 2))
		{
			return 2;
		}
		if (num2 > 0 && (yFirst % 24 != 0 || !TileMap.tileTypeAt(xFirst, yFirst, 2)))
		{
			return 4;
		}
		this.cvy = -10;
		this.cp1 = 0;
		this.cdir = ((num <= 0) ? -1 : 1);
		if (num <= 5)
		{
			this.cvx = 0;
		}
		else if (num <= 10)
		{
			this.cvx = 3;
		}
		else
		{
			this.cvx = 5;
		}
		return 9;
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x00051CA4 File Offset: 0x0004FEA4
	public void setAutoJump()
	{
		int num = ((MovePoint)this.vMovePoints.firstElement()).xEnd - this.cx;
		this.cvyJump = -10;
		this.cp1 = 0;
		this.cdir = ((num <= 0) ? -1 : 1);
		if (num <= 6)
		{
			this.cvx = 0;
		}
		else if (num <= 20)
		{
			this.cvx = 3;
		}
		else
		{
			this.cvx = 5;
		}
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x00051D20 File Offset: 0x0004FF20
	public void updateCharStand()
	{
		this.isSoundJump = false;
		this.isAttack = false;
		this.isAttFly = false;
		this.cvx = 0;
		this.cvy = 0;
		this.cp1++;
		if (this.cp1 > 30)
		{
			this.cp1 = 0;
		}
		if (this.cp1 % 15 < 5)
		{
			this.cf = 0;
		}
		else
		{
			this.cf = 1;
		}
		this.updateCharInBridge();
		if (!this.me)
		{
			this.cp3++;
			if (this.cp3 > 50)
			{
				this.cp3 = 0;
				this.currentMovePoint = null;
			}
		}
		this.updateSuperEff();
		if (this.me && GameScr.vCharInMap.size() != 0 && TileMap.mapID == 50)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(0);
			if (!@char.changePos)
			{
				if (@char.statusMe != 2)
				{
					@char.moveTo(this.cx - 45, this.cy, 0);
				}
				@char.lastUpdateTime = mSystem.currentTimeMillis();
				if (Res.abs(this.cx - 45 - @char.cx) <= 10)
				{
					@char.changePos = true;
				}
			}
			else
			{
				if (@char.statusMe != 2)
				{
					@char.moveTo(this.cx + 45, this.cy, 0);
				}
				@char.lastUpdateTime = mSystem.currentTimeMillis();
				if (Res.abs(this.cx + 45 - @char.cx) <= 10)
				{
					@char.changePos = false;
				}
			}
			if (GameCanvas.gameTick % 100 == 0)
			{
				@char.addInfo("Cắc cùm cum");
			}
		}
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x00051ED4 File Offset: 0x000500D4
	public void updateSuperEff()
	{
		if (this.isCopy || this.isFusion || this.isSetPos || this.isPet || this.isMiniPet || (int)this.isMonkey == 1)
		{
			return;
		}
		if (this.me && !global::Char.isPaintAura2)
		{
			return;
		}
		this.ty++;
		if (this.clevel < 9 || this.clevel >= 14)
		{
			return;
		}
		if ((this.ty == 40 || this.ty == 50) && !GameCanvas.lowGraphic)
		{
			GameCanvas.gI().startDust(-1, this.cx + 8, this.cy);
			GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
			this.addDustEff(1);
		}
		if (this.ty <= 50)
		{
			return;
		}
		int num = this.cgender;
		if (num != 0)
		{
			if (num != 1)
			{
				if (num == 2)
				{
					if (GameCanvas.gameTick % 4 == 0)
					{
						ServerEffect.addServerEffect(131, this, 1);
					}
					if (this.clevel >= 13 && GameCanvas.gameTick % 25 == 0)
					{
						ServerEffect.addServerEffect(114, this, 1);
					}
				}
			}
			else
			{
				if (GameCanvas.gameTick % 4 == 0)
				{
					ServerEffect.addServerEffect(132, this, 1);
				}
				if (this.clevel >= 13 && GameCanvas.gameTick % 12 == 0)
				{
					ServerEffect.addServerEffect(114, this, 1);
				}
				if (this.clevel >= 13 && GameCanvas.gameTick % 25 == 0)
				{
					ServerEffect.addServerEffect(131, this, 1);
				}
			}
		}
		else
		{
			if (GameCanvas.gameTick % 25 == 0)
			{
				ServerEffect.addServerEffect(114, this, 1);
			}
			if (this.clevel >= 13 && GameCanvas.gameTick % 4 == 0)
			{
				ServerEffect.addServerEffect(132, this, 1);
			}
		}
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x000520FC File Offset: 0x000502FC
	public float getSoundVolumn()
	{
		if (this.me)
		{
			return 0.1f;
		}
		int num = Res.abs(global::Char.myChar.cx - this.cx);
		if (num >= 0 && num <= 50)
		{
			return 0.1f;
		}
		return 0.05f;
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x0005214C File Offset: 0x0005034C
	public void updateCharRun()
	{
		int num = ((int)this.isMonkey != 1 || this.me) ? 1 : 1;
		if (this.cx >= GameScr.cmx && this.cx <= GameScr.cmx + GameCanvas.w)
		{
			if ((int)this.isMonkey == 0)
			{
				SoundMn.gI().charRun(this.getSoundVolumn());
			}
			else
			{
				SoundMn.gI().monkeyRun(this.getSoundVolumn());
			}
		}
		this.ty = 0;
		this.isFreez = false;
		if (this.isCharge)
		{
			this.isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		int num2 = 0;
		if (!this.me && this.currentMovePoint != null)
		{
			num2 = global::Char.abs(this.cx - this.currentMovePoint.xEnd);
		}
		this.cp1++;
		if (this.cp1 >= 10)
		{
			this.cp1 = 0;
			this.cBonusSpeed = 0;
		}
		this.cf = (this.cp1 >> 1) + 2;
		if ((TileMap.tileTypeAtPixel(this.cx, this.cy - 1) & 64) == 64)
		{
			this.cx += this.cvx * num >> 1;
		}
		else
		{
			this.cx += this.cvx * num;
		}
		if (this.cdir == 1)
		{
			if (TileMap.tileTypeAt(this.cx + this.chw, this.cy - this.chh, 4))
			{
				if (this.me)
				{
					this.cvx = 0;
					this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
				}
				else
				{
					this.stop();
				}
			}
		}
		else if (TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - this.chh, 8))
		{
			if (this.me)
			{
				this.cvx = 0;
				this.cx = TileMap.tileXofPixel(this.cx - this.chw - 1) + (int)TileMap.size + this.chw;
			}
			else
			{
				this.stop();
			}
		}
		if (this.me)
		{
			if (this.cvx > 0)
			{
				this.cvx--;
			}
			else if (this.cvx < 0)
			{
				this.cvx++;
			}
			else
			{
				if (this.cx - this.cxSend != 0 && this.me)
				{
					Service.gI().charMove();
				}
				this.statusMe = 1;
				this.cBonusSpeed = 0;
			}
		}
		if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
		{
			if (this.me)
			{
				if (this.cx - this.cxSend != 0 || this.cy - this.cySend != 0)
				{
					Service.gI().charMove();
				}
				this.cf = 7;
				this.statusMe = 4;
				this.delayFall = 0;
				this.cvx = 3 * this.cdir;
				this.cp2 = 0;
			}
			else
			{
				this.stop();
			}
		}
		if (!this.me)
		{
			if (this.currentMovePoint != null)
			{
				int num3 = global::Char.abs(this.cx - this.currentMovePoint.xEnd);
				if (num3 > num2)
				{
					this.stop();
				}
			}
		}
		GameCanvas.gI().startDust(this.cdir, this.cx - (this.cdir << 3), this.cy);
		this.updateCharInBridge();
		this.addDustEff(2);
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x00052514 File Offset: 0x00050714
	private void stop()
	{
		this.statusMe = 6;
		this.cp3 = 0;
		this.cvx = 0;
		this.cvy = 0;
		this.cp1 = (this.cp2 = 0);
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0000430E File Offset: 0x0000250E
	public static int abs(int i)
	{
		return (i <= 0) ? (-i) : i;
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x00052550 File Offset: 0x00050750
	public void updateCharJump()
	{
		this.setMountIsStart();
		this.ty = 0;
		this.isFreez = false;
		if (this.isCharge)
		{
			this.isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		this.addDustEff(3);
		this.cx += this.cvx;
		this.cy += this.cvy;
		if (this.cy < 0)
		{
			this.cy = 0;
			this.cvy = -1;
		}
		this.cvy++;
		if (this.cvy > 0)
		{
			this.cvy = 0;
		}
		if (!this.me && this.currentMovePoint != null)
		{
			int num = this.currentMovePoint.xEnd - this.cx;
			if (num > 0)
			{
				if (this.cvx > num)
				{
					this.cvx = num;
				}
				if (this.cvx < 0)
				{
					this.cvx = num;
				}
			}
			else if (num < 0)
			{
				if (this.cvx < num)
				{
					this.cvx = num;
				}
				if (this.cvx > 0)
				{
					this.cvx = num;
				}
			}
			else
			{
				this.cvx = num;
			}
		}
		if (this.cdir == 1)
		{
			if ((TileMap.tileTypeAtPixel(this.cx + this.chw, this.cy - 1) & 4) == 4 && this.cx <= TileMap.tileXofPixel(this.cx + this.chw) + 12)
			{
				this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
				this.cvx = 0;
			}
		}
		else if ((TileMap.tileTypeAtPixel(this.cx - this.chw, this.cy - 1) & 8) == 8 && this.cx >= TileMap.tileXofPixel(this.cx - this.chw) + 12)
		{
			this.cx = TileMap.tileXofPixel(this.cx + 24 - this.chw) + this.chw;
			this.cvx = 0;
		}
		if (this.cvy == 0)
		{
			if (!this.isAttFly)
			{
				if (this.me)
				{
					this.setCharFallFromJump();
				}
				else
				{
					this.stop();
				}
			}
			else
			{
				this.setCharFallFromJump();
			}
		}
		if (this.me && !global::Char.ischangingMap && this.isInWaypoint())
		{
			if (Mod.DungPham.KoiOctiiu957.MainMod.isLockMap && !Mod.DungPham.KoiOctiiu957.AutoMap.isAutoChangeMap)
			{
				// Ignore waypoint when map is locked
			}
			else
			{
				Service.gI().charMove();
				if (TileMap.isTrainingMap())
				{
					global::Char.ischangingMap = true;
					Service.gI().getMapOffline();
				}
				else
				{
					Service.gI().requestChangeMap();
				}
				global::Char.isLockKey = true;
				global::Char.ischangingMap = true;
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				InfoDlg.showWait();
				return;
			}
		}
		if (this.statusMe != 16 && (TileMap.tileTypeAt(this.cx, this.cy - this.ch + 24, 8192) || this.cy < 0))
		{
			this.statusMe = 4;
			this.cp1 = 0;
			this.cp2 = 0;
			this.cvy = 1;
			this.delayFall = 0;
			if (this.cy < 0)
			{
				this.cy = 0;
			}
			this.cy = TileMap.tileYofPixel(this.cy + 25);
			GameCanvas.clearKeyHold();
		}
		if (this.cp3 < 0)
		{
			this.cp3++;
		}
		this.cf = 7;
		if (!this.me)
		{
			if (this.currentMovePoint != null && this.cy < this.currentMovePoint.yEnd)
			{
				this.stop();
			}
		}
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x00007179 File Offset: 0x00005379
	public bool checkInRangeJump(int x1, int xw1, int xmob, int y1, int yh1, int ymob)
	{
		return xmob <= xw1 && xmob >= x1 && ymob <= y1 && ymob >= yh1;
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0005290C File Offset: 0x00050B0C
	public void setCharFallFromJump()
	{
		this.cyStartFall = this.cy;
		this.cp1 = 0;
		this.cp2 = 0;
		this.statusMe = 10;
		this.cvx = this.cdir << 2;
		this.cvy = 0;
		this.cy = TileMap.tileYofPixel(this.cy) + 12;
		if (this.me && (this.cx - this.cxSend != 0 || this.cy - this.cySend != 0) && (Res.abs(global::Char.myCharz().cx - global::Char.myCharz().cxSend) > 96 || Res.abs(global::Char.myCharz().cy - global::Char.myCharz().cySend) > 24))
		{
			Service.gI().charMove();
		}
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x000529E0 File Offset: 0x00050BE0
	public void updateCharFall()
	{
		if (this.holder)
		{
			return;
		}
		this.ty = 0;
		if (this.cy + 4 >= TileMap.pxh)
		{
			this.statusMe = 1;
			if (this.me)
			{
				SoundMn.gI().charFall();
			}
			this.cvx = (this.cvy = 0);
			this.cp3 = 0;
			return;
		}
		if (this.cy % 24 == 0 && (TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
		{
			this.delayFall = 0;
			if (this.me)
			{
				if (this.cy - this.cySend > 0)
				{
					Service.gI().charMove();
				}
				else if (this.cx - this.cxSend != 0 || this.cy - this.cySend < 0)
				{
					Service.gI().charMove();
				}
				this.cvx = (this.cvy = 0);
				this.cp1 = (this.cp2 = 0);
				this.statusMe = 1;
				this.cp3 = 0;
				return;
			}
			this.stop();
			this.cf = 0;
			GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
			GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
			this.addDustEff(1);
		}
		if (this.delayFall > 0)
		{
			this.delayFall--;
			if (this.delayFall % 10 > 5)
			{
				this.cy++;
			}
			else
			{
				this.cy--;
			}
			return;
		}
		if (this.cvy < -4)
		{
			this.cf = 7;
		}
		else
		{
			this.cf = 12;
		}
		this.cx += this.cvx;
		if (!this.me && this.currentMovePoint != null)
		{
			int num = this.currentMovePoint.xEnd - this.cx;
			if (num > 0)
			{
				if (this.cvx > num)
				{
					this.cvx = num;
				}
				if (this.cvx < 0)
				{
					this.cvx = num;
				}
			}
			else if (num < 0)
			{
				if (this.cvx < num)
				{
					this.cvx = num;
				}
				if (this.cvx > 0)
				{
					this.cvx = num;
				}
			}
			else
			{
				this.cvx = num;
			}
		}
		this.cvy++;
		if (this.cvy > 8)
		{
			this.cvy = 8;
		}
		if (this.skillPaintRandomPaint == null)
		{
			this.cy += this.cvy;
		}
		if (this.cdir == 1)
		{
			if ((TileMap.tileTypeAtPixel(this.cx + this.chw, this.cy - 1) & 4) == 4 && this.cx <= TileMap.tileXofPixel(this.cx + this.chw) + 12)
			{
				this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
				this.cvx = 0;
			}
		}
		else if ((TileMap.tileTypeAtPixel(this.cx - this.chw, this.cy - 1) & 8) == 8 && this.cx >= TileMap.tileXofPixel(this.cx - this.chw) + 12)
		{
			this.cx = TileMap.tileXofPixel(this.cx + 24 - this.chw) + this.chw;
			this.cvx = 0;
		}
		if (this.cvy > 3 && (this.cyStartFall == 0 || this.cyStartFall <= TileMap.tileYofPixel(this.cy + 3)) && (TileMap.tileTypeAtPixel(this.cx, this.cy + 3) & 2) == 2)
		{
			if (this.me)
			{
				this.cyStartFall = 0;
				this.cvx = (this.cvy = 0);
				this.cp1 = (this.cp2 = 0);
				this.cy = TileMap.tileXofPixel(this.cy + 3);
				this.statusMe = 1;
				if (this.me)
				{
					SoundMn.gI().charFall();
				}
				this.cp3 = 0;
				GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
				GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
				this.addDustEff(1);
				if (this.cy - this.cySend > 0)
				{
					if (this.me)
					{
						Service.gI().charMove();
					}
				}
				else if ((this.cx - this.cxSend != 0 || this.cy - this.cySend < 0) && this.me)
				{
					Service.gI().charMove();
				}
			}
			else
			{
				this.stop();
				this.cy = TileMap.tileXofPixel(this.cy + 3);
				this.cf = 0;
				GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
				GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
				this.addDustEff(1);
				this.currentMovePoint = null;
			}
			return;
		}
		this.cf = 12;
		if (this.me)
		{
			if (this.isAttack)
			{
				return;
			}
		}
		else
		{
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 1) & 2) == 2)
			{
				this.cf = 0;
			}
			if (this.currentMovePoint != null && this.cy > this.currentMovePoint.yEnd)
			{
				this.stop();
				this.cy = TileMap.tileXofPixel(this.cy + 3);
				this.currentMovePoint = null;
			}
		}
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x00052FC4 File Offset: 0x000511C4
	public void updateCharFly()
	{
		int num = ((int)this.isMonkey != 1 || this.me) ? 1 : 2;
		this.setMountIsStart();
		if (this.statusMe != 16 && (TileMap.tileTypeAt(this.cx, this.cy - this.ch + 24, 8192) || this.cy < 0))
		{
			if (this.cy - this.ch < 0)
			{
				this.cy = this.ch;
			}
			this.cf = 7;
			this.statusMe = 4;
			this.cvx = 0;
			this.cp2 = 0;
			this.currentMovePoint = null;
			return;
		}
		int num2 = this.cy;
		if (this.isHead_Fly(this.head))
		{
			if (GameCanvas.gameTick % 3 == 0)
			{
				this.cp1++;
			}
			if (this.cp1 > 4)
			{
				this.cp1 = 0;
			}
			this.cf = this.cp1 + 2;
		}
		else
		{
			this.cp1++;
			if (this.cp1 >= 9)
			{
				this.cp1 = 0;
				if (!this.me)
				{
					this.cvx = (this.cvy = 0);
				}
				this.cBonusSpeed = 0;
			}
			this.cf = 8;
			if (Res.abs(this.cvx) <= 4 && this.me)
			{
				if (this.currentMovePoint != null)
				{
					int num3 = global::Char.abs(this.cx - this.currentMovePoint.xEnd);
					int num4 = global::Char.abs(this.cy - this.currentMovePoint.yEnd);
					if (num3 > num4 * 10)
					{
						this.cf = 8;
					}
					else if (num3 > num4 && num3 > 48 && num4 > 32)
					{
						this.cf = 8;
					}
					else
					{
						this.cf = 7;
					}
				}
				else
				{
					if (this.cvy < 0)
					{
						this.cvy = 0;
					}
					if (this.cvy > 16)
					{
						this.cvy = 16;
					}
					this.cf = 7;
				}
			}
			if (!this.me)
			{
				if (global::Char.abs(this.cvx) < 2)
				{
					this.cvx = (this.cdir << 1) * num;
				}
				if (this.cvy != 0)
				{
					this.cf = 7;
				}
				if (global::Char.abs(this.cvx) <= 2)
				{
					this.cp2++;
					if (this.cp2 > 32)
					{
						this.statusMe = 4;
						this.cvx = 0;
						this.cvy = 0;
					}
				}
			}
		}
		if (this.cdir == 1)
		{
			if (TileMap.tileTypeAt(this.cx + this.chw, this.cy - 1, 4))
			{
				this.cvx = 0;
				this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
				if (this.cvy == 0)
				{
					this.currentMovePoint = null;
				}
			}
		}
		else if (TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - 1, 8))
		{
			this.cvx = 0;
			this.cx = TileMap.tileXofPixel(this.cx - this.chw - 1) + (int)TileMap.size + this.chw;
			if (this.cvy == 0)
			{
				this.currentMovePoint = null;
			}
		}
		this.cx += this.cvx * num;
		this.cy += this.cvy * num;
		if (!this.isMount && num2 - this.cy == 0)
		{
			this.ty++;
			this.wt++;
			this.fy += (this.wy ? -1 : 1);
			if (this.wt == 10)
			{
				this.wt = 0;
				this.wy = !this.wy;
			}
			if (this.ty > 20)
			{
				this.delayFall = 10;
				if (GameCanvas.gameTick % 3 == 0)
				{
					ServerEffect.addServerEffect(111, this.cx + ((this.cdir != 1) ? 27 : -17), this.cy + this.fy + 13, 1, (this.cdir == 1) ? 0 : 2);
				}
			}
		}
		if (this.me)
		{
			if (this.cvx > 0)
			{
				this.cvx--;
			}
			else if (this.cvx < 0)
			{
				this.cvx++;
			}
			else if (this.cvy == 0)
			{
				this.statusMe = 4;
				this.checkDelayFallIfTooHigh();
				Service.gI().charMove();
			}
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 20) & 2) == 2 || (TileMap.tileTypeAtPixel(this.cx, this.cy + 40) & 2) == 2)
			{
				if (this.cvy == 0)
				{
					this.delayFall = 0;
				}
				this.cyStartFall = 0;
				this.cvx = (this.cvy = 0);
				this.cp1 = (this.cp2 = 0);
				this.statusMe = 4;
				this.addDustEff(3);
			}
			if (global::Char.abs(this.cx - this.cxSend) > 96 || global::Char.abs(this.cy - this.cySend) > 24)
			{
				Service.gI().charMove();
			}
		}
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x00053568 File Offset: 0x00051768
	private bool isHead_Fly(int head2)
	{
		if (global::Char.Arr_Head_FlyMove.Length > 0)
		{
			for (int i = 0; i < global::Char.Arr_Head_FlyMove.Length; i++)
			{
				if ((int)global::Char.Arr_Head_FlyMove[i] == head2)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x000535AC File Offset: 0x000517AC
	public void setMount(int cid, int ctrans, int cgender)
	{
		this.idcharMount = cid;
		this.transMount = ctrans;
		this.genderMount = cgender;
		this.speedMount = 30;
		if (this.transMount < 0)
		{
			this.transMount = 0;
			this.xMount = GameScr.cmx + GameCanvas.w + 50;
			this.dxMount = -19;
		}
		else if (this.transMount == 1)
		{
			this.transMount = 2;
			this.xMount = GameScr.cmx - 100;
			this.dxMount = -33;
		}
		this.dyMount = -17;
		this.yMount = this.cy;
		this.frameMount = 0;
		this.frameNewMount = 0;
		this.isMount = false;
		this.isEndMount = false;
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x00053664 File Offset: 0x00051864
	public void updateMount()
	{
		this.frameMount++;
		if (this.frameMount > this.FrameMount.Length - 1)
		{
			this.frameMount = 0;
		}
		this.frameNewMount++;
		if (this.frameNewMount > 1000)
		{
			this.frameNewMount = 0;
		}
		if (this.isStartMount && !this.isMount)
		{
			this.yMount = this.cy;
			if (this.transMount == 0)
			{
				if (this.xMount - this.cx >= this.speedMount)
				{
					this.xMount -= this.speedMount;
				}
				else
				{
					this.xMount = this.cx;
					this.isMount = true;
					this.isEndMount = false;
				}
			}
			else if (this.transMount == 2)
			{
				if (this.cx - this.xMount >= this.speedMount)
				{
					this.xMount += this.speedMount;
				}
				else
				{
					this.xMount = this.cx;
					this.isMount = true;
					this.isEndMount = false;
				}
			}
		}
		else if (this.isMount)
		{
			if (this.statusMe == 14 || this.ySd - this.cy < 24)
			{
				this.setMountIsEnd();
			}
			if (this.cp1 % 15 < 5)
			{
				this.cf = 0;
			}
			else
			{
				this.cf = 1;
			}
			this.transMount = this.cdir;
			this.updateSuperEff();
			if (this.transMount < 0)
			{
				this.transMount = 0;
				this.dxMount = -19;
			}
			else if (this.transMount == 1)
			{
				this.transMount = 2;
				this.dxMount = -31;
				if (this.isEventMount)
				{
					this.dxMount = -38;
				}
			}
			if (this.skillInfoPaint() != null)
			{
				this.dyMount = -15;
			}
			else
			{
				this.dyMount = -17;
			}
			this.yMount = this.cy;
			this.xMount = this.cx;
		}
		else if (this.isEndMount)
		{
			if (this.transMount == 0)
			{
				if (this.xMount > GameScr.cmx - 100)
				{
					this.xMount -= 20;
				}
				else
				{
					this.isStartMount = false;
					this.isMount = false;
					this.isEndMount = false;
				}
			}
			else if (this.transMount == 2)
			{
				if (this.xMount < GameScr.cmx + GameCanvas.w + 50)
				{
					this.xMount += 20;
				}
				else
				{
					this.isStartMount = false;
					this.isMount = false;
					this.isEndMount = false;
				}
			}
		}
		else if (!this.isStartMount || !this.isMount || !this.isEndMount)
		{
			this.xMount = GameScr.cmx - 100;
			this.yMount = GameScr.cmy - 100;
		}
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x00053970 File Offset: 0x00051B70
	public void getMountData()
	{
		if (Mob.arrMobTemplate[50].data == null)
		{
			Mob.arrMobTemplate[50].data = new EffectData();
			string text = "/Mob/" + 50;
			DataInputStream dataInputStream = MyStream.readFile(text);
			if (dataInputStream != null)
			{
				Mob.arrMobTemplate[50].data.readData(text + "/data");
				Mob.arrMobTemplate[50].data.img = GameCanvas.loadImage(text + "/img.png");
			}
			else
			{
				Service.gI().requestModTemplate(50);
			}
			Mob.lastMob.addElement(50 + string.Empty);
		}
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x0000719E File Offset: 0x0000539E
	public void checkFrameTick(int[] array)
	{
		this.t++;
		if (this.t > array.Length - 1)
		{
			this.t = 0;
		}
		this.fM = array[this.t];
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x00053A30 File Offset: 0x00051C30
	public void paintMount1(mGraphics g)
	{
		if (this.xMount > GameScr.cmx && this.xMount < GameScr.cmx + GameCanvas.w)
		{
			if (this.me)
			{
				if (this.isEndMount || this.isStartMount || this.isMount)
				{
					if (this.idMount >= global::Char.ID_NEW_MOUNT)
					{
						string nameImg = this.strMount + (int)(this.idMount - global::Char.ID_NEW_MOUNT) + "_0";
						FrameImage fraImage = mSystem.getFraImage(nameImg);
						if (fraImage == null && this.me)
						{
							int num = -1;
							if (Mod.DungPham.KoiOctiiu957.ModSkin.TryGetBoardFallbackMountImageIndex(out num))
							{
								fraImage = mSystem.getFraImage(this.strMount + num.ToString() + "_0");
							}
						}
						if (fraImage != null)
						{
							int num2 = Mod.DungPham.KoiOctiiu957.ModSkin.ShouldAnimateBoard() ? (this.frameNewMount / 2 % fraImage.nFrame) : 0;
							fraImage.drawFrame(num2, this.xMount, this.yMount + this.fy, this.transMount, 3, g);
						}
						return;
					}
					if (this.isSpeacialMount)
					{
						return;
					}
					if (this.isEventMount)
					{
						int num3 = Mod.DungPham.KoiOctiiu957.ModSkin.ShouldAnimateBoard() ? ((int)this.FrameMount[this.frameMount] * 60) : ((int)this.FrameMount[0] * 60);
						g.drawRegion(global::Char.imgEventMountWing, 0, num3, 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					if (this.genderMount == 2)
					{
						if (!this.isMountVip)
						{
							int num4 = Mod.DungPham.KoiOctiiu957.ModSkin.ShouldAnimateBoard() ? ((int)this.FrameMount[this.frameMount] * 40) : ((int)this.FrameMount[0] * 40);
							g.drawRegion(global::Char.imgMount_XD, 0, num4, 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						}
						else
						{
							int num5 = Mod.DungPham.KoiOctiiu957.ModSkin.ShouldAnimateBoard() ? ((int)this.FrameMount[this.frameMount] * 40) : ((int)this.FrameMount[0] * 40);
							g.drawRegion(global::Char.imgMount_XD_VIP, 0, num5, 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						}
					}
					else if (this.genderMount == 1)
					{
						if (!this.isMountVip)
						{
							int num6 = Mod.DungPham.KoiOctiiu957.ModSkin.ShouldAnimateBoard() ? ((int)this.FrameMount[this.frameMount] * 40) : ((int)this.FrameMount[0] * 40);
							g.drawRegion(global::Char.imgMount_NM, 0, num6, 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						}
						else
						{
							int num7 = Mod.DungPham.KoiOctiiu957.ModSkin.ShouldAnimateBoard() ? ((int)this.FrameMount[this.frameMount] * 40) : ((int)this.FrameMount[0] * 40);
							g.drawRegion(global::Char.imgMount_NM_VIP, 0, num7, 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						}
					}
				}
			}
			else if (!this.me)
			{
				if (this.idMount >= global::Char.ID_NEW_MOUNT)
				{
					string nameImg2 = this.strMount + (int)(this.idMount - global::Char.ID_NEW_MOUNT) + "_0";
					FrameImage fraImage2 = mSystem.getFraImage(nameImg2);
					if (fraImage2 != null)
					{
						fraImage2.drawFrame(this.frameNewMount / 2 % fraImage2.nFrame, this.xMount, this.yMount + this.fy, this.transMount, 3, g);
					}
					return;
				}
				if (this.isSpeacialMount)
				{
					return;
				}
				if (this.isEventMount)
				{
					g.drawRegion(global::Char.imgEventMountWing, 0, (int)this.FrameMount[this.frameMount] * 60, 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
					return;
				}
				if (this.isMount)
				{
					if (this.genderMount == 2)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(global::Char.imgMount_XD, 0, (int)this.FrameMount[this.frameMount] * 40, 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						}
						else
						{
							g.drawRegion(global::Char.imgMount_XD_VIP, 0, (int)this.FrameMount[this.frameMount] * 40, 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						}
					}
					else if (this.genderMount == 1)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(global::Char.imgMount_NM, 0, (int)this.FrameMount[this.frameMount] * 40, 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						}
						else
						{
							g.drawRegion(global::Char.imgMount_NM_VIP, 0, (int)this.FrameMount[this.frameMount] * 40, 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x00053F10 File Offset: 0x00052110
	public void paintMount2(mGraphics g)
	{
		if (this.xMount > GameScr.cmx && this.xMount < GameScr.cmx + GameCanvas.w)
		{
			if (this.me)
			{
				if (this.isEndMount || this.isStartMount || this.isMount)
				{
					if (this.idMount >= global::Char.ID_NEW_MOUNT)
					{
						string nameImg = this.strMount + (int)(this.idMount - global::Char.ID_NEW_MOUNT) + "_1";
						FrameImage fraImage = mSystem.getFraImage(nameImg);
						if (fraImage == null && this.me)
						{
							int num = -1;
							if (Mod.DungPham.KoiOctiiu957.ModSkin.TryGetBoardFallbackMountImageIndex(out num))
							{
								fraImage = mSystem.getFraImage(this.strMount + num.ToString() + "_1");
							}
						}
						if (fraImage != null)
						{
							int num2 = Mod.DungPham.KoiOctiiu957.ModSkin.ShouldAnimateBoard() ? (this.frameNewMount / 2 % fraImage.nFrame) : 0;
							fraImage.drawFrame(num2, this.xMount, this.yMount + this.fy, this.transMount, 3, g);
						}
						return;
					}
					if (this.isSpeacialMount)
					{
						if (Mod.DungPham.KoiOctiiu957.ModSkin.ShouldAnimateBoard())
						{
							this.checkFrameTick(this.move);
						}
						else
						{
							this.fM = this.move[0];
						}
						if (Mob.arrMobTemplate[50] != null && Mob.arrMobTemplate[50].data != null)
						{
							Mob.arrMobTemplate[50].data.paintFrame(g, this.fM, this.xMount + ((this.cdir != 1) ? 8 : -8), this.yMount + 35, (this.cdir != 1) ? 1 : 0, 0);
						}
						else
						{
							this.getMountData();
						}
						return;
					}
					if (this.isEventMount)
					{
						int num3 = Mod.DungPham.KoiOctiiu957.ModSkin.ShouldAnimateBoard() ? ((int)this.FrameMount[this.frameMount] * 60) : ((int)this.FrameMount[0] * 60);
						g.drawRegion(global::Char.imgEventMount, 0, num3, 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					if (this.genderMount == 0)
					{
						if (!this.isMountVip)
						{
							int num4 = Mod.DungPham.KoiOctiiu957.ModSkin.ShouldAnimateBoard() ? ((int)this.FrameMount[this.frameMount] * 40) : ((int)this.FrameMount[0] * 40);
							g.drawRegion(global::Char.imgMount_TD, 0, num4, 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						}
						else
						{
							int num5 = Mod.DungPham.KoiOctiiu957.ModSkin.ShouldAnimateBoard() ? ((int)this.FrameMount[this.frameMount] * 40) : ((int)this.FrameMount[0] * 40);
							g.drawRegion(global::Char.imgMount_TD_VIP, 0, num5, 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						}
					}
					else if (this.genderMount == 1)
					{
						if (!this.isMountVip)
						{
							int num6 = Mod.DungPham.KoiOctiiu957.ModSkin.ShouldAnimateBoard() ? ((int)this.FrameMount[this.frameMount] * 40) : ((int)this.FrameMount[0] * 40);
							g.drawRegion(global::Char.imgMount_NM_1, 0, num6, 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						}
						else
						{
							int num7 = Mod.DungPham.KoiOctiiu957.ModSkin.ShouldAnimateBoard() ? ((int)this.FrameMount[this.frameMount] * 40) : ((int)this.FrameMount[0] * 40);
							g.drawRegion(global::Char.imgMount_NM_1_VIP, 0, num7, 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						}
					}
				}
			}
			else if (!this.me)
			{
				if (this.idMount >= global::Char.ID_NEW_MOUNT)
				{
					string nameImg2 = this.strMount + (int)(this.idMount - global::Char.ID_NEW_MOUNT) + "_1";
					FrameImage fraImage2 = mSystem.getFraImage(nameImg2);
					if (fraImage2 != null)
					{
						fraImage2.drawFrame(this.frameNewMount / 2 % fraImage2.nFrame, this.xMount, this.yMount + this.fy, this.transMount, 3, g);
					}
					return;
				}
				if (this.isSpeacialMount)
				{
					this.checkFrameTick(this.move);
					if (Mob.arrMobTemplate[50] != null && Mob.arrMobTemplate[50].data != null)
					{
						Mob.arrMobTemplate[50].data.paintFrame(g, this.fM, this.xMount + ((this.cdir != 1) ? 8 : -8), this.yMount + 35, (this.cdir != 1) ? 1 : 0, 0);
					}
					else
					{
						this.getMountData();
					}
					return;
				}
				if (this.isEventMount)
				{
					g.drawRegion(global::Char.imgEventMount, 0, (int)this.FrameMount[this.frameMount] * 60, 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
				}
				if (this.isMount)
				{
					if (this.genderMount == 0)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(global::Char.imgMount_TD, 0, (int)this.FrameMount[this.frameMount] * 40, 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						}
						else
						{
							g.drawRegion(global::Char.imgMount_TD_VIP, 0, (int)this.FrameMount[this.frameMount] * 40, 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						}
					}
					else if (this.genderMount == 1)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(global::Char.imgMount_NM_1, 0, (int)this.FrameMount[this.frameMount] * 40, 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						}
						else
						{
							g.drawRegion(global::Char.imgMount_NM_1_VIP, 0, (int)this.FrameMount[this.frameMount] * 40, 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x000544FC File Offset: 0x000526FC
	public void setMountIsStart()
	{
		if (this.me)
		{
			this.isHaveMount = this.checkHaveMount();
			if (TileMap.isVoDaiMap())
			{
				this.isHaveMount = false;
			}
		}
		if (this.isHaveMount)
		{
			if (this.ySd - this.cy <= 20)
			{
				this.xChar = this.cx;
			}
			if (this.xdis < 100)
			{
				this.xdis = Res.abs(this.xChar - this.cx);
			}
			if (this.xdis >= 70 && this.ySd - this.cy > 30 && !this.isStartMount && !this.isEndMount)
			{
				this.setMount(this.charID, this.cdir, this.cgender);
				this.isStartMount = true;
			}
		}
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x000071D3 File Offset: 0x000053D3
	public void setMountIsEnd()
	{
		if (this.ySd - this.cy < 24 && !this.isEndMount)
		{
			this.isStartMount = false;
			this.isMount = false;
			this.isEndMount = true;
			this.xdis = 0;
		}
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x000545D8 File Offset: 0x000527D8
	public bool checkHaveMount()
	{
		bool result = false;
		short num = -1;
		if (this.me && Mod.DungPham.KoiOctiiu957.ModSkin.TryGetBoardMountId(out num))
		{
			result = true;
		}
		Item[] array = this.arrItemBody;
		for (int i = 0; i < array.Length && !result; i++)
		{
			if (array[i] != null && ((int)array[i].template.type == 24 || (int)array[i].template.type == 23))
			{
				if (array[i].template.part >= 0)
				{
					num = (short)(global::Char.ID_NEW_MOUNT + array[i].template.part);
				}
				else
				{
					num = array[i].template.id;
				}
				result = true;
				break;
			}
		}
		this.ApplyMountFlags(num);
		return result;
	}

	private void ApplyMountFlags(short mountId)
	{
		this.isMountVip = false;
		this.isSpeacialMount = false;
		this.isEventMount = false;
		this.idMount = -1;
		if (mountId == 349 || mountId == 350 || mountId == 351)
		{
			this.isMountVip = true;
		}
		else if (mountId == 396)
		{
			this.isEventMount = true;
		}
		else if (mountId == 532)
		{
			this.isSpeacialMount = true;
		}
		else if (mountId >= global::Char.ID_NEW_MOUNT)
		{
			this.idMount = mountId;
		}
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x00054704 File Offset: 0x00052904
	private void checkDelayFallIfTooHigh()
	{
		bool flag = true;
		for (int i = 0; i < 150; i += 24)
		{
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy + i) & 2) == 2 || this.cy + i > TileMap.tmh * (int)TileMap.size - 24)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			this.delayFall = 40;
		}
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x00007210 File Offset: 0x00005410
	public void setDefaultPart()
	{
		this.setDefaultWeapon();
		this.setDefaultBody();
		this.setDefaultLeg();
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x00007224 File Offset: 0x00005424
	public void setDefaultWeapon()
	{
		if (this.cgender == 0)
		{
			this.wp = 0;
		}
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x00054778 File Offset: 0x00052978
	public void setDefaultBody()
	{
		if (this.cgender == 0)
		{
			this.body = 57;
		}
		else if (this.cgender == 1)
		{
			this.body = 59;
		}
		else if (this.cgender == 2)
		{
			this.body = 57;
		}
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x000547CC File Offset: 0x000529CC
	public void setDefaultLeg()
	{
		if (this.cgender == 0)
		{
			this.leg = 58;
		}
		else if (this.cgender == 1)
		{
			this.leg = 60;
		}
		else if (this.cgender == 2)
		{
			this.leg = 58;
		}
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x00007238 File Offset: 0x00005438
	public bool isSelectingSkillUseAlone()
	{
		return this.myskill != null && this.myskill.template.isUseAlone();
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x00007258 File Offset: 0x00005458
	public bool isUseSkillSpec()
	{
		return this.myskill != null && this.myskill.template.isSkillSpec();
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x00007278 File Offset: 0x00005478
	public bool isSelectingSkillBuffToPlayer()
	{
		return this.myskill != null && this.myskill.template.isBuffToPlayer();
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x00054820 File Offset: 0x00052A20
	public bool isUseChargeSkill()
	{
		return !this.isUseSkillAfterCharge && this.myskill != null && ((int)this.myskill.template.id == 10 || (int)this.myskill.template.id == 11);
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x00054878 File Offset: 0x00052A78
	public void setSkillPaint(SkillPaint skillPaint, int sType)
	{
		this.hasSendAttack = false;
		if (this.stone)
		{
			return;
		}
		if (this.me && (int)this.myskill.template.id == 9 && this.cHP <= this.cHPFull / 10L)
		{
			return;
		}
		if (this.me)
		{
			if (this.mobFocus == null && this.charFocus == null)
			{
				this.stopUseChargeSkill();
			}
			if (this.mobFocus != null && (this.mobFocus.status == 1 || this.mobFocus.status == 0))
			{
				this.stopUseChargeSkill();
			}
			if (this.charFocus != null && (this.charFocus.statusMe == 14 || this.charFocus.statusMe == 5))
			{
				this.stopUseChargeSkill();
			}
			if ((int)this.myskill.template.id == 23)
			{
				if (this.charFocus != null && this.charFocus.holdEffID != 0)
				{
					return;
				}
				if (this.mobFocus != null && this.mobFocus.holdEffID != 0)
				{
					return;
				}
				if (this.holdEffID != 0)
				{
					return;
				}
			}
			if (this.sleepEff || this.blindEff)
			{
				return;
			}
		}
		Res.outz("skill id= " + skillPaint.id);
		if (this.me && this.dart != null)
		{
			return;
		}
		if (TileMap.isOfflineMap())
		{
			return;
		}
		long num = mSystem.currentTimeMillis();
		if (this.me)
		{
			if (this.isSelectingSkillBuffToPlayer() && this.charFocus == null)
			{
				return;
			}
			if (num - this.myskill.lastTimeUseThisSkill < (long)this.myskill.coolDown)
			{
				this.myskill.paintCanNotUseSkill = true;
				return;
			}
			this.myskill.lastTimeUseThisSkill = num;
			if (this.myskill.template.manaUseType == 2)
			{
				this.cMP = 1L;
			}
			else if (this.myskill.template.manaUseType != 1)
			{
				this.cMP -= (long)this.myskill.manaUse;
			}
			else
			{
				this.cMP -= (long)this.myskill.manaUse * this.cMPFull / 100L;
			}
			global::Char.myCharz().cStamina--;
			GameScr.gI().isInjureMp = true;
			GameScr.gI().twMp = 0L;
			if (this.cMP < 0L)
			{
				this.cMP = 0L;
			}
		}
		if (this.me)
		{
			if ((int)this.myskill.template.id == 10)
			{
				Service.gI().skill_not_focus(4);
			}
			if ((int)this.myskill.template.id == 11)
			{
				Service.gI().skill_not_focus(4);
			}
			if ((int)this.myskill.template.id == 7)
			{
				SoundMn.gI().hoisinh();
			}
			if ((int)this.myskill.template.id == 6)
			{
				Service.gI().skill_not_focus(0);
				GameScr.gI().isUseFreez = true;
				SoundMn.gI().thaiduonghasan();
			}
			if ((int)this.myskill.template.id == 8)
			{
				if (!this.isCharge)
				{
					SoundMn.gI().taitaoPause();
					Service.gI().skill_not_focus(1);
					this.isCharge = true;
					this.last = (this.cur = mSystem.currentTimeMillis());
				}
				else
				{
					Service.gI().skill_not_focus(3);
					this.isCharge = false;
					SoundMn.gI().taitaoPause();
				}
			}
			if ((int)this.myskill.template.id == 13)
			{
				if ((int)this.isMonkey != 0)
				{
					GameScr.gI().auto = 0;
					return;
				}
				if (this.isCreateDark)
				{
					return;
				}
				SoundMn.gI().gong();
				Service.gI().skill_not_focus(6);
				this.chargeCount = 0;
				this.isWaitMonkey = true;
				return;
			}
			else
			{
				if ((int)this.myskill.template.id == 14)
				{
					SoundMn.gI().gong();
					Service.gI().skill_not_focus(7);
					this.useChargeSkill(true);
				}
				if ((int)this.myskill.template.id == 21)
				{
					Service.gI().skill_not_focus(10);
					return;
				}
				if ((int)this.myskill.template.id == 12)
				{
					Service.gI().skill_not_focus(8);
				}
				if ((int)this.myskill.template.id == 19)
				{
					Service.gI().skill_not_focus(9);
					return;
				}
			}
		}
		if ((int)this.isMonkey == 1 && skillPaint.id >= 35 && skillPaint.id <= 41)
		{
			skillPaint = GameScr.sks[106];
		}
		if (skillPaint.id >= 128 && skillPaint.id <= 134)
		{
			if (this.charFocus != null)
			{
				this.cx = this.charFocus.cx;
				this.cy = this.charFocus.cy;
				this.currentMovePoint = null;
			}
			if (this.mobFocus != null)
			{
				this.cx = this.mobFocus.x;
				this.cy = this.mobFocus.y;
				this.currentMovePoint = null;
			}
			ServerEffect.addServerEffect(60, this.cx, this.cy, 1);
			this.telePortSkill = true;
			if (this.me && !this.hasSendAttack)
			{
				MyVector myVector = new MyVector();
				MyVector myVector2 = new MyVector();
				int type = 0;
				if (this.mobFocus != null)
				{
					myVector.addElement(this.mobFocus);
					type = 1;
				}
				else if (this.charFocus != null)
				{
					myVector2.addElement(this.charFocus);
					type = 2;
				}
				Service.gI().sendPlayerAttack(myVector, myVector2, type);
				this.hasSendAttack = true;
			}
			this.skillPaint = null;
			this.skillPaintRandomPaint = null;
			return;
		}
		if (skillPaint.id >= 107 && skillPaint.id <= 113)
		{
			skillPaint = GameScr.sks[skillPaint.id - 44];
			EffecMn.addEff(new Effect(23, this.cx, this.cy + this.ch / 2, 3, 2, 1));
		}
		this.setAutoSkillPaint(skillPaint, sType);
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x00054E80 File Offset: 0x00053080
	public void useSkillNotFocus()
	{
		GameScr.gI().auto = 0;
		global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], (!TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2)) ? 1 : 0);
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x00054EE0 File Offset: 0x000530E0
	public void sendUseChargeSkill()
	{
		if (this.me && (this.isFreez || this.isUsePlane))
		{
			GameScr.gI().auto = 0;
			return;
		}
		long num = mSystem.currentTimeMillis();
		if (this.me && num - this.myskill.lastTimeUseThisSkill < (long)this.myskill.coolDown)
		{
			this.myskill.paintCanNotUseSkill = true;
			return;
		}
		if ((int)this.myskill.template.id == 10)
		{
			this.useChargeSkill(false);
		}
		if ((int)this.myskill.template.id == 11)
		{
			this.useChargeSkill(true);
		}
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x00054F94 File Offset: 0x00053194
	public void stopUseChargeSkill()
	{
		this.isFlyAndCharge = false;
		this.isStandAndCharge = false;
		this.isUseSkillAfterCharge = false;
		this.isCreateDark = false;
		if (this.me && this.statusMe != 14 && this.statusMe != 5)
		{
			this.isLockMove = false;
		}
		GameScr.gI().auto = 0;
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x00054FF4 File Offset: 0x000531F4
	public void useChargeSkill(bool isGround)
	{
		if (this.isCreateDark)
		{
			return;
		}
		GameScr.gI().auto = 0;
		if (isGround)
		{
			if (!this.isStandAndCharge)
			{
				this.chargeCount = 0;
				this.seconds = 50000;
				this.posDisY = 0;
				this.last = mSystem.currentTimeMillis();
				if (this.me)
				{
					this.isLockMove = true;
					if (this.cgender == 1)
					{
						Service.gI().skill_not_focus(4);
					}
					if (TileMap.mapID == 170 && this.cgender != 1)
					{
						Service.gI().skill_not_focus(4);
					}
				}
				if (this.cgender == 1)
				{
					SoundMn.gI().gongName();
				}
				if (TileMap.mapID == 170 && this.cgender != 1)
				{
					SoundMn.gI().gongName();
				}
				this.isStandAndCharge = true;
			}
		}
		else if (!this.isFlyAndCharge)
		{
			if (this.me)
			{
				GameScr.gI().auto = 0;
				this.isLockMove = true;
				Service.gI().skill_not_focus(4);
			}
			this.isUseSkillAfterCharge = false;
			this.chargeCount = 0;
			this.isFlyAndCharge = true;
			this.posDisY = 0;
			this.seconds = 50000;
			this.isFlying = TileMap.tileTypeAt(this.cx, this.cy, 2);
		}
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x00055158 File Offset: 0x00053358
	public void setAutoSkillPaint(SkillPaint skillPaint, int sType)
	{
		this.skillPaint = skillPaint;
		Res.outz("set auto skill " + ((skillPaint == null) ? "null" : "ko null"));
		if (skillPaint.id >= 0 && skillPaint.id <= 6)
		{
			int num = Res.random(0, skillPaint.id + 4) - 1;
			if (num < 0)
			{
				num = 0;
			}
			if (num > 6)
			{
				num = 6;
			}
			this.skillPaintRandomPaint = GameScr.sks[num];
		}
		else if (skillPaint.id >= 14 && skillPaint.id <= 20)
		{
			int num2 = Res.random(0, skillPaint.id - 14 + 4) - 1;
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 > 6)
			{
				num2 = 6;
			}
			this.skillPaintRandomPaint = GameScr.sks[num2 + 14];
		}
		else if (skillPaint.id >= 28 && skillPaint.id <= 34)
		{
			int num3 = Res.random(0, (((int)this.isMonkey != 1) ? skillPaint.id : 105) - (((int)this.isMonkey != 1) ? 28 : 105) + 4) - 1;
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num3 > 6)
			{
				num3 = 6;
			}
			if ((int)this.isMonkey == 1)
			{
				num3 = 0;
			}
			this.skillPaintRandomPaint = GameScr.sks[num3 + (((int)this.isMonkey != 1) ? 28 : 105)];
		}
		else if (skillPaint.id >= 63 && skillPaint.id <= 69)
		{
			int num4 = Res.random(0, skillPaint.id - 63 + 4) - 1;
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num4 > 6)
			{
				num4 = 6;
			}
			this.skillPaintRandomPaint = GameScr.sks[num4 + 63];
		}
		else if (skillPaint.id >= 107 && skillPaint.id <= 109)
		{
			int num5 = Res.random(0, skillPaint.id - 107 + 4) - 1;
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num5 > 6)
			{
				num5 = 6;
			}
			this.skillPaintRandomPaint = GameScr.sks[num5 + 107];
		}
		else
		{
			this.skillPaintRandomPaint = skillPaint;
		}
		this.sType = sType;
		this.indexSkill = 0;
		this.i0 = (this.i1 = (this.i2 = (this.dx0 = (this.dx1 = (this.dx2 = (this.dy0 = (this.dy1 = (this.dy2 = 0))))))));
		this.eff0 = null;
		this.eff1 = null;
		this.eff2 = null;
		this.cvy = 0;
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x00007298 File Offset: 0x00005498
	public SkillInfoPaint[] skillInfoPaint()
	{
		if (this.skillPaint == null)
		{
			return null;
		}
		if (this.skillPaintRandomPaint == null)
		{
			return null;
		}
		if (this.sType == 0)
		{
			return this.skillPaintRandomPaint.skillStand;
		}
		return this.skillPaintRandomPaint.skillfly;
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x00055414 File Offset: 0x00053614
	public void setAttack()
	{
		if (this.me)
		{
			SkillPaint skillPaint = this.skillPaintRandomPaint;
			if (this.dart != null)
			{
				skillPaint = this.dart.skillPaint;
			}
			if (skillPaint != null)
			{
				MyVector myVector = new MyVector();
				MyVector myVector2 = new MyVector();
				if (this.charFocus != null)
				{
					myVector2.addElement(this.charFocus);
				}
				else if (this.mobFocus != null)
				{
					myVector.addElement(this.mobFocus);
				}
				this.effPaints = new EffectPaint[myVector.size() + myVector2.size()];
				for (int i = 0; i < myVector.size(); i++)
				{
					this.effPaints[i] = new EffectPaint();
					this.effPaints[i].effCharPaint = GameScr.efs[skillPaint.effectHappenOnMob - 1];
					if (!this.isSelectingSkillUseAlone())
					{
						this.effPaints[i].eMob = (Mob)myVector.elementAt(i);
					}
				}
				for (int j = 0; j < myVector2.size(); j++)
				{
					this.effPaints[j + myVector.size()] = new EffectPaint();
					this.effPaints[j + myVector.size()].effCharPaint = GameScr.efs[skillPaint.effectHappenOnMob - 1];
					this.effPaints[j + myVector.size()].eChar = (global::Char)myVector2.elementAt(j);
				}
				int type = 0;
				if (this.mobFocus != null)
				{
					type = 1;
				}
				else if (this.charFocus != null)
				{
					type = 2;
				}
				if (myVector.size() == 0 && myVector2.size() == 0)
				{
					this.stopUseChargeSkill();
				}
				if (this.me && !this.isSelectingSkillUseAlone() && !this.hasSendAttack)
				{
					Service.gI().sendPlayerAttack(myVector, myVector2, type);
					this.hasSendAttack = true;
				}
			}
		}
		else
		{
			SkillPaint skillPaint2 = this.skillPaintRandomPaint;
			if (this.dart != null)
			{
				skillPaint2 = this.dart.skillPaint;
			}
			if (skillPaint2 != null)
			{
				if (this.attMobs != null)
				{
					this.effPaints = new EffectPaint[this.attMobs.Length];
					for (int k = 0; k < this.attMobs.Length; k++)
					{
						this.effPaints[k] = new EffectPaint();
						this.effPaints[k].effCharPaint = GameScr.efs[skillPaint2.effectHappenOnMob - 1];
						this.effPaints[k].eMob = this.attMobs[k];
					}
					this.attMobs = null;
				}
				else if (this.attChars != null)
				{
					this.effPaints = new EffectPaint[this.attChars.Length];
					for (int l = 0; l < this.attChars.Length; l++)
					{
						this.effPaints[l] = new EffectPaint();
						this.effPaints[l].effCharPaint = GameScr.efs[skillPaint2.effectHappenOnMob - 1];
						this.effPaints[l].eChar = this.attChars[l];
					}
					this.attChars = null;
				}
			}
		}
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x000072D6 File Offset: 0x000054D6
	public bool isOutX()
	{
		return this.cx < GameScr.cmx || this.cx > GameScr.cmx + GameScr.gW;
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x00055730 File Offset: 0x00053930
	public bool isPaint()
	{
		return this.cy >= GameScr.cmy && this.cy <= GameScr.cmy + GameScr.gH + 30 && !this.isOutX() && !this.isSetPos && !this.isFusion;
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x00007303 File Offset: 0x00005503
	public void createShadow(int x, int y, int life)
	{
		this.shadowX = x;
		this.shadowY = y;
		this.shadowLife = life;
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x0000731A File Offset: 0x0000551A
	public void setMabuHold(bool m)
	{
		this.isMabuHold = m;
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x00055794 File Offset: 0x00053994
	public virtual void paint(mGraphics g)
	{
		if (this.isHide)
		{
			return;
		}
		if (this.isMafuba)
		{
			this.paintCharWithoutSkill(g);
			return;
		}
		if (this.isMabuHold)
		{
			if (this.cmtoChar)
			{
				GameScr.cmtoX = this.cx - GameScr.gW2;
				GameScr.cmtoY = this.cy - GameScr.gH23;
				if (!GameCanvas.isTouchControl)
				{
					GameScr.cmtoX += GameScr.gW6 * this.cdir;
				}
			}
			return;
		}
		if (!this.isPaint())
		{
			return;
		}
		if (!this.me && GameScr.notPaint)
		{
			return;
		}
		if (this.petFollow != null)
		{
			this.petFollow.paint(g);
		}
		this.paintMount1(g);
		if (TileMap.isInAirMap() && this.cy >= TileMap.pxh - 48)
		{
			return;
		}
		if (this.isTeleport)
		{
			return;
		}
		if (this.holder && GameCanvas.gameTick % 2 == 0)
		{
			g.setColor(16185600);
			if (this.charHold != null)
			{
				g.drawLine(this.cx, this.cy - this.ch / 2, this.charHold.cx, this.charHold.cy - this.charHold.ch / 2);
			}
			if (this.mobHold != null)
			{
				g.drawLine(this.cx, this.cy - this.ch / 2, this.mobHold.x, this.mobHold.y - this.mobHold.h / 2);
			}
		}
		this.paintSuperEffBehind(g);
		this.paintAuraBehind(g);
		this.paintEffBehind(g);
		this.paintEff_Lvup_behind(g);
		this.paintEff_Pet(g);
		if (this.shadowLife > 0)
		{
			if (GameCanvas.gameTick % 2 == 0)
			{
				this.paintCharBody(g, this.shadowX, this.shadowY, this.cdir, 25, true);
			}
			else if (this.shadowLife > 5)
			{
				this.paintCharBody(g, this.shadowX, this.shadowY, this.cdir, 7, true);
			}
		}
		if (!this.isPaint() && this.skillPaint != null && (this.skillPaint.id < 70 || this.skillPaint.id > 76) && (this.skillPaint.id < 77 || this.skillPaint.id > 83))
		{
			if (this.skillPaint != null)
			{
				this.indexSkill = this.skillInfoPaint().Length;
				this.skillPaint = null;
			}
			this.effPaints = null;
			this.eff = null;
			this.effTask = null;
			this.indexEff = -1;
			this.indexEffTask = -1;
			return;
		}
		if (this.statusMe == 15 || (this.moveFast != null && this.moveFast[0] > 0))
		{
			return;
		}
		this.paintCharName_HP_MP_Overhead(g);
		if (this.skillPaint == null || this.skillInfoPaint() == null || this.indexSkill >= this.skillInfoPaint().Length)
		{
			this.paintCharWithoutSkill(g);
		}
		if (this.arr != null)
		{
			this.arr.paint(g);
		}
		if (this.dart != null)
		{
			this.dart.paint(g);
		}
		this.paintEffect(g);
		if (this.mobMe != null)
		{
		}
		this.paintMount2(g);
		this.paintEff_Lvup_front(g);
		this.paintSuperEffFront(g);
		this.paintAuraFront(g);
		this.paintEffFront(g);
		this.paint_map_line(g);
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x00055B2C File Offset: 0x00053D2C
	private void paint_map_line(mGraphics g)
	{
		if (this.isPaintNewSkill)
		{
			return;
		}
		if (this.x_hint != 0 && this.y_hint != 0 && this.statusMe != 14)
		{
			int arg = 0;
			int x = this.cx - 30;
			int y = this.cy - 15;
			int num = -30;
			int num2 = 5;
			if (Res.abs(this.cy - (int)this.y_hint) > 150)
			{
				if (this.cy > (int)this.y_hint)
				{
					arg = 7;
					x = this.cx;
					y = this.cy - 15 - 60;
				}
				else
				{
					arg = 5;
					x = this.cx;
					y = this.cy - 15 + 60;
				}
			}
			else if (this.cx > (int)this.x_hint)
			{
				arg = 2;
			}
			else if (this.cx <= (int)this.x_hint)
			{
				x = this.cx + 30;
			}
			if (GameCanvas.gameTick % 10 < 5)
			{
				return;
			}
			if (Res.abs(this.cx - (int)this.x_hint) > 100)
			{
				g.drawRegion(GameScr.arrow, 0, 0, 13, 16, arg, x, y, StaticObj.VCENTER_HCENTER);
			}
			else if (Res.abs(this.cx - (int)this.x_hint) < 50)
			{
				g.drawImage(Panel.imgBantay, (int)this.x_hint + num, (int)(this.y_hint - 60) + num2, 0);
			}
		}
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x00055C9C File Offset: 0x00053E9C
	private void paintEff_Pet(mGraphics g)
	{
		for (int i = 0; i < this.vEffChar.size(); i++)
		{
			Effect effect = (Effect)this.vEffChar.elementAt(i);
			if (effect.effId >= 201)
			{
				effect.paint(g);
			}
		}
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x00055CF0 File Offset: 0x00053EF0
	private void paintSuperEffBehind(mGraphics g)
	{
		if (this.me && !global::Char.isPaintAura2)
		{
			return;
		}
		if ((this.statusMe == 1 || this.statusMe == 6) && mSystem.currentTimeMillis() - this.timeBlue > 0L && !this.isCopy && this.clevel >= 16)
		{
			int num = 7598;
			int num2 = 4;
			if (this.clevel >= 19)
			{
				num = 7676;
			}
			if (this.clevel >= 22)
			{
				num = 7677;
			}
			if (this.clevel >= 25)
			{
				num = 7678;
			}
			if (num != -1)
			{
				Small small = SmallImage.imgNew[num];
				if (small == null)
				{
					SmallImage.createImage(num);
				}
				else
				{
					int y = GameCanvas.gameTick / 4 % num2 * (mGraphics.getImageHeight(small.img) / num2);
					g.drawRegion(small.img, 0, y, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img) / num2, 0, this.cx, this.cy + 2, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
		}
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x00055E1C File Offset: 0x0005401C
	private void paintSuperEffFront(mGraphics g)
	{
		if (!global::Char.isPaintAura2)
		{
			return;
		}
		if (this.statusMe == 1 || this.statusMe == 6)
		{
			if (mSystem.currentTimeMillis() - this.timeBlue > 0L)
			{
				if (this.isCopy)
				{
					if (GameCanvas.gameTick % 2 == 0)
					{
						this.tBlue++;
					}
					if (this.tBlue > 6)
					{
						this.tBlue = 0;
					}
					g.drawImage(GameCanvas.imgViolet[this.tBlue], this.cx, this.cy + 9, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				else
				{
					if (this.clevel >= 14 && !GameCanvas.lowGraphic)
					{
						bool flag = false;
						if (mSystem.currentTimeMillis() - this.timeBlue > -1000L && this.IsAddDust1)
						{
							flag = true;
							this.IsAddDust1 = false;
						}
						if (mSystem.currentTimeMillis() - this.timeBlue > -500L && this.IsAddDust2)
						{
							flag = true;
							this.IsAddDust2 = false;
						}
						if (flag)
						{
							GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
							GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
							this.addDustEff(1);
						}
					}
					if (this.clevel == 14)
					{
						if (GameCanvas.gameTick % 2 == 0)
						{
							this.tBlue++;
						}
						if (this.tBlue > 6)
						{
							this.tBlue = 0;
						}
						g.drawImage(GameCanvas.imgBlue[this.tBlue], this.cx, this.cy + 9, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					else if (this.clevel == 15)
					{
						if (GameCanvas.gameTick % 2 == 0)
						{
							this.tBlue++;
						}
						if (this.tBlue > 6)
						{
							this.tBlue = 0;
						}
						g.drawImage(GameCanvas.imgViolet[this.tBlue], this.cx, this.cy + 9, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					else if (this.clevel >= 16)
					{
						int num = -1;
						int num2 = 4;
						if (this.clevel >= 16 && this.clevel < 22)
						{
							num = 7599;
							num2 = 4;
						}
						if (num != -1)
						{
							Small small = SmallImage.imgNew[num];
							if (small == null)
							{
								SmallImage.createImage(num);
							}
							else
							{
								int y = GameCanvas.gameTick / 4 % num2 * (mGraphics.getImageHeight(small.img) / num2);
								g.drawRegion(small.img, 0, y, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img) / num2, 0, this.cx, this.cy + 2, mGraphics.BOTTOM | mGraphics.HCENTER);
							}
						}
					}
				}
			}
		}
		else
		{
			this.timeBlue = mSystem.currentTimeMillis() + 1500L;
			this.IsAddDust1 = true;
			this.IsAddDust2 = true;
		}
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x00056124 File Offset: 0x00054324
	public void paintEffect(mGraphics g)
	{
		if (this.effPaints != null)
		{
			for (int i = 0; i < this.effPaints.Length; i++)
			{
				if (this.effPaints[i] != null)
				{
					if (this.effPaints[i].eMob != null)
					{
						int y = this.effPaints[i].eMob.y;
						if (this.effPaints[i].eMob is BigBoss)
						{
							y = this.effPaints[i].eMob.y - 60;
						}
						if (this.effPaints[i].eMob is BigBoss2)
						{
							y = this.effPaints[i].eMob.y - 50;
						}
						if (this.effPaints[i].eMob is BachTuoc)
						{
							y = this.effPaints[i].eMob.y - 40;
						}
						SmallImage.drawSmallImage(g, this.effPaints[i].getImgId(), this.effPaints[i].eMob.x, y, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					else if (this.effPaints[i].eChar != null)
					{
						SmallImage.drawSmallImage(g, this.effPaints[i].getImgId(), this.effPaints[i].eChar.cx, this.effPaints[i].eChar.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
				}
			}
		}
		if (this.indexEff >= 0 && this.eff != null)
		{
			SmallImage.drawSmallImage(g, this.eff.arrEfInfo[this.indexEff].idImg, this.cx + this.eff.arrEfInfo[this.indexEff].dx, this.cy + this.eff.arrEfInfo[this.indexEff].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
		}
		if (this.indexEffTask >= 0 && this.effTask != null)
		{
			SmallImage.drawSmallImage(g, this.effTask.arrEfInfo[this.indexEffTask].idImg, this.cx + this.effTask.arrEfInfo[this.indexEffTask].dx, this.cy + this.effTask.arrEfInfo[this.indexEffTask].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
		}
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x000045ED File Offset: 0x000027ED
	private void paintArrowAttack(mGraphics g)
	{
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x00056394 File Offset: 0x00054594
	public void paintHp(mGraphics g, int x, int y)
	{
		int num = (int)((long)((int)this.cHP * 100) / this.cHPFull) / 10 - 1;
		if (num < 0)
		{
			num = 0;
		}
		if (num > 9)
		{
			num = 9;
		}
		if (!this.me)
		{
			g.drawRegion(Mob.imgHP, 0, 6 * (9 - num), 9, 6, 0, x, y - mFont.tahoma_7.getHeight() - 6, 3);
		}
		if ((int)this.cTypePk != 0 || ((int)global::Char.myCharz().cFlag != 0 && (int)this.cFlag != 0 && ((int)this.cFlag == 8 || (int)global::Char.myCharz().cFlag == 8 || (int)this.cFlag != (int)global::Char.myCharz().cFlag)))
		{
			this.len = (int)(this.cHP * 100L / this.cHPFull * (long)this.w_hp_bar) / 100;
			num = (int)(this.cHP * 100L / this.cHPFull);
			if (num < 30)
			{
				this.imgHPtem = GameScr.imgHP_tm_do;
			}
			else if (num < 60)
			{
				this.imgHPtem = GameScr.imgHP_tm_vang;
			}
			else
			{
				this.imgHPtem = GameScr.imgHP_tm_xanh;
			}
			int imageWidth = mGraphics.getImageWidth(GameScr.imgHP_tm_xam);
			int imageHeight = mGraphics.getImageHeight(GameScr.imgHP_tm_xam);
			int w = imageWidth * num / 100;
			g.drawImage(GameScr.imgHP_tm_xam, x - (imageWidth >> 1), y - 1, mGraphics.TOP | mGraphics.LEFT);
			if (this.len < 5)
			{
				if (GameCanvas.gameTick % 6 < 3)
				{
					g.drawRegion(this.imgHPtem, 0, 0, w, imageHeight, 0, x - (imageWidth >> 1), y - 1, mGraphics.TOP | mGraphics.LEFT);
				}
			}
			else
			{
				g.drawRegion(this.imgHPtem, 0, 0, w, imageHeight, 0, x - (imageWidth >> 1), y - 1, mGraphics.TOP | mGraphics.LEFT);
			}
		}
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x00056570 File Offset: 0x00054770
	public int getClassColor()
	{
		int result = 9145227;
		if (this.nClass.classId == 1 || this.nClass.classId == 2)
		{
			result = 16711680;
		}
		else if (this.nClass.classId == 3 || this.nClass.classId == 4)
		{
			result = 33023;
		}
		else if (this.nClass.classId == 5 || this.nClass.classId == 6)
		{
			result = 7443811;
		}
		return result;
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x00056608 File Offset: 0x00054808
	public void paintNameInSameParty(mGraphics g)
	{
		if ((int)this.cTypePk == 3 || (int)this.cTypePk == 5)
		{
			return;
		}
		if (this.isPaint())
		{
			if (global::Char.myCharz().charFocus == null || !global::Char.myCharz().charFocus.Equals(this))
			{
				mFont.tahoma_7_yellow.drawString(g, this.cName, this.cx, this.cy - this.ch - mFont.tahoma_7_green.getHeight() - 5, mFont.CENTER, mFont.tahoma_7_grey);
			}
			else if (global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.Equals(this))
			{
				mFont.tahoma_7_yellow.drawString(g, this.cName, this.cx, this.cy - this.ch - mFont.tahoma_7_green.getHeight() - 10, mFont.CENTER, mFont.tahoma_7_grey);
			}
		}
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x00056700 File Offset: 0x00054900
	private void paintCharName_HP_MP_Overhead(mGraphics g)
	{
		Part part = GameScr.parts[this.getFHead(this.head)];
		int num = global::Char.CharInfo[this.cf][0][2] - (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dy + 5;
		if (this.isInvisiblez && !this.me)
		{
			return;
		}
		if (!this.me && TileMap.mapID == 113 && this.cy >= 360)
		{
			return;
		}
		if (this.me)
		{
			num += 5;
			this.paintHp(g, this.cx, this.cy - num + 3);
			if (this.fraDanhHieu != null)
			{
				int x = this.cx - this.fraDanhHieu.frameWidth / 2;
				int y = this.cy - num + 3 - mFont.tahoma_7.getHeight() - (this.fraDanhHieu.frameHeight + 5);
				if (GameCanvas.gameTick % 5 == 0)
				{
					this.danhHieuFramme++;
				}
				if (this.danhHieuFramme >= this.fraDanhHieu.nFrame)
				{
					this.danhHieuFramme = 0;
				}
				this.fraDanhHieu.drawFrame(this.danhHieuFramme, x, y, 0, mGraphics.TOP | mGraphics.LEFT, g);
			}
		}
		else
		{
			bool flag = global::Char.myChar.clan != null && this.clanID == global::Char.myChar.clan.ID;
			bool flag2 = (int)this.cTypePk == 3 || (int)this.cTypePk == 5;
			bool flag3 = (int)this.cTypePk == 4;
			if (this.cName.StartsWith("$"))
			{
				this.cName = this.cName.Substring(1);
				this.isPet = true;
			}
			if (this.cName.StartsWith("#"))
			{
				this.cName = this.cName.Substring(1);
				this.isMiniPet = true;
			}
			if (global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.Equals(this))
			{
				num += 5;
				this.paintHp(g, this.cx, this.cy - num + 3);
				if (this.fraDanhHieu != null)
				{
					int x2 = this.cx - this.fraDanhHieu.frameWidth / 2;
					int y2 = this.cy - num + 3 - mFont.tahoma_7.getHeight() - (this.fraDanhHieu.frameHeight + 5);
					if (GameCanvas.gameTick % 5 == 0)
					{
						this.danhHieuFramme++;
					}
					if (this.danhHieuFramme >= this.fraDanhHieu.nFrame)
					{
						this.danhHieuFramme = 0;
					}
					this.fraDanhHieu.drawFrame(this.danhHieuFramme, x2, y2, 0, mGraphics.TOP | mGraphics.LEFT, g);
				}
			}
			num += mFont.tahoma_7_white.getHeight();
			mFont font = mFont.tahoma_7_whiteSmall;
			if (this.isPet || this.isMiniPet)
			{
				font = mFont.tahoma_7_blue1Small;
			}
			else if (flag2)
			{
				font = mFont.nameFontRed;
			}
			else if (flag3)
			{
				font = mFont.nameFontYellow;
			}
			else if (flag)
			{
				font = mFont.nameFontGreen;
			}
			if (TileMap.mapID == 170)
			{
				if (this.flagImage == 2325)
				{
					font = mFont.tahoma_7_blue;
				}
				else if (this.flagImage == 2323)
				{
					font = mFont.tahoma_7_red;
				}
			}
			if ((this.paintName || flag2 || flag3) && !flag)
			{
				if (mSystem.clientType == 1)
				{
					font.drawString(g, this.cName, this.cx, this.cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
				}
				else
				{
					font.drawString(g, this.cName, this.cx, this.cy - num, mFont.CENTER);
				}
				num += mFont.tahoma_7.getHeight();
			}
			if (flag)
			{
				if (global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.Equals(this))
				{
					font.drawString(g, this.cName, this.cx, this.cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
				}
				else if (this.charFocus == null)
				{
					font.drawString(g, this.cName, this.cx - 10, this.cy - num + 3, mFont.LEFT, mFont.tahoma_7_grey);
					this.paintHp(g, this.cx - 16, this.cy - num + 10);
				}
			}
		}
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x00056BBC File Offset: 0x00054DBC
	public void paintShadow(mGraphics g)
	{
		if (this.isMabuHold)
		{
			return;
		}
		if (this.head == 377)
		{
			return;
		}
		if (this.leg == 471)
		{
			return;
		}
		if (this.isTeleport)
		{
			return;
		}
		if (this.isFlyUp)
		{
			return;
		}
		int num = (int)TileMap.size;
		if ((TileMap.mapID < 114 || TileMap.mapID > 120) && TileMap.mapID != 127 && TileMap.mapID != 128)
		{
			if (!TileMap.tileTypeAt(this.xSd + num / 2, this.ySd + 1, 4))
			{
				if (TileMap.tileTypeAt((this.xSd - num / 2) / num, (this.ySd + 1) / num) == 0)
				{
					g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, 100, 100);
				}
				else if (TileMap.tileTypeAt((this.xSd + num / 2) / num, (this.ySd + 1) / num) == 0)
				{
					g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, num, 100);
				}
				else if (TileMap.tileTypeAt(this.xSd - num / 2, this.ySd + 1, 8))
				{
					g.setClip(this.xSd / 24 * num, (this.ySd - 30) / num * num, num, 100);
				}
			}
		}
		g.drawImage(TileMap.bong, this.xSd, this.ySd, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x00056D74 File Offset: 0x00054F74
	public void updateShadown()
	{
		int i = 0;
		this.xSd = this.cx;
		if (TileMap.tileTypeAt(this.cx, this.cy, 2))
		{
			this.ySd = this.cy;
			return;
		}
		this.ySd = this.cy;
		while (i < 30)
		{
			i++;
			this.ySd += 24;
			if (TileMap.tileTypeAt(this.xSd, this.ySd, 2))
			{
				if (this.ySd % 24 != 0)
				{
					this.ySd -= this.ySd % 24;
				}
				break;
			}
		}
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x00056E20 File Offset: 0x00055020
	private void paintCharWithoutSkill(mGraphics g)
	{
		try
		{
			if (this.isMafuba)
			{
				this.paintCharBody(g, this.xMFB, this.yMFB, this.cdir, this.cf, false);
			}
			else
			{
				if (this.isInvisiblez)
				{
					if (this.me)
					{
						if (GameCanvas.gameTick % 50 == 48 || GameCanvas.gameTick % 50 == 90)
						{
							SmallImage.drawSmallImage(g, 1196, this.cx, this.cy - 18, 0, mGraphics.VCENTER | mGraphics.HCENTER);
						}
						else
						{
							SmallImage.drawSmallImage(g, 1195, this.cx, this.cy - 18, 0, mGraphics.VCENTER | mGraphics.HCENTER);
						}
					}
				}
				else
				{
					this.paintCharBody(g, this.cx, this.cy + this.fy, this.cdir, this.cf, true);
				}
				if (this.isLockAttack)
				{
					SmallImage.drawSmallImage(g, 290, this.cx, this.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi paint char without skill: " + ex.ToString());
		}
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x00056F78 File Offset: 0x00055178
	public void paintBag(mGraphics g, short[] id, int x, int y, int dir, bool isPaintChar)
	{
		int num = 0;
		int num2 = 0;
		if (this.statusMe == 6)
		{
			num = 8;
			num2 = 17;
		}
		if (this.statusMe == 1)
		{
			if (this.cp1 % 15 < 5)
			{
				num = 8;
				num2 = 17;
			}
			else
			{
				num = 8;
				num2 = 18;
			}
		}
		if (this.statusMe == 2)
		{
			if (this.cf <= 3)
			{
				num = 7;
				num2 = 17;
			}
			else
			{
				num = 7;
				num2 = 18;
			}
		}
		if (this.statusMe == 3 || this.statusMe == 9)
		{
			num = 5;
			num2 = 20;
		}
		if (this.statusMe == 4)
		{
			if (this.cf == 8)
			{
				num = 5;
				num2 = 16;
			}
			else
			{
				num = 5;
				num2 = 20;
			}
		}
		if (this.statusMe == 10)
		{
			if (this.cf == 8)
			{
				num = 0;
				num2 = 23;
			}
			else
			{
				num = 5;
				num2 = 22;
			}
		}
		if ((int)this.isInjure > 0)
		{
			num = 5;
			num2 = 18;
		}
		if (this.skillPaint != null && this.skillInfoPaint() != null && this.indexSkill < this.skillInfoPaint().Length)
		{
			num = -1;
			num2 = 17;
		}
		this.fBag++;
		if (this.fBag > 10000)
		{
			this.fBag = 0;
		}
		sbyte b = (sbyte)(this.fBag / 4 % id.Length);
		if (this.me && !Mod.DungPham.KoiOctiiu957.ModSkin.ShouldAnimateBack())
		{
			b = 0;
		}
		if (!isPaintChar)
		{
			if (id.Length == 2)
			{
				b = 1;
			}
			if (id.Length == 3)
			{
				if (id[2] >= 0)
				{
					b = 2;
					if (GameCanvas.gameTick % 10 > 5)
					{
						b = 1;
					}
				}
				else
				{
					b = 1;
				}
			}
		}
		else if (id.Length > 1 && ((int)b == 0 || (int)b == 1) && this.statusMe != 1 && this.statusMe != 6)
		{
			this.fBag = 0;
			b = 0;
			if (GameCanvas.gameTick % 10 > 5)
			{
				b = 1;
			}
		}
		SmallImage.drawSmallImage(g, (int)id[(int)b], x + ((dir != 1) ? num : (-num)), y - num2, (dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0005718C File Offset: 0x0005538C
	public bool isCharBodyImageID(int id)
	{
		Part part = GameScr.parts[this.head];
		Part part2 = GameScr.parts[this.leg];
		Part part3 = GameScr.parts[this.body];
		for (int i = 0; i < global::Char.CharInfo.Length; i++)
		{
			if (id == (int)part.pi[global::Char.CharInfo[i][0][0]].id)
			{
				return true;
			}
			if (id == (int)part2.pi[global::Char.CharInfo[i][1][0]].id)
			{
				return true;
			}
			if (id == (int)part3.pi[global::Char.CharInfo[i][2][0]].id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x00057238 File Offset: 0x00055438
	public void paintHead(mGraphics g, int cx, int cy, int look)
	{
		Part part = GameScr.parts[this.head];
		SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, cx, cy, (look != 0) ? 2 : 0, mGraphics.RIGHT | mGraphics.VCENTER);
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0005728C File Offset: 0x0005548C
	public void paintHeadWithXY(mGraphics g, int x, int y, int look)
	{
		Part part = GameScr.parts[this.head];
		SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, x + global::Char.CharInfo[0][0][1] + (int)part.pi[global::Char.CharInfo[0][0][0]].dx - 3, y + 3, look, mGraphics.LEFT | mGraphics.BOTTOM);
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x000572FC File Offset: 0x000554FC
	public void paintCharBody(mGraphics g, int cx, int cy, int cdir, int cf, bool isPaintBag)
	{
		this.ph = GameScr.parts[this.head];
		this.pl = GameScr.parts[this.leg];
		this.pb = GameScr.parts[this.body];
		if (this.bag >= 0 && this.statusMe != 14)
		{
			if (!ClanImage.idImages.containsKey(this.bag + string.Empty))
			{
				ClanImage.idImages.put(this.bag + string.Empty, new ClanImage());
				Service.gI().requestBagImage(this.bag);
			}
			else
			{
				ClanImage clanImage = (ClanImage)ClanImage.idImages.get(this.bag + string.Empty);
				if (clanImage.idImage != null && isPaintBag)
				{
					this.paintBag(g, clanImage.idImage, cx, cy, cdir, true);
				}
			}
		}
		int num = 2;
		int anchor = 24;
		int anchor2 = StaticObj.TOP_RIGHT;
		int num2 = -1;
		if (cdir == 1)
		{
			num = 0;
			anchor = 0;
			anchor2 = 0;
			num2 = 1;
		}
		if (this.statusMe == 14)
		{
			if (GameCanvas.gameTick % 4 > 0)
			{
				g.drawImage(ItemMap.imageFlare, cx, cy - this.ch - 11, mGraphics.HCENTER | mGraphics.VCENTER);
			}
			int num3 = 0;
			if (this.head == 89 || this.head == 457 || this.head == 460 || this.head == 461 || this.head == 462 || this.head == 463 || this.head == 464 || this.head == 465 || this.head == 466)
			{
				num3 = 15;
			}
			if (this.head == 1291)
			{
				num3 = 23;
			}
			SmallImage.drawSmallImage(g, 834, cx, cy - global::Char.CharInfo[cf][2][2] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dy - 2 + num3, num, StaticObj.TOP_CENTER);
			SmallImage.drawSmallImage(g, 79, cx, cy - this.ch - 8, 0, mGraphics.HCENTER | mGraphics.BOTTOM);
			SmallImage.drawSmallImage(g, (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
			this.paintHat_behind(g, cf, cy - global::Char.CharInfo[cf][2][2] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dy);
			if (this.isHead_2Fr(this.head))
			{
				Part part = GameScr.parts[this.getFHead(this.head)];
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
			}
			this.paintHat_front(g, cf, cy - global::Char.CharInfo[cf][2][2] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dy);
			this.paintRedEye(g, cx + (global::Char.CharInfo[cf][0][1] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
		}
		else
		{
			this.paintHat_behind(g, cf, cy - global::Char.CharInfo[cf][2][2] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dy);
			try
			{
				if (this.isHead_2Fr(this.head))
				{
					Part part2 = GameScr.parts[this.getFHead(this.head)];
					SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)part2.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)part2.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
				}
				else
				{
					SmallImage.drawSmallImage(g, (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
				}
				SmallImage.drawSmallImage(g, (int)this.pl.pi[global::Char.CharInfo[cf][1][0]].id, cx + (global::Char.CharInfo[cf][1][1] + (int)this.pl.pi[global::Char.CharInfo[cf][1][0]].dx) * num2, cy - global::Char.CharInfo[cf][1][2] + (int)this.pl.pi[global::Char.CharInfo[cf][1][0]].dy, num, anchor);
				SmallImage.drawSmallImage(g, (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].id, cx + (global::Char.CharInfo[cf][2][1] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dx) * num2, cy - global::Char.CharInfo[cf][2][2] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dy, num, anchor);
				this.paintRedEye(g, cx + (global::Char.CharInfo[cf][0][1] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
			}
			catch (Exception ex)
			{
				Debug.LogError(">>>>>>err: " + ex.ToString());
			}
		}
		this.ch = (((int)this.isMonkey != 1 && !this.isFusion) ? (global::Char.CharInfo[0][0][2] + (int)this.ph.pi[global::Char.CharInfo[0][0][0]].dy + 10) : 60);
		int num4 = (Res.abs((int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy) < 22) ? ((int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy) : (((int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy >= 0) ? ((int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy - 5) : ((int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy + 5));
		this.cH_new = cy - global::Char.CharInfo[cf][0][2] + num4;
		if (this.statusMe == 1 && this.charID > 0 && !this.isMask && !this.isUseChargeSkill() && !this.isWaitMonkey && this.skillPaint == null && cf != 23 && this.bag < 0 && ((GameCanvas.gameTick + this.charID) % 30 == 0 || this.isFreez))
		{
			g.drawImage((this.cgender != 1) ? global::Char.eyeTraiDat : global::Char.eyeNamek, cx + -((this.cgender != 1) ? 2 : 2) * num2, cy - 32 + ((this.cgender != 1) ? 11 : 10) - cf, anchor2);
		}
		if (this.eProtect != null)
		{
			this.eProtect.paint(g);
		}
		if (this.eDanhHieu != null)
		{
			this.eDanhHieu.paint(g);
		}
		this.paintPKFlag(g);
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x00057D04 File Offset: 0x00055F04
	public void paintCharWithSkill(mGraphics g)
	{
		this.ty = 0;
		SkillInfoPaint[] array = this.skillInfoPaint();
		this.cf = array[this.indexSkill].status;
		this.paintCharWithoutSkill(g);
		if (this.cdir == 1)
		{
			if (this.eff0 != null)
			{
				if (this.dx0 == 0)
				{
					this.dx0 = array[this.indexSkill].e0dx;
				}
				if (this.dy0 == 0)
				{
					this.dy0 = array[this.indexSkill].e0dy;
				}
				SmallImage.drawSmallImage(g, this.eff0.arrEfInfo[this.i0].idImg, this.cx + this.dx0 + this.eff0.arrEfInfo[this.i0].dx, this.cy + this.dy0 + this.eff0.arrEfInfo[this.i0].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i0++;
				if (this.i0 >= this.eff0.arrEfInfo.Length)
				{
					this.eff0 = null;
					this.i0 = (this.dx0 = (this.dy0 = 0));
				}
			}
			if (this.eff1 != null)
			{
				if (this.dx1 == 0)
				{
					this.dx1 = array[this.indexSkill].e1dx;
				}
				if (this.dy1 == 0)
				{
					this.dy1 = array[this.indexSkill].e1dy;
				}
				SmallImage.drawSmallImage(g, this.eff1.arrEfInfo[this.i1].idImg, this.cx + this.dx1 + this.eff1.arrEfInfo[this.i1].dx, this.cy + this.dy1 + this.eff1.arrEfInfo[this.i1].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i1++;
				if (this.i1 >= this.eff1.arrEfInfo.Length)
				{
					this.eff1 = null;
					this.i1 = (this.dx1 = (this.dy1 = 0));
				}
			}
			if (this.eff2 != null)
			{
				if (this.dx2 == 0)
				{
					this.dx2 = array[this.indexSkill].e2dx;
				}
				if (this.dy2 == 0)
				{
					this.dy2 = array[this.indexSkill].e2dy;
				}
				SmallImage.drawSmallImage(g, this.eff2.arrEfInfo[this.i2].idImg, this.cx + this.dx2 + this.eff2.arrEfInfo[this.i2].dx, this.cy + this.dy2 + this.eff2.arrEfInfo[this.i2].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i2++;
				if (this.i2 >= this.eff2.arrEfInfo.Length)
				{
					this.eff2 = null;
					this.i2 = (this.dx2 = (this.dy2 = 0));
				}
			}
		}
		else
		{
			if (this.eff0 != null)
			{
				if (this.dx0 == 0)
				{
					this.dx0 = array[this.indexSkill].e0dx;
				}
				if (this.dy0 == 0)
				{
					this.dy0 = array[this.indexSkill].e0dy;
				}
				SmallImage.drawSmallImage(g, this.eff0.arrEfInfo[this.i0].idImg, this.cx - this.dx0 - this.eff0.arrEfInfo[this.i0].dx, this.cy + this.dy0 + this.eff0.arrEfInfo[this.i0].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i0++;
				if (this.i0 >= this.eff0.arrEfInfo.Length)
				{
					this.eff0 = null;
					this.i0 = 0;
					this.dx0 = 0;
					this.dy0 = 0;
				}
			}
			if (this.eff1 != null)
			{
				if (this.dx1 == 0)
				{
					this.dx1 = array[this.indexSkill].e1dx;
				}
				if (this.dy1 == 0)
				{
					this.dy1 = array[this.indexSkill].e1dy;
				}
				SmallImage.drawSmallImage(g, this.eff1.arrEfInfo[this.i1].idImg, this.cx - this.dx1 - this.eff1.arrEfInfo[this.i1].dx, this.cy + this.dy1 + this.eff1.arrEfInfo[this.i1].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i1++;
				if (this.i1 >= this.eff1.arrEfInfo.Length)
				{
					this.eff1 = null;
					this.i1 = 0;
					this.dx1 = 0;
					this.dy1 = 0;
				}
			}
			if (this.eff2 != null)
			{
				if (this.dx2 == 0)
				{
					this.dx2 = array[this.indexSkill].e2dx;
				}
				if (this.dy2 == 0)
				{
					this.dy2 = array[this.indexSkill].e2dy;
				}
				SmallImage.drawSmallImage(g, this.eff2.arrEfInfo[this.i2].idImg, this.cx - this.dx2 - this.eff2.arrEfInfo[this.i2].dx, this.cy + this.dy2 + this.eff2.arrEfInfo[this.i2].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i2++;
				if (this.i2 >= this.eff2.arrEfInfo.Length)
				{
					this.eff2 = null;
					this.i2 = 0;
					this.dx2 = 0;
					this.dy2 = 0;
				}
			}
		}
		this.indexSkill++;
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x0005834C File Offset: 0x0005654C
	public static int getIndexChar(int ID)
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char.charID == ID)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x00058394 File Offset: 0x00056594
	public void moveTo(int toX, int toY, int type)
	{
		if (type == 1 || Res.abs(toX - this.cx) > 100 || Res.abs(toY - this.cy) > 300)
		{
			this.createShadow(this.cx, this.cy, 10);
			this.cx = toX;
			this.cy = toY;
			this.vMovePoints.removeAllElements();
			this.statusMe = 6;
			this.cp3 = 0;
			this.currentMovePoint = null;
			this.cf = 25;
			return;
		}
		int dir = 0;
		int act = 0;
		int num = toX - this.cx;
		int num2 = toY - this.cy;
		if (num == 0 && num2 == 0)
		{
			act = 1;
			this.cp3 = 0;
		}
		else if (num2 == 0)
		{
			act = 2;
			if (num > 0)
			{
				dir = 1;
			}
			if (num < 0)
			{
				dir = -1;
			}
		}
		else if (num2 != 0)
		{
			if (num2 < 0)
			{
				act = 3;
			}
			if (num2 > 0)
			{
				act = 4;
			}
			if (num < 0)
			{
				dir = -1;
			}
			if (num > 0)
			{
				dir = 1;
			}
		}
		this.vMovePoints.addElement(new MovePoint(toX, toY, act, dir));
		if (this.statusMe != 6)
		{
			this.statusBeforeNothing = this.statusMe;
		}
		this.statusMe = 6;
		this.cp3 = 0;
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x000584D4 File Offset: 0x000566D4
	public static void getcharInjure(int cID, int dx, int dy, long HP)
	{
		global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(cID);
		if (@char.vMovePoints.size() == 0)
		{
			return;
		}
		MovePoint movePoint = (MovePoint)@char.vMovePoints.lastElement();
		int xEnd = movePoint.xEnd + dx;
		int yEnd = movePoint.yEnd + dy;
		global::Char char2 = (global::Char)GameScr.vCharInMap.elementAt(cID);
		char2.cHP -= HP;
		if (char2.cHP < 0L)
		{
			char2.cHP = 0L;
		}
		char2.cHPShow = ((global::Char)GameScr.vCharInMap.elementAt(cID)).cHP - HP;
		char2.statusMe = 6;
		char2.cp3 = 0;
		char2.vMovePoints.addElement(new MovePoint(xEnd, yEnd, 8, char2.cdir));
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x000585A8 File Offset: 0x000567A8
	public bool isMagicTree()
	{
		if (GameScr.gI().magicTree != null)
		{
			int x = GameScr.gI().magicTree.x;
			int y = GameScr.gI().magicTree.y;
			return this.cx > x - 30 && this.cx < x + 30 && this.cy > y - 30 && this.cy < y + 30;
		}
		return false;
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x00058628 File Offset: 0x00056828
	public void searchItem()
	{
		int[] array = new int[]
		{
			-1,
			-1,
			-1,
			-1
		};
		if (this.itemFocus == null)
		{
			for (int i = 0; i < GameScr.vItemMap.size(); i++)
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
				int num = global::Math.abs(global::Char.myCharz().cx - itemMap.x);
				int num2 = global::Math.abs(global::Char.myCharz().cy - itemMap.y);
				int num3 = (num <= num2) ? num2 : num;
				if (num <= 48 && num2 <= 48 && (this.itemFocus == null || num3 < array[3]))
				{
					if (GameScr.gI().auto != 0 && GameScr.gI().isBagFull())
					{
						if ((int)itemMap.template.type == 9)
						{
							this.itemFocus = itemMap;
							array[3] = num3;
						}
					}
					else
					{
						this.itemFocus = itemMap;
						array[3] = num3;
					}
				}
			}
		}
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x000045ED File Offset: 0x000027ED
	public void searchFocus()
	{
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x00058734 File Offset: 0x00056934
	public void clearFocus(int index)
	{
		if (index == 0)
		{
			this.deFocusNPC();
			this.charFocus = null;
			this.itemFocus = null;
		}
		else if (index == 1)
		{
			this.mobFocus = null;
			this.charFocus = null;
			this.itemFocus = null;
		}
		else if (index == 2)
		{
			this.mobFocus = null;
			this.deFocusNPC();
			this.itemFocus = null;
		}
		else if (index == 3)
		{
			this.mobFocus = null;
			this.deFocusNPC();
			this.charFocus = null;
		}
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x000587BC File Offset: 0x000569BC
	public static bool isCharInScreen(global::Char c)
	{
		int cmx = GameScr.cmx;
		int num = GameScr.cmx + GameCanvas.w;
		int num2 = GameScr.cmy + 10;
		int num3 = GameScr.cmy + GameScr.gH;
		return c.statusMe != 15 && !c.isInvisiblez && cmx <= c.cx && c.cx <= num && num2 <= c.cy && c.cy <= num3;
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x00007323 File Offset: 0x00005523
	public bool isAttacPlayerStatus()
	{
		return (int)this.cTypePk == 4 || (int)this.cTypePk == 3;
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0000733F File Offset: 0x0000553F
	public void setHoldChar(global::Char r)
	{
		if (this.cx < r.cx)
		{
			this.cdir = 1;
		}
		else
		{
			this.cdir = -1;
		}
		this.charHold = r;
		this.holder = true;
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x00007373 File Offset: 0x00005573
	public void setHoldMob(Mob r)
	{
		if (this.cx < r.x)
		{
			this.cdir = 1;
		}
		else
		{
			this.cdir = -1;
		}
		this.mobHold = r;
		this.holder = true;
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x0005883C File Offset: 0x00056A3C
	public void findNextFocusByKey()
	{
		Res.outz("focus size= " + this.focus.size());
		if ((global::Char.myCharz().skillPaint != null || global::Char.myCharz().arr != null || global::Char.myCharz().dart != null || global::Char.myCharz().skillInfoPaint() != null) && this.focus.size() == 0)
		{
			return;
		}
		this.focus.removeAllElements();
		int num = 0;
		int num2 = GameScr.cmx + 10;
		int num3 = GameScr.cmx + GameCanvas.w - 10;
		int num4 = GameScr.cmy + 10;
		int num5 = GameScr.cmy + GameScr.gH;
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char.statusMe != 15 && !@char.isInvisiblez && num2 <= @char.cx && @char.cx <= num3 && num4 <= @char.cy && @char.cy <= num5 && @char.charID != -114 && (TileMap.mapID != 129 || (TileMap.mapID == 129 && global::Char.myCharz().cy > 264)))
			{
				this.focus.addElement(@char);
				if (this.charFocus != null && @char.Equals(this.charFocus))
				{
					num = this.focus.size();
				}
			}
		}
		if (this.me && this.isAttacPlayerStatus())
		{
			Res.outz("co the tan cong nguoi");
			for (int j = 0; j < GameScr.vMob.size(); j++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(j);
				if (!GameScr.gI().isMeCanAttackMob(mob))
				{
					Res.outz("khong the tan cong quai");
					this.mobFocus = null;
				}
				else
				{
					Res.outz("co the tan ong quai");
					this.focus.addElement(mob);
					if (this.mobFocus != null)
					{
						num = this.focus.size();
					}
				}
			}
			this.npcFocus = null;
			this.itemFocus = null;
			if (this.focus.size() > 0)
			{
				if (num >= this.focus.size())
				{
					num = 0;
				}
				this.focusManualTo(this.focus.elementAt(num));
			}
			else
			{
				this.mobFocus = null;
				this.deFocusNPC();
				this.charFocus = null;
				this.itemFocus = null;
				global::Char.isManualFocus = false;
			}
			return;
		}
		for (int k = 0; k < GameScr.vItemMap.size(); k++)
		{
			ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(k);
			if (num2 <= itemMap.x && itemMap.x <= num3 && num4 <= itemMap.y && itemMap.y <= num5)
			{
				this.focus.addElement(itemMap);
				if (this.itemFocus != null && itemMap.Equals(this.itemFocus))
				{
					num = this.focus.size();
				}
			}
		}
		for (int l = 0; l < GameScr.vMob.size(); l++)
		{
			Mob mob2 = (Mob)GameScr.vMob.elementAt(l);
			if (mob2.status != 1 && mob2.status != 0 && num2 <= mob2.x && mob2.x <= num3 && num4 <= mob2.y && mob2.y <= num5)
			{
				this.focus.addElement(mob2);
				if (this.mobFocus != null && mob2.Equals(this.mobFocus))
				{
					num = this.focus.size();
				}
			}
		}
		for (int m = 0; m < GameScr.vNpc.size(); m++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(m);
			if (npc.statusMe != 15 && num2 <= npc.cx && npc.cx <= num3 && num4 <= npc.cy && npc.cy <= num5)
			{
				this.focus.addElement(npc);
				if (this.npcFocus != null && npc.Equals(this.npcFocus))
				{
					num = this.focus.size();
				}
			}
		}
		if (this.focus.size() > 0)
		{
			if (num >= this.focus.size())
			{
				num = 0;
			}
			this.focusManualTo(this.focus.elementAt(num));
		}
		else
		{
			this.mobFocus = null;
			this.deFocusNPC();
			this.charFocus = null;
			this.itemFocus = null;
			global::Char.isManualFocus = false;
		}
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x000073A7 File Offset: 0x000055A7
	public void deFocusNPC()
	{
		if (this.me && this.npcFocus != null)
		{
			if (!GameCanvas.menu.showMenu)
			{
				global::Char.chatPopup = null;
			}
			this.npcFocus = null;
		}
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x00058D54 File Offset: 0x00056F54
	public void updateCharInBridge()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		if (TileMap.tileTypeAt(this.cx, this.cy + 1, 1024))
		{
			TileMap.setTileTypeAtPixel(this.cx, this.cy + 1, 512);
			TileMap.setTileTypeAtPixel(this.cx, this.cy - 2, 512);
		}
		if (TileMap.tileTypeAt(this.cx - (int)TileMap.size, this.cy + 1, 512))
		{
			TileMap.killTileTypeAt(this.cx - (int)TileMap.size, this.cy + 1, 512);
			TileMap.killTileTypeAt(this.cx - (int)TileMap.size, this.cy - 2, 512);
		}
		if (TileMap.tileTypeAt(this.cx + (int)TileMap.size, this.cy + 1, 512))
		{
			TileMap.killTileTypeAt(this.cx + (int)TileMap.size, this.cy + 1, 512);
			TileMap.killTileTypeAt(this.cx + (int)TileMap.size, this.cy - 2, 512);
		}
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x00058E80 File Offset: 0x00057080
	public static void sort(int[] data)
	{
		int num = 5;
		for (int i = 0; i < num - 1; i++)
		{
			for (int j = i + 1; j < num; j++)
			{
				if (data[i] < data[j])
				{
					int num2 = data[j];
					data[j] = data[i];
					data[i] = num2;
				}
			}
		}
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x000073DB File Offset: 0x000055DB
	public static bool setInsc(int cmX, int cmWx, int x, int cmy, int cmyH, int y)
	{
		return x <= cmWx && x >= cmX && y <= cmyH && y >= cmy;
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x00058ED0 File Offset: 0x000570D0
	public void kickOption(Item item, int maxKick)
	{
		int num = 0;
		if (item != null && item.options != null)
		{
			for (int i = 0; i < item.options.size(); i++)
			{
				ItemOption itemOption = (ItemOption)item.options.elementAt(i);
				itemOption.active = 0;
				if (itemOption.optionTemplate.type == 2)
				{
					if (num < maxKick)
					{
						itemOption.active = 1;
						num++;
					}
				}
				else if (itemOption.optionTemplate.type == 3 && item.upgrade >= 4)
				{
					itemOption.active = 1;
				}
				else if (itemOption.optionTemplate.type == 4 && item.upgrade >= 8)
				{
					itemOption.active = 1;
				}
				else if (itemOption.optionTemplate.type == 5 && item.upgrade >= 12)
				{
					itemOption.active = 1;
				}
				else if (itemOption.optionTemplate.type == 6 && item.upgrade >= 14)
				{
					itemOption.active = 1;
				}
				else if (itemOption.optionTemplate.type == 7 && item.upgrade >= 16)
				{
					itemOption.active = 1;
				}
			}
		}
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x00059018 File Offset: 0x00057218
	public void doInjure(long HPShow, long MPShow, bool isCrit, bool isMob)
	{
		this.isCrit = isCrit;
		this.isMob = isMob;
		Res.outz(string.Concat(new object[]
		{
			"CHP= ",
			this.cHP,
			" dame -= ",
			HPShow,
			" HP FULL= ",
			this.cHPFull
		}));
		this.cHP -= HPShow;
		this.cMP -= MPShow;
		GameScr.gI().isInjureHp = true;
		GameScr.gI().twHp = 0L;
		GameScr.gI().isInjureMp = true;
		GameScr.gI().twMp = 0L;
		if (this.cHP < 0L)
		{
			this.cHP = 0L;
		}
		if (this.cMP < 0L)
		{
			this.cMP = 0L;
		}
		if (isMob || (!isMob && (int)this.cTypePk != 4 && this.damMP != -100L))
		{
			if (HPShow <= 0L)
			{
				if (this.me)
				{
					GameScr.startFlyText(mResources.miss, this.cx, this.cy - this.ch, 0, -2, mFont.MISS_ME);
				}
				else
				{
					GameScr.startFlyText(mResources.miss, this.cx, this.cy - this.ch, 0, -2, mFont.MISS);
				}
			}
			else
			{
				GameScr.startFlyText("-" + HPShow, this.cx, this.cy - this.ch, 0, -2, isCrit ? mFont.FATAL : mFont.RED);
			}
		}
		if (HPShow > 0L)
		{
			this.isInjure = 6;
		}
		ServerEffect.addServerEffect(80, this, 1);
		if (this.isDie)
		{
			this.isDie = false;
			global::Char.isLockKey = false;
			this.startDie((short)this.xSd, (short)this.ySd);
		}
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x0005920C File Offset: 0x0005740C
	public void doInjure()
	{
		GameScr.gI().isInjureHp = true;
		GameScr.gI().twHp = 0L;
		GameScr.gI().isInjureMp = true;
		GameScr.gI().twMp = 0L;
		this.isInjure = 6;
		ServerEffect.addServerEffect(8, this, 1);
		this.isInjureHp = true;
		this.twHp = 0;
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x00059264 File Offset: 0x00057464
	public void startDie(short toX, short toY)
	{
		this.isMonkey = 0;
		this.isWaitMonkey = false;
		if (this.me && this.isDie)
		{
			return;
		}
		if (this.me)
		{
			this.isLockMove = true;
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				@char.killCharId = -9999;
			}
			if (GameCanvas.panel != null && GameCanvas.panel.cp != null)
			{
				GameCanvas.panel.cp = null;
			}
			if (GameCanvas.panel2 != null && GameCanvas.panel2.cp != null)
			{
				GameCanvas.panel2.cp = null;
			}
		}
		this.statusMe = 5;
		this.cp2 = (int)toX;
		this.cp3 = (int)toY;
		this.cp1 = 0;
		this.cHP = 0L;
		this.testCharId = -9999;
		this.killCharId = -9999;
		if (this.me && this.myskill != null && (int)this.myskill.template.id != 14)
		{
			this.stopUseChargeSkill();
		}
		this.cTypePk = 0;
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x000073FF File Offset: 0x000055FF
	public void waitToDie(short toX, short toY)
	{
		this.wdx = toX;
		this.wdy = toY;
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x0005939C File Offset: 0x0005759C
	public void liveFromDead()
	{
		this.cHP = this.cHPFull;
		this.cMP = this.cMPFull;
		this.statusMe = 1;
		this.cp1 = (this.cp2 = (this.cp3 = 0));
		ServerEffect.addServerEffect(109, this, 2);
		GameScr.gI().center = null;
		GameScr.isHaveSelectSkill = true;
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x000593FC File Offset: 0x000575FC
	public bool doUsePotion()
	{
		if (this.arrItemBag == null)
		{
			return false;
		}
		for (int i = 0; i < this.arrItemBag.Length; i++)
		{
			if (this.arrItemBag[i] != null)
			{
				if ((int)this.arrItemBag[i].template.type == 6)
				{
					Service.gI().useItem(0, 1, -1, this.arrItemBag[i].template.id);
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x0005947C File Offset: 0x0005767C
	public bool isLang()
	{
		return TileMap.mapID == 1 || TileMap.mapID == 27 || TileMap.mapID == 72 || TileMap.mapID == 10 || TileMap.mapID == 17 || TileMap.mapID == 22 || TileMap.mapID == 32 || TileMap.mapID == 38 || TileMap.mapID == 43 || TileMap.mapID == 48;
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x00059504 File Offset: 0x00057704
	public bool isMeCanAttackOtherPlayer(global::Char cAtt)
	{
		return cAtt != null && global::Char.myCharz().myskill != null && global::Char.myCharz().myskill.template.type != 2 && (global::Char.myCharz().myskill.template.type != 4 || cAtt.statusMe == 14 || cAtt.statusMe == 5) && ((((int)cAtt.cTypePk == 3 && (int)global::Char.myCharz().cTypePk == 3) || ((int)global::Char.myCharz().cTypePk == 5 || (int)cAtt.cTypePk == 5 || ((int)global::Char.myCharz().cTypePk == 1 && (int)cAtt.cTypePk == 1)) || ((int)global::Char.myCharz().cTypePk == 4 && (int)cAtt.cTypePk == 4) || (global::Char.myCharz().testCharId >= 0 && global::Char.myCharz().testCharId == cAtt.charID) || (global::Char.myCharz().killCharId >= 0 && global::Char.myCharz().killCharId == cAtt.charID && !this.isLang()) || (cAtt.killCharId >= 0 && cAtt.killCharId == global::Char.myCharz().charID && !this.isLang()) || ((int)global::Char.myCharz().cFlag == 8 && (int)cAtt.cFlag != 0) || ((int)global::Char.myCharz().cFlag != 0 && (int)cAtt.cFlag == 8) || ((int)global::Char.myCharz().cFlag != (int)cAtt.cFlag && (int)global::Char.myCharz().cFlag != 0 && (int)cAtt.cFlag != 0)) && cAtt.statusMe != 14) && cAtt.statusMe != 5;
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x000596F8 File Offset: 0x000578F8
	public void clearTask()
	{
		global::Char.myCharz().taskMaint = null;
		for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
		{
			if (global::Char.myCharz().arrItemBag[i] != null && (int)global::Char.myCharz().arrItemBag[i].template.type == 8)
			{
				global::Char.myCharz().arrItemBag[i] = null;
			}
		}
		Npc.clearEffTask();
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x0000740F File Offset: 0x0000560F
	public int getX()
	{
		return this.cx;
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x00007417 File Offset: 0x00005617
	public int getY()
	{
		return this.cy;
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x0000741F File Offset: 0x0000561F
	public int getH()
	{
		return 32;
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x00007423 File Offset: 0x00005623
	public int getW()
	{
		return 24;
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x00059770 File Offset: 0x00057970
	public void focusManualTo(object objectz)
	{
		if (objectz is Mob)
		{
			this.mobFocus = (Mob)objectz;
			this.deFocusNPC();
			this.charFocus = null;
			this.itemFocus = null;
		}
		else if (objectz is Npc)
		{
			global::Char.myCharz().mobFocus = null;
			global::Char.myCharz().deFocusNPC();
			global::Char.myCharz().npcFocus = (Npc)objectz;
			global::Char.myCharz().charFocus = null;
			global::Char.myCharz().itemFocus = null;
		}
		else if (objectz is global::Char)
		{
			global::Char.myCharz().mobFocus = null;
			global::Char.myCharz().deFocusNPC();
			global::Char.myCharz().charFocus = (global::Char)objectz;
			global::Char.myCharz().itemFocus = null;
		}
		else if (objectz is ItemMap)
		{
			global::Char.myCharz().mobFocus = null;
			global::Char.myCharz().deFocusNPC();
			global::Char.myCharz().charFocus = null;
			global::Char.myCharz().itemFocus = (ItemMap)objectz;
		}
		global::Char.isManualFocus = true;
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x000045ED File Offset: 0x000027ED
	public void stopMoving()
	{
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x000045ED File Offset: 0x000027ED
	public void cancelAttack()
	{
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x00004381 File Offset: 0x00002581
	public bool isInvisible()
	{
		return false;
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x00007427 File Offset: 0x00005627
	public bool focusToAttack()
	{
		return this.mobFocus != null || (this.charFocus != null && this.isMeCanAttackOtherPlayer(this.charFocus));
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x0005987C File Offset: 0x00057A7C
	public void addDustEff(int type)
	{
		if (!GameCanvas.lowGraphic)
		{
			if (type == 1)
			{
				if (this.clevel >= 9)
				{
					Effect effect = new Effect(19, this.cx - 5, this.cy + 20, 2, 1, -1);
					EffecMn.addEff(effect);
				}
			}
			else if (type == 2)
			{
				if (this.me && (int)this.isMonkey == 1)
				{
					return;
				}
				if (this.isNhapThe && GameCanvas.gameTick % 5 == 0)
				{
					Effect effect2 = new Effect(22, this.cx - 5, this.cy + 35, 2, 1, -1);
					EffecMn.addEff(effect2);
				}
			}
			else if (type == 3 && this.clevel >= 9 && this.ySd - this.cy <= 5)
			{
				Effect effect3 = new Effect(19, this.cx - 5, this.ySd + 20, 2, 1, -1);
				EffecMn.addEff(effect3);
			}
		}
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x00059978 File Offset: 0x00057B78
	public bool isGetFlagImage(sbyte getFlag)
	{
		bool result = true;
		for (int i = 0; i < GameScr.vFlag.size(); i++)
		{
			PKFlag pkflag = (PKFlag)GameScr.vFlag.elementAt(i);
			if (pkflag != null)
			{
				if ((int)pkflag.cflag == (int)getFlag)
				{
					return true;
				}
				result = false;
			}
		}
		return result;
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x000599D0 File Offset: 0x00057BD0
	private void paintPKFlag(mGraphics g)
	{
		if (this.cdir == 1)
		{
			if ((int)this.cFlag != 0 && (int)this.cFlag != -1)
			{
				SmallImage.drawSmallImage(g, this.flagImage, this.cx - 10, this.cy - this.ch - ((!this.me) ? 30 : 30) + ((GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), 2, 0);
			}
		}
		else if ((int)this.cFlag != 0 && (int)this.cFlag != -1)
		{
			SmallImage.drawSmallImage(g, this.flagImage, this.cx, this.cy - this.ch - ((!this.me) ? 30 : 30) + ((GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), 0, 0);
		}
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x00007451 File Offset: 0x00005651
	public void removeHoleEff()
	{
		if (this.holder)
		{
			this.holder = false;
			this.charHold = null;
			this.mobHold = null;
		}
		else
		{
			this.holdEffID = 0;
			this.charHold = null;
			this.mobHold = null;
		}
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x0000748D File Offset: 0x0000568D
	public void removeProtectEff()
	{
		this.protectEff = false;
		this.eProtect = null;
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x0000749D File Offset: 0x0000569D
	public void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x00059AD0 File Offset: 0x00057CD0
	public void removeEffect()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
		if (this.holder)
		{
			this.holder = false;
		}
		if (this.protectEff)
		{
			this.protectEff = false;
		}
		this.eProtect = null;
		this.charHold = null;
		this.mobHold = null;
		this.blindEff = false;
		this.sleepEff = false;
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x00059B38 File Offset: 0x00057D38
	public void setPos(short xPos, short yPos, sbyte typePos)
	{
		this.isSetPos = true;
		this.xPos = xPos;
		this.yPos = yPos;
		this.typePos = typePos;
		this.tpos = 0;
		if (this.me)
		{
			if (GameCanvas.panel != null)
			{
				GameCanvas.panel.hide();
			}
			if (GameCanvas.panel2 != null)
			{
				GameCanvas.panel2.hide();
			}
		}
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x000074A6 File Offset: 0x000056A6
	public void removeHuytSao()
	{
		this.huytSao = false;
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x000074AF File Offset: 0x000056AF
	public void fusionComplete()
	{
		this.isFusion = false;
		global::Char.isLockKey = false;
		this.tFusion = 0;
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x00059B9C File Offset: 0x00057D9C
	public void setFusion(sbyte fusion)
	{
		this.tFusion = 0;
		if ((int)fusion == 4 || (int)fusion == 5)
		{
			if (this.me)
			{
				Service.gI().funsion(fusion);
			}
			EffecMn.addEff(new Effect(34, this.cx, this.cy + 12, 2, 1, -1));
		}
		if ((int)fusion == 6)
		{
			EffecMn.addEff(new Effect(38, this.cx, this.cy + 12, 2, 1, -1));
		}
		if (this.me)
		{
			GameCanvas.panel.hideNow();
			global::Char.isLockKey = true;
		}
		this.isFusion = true;
		if ((int)fusion == 1)
		{
			this.isNhapThe = false;
		}
		else
		{
			this.isNhapThe = true;
		}
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x000074C5 File Offset: 0x000056C5
	public void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x000074CE File Offset: 0x000056CE
	public void setPartOld()
	{
		this.headTemp = this.head;
		this.bodyTemp = this.body;
		this.legTemp = this.leg;
		this.bagTemp = this.bag;
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x00007500 File Offset: 0x00005700
	public void setPartTemp(int head, int body, int leg, int bag)
	{
		if (head != -1)
		{
			this.head = head;
		}
		if (body != -1)
		{
			this.body = body;
		}
		if (leg != -1)
		{
			this.leg = leg;
		}
		if (bag != -1)
		{
			this.bag = bag;
		}
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x00059C58 File Offset: 0x00057E58
	public void resetPartTemp()
	{
		if (this.headTemp != -1)
		{
			this.head = this.headTemp;
			this.headTemp = -1;
		}
		if (this.bodyTemp != -1)
		{
			this.body = this.bodyTemp;
			this.bodyTemp = -1;
		}
		if (this.legTemp != -1)
		{
			this.leg = this.legTemp;
			this.legTemp = -1;
		}
		if (this.bagTemp != -1)
		{
			this.bag = this.bagTemp;
			this.bagTemp = -1;
		}
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x00059CE4 File Offset: 0x00057EE4
	public Effect getEffById(int id)
	{
		for (int i = 0; i < this.vEffChar.size(); i++)
		{
			Effect effect = (Effect)this.vEffChar.elementAt(i);
			if (effect.effId == id)
			{
				return effect;
			}
		}
		return null;
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0000753C File Offset: 0x0000573C
	public void addEffChar(Effect e)
	{
		this.removeEffChar(0, e.effId);
		this.vEffChar.addElement(e);
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x00007557 File Offset: 0x00005757
	public void removeEffChar(int type, int id)
	{
		if (type == -1)
		{
			this.vEffChar.removeAllElements();
		}
		else if (this.getEffById(id) != null)
		{
			this.vEffChar.removeElement(this.getEffById(id));
		}
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x00059D30 File Offset: 0x00057F30
	public void paintEffBehind(mGraphics g)
	{
		for (int i = 0; i < this.vEffChar.size(); i++)
		{
			Effect effect = (Effect)this.vEffChar.elementAt(i);
			if (effect.layer == 0)
			{
				bool flag = true;
				if (effect.isStand == 0)
				{
					flag = (this.statusMe == 1 || this.statusMe == 6);
				}
				if (flag)
				{
					effect.paint(g);
				}
			}
		}
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x00059DB4 File Offset: 0x00057FB4
	public void paintEffFront(mGraphics g)
	{
		for (int i = 0; i < this.vEffChar.size(); i++)
		{
			Effect effect = (Effect)this.vEffChar.elementAt(i);
			if (effect.layer == 1)
			{
				bool flag = true;
				if (effect.isStand == 0)
				{
					flag = (this.statusMe == 1 || this.statusMe == 6);
				}
				if (flag)
				{
					effect.paint(g);
				}
			}
		}
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x00059E38 File Offset: 0x00058038
	public void updEffChar()
	{
		for (int i = 0; i < this.vEffChar.size(); i++)
		{
			((Effect)this.vEffChar.elementAt(i)).update();
		}
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x0000758E File Offset: 0x0000578E
	public int checkLuong()
	{
		return this.luong + this.luongKhoa;
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x00059E78 File Offset: 0x00058078
	public void updateEye()
	{
		if (this.head == 934)
		{
			if (GameCanvas.timeNow - this.timeAddChopmat > 0L)
			{
				this.fChopmat++;
				if (this.fChopmat > this.frEye.Length - 1)
				{
					this.fChopmat = 0;
					this.timeAddChopmat = GameCanvas.timeNow + (long)Res.random(2000, 3500);
					this.frEye = this.frChopCham;
					if (Res.random(2) == 0)
					{
						this.frEye = this.frChopNhanh;
					}
				}
			}
			else
			{
				this.fChopmat = 0;
			}
		}
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x00059F20 File Offset: 0x00058120
	private void paintRedEye(mGraphics g, int xx, int yy, int trans, int anchor)
	{
		if (this.head == 934 && (this.statusMe == 1 || this.statusMe == 6))
		{
			if (global::Char.fraRedEye == null || global::Char.fraRedEye.imgFrame == null)
			{
				Image img = mSystem.loadImage("/redeye.png");
				global::Char.fraRedEye = new FrameImage(img, 14, 10);
			}
			else if (this.frEye[this.fChopmat] != -1)
			{
				int num = 8;
				int num2 = 15;
				if (trans == 2)
				{
					num = -8;
				}
				global::Char.fraRedEye.drawFrame(this.frEye[this.fChopmat], xx + num, yy + num2, trans, anchor, g);
			}
		}
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x00059FD4 File Offset: 0x000581D4
	public bool isHead_2Fr(int idHead)
	{
		for (int i = 0; i < global::Char.Arr_Head_2Fr.Length; i++)
		{
			if (global::Char.Arr_Head_2Fr[i][0] == idHead)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x0005A00C File Offset: 0x0005820C
	private void updateFHead()
	{
		if (this.isHead_2Fr(this.head))
		{
			this.fHead++;
			if (this.fHead > 10000)
			{
				this.fHead = 0;
			}
		}
		else
		{
			this.fHead = 0;
		}
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x0005A05C File Offset: 0x0005825C
	private int getFHead(int idHead)
	{
		for (int i = 0; i < global::Char.Arr_Head_2Fr.Length; i++)
		{
			if (global::Char.Arr_Head_2Fr[i][0] == idHead)
			{
				return global::Char.Arr_Head_2Fr[i][this.fHead / 4 % global::Char.Arr_Head_2Fr[i].Length];
			}
		}
		return idHead;
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x0005A0B0 File Offset: 0x000582B0
	public void paintAuraBehind(mGraphics g)
	{
		if (this.me && !global::Char.isPaintAura)
		{
			return;
		}
		if (this.idAuraEff <= -1)
		{
			return;
		}
		if ((this.statusMe == 1 || this.statusMe == 6) && !GameCanvas.panel.isShow && mSystem.currentTimeMillis() - this.timeBlue > 0L)
		{
			string nameImg = this.strEffAura + this.idAuraEff + "_0";
			FrameImage fraImage = mSystem.getFraImage(nameImg);
			if (fraImage != null)
			{
				fraImage.drawFrame(GameCanvas.gameTick / 4 % fraImage.nFrame, this.cx, this.cy, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
			}
		}
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x0005A184 File Offset: 0x00058384
	public void paintAuraFront(mGraphics g)
	{
		if (this.me && !global::Char.isPaintAura)
		{
			return;
		}
		if (this.idAuraEff <= -1)
		{
			return;
		}
		if (this.statusMe == 1 || this.statusMe == 6)
		{
			if (!GameCanvas.panel.isShow && !GameCanvas.lowGraphic)
			{
				bool flag = false;
				if (mSystem.currentTimeMillis() - this.timeBlue > -1000L && this.IsAddDust1)
				{
					flag = true;
					this.IsAddDust1 = false;
				}
				if (mSystem.currentTimeMillis() - this.timeBlue > -500L && this.IsAddDust2)
				{
					flag = true;
					this.IsAddDust2 = false;
				}
				if (flag)
				{
					GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
					GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
					this.addDustEff(1);
				}
				if (mSystem.currentTimeMillis() - this.timeBlue > 0L && !(mGraphics.zoomLevel == 1 && this.idAuraEff == 2))
				{
					string nameImg = this.strEffAura + this.idAuraEff + "_1";
					FrameImage fraImage = mSystem.getFraImage(nameImg);
					if (fraImage != null)
					{
						fraImage.drawFrame(GameCanvas.gameTick / 4 % fraImage.nFrame, this.cx, this.cy + 2, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					}
				}
			}
		}
		else
		{
			this.timeBlue = mSystem.currentTimeMillis() + 1500L;
			this.IsAddDust1 = true;
			this.IsAddDust2 = true;
		}
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x0005A324 File Offset: 0x00058524
	public void paintEff_Lvup_behind(mGraphics g)
	{
		if (this.idEff_Set_Item == -1)
		{
			return;
		}
		if (this.fraEff != null)
		{
			this.fraEff.drawFrame(GameCanvas.gameTick / 4 % this.fraEff.nFrame, this.cx, this.cy + 3, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
		}
		else
		{
			this.fraEff = mSystem.getFraImage(this.strEff_Set_Item + this.idEff_Set_Item + "_0");
		}
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x0005A3C0 File Offset: 0x000585C0
	public void paintEff_Lvup_front(mGraphics g)
	{
		if (this.idEff_Set_Item == -1)
		{
			return;
		}
		if (this.fraEffSub != null)
		{
			this.fraEffSub.drawFrame(GameCanvas.gameTick / 4 % this.fraEffSub.nFrame, this.cx, this.cy + 8, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
		}
		else
		{
			this.fraEffSub = mSystem.getFraImage(this.strEff_Set_Item + this.idEff_Set_Item + "_1");
		}
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x0005A45C File Offset: 0x0005865C
	public void paintHat_behind(mGraphics g, int cf, int yh)
	{
		try
		{
			if (this.idHat != -1)
			{
				if (this.isFrNgang(cf))
				{
					if (this.fraHat_behind_2 != null)
					{
						this.fraHat_behind_2.drawFrame(GameCanvas.gameTick / 4 % this.fraHat_behind_2.nFrame, this.cx + global::Char.hatInfo[cf][0] * ((this.cdir != 1) ? -1 : 1), yh + global::Char.hatInfo[cf][1], (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					}
					else
					{
						this.fraHat_behind_2 = mSystem.getFraImage(this.strHat_behind + this.strNgang + this.idHat);
					}
				}
				else if (this.fraHat_behind != null)
				{
					this.fraHat_behind.drawFrame(GameCanvas.gameTick / 4 % this.fraHat_behind.nFrame, this.cx + global::Char.hatInfo[cf][0] * ((this.cdir != 1) ? -1 : 1), yh + global::Char.hatInfo[cf][1], (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
				}
				else
				{
					this.fraHat_behind = mSystem.getFraImage(this.strHat_behind + this.idHat);
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x0005A5F4 File Offset: 0x000587F4
	public void paintHat_front(mGraphics g, int cf, int yh)
	{
		try
		{
			if (this.idHat != -1)
			{
				if (this.isFrNgang(cf))
				{
					if (this.fraHat_font_2 != null)
					{
						this.fraHat_font_2.drawFrame(GameCanvas.gameTick / 4 % this.fraHat_font_2.nFrame, this.cx + global::Char.hatInfo[cf][0] * ((this.cdir != 1) ? -1 : 1), yh + global::Char.hatInfo[cf][1], (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					}
					else
					{
						this.fraHat_font_2 = mSystem.getFraImage(this.strHat_font + this.strNgang + this.idHat);
					}
				}
				else if (this.fraHat_font != null)
				{
					this.fraHat_font.drawFrame(GameCanvas.gameTick / 4 % this.fraHat_font.nFrame, this.cx + global::Char.hatInfo[cf][0] * ((this.cdir != 1) ? -1 : 1), yh + global::Char.hatInfo[cf][1], (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
				}
				else
				{
					this.fraHat_font = mSystem.getFraImage(this.strHat_font + this.idHat);
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x0005A78C File Offset: 0x0005898C
	public bool isFrNgang(int fr)
	{
		return fr == 2 || fr == 3 || fr == 4 || fr == 5 || fr == 6 || fr == 9 || fr == 10 || fr == 13 || fr == 14 || fr == 15 || fr == 16 || fr == 26 || fr == 27 || fr == 28 || fr == 29;
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x0005A810 File Offset: 0x00058A10
	public void sendNewAttack(short idTemplateSkill)
	{
		short x = -1;
		short y = -1;
		if (this.mobFocus != null)
		{
			x = (short)this.mobFocus.x;
			y = (short)this.mobFocus.y;
		}
		if (this.charFocus != null && !this.charFocus.isPet && !this.charFocus.isMiniPet)
		{
			x = (short)this.charFocus.cx;
			y = (short)this.charFocus.cy;
		}
		Service.gI().new_skill_not_focus((sbyte)idTemplateSkill, (sbyte)this.cdir, x, y);
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x0005A8A0 File Offset: 0x00058AA0
	public void SetSkillPaint_NEW(short idskillPaint, bool isFly, sbyte typeFrame, sbyte typePaint, sbyte dir, short timeGong, sbyte typeItem)
	{
		this.isPaintNewSkill = true;
		this.timeReset_newSkill = GameCanvas.timeNow + 10000L;
		this.idskillPaint = idskillPaint;
		this.isFly = isFly;
		this.typeFrame = typeFrame;
		this.typePaint = typePaint;
		this.typeItem = typeItem;
		this.cdir = (int)dir;
		this.count_NEW = 0;
		this.stt = 0;
		long lastTimeUseThisSkill = mSystem.currentTimeMillis();
		if (this.me)
		{
			this.saveLoadPreviousSkill();
			this.myskill.lastTimeUseThisSkill = lastTimeUseThisSkill;
			if (this.myskill.template.manaUseType == 2)
			{
				this.cMP = 1L;
			}
			else if (this.myskill.template.manaUseType != 1)
			{
				this.cMP -= (long)this.myskill.manaUse;
			}
			else
			{
				this.cMP -= (long)this.myskill.manaUse * this.cMPFull / 100L;
			}
			global::Char.myCharz().cStamina--;
			GameScr.gI().isInjureMp = true;
			GameScr.gI().twMp = 0L;
			if (this.cMP < 0L)
			{
				this.cMP = 0L;
			}
		}
		if (idskillPaint == 24)
		{
			GameScr.addEffectEnd_Target(18, 0, (int)typePaint, this.clone(), null, 3, timeGong, 0);
			GameScr.addEffectEnd_Target(21, 0, (int)typePaint, this.clone(), null, 1, timeGong, 0);
		}
		else if (idskillPaint == 25)
		{
			GameScr.addEffectEnd_Target(19, 0, (int)typePaint, this.clone(), null, 3, timeGong, 0);
			GameScr.addEffectEnd_Target(22, 0, (int)typePaint, this.clone(), null, 1, timeGong, 0);
		}
		else if (idskillPaint == 26)
		{
			GameScr.addEffectEnd_Target(20, 0, (int)typePaint, this.clone(), null, 3, timeGong, 0);
			GameScr.addEffectEnd_Target(23, 0, (int)typePaint, this.clone(), null, 1, timeGong, 0);
		}
		if ((int)this.typeFrame == 1)
		{
			if (!this.isFly)
			{
				this.fr_start = new byte[]
				{
					20,
					20,
					20,
					20,
					20,
					20,
					19
				};
				this.fr_atk = new byte[]
				{
					20
				};
				this.fr_end = new byte[1];
			}
			else
			{
				this.fr_start = new byte[]
				{
					31,
					31,
					31,
					31,
					31,
					31,
					30
				};
				this.fr_atk = new byte[]
				{
					31
				};
				this.fr_end = new byte[]
				{
					12
				};
			}
		}
		if ((int)this.typeFrame == 2)
		{
			if (!this.isFly)
			{
				this.fr_start = new byte[]
				{
					20
				};
				this.fr_atk = new byte[]
				{
					13,
					13,
					13,
					14,
					14,
					14
				};
				this.fr_end = new byte[1];
			}
			else
			{
				this.fr_start = new byte[]
				{
					31
				};
				this.fr_atk = new byte[]
				{
					26,
					26,
					26,
					27,
					27,
					27
				};
				this.fr_end = new byte[]
				{
					12
				};
			}
		}
		if ((int)this.typeFrame == 4)
		{
			if (!this.isFly)
			{
				this.fr_start = new byte[]
				{
					17,
					17,
					17,
					18,
					18,
					18
				};
				this.fr_atk = new byte[]
				{
					18
				};
				this.fr_end = new byte[1];
			}
			else
			{
				this.fr_start = new byte[]
				{
					7,
					7,
					7,
					12,
					12,
					12,
					12
				};
				this.fr_atk = new byte[]
				{
					12
				};
				this.fr_end = new byte[]
				{
					12
				};
			}
		}
		if ((int)this.typeFrame == 3)
		{
			if (!this.isFly)
			{
				this.fr_start = new byte[]
				{
					24,
					24,
					24,
					17,
					17,
					17,
					18,
					18,
					18
				};
				this.fr_atk = new byte[]
				{
					20
				};
				this.fr_end = new byte[1];
			}
			else
			{
				this.fr_start = new byte[]
				{
					23,
					23,
					23,
					7,
					7,
					7,
					12,
					12,
					12,
					12
				};
				this.fr_atk = new byte[]
				{
					31
				};
				this.fr_end = new byte[]
				{
					12
				};
			}
		}
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x0005ACB0 File Offset: 0x00058EB0
	public void SetSkillPaint_STT(int stt, short idskillPaint, Point targetDame, short timeDame, short rangeDame, sbyte typePaint, Point[] listObj, sbyte typeItem)
	{
		this.stt = stt;
		this.idskillPaint = idskillPaint;
		this.count_NEW = 0;
		this.targetDame = targetDame;
		this.typePaint = typePaint;
		this.timeDame = mSystem.currentTimeMillis() + (long)timeDame;
		this.rangeDame = rangeDame;
		this.typeItem = typeItem;
		if (this.stt != 1)
		{
			return;
		}
		if (this.idskillPaint == 24)
		{
			GameScr.addEffectEnd_Target(18, 1, (int)typePaint, this, null, 3, timeDame, 0);
			GameScr.addEffectEnd_Target(24, 0, (int)typePaint, this, this.targetDame, 1, timeDame, rangeDame);
		}
		if (this.idskillPaint == 25)
		{
			GameScr.addEffectEnd_Target(19, 0, (int)typePaint, this, null, 3, timeDame, 0);
			GameScr.addEffectEnd_Target(25, 0, (int)typePaint, this, this.targetDame, 1, timeDame, rangeDame);
		}
		if (this.idskillPaint == 26)
		{
			GameScr.addEffectEnd_Target(20, 0, (int)typePaint, this, null, 3, timeDame, 0);
			GameScr.addEffectEnd(26, (int)typeItem, (int)typePaint, targetDame.x, targetDame.y, 1, 0, timeDame, listObj);
		}
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x0005ADB4 File Offset: 0x00058FB4
	public void UpdSkillPaint_NEW()
	{
		if (this.stt == 0)
		{
			if (this.isFly && this.count_NEW < 20)
			{
				this.cvy = -3;
				this.cy += this.cvy;
			}
			if (this.fr_start.Length == 1)
			{
				this.cf = (int)this.fr_start[0];
			}
			else if (this.count_NEW > this.fr_start.Length - 1)
			{
				this.cf = (int)this.fr_start[this.fr_start.Length - 1];
			}
			else
			{
				this.cf = (int)this.fr_start[this.count_NEW];
			}
		}
		else if (this.stt == 1)
		{
			this.cf = (int)this.fr_atk[this.count_NEW % this.fr_atk.Length];
			if (mSystem.currentTimeMillis() - this.timeDame > 0L)
			{
				this.SetSkillPaint_STT(2, 0, null, 0, 0, 0, null, 0);
			}
			if (this.count_NEW % 5 == 0)
			{
				GameScr.shock_scr = 5;
			}
			if ((int)this.typeFrame == 1 && this.count_NEW < 10 && !TileMap.tileTypeAt(this.cx - (this.chw + 1) * this.cdir, this.cy, (this.cdir != 1) ? 4 : 8))
			{
				this.cx -= this.cdir;
			}
			if ((int)this.typeFrame == 2)
			{
			}
		}
		else if (this.stt == 2)
		{
			if (this.fr_end.Length == 1)
			{
				this.cf = (int)this.fr_end[0];
			}
			else if (this.count_NEW > this.fr_end.Length - 1)
			{
				this.cf = (int)this.fr_end[this.fr_end.Length - 1];
			}
			else
			{
				this.cf = (int)this.fr_end[this.count_NEW];
			}
			if (this.isFly)
			{
				this.cvx = (this.cvy = 0);
				this.statusMe = 4;
			}
			this.isPaintNewSkill = false;
		}
		this.count_NEW++;
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x0005AFE8 File Offset: 0x000591E8
	public global::Char clone()
	{
		global::Char @char = new global::Char();
		@char.charID = this.charID;
		@char.cx = this.cx;
		@char.cy = this.cy;
		@char.cdir = this.cdir;
		if (this.arrItemBody != null)
		{
			@char.arrItemBody = new Item[this.arrItemBody.Length];
			for (int i = 0; i < this.arrItemBody.Length; i++)
			{
				if (this.arrItemBody[i] == null)
				{
					@char.arrItemBody[i] = null;
				}
				else
				{
					@char.arrItemBody[i] = this.arrItemBody[i].clone();
				}
			}
		}
		return @char;
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x0005B094 File Offset: 0x00059294
	public bool containsCaiTrang(int v)
	{
		if (this.arrItemBody != null)
		{
			for (int i = 0; i < this.arrItemBody.Length; i++)
			{
				if (this.arrItemBody[i] != null && this.arrItemBody[i].template != null)
				{
					if ((int)this.arrItemBody[i].template.id == v)
					{
						return true;
					}
				}
			}
		}
		Res.err("tim kiem id cai trang " + v + " ko tim thay");
		return false;
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x0005B120 File Offset: 0x00059320
	public void printlog()
	{
		string text = string.Empty;
		string text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isInjure,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isMonkey,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isAddChopMat,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isAttack,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isAttFly,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			global::Char.ischangingMap,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isCharge,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isCopy,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isCreateDark,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isCrit,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isDirtyPostion,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isEndMount,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isEventMount,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isMafuba,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isFusion,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isFeetEff,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isFlying,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isWaitMonkey,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isUseSkillSpec(),
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isDie,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isDie,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isDie,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isDie,
			"\n"
		});
		Res.outz(text);
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x0005B588 File Offset: 0x00059788
	public void setDanhHieu(int smallDanhHieu, int frame)
	{
		if (this.mainImg == null)
		{
			this.mainImg = ImgByName.getImagePath("banner_" + 0, ImgByName.hashImagePath);
		}
		if (this.mainImg.img != null)
		{
			int num = this.mainImg.img.getHeight() / (int)this.mainImg.nFrame;
			if (num < 1)
			{
				num = 1;
			}
			this.fraDanhHieu = new FrameImage(this.mainImg.img, this.mainImg.img.getWidth(), num);
		}
		Res.err("===== tim thay DanhHieu ve danh hieu ra");
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x0005B620 File Offset: 0x00059820
	static Char()
	{
		int[][] array = new int[32][];
		array[0] = new int[]
		{
			5,
			-7
		};
		array[1] = new int[]
		{
			5,
			-7
		};
		array[2] = new int[]
		{
			5,
			-8
		};
		array[3] = new int[]
		{
			5,
			-7
		};
		array[4] = new int[]
		{
			5,
			-6
		};
		array[5] = new int[]
		{
			5,
			-8
		};
		array[6] = new int[]
		{
			5,
			-7
		};
		int num = 7;
		int[] array2 = new int[2];
		array2[0] = 9;
		array[num] = array2;
		array[8] = new int[]
		{
			11,
			1
		};
		int num2 = 9;
		int[] array3 = new int[2];
		array3[0] = 4;
		array[num2] = array3;
		array[10] = new int[]
		{
			4,
			-1
		};
		array[11] = new int[]
		{
			4,
			8
		};
		array[12] = new int[]
		{
			6,
			5
		};
		array[13] = new int[]
		{
			6,
			-6
		};
		array[14] = new int[]
		{
			2,
			-5
		};
		array[15] = new int[]
		{
			7,
			-8
		};
		array[16] = new int[]
		{
			7,
			-6
		};
		int num3 = 17;
		int[] array4 = new int[2];
		array4[0] = 8;
		array[num3] = array4;
		array[18] = new int[]
		{
			7,
			5
		};
		array[19] = new int[]
		{
			9,
			-7
		};
		array[20] = new int[]
		{
			7,
			-3
		};
		array[21] = new int[]
		{
			2,
			8
		};
		array[22] = new int[]
		{
			4,
			5
		};
		array[23] = new int[]
		{
			10,
			-5
		};
		array[24] = new int[]
		{
			9,
			-5
		};
		array[25] = new int[]
		{
			9,
			-5
		};
		array[26] = new int[]
		{
			6,
			-6
		};
		array[27] = new int[]
		{
			2,
			-5
		};
		array[28] = new int[]
		{
			7,
			-8
		};
		array[29] = new int[]
		{
			7,
			-6
		};
		array[30] = new int[]
		{
			9,
			-7
		};
		array[31] = new int[]
		{
			7,
			-3
		};
		global::Char.hatInfo = array;
		global::Char.Arr_Head_FlyMove = new short[0];
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x0005C580 File Offset: 0x0005A780
	public string getGender()
	{
		string result;
		switch (this.cgender)
		{
		case 0:
			result = "TD";
			break;
		case 1:
			result = "NM";
			break;
		case 2:
			result = "XD";
			break;
		default:
			result = "";
			break;
		}
		return result;
	}

	// Token: 0x04000AE1 RID: 2785
	public string xuStr;

	// Token: 0x04000AE2 RID: 2786
	public string luongStr;

	// Token: 0x04000AE3 RID: 2787
	public string luongKhoaStr;

	// Token: 0x04000AE4 RID: 2788
	public long lastUpdateTime;

	// Token: 0x04000AE5 RID: 2789
	public bool meLive;

	// Token: 0x04000AE6 RID: 2790
	public bool isMask;

	// Token: 0x04000AE7 RID: 2791
	public bool isTeleport;

	// Token: 0x04000AE8 RID: 2792
	public bool isUsePlane;

	// Token: 0x04000AE9 RID: 2793
	public int shadowX;

	// Token: 0x04000AEA RID: 2794
	public int shadowY;

	// Token: 0x04000AEB RID: 2795
	public int shadowLife;

	// Token: 0x04000AEC RID: 2796
	public bool isNhapThe;

	// Token: 0x04000AED RID: 2797
	public PetFollow petFollow;

	// Token: 0x04000AEE RID: 2798
	public int rank;

	// Token: 0x04000AEF RID: 2799
	public const sbyte A_STAND = 1;

	// Token: 0x04000AF0 RID: 2800
	public const sbyte A_RUN = 2;

	// Token: 0x04000AF1 RID: 2801
	public const sbyte A_JUMP = 3;

	// Token: 0x04000AF2 RID: 2802
	public const sbyte A_FALL = 4;

	// Token: 0x04000AF3 RID: 2803
	public const sbyte A_DEADFLY = 5;

	// Token: 0x04000AF4 RID: 2804
	public const sbyte A_NOTHING = 6;

	// Token: 0x04000AF5 RID: 2805
	public const sbyte A_ATTK = 7;

	// Token: 0x04000AF6 RID: 2806
	public const sbyte A_INJURE = 8;

	// Token: 0x04000AF7 RID: 2807
	public const sbyte A_AUTOJUMP = 9;

	// Token: 0x04000AF8 RID: 2808
	public const sbyte A_FLY = 10;

	// Token: 0x04000AF9 RID: 2809
	public const sbyte SKILL_STAND = 12;

	// Token: 0x04000AFA RID: 2810
	public const sbyte SKILL_FALL = 13;

	// Token: 0x04000AFB RID: 2811
	public const sbyte A_DEAD = 14;

	// Token: 0x04000AFC RID: 2812
	public const sbyte A_HIDE = 15;

	// Token: 0x04000AFD RID: 2813
	public const sbyte A_RESETPOINT = 16;

	// Token: 0x04000AFE RID: 2814
	public static ChatPopup chatPopup;

	// Token: 0x04000AFF RID: 2815
	public long cPower;

	// Token: 0x04000B00 RID: 2816
	public Info chatInfo;

	// Token: 0x04000B01 RID: 2817
	public sbyte petStatus;

	// Token: 0x04000B02 RID: 2818
	public int cx = 24;

	// Token: 0x04000B03 RID: 2819
	public int cy = 24;

	// Token: 0x04000B04 RID: 2820
	public int cvx;

	// Token: 0x04000B05 RID: 2821
	public int cvy;

	// Token: 0x04000B06 RID: 2822
	public int cp1;

	// Token: 0x04000B07 RID: 2823
	public int cp2;

	// Token: 0x04000B08 RID: 2824
	public int cp3;

	// Token: 0x04000B09 RID: 2825
	public int statusMe = 5;

	// Token: 0x04000B0A RID: 2826
	public int cdir = 1;

	// Token: 0x04000B0B RID: 2827
	public int charID;

	// Token: 0x04000B0C RID: 2828
	public int cgender;

	// Token: 0x04000B0D RID: 2829
	public int ctaskId;

	// Token: 0x04000B0E RID: 2830
	public int menuSelect;

	// Token: 0x04000B0F RID: 2831
	public int cBonusSpeed;

	// Token: 0x04000B10 RID: 2832
	public int cspeed = 4;

	// Token: 0x04000B11 RID: 2833
	public int cCriticalFull;

	// Token: 0x04000B12 RID: 2834
	public int cCritDameFull;

	// Token: 0x04000B13 RID: 2835
	public int clevel;

	// Token: 0x04000B14 RID: 2836
	public int xReload;

	// Token: 0x04000B15 RID: 2837
	public int yReload;

	// Token: 0x04000B16 RID: 2838
	public int cyStartFall;

	// Token: 0x04000B17 RID: 2839
	public int saveStatus;

	// Token: 0x04000B18 RID: 2840
	public int eff5BuffHp;

	// Token: 0x04000B19 RID: 2841
	public int eff5BuffMp;

	// Token: 0x04000B1A RID: 2842
	public int cdameDown;

	// Token: 0x04000B1B RID: 2843
	public int cStr;

	// Token: 0x04000B1C RID: 2844
	public long cMP;

	// Token: 0x04000B1D RID: 2845
	public long cHP;

	// Token: 0x04000B1E RID: 2846
	public long cHPNew;

	// Token: 0x04000B1F RID: 2847
	public long cHPShow;

	// Token: 0x04000B20 RID: 2848
	public long cHPFull;

	// Token: 0x04000B21 RID: 2849
	public long cMPFull;

	// Token: 0x04000B22 RID: 2850
	public long cDamFull;

	// Token: 0x04000B23 RID: 2851
	public long cDefull;

	// Token: 0x04000B24 RID: 2852
	public long cGiamST;

	// Token: 0x04000B25 RID: 2853
	public long cLevelPercent;

	// Token: 0x04000B26 RID: 2854
	public long cTiemNang;

	// Token: 0x04000B27 RID: 2855
	public long cNangdong;

	// Token: 0x04000B28 RID: 2856
	public long damHP;

	// Token: 0x04000B29 RID: 2857
	public long damMP;

	// Token: 0x04000B2A RID: 2858
	public bool isMob;

	// Token: 0x04000B2B RID: 2859
	public bool isCrit;

	// Token: 0x04000B2C RID: 2860
	public bool isDie;

	// Token: 0x04000B2D RID: 2861
	public int pointUydanh;

	// Token: 0x04000B2E RID: 2862
	public int pointNon;

	// Token: 0x04000B2F RID: 2863
	public int pointVukhi;

	// Token: 0x04000B30 RID: 2864
	public int pointAo;

	// Token: 0x04000B31 RID: 2865
	public int pointLien;

	// Token: 0x04000B32 RID: 2866
	public int pointGangtay;

	// Token: 0x04000B33 RID: 2867
	public int pointNhan;

	// Token: 0x04000B34 RID: 2868
	public int pointQuan;

	// Token: 0x04000B35 RID: 2869
	public int pointNgocboi;

	// Token: 0x04000B36 RID: 2870
	public int pointGiay;

	// Token: 0x04000B37 RID: 2871
	public int pointPhu;

	// Token: 0x04000B38 RID: 2872
	public int countFinishDay;

	// Token: 0x04000B39 RID: 2873
	public int countLoopBoos;

	// Token: 0x04000B3A RID: 2874
	public int limitTiemnangso;

	// Token: 0x04000B3B RID: 2875
	public int limitKynangso;

	// Token: 0x04000B3C RID: 2876
	public short[] potential = new short[4];

	// Token: 0x04000B3D RID: 2877
	public string cName = string.Empty;

	// Token: 0x04000B3E RID: 2878
	public int clanID;

	// Token: 0x04000B3F RID: 2879
	public sbyte ctypeClan;

	// Token: 0x04000B40 RID: 2880
	public Clan clan;

	// Token: 0x04000B41 RID: 2881
	public sbyte role;

	// Token: 0x04000B42 RID: 2882
	public int cw = 22;

	// Token: 0x04000B43 RID: 2883
	public int ch = 32;

	// Token: 0x04000B44 RID: 2884
	public int chw = 11;

	// Token: 0x04000B45 RID: 2885
	public int chh = 16;

	// Token: 0x04000B46 RID: 2886
	public Command cmdMenu;

	// Token: 0x04000B47 RID: 2887
	public bool canFly = true;

	// Token: 0x04000B48 RID: 2888
	public bool cmtoChar;

	// Token: 0x04000B49 RID: 2889
	public bool me;

	// Token: 0x04000B4A RID: 2890
	public bool cFinishedAttack;

	// Token: 0x04000B4B RID: 2891
	public bool cchistlast;

	// Token: 0x04000B4C RID: 2892
	public bool isAttack;

	// Token: 0x04000B4D RID: 2893
	public bool isAttFly;

	// Token: 0x04000B4E RID: 2894
	public int cwpt;

	// Token: 0x04000B4F RID: 2895
	public int cwplv;

	// Token: 0x04000B50 RID: 2896
	public int cf;

	// Token: 0x04000B51 RID: 2897
	public int tick;

	// Token: 0x04000B52 RID: 2898
	public static bool fallAttack;

	// Token: 0x04000B53 RID: 2899
	public bool isJump;

	// Token: 0x04000B54 RID: 2900
	public bool autoFall;

	// Token: 0x04000B55 RID: 2901
	public bool attack = true;

	// Token: 0x04000B56 RID: 2902
	public long xu;

	// Token: 0x04000B57 RID: 2903
	public int xuInBox;

	// Token: 0x04000B58 RID: 2904
	public int yen;

	// Token: 0x04000B59 RID: 2905
	public int gold_lock;

	// Token: 0x04000B5A RID: 2906
	public int luong;

	// Token: 0x04000B5B RID: 2907
	public int luongKhoa;

	// Token: 0x04000B5C RID: 2908
	public NClass nClass;

	// Token: 0x04000B5D RID: 2909
	public Command endMovePointCommand;

	// Token: 0x04000B5E RID: 2910
	public MyVector vSkill = new MyVector();

	// Token: 0x04000B5F RID: 2911
	public MyVector vSkillFight = new MyVector();

	// Token: 0x04000B60 RID: 2912
	public MyVector vEff = new MyVector();

	// Token: 0x04000B61 RID: 2913
	public Skill myskill;

	// Token: 0x04000B62 RID: 2914
	public Task taskMaint;

	// Token: 0x04000B63 RID: 2915
	public bool paintName = true;

	// Token: 0x04000B64 RID: 2916
	public Archivement[] arrArchive;

	// Token: 0x04000B65 RID: 2917
	public Item[] arrItemBag;

	// Token: 0x04000B66 RID: 2918
	public Item[] arrItemBox;

	// Token: 0x04000B67 RID: 2919
	public Item[] arrItemBody;

	// Token: 0x04000B68 RID: 2920
	public Skill[] arrPetSkill;

	// Token: 0x04000B69 RID: 2921
	public Item[][] arrItemShop;

	// Token: 0x04000B6A RID: 2922
	public string[][] infoSpeacialSkill;

	// Token: 0x04000B6B RID: 2923
	public short[][] imgSpeacialSkill;

	// Token: 0x04000B6C RID: 2924
	public short cResFire;

	// Token: 0x04000B6D RID: 2925
	public short cResIce;

	// Token: 0x04000B6E RID: 2926
	public short cResWind;

	// Token: 0x04000B6F RID: 2927
	public short cMiss;

	// Token: 0x04000B70 RID: 2928
	public short cExactly;

	// Token: 0x04000B71 RID: 2929
	public short cFatal;

	// Token: 0x04000B72 RID: 2930
	public sbyte cPk;

	// Token: 0x04000B73 RID: 2931
	public sbyte cTypePk;

	// Token: 0x04000B74 RID: 2932
	public short cReactDame;

	// Token: 0x04000B75 RID: 2933
	public short sysUp;

	// Token: 0x04000B76 RID: 2934
	public short sysDown;

	// Token: 0x04000B77 RID: 2935
	public int avatar;

	// Token: 0x04000B78 RID: 2936
	public int skillTemplateId;

	// Token: 0x04000B79 RID: 2937
	public Mob mobFocus;

	// Token: 0x04000B7A RID: 2938
	public Mob mobMe;

	// Token: 0x04000B7B RID: 2939
	public int tMobMeBorn;

	// Token: 0x04000B7C RID: 2940
	public Npc npcFocus;

	// Token: 0x04000B7D RID: 2941
	public global::Char charFocus;

	// Token: 0x04000B7E RID: 2942
	public ItemMap itemFocus;

	// Token: 0x04000B7F RID: 2943
	public MyVector focus = new MyVector();

	// Token: 0x04000B80 RID: 2944
	public Mob[] attMobs;

	// Token: 0x04000B81 RID: 2945
	public global::Char[] attChars;

	// Token: 0x04000B82 RID: 2946
	public short[] moveFast;

	// Token: 0x04000B83 RID: 2947
	public int testCharId = -9999;

	// Token: 0x04000B84 RID: 2948
	public int killCharId = -9999;

	// Token: 0x04000B85 RID: 2949
	public sbyte resultTest;

	// Token: 0x04000B86 RID: 2950
	public int countKill;

	// Token: 0x04000B87 RID: 2951
	public int countKillMax;

	// Token: 0x04000B88 RID: 2952
	public bool isInvisiblez;

	// Token: 0x04000B89 RID: 2953
	public bool isShadown = true;

	// Token: 0x04000B8A RID: 2954
	public const sbyte PK_NORMAL = 0;

	// Token: 0x04000B8B RID: 2955
	public const sbyte PK_PHE = 1;

	// Token: 0x04000B8C RID: 2956
	public const sbyte PK_BANG = 2;

	// Token: 0x04000B8D RID: 2957
	public const sbyte PK_THIDAU = 3;

	// Token: 0x04000B8E RID: 2958
	public const sbyte PK_LUYENTAP = 4;

	// Token: 0x04000B8F RID: 2959
	public const sbyte PK_TUDO = 5;

	// Token: 0x04000B90 RID: 2960
	public MyVector taskOrders = new MyVector();

	// Token: 0x04000B91 RID: 2961
	public int cStamina;

	// Token: 0x04000B92 RID: 2962
	public static short[] idHead;

	// Token: 0x04000B93 RID: 2963
	public static short[] idAvatar;

	// Token: 0x04000B94 RID: 2964
	public int exp;

	// Token: 0x04000B95 RID: 2965
	public string[] strLevel;

	// Token: 0x04000B96 RID: 2966
	public string currStrLevel;

	// Token: 0x04000B97 RID: 2967
	public static Image eyeTraiDat = GameCanvas.loadImage("/mainImage/myTexture2dmat-trai-dat.png");

	// Token: 0x04000B98 RID: 2968
	public static Image eyeNamek = GameCanvas.loadImage("/mainImage/myTexture2dmat-namek.png");

	// Token: 0x04000B99 RID: 2969
	public bool isFreez;

	// Token: 0x04000B9A RID: 2970
	public bool isCharge;

	// Token: 0x04000B9B RID: 2971
	public int seconds;

	// Token: 0x04000B9C RID: 2972
	public int freezSeconds;

	// Token: 0x04000B9D RID: 2973
	public long last;

	// Token: 0x04000B9E RID: 2974
	public long cur;

	// Token: 0x04000B9F RID: 2975
	public long lastFreez;

	// Token: 0x04000BA0 RID: 2976
	public long currFreez;

	// Token: 0x04000BA1 RID: 2977
	public bool isFlyUp;

	// Token: 0x04000BA2 RID: 2978
	public static MyVector vItemTime = new MyVector();

	// Token: 0x04000BA3 RID: 2979
	public static short ID_NEW_MOUNT = 30000;

	// Token: 0x04000BA4 RID: 2980
	public short idMount;

	// Token: 0x04000BA5 RID: 2981
	public bool isHaveMount;

	// Token: 0x04000BA6 RID: 2982
	public bool isMountVip;

	// Token: 0x04000BA7 RID: 2983
	public bool isEventMount;

	// Token: 0x04000BA8 RID: 2984
	public bool isSpeacialMount;

	// Token: 0x04000BA9 RID: 2985
	public static Image imgMount_TD = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi10.png");

	// Token: 0x04000BAA RID: 2986
	public static Image imgMount_NM = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi20.png");

	// Token: 0x04000BAB RID: 2987
	public static Image imgMount_NM_1 = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi21.png");

	// Token: 0x04000BAC RID: 2988
	public static Image imgMount_XD = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi30.png");

	// Token: 0x04000BAD RID: 2989
	public static Image imgMount_TD_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi11.png");

	// Token: 0x04000BAE RID: 2990
	public static Image imgMount_NM_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi22.png");

	// Token: 0x04000BAF RID: 2991
	public static Image imgMount_NM_1_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi23.png");

	// Token: 0x04000BB0 RID: 2992
	public static Image imgMount_XD_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi31.png");

	// Token: 0x04000BB1 RID: 2993
	public static Image imgEventMount = GameCanvas.loadImage("/mainImage/myTexture2drong.png");

	// Token: 0x04000BB2 RID: 2994
	public static Image imgEventMountWing = GameCanvas.loadImage("/mainImage/myTexture2dcanhrong.png");

	// Token: 0x04000BB3 RID: 2995
	public sbyte[] FrameMount = new sbyte[]
	{
		0,
		0,
		1,
		1,
		2,
		2,
		1,
		1
	};

	// Token: 0x04000BB4 RID: 2996
	public int frameMount;

	// Token: 0x04000BB5 RID: 2997
	public int frameNewMount;

	// Token: 0x04000BB6 RID: 2998
	public int transMount;

	// Token: 0x04000BB7 RID: 2999
	public int genderMount;

	// Token: 0x04000BB8 RID: 3000
	public int idcharMount;

	// Token: 0x04000BB9 RID: 3001
	public int xMount;

	// Token: 0x04000BBA RID: 3002
	public int yMount;

	// Token: 0x04000BBB RID: 3003
	public int dxMount;

	// Token: 0x04000BBC RID: 3004
	public int dyMount;

	// Token: 0x04000BBD RID: 3005
	public int xChar;

	// Token: 0x04000BBE RID: 3006
	public int xdis;

	// Token: 0x04000BBF RID: 3007
	public int speedMount;

	// Token: 0x04000BC0 RID: 3008
	public bool isStartMount;

	// Token: 0x04000BC1 RID: 3009
	public bool isMount;

	// Token: 0x04000BC2 RID: 3010
	public bool isEndMount;

	// Token: 0x04000BC3 RID: 3011
	public sbyte cFlag;

	// Token: 0x04000BC4 RID: 3012
	public int flagImage;

	// Token: 0x04000BC5 RID: 3013
	public short x_hint;

	// Token: 0x04000BC6 RID: 3014
	public short y_hint;

	// Token: 0x04000BC7 RID: 3015
	public short s_danhHieu1;

	// Token: 0x04000BC8 RID: 3016
	public static int[][][] CharInfo = new int[][][]
	{
		new int[][]
		{
			new int[]
			{
				0,
				-13,
				34
			},
			new int[]
			{
				1,
				-8,
				10
			},
			new int[]
			{
				1,
				-9,
				16
			},
			new int[]
			{
				1,
				-9,
				45
			}
		},
		new int[][]
		{
			new int[]
			{
				0,
				-13,
				35
			},
			new int[]
			{
				1,
				-8,
				10
			},
			new int[]
			{
				1,
				-9,
				17
			},
			new int[]
			{
				1,
				-9,
				46
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				33
			},
			new int[]
			{
				2,
				-10,
				11
			},
			new int[]
			{
				2,
				-8,
				16
			},
			new int[]
			{
				1,
				-12,
				49
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				32
			},
			new int[]
			{
				3,
				-12,
				10
			},
			new int[]
			{
				3,
				-11,
				15
			},
			new int[]
			{
				1,
				-13,
				47
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				34
			},
			new int[]
			{
				4,
				-8,
				11
			},
			new int[]
			{
				4,
				-7,
				17
			},
			new int[]
			{
				1,
				-12,
				47
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				34
			},
			new int[]
			{
				5,
				-12,
				11
			},
			new int[]
			{
				5,
				-9,
				17
			},
			new int[]
			{
				1,
				-13,
				49
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				33
			},
			new int[]
			{
				6,
				-10,
				10
			},
			new int[]
			{
				6,
				-8,
				16
			},
			new int[]
			{
				1,
				-12,
				47
			}
		},
		new int[][]
		{
			new int[]
			{
				0,
				-9,
				36
			},
			new int[]
			{
				7,
				-5,
				17
			},
			new int[]
			{
				7,
				-11,
				25
			},
			new int[]
			{
				1,
				-8,
				49
			}
		},
		new int[][]
		{
			new int[]
			{
				0,
				-7,
				35
			},
			new int[]
			{
				0,
				-18,
				22
			},
			new int[]
			{
				7,
				-10,
				25
			},
			new int[]
			{
				1,
				-7,
				48
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-11,
				35
			},
			new int[]
			{
				10,
				-3,
				25
			},
			new int[]
			{
				12,
				-10,
				26
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-11,
				37
			},
			new int[]
			{
				11,
				-3,
				25
			},
			new int[]
			{
				12,
				-11,
				27
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-14,
				34
			},
			new int[]
			{
				12,
				-8,
				21
			},
			new int[]
			{
				9,
				-7,
				31
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-12,
				35
			},
			new int[]
			{
				8,
				-5,
				14
			},
			new int[]
			{
				8,
				-15,
				29
			},
			new int[]
			{
				1,
				-9,
				49
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-9,
				34
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				10,
				-7,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-13,
				34
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				11,
				-10,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-8,
				32
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				2,
				-6,
				15
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-8,
				32
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				13,
				-12,
				16
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-10,
				31
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				7,
				-13,
				20
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-11,
				32
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				8,
				-15,
				26
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-9,
				33
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				14,
				-8,
				18
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-11,
				33
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				15,
				-6,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-16,
				31
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				9,
				-8,
				28
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-14,
				34
			},
			new int[]
			{
				1,
				-8,
				10
			},
			new int[]
			{
				8,
				-16,
				28
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-8,
				36
			},
			new int[]
			{
				7,
				-5,
				17
			},
			new int[]
			{
				0,
				-5,
				25
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-9,
				31
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				0,
				-6,
				20
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				2,
				-9,
				36
			},
			new int[]
			{
				13,
				-5,
				17
			},
			new int[]
			{
				16,
				-11,
				25
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-9,
				34
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				10,
				-7,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-13,
				34
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				11,
				-10,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-8,
				32
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				2,
				-6,
				15
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-8,
				32
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				13,
				-12,
				16
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-9,
				33
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				14,
				-8,
				18
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-11,
				33
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				15,
				-6,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-16,
				32
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				9,
				-8,
				29
			},
			new int[3]
		}
	};

	// Token: 0x04000BC9 RID: 3017
	public static int[] CHAR_WEAPONX = new int[]
	{
		-2,
		-6,
		22,
		21,
		19,
		22,
		10,
		-2,
		-2,
		5,
		19
	};

	// Token: 0x04000BCA RID: 3018
	public static int[] CHAR_WEAPONY = new int[]
	{
		9,
		22,
		25,
		17,
		26,
		37,
		36,
		49,
		50,
		52,
		36
	};

	// Token: 0x04000BCB RID: 3019
	private static global::Char myChar;

	// Token: 0x04000BCC RID: 3020
	private static global::Char myPet;

	// Token: 0x04000BCD RID: 3021
	public static int[] listAttack;

	// Token: 0x04000BCE RID: 3022
	public static int[][] listIonC;

	// Token: 0x04000BCF RID: 3023
	public int cvyJump;

	// Token: 0x04000BD0 RID: 3024
	private int indexUseSkill = -1;

	// Token: 0x04000BD1 RID: 3025
	public int cxSend;

	// Token: 0x04000BD2 RID: 3026
	public int cySend;

	// Token: 0x04000BD3 RID: 3027
	public int cdirSend = 1;

	// Token: 0x04000BD4 RID: 3028
	public int cxFocus;

	// Token: 0x04000BD5 RID: 3029
	public int cyFocus;

	// Token: 0x04000BD6 RID: 3030
	public int cactFirst = 5;

	// Token: 0x04000BD7 RID: 3031
	public MyVector vMovePoints = new MyVector();

	// Token: 0x04000BD8 RID: 3032
	public static string[][] inforClass = new string[][]
	{
		new string[]
		{
			"1",
			"1",
			"chiêu 1",
			"0"
		},
		new string[]
		{
			"2",
			"2",
			"chiêu 2",
			"5"
		}
	};

	// Token: 0x04000BD9 RID: 3033
	public static int[][] inforSkill = new int[][]
	{
		new int[]
		{
			1,
			0,
			1,
			1000,
			40,
			1,
			0,
			20,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			2,
			1,
			10,
			1000,
			100,
			1,
			0,
			40,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			2,
			2,
			11,
			800,
			100,
			1,
			0,
			45,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			2,
			3,
			12,
			600,
			100,
			1,
			0,
			50,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			2,
			4,
			13,
			500,
			100,
			1,
			0,
			55,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			1,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			2,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			3,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			4,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			5,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		}
	};

	// Token: 0x04000BDA RID: 3034
	public static bool flag;

	// Token: 0x04000BDB RID: 3035
	public static bool ischangingMap;

	// Token: 0x04000BDC RID: 3036
	public static bool isLockKey;

	// Token: 0x04000BDD RID: 3037
	public static bool isLoadingMap;

	// Token: 0x04000BDE RID: 3038
	public bool isLockMove;

	// Token: 0x04000BDF RID: 3039
	public bool isLockAttack;

	// Token: 0x04000BE0 RID: 3040
	public string strInfo;

	// Token: 0x04000BE1 RID: 3041
	public short powerPoint;

	// Token: 0x04000BE2 RID: 3042
	public short maxPowerPoint;

	// Token: 0x04000BE3 RID: 3043
	public short secondPower;

	// Token: 0x04000BE4 RID: 3044
	public long lastS;

	// Token: 0x04000BE5 RID: 3045
	public long currS;

	// Token: 0x04000BE6 RID: 3046
	public const int C_XAYDA_2 = 2;

	// Token: 0x04000BE7 RID: 3047
	public const int C_NAMEC_1 = 1;

	// Token: 0x04000BE8 RID: 3048
	public const int C_TRAIDAT_0 = 0;

	// Token: 0x04000BE9 RID: 3049
	public bool havePet = true;

	// Token: 0x04000BEA RID: 3050
	public MovePoint currentMovePoint;

	// Token: 0x04000BEB RID: 3051
	public int bom;

	// Token: 0x04000BEC RID: 3052
	public int delayFall;

	// Token: 0x04000BED RID: 3053
	private bool isSoundJump;

	// Token: 0x04000BEE RID: 3054
	public int lastFrame;

	// Token: 0x04000BEF RID: 3055
	private Effect eProtect;

	// Token: 0x04000BF0 RID: 3056
	private Effect eDanhHieu;

	// Token: 0x04000BF1 RID: 3057
	private int twHp;

	// Token: 0x04000BF2 RID: 3058
	public bool isInjureHp;

	// Token: 0x04000BF3 RID: 3059
	public bool changePos;

	// Token: 0x04000BF4 RID: 3060
	public bool isHide;

	// Token: 0x04000BF5 RID: 3061
	private int count;

	// Token: 0x04000BF6 RID: 3062
	private bool wy;

	// Token: 0x04000BF7 RID: 3063
	public int wt;

	// Token: 0x04000BF8 RID: 3064
	public int fy;

	// Token: 0x04000BF9 RID: 3065
	public int ty;

	// Token: 0x04000BFA RID: 3066
	private int t;

	// Token: 0x04000BFB RID: 3067
	private int fM;

	// Token: 0x04000BFC RID: 3068
	public int[] move = new int[]
	{
		1,
		1,
		1,
		1,
		2,
		2,
		2,
		2,
		3,
		3,
		3,
		3,
		2,
		2,
		2
	};

	// Token: 0x04000BFD RID: 3069
	private string strMount = "mount_";

	// Token: 0x04000BFE RID: 3070
	public int headICON = -1;

	// Token: 0x04000BFF RID: 3071
	public int head;

	// Token: 0x04000C00 RID: 3072
	public int leg;

	// Token: 0x04000C01 RID: 3073
	public int body;

	// Token: 0x04000C02 RID: 3074
	public int bag;

	// Token: 0x04000C03 RID: 3075
	public int wp;

	// Token: 0x04000C04 RID: 3076
	public int indexEff = -1;

	// Token: 0x04000C05 RID: 3077
	public int indexEffTask = -1;

	// Token: 0x04000C06 RID: 3078
	public EffectCharPaint eff;

	// Token: 0x04000C07 RID: 3079
	public EffectCharPaint effTask;

	// Token: 0x04000C08 RID: 3080
	public int indexSkill;

	// Token: 0x04000C09 RID: 3081
	public int i0;

	// Token: 0x04000C0A RID: 3082
	public int i1;

	// Token: 0x04000C0B RID: 3083
	public int i2;

	// Token: 0x04000C0C RID: 3084
	public int dx0;

	// Token: 0x04000C0D RID: 3085
	public int dx1;

	// Token: 0x04000C0E RID: 3086
	public int dx2;

	// Token: 0x04000C0F RID: 3087
	public int dy0;

	// Token: 0x04000C10 RID: 3088
	public int dy1;

	// Token: 0x04000C11 RID: 3089
	public int dy2;

	// Token: 0x04000C12 RID: 3090
	public EffectCharPaint eff0;

	// Token: 0x04000C13 RID: 3091
	public EffectCharPaint eff1;

	// Token: 0x04000C14 RID: 3092
	public EffectCharPaint eff2;

	// Token: 0x04000C15 RID: 3093
	public Arrow arr;

	// Token: 0x04000C16 RID: 3094
	public PlayerDart dart;

	// Token: 0x04000C17 RID: 3095
	public bool isCreateDark;

	// Token: 0x04000C18 RID: 3096
	public SkillPaint skillPaint;

	// Token: 0x04000C19 RID: 3097
	public SkillPaint skillPaintRandomPaint;

	// Token: 0x04000C1A RID: 3098
	public EffectPaint[] effPaints;

	// Token: 0x04000C1B RID: 3099
	public int sType;

	// Token: 0x04000C1C RID: 3100
	public sbyte isInjure;

	// Token: 0x04000C1D RID: 3101
	public bool isUseSkillAfterCharge;

	// Token: 0x04000C1E RID: 3102
	public bool isFlyAndCharge;

	// Token: 0x04000C1F RID: 3103
	public bool isStandAndCharge;

	// Token: 0x04000C20 RID: 3104
	private bool isFlying;

	// Token: 0x04000C21 RID: 3105
	public int posDisY;

	// Token: 0x04000C22 RID: 3106
	private int chargeCount;

	// Token: 0x04000C23 RID: 3107
	private bool hasSendAttack;

	// Token: 0x04000C24 RID: 3108
	public bool isMabuHold;

	// Token: 0x04000C25 RID: 3109
	private long timeBlue;

	// Token: 0x04000C26 RID: 3110
	private int tBlue;

	// Token: 0x04000C27 RID: 3111
	private bool IsAddDust1;

	// Token: 0x04000C28 RID: 3112
	private bool IsAddDust2;

	// Token: 0x04000C29 RID: 3113
	public int len = 24;

	// Token: 0x04000C2A RID: 3114
	public int w_hp_bar = 24;

	// Token: 0x04000C2B RID: 3115
	private int per = 100;

	// Token: 0x04000C2C RID: 3116
	private int per_tem = 100;

	// Token: 0x04000C2D RID: 3117
	private Image imgHPtem;

	// Token: 0x04000C2E RID: 3118
	public bool isPet;

	// Token: 0x04000C2F RID: 3119
	public bool isMiniPet;

	// Token: 0x04000C30 RID: 3120
	private int iiii;

	// Token: 0x04000C31 RID: 3121
	private int danhHieuFramme;

	// Token: 0x04000C32 RID: 3122
	public int xSd;

	// Token: 0x04000C33 RID: 3123
	public int ySd;

	// Token: 0x04000C34 RID: 3124
	private bool isOutMap;

	// Token: 0x04000C35 RID: 3125
	private int fBag;

	// Token: 0x04000C36 RID: 3126
	private Part ph;

	// Token: 0x04000C37 RID: 3127
	private Part pl;

	// Token: 0x04000C38 RID: 3128
	private Part pb;

	// Token: 0x04000C39 RID: 3129
	public int cH_new = 32;

	// Token: 0x04000C3A RID: 3130
	private int statusBeforeNothing;

	// Token: 0x04000C3B RID: 3131
	private int timeFocusToMob;

	// Token: 0x04000C3C RID: 3132
	public static bool isManualFocus = false;

	// Token: 0x04000C3D RID: 3133
	private global::Char charHold;

	// Token: 0x04000C3E RID: 3134
	private Mob mobHold;

	// Token: 0x04000C3F RID: 3135
	private int nInjure;

	// Token: 0x04000C40 RID: 3136
	public short wdx;

	// Token: 0x04000C41 RID: 3137
	public short wdy;

	// Token: 0x04000C42 RID: 3138
	public bool isDirtyPostion;

	// Token: 0x04000C43 RID: 3139
	public Skill lastNormalSkill;

	// Token: 0x04000C44 RID: 3140
	public bool currentFireByShortcut;

	// Token: 0x04000C45 RID: 3141
	public int cDamGoc;

	// Token: 0x04000C46 RID: 3142
	public int cHPGoc;

	// Token: 0x04000C47 RID: 3143
	public int cMPGoc;

	// Token: 0x04000C48 RID: 3144
	public int cDefGoc;

	// Token: 0x04000C49 RID: 3145
	public int cCriticalGoc;

	// Token: 0x04000C4A RID: 3146
	public sbyte hpFrom1000TiemNang;

	// Token: 0x04000C4B RID: 3147
	public sbyte mpFrom1000TiemNang;

	// Token: 0x04000C4C RID: 3148
	public sbyte damFrom1000TiemNang;

	// Token: 0x04000C4D RID: 3149
	public sbyte defFrom1000TiemNang = 1;

	// Token: 0x04000C4E RID: 3150
	public sbyte criticalFrom1000Tiemnang = 1;

	// Token: 0x04000C4F RID: 3151
	public short cMaxStamina;

	// Token: 0x04000C50 RID: 3152
	public short expForOneAdd;

	// Token: 0x04000C51 RID: 3153
	public sbyte isMonkey;

	// Token: 0x04000C52 RID: 3154
	public bool isCopy;

	// Token: 0x04000C53 RID: 3155
	public bool isWaitMonkey;

	// Token: 0x04000C54 RID: 3156
	private bool isFeetEff;

	// Token: 0x04000C55 RID: 3157
	public bool meDead;

	// Token: 0x04000C56 RID: 3158
	public int holdEffID;

	// Token: 0x04000C57 RID: 3159
	public bool holder;

	// Token: 0x04000C58 RID: 3160
	public bool protectEff;

	// Token: 0x04000C59 RID: 3161
	public bool danhHieuEff = true;

	// Token: 0x04000C5A RID: 3162
	private bool isSetPos;

	// Token: 0x04000C5B RID: 3163
	private int tpos;

	// Token: 0x04000C5C RID: 3164
	private short xPos;

	// Token: 0x04000C5D RID: 3165
	private short yPos;

	// Token: 0x04000C5E RID: 3166
	private sbyte typePos;

	// Token: 0x04000C5F RID: 3167
	private bool isMyFusion;

	// Token: 0x04000C60 RID: 3168
	public bool isFusion;

	// Token: 0x04000C61 RID: 3169
	public int tFusion;

	// Token: 0x04000C62 RID: 3170
	public bool huytSao;

	// Token: 0x04000C63 RID: 3171
	public bool blindEff;

	// Token: 0x04000C64 RID: 3172
	public bool telePortSkill;

	// Token: 0x04000C65 RID: 3173
	public bool sleepEff;

	// Token: 0x04000C66 RID: 3174
	public bool stone;

	// Token: 0x04000C67 RID: 3175
	public int perCentMp = 100;

	// Token: 0x04000C68 RID: 3176
	public long dHP;

	// Token: 0x04000C69 RID: 3177
	public int headTemp = -1;

	// Token: 0x04000C6A RID: 3178
	public int bodyTemp = -1;

	// Token: 0x04000C6B RID: 3179
	public int legTemp = -1;

	// Token: 0x04000C6C RID: 3180
	public int bagTemp = -1;

	// Token: 0x04000C6D RID: 3181
	public int wpTemp = -1;

	// Token: 0x04000C6E RID: 3182
	public MyVector vEffChar = new MyVector("vEff");

	// Token: 0x04000C6F RID: 3183
	public static FrameImage fraRedEye;

	// Token: 0x04000C70 RID: 3184
	private int fChopmat;

	// Token: 0x04000C71 RID: 3185
	private bool isAddChopMat;

	// Token: 0x04000C72 RID: 3186
	private long timeAddChopmat;

	// Token: 0x04000C73 RID: 3187
	private int[] frChopNhanh = new int[]
	{
		-1,
		-1,
		-1,
		-1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		-1,
		-1,
		-1,
		-1
	};

	// Token: 0x04000C74 RID: 3188
	private int[] frChopCham = new int[]
	{
		-1,
		-1,
		-1,
		-1,
		0,
		0,
		1,
		1,
		1,
		0,
		0,
		1,
		1,
		1,
		0,
		0,
		1,
		1,
		1,
		-1,
		-1,
		-1,
		-1
	};

	// Token: 0x04000C75 RID: 3189
	private int[] frEye = new int[]
	{
		-1,
		-1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		-1,
		-1
	};

	// Token: 0x04000C76 RID: 3190
	public static int[][] Arr_Head_2Fr = new int[][]
	{
		new int[]
		{
			542,
			543
		}
	};

	// Token: 0x04000C77 RID: 3191
	private int fHead;

	// Token: 0x04000C78 RID: 3192
	private string strEffAura = "aura_";

	// Token: 0x04000C79 RID: 3193
	public short idAuraEff = -1;

	// Token: 0x04000C7A RID: 3194
	public static bool isPaintAura = true;

	// Token: 0x04000C7B RID: 3195
	public static bool isPaintAura2 = true;

	// Token: 0x04000C7C RID: 3196
	private FrameImage fraEff;

	// Token: 0x04000C7D RID: 3197
	private FrameImage fraEffSub;

	// Token: 0x04000C7E RID: 3198
	private string strEff_Set_Item = "set_eff_";

	// Token: 0x04000C7F RID: 3199
	public short idEff_Set_Item = -1;

	// Token: 0x04000C80 RID: 3200
	private FrameImage fraHat_behind;

	// Token: 0x04000C81 RID: 3201
	private FrameImage fraHat_font;

	// Token: 0x04000C82 RID: 3202
	private FrameImage fraHat_behind_2;

	// Token: 0x04000C83 RID: 3203
	private FrameImage fraHat_font_2;

	// Token: 0x04000C84 RID: 3204
	private string strHat_behind = "hat_sau_";

	// Token: 0x04000C85 RID: 3205
	private string strHat_font = "hat_truoc_";

	// Token: 0x04000C86 RID: 3206
	private string strNgang = "ngang_";

	// Token: 0x04000C87 RID: 3207
	public short idHat = -1;

	// Token: 0x04000C88 RID: 3208
	public static int[][] hatInfo;

	// Token: 0x04000C89 RID: 3209
	public static short[] Arr_Head_FlyMove;

	// Token: 0x04000C8A RID: 3210
	public const byte TYPE_SKILL_KAMEX10 = 1;

	// Token: 0x04000C8B RID: 3211
	public const byte TYPE_SKILL_FINAL = 2;

	// Token: 0x04000C8C RID: 3212
	public const byte TYPE_SKILL_MAFUBA = 3;

	// Token: 0x04000C8D RID: 3213
	public const byte TYPE_SKILL_GENKI = 4;

	// Token: 0x04000C8E RID: 3214
	public bool isPaintNewSkill;

	// Token: 0x04000C8F RID: 3215
	private bool isFly;

	// Token: 0x04000C90 RID: 3216
	private long timeReset_newSkill;

	// Token: 0x04000C91 RID: 3217
	private sbyte typeFrame;

	// Token: 0x04000C92 RID: 3218
	private short idskillPaint;

	// Token: 0x04000C93 RID: 3219
	private byte[] fr_start;

	// Token: 0x04000C94 RID: 3220
	private byte[] fr_atk;

	// Token: 0x04000C95 RID: 3221
	private byte[] fr_end;

	// Token: 0x04000C96 RID: 3222
	private int count_NEW;

	// Token: 0x04000C97 RID: 3223
	private int stt;

	// Token: 0x04000C98 RID: 3224
	private short rangeDame;

	// Token: 0x04000C99 RID: 3225
	private sbyte typePaint;

	// Token: 0x04000C9A RID: 3226
	private sbyte typeItem;

	// Token: 0x04000C9B RID: 3227
	private Point targetDame;

	// Token: 0x04000C9C RID: 3228
	private long timeDame;

	// Token: 0x04000C9D RID: 3229
	public bool isMafuba;

	// Token: 0x04000C9E RID: 3230
	private short countMafuba;

	// Token: 0x04000C9F RID: 3231
	public int xMFB;

	// Token: 0x04000CA0 RID: 3232
	public int yMFB;

	// Token: 0x04000CA1 RID: 3233
	public int timeGongSkill;

	// Token: 0x04000CA2 RID: 3234
	private FrameImage fraDanhHieu;

	// Token: 0x04000CA3 RID: 3235
	private MainImage mainImg;

	// Token: 0x04000CA4 RID: 3236
	public bool isNRD;

	// Token: 0x04000CA5 RID: 3237
	public int timeNRD;

	// Token: 0x04000CA6 RID: 3238
	public long lastTimeNRD;
}
