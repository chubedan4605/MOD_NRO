using System;
using Assets.src.g;
using Mod.DungPham.KoiOctiiu957;

// Token: 0x020000B8 RID: 184
public class Mob : IMapObject
{
	// Token: 0x0600083C RID: 2108 RVA: 0x00076368 File Offset: 0x00074568
	public Mob()
	{
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x000764F4 File Offset: 0x000746F4
	public Mob(int mobId, bool isDisable, bool isDontMove, bool isFire, bool isIce, bool isWind, int templateId, int sys, long hp, sbyte level, long maxp, short pointx, short pointy, sbyte status, sbyte levelBoss)
	{
		this.isDisable = isDisable;
		this.isDontMove = isDontMove;
		this.isFire = isFire;
		this.isIce = isIce;
		this.isWind = isWind;
		this.sys = sys;
		this.mobId = mobId;
		this.templateId = templateId;
		this.hp = hp;
		this.level = level;
		this.pointx = pointx;
		this.x = (int)pointx;
		this.xFirst = (int)pointx;
		this.pointy = pointy;
		this.y = (int)pointy;
		this.yFirst = (int)pointy;
		this.status = (int)status;
		if (templateId != 70)
		{
			this.checkData();
			this.getData();
		}
		if (!Mob.isExistNewMob(templateId + string.Empty))
		{
			Mob.newMob.addElement(templateId + string.Empty);
		}
		this.maxHp = maxp;
		this.levelBoss = levelBoss;
		this.updateHp_bar();
		this.isDie = false;
		this.xSd = (int)pointx;
		this.ySd = (int)pointy;
		if (this.isNewModStand())
		{
			this.stand = new int[]
			{
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1,
				1,
				2,
				2,
				2,
				2,
				2,
				2,
				2
			};
			this.move = new int[]
			{
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1,
				1,
				2,
				2,
				2,
				2,
				2,
				2,
				2
			};
			this.moveFast = new int[]
			{
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1,
				1,
				2,
				2,
				2,
				2,
				2,
				2,
				2
			};
			this.attack1 = new int[]
			{
				3,
				3,
				3,
				3,
				4,
				4,
				4,
				4,
				5,
				5,
				5,
				5
			};
			this.attack2 = new int[]
			{
				3,
				3,
				3,
				3,
				4,
				4,
				4,
				4,
				5,
				5,
				5,
				5
			};
			return;
		}
		if (this.isNewMod())
		{
			this.stand = new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			};
			this.move = new int[]
			{
				1,
				1,
				1,
				1,
				2,
				2,
				2,
				2,
				1,
				1,
				1,
				1,
				3,
				3,
				3,
				3
			};
			this.moveFast = new int[]
			{
				1,
				1,
				2,
				2,
				1,
				1,
				3,
				3
			};
			this.attack1 = new int[]
			{
				4,
				4,
				4,
				5,
				5,
				5,
				6,
				6,
				6,
				6,
				6
			};
			this.attack2 = new int[]
			{
				7,
				7,
				7,
				8,
				8,
				8,
				9,
				9,
				9,
				9,
				9
			};
			return;
		}
		if (this.isSpecial())
		{
			this.stand = new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			};
			this.move = new int[]
			{
				2,
				2,
				3,
				3,
				2,
				2,
				4,
				4,
				2,
				2,
				3,
				3,
				2,
				2,
				4,
				4
			};
			this.moveFast = new int[]
			{
				2,
				2,
				3,
				3,
				2,
				2,
				4,
				4
			};
			this.attack1 = new int[]
			{
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12
			};
			this.attack2 = new int[]
			{
				5,
				12,
				13,
				14
			};
			return;
		}
		this.stand = new int[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		};
		this.move = new int[]
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
		this.moveFast = new int[]
		{
			1,
			1,
			2,
			2,
			3,
			3,
			2
		};
		this.attack1 = new int[]
		{
			4,
			5,
			6
		};
		this.attack2 = new int[]
		{
			7,
			8,
			9
		};
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x00007D7A File Offset: 0x00005F7A
	public bool isBigBoss()
	{
		return this is BachTuoc || this is BigBoss2 || this is BigBoss || this is NewBoss;
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x0007696C File Offset: 0x00074B6C
	public void getData()
	{
		if (Mob.arrMobTemplate[this.templateId].data == null)
		{
			Mob.arrMobTemplate[this.templateId].data = new EffectData();
			string text = "/Mob/" + this.templateId;
			if (MyStream.readFile(text) != null)
			{
				Mob.arrMobTemplate[this.templateId].data.readData(text + "/data");
				Mob.arrMobTemplate[this.templateId].data.img = GameCanvas.loadImage(text + "/img.png");
			}
			else
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			if (Mob.lastMob.size() > 15)
			{
				Mob.arrMobTemplate[int.Parse((string)Mob.lastMob.elementAt(0))].data = null;
				Mob.lastMob.removeElementAt(0);
			}
			Mob.lastMob.addElement(this.templateId + string.Empty);
			return;
		}
		this.w = Mob.arrMobTemplate[this.templateId].data.width;
		this.h = Mob.arrMobTemplate[this.templateId].data.height;
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x00006CB9 File Offset: 0x00004EB9
	public virtual void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x00006CC9 File Offset: 0x00004EC9
	public virtual void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x00076AB0 File Offset: 0x00074CB0
	public static bool isExistNewMob(string id)
	{
		for (int i = 0; i < Mob.newMob.size(); i++)
		{
			if (((string)Mob.newMob.elementAt(i)).Equals(id))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x00076AF0 File Offset: 0x00074CF0
	public void checkData()
	{
		int num = 0;
		for (int i = 0; i < Mob.arrMobTemplate.Length; i++)
		{
			if (Mob.arrMobTemplate[i].data != null)
			{
				num++;
			}
		}
		if (num >= 10)
		{
			for (int j = 0; j < Mob.arrMobTemplate.Length; j++)
			{
				if (Mob.arrMobTemplate[j].data != null && num > 5)
				{
					Mob.arrMobTemplate[j].data = null;
				}
			}
		}
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x00007D9F File Offset: 0x00005F9F
	public void checkFrameTick(int[] array)
	{
		if (this.tick > array.Length - 1)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
		this.tick++;
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x00076B5C File Offset: 0x00074D5C
	private void updateShadown()
	{
		int size = (int)TileMap.size;
		this.xSd = this.x;
		this.wCount = 0;
		if (this.ySd <= 0)
		{
			return;
		}
		if (TileMap.tileTypeAt(this.xSd, this.ySd, 2))
		{
			return;
		}
		if (TileMap.tileTypeAt(this.xSd / size, this.ySd / size) == 0)
		{
			this.isOutMap = true;
		}
		else if (TileMap.tileTypeAt(this.xSd / size, this.ySd / size) != 0 && !TileMap.tileTypeAt(this.xSd, this.ySd, 2))
		{
			this.xSd = this.x;
			this.ySd = this.y;
			this.isOutMap = false;
		}
		while (this.isOutMap && this.wCount < 10)
		{
			this.wCount++;
			this.ySd += 24;
			if (TileMap.tileTypeAt(this.xSd, this.ySd, 2))
			{
				if (this.ySd % 24 != 0)
				{
					this.ySd -= this.ySd % 24;
				}
				return;
			}
		}
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x00076C78 File Offset: 0x00074E78
	private void paintShadow(mGraphics g)
	{
		int size = (int)TileMap.size;
		if (TileMap.tileTypeAt(this.xSd + size / 2, this.ySd + 1, 4))
		{
			g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, size, 100);
		}
		else if (TileMap.tileTypeAt((this.xSd - size / 2) / size, (this.ySd + 1) / size) == 0)
		{
			g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, 100, 100);
		}
		else if (TileMap.tileTypeAt((this.xSd + size / 2) / size, (this.ySd + 1) / size) == 0)
		{
			g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, size, 100);
		}
		else if (TileMap.tileTypeAt(this.xSd - size / 2, this.ySd + 1, 8))
		{
			g.setClip(this.xSd / 24 * size, (this.ySd - 30) / size * size, size, 100);
		}
		g.drawImage(TileMap.bong, this.xSd, this.ySd, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x00076DC4 File Offset: 0x00074FC4
	public void updateSuperEff()
	{
		if (this.typeSuperEff == 0 && GameCanvas.gameTick % 25 == 0)
		{
			ServerEffect.addServerEffect(114, this, 1);
		}
		if (this.typeSuperEff == 1 && GameCanvas.gameTick % 4 == 0)
		{
			ServerEffect.addServerEffect(132, this, 1);
		}
		if (this.typeSuperEff == 2 && GameCanvas.gameTick % 7 == 0)
		{
			ServerEffect.addServerEffect(131, this, 1);
		}
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x00076E28 File Offset: 0x00075028
	public virtual void update()
	{
		if (this.isMafuba)
		{
			return;
		}
		this.GetFrame();
		if (this.blindEff && GameCanvas.gameTick % 5 == 0)
		{
			ServerEffect.addServerEffect(113, this.x, this.y, 1);
		}
		if (this.sleepEff && GameCanvas.gameTick % 10 == 0)
		{
			EffecMn.addEff(new Effect(41, this.x, this.y, 3, 1, 1));
		}
		if (!GameCanvas.lowGraphic && this.status != 1 && this.status != 0 && !GameCanvas.lowGraphic && GameCanvas.gameTick % (15 + this.mobId * 2) == 0)
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (@char != null && @char.isFlyAndCharge && @char.cf == 32)
				{
					global::Char char2 = new global::Char();
					char2.cx = @char.cx;
					char2.cy = @char.cy - @char.ch;
					if (@char.cgender == 0)
					{
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), -100L, -100L, char2, 25);
					}
				}
			}
			if (global::Char.myCharz().isFlyAndCharge && global::Char.myCharz().cf == 32)
			{
				global::Char char3 = new global::Char();
				char3.cx = global::Char.myCharz().cx;
				char3.cy = global::Char.myCharz().cy - global::Char.myCharz().ch;
				if (global::Char.myCharz().cgender == 0)
				{
					MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), -100L, -100L, char3, 25);
				}
			}
		}
		if (this.holdEffID != 0 && GameCanvas.gameTick % 5 == 0)
		{
			EffecMn.addEff(new Effect(this.holdEffID, this.x, this.y + 24, 3, 5, 1));
		}
		if (this.isFreez)
		{
			if (GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(113, this.x, this.y, 1);
			}
			long num = mSystem.currentTimeMillis();
			if (num - this.last >= 1000L)
			{
				this.seconds--;
				this.last = num;
				if (this.seconds < 0)
				{
					this.isFreez = false;
					this.seconds = 0;
				}
			}
			if (this.isTypeNewMod())
			{
				this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
			}
			else if (this.isNewModStand())
			{
				this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
			}
			else if (this.isNewMod())
			{
				if (GameCanvas.gameTick % 20 > 5)
				{
					this.frame = 11;
				}
				else
				{
					this.frame = 10;
				}
			}
			else if (this.isSpecial())
			{
				if (GameCanvas.gameTick % 20 > 5)
				{
					this.frame = 1;
				}
				else
				{
					this.frame = 15;
				}
			}
			else if (GameCanvas.gameTick % 20 > 5)
			{
				this.frame = 11;
			}
			else
			{
				this.frame = 10;
			}
		}
		if (!this.isUpdate())
		{
			return;
		}
		if (this.isShadown)
		{
			this.updateShadown();
		}
		if (this.vMobMove == null && Mob.arrMobTemplate[this.templateId].rangeMove != 0)
		{
			return;
		}
		if (this.status != 3 && this.isBusyAttackSomeOne)
		{
			if (this.cFocus != null)
			{
				this.cFocus.doInjure(this.dame, this.dameMp, false, true);
			}
			else if (this.mobToAttack != null)
			{
				this.mobToAttack.setInjure();
			}
			this.isBusyAttackSomeOne = false;
		}
		if (this.levelBoss > 0)
		{
			this.updateSuperEff();
		}
		switch (this.status)
		{
		case 1:
			this.isDisable = false;
			this.isDontMove = false;
			this.isFire = false;
			this.isIce = false;
			this.isWind = false;
			this.y += this.p1;
			if (GameCanvas.gameTick % 2 == 0)
			{
				if (this.p2 > 1)
				{
					this.p2--;
				}
				else if (this.p2 < -1)
				{
					this.p2++;
				}
			}
			this.x += this.p2;
			if (this.isTypeNewMod())
			{
				this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
			}
			else if (this.isNewModStand())
			{
				this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
			}
			else if (this.isNewMod())
			{
				this.frame = 11;
			}
			else if (this.isSpecial())
			{
				this.frame = 15;
			}
			else
			{
				this.frame = 11;
			}
			if (this.isDie)
			{
				this.isDie = false;
				if (this.isMobMe)
				{
					for (int j = 0; j < GameScr.vMob.size(); j++)
					{
						if (((Mob)GameScr.vMob.elementAt(j)).mobId == this.mobId)
						{
							GameScr.vMob.removeElementAt(j);
						}
					}
				}
				this.p1 = 0;
				this.p2 = 0;
				this.x = (this.y = 0);
				this.hp = this.getTemplate().hp;
				this.status = 0;
				this.timeStatus = 0;
				return;
			}
			if ((TileMap.tileTypeAtPixel(this.x, this.y) & 2) == 2)
			{
				this.p1 = ((this.p1 <= 4) ? (-this.p1) : -4);
				if (this.p3 == 0)
				{
					this.p3 = 16;
				}
			}
			else
			{
				this.p1++;
			}
			if (this.p3 > 0)
			{
				this.p3--;
				if (this.p3 == 0)
				{
					this.isDie = true;
					return;
				}
			}
			break;
		case 2:
			if (this.holdEffID != 0)
			{
				return;
			}
			if (this.isFreez)
			{
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
			this.timeStatus = 0;
			this.updateMobStandWait();
			return;
		case 3:
			if (this.holdEffID != 0)
			{
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
			if (this.isFreez)
			{
				return;
			}
			this.updateMobAttack();
			return;
		case 4:
			if (this.holdEffID != 0)
			{
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
			if (this.isFreez)
			{
				return;
			}
			this.timeStatus = 0;
			this.p1++;
			if (this.p1 > 40 + this.mobId % 5)
			{
				this.y -= 2;
				this.status = 5;
				this.p1 = 0;
				return;
			}
			break;
		case 5:
			if (this.holdEffID != 0)
			{
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
			if (this.isFreez)
			{
				if (Mob.arrMobTemplate[this.templateId].type == 4)
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
			this.timeStatus = 0;
			this.updateMobWalk();
			return;
		case 6:
			this.timeStatus = 0;
			this.p1++;
			this.y += this.p1;
			if (this.y >= this.yFirst)
			{
				this.y = this.yFirst;
				this.p1 = 0;
				this.status = 5;
				return;
			}
			break;
		case 7:
			this.updateInjure();
			break;
		default:
			return;
		}
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x000775DC File Offset: 0x000757DC
	public void setInjure()
	{
		if (this.hp > 0L && this.status != 3 && this.status != 7)
		{
			this.timeStatus = 4;
			this.status = 7;
			if (this.getTemplate().type != 0 && Res.abs(this.x - this.xFirst) < 30)
			{
				this.x -= 10 * this.dir;
			}
		}
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x0007764C File Offset: 0x0007584C
	public static BigBoss getBigBoss()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BigBoss)
			{
				return (BigBoss)mob;
			}
		}
		return null;
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x00077690 File Offset: 0x00075890
	public static BigBoss2 getBigBoss2()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BigBoss2)
			{
				return (BigBoss2)mob;
			}
		}
		return null;
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x000776D4 File Offset: 0x000758D4
	public static BachTuoc getBachTuoc()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BachTuoc)
			{
				return (BachTuoc)mob;
			}
		}
		return null;
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x00077718 File Offset: 0x00075918
	public static NewBoss getNewBoss(sbyte idBoss)
	{
		Mob mob = (Mob)GameScr.vMob.elementAt((int)idBoss);
		if (mob is NewBoss)
		{
			return (NewBoss)mob;
		}
		return null;
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x00077748 File Offset: 0x00075948
	public static void removeBigBoss()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BigBoss)
			{
				GameScr.vMob.removeElement(mob);
				return;
			}
		}
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x00077790 File Offset: 0x00075990
	public void setAttack(global::Char cFocus)
	{
		this.isBusyAttackSomeOne = true;
		this.mobToAttack = null;
		this.cFocus = cFocus;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
		this.dir = ((cFocus.cx <= this.x) ? -1 : 1);
		int cx = cFocus.cx;
		int cy = cFocus.cy;
		if (Res.abs(cx - this.x) < this.w * 2 && Res.abs(cy - this.y) < this.h * 2)
		{
			this.p3 = 0;
			return;
		}
		this.p3 = 1;
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x00007DD1 File Offset: 0x00005FD1
	private bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x00007DFD File Offset: 0x00005FFD
	private bool isNewModStand()
	{
		return this.templateId == 76;
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x00007E09 File Offset: 0x00006009
	private bool isNewMod()
	{
		return this.templateId >= 73 && !this.isNewModStand();
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x00077830 File Offset: 0x00075A30
	private void updateInjure()
	{
		if (!this.isBusyAttackSomeOne && GameCanvas.gameTick % 4 == 0)
		{
			if (this.isTypeNewMod())
			{
				this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
			}
			else if (this.isNewModStand())
			{
				this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
			}
			else if (this.isNewMod())
			{
				if (this.frame != 10)
				{
					this.frame = 10;
				}
				else
				{
					this.frame = 11;
				}
			}
			else if (this.isSpecial())
			{
				if (this.frame != 1)
				{
					this.frame = 1;
				}
				else
				{
					this.frame = 15;
				}
			}
			else if (this.frame != 10)
			{
				this.frame = 10;
			}
			else
			{
				this.frame = 11;
			}
		}
		this.timeStatus--;
		if (this.timeStatus <= 0 && (this.isTypeNewMod() || this.isNewModStand() || (this.isNewMod() && this.frame == 11) || (this.isSpecial() && this.frame == 15) || (this.templateId < 58 && this.frame == 11)))
		{
			if ((this.injureBy != null && this.injureThenDie) || this.hp == 0L)
			{
				this.status = 1;
				this.p2 = this.injureBy.cdir << 1;
				this.p1 = -3;
				this.p3 = 0;
			}
			else
			{
				this.status = 5;
				if (this.injureBy != null)
				{
					this.dir = -this.injureBy.cdir;
					if (Res.abs(this.x - this.injureBy.cx) < 24)
					{
						this.status = 2;
					}
				}
				this.p1 = (this.p2 = (this.p3 = 0));
				this.timeStatus = 0;
			}
			this.injureBy = null;
			return;
		}
		if (Mob.arrMobTemplate[this.templateId].type != 0 && this.injureBy != null)
		{
			int num = -this.injureBy.cdir << 1;
			if (this.x > this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove && this.x < this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
			{
				this.x -= num;
			}
		}
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x00077A98 File Offset: 0x00075C98
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.stand);
		sbyte type = Mob.arrMobTemplate[this.templateId].type;
		if (type > 3)
		{
			if (type - 4 <= 1)
			{
				this.p1++;
				if (this.p1 > this.mobId % 3 && (this.cFocus == null || Res.abs(this.cFocus.cx - this.x) > 80) && (this.mobToAttack == null || Res.abs(this.mobToAttack.x - this.x) > 80))
				{
					this.status = 5;
				}
			}
		}
		else
		{
			this.p1++;
			if (this.p1 > 10 + this.mobId % 10 && (this.cFocus == null || Res.abs(this.cFocus.cx - this.x) > 80) && (this.mobToAttack == null || Res.abs(this.mobToAttack.x - this.x) > 80))
			{
				this.status = 5;
			}
		}
		if (this.cFocus != null && GameCanvas.gameTick % (10 + this.p1 % 20) == 0)
		{
			if (this.cFocus.cx > this.x)
			{
				this.dir = 1;
			}
			else
			{
				this.dir = -1;
			}
		}
		else if (this.mobToAttack != null && GameCanvas.gameTick % (10 + this.p1 % 20) == 0)
		{
			if (this.mobToAttack.x > this.x)
			{
				this.dir = 1;
			}
			else
			{
				this.dir = -1;
			}
		}
		if (this.forceWait > 0)
		{
			this.forceWait--;
			this.status = 2;
		}
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x00077C54 File Offset: 0x00075E54
	public void updateMobAttack()
	{
		int[] array = (this.p3 != 0) ? this.attack2 : this.attack1;
		if (this.tick < array.Length)
		{
			this.checkFrameTick(array);
			if (this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w && this.p3 == 0 && GameCanvas.gameTick % 2 == 0)
			{
				SoundMn.gI().charPunch(false, 0.05f);
			}
		}
		if (this.p1 == 0)
		{
			int num = (this.cFocus == null) ? this.mobToAttack.x : this.cFocus.cx;
			int num2 = (this.cFocus == null) ? this.mobToAttack.y : this.cFocus.cy;
			if (!this.isNewMod())
			{
				if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
				{
					this.p1 = 1;
				}
				if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
				{
					this.p1 = 1;
				}
			}
			if ((Mob.arrMobTemplate[this.templateId].type == 4 || Mob.arrMobTemplate[this.templateId].type == 5) && !this.isDontMove && !AutoTrain.isAutoTrain)
			{
				this.y += (num2 - this.y) / 20;
			}
			this.p2++;
			if (this.p2 > array.Length - 1 || this.p1 == 1)
			{
				this.p1 = 1;
				if (this.p3 == 0)
				{
					if (this.cFocus != null)
					{
						this.cFocus.doInjure(this.dame, this.dameMp, false, true);
					}
					else
					{
						this.mobToAttack.setInjure();
					}
					this.isBusyAttackSomeOne = false;
				}
				else
				{
					if (this.cFocus != null)
					{
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), this.dame, this.dameMp, this.cFocus, (int)this.getTemplate().dartType);
					}
					else
					{
						global::Char @char = new global::Char();
						@char.cx = this.mobToAttack.x;
						@char.cy = this.mobToAttack.y;
						@char.charID = -100;
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), this.dame, this.dameMp, @char, (int)this.getTemplate().dartType);
					}
					this.isBusyAttackSomeOne = false;
				}
			}
			this.dir = ((this.x >= num) ? -1 : 1);
		}
		else if (this.p1 == 1)
		{
			if (Mob.arrMobTemplate[this.templateId].type != 0 && !this.isDontMove && !this.isIce)
			{
				bool flag = this.isWind;
			}
			if (this.tick == array.Length)
			{
				this.status = 2;
				this.p1 = 0;
				this.p2 = 0;
				this.tick = 0;
			}
		}
		if (this.tick == 5 && this.cFocus != null && this.cFocus.charID == global::Char.myCharz().charID)
		{
			if (this.templateId == 88 && this.p3 != 0)
			{
				GameScr.shock_scr = 2;
			}
			if (this.templateId == 89)
			{
				GameScr.shock_scr = 2;
			}
		}
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x00077FA8 File Offset: 0x000761A8
	public void updateMobWalk()
	{
		int num = 0;
		try
		{
			if (this.injureThenDie)
			{
				this.status = 1;
				this.p2 = this.injureBy.cdir << 3;
				this.p1 = -5;
				this.p3 = 0;
			}
			num = 1;
			if (!this.isIce)
			{
				if (this.isDontMove || this.isWind || AutoTrain.isAutoTrain)
				{
					this.checkFrameTick(this.stand);
				}
				else
				{
					switch (Mob.arrMobTemplate[this.templateId].type)
					{
					case 0:
						if (this.isNewModStand())
						{
							this.frame = this.stand[GameCanvas.gameTick % this.stand.Length];
						}
						else
						{
							this.frame = 0;
						}
						num = 2;
						break;
					case 1:
					case 2:
					case 3:
					{
						num = 3;
						sbyte b = Mob.arrMobTemplate[this.templateId].speed;
						if (b == 1)
						{
							if (GameCanvas.gameTick % 2 == 1)
							{
								break;
							}
						}
						else if (b > 2)
						{
							b += (sbyte)(this.mobId % 2);
						}
						else if (GameCanvas.gameTick % 2 == 1)
						{
							b -= 1;
						}
						this.x += (int)b * this.dir;
						if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = -1;
						}
						else if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = 1;
						}
						if (Res.abs(this.x - global::Char.myCharz().cx) < 40 && Res.abs(this.x - this.xFirst) < (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = ((this.x <= global::Char.myCharz().cx) ? 1 : -1);
							if (Res.abs(this.x - global::Char.myCharz().cx) < 20)
							{
								this.x -= this.dir * 10;
							}
							this.status = 2;
							this.forceWait = 20;
						}
						this.checkFrameTick((this.w <= 30) ? this.moveFast : this.move);
						break;
					}
					case 4:
					{
						num = 4;
						sbyte b2 = Mob.arrMobTemplate[this.templateId].speed;
						b2 += (sbyte)(this.mobId % 2);
						this.x += (int)b2 * this.dir;
						if (GameCanvas.gameTick % 10 > 2)
						{
							this.y += (int)b2 * this.dirV;
						}
						b2 += (sbyte)((GameCanvas.gameTick + this.mobId) % 2);
						if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = -1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						else if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = 1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						if (this.y > this.yFirst + 24)
						{
							this.dirV = -1;
						}
						else if (this.y < this.yFirst - (20 + GameCanvas.gameTick % 10))
						{
							this.dirV = 1;
						}
						this.checkFrameTick(this.move);
						break;
					}
					case 5:
					{
						num = 5;
						sbyte b3 = Mob.arrMobTemplate[this.templateId].speed;
						b3 += (sbyte)(this.mobId % 2);
						this.x += (int)b3 * this.dir;
						b3 += (sbyte)((GameCanvas.gameTick + this.mobId) % 2);
						if (GameCanvas.gameTick % 10 > 2)
						{
							this.y += (int)b3 * this.dirV;
						}
						if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = -1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						else if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = 1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						if (this.y > this.yFirst + 24)
						{
							this.dirV = -1;
						}
						else if (this.y < this.yFirst - (20 + GameCanvas.gameTick % 10))
						{
							this.dirV = 1;
						}
						if (TileMap.tileTypeAt(this.x, this.y, 2))
						{
							if (GameCanvas.gameTick % 10 > 5)
							{
								this.y = TileMap.tileYofPixel(this.y);
								this.status = 4;
								this.p1 = 0;
								this.dirV = -1;
							}
							else
							{
								this.dirV = -1;
							}
						}
						break;
					}
					}
				}
			}
		}
		catch (Exception)
		{
			Cout.println("lineee: " + num);
		}
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x00007E20 File Offset: 0x00006020
	public MobTemplate getTemplate()
	{
		return Mob.arrMobTemplate[this.templateId];
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x000784EC File Offset: 0x000766EC
	public bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.img != null && this.status != 0;
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x00007E2E File Offset: 0x0000602E
	public bool isUpdate()
	{
		return Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && this.status != 0;
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x00007E5C File Offset: 0x0000605C
	public bool checkIsBoss()
	{
		return this.isBoss || this.levelBoss > 0;
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x00078580 File Offset: 0x00076780
	public void updateHp_bar()
	{
		this.len = (int)(this.hp * 100L / this.maxHp * (long)this.w_hp_bar) / 100;
		this.per = (int)(this.hp * 100L / this.maxHp);
		if (this.per == 100)
		{
			this.per_tem = this.per;
		}
		if (this.per >= 100)
		{
			this.per_tem = this.per;
		}
		this.offset = 0;
		if (this.per < 30)
		{
			this.color = 15473700;
			this.imgHPtem = GameScr.imgHP_tm_do;
			return;
		}
		if (this.per < 60)
		{
			this.color = 16744448;
			this.imgHPtem = GameScr.imgHP_tm_vang;
			return;
		}
		this.color = 11992374;
		this.imgHPtem = GameScr.imgHP_tm_xanh;
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x00078654 File Offset: 0x00076854
	public virtual void paint(mGraphics g)
	{
		if (this.isHide)
		{
			return;
		}
		if (this.isMafuba)
		{
			if (!this.changBody)
			{
				Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
				return;
			}
			SmallImage.drawSmallImage(g, (int)this.smallBody, this.xMFB, this.yMFB, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			return;
		}
		else
		{
			if (this.isShadown && this.status != 0)
			{
				this.paintShadow(g);
			}
			if (!this.isPaint())
			{
				return;
			}
			if (this.status == 1 && this.p3 > 0 && GameCanvas.gameTick % 3 == 0)
			{
				return;
			}
			g.translate(0, GameCanvas.transY);
			if (!this.changBody)
			{
				Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.smallBody, this.x, this.y + this.fy - 9, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			g.translate(0, -GameCanvas.transY);
			if (global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.Equals(this) && this.status != 1 && this.hp > 0L && this.imgHPtem != null)
			{
				int imageWidth = mGraphics.getImageWidth(this.imgHPtem);
				int imageHeight = mGraphics.getImageHeight(this.imgHPtem);
				int num = imageWidth * this.per / 100;
				int num2 = num;
				if (this.per_tem >= this.per)
				{
					int num3 = imageWidth;
					int num4 = this.per_tem;
					int num6;
					if (GameCanvas.gameTick % 6 > 3)
					{
						int num5 = this.offset;
						this.offset = num5 + 1;
						num6 = num5;
					}
					else
					{
						num6 = this.offset;
					}
					num2 = num3 * (this.per_tem = num4 - num6) / 100;
					if (this.per_tem <= 0)
					{
						this.per_tem = 0;
					}
					if (this.per_tem < this.per)
					{
						this.per_tem = this.per;
					}
					if (this.offset >= 3)
					{
						this.offset = 3;
					}
				}
				g.drawImage(GameScr.imgHP_tm_xam, this.x - (imageWidth >> 1), this.y - this.h - 5, mGraphics.TOP | mGraphics.LEFT);
				g.setColor(16777215);
				g.fillRect(this.x - (imageWidth >> 1), this.y - this.h - 5, num2, 2);
				g.drawRegion(this.imgHPtem, 0, 0, num, imageHeight, 0, this.x - (imageWidth >> 1), this.y - this.h - 5, mGraphics.TOP | mGraphics.LEFT);
			}
			return;
		}
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x00006D9B File Offset: 0x00004F9B
	public int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x00078940 File Offset: 0x00076B40
	public void startDie()
	{
		this.cTimeDie = mSystem.currentTimeMillis();
		this.hp = 0L;
		this.injureThenDie = true;
		this.hp = 0L;
		this.status = 1;
		Res.outz("MOB DIEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEe");
		this.p1 = -3;
		this.p2 = -this.dir;
		this.p3 = 0;
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x0007899C File Offset: 0x00076B9C
	public void attackOtherMob(Mob mobToAttack)
	{
		this.mobToAttack = mobToAttack;
		this.isBusyAttackSomeOne = true;
		this.cFocus = null;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
		this.dir = ((mobToAttack.x <= this.x) ? -1 : 1);
		int num = mobToAttack.x;
		int num2 = mobToAttack.y;
		if (Res.abs(num - this.x) < this.w * 2 && Res.abs(num2 - this.y) < this.h * 2)
		{
			if (this.x < num)
			{
				this.x = num - this.w;
			}
			else
			{
				this.x = num + this.w;
			}
			this.p3 = 0;
			return;
		}
		this.p3 = 1;
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x00006DDE File Offset: 0x00004FDE
	public int getX()
	{
		return this.x;
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x00007E71 File Offset: 0x00006071
	public int getY()
	{
		return this.y;
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x00007E79 File Offset: 0x00006079
	public int getH()
	{
		return this.h;
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x00007E81 File Offset: 0x00006081
	public int getW()
	{
		return this.w;
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x00078A64 File Offset: 0x00076C64
	public void stopMoving()
	{
		if (this.status == 5)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x00007E89 File Offset: 0x00006089
	public bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x00007E9E File Offset: 0x0000609E
	public void removeHoldEff()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x00007EAF File Offset: 0x000060AF
	public void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x00007EB8 File Offset: 0x000060B8
	public void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x00078AA4 File Offset: 0x00076CA4
	public void GetFrame()
	{
		if (this.isGetFr && this.isTypeNewMod() && Mob.arrMobTemplate[this.templateId].data != null)
		{
			this.frameArr = (int[][])Controller.frameHT_NEWBOSS.get(this.templateId + string.Empty);
			this.stand = this.frameArr[0];
			this.move = this.frameArr[1];
			this.moveFast = this.frameArr[2];
			this.attack1 = this.frameArr[3];
			this.attack2 = this.frameArr[4];
			this.hurt = this.frameArr[5];
			this.isGetFr = false;
		}
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x00007EC1 File Offset: 0x000060C1
	private bool isTypeNewMod()
	{
		return Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.typeData == 2;
	}

	// Token: 0x04000FDC RID: 4060
	public const sbyte TYPE_DUNG = 0;

	// Token: 0x04000FDD RID: 4061
	public const sbyte TYPE_DI = 1;

	// Token: 0x04000FDE RID: 4062
	public const sbyte TYPE_NHAY = 2;

	// Token: 0x04000FDF RID: 4063
	public const sbyte TYPE_LET = 3;

	// Token: 0x04000FE0 RID: 4064
	public const sbyte TYPE_BAY = 4;

	// Token: 0x04000FE1 RID: 4065
	public const sbyte TYPE_BAY_DAU = 5;

	// Token: 0x04000FE2 RID: 4066
	public static MobTemplate[] arrMobTemplate;

	// Token: 0x04000FE3 RID: 4067
	public const sbyte MA_INHELL = 0;

	// Token: 0x04000FE4 RID: 4068
	public const sbyte MA_DEADFLY = 1;

	// Token: 0x04000FE5 RID: 4069
	public const sbyte MA_STANDWAIT = 2;

	// Token: 0x04000FE6 RID: 4070
	public const sbyte MA_ATTACK = 3;

	// Token: 0x04000FE7 RID: 4071
	public const sbyte MA_STANDFLY = 4;

	// Token: 0x04000FE8 RID: 4072
	public const sbyte MA_WALK = 5;

	// Token: 0x04000FE9 RID: 4073
	public const sbyte MA_FALL = 6;

	// Token: 0x04000FEA RID: 4074
	public const sbyte MA_INJURE = 7;

	// Token: 0x04000FEB RID: 4075
	public bool changBody;

	// Token: 0x04000FEC RID: 4076
	public short smallBody;

	// Token: 0x04000FED RID: 4077
	public bool isHintFocus;

	// Token: 0x04000FEE RID: 4078
	public string flystring;

	// Token: 0x04000FEF RID: 4079
	public int flyx;

	// Token: 0x04000FF0 RID: 4080
	public int flyy;

	// Token: 0x04000FF1 RID: 4081
	public int flyIndex;

	// Token: 0x04000FF2 RID: 4082
	public bool isFreez;

	// Token: 0x04000FF3 RID: 4083
	public int seconds;

	// Token: 0x04000FF4 RID: 4084
	public long last;

	// Token: 0x04000FF5 RID: 4085
	public long cur;

	// Token: 0x04000FF6 RID: 4086
	public int holdEffID;

	// Token: 0x04000FF7 RID: 4087
	public long hp;

	// Token: 0x04000FF8 RID: 4088
	public long maxHp;

	// Token: 0x04000FF9 RID: 4089
	public long hpInjure;

	// Token: 0x04000FFA RID: 4090
	public int x;

	// Token: 0x04000FFB RID: 4091
	public int y;

	// Token: 0x04000FFC RID: 4092
	public int dir = 1;

	// Token: 0x04000FFD RID: 4093
	public int dirV = 1;

	// Token: 0x04000FFE RID: 4094
	public int status;

	// Token: 0x04000FFF RID: 4095
	public int p1;

	// Token: 0x04001000 RID: 4096
	public int p2;

	// Token: 0x04001001 RID: 4097
	public int p3;

	// Token: 0x04001002 RID: 4098
	public int xFirst;

	// Token: 0x04001003 RID: 4099
	public int yFirst;

	// Token: 0x04001004 RID: 4100
	public int vy;

	// Token: 0x04001005 RID: 4101
	public int exp;

	// Token: 0x04001006 RID: 4102
	public int w;

	// Token: 0x04001007 RID: 4103
	public int h;

	// Token: 0x04001008 RID: 4104
	public int charIndex;

	// Token: 0x04001009 RID: 4105
	public int timeStatus;

	// Token: 0x0400100A RID: 4106
	public int mobId;

	// Token: 0x0400100B RID: 4107
	public bool isx;

	// Token: 0x0400100C RID: 4108
	public bool isy;

	// Token: 0x0400100D RID: 4109
	public bool isDisable;

	// Token: 0x0400100E RID: 4110
	public bool isDontMove;

	// Token: 0x0400100F RID: 4111
	public bool isFire;

	// Token: 0x04001010 RID: 4112
	public bool isIce;

	// Token: 0x04001011 RID: 4113
	public bool isWind;

	// Token: 0x04001012 RID: 4114
	public bool isDie;

	// Token: 0x04001013 RID: 4115
	public MyVector vMobMove = new MyVector();

	// Token: 0x04001014 RID: 4116
	public bool isGo;

	// Token: 0x04001015 RID: 4117
	public string mobName;

	// Token: 0x04001016 RID: 4118
	public int templateId;

	// Token: 0x04001017 RID: 4119
	public short pointx;

	// Token: 0x04001018 RID: 4120
	public short pointy;

	// Token: 0x04001019 RID: 4121
	public global::Char cFocus;

	// Token: 0x0400101A RID: 4122
	public long dame;

	// Token: 0x0400101B RID: 4123
	public long dameMp;

	// Token: 0x0400101C RID: 4124
	public int sys;

	// Token: 0x0400101D RID: 4125
	public sbyte levelBoss;

	// Token: 0x0400101E RID: 4126
	public sbyte level;

	// Token: 0x0400101F RID: 4127
	public bool isBoss;

	// Token: 0x04001020 RID: 4128
	public bool isMobMe;

	// Token: 0x04001021 RID: 4129
	public static MyVector lastMob = new MyVector();

	// Token: 0x04001022 RID: 4130
	public static MyVector newMob = new MyVector();

	// Token: 0x04001023 RID: 4131
	public bool isMafuba;

	// Token: 0x04001024 RID: 4132
	public int xMFB;

	// Token: 0x04001025 RID: 4133
	public int yMFB;

	// Token: 0x04001026 RID: 4134
	public int xSd;

	// Token: 0x04001027 RID: 4135
	public int ySd;

	// Token: 0x04001028 RID: 4136
	private bool isOutMap;

	// Token: 0x04001029 RID: 4137
	private int wCount;

	// Token: 0x0400102A RID: 4138
	public bool isShadown = true;

	// Token: 0x0400102B RID: 4139
	private int tick;

	// Token: 0x0400102C RID: 4140
	private int frame;

	// Token: 0x0400102D RID: 4141
	public static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x0400102E RID: 4142
	private bool wy;

	// Token: 0x0400102F RID: 4143
	private int wt;

	// Token: 0x04001030 RID: 4144
	private int fy;

	// Token: 0x04001031 RID: 4145
	private int ty;

	// Token: 0x04001032 RID: 4146
	public int typeSuperEff;

	// Token: 0x04001033 RID: 4147
	public bool isBusyAttackSomeOne = true;

	// Token: 0x04001034 RID: 4148
	public int[] stand = new int[]
	{
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1
	};

	// Token: 0x04001035 RID: 4149
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

	// Token: 0x04001036 RID: 4150
	public int[] moveFast = new int[]
	{
		1,
		1,
		2,
		2,
		3,
		3,
		2
	};

	// Token: 0x04001037 RID: 4151
	public int[] attack1 = new int[]
	{
		4,
		5,
		6
	};

	// Token: 0x04001038 RID: 4152
	public int[] attack2 = new int[]
	{
		7,
		8,
		9
	};

	// Token: 0x04001039 RID: 4153
	public int[] hurt = new int[1];

	// Token: 0x0400103A RID: 4154
	private int color = 8421504;

	// Token: 0x0400103B RID: 4155
	public int len = 24;

	// Token: 0x0400103C RID: 4156
	public int w_hp_bar = 24;

	// Token: 0x0400103D RID: 4157
	public int per = 100;

	// Token: 0x0400103E RID: 4158
	public int per_tem = 100;

	// Token: 0x0400103F RID: 4159
	public byte h_hp_bar = 4;

	// Token: 0x04001040 RID: 4160
	public Image imgHPtem;

	// Token: 0x04001041 RID: 4161
	private int offset;

	// Token: 0x04001042 RID: 4162
	public bool isHide;

	// Token: 0x04001043 RID: 4163
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x04001044 RID: 4164
	public global::Char injureBy;

	// Token: 0x04001045 RID: 4165
	public bool injureThenDie;

	// Token: 0x04001046 RID: 4166
	public Mob mobToAttack;

	// Token: 0x04001047 RID: 4167
	public int forceWait;

	// Token: 0x04001048 RID: 4168
	public bool blindEff;

	// Token: 0x04001049 RID: 4169
	public bool sleepEff;

	// Token: 0x0400104A RID: 4170
	private int[][] frameArr = new int[][]
	{
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		}
	};

	// Token: 0x0400104B RID: 4171
	private bool isGetFr = true;

	// Token: 0x0400104C RID: 4172
	public long cTimeDie;
}
