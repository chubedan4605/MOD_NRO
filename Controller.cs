using System;
using Assets.src.e;
using Assets.src.f;
using Assets.src.g;
using UnityEngine;
using Mod.DungPham.KoiOctiiu957;

// Token: 0x02000099 RID: 153
public class Controller : IMessageHandler
{
	// Token: 0x060004D1 RID: 1233 RVA: 0x00006BF3 File Offset: 0x00004DF3
	public static Controller gI()
	{
		if (Controller.me == null)
		{
			Controller.me = new Controller();
		}
		return Controller.me;
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x00006C0E File Offset: 0x00004E0E
	public static Controller gI2()
	{
		if (Controller.me2 == null)
		{
			Controller.me2 = new Controller();
		}
		return Controller.me2;
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00006C29 File Offset: 0x00004E29
	public void onConnectOK(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onConnectOK();
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x00006C36 File Offset: 0x00004E36
	public void onConnectionFail(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onConnectionFail();
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x00006C43 File Offset: 0x00004E43
	public void onDisconnected(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onDisconnected();
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x000315AC File Offset: 0x0002F7AC
	public void requestItemPlayer(Message msg)
	{
		try
		{
			int num = (int)msg.reader().readUnsignedByte();
			Item item = GameScr.currentCharViewInfo.arrItemBody[num];
			item.saleCoinLock = msg.reader().readInt();
			item.sys = (int)msg.reader().readByte();
			item.options = new MyVector();
			try
			{
				for (;;)
				{
					ItemOption itemOption = this.readItemOption(msg);
					if (itemOption != null)
					{
						item.options.addElement(itemOption);
					}
				}
			}
			catch (Exception ex)
			{
				Cout.println("Loi tairequestItemPlayer 1" + ex.ToString());
			}
		}
		catch (Exception ex2)
		{
			Cout.println("Loi tairequestItemPlayer 2" + ex2.ToString());
		}
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x00031678 File Offset: 0x0002F878
	public void onMessage(Message msg)
	{
		GameCanvas.debugSession.removeAllElements();
		GameCanvas.debug("SA1", 2);
		try
		{
			if (msg.command != -74)
			{
				Res.outz("=========> [READ] cmd= " + msg.command);
			}
			global::Char @char = null;
			MyVector myVector = new MyVector();
			int i = 0;
			GameCanvas.timeLoading = 15;
			Controller2.readMessage(msg);
			sbyte b = msg.command;
			switch (b)
			{
			case -112:
			{
				sbyte b2 = msg.reader().readByte();
				if (b2 == 0)
				{
					GameScr.findMobInMap(msg.reader().readByte()).clearBody();
				}
				if (b2 == 1)
				{
					GameScr.findMobInMap(msg.reader().readByte()).setBody(msg.reader().readShort());
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -111:
			case -110:
			case -109:
			case -108:
			case -106:
			case -105:
			case -104:
			case -103:
			case -102:
			case -101:
			case -100:
			case -89:
			case -78:
			case -75:
			case -73:
			case -72:
			case -71:
			case -58:
			case -56:
			case -55:
			case -54:
			case -49:
			case -48:
			case -40:
			case -39:
			case -38:
			case -33:
			case -27:
			case -23:
			case -17:
			case -16:
			case -15:
			case -13:
			case -12:
			case -11:
			case -10:
			case -9:
			case -8:
			case -7:
			case -6:
			case -5:
			case -3:
			case -2:
			case -1:
			case 3:
			case 4:
			case 5:
			case 8:
			case 9:
			case 10:
			case 13:
			case 14:
			case 15:
			case 16:
			case 17:
			case 18:
			case 19:
			case 21:
			case 22:
			case 23:
			case 25:
			case 26:
			case 28:
			case 30:
			case 31:
			case 34:
			case 35:
			case 36:
			case 37:
			case 42:
			case 44:
			case 45:
			case 48:
			case 49:
			case 50:
			case 51:
			case 52:
			case 53:
			case 55:
			case 59:
			case 60:
			case 61:
			case 67:
			case 70:
			case 71:
			case 72:
			case 73:
			case 74:
			case 75:
			case 76:
			case 77:
			case 78:
			case 79:
			case 80:
			case 89:
			case 91:
			case 93:
				goto IL_8E36;
			case -107:
			{
				sbyte b3 = msg.reader().readByte();
				if (b3 == 0)
				{
					global::Char.myCharz().havePet = false;
				}
				if (b3 == 1)
				{
					global::Char.myCharz().havePet = true;
				}
				if (b3 != 2)
				{
					goto IL_8E36;
				}
				InfoDlg.hide();
				global::Char.myPetz().head = (int)msg.reader().readShort();
				global::Char.myPetz().setDefaultPart();
				int num = (int)msg.reader().readUnsignedByte();
				Res.outz("num body = " + num);
				global::Char.myPetz().arrItemBody = new Item[num];
				for (int j = 0; j < num; j++)
				{
					short num2 = msg.reader().readShort();
					Res.outz("template id= " + num2);
					if (num2 != -1)
					{
						Res.outz("1");
						global::Char.myPetz().arrItemBody[j] = new Item();
						global::Char.myPetz().arrItemBody[j].template = ItemTemplates.get(num2);
						int type = (int)global::Char.myPetz().arrItemBody[j].template.type;
						global::Char.myPetz().arrItemBody[j].quantity = msg.reader().readInt();
						Res.outz("3");
						global::Char.myPetz().arrItemBody[j].info = msg.reader().readUTF();
						global::Char.myPetz().arrItemBody[j].content = msg.reader().readUTF();
						int num3 = (int)msg.reader().readUnsignedByte();
						Res.outz("option size= " + num3);
						if (num3 != 0)
						{
							global::Char.myPetz().arrItemBody[j].itemOption = new ItemOption[num3];
							for (int k = 0; k < global::Char.myPetz().arrItemBody[j].itemOption.Length; k++)
							{
								ItemOption itemOption = this.readItemOption(msg);
								if (itemOption != null)
								{
									global::Char.myPetz().arrItemBody[j].itemOption[k] = itemOption;
								}
							}
						}
						if (type != 0)
						{
							if (type == 1)
							{
								global::Char.myPetz().leg = (int)global::Char.myPetz().arrItemBody[j].template.part;
							}
						}
						else
						{
							global::Char.myPetz().body = (int)global::Char.myPetz().arrItemBody[j].template.part;
						}
					}
				}
				global::Char.myPetz().cHP = msg.reader().readLong();
				global::Char.myPetz().cHPFull = msg.reader().readLong();
				global::Char.myPetz().cMP = msg.reader().readLong();
				global::Char.myPetz().cMPFull = msg.reader().readLong();
				global::Char.myPetz().cDamFull = msg.reader().readLong();
				global::Char.myPetz().cName = msg.reader().readUTF();
				global::Char.myPetz().currStrLevel = msg.reader().readUTF();
				global::Char.myPetz().cPower = msg.reader().readLong();
				global::Char.myPetz().cTiemNang = msg.reader().readLong();
				global::Char.myPetz().petStatus = msg.reader().readByte();
				global::Char.myPetz().cStamina = (int)msg.reader().readShort();
				global::Char.myPetz().cMaxStamina = msg.reader().readShort();
				global::Char.myPetz().cCriticalFull = (int)msg.reader().readByte();
				global::Char.myPetz().cDefull = msg.reader().readLong();
				global::Char.myPetz().arrPetSkill = new Skill[(int)msg.reader().readByte()];
				Res.outz("SKILLENT = " + global::Char.myPetz().arrPetSkill);
				for (int l = 0; l < global::Char.myPetz().arrPetSkill.Length; l++)
				{
					short num4 = msg.reader().readShort();
					if (num4 != -1)
					{
						global::Char.myPetz().arrPetSkill[l] = Skills.get(num4);
					}
					else
					{
						global::Char.myPetz().arrPetSkill[l] = new Skill();
						global::Char.myPetz().arrPetSkill[l].template = null;
						global::Char.myPetz().arrPetSkill[l].moreInfo = msg.reader().readUTF();
					}
				}
				global::Char.myPetz().cGiamST = (long)msg.reader().readByte();
				global::Char.myPetz().cCritDameFull = (int)msg.reader().readShort();
				if (!Panel.isOpeningPet)
				{
					goto IL_8E36;
				}
				Panel.isOpeningPet = false;
				if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
				{
					GameCanvas.panel2 = new Panel();
					GameCanvas.panel2.tabName[7] = new string[][]
					{
						new string[]
						{
							string.Empty
						}
					};
					GameCanvas.panel2.setTypeBodyOnly();
					GameCanvas.panel2.show();
					GameCanvas.panel.setTypePetMain();
					GameCanvas.panel.show();
					goto IL_8E36;
				}
				GameCanvas.panel.tabName[21] = mResources.petMainTab;
				GameCanvas.panel.setTypePetMain();
				GameCanvas.panel.show();
				goto IL_8E36;
			}
			case -99:
				InfoDlg.hide();
				if (msg.reader().readByte() == 0)
				{
					GameCanvas.panel.vEnemy.removeAllElements();
					int num5 = (int)msg.reader().readUnsignedByte();
					for (int m = 0; m < num5; m++)
					{
						global::Char char2 = new global::Char();
						char2.charID = msg.reader().readInt();
						char2.head = (int)msg.reader().readShort();
						char2.headICON = (int)msg.reader().readShort();
						char2.body = (int)msg.reader().readShort();
						char2.leg = (int)msg.reader().readShort();
						char2.bag = (int)msg.reader().readShort();
						char2.cName = msg.reader().readUTF();
						InfoItem infoItem = new InfoItem(msg.reader().readUTF());
						bool isOnline = msg.reader().readBoolean();
						infoItem.charInfo = char2;
						infoItem.isOnline = isOnline;
						Res.outz("isonline = " + isOnline.ToString());
						GameCanvas.panel.vEnemy.addElement(infoItem);
					}
					GameCanvas.panel.setTypeEnemy();
					GameCanvas.panel.show();
					goto IL_8E36;
				}
				goto IL_8E36;
			case -98:
			{
				bool flag = msg.reader().readByte() != 0;
				GameCanvas.menu.showMenu = false;
				if (!flag)
				{
					GameCanvas.startYesNoDlg(msg.reader().readUTF(), new Command(mResources.YES, GameCanvas.instance, 888397, msg.reader().readUTF()), new Command(mResources.NO, GameCanvas.instance, 888396, null));
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -97:
				global::Char.myCharz().cNangdong = (long)msg.reader().readInt();
				goto IL_8E36;
			case -96:
			{
				sbyte typeTop = msg.reader().readByte();
				GameCanvas.panel.vTop.removeAllElements();
				string topName = msg.reader().readUTF();
				sbyte b4 = msg.reader().readByte();
				for (int n = 0; n < (int)b4; n++)
				{
					int rank = msg.reader().readInt();
					int pId = msg.reader().readInt();
					short headID = msg.reader().readShort();
					short headICON = msg.reader().readShort();
					short body = msg.reader().readShort();
					short leg = msg.reader().readShort();
					string name = msg.reader().readUTF();
					string info = msg.reader().readUTF();
					TopInfo topInfo = new TopInfo();
					topInfo.rank = rank;
					topInfo.headID = (int)headID;
					topInfo.headICON = (int)headICON;
					topInfo.body = body;
					topInfo.leg = leg;
					topInfo.name = name;
					topInfo.info = info;
					topInfo.info2 = msg.reader().readUTF();
					topInfo.pId = pId;
					GameCanvas.panel.vTop.addElement(topInfo);
				}
				GameCanvas.panel.topName = topName;
				GameCanvas.panel.setTypeTop(typeTop);
				GameCanvas.panel.show();
				goto IL_8E36;
			}
			case -95:
			{
				sbyte b5 = msg.reader().readByte();
				Res.outz("type= " + b5);
				if (b5 == 0)
				{
					int num6 = msg.reader().readInt();
					short templateId = msg.reader().readShort();
					long num7 = msg.reader().readLong();
					SoundMn.gI().explode_1();
					if (num6 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().mobMe = new Mob(num6, false, false, false, false, false, (int)templateId, 1, num7, 0, num7, (short)(global::Char.myCharz().cx + ((global::Char.myCharz().cdir != 1) ? -40 : 40)), (short)global::Char.myCharz().cy, 4, 0);
						global::Char.myCharz().mobMe.isMobMe = true;
						EffecMn.addEff(new Effect(18, global::Char.myCharz().mobMe.x, global::Char.myCharz().mobMe.y, 2, 10, -1));
						global::Char.myCharz().tMobMeBorn = 30;
						GameScr.vMob.addElement(global::Char.myCharz().mobMe);
					}
					else
					{
						@char = GameScr.findCharInMap(num6);
						if (@char != null)
						{
							@char.mobMe = new Mob(num6, false, false, false, false, false, (int)templateId, 1, num7, 0, num7, (short)@char.cx, (short)@char.cy, 4, 0)
							{
								isMobMe = true
							};
							GameScr.vMob.addElement(@char.mobMe);
						}
						else if (GameScr.findMobInMap(num6) == null)
						{
							Mob mob = new Mob(num6, false, false, false, false, false, (int)templateId, 1, num7, 0, num7, -100, -100, 4, 0);
							mob.isMobMe = true;
							GameScr.vMob.addElement(mob);
						}
					}
				}
				if (b5 == 1)
				{
					int num8 = msg.reader().readInt();
					int mobId = (int)msg.reader().readByte();
					Res.outz("mod attack id= " + num8);
					if (num8 == global::Char.myCharz().charID)
					{
						if (GameScr.findMobInMap(mobId) != null)
						{
							global::Char.myCharz().mobMe.attackOtherMob(GameScr.findMobInMap(mobId));
						}
					}
					else
					{
						@char = GameScr.findCharInMap(num8);
						if (@char != null && GameScr.findMobInMap(mobId) != null)
						{
							@char.mobMe.attackOtherMob(GameScr.findMobInMap(mobId));
						}
					}
				}
				if (b5 == 2)
				{
					int num9 = msg.reader().readInt();
					int num10 = msg.reader().readInt();
					long num11 = msg.reader().readLong();
					long cHPNew = msg.reader().readLong();
					if (num9 == global::Char.myCharz().charID)
					{
						Res.outz("mob dame= " + num11);
						@char = GameScr.findCharInMap(num10);
						if (@char != null)
						{
							@char.cHPNew = cHPNew;
							if (global::Char.myCharz().mobMe.isBusyAttackSomeOne)
							{
								@char.doInjure(num11, 0L, false, true);
							}
							else
							{
								global::Char.myCharz().mobMe.dame = num11;
								global::Char.myCharz().mobMe.setAttack(@char);
							}
						}
					}
					else
					{
						Mob mob2 = GameScr.findMobInMap(num9);
						if (mob2 != null)
						{
							if (num10 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().cHPNew = cHPNew;
								if (mob2.isBusyAttackSomeOne)
								{
									global::Char.myCharz().doInjure(num11, 0L, false, true);
								}
								else
								{
									mob2.dame = num11;
									mob2.setAttack(global::Char.myCharz());
								}
							}
							else
							{
								@char = GameScr.findCharInMap(num10);
								if (@char != null)
								{
									@char.cHPNew = cHPNew;
									if (mob2.isBusyAttackSomeOne)
									{
										@char.doInjure(num11, 0L, false, true);
									}
									else
									{
										mob2.dame = num11;
										mob2.setAttack(@char);
									}
								}
							}
						}
					}
				}
				if (b5 == 3)
				{
					int num12 = msg.reader().readInt();
					int mobId2 = msg.reader().readInt();
					long hp = msg.reader().readLong();
					long num13 = msg.reader().readLong();
					@char = null;
					@char = ((global::Char.myCharz().charID != num12) ? GameScr.findCharInMap(num12) : global::Char.myCharz());
					if (@char != null)
					{
						Mob mob3 = GameScr.findMobInMap(mobId2);
						if (@char.mobMe != null)
						{
							@char.mobMe.attackOtherMob(mob3);
						}
						if (mob3 != null)
						{
							mob3.hp = hp;
							mob3.updateHp_bar();
							if (num13 == 0L)
							{
								mob3.x = mob3.xFirst;
								mob3.y = mob3.yFirst;
								GameScr.startFlyText(mResources.miss, mob3.x, mob3.y - mob3.h, 0, -2, mFont.MISS);
							}
							else
							{
								GameScr.startFlyText("-" + num13, mob3.x, mob3.y - mob3.h, 0, -2, mFont.ORANGE);
							}
						}
					}
				}
				if (b5 == 5)
				{
					int num14 = msg.reader().readInt();
					sbyte b6 = msg.reader().readByte();
					int mobId3 = msg.reader().readInt();
					long num15 = msg.reader().readLong();
					long hp2 = msg.reader().readLong();
					@char = null;
					@char = ((num14 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num14) : global::Char.myCharz());
					if (@char == null)
					{
						return;
					}
					if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
					{
						@char.setSkillPaint(GameScr.sks[(int)b6], 0);
					}
					else
					{
						@char.setSkillPaint(GameScr.sks[(int)b6], 1);
					}
					Mob mob4 = GameScr.findMobInMap(mobId3);
					if (@char.cx <= mob4.x)
					{
						@char.cdir = 1;
					}
					else
					{
						@char.cdir = -1;
					}
					@char.mobFocus = mob4;
					mob4.hp = hp2;
					mob4.updateHp_bar();
					GameCanvas.debug("SA83v2", 2);
					if (num15 == 0L)
					{
						mob4.x = mob4.xFirst;
						mob4.y = mob4.yFirst;
						GameScr.startFlyText(mResources.miss, mob4.x, mob4.y - mob4.h, 0, -2, mFont.MISS);
					}
					else
					{
						GameScr.startFlyText("-" + num15, mob4.x, mob4.y - mob4.h, 0, -2, mFont.ORANGE);
					}
				}
				if (b5 == 6)
				{
					int num16 = msg.reader().readInt();
					if (num16 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().mobMe.startDie();
					}
					else
					{
						global::Char char3 = GameScr.findCharInMap(num16);
						if (char3 != null)
						{
							char3.mobMe.startDie();
						}
					}
				}
				if (b5 != 7)
				{
					goto IL_8E36;
				}
				int num17 = msg.reader().readInt();
				if (num17 == global::Char.myCharz().charID)
				{
					global::Char.myCharz().mobMe = null;
					for (int num18 = 0; num18 < GameScr.vMob.size(); num18++)
					{
						if (((Mob)GameScr.vMob.elementAt(num18)).mobId == num17)
						{
							GameScr.vMob.removeElementAt(num18);
						}
					}
					goto IL_8E36;
				}
				@char = GameScr.findCharInMap(num17);
				for (int num19 = 0; num19 < GameScr.vMob.size(); num19++)
				{
					if (((Mob)GameScr.vMob.elementAt(num19)).mobId == num17)
					{
						GameScr.vMob.removeElementAt(num19);
					}
				}
				if (@char != null)
				{
					@char.mobMe = null;
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -94:
				while (msg.reader().available() > 0)
				{
					short num20 = msg.reader().readShort();
					int num21 = msg.reader().readInt();
					for (int num22 = 0; num22 < global::Char.myCharz().vSkill.size(); num22++)
					{
						Skill skill = (Skill)global::Char.myCharz().vSkill.elementAt(num22);
						if (skill != null && skill.skillId == num20)
						{
							if (num21 < skill.coolDown)
							{
								skill.lastTimeUseThisSkill = mSystem.currentTimeMillis() - (long)(skill.coolDown - num21);
							}
							Res.outz(string.Concat(new object[]
							{
								"1 chieu id= ",
								skill.template.id,
								" cooldown= ",
								num21,
								"curr cool down= ",
								skill.coolDown
							}));
						}
					}
				}
				goto IL_8E36;
			case -93:
			{
				short num23 = msg.reader().readShort();
				BgItem.newSmallVersion = new sbyte[(int)num23];
				for (int num24 = 0; num24 < (int)num23; num24++)
				{
					BgItem.newSmallVersion[num24] = msg.reader().readByte();
				}
				goto IL_8E36;
			}
			case -92:
				Main.typeClient = (int)msg.reader().readByte();
				if (Rms.loadRMSString("ResVersion") == null)
				{
					Rms.clearAll();
				}
				Rms.saveRMSInt("clienttype", Main.typeClient);
				Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
				if (Rms.loadRMSString("ResVersion") == null)
				{
					GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
					goto IL_8E36;
				}
				goto IL_8E36;
			case -91:
			{
				sbyte b7 = msg.reader().readByte();
				GameCanvas.panel.mapNames = new string[(int)b7];
				GameCanvas.panel.planetNames = new string[(int)b7];
				for (int num25 = 0; num25 < (int)b7; num25++)
				{
					GameCanvas.panel.mapNames[num25] = msg.reader().readUTF();
					GameCanvas.panel.planetNames[num25] = msg.reader().readUTF();
				}
				GameCanvas.panel.setTypeMapTrans();
				GameCanvas.panel.show();
				goto IL_8E36;
			}
			case -90:
			{
				sbyte b8 = msg.reader().readByte();
				int num26 = msg.reader().readInt();
				Res.outz("===> UPDATE_BODY:    type = " + b8);
				@char = ((global::Char.myCharz().charID != num26) ? GameScr.findCharInMap(num26) : global::Char.myCharz());
				if (b8 != -1)
				{
					short num27 = msg.reader().readShort();
					short num28 = msg.reader().readShort();
					short num29 = msg.reader().readShort();
					sbyte isMonkey = msg.reader().readByte();
					if (@char != null)
					{
						if (@char.charID == num26)
						{
							@char.isMask = true;
							@char.isMonkey = isMonkey;
							if (@char.isMonkey != 0)
							{
								@char.isWaitMonkey = false;
								@char.isLockMove = false;
							}
						}
						else if (@char != null)
						{
							@char.isMask = true;
							@char.isMonkey = isMonkey;
						}
						if (num27 != -1)
						{
							@char.head = (int)num27;
						}
						if (num28 != -1)
						{
							@char.body = (int)num28;
						}
						if (num29 != -1)
						{
							@char.leg = (int)num29;
						}
					}
				}
				if (b8 == -1 && @char != null)
				{
					@char.isMask = false;
					@char.isMonkey = 0;
				}
				if (@char == null)
				{
					goto IL_8E36;
				}
				for (int num30 = 0; num30 < 54; num30++)
				{
					@char.removeEffChar(0, 201 + num30);
				}
				if (@char.bag >= 201 && @char.bag < 255)
				{
					@char.addEffChar(new Effect(@char.bag, @char, 2, -1, 10, 1)
					{
						typeEff = 5
					});
				}
				if (@char.bag == 30 && @char.me)
				{
					GameScr.isPickNgocRong = true;
				}
				if (@char.me)
				{
					GameScr.isudungCapsun4 = false;
					GameScr.isudungCapsun3 = false;
					for (int num31 = 0; num31 < global::Char.myCharz().arrItemBag.Length; num31++)
					{
						Item item = global::Char.myCharz().arrItemBag[num31];
						if (item != null)
						{
							if (item.template.id == 194)
							{
								GameScr.isudungCapsun4 = (item.quantity > 0);
								if (GameScr.isudungCapsun4)
								{
									break;
								}
							}
							else if (item.template.id == 193)
							{
								GameScr.isudungCapsun3 = (item.quantity > 0);
							}
						}
					}
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -88:
				GameCanvas.endDlg();
				GameCanvas.serverScreen.switchToMe();
				goto IL_8E36;
			case -87:
			{
				Res.outz("GET UPDATE_DATA " + msg.reader().available() + " bytes");
				msg.reader().mark(500000);
				this.createData(msg.reader(), true);
				msg.reader().reset();
				sbyte[] array = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref array);
				sbyte[] data = new sbyte[]
				{
					GameScr.vcData
				};
				Rms.saveRMS("NRdataVersion", data);
				LoginScr.isUpdateData = false;
				GameScr.gI().readOk();
				goto IL_8E36;
			}
			case -86:
			{
				sbyte b9 = msg.reader().readByte();
				Res.outz("server gui ve giao dich action = " + b9);
				if (b9 == 0)
				{
					int playerID = msg.reader().readInt();
					GameScr.gI().giaodich(playerID);
				}
				if (b9 == 1)
				{
					int num32 = msg.reader().readInt();
					global::Char char4 = GameScr.findCharInMap(num32);
					if (char4 == null)
					{
						return;
					}
					GameCanvas.panel.setTypeGiaoDich(char4);
					GameCanvas.panel.show();
					Service.gI().getPlayerMenu(num32);
				}
				if (b9 == 2)
				{
					sbyte b10 = msg.reader().readByte();
					for (int num33 = 0; num33 < GameCanvas.panel.vMyGD.size(); num33++)
					{
						Item item2 = (Item)GameCanvas.panel.vMyGD.elementAt(num33);
						if (item2.indexUI == (int)b10)
						{
							GameCanvas.panel.vMyGD.removeElement(item2);
							break;
						}
					}
				}
				if (b9 == 6)
				{
					GameCanvas.panel.isFriendLock = true;
					if (GameCanvas.panel2 != null)
					{
						GameCanvas.panel2.isFriendLock = true;
					}
					GameCanvas.panel.vFriendGD.removeAllElements();
					if (GameCanvas.panel2 != null)
					{
						GameCanvas.panel2.vFriendGD.removeAllElements();
					}
					int friendMoneyGD = msg.reader().readInt();
					sbyte b11 = msg.reader().readByte();
					Res.outz("item size = " + b11);
					for (int num34 = 0; num34 < (int)b11; num34++)
					{
						Item item3 = new Item();
						item3.template = ItemTemplates.get(msg.reader().readShort());
						item3.quantity = msg.reader().readInt();
						int num35 = (int)msg.reader().readUnsignedByte();
						if (num35 != 0)
						{
							item3.itemOption = new ItemOption[num35];
							for (int num36 = 0; num36 < item3.itemOption.Length; num36++)
							{
								ItemOption itemOption2 = this.readItemOption(msg);
								if (itemOption2 != null)
								{
									item3.itemOption[num36] = itemOption2;
									item3.compare = GameCanvas.panel.getCompare(item3);
								}
							}
						}
						if (GameCanvas.panel2 != null)
						{
							GameCanvas.panel2.vFriendGD.addElement(item3);
						}
						else
						{
							GameCanvas.panel.vFriendGD.addElement(item3);
						}
					}
					if (GameCanvas.panel2 != null)
					{
						GameCanvas.panel2.setTabGiaoDich(false);
						GameCanvas.panel2.friendMoneyGD = friendMoneyGD;
					}
					else
					{
						GameCanvas.panel.friendMoneyGD = friendMoneyGD;
						if (GameCanvas.panel.currentTabIndex == 2)
						{
							GameCanvas.panel.setTabGiaoDich(false);
						}
					}
				}
				if (b9 != 7)
				{
					goto IL_8E36;
				}
				InfoDlg.hide();
				if (GameCanvas.panel.isShow)
				{
					GameCanvas.panel.hide();
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -85:
			{
				Res.outz("CAP CHAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
				sbyte b12 = msg.reader().readByte();
				if (b12 == 0)
				{
					int num37 = (int)msg.reader().readUnsignedShort();
					Res.outz("lent =" + num37);
					sbyte[] imageData = new sbyte[num37];
					msg.reader().read(ref imageData, 0, num37);
					GameScr.imgCapcha = Image.createImage(imageData, 0, num37);
					GameScr.gI().keyInput = "-----";
					GameScr.gI().strCapcha = msg.reader().readUTF();
					GameScr.gI().keyCapcha = new int[GameScr.gI().strCapcha.Length];
					GameScr.gI().mobCapcha = new Mob();
					GameScr.gI().right = null;
				}
				if (b12 == 1)
				{
					MobCapcha.isAttack = true;
				}
				if (b12 == 2)
				{
					MobCapcha.explode = true;
					GameScr.gI().right = GameScr.gI().cmdFocus;
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -84:
			{
				int index = (int)msg.reader().readUnsignedByte();
				Mob mob5 = null;
				try
				{
					mob5 = (Mob)GameScr.vMob.elementAt(index);
				}
				catch (Exception)
				{
				}
				if (mob5 != null)
				{
					mob5.maxHp = msg.reader().readLong();
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -83:
			{
				sbyte b13 = msg.reader().readByte();
				if (b13 == 0)
				{
					int num38 = (int)msg.reader().readShort();
					int bgRID = (int)msg.reader().readShort();
					int num39 = (int)msg.reader().readUnsignedByte();
					int num40 = msg.reader().readInt();
					msg.reader().readUTF();
					int num41 = (int)msg.reader().readShort();
					int num42 = (int)msg.reader().readShort();
					if (msg.reader().readByte() == 1)
					{
						GameScr.gI().isRongNamek = true;
					}
					else
					{
						GameScr.gI().isRongNamek = false;
					}
					GameScr.gI().xR = num41;
					GameScr.gI().yR = num42;
					Res.outz(string.Concat(new object[]
					{
						"xR= ",
						num41,
						" yR= ",
						num42,
						" +++++++++++++++++++++++++++++++++++++++"
					}));
					if (global::Char.myCharz().charID == num40)
					{
						GameCanvas.panel.hideNow();
						GameScr.gI().activeRongThanEff(true);
					}
					else if (TileMap.mapID == num38 && TileMap.zoneID == num39)
					{
						GameScr.gI().activeRongThanEff(false);
					}
					else if (mGraphics.zoomLevel > 1)
					{
						GameScr.gI().doiMauTroi();
					}
					GameScr.gI().mapRID = num38;
					GameScr.gI().bgRID = bgRID;
					GameScr.gI().zoneRID = num39;
				}
				if (b13 == 1)
				{
					Res.outz(string.Concat(new object[]
					{
						"map RID = ",
						GameScr.gI().mapRID,
						" zone RID= ",
						GameScr.gI().zoneRID
					}));
					Res.outz(string.Concat(new object[]
					{
						"map ID = ",
						TileMap.mapID,
						" zone ID= ",
						TileMap.zoneID
					}));
					if (TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID)
					{
						GameScr.gI().hideRongThanEff();
					}
					else
					{
						GameScr.gI().isRongThanXuatHien = false;
						if (GameScr.gI().isRongNamek)
						{
							GameScr.gI().isRongNamek = false;
						}
					}
				}
				if (b13 != 2)
				{
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -82:
			{
				sbyte b14 = msg.reader().readByte();
				TileMap.tileIndex = new int[(int)b14][][];
				TileMap.tileType = new int[(int)b14][];
				TileMap.tileTypeCache = null;
				for (int num43 = 0; num43 < (int)b14; num43++)
				{
					sbyte b15 = msg.reader().readByte();
					TileMap.tileType[num43] = new int[(int)b15];
					TileMap.tileIndex[num43] = new int[(int)b15][];
					for (int num44 = 0; num44 < (int)b15; num44++)
					{
						TileMap.tileType[num43][num44] = msg.reader().readInt();
						sbyte b16 = msg.reader().readByte();
						TileMap.tileIndex[num43][num44] = new int[(int)b16];
						for (int num45 = 0; num45 < (int)b16; num45++)
						{
							TileMap.tileIndex[num43][num44][num45] = (int)msg.reader().readByte();
						}
					}
				}
				goto IL_8E36;
			}
			case -81:
			{
				sbyte b17 = msg.reader().readByte();
				if (b17 == 0)
				{
					string src = msg.reader().readUTF();
					string src2 = msg.reader().readUTF();
					GameCanvas.panel.setTypeCombine();
					GameCanvas.panel.combineInfo = mFont.tahoma_7b_blue.splitFontArray(src, Panel.WIDTH_PANEL);
					GameCanvas.panel.combineTopInfo = mFont.tahoma_7.splitFontArray(src2, Panel.WIDTH_PANEL);
					GameCanvas.panel.show();
				}
				if (b17 == 1)
				{
					GameCanvas.panel.vItemCombine.removeAllElements();
					sbyte b18 = msg.reader().readByte();
					for (int num46 = 0; num46 < (int)b18; num46++)
					{
						sbyte b19 = msg.reader().readByte();
						for (int num47 = 0; num47 < global::Char.myCharz().arrItemBag.Length; num47++)
						{
							Item item4 = global::Char.myCharz().arrItemBag[num47];
							if (item4 != null && item4.indexUI == (int)b19)
							{
								item4.isSelect = true;
								GameCanvas.panel.vItemCombine.addElement(item4);
							}
						}
					}
					if (GameCanvas.panel.isShow)
					{
						GameCanvas.panel.setTabCombine();
					}
				}
				if (b17 == 2)
				{
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(0);
				}
				if (b17 == 3)
				{
					GameCanvas.panel.combineSuccess = 1;
					GameCanvas.panel.setCombineEff(0);
				}
				if (b17 == 4)
				{
					short iconID = msg.reader().readShort();
					GameCanvas.panel.iconID3 = iconID;
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(1);
				}
				if (b17 == 5)
				{
					short iconID2 = msg.reader().readShort();
					GameCanvas.panel.iconID3 = iconID2;
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(2);
				}
				if (b17 == 6)
				{
					short iconID3 = msg.reader().readShort();
					short iconID4 = msg.reader().readShort();
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(3);
					GameCanvas.panel.iconID1 = iconID3;
					GameCanvas.panel.iconID3 = iconID4;
				}
				if (b17 == 7)
				{
					short iconID5 = msg.reader().readShort();
					GameCanvas.panel.iconID3 = iconID5;
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(4);
				}
				if (b17 == 8)
				{
					GameCanvas.panel.iconID3 = -1;
					GameCanvas.panel.combineSuccess = 1;
					GameCanvas.panel.setCombineEff(4);
				}
				short num48 = 21;
				try
				{
					num48 = msg.reader().readShort();
					int num49 = (int)msg.reader().readShort();
					int num50 = (int)msg.reader().readShort();
					GameCanvas.panel.xS = num49 - GameScr.cmx;
					GameCanvas.panel.yS = num50 - GameScr.cmy;
				}
				catch (Exception)
				{
				}
				for (int num51 = 0; num51 < GameScr.vNpc.size(); num51++)
				{
					Npc npc = (Npc)GameScr.vNpc.elementAt(num51);
					if (npc.template.npcTemplateId == (int)num48)
					{
						GameCanvas.panel.xS = npc.cx - GameScr.cmx;
						GameCanvas.panel.yS = npc.cy - GameScr.cmy;
						GameCanvas.panel.idNPC = (int)num48;
						break;
					}
				}
				goto IL_8E36;
			}
			case -80:
			{
				sbyte b20 = msg.reader().readByte();
				InfoDlg.hide();
				if (b20 == 0)
				{
					GameCanvas.panel.vFriend.removeAllElements();
					int num52 = (int)msg.reader().readUnsignedByte();
					for (int num53 = 0; num53 < num52; num53++)
					{
						global::Char char5 = new global::Char();
						char5.charID = msg.reader().readInt();
						char5.head = (int)msg.reader().readShort();
						char5.headICON = (int)msg.reader().readShort();
						char5.body = (int)msg.reader().readShort();
						char5.leg = (int)msg.reader().readShort();
						char5.bag = (int)msg.reader().readShort();
						char5.cName = msg.reader().readUTF();
						bool isOnline2 = msg.reader().readBoolean();
						InfoItem infoItem2 = new InfoItem(mResources.power + ": " + msg.reader().readUTF());
						infoItem2.charInfo = char5;
						infoItem2.isOnline = isOnline2;
						GameCanvas.panel.vFriend.addElement(infoItem2);
					}
					GameCanvas.panel.setTypeFriend();
					GameCanvas.panel.show();
				}
				if (b20 == 3)
				{
					MyVector vFriend = GameCanvas.panel.vFriend;
					int num54 = msg.reader().readInt();
					Res.outz("online offline id=" + num54);
					for (int num55 = 0; num55 < vFriend.size(); num55++)
					{
						InfoItem infoItem3 = (InfoItem)vFriend.elementAt(num55);
						if (infoItem3.charInfo != null && infoItem3.charInfo.charID == num54)
						{
							Res.outz("online= " + infoItem3.isOnline.ToString());
							infoItem3.isOnline = msg.reader().readBoolean();
							break;
						}
					}
				}
				if (b20 != 2)
				{
					goto IL_8E36;
				}
				MyVector vFriend2 = GameCanvas.panel.vFriend;
				int num56 = msg.reader().readInt();
				for (int num57 = 0; num57 < vFriend2.size(); num57++)
				{
					InfoItem infoItem4 = (InfoItem)vFriend2.elementAt(num57);
					if (infoItem4.charInfo != null && infoItem4.charInfo.charID == num56)
					{
						vFriend2.removeElement(infoItem4);
						break;
					}
				}
				if (GameCanvas.panel.isShow)
				{
					GameCanvas.panel.setTabFriend();
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -79:
			{
				InfoDlg.hide();
				msg.reader().readInt();
				global::Char charMenu = GameCanvas.panel.charMenu;
				if (charMenu == null)
				{
					return;
				}
				charMenu.cPower = msg.reader().readLong();
				charMenu.currStrLevel = msg.reader().readUTF();
				goto IL_8E36;
			}
			case -77:
			{
				short num58 = msg.reader().readShort();
				SmallImage.newSmallVersion = new sbyte[(int)num58];
				SmallImage.maxSmall = num58;
				SmallImage.imgNew = new Small[(int)num58];
				for (int num59 = 0; num59 < (int)num58; num59++)
				{
					SmallImage.newSmallVersion[num59] = msg.reader().readByte();
				}
				goto IL_8E36;
			}
			case -76:
				b = msg.reader().readByte();
				if (b != 0)
				{
					if (b != 1)
					{
						goto IL_8E36;
					}
					int num60 = (int)msg.reader().readUnsignedByte();
					if (global::Char.myCharz().arrArchive[num60] != null)
					{
						global::Char.myCharz().arrArchive[num60].isRecieve = true;
						goto IL_8E36;
					}
					goto IL_8E36;
				}
				else
				{
					sbyte b21 = msg.reader().readByte();
					if (b21 <= 0)
					{
						return;
					}
					global::Char.myCharz().arrArchive = new Archivement[(int)b21];
					for (int num61 = 0; num61 < (int)b21; num61++)
					{
						global::Char.myCharz().arrArchive[num61] = new Archivement();
						global::Char.myCharz().arrArchive[num61].info1 = num61 + 1 + ". " + msg.reader().readUTF();
						global::Char.myCharz().arrArchive[num61].info2 = msg.reader().readUTF();
						global::Char.myCharz().arrArchive[num61].money = (int)msg.reader().readShort();
						global::Char.myCharz().arrArchive[num61].isFinish = msg.reader().readBoolean();
						global::Char.myCharz().arrArchive[num61].isRecieve = msg.reader().readBoolean();
					}
					GameCanvas.panel.setTypeArchivement();
					GameCanvas.panel.show();
					goto IL_8E36;
				}
				break;
			case -74:
			{
				if (ServerListScreen.stopDownload)
				{
					return;
				}
				if (!GameCanvas.isGetResourceFromServer())
				{
					Service.gI().getResource(3, null);
					SmallImage.loadBigRMS();
					SplashScr.imgLogo = null;
					if (Rms.loadRMSString("acc") != null || Rms.loadRMSString("userAo" + ServerListScreen.ipSelect) != null)
					{
						LoginScr.isContinueToLogin = true;
					}
					GameCanvas.loginScr = new LoginScr();
					GameCanvas.loginScr.switchToMe();
					return;
				}
				bool flag2 = true;
				Res.outz("1>>GET_IMAGE_SOURCE = " + msg.reader().available());
				sbyte b22 = msg.reader().readByte();
				Res.outz("2>GET_IMAGE_SOURCE = " + b22);
				if (b22 == 0)
				{
					int num62 = msg.reader().readInt();
					Res.outz("3>GET_IMAGE_SOURCE serverVersion = " + num62);
					string text = Rms.loadRMSString("ResVersion");
					int num63 = (text == null || !(text != string.Empty)) ? -1 : int.Parse(text);
					Res.outz(string.Concat(new object[]
					{
						"4>>>GET_IMAGE_SOURCE: version>> ",
						text,
						" <> ",
						num63,
						"!=",
						num62
					}));
					if (num63 == -1 || num63 != num62)
					{
						GameCanvas.serverScreen.show2();
					}
					else
					{
						SmallImage.loadBigRMS();
						SplashScr.imgLogo = null;
						ServerListScreen.loadScreen = true;
						Res.outz(">>>vo ne: " + GameCanvas.currentScreen);
						if (GameCanvas.currentScreen != GameCanvas.loginScr)
						{
							if (GameCanvas.serverScreen == null)
							{
								GameCanvas.serverScreen = new ServerListScreen();
							}
							GameCanvas.serverScreen.switchToMe();
						}
						else
						{
							if (GameCanvas.loginScr == null)
							{
								GameCanvas.loginScr = new LoginScr();
							}
							GameCanvas.loginScr.doLogin();
						}
					}
				}
				if (b22 == 1)
				{
					ServerListScreen.strWait = mResources.downloading_data;
					ServerListScreen.nBig = (int)msg.reader().readShort();
					Service.gI().getResource(2, null);
				}
				if (b22 == 2)
				{
					try
					{
						Controller.isLoadingData = true;
						GameCanvas.endDlg();
						ServerListScreen.demPercent++;
						ServerListScreen.percent = ServerListScreen.demPercent * 100 / ServerListScreen.nBig;
						string text2 = msg.reader().readUTF();
						Res.outz(">>>vo serverPath: " + text2);
						string[] array2 = Res.split(text2, "/", 0);
						string filename = "x" + mGraphics.zoomLevel + array2[array2.Length - 1];
						int num64 = msg.reader().readInt();
						sbyte[] data2 = new sbyte[num64];
						msg.reader().read(ref data2, 0, num64);
						Rms.saveRMS(filename, data2);
					}
					catch (Exception)
					{
						GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
					}
				}
				if (b22 != 3 || !flag2)
				{
					goto IL_8E36;
				}
				Controller.isLoadingData = false;
				int num65 = msg.reader().readInt();
				Res.outz(">>>GET_IMAGE_SOURCE: lastVersion>> " + num65);
				Rms.saveRMSString("ResVersion", num65 + string.Empty);
				Service.gI().getResource(3, null);
				GameCanvas.endDlg();
				SplashScr.imgLogo = null;
				SmallImage.loadBigRMS();
				mSystem.gcc();
				ServerListScreen.bigOk = true;
				ServerListScreen.loadScreen = true;
				GameScr.gI().loadGameScr();
				GameScr.isLoadAllData = false;
				Service.gI().updateData();
				if (GameCanvas.currentScreen != GameCanvas.loginScr)
				{
					GameCanvas.serverScreen.switchToMe();
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -70:
			{
				Res.outz("BIG MESSAGE .......................................");
				GameCanvas.endDlg();
				int avatar = (int)msg.reader().readShort();
				ChatPopup.addBigMessage(msg.reader().readUTF(), 100000, new Npc(-1, 0, 0, 0, 0, 0)
				{
					avatar = avatar
				});
				sbyte b23 = msg.reader().readByte();
				if (b23 == 0)
				{
					ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
					ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 35;
					ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
				}
				if (b23 == 1)
				{
					string p = msg.reader().readUTF();
					string caption = msg.reader().readUTF();
					ChatPopup.serverChatPopUp.cmdMsg1 = new Command(caption, ChatPopup.serverChatPopUp, 1000, p);
					ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 75;
					ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
					ChatPopup.serverChatPopUp.cmdMsg2 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
					ChatPopup.serverChatPopUp.cmdMsg2.x = GameCanvas.w / 2 + 11;
					ChatPopup.serverChatPopUp.cmdMsg2.y = GameCanvas.h - 35;
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -69:
				global::Char.myCharz().cMaxStamina = msg.reader().readShort();
				goto IL_8E36;
			case -68:
				global::Char.myCharz().cStamina = (int)msg.reader().readShort();
				goto IL_8E36;
			case -67:
			{
				this.demCount += 1f;
				int num66 = msg.reader().readInt();
				Res.outz("RECIEVE  hinh small: " + num66);
				sbyte[] array3 = null;
				try
				{
					array3 = NinjaUtil.readByteArray(msg);
					Res.outz(">SIZE CHECK= " + array3.Length);
					SmallImage.imgNew[num66].img = this.createImage(array3);
				}
				catch (Exception)
				{
					array3 = null;
					SmallImage.imgNew[num66].img = Image.createRGBImage(new int[1], 1, 1, true);
				}
				if (array3 != null)
				{
					Rms.saveRMS(mGraphics.zoomLevel + "Small" + num66, array3);
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -66:
			{
				int id = (int)msg.reader().readShort();
				sbyte[] data3 = NinjaUtil.readByteArray(msg);
				EffectData effDataById = Effect.getEffDataById(id);
				sbyte b24 = msg.reader().readSByte();
				if (b24 == 0)
				{
					effDataById.readData(data3);
				}
				else
				{
					effDataById.readDataNewBoss(data3, b24);
				}
				sbyte[] array4 = NinjaUtil.readByteArray(msg);
				effDataById.img = Image.createImage(array4, 0, array4.Length);
				goto IL_8E36;
			}
			case -65:
			{
				InfoDlg.hide();
				int num67 = msg.reader().readInt();
				sbyte b25 = msg.reader().readByte();
				if (b25 == 0)
				{
					goto IL_8E36;
				}
				if (global::Char.myCharz().charID == num67)
				{
					GameScr.gI().center = null;
					if (b25 == 0 || b25 == 1 || b25 == 3)
					{
						new Teleport(global::Char.myCharz().cx, global::Char.myCharz().cy, global::Char.myCharz().head, global::Char.myCharz().cdir, 0, true, (b25 != 1) ? ((int)b25) : global::Char.myCharz().cgender);
						goto IL_8E36;
					}
					goto IL_8E36;
				}
				else
				{
					global::Char char6 = GameScr.findCharInMap(num67);
					if (char6 == null)
					{
						goto IL_8E36;
					}
					if (b25 == 0 || b25 == 1 || b25 == 3)
					{
						char6.isUsePlane = true;
						Teleport.addTeleport(new Teleport(char6.cx, char6.cy, char6.head, char6.cdir, 0, false, (b25 != 1) ? ((int)b25) : char6.cgender)
						{
							id = num67,
							Char = char6
						});
					}
					if (b25 == 2)
					{
						char6.hide();
						goto IL_8E36;
					}
					goto IL_8E36;
				}
				break;
			}
			case -64:
			{
				int num68 = msg.reader().readInt();
				int num69 = (int)msg.reader().readShort();
				@char = null;
				@char = ((num68 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num68) : global::Char.myCharz());
				if (@char == null)
				{
					return;
				}
				@char.bag = num69;
				for (int num70 = 0; num70 < 54; num70++)
				{
					@char.removeEffChar(0, 201 + num70);
				}
				if (@char.bag >= 201 && @char.bag < 255)
				{
					@char.addEffChar(new Effect(@char.bag, @char, 2, -1, 10, 1)
					{
						typeEff = 5
					});
				}
				Res.outz(string.Concat(new object[]
				{
					"cmd:-64 UPDATE BAG PLAER = ",
					(@char != null) ? @char.cName : string.Empty,
					num68,
					" BAG ID= ",
					num69
				}));
				if (num69 == 30 && @char.me)
				{
					GameScr.isPickNgocRong = true;
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -63:
			{
				Res.outz("GET BAG");
				int num71 = (int)msg.reader().readShort();
				sbyte b26 = msg.reader().readByte();
				ClanImage clanImage = new ClanImage();
				clanImage.ID = num71;
				if (b26 > 0)
				{
					clanImage.idImage = new short[(int)b26];
					for (int num72 = 0; num72 < (int)b26; num72++)
					{
						clanImage.idImage[num72] = msg.reader().readShort();
						Res.outz(string.Concat(new object[]
						{
							"ID=  ",
							num71,
							" frame= ",
							clanImage.idImage[num72]
						}));
					}
					ClanImage.idImages.put(num71 + string.Empty, clanImage);
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -62:
			{
				int num73 = (int)msg.reader().readUnsignedByte();
				sbyte b27 = msg.reader().readByte();
				if (b27 <= 0)
				{
					goto IL_8E36;
				}
				ClanImage clanImage2 = ClanImage.getClanImage((short)num73);
				if (clanImage2 != null)
				{
					clanImage2.idImage = new short[(int)b27];
					for (int num74 = 0; num74 < (int)b27; num74++)
					{
						clanImage2.idImage[num74] = msg.reader().readShort();
						if (clanImage2.idImage[num74] > 0)
						{
							SmallImage.vKeys.addElement(clanImage2.idImage[num74] + string.Empty);
						}
					}
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -61:
			{
				int num75 = msg.reader().readInt();
				if (num75 != global::Char.myCharz().charID)
				{
					if (GameScr.findCharInMap(num75) == null)
					{
						goto IL_8E36;
					}
					GameScr.findCharInMap(num75).clanID = msg.reader().readInt();
					if (GameScr.findCharInMap(num75).clanID == -2)
					{
						GameScr.findCharInMap(num75).isCopy = true;
						goto IL_8E36;
					}
					goto IL_8E36;
				}
				else
				{
					if (global::Char.myCharz().clan != null)
					{
						global::Char.myCharz().clan.ID = msg.reader().readInt();
						goto IL_8E36;
					}
					goto IL_8E36;
				}
				break;
			}
			case -60:
			{
				GameCanvas.debug("SA7666", 2);
				int num76 = msg.reader().readInt();
				int num77 = -1;
				if (num76 != global::Char.myCharz().charID)
				{
					global::Char char7 = GameScr.findCharInMap(num76);
					if (char7 == null)
					{
						return;
					}
					if (char7.currentMovePoint != null)
					{
						char7.createShadow(char7.cx, char7.cy, 10);
						char7.cx = char7.currentMovePoint.xEnd;
						char7.cy = char7.currentMovePoint.yEnd;
					}
					int num78 = (int)msg.reader().readUnsignedByte();
					if ((TileMap.tileTypeAtPixel(char7.cx, char7.cy) & 2) == 2)
					{
						char7.setSkillPaint(GameScr.sks[num78], 0);
					}
					else
					{
						char7.setSkillPaint(GameScr.sks[num78], 1);
					}
					global::Char[] array5 = new global::Char[(int)msg.reader().readByte()];
					for (i = 0; i < array5.Length; i++)
					{
						num77 = msg.reader().readInt();
						global::Char char8 = array5[i] = ((num77 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num77) : global::Char.myCharz());
						if (i == 0)
						{
							if (char7.cx <= char8.cx)
							{
								char7.cdir = 1;
							}
							else
							{
								char7.cdir = -1;
							}
						}
					}
					if (i > 0)
					{
						char7.attChars = new global::Char[i];
						for (i = 0; i < char7.attChars.Length; i++)
						{
							char7.attChars[i] = array5[i];
						}
						char7.mobFocus = null;
						char7.charFocus = char7.attChars[0];
					}
				}
				else
				{
					msg.reader().readByte();
					msg.reader().readByte();
					num77 = msg.reader().readInt();
				}
				try
				{
					sbyte b28 = msg.reader().readByte();
					Res.outz("isRead continue = " + b28);
					if (b28 == 1)
					{
						sbyte b29 = msg.reader().readByte();
						Res.outz("type skill = " + b29);
						if (num77 == global::Char.myCharz().charID)
						{
							@char = global::Char.myCharz();
							long num79 = msg.reader().readLong();
							Res.outz("dame hit = " + num79);
							@char.isDie = msg.reader().readBoolean();
							if (@char.isDie)
							{
								global::Char.isLockKey = true;
							}
							Res.outz("isDie=" + @char.isDie.ToString() + "---------------------------------------");
							int num80 = 0;
							bool isCrit = @char.isCrit = msg.reader().readBoolean();
							@char.isMob = false;
							num79 = (@char.damHP = num79 + (long)num80);
							if (b29 == 0)
							{
								@char.doInjure(num79, 0L, isCrit, false);
							}
						}
						else
						{
							@char = GameScr.findCharInMap(num77);
							if (@char == null)
							{
								return;
							}
							long num81 = msg.reader().readLong();
							Res.outz("dame hit= " + num81);
							@char.isDie = msg.reader().readBoolean();
							Res.outz("isDie=" + @char.isDie.ToString() + "---------------------------------------");
							int num82 = 0;
							bool isCrit2 = @char.isCrit = msg.reader().readBoolean();
							@char.isMob = false;
							num81 = (@char.damHP = num81 + (long)num82);
							if (b29 == 0)
							{
								@char.doInjure(num81, 0L, isCrit2, false);
							}
						}
					}
					goto IL_8E36;
				}
				catch (Exception)
				{
					goto IL_8E36;
				}
				break;
			}
			case -59:
			{
				sbyte typePK = msg.reader().readByte();
				GameScr.gI().player_vs_player(msg.reader().readInt(), msg.reader().readInt(), msg.reader().readUTF(), typePK);
				goto IL_8E36;
			}
			case -57:
				break;
			case -53:
			{
				InfoDlg.hide();
				bool flag3 = false;
				int num83 = msg.reader().readInt();
				Res.outz("clanId= " + num83);
				if (num83 == -1)
				{
					global::Char.myCharz().clan = null;
					ClanMessage.vMessage.removeAllElements();
					if (GameCanvas.panel.member != null)
					{
						GameCanvas.panel.member.removeAllElements();
					}
					if (GameCanvas.panel.myMember != null)
					{
						GameCanvas.panel.myMember.removeAllElements();
					}
					if (GameCanvas.currentScreen == GameScr.gI())
					{
						GameCanvas.panel.setTabClans();
					}
					return;
				}
				GameCanvas.panel.tabIcon = null;
				if (global::Char.myCharz().clan == null)
				{
					global::Char.myCharz().clan = new Clan();
				}
				global::Char.myCharz().clan.ID = num83;
				global::Char.myCharz().clan.name = msg.reader().readUTF();
				global::Char.myCharz().clan.slogan = msg.reader().readUTF();
				global::Char.myCharz().clan.imgID = (int)msg.reader().readShort();
				global::Char.myCharz().clan.powerPoint = msg.reader().readUTF();
				global::Char.myCharz().clan.leaderName = msg.reader().readUTF();
				global::Char.myCharz().clan.currMember = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().clan.maxMember = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().role = msg.reader().readByte();
				global::Char.myCharz().clan.clanPoint = msg.reader().readInt();
				global::Char.myCharz().clan.level = (int)msg.reader().readByte();
				GameCanvas.panel.myMember = new MyVector();
				for (int num84 = 0; num84 < global::Char.myCharz().clan.currMember; num84++)
				{
					Member member = new Member();
					member.ID = msg.reader().readInt();
					member.head = msg.reader().readShort();
					member.headICON = msg.reader().readShort();
					member.leg = msg.reader().readShort();
					member.body = msg.reader().readShort();
					member.name = msg.reader().readUTF();
					member.role = msg.reader().readByte();
					member.powerPoint = msg.reader().readUTF();
					member.donate = msg.reader().readInt();
					member.receive_donate = msg.reader().readInt();
					member.clanPoint = msg.reader().readInt();
					member.curClanPoint = msg.reader().readInt();
					member.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					GameCanvas.panel.myMember.addElement(member);
				}
				int num85 = (int)msg.reader().readUnsignedByte();
				for (int num86 = 0; num86 < num85; num86++)
				{
					this.readClanMsg(msg, -1);
				}
				if (GameCanvas.panel.isSearchClan || GameCanvas.panel.isViewMember || GameCanvas.panel.isMessage)
				{
					GameCanvas.panel.setTabClans();
				}
				if (flag3)
				{
					GameCanvas.panel.setTabClans();
				}
				Res.outz("=>>>>>>>>>>>>>>>>>>>>>> -537 MY CLAN INFO");
				goto IL_8E36;
			}
			case -52:
			{
				sbyte b30 = msg.reader().readByte();
				if (b30 == 0)
				{
					Member member2 = new Member();
					member2.ID = msg.reader().readInt();
					member2.head = msg.reader().readShort();
					member2.headICON = msg.reader().readShort();
					member2.leg = msg.reader().readShort();
					member2.body = msg.reader().readShort();
					member2.name = msg.reader().readUTF();
					member2.role = msg.reader().readByte();
					member2.powerPoint = msg.reader().readUTF();
					member2.donate = msg.reader().readInt();
					member2.receive_donate = msg.reader().readInt();
					member2.clanPoint = msg.reader().readInt();
					member2.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					if (GameCanvas.panel.myMember == null)
					{
						GameCanvas.panel.myMember = new MyVector();
					}
					GameCanvas.panel.myMember.addElement(member2);
					GameCanvas.panel.initTabClans();
				}
				if (b30 == 1)
				{
					GameCanvas.panel.myMember.removeElementAt((int)msg.reader().readByte());
					GameCanvas.panel.currentListLength--;
					GameCanvas.panel.initTabClans();
				}
				if (b30 == 2)
				{
					Member member3 = new Member();
					member3.ID = msg.reader().readInt();
					member3.head = msg.reader().readShort();
					member3.headICON = msg.reader().readShort();
					member3.leg = msg.reader().readShort();
					member3.body = msg.reader().readShort();
					member3.name = msg.reader().readUTF();
					member3.role = msg.reader().readByte();
					member3.powerPoint = msg.reader().readUTF();
					member3.donate = msg.reader().readInt();
					member3.receive_donate = msg.reader().readInt();
					member3.clanPoint = msg.reader().readInt();
					member3.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					for (int num87 = 0; num87 < GameCanvas.panel.myMember.size(); num87++)
					{
						Member member4 = (Member)GameCanvas.panel.myMember.elementAt(num87);
						if (member4.ID == member3.ID)
						{
							if (global::Char.myCharz().charID == member3.ID)
							{
								global::Char.myCharz().role = member3.role;
							}
							Member o = member3;
							GameCanvas.panel.myMember.removeElement(member4);
							GameCanvas.panel.myMember.insertElementAt(o, num87);
							return;
						}
					}
				}
				Res.outz("=>>>>>>>>>>>>>>>>>>>>>> -52  MY CLAN UPDSTE");
				goto IL_8E36;
			}
			case -51:
				InfoDlg.hide();
				this.readClanMsg(msg, 0);
				if (GameCanvas.panel.isMessage && GameCanvas.panel.type == 5)
				{
					GameCanvas.panel.initTabClans();
					goto IL_8E36;
				}
				goto IL_8E36;
			case -50:
			{
				InfoDlg.hide();
				GameCanvas.panel.member = new MyVector();
				sbyte b31 = msg.reader().readByte();
				for (int num88 = 0; num88 < (int)b31; num88++)
				{
					Member member5 = new Member();
					member5.ID = msg.reader().readInt();
					member5.head = msg.reader().readShort();
					member5.headICON = msg.reader().readShort();
					member5.leg = msg.reader().readShort();
					member5.body = msg.reader().readShort();
					member5.name = msg.reader().readUTF();
					member5.role = msg.reader().readByte();
					member5.powerPoint = msg.reader().readUTF();
					member5.donate = msg.reader().readInt();
					member5.receive_donate = msg.reader().readInt();
					member5.clanPoint = msg.reader().readInt();
					member5.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					GameCanvas.panel.member.addElement(member5);
				}
				GameCanvas.panel.isViewMember = true;
				GameCanvas.panel.isSearchClan = false;
				GameCanvas.panel.isMessage = false;
				GameCanvas.panel.currentListLength = GameCanvas.panel.member.size() + 2;
				GameCanvas.panel.initTabClans();
				goto IL_8E36;
			}
			case -47:
			{
				InfoDlg.hide();
				sbyte b32 = msg.reader().readByte();
				Res.outz("clan = " + b32);
				if (b32 == 0)
				{
					GameCanvas.panel.clanReport = mResources.cannot_find_clan;
					GameCanvas.panel.clans = null;
				}
				else
				{
					GameCanvas.panel.clans = new Clan[(int)b32];
					Res.outz("clan search lent= " + GameCanvas.panel.clans.Length);
					for (int num89 = 0; num89 < GameCanvas.panel.clans.Length; num89++)
					{
						GameCanvas.panel.clans[num89] = new Clan();
						GameCanvas.panel.clans[num89].ID = msg.reader().readInt();
						GameCanvas.panel.clans[num89].name = msg.reader().readUTF();
						GameCanvas.panel.clans[num89].slogan = msg.reader().readUTF();
						GameCanvas.panel.clans[num89].imgID = (int)msg.reader().readShort();
						GameCanvas.panel.clans[num89].powerPoint = msg.reader().readUTF();
						GameCanvas.panel.clans[num89].leaderName = msg.reader().readUTF();
						GameCanvas.panel.clans[num89].currMember = (int)msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num89].maxMember = (int)msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num89].date = msg.reader().readInt();
					}
				}
				GameCanvas.panel.isSearchClan = true;
				GameCanvas.panel.isViewMember = false;
				GameCanvas.panel.isMessage = false;
				if (GameCanvas.panel.isSearchClan)
				{
					GameCanvas.panel.initTabClans();
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -46:
			{
				InfoDlg.hide();
				sbyte b33 = msg.reader().readByte();
				if (b33 == 1 || b33 == 3)
				{
					GameCanvas.endDlg();
					ClanImage.vClanImage.removeAllElements();
					int num90 = (int)msg.reader().readShort();
					for (int num91 = 0; num91 < num90; num91++)
					{
						ClanImage clanImage3 = new ClanImage();
						clanImage3.ID = (int)msg.reader().readShort();
						clanImage3.name = msg.reader().readUTF();
						clanImage3.xu = msg.reader().readInt();
						clanImage3.luong = msg.reader().readInt();
						if (!ClanImage.isExistClanImage(clanImage3.ID))
						{
							ClanImage.addClanImage(clanImage3);
						}
						else
						{
							ClanImage.getClanImage((short)clanImage3.ID).name = clanImage3.name;
							ClanImage.getClanImage((short)clanImage3.ID).xu = clanImage3.xu;
							ClanImage.getClanImage((short)clanImage3.ID).luong = clanImage3.luong;
						}
					}
					if (global::Char.myCharz().clan != null)
					{
						GameCanvas.panel.changeIcon();
					}
				}
				if (b33 == 4)
				{
					global::Char.myCharz().clan.imgID = (int)msg.reader().readShort();
					global::Char.myCharz().clan.slogan = msg.reader().readUTF();
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -45:
			{
				sbyte b34 = msg.reader().readByte();
				int num92 = msg.reader().readInt();
				short num93 = msg.reader().readShort();
				Res.outz(string.Concat(new object[]
				{
					">.SKILL_NOT_FOCUS      skillNotFocusID: ",
					num93,
					" skill type= ",
					b34,
					"   player use= ",
					num92
				}));
				if (b34 == 20)
				{
					sbyte b35 = msg.reader().readByte();
					sbyte dir = msg.reader().readByte();
					short timeGong = msg.reader().readShort();
					bool isFly = msg.reader().readByte() != 0;
					sbyte typePaint = msg.reader().readByte();
					sbyte typeItem = -1;
					try
					{
						typeItem = msg.reader().readByte();
					}
					catch (Exception)
					{
					}
					Res.outz(">.SKILL_NOT_FOCUS  skill typeFrame= " + b35);
					@char = ((global::Char.myCharz().charID != num92) ? GameScr.findCharInMap(num92) : global::Char.myCharz());
					@char.SetSkillPaint_NEW(num93, isFly, b35, typePaint, dir, timeGong, typeItem);
				}
				if (b34 == 21)
				{
					Point point = new Point();
					point.x = (int)msg.reader().readShort();
					point.y = (int)msg.reader().readShort();
					short timeDame = msg.reader().readShort();
					short rangeDame = msg.reader().readShort();
					sbyte typePaint2 = 0;
					sbyte typeItem2 = -1;
					Point[] array6 = null;
					@char = ((global::Char.myCharz().charID != num92) ? GameScr.findCharInMap(num92) : global::Char.myCharz());
					try
					{
						typePaint2 = msg.reader().readByte();
						sbyte b36 = msg.reader().readByte();
						if (b36 > 0)
						{
							array6 = new Point[(int)b36];
							for (int num94 = 0; num94 < array6.Length; num94++)
							{
								array6[num94] = new Point();
								array6[num94].type = msg.reader().readByte();
								if (array6[num94].type == 0)
								{
									array6[num94].id = (int)msg.reader().readByte();
								}
								else
								{
									array6[num94].id = msg.reader().readInt();
								}
							}
						}
					}
					catch (Exception)
					{
					}
					try
					{
						typeItem2 = msg.reader().readByte();
					}
					catch (Exception)
					{
					}
					Res.outz(string.Concat(new object[]
					{
						">.SKILL_NOT_FOCUS  skill targetDame= ",
						point.x,
						":",
						point.y,
						"    c:",
						@char.cx,
						":",
						@char.cy,
						"   cdir:",
						@char.cdir
					}));
					@char.SetSkillPaint_STT(1, num93, point, timeDame, rangeDame, typePaint2, array6, typeItem2);
				}
				if (b34 == 0)
				{
					Res.outz("id use= " + num92);
					if (global::Char.myCharz().charID != num92)
					{
						@char = GameScr.findCharInMap(num92);
						if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
						{
							@char.setSkillPaint(GameScr.sks[(int)num93], 0);
						}
						else
						{
							@char.setSkillPaint(GameScr.sks[(int)num93], 1);
							@char.delayFall = 20;
						}
					}
					else
					{
						global::Char.myCharz().saveLoadPreviousSkill();
						Res.outz("LOAD LAST SKILL");
					}
					sbyte b37 = msg.reader().readByte();
					Res.outz("npc size= " + b37);
					for (int num95 = 0; num95 < (int)b37; num95++)
					{
						sbyte b38 = msg.reader().readByte();
						sbyte seconds = msg.reader().readByte();
						Res.outz("index= " + b38);
						if (num93 >= 42 && num93 <= 48)
						{
							((Mob)GameScr.vMob.elementAt((int)b38)).isFreez = true;
							((Mob)GameScr.vMob.elementAt((int)b38)).seconds = (int)seconds;
							((Mob)GameScr.vMob.elementAt((int)b38)).last = (((Mob)GameScr.vMob.elementAt((int)b38)).cur = mSystem.currentTimeMillis());
						}
					}
					sbyte b39 = msg.reader().readByte();
					for (int num96 = 0; num96 < (int)b39; num96++)
					{
						int num97 = msg.reader().readInt();
						sbyte b40 = msg.reader().readByte();
						Res.outz(string.Concat(new object[]
						{
							"player ID= ",
							num97,
							" my ID= ",
							global::Char.myCharz().charID
						}));
						if (num93 >= 42 && num93 <= 48)
						{
							if (num97 == global::Char.myCharz().charID)
							{
								if (!global::Char.myCharz().isFlyAndCharge && !global::Char.myCharz().isStandAndCharge)
								{
									GameScr.gI().isFreez = true;
									global::Char.myCharz().isFreez = true;
									global::Char.myCharz().freezSeconds = (int)b40;
									global::Char.myCharz().lastFreez = (global::Char.myCharz().currFreez = mSystem.currentTimeMillis());
									global::Char.myCharz().isLockMove = true;
								}
							}
							else
							{
								@char = GameScr.findCharInMap(num97);
								if (@char != null && !@char.isFlyAndCharge && !@char.isStandAndCharge)
								{
									@char.isFreez = true;
									@char.seconds = (int)b40;
									@char.freezSeconds = (int)b40;
									@char.lastFreez = (GameScr.findCharInMap(num97).currFreez = mSystem.currentTimeMillis());
								}
							}
						}
					}
				}
				if (b34 == 1 && num92 != global::Char.myCharz().charID)
				{
					try
					{
						GameScr.findCharInMap(num92).isCharge = true;
					}
					catch (Exception)
					{
					}
				}
				if (b34 == 3)
				{
					if (num92 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().isCharge = false;
						SoundMn.gI().taitaoPause();
						global::Char.myCharz().saveLoadPreviousSkill();
					}
					else
					{
						GameScr.findCharInMap(num92).isCharge = false;
					}
				}
				if (b34 == 4)
				{
					if (num92 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().seconds = (int)(msg.reader().readShort() - 1000);
						global::Char.myCharz().last = mSystem.currentTimeMillis();
						Res.outz(string.Concat(new object[]
						{
							"second= ",
							global::Char.myCharz().seconds,
							" last= ",
							global::Char.myCharz().last
						}));
					}
					else if (GameScr.findCharInMap(num92) != null)
					{
						int cgender = GameScr.findCharInMap(num92).cgender;
						if (cgender != 0)
						{
							if (cgender != 1)
							{
								if (TileMap.mapID == 170)
								{
									bool isGround = true;
									if (num93 >= 70 && num93 <= 76)
									{
										isGround = false;
									}
									if (num93 >= 77 && num93 <= 83)
									{
										isGround = true;
									}
									@char.useChargeSkill(isGround);
								}
							}
							else if (TileMap.mapID != 170)
							{
								@char.useChargeSkill(true);
							}
							else
							{
								bool isGround2 = true;
								if (num93 >= 70 && num93 <= 76)
								{
									isGround2 = false;
								}
								if (num93 >= 77 && num93 <= 83)
								{
									isGround2 = true;
								}
								@char.useChargeSkill(isGround2);
							}
						}
						else if (TileMap.mapID != 170)
						{
							@char.useChargeSkill(false);
						}
						else
						{
							if (num93 >= 77 && num93 <= 83)
							{
								@char.useChargeSkill(true);
							}
							if (num93 >= 70 && num93 <= 76)
							{
								@char.useChargeSkill(false);
							}
						}
						@char.skillTemplateId = (int)num93;
						if (num93 >= 70 && num93 <= 76)
						{
							@char.isUseSkillAfterCharge = true;
						}
						@char.seconds = (int)msg.reader().readShort();
						@char.last = mSystem.currentTimeMillis();
					}
				}
				if (b34 == 5)
				{
					if (num92 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().stopUseChargeSkill();
					}
					else if (GameScr.findCharInMap(num92) != null)
					{
						GameScr.findCharInMap(num92).stopUseChargeSkill();
					}
				}
				if (b34 == 6)
				{
					if (num92 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().setAutoSkillPaint(GameScr.sks[(int)num93], 0);
					}
					else if (GameScr.findCharInMap(num92) != null)
					{
						GameScr.findCharInMap(num92).setAutoSkillPaint(GameScr.sks[(int)num93], 0);
						SoundMn.gI().gong();
					}
				}
				if (b34 == 7)
				{
					if (num92 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().seconds = (int)msg.reader().readShort();
						Res.outz("second = " + global::Char.myCharz().seconds);
						global::Char.myCharz().last = mSystem.currentTimeMillis();
					}
					else if (GameScr.findCharInMap(num92) != null)
					{
						GameScr.findCharInMap(num92).useChargeSkill(true);
						GameScr.findCharInMap(num92).seconds = (int)msg.reader().readShort();
						GameScr.findCharInMap(num92).last = mSystem.currentTimeMillis();
						SoundMn.gI().gong();
					}
				}
				if (b34 == 8 && num92 != global::Char.myCharz().charID && GameScr.findCharInMap(num92) != null)
				{
					GameScr.findCharInMap(num92).setAutoSkillPaint(GameScr.sks[(int)num93], 0);
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -44:
			{
				bool flag4 = false;
				if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
				{
					flag4 = true;
				}
				sbyte b41 = msg.reader().readByte();
				int num98 = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().arrItemShop = new Item[num98][];
				GameCanvas.panel.shopTabName = new string[num98 + ((!flag4) ? 1 : 0)][];
				for (int num99 = 0; num99 < GameCanvas.panel.shopTabName.Length; num99++)
				{
					GameCanvas.panel.shopTabName[num99] = new string[2];
				}
				if (b41 == 2)
				{
					GameCanvas.panel.maxPageShop = new int[num98];
					GameCanvas.panel.currPageShop = new int[num98];
				}
				if (!flag4)
				{
					GameCanvas.panel.shopTabName[num98] = mResources.inventory;
				}
				for (int num100 = 0; num100 < num98; num100++)
				{
					string[] array7 = Res.split(msg.reader().readUTF(), "\n", 0);
					if (b41 == 2)
					{
						GameCanvas.panel.maxPageShop[num100] = (int)msg.reader().readUnsignedByte();
					}
					if (array7.Length == 2)
					{
						GameCanvas.panel.shopTabName[num100] = array7;
					}
					if (array7.Length == 1)
					{
						GameCanvas.panel.shopTabName[num100][0] = array7[0];
						GameCanvas.panel.shopTabName[num100][1] = string.Empty;
					}
					int num101 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemShop[num100] = new Item[num101];
					Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
					if (b41 == 1)
					{
						Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy2;
					}
					for (int num102 = 0; num102 < num101; num102++)
					{
						short num103 = msg.reader().readShort();
						if (num103 != -1)
						{
							global::Char.myCharz().arrItemShop[num100][num102] = new Item();
							global::Char.myCharz().arrItemShop[num100][num102].template = ItemTemplates.get(num103);
							switch (b41)
							{
							case 0:
								global::Char.myCharz().arrItemShop[num100][num102].buyCoin = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num100][num102].buyGold = msg.reader().readInt();
								break;
							case 1:
								global::Char.myCharz().arrItemShop[num100][num102].powerRequire = msg.reader().readLong();
								break;
							case 2:
								global::Char.myCharz().arrItemShop[num100][num102].itemId = (int)msg.reader().readShort();
								global::Char.myCharz().arrItemShop[num100][num102].buyCoin = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num100][num102].buyGold = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num100][num102].buyType = msg.reader().readByte();
								global::Char.myCharz().arrItemShop[num100][num102].quantity = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num100][num102].isMe = msg.reader().readByte();
								break;
							case 3:
								global::Char.myCharz().arrItemShop[num100][num102].isBuySpec = true;
								global::Char.myCharz().arrItemShop[num100][num102].iconSpec = msg.reader().readShort();
								global::Char.myCharz().arrItemShop[num100][num102].buySpec = msg.reader().readInt();
								break;
							case 4:
								global::Char.myCharz().arrItemShop[num100][num102].reason = msg.reader().readUTF();
								break;
							case 8:
								global::Char.myCharz().arrItemShop[num100][num102].buyCoin = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num100][num102].buyGold = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num100][num102].quantity = msg.reader().readInt();
								break;
							}
							int num104 = (int)msg.reader().readUnsignedByte();
							if (num104 != 0)
							{
								global::Char.myCharz().arrItemShop[num100][num102].itemOption = new ItemOption[num104];
								for (int num105 = 0; num105 < global::Char.myCharz().arrItemShop[num100][num102].itemOption.Length; num105++)
								{
									ItemOption itemOption3 = this.readItemOption(msg);
									if (itemOption3 != null)
									{
										global::Char.myCharz().arrItemShop[num100][num102].itemOption[num105] = itemOption3;
										global::Char.myCharz().arrItemShop[num100][num102].compare = GameCanvas.panel.getCompare(global::Char.myCharz().arrItemShop[num100][num102]);
									}
								}
							}
							sbyte b42 = msg.reader().readByte();
							global::Char.myCharz().arrItemShop[num100][num102].newItem = (b42 != 0);
							if (msg.reader().readByte() == 1)
							{
								int headTemp = (int)msg.reader().readShort();
								int bodyTemp = (int)msg.reader().readShort();
								int legTemp = (int)msg.reader().readShort();
								int bagTemp = (int)msg.reader().readShort();
								global::Char.myCharz().arrItemShop[num100][num102].setPartTemp(headTemp, bodyTemp, legTemp, bagTemp);
							}
							if (b41 == 2 && GameMidlet.intVERSION >= 237)
							{
								global::Char.myCharz().arrItemShop[num100][num102].nameNguoiKyGui = msg.reader().readUTF();
								Res.err("nguoi ki gui  " + global::Char.myCharz().arrItemShop[num100][num102].nameNguoiKyGui);
							}
						}
					}
				}
				if (flag4)
				{
					if (b41 != 2)
					{
						GameCanvas.panel2 = new Panel();
						GameCanvas.panel2.tabName[7] = new string[][]
						{
							new string[]
							{
								string.Empty
							}
						};
						GameCanvas.panel2.setTypeBodyOnly();
						GameCanvas.panel2.show();
					}
					else
					{
						GameCanvas.panel2 = new Panel();
						GameCanvas.panel2.setTypeKiGuiOnly();
						GameCanvas.panel2.show();
					}
				}
				GameCanvas.panel.tabName[1] = GameCanvas.panel.shopTabName;
				if (b41 == 2)
				{
					string[][] array8 = GameCanvas.panel.tabName[1];
					if (flag4)
					{
						GameCanvas.panel.tabName[1] = new string[][]
						{
							array8[0],
							array8[1],
							array8[2],
							array8[3]
						};
					}
					else
					{
						GameCanvas.panel.tabName[1] = new string[][]
						{
							array8[0],
							array8[1],
							array8[2],
							array8[3],
							array8[4]
						};
					}
				}
				GameCanvas.panel.setTypeShop((int)b41);
				GameCanvas.panel.show();
				goto IL_8E36;
			}
			case -43:
			{
				sbyte itemAction = msg.reader().readByte();
				sbyte where = msg.reader().readByte();
				sbyte index2 = msg.reader().readByte();
				string info2 = msg.reader().readUTF();
				GameCanvas.panel.itemRequest(itemAction, info2, where, index2);
				goto IL_8E36;
			}
			case -42:
				global::Char.myCharz().cHPGoc = msg.readInt3Byte();
				global::Char.myCharz().cMPGoc = msg.readInt3Byte();
				global::Char.myCharz().cDamGoc = msg.reader().readInt();
				global::Char.myCharz().cHPFull = msg.reader().readLong();
				global::Char.myCharz().cMPFull = msg.reader().readLong();
				global::Char.myCharz().cHP = msg.reader().readLong();
				global::Char.myCharz().cMP = msg.reader().readLong();
				global::Char.myCharz().cspeed = (int)msg.reader().readByte();
				global::Char.myCharz().hpFrom1000TiemNang = msg.reader().readByte();
				global::Char.myCharz().mpFrom1000TiemNang = msg.reader().readByte();
				global::Char.myCharz().damFrom1000TiemNang = msg.reader().readByte();
				global::Char.myCharz().cDamFull = msg.reader().readLong();
				global::Char.myCharz().cDefull = msg.reader().readLong();
				global::Char.myCharz().cCriticalFull = (int)msg.reader().readByte();
				global::Char.myCharz().cTiemNang = msg.reader().readLong();
				global::Char.myCharz().expForOneAdd = msg.reader().readShort();
				global::Char.myCharz().cDefGoc = msg.reader().readInt();
				global::Char.myCharz().cCriticalGoc = (int)msg.reader().readByte();
				global::Char.myCharz().cGiamST = (long)msg.reader().readByte();
				global::Char.myCharz().cCritDameFull = (int)msg.reader().readShort();
				InfoDlg.hide();
				goto IL_8E36;
			case -41:
			{
				sbyte b43 = msg.reader().readByte();
				global::Char.myCharz().strLevel = new string[(int)b43];
				for (int num106 = 0; num106 < (int)b43; num106++)
				{
					string text3 = msg.reader().readUTF();
					global::Char.myCharz().strLevel[num106] = text3;
				}
				Res.outz("---   xong  level caption cmd : " + msg.command);
				goto IL_8E36;
			}
			case -37:
			{
				sbyte b44 = msg.reader().readByte();
				Res.outz("cAction= " + b44);
				if (b44 == 0)
				{
					global::Char.myCharz().head = (int)msg.reader().readShort();
					global::Char.myCharz().setDefaultPart();
					int num107 = (int)msg.reader().readUnsignedByte();
					Res.outz("num body = " + num107);
					global::Char.myCharz().arrItemBody = new Item[num107];
					for (int num108 = 0; num108 < num107; num108++)
					{
						short num109 = msg.reader().readShort();
						if (num109 != -1)
						{
							global::Char.myCharz().arrItemBody[num108] = new Item();
							global::Char.myCharz().arrItemBody[num108].template = ItemTemplates.get(num109);
							int type2 = (int)global::Char.myCharz().arrItemBody[num108].template.type;
							global::Char.myCharz().arrItemBody[num108].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBody[num108].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBody[num108].content = msg.reader().readUTF();
							int num110 = (int)msg.reader().readUnsignedByte();
							if (num110 != 0)
							{
								global::Char.myCharz().arrItemBody[num108].itemOption = new ItemOption[num110];
								for (int num111 = 0; num111 < global::Char.myCharz().arrItemBody[num108].itemOption.Length; num111++)
								{
									ItemOption itemOption4 = this.readItemOption(msg);
									if (itemOption4 != null)
									{
										global::Char.myCharz().arrItemBody[num108].itemOption[num111] = itemOption4;
									}
								}
							}
							if (type2 != 0)
							{
								if (type2 == 1)
								{
									global::Char.myCharz().leg = (int)global::Char.myCharz().arrItemBody[num108].template.part;
								}
							}
							else
							{
								global::Char.myCharz().body = (int)global::Char.myCharz().arrItemBody[num108].template.part;
							}
						}
					}
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -36:
			{
				sbyte b45 = msg.reader().readByte();
				Res.outz("cAction= " + b45);
				GameScr.isudungCapsun4 = false;
				GameScr.isudungCapsun3 = false;
				if (b45 == 0)
				{
					int num112 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemBag = new Item[num112];
					GameScr.hpPotion = 0;
					Res.outz("numC=" + num112);
					for (int num113 = 0; num113 < num112; num113++)
					{
						short num114 = msg.reader().readShort();
						if (num114 != -1)
						{
							global::Char.myCharz().arrItemBag[num113] = new Item();
							global::Char.myCharz().arrItemBag[num113].template = ItemTemplates.get(num114);
							global::Char.myCharz().arrItemBag[num113].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBag[num113].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBag[num113].content = msg.reader().readUTF();
							global::Char.myCharz().arrItemBag[num113].indexUI = num113;
							int num115 = (int)msg.reader().readUnsignedByte();
							if (num115 != 0)
							{
								global::Char.myCharz().arrItemBag[num113].itemOption = new ItemOption[num115];
								for (int num116 = 0; num116 < global::Char.myCharz().arrItemBag[num113].itemOption.Length; num116++)
								{
									ItemOption itemOption5 = this.readItemOption(msg);
									if (itemOption5 != null)
									{
										global::Char.myCharz().arrItemBag[num113].itemOption[num116] = itemOption5;
									}
								}
								global::Char.myCharz().arrItemBag[num113].compare = GameCanvas.panel.getCompare(global::Char.myCharz().arrItemBag[num113]);
							}
							sbyte type3 = global::Char.myCharz().arrItemBag[num113].template.type;
							if (global::Char.myCharz().arrItemBag[num113].template.type == 6)
							{
								GameScr.hpPotion += global::Char.myCharz().arrItemBag[num113].quantity;
							}
							if (global::Char.myCharz().arrItemBag[num113].template.id == 194)
							{
								GameScr.isudungCapsun4 = (global::Char.myCharz().arrItemBag[num113].quantity > 0);
							}
							else if (global::Char.myCharz().arrItemBag[num113].template.id == 193 && !GameScr.isudungCapsun4)
							{
								GameScr.isudungCapsun3 = (global::Char.myCharz().arrItemBag[num113].quantity > 0);
							}
						}
					}
				}
				if (b45 != 2)
				{
					goto IL_8E36;
				}
				sbyte b46 = msg.reader().readByte();
				int num117 = msg.reader().readInt();
				int quantity = global::Char.myCharz().arrItemBag[(int)b46].quantity;
				int id2 = (int)global::Char.myCharz().arrItemBag[(int)b46].template.id;
				global::Char.myCharz().arrItemBag[(int)b46].quantity = num117;
				if (global::Char.myCharz().arrItemBag[(int)b46].quantity < quantity && global::Char.myCharz().arrItemBag[(int)b46].template.type == 6)
				{
					GameScr.hpPotion -= quantity - global::Char.myCharz().arrItemBag[(int)b46].quantity;
				}
				if (global::Char.myCharz().arrItemBag[(int)b46].quantity == 0)
				{
					global::Char.myCharz().arrItemBag[(int)b46] = null;
				}
				if (id2 == 193)
				{
					GameScr.isudungCapsun3 = (num117 > 0);
					goto IL_8E36;
				}
				if (id2 == 194)
				{
					GameScr.isudungCapsun4 = (num117 > 0);
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -35:
			{
				sbyte b47 = msg.reader().readByte();
				Res.outz("cAction= " + b47);
				if (b47 == 0)
				{
					int num118 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemBox = new Item[num118];
					GameCanvas.panel.hasUse = 0;
					for (int num119 = 0; num119 < num118; num119++)
					{
						short num120 = msg.reader().readShort();
						if (num120 != -1)
						{
							global::Char.myCharz().arrItemBox[num119] = new Item();
							global::Char.myCharz().arrItemBox[num119].template = ItemTemplates.get(num120);
							global::Char.myCharz().arrItemBox[num119].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBox[num119].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBox[num119].content = msg.reader().readUTF();
							int num121 = (int)msg.reader().readUnsignedByte();
							if (num121 != 0)
							{
								global::Char.myCharz().arrItemBox[num119].itemOption = new ItemOption[num121];
								for (int num122 = 0; num122 < global::Char.myCharz().arrItemBox[num119].itemOption.Length; num122++)
								{
									ItemOption itemOption6 = this.readItemOption(msg);
									if (itemOption6 != null)
									{
										global::Char.myCharz().arrItemBox[num119].itemOption[num122] = itemOption6;
									}
								}
							}
							GameCanvas.panel.hasUse++;
						}
					}
				}
				if (b47 == 1)
				{
					bool isBoxClan = false;
					try
					{
						if (msg.reader().readByte() == 1)
						{
							isBoxClan = true;
						}
					}
					catch (Exception)
					{
					}
					GameCanvas.panel.setTypeBox();
					GameCanvas.panel.isBoxClan = isBoxClan;
					GameCanvas.panel.show();
				}
				if (b47 != 2)
				{
					goto IL_8E36;
				}
				sbyte b48 = msg.reader().readByte();
				int quantity2 = msg.reader().readInt();
				global::Char.myCharz().arrItemBox[(int)b48].quantity = quantity2;
				if (global::Char.myCharz().arrItemBox[(int)b48].quantity == 0)
				{
					global::Char.myCharz().arrItemBox[(int)b48] = null;
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -34:
			{
				sbyte b49 = msg.reader().readByte();
				Res.outz("act= " + b49);
				if (b49 == 0 && GameScr.gI().magicTree != null)
				{
					Res.outz("toi duoc day");
					MagicTree magicTree = GameScr.gI().magicTree;
					magicTree.id = (int)msg.reader().readShort();
					magicTree.name = msg.reader().readUTF();
					magicTree.name = Res.changeString(magicTree.name);
					magicTree.x = (int)msg.reader().readShort();
					magicTree.y = (int)msg.reader().readShort();
					magicTree.level = (int)msg.reader().readByte();
					magicTree.currPeas = (int)msg.reader().readShort();
					magicTree.maxPeas = (int)msg.reader().readShort();
					Res.outz("curr Peas= " + magicTree.currPeas);
					magicTree.strInfo = msg.reader().readUTF();
					magicTree.seconds = msg.reader().readInt();
					magicTree.timeToRecieve = magicTree.seconds;
					sbyte b50 = msg.reader().readByte();
					magicTree.peaPostionX = new int[(int)b50];
					magicTree.peaPostionY = new int[(int)b50];
					for (int num123 = 0; num123 < (int)b50; num123++)
					{
						magicTree.peaPostionX[num123] = (int)msg.reader().readByte();
						magicTree.peaPostionY[num123] = (int)msg.reader().readByte();
					}
					magicTree.isUpdate = msg.reader().readBool();
					magicTree.last = (magicTree.cur = mSystem.currentTimeMillis());
					GameScr.gI().magicTree.isUpdateTree = true;
				}
				if (b49 == 1)
				{
					myVector = new MyVector();
					try
					{
						while (msg.reader().available() > 0)
						{
							string caption2 = msg.reader().readUTF();
							myVector.addElement(new Command(caption2, GameCanvas.instance, 888392, null));
						}
					}
					catch (Exception ex)
					{
						Cout.println("Loi MAGIC_TREE " + ex.ToString());
					}
					GameCanvas.menu.startAt(myVector, 3);
				}
				if (b49 == 2)
				{
					GameScr.gI().magicTree.remainPeas = (int)msg.reader().readShort();
					GameScr.gI().magicTree.seconds = msg.reader().readInt();
					GameScr.gI().magicTree.last = (GameScr.gI().magicTree.cur = mSystem.currentTimeMillis());
					GameScr.gI().magicTree.isUpdateTree = true;
					GameScr.gI().magicTree.isPeasEffect = true;
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -32:
			{
				short num124 = msg.reader().readShort();
				int num125 = msg.reader().readInt();
				sbyte[] array9 = null;
				Image image = null;
				try
				{
					array9 = new sbyte[num125];
					for (int num126 = 0; num126 < num125; num126++)
					{
						array9[num126] = msg.reader().readByte();
					}
					image = Image.createImage(array9, 0, num125);
					BgItem.imgNew.put(num124 + string.Empty, image);
				}
				catch (Exception)
				{
					array9 = null;
					BgItem.imgNew.put(num124 + string.Empty, Image.createRGBImage(new int[1], 1, 1, true));
				}
				if (array9 != null)
				{
					if (mGraphics.zoomLevel > 1)
					{
						Rms.saveRMS(mGraphics.zoomLevel + "bgItem" + num124, array9);
					}
					BgItemMn.blendcurrBg(num124, image);
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case -31:
			{
				TileMap.vItemBg.removeAllElements();
				short num127 = msg.reader().readShort();
				Res.err("[ITEM_BACKGROUND] nItem= " + num127);
				for (int num128 = 0; num128 < (int)num127; num128++)
				{
					BgItem bgItem = new BgItem();
					bgItem.id = num128;
					bgItem.idImage = msg.reader().readShort();
					bgItem.layer = msg.reader().readByte();
					bgItem.dx = (int)msg.reader().readShort();
					bgItem.dy = (int)msg.reader().readShort();
					sbyte b51 = msg.reader().readByte();
					bgItem.tileX = new int[(int)b51];
					bgItem.tileY = new int[(int)b51];
					for (int num129 = 0; num129 < (int)b51; num129++)
					{
						bgItem.tileX[num128] = (int)msg.reader().readByte();
						bgItem.tileY[num128] = (int)msg.reader().readByte();
					}
					TileMap.vItemBg.addElement(bgItem);
				}
				goto IL_8E36;
			}
			case -30:
				this.messageSubCommand(msg);
				goto IL_8E36;
			case -29:
				this.messageNotLogin(msg);
				goto IL_8E36;
			case -28:
				this.messageNotMap(msg);
				goto IL_8E36;
			case -26:
				ServerListScreen.testConnect = 2;
				GameCanvas.debug("SA2", 2);
				GameCanvas.startOKDlg(msg.reader().readUTF());
				InfoDlg.hide();
				LoginScr.isContinueToLogin = false;
				global::Char.isLoadingMap = false;
				if (GameCanvas.currentScreen == GameCanvas.loginScr)
				{
					GameCanvas.serverScreen.switchToMe();
					goto IL_8E36;
				}
				goto IL_8E36;
			case -25:
				GameCanvas.debug("SA3", 2);
				GameScr.info1.addInfo(msg.reader().readUTF(), 0);
				goto IL_8E36;
			case -24:
				Res.outz("***************MAP_INFO**************");
				GameScr.isPickNgocRong = false;
				global::Char.isLoadingMap = true;
				Cout.println("GET MAP INFO");
				GameScr.gI().magicTree = null;
				GameCanvas.isLoading = true;
				GameCanvas.debug("SA75", 2);
				GameScr.resetAllvector();
				GameCanvas.endDlg();
				TileMap.vGo.removeAllElements();
				PopUp.vPopups.removeAllElements();
				TileMap.mapID = (int)msg.reader().readUnsignedByte();
				TileMap.planetID = msg.reader().readByte();
				TileMap.tileID = (int)msg.reader().readByte();
				TileMap.bgID = (int)msg.reader().readByte();
				GameScr.isPaint_CT = (TileMap.mapID != 170);
				Cout.println(string.Concat(new object[]
				{
					"load planet from server: ",
					TileMap.planetID,
					"bgType= ",
					TileMap.bgType,
					"............................."
				}));
				TileMap.typeMap = (int)msg.reader().readByte();
				TileMap.mapName = msg.reader().readUTF();
				TileMap.zoneID = (int)msg.reader().readByte();
				GameCanvas.debug("SA75x1", 2);
				try
				{
					TileMap.loadMapFromResource(TileMap.mapID);
				}
				catch (Exception)
				{
					Service.gI().requestMaptemplate(TileMap.mapID);
					this.messWait = msg;
					goto IL_8E36;
				}
				this.loadInfoMap(msg);
				try
				{
					TileMap.isMapDouble = (msg.reader().readByte() != 0);
				}
				catch (Exception)
				{
				}
				GameScr.cmx = GameScr.cmtoX;
				GameScr.cmy = GameScr.cmtoY;
				GameCanvas.isRequestMapID = 2;
				GameCanvas.waitingTimeChangeMap = 0L;
				global::Char.isLoadingMap = false;
				GameCanvas.isLoading = false;
				goto IL_8E36;
			case -22:
				GameCanvas.debug("SA65", 2);
				global::Char.ischangingMap = true;
				GameScr.gI().timeStartMap = 0;
				GameScr.gI().timeLengthMap = 0;
				global::Char.myCharz().mobFocus = null;
				global::Char.myCharz().npcFocus = null;
				global::Char.myCharz().charFocus = null;
				global::Char.myCharz().itemFocus = null;
				global::Char.myCharz().focus.removeAllElements();
				global::Char.myCharz().testCharId = -9999;
				global::Char.myCharz().killCharId = -9999;
				GameCanvas.resetBg();
				GameScr.gI().resetButton();
				GameScr.gI().center = null;
				mGraphics.clearCachedTextures();
				MainMod.clearMapData();
				if (Effect.vEffData.size() > 15)
				{
					for (int num130 = 0; num130 < 5; num130++)
					{
						Effect.vEffData.removeElementAt(0);
					}
					goto IL_8E36;
				}
				goto IL_8E36;
			case -21:
			{
				GameCanvas.debug("SA60", 2);
				short num131 = msg.reader().readShort();
				for (int num132 = 0; num132 < GameScr.vItemMap.size(); num132++)
				{
					if (((ItemMap)GameScr.vItemMap.elementAt(num132)).itemMapID == (int)num131)
					{
						GameScr.vItemMap.removeElementAt(num132);
						break;
					}
				}
				goto IL_8E36;
			}
			case -20:
			{
				GameCanvas.debug("SA61", 2);
				global::Char.myCharz().itemFocus = null;
				short num133 = msg.reader().readShort();
				int num134 = 0;
				while (num134 < GameScr.vItemMap.size())
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(num134);
					if (itemMap.itemMapID == (int)num133)
					{
						itemMap.setPoint(global::Char.myCharz().cx, global::Char.myCharz().cy - 10);
						string text4 = msg.reader().readUTF();
						i = 0;
						try
						{
							i = (int)msg.reader().readShort();
							if (itemMap.template.type == 9)
							{
								i = (int)msg.reader().readShort();
								global::Char.myCharz().xu += (long)i;
								global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
							}
							else if (itemMap.template.type == 10)
							{
								i = (int)msg.reader().readShort();
								global::Char.myCharz().luong += i;
								global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
							}
							else if (itemMap.template.type == 34)
							{
								i = (int)msg.reader().readShort();
								global::Char.myCharz().luongKhoa += i;
								global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
							}
						}
						catch (Exception)
						{
						}
						if (text4.Equals(string.Empty))
						{
							if (itemMap.template.type == 9)
							{
								GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.YELLOW);
								SoundMn.gI().getItem();
							}
							else if (itemMap.template.type == 10)
							{
								GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.GREEN);
								SoundMn.gI().getItem();
							}
							else if (itemMap.template.type == 34)
							{
								GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.RED);
								SoundMn.gI().getItem();
							}
							else
							{
								GameScr.info1.addInfo(mResources.you_receive + " " + ((i <= 0) ? string.Empty : (i + " ")) + itemMap.template.name, 0);
								SoundMn.gI().getItem();
							}
							if (i > 0 && global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 4683)
							{
								ServerEffect.addServerEffect(55, global::Char.myCharz().petFollow.cmx, global::Char.myCharz().petFollow.cmy, 1);
								ServerEffect.addServerEffect(55, global::Char.myCharz().cx, global::Char.myCharz().cy, 1);
								break;
							}
							break;
						}
						else
						{
							if (text4.Length == 1)
							{
								Cout.LogError3("strInf.Length =1:  " + text4);
								break;
							}
							GameScr.info1.addInfo(text4, 0);
							break;
						}
					}
					else
					{
						num134++;
					}
				}
				goto IL_8E36;
			}
			case -19:
			{
				GameCanvas.debug("SA62", 2);
				short num135 = msg.reader().readShort();
				@char = GameScr.findCharInMap(msg.reader().readInt());
				int num136 = 0;
				while (num136 < GameScr.vItemMap.size())
				{
					ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(num136);
					if (itemMap2.itemMapID == (int)num135)
					{
						if (@char == null)
						{
							return;
						}
						itemMap2.setPoint(@char.cx, @char.cy - 10);
						if (itemMap2.x < @char.cx)
						{
							@char.cdir = -1;
							break;
						}
						if (itemMap2.x > @char.cx)
						{
							@char.cdir = 1;
							break;
						}
						break;
					}
					else
					{
						num136++;
					}
				}
				goto IL_8E36;
			}
			case -18:
			{
				GameCanvas.debug("SA63", 2);
				int num137 = (int)msg.reader().readByte();
				GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), global::Char.myCharz().arrItemBag[num137].template.id, global::Char.myCharz().cx, global::Char.myCharz().cy, (int)msg.reader().readShort(), (int)msg.reader().readShort()));
				global::Char.myCharz().arrItemBag[num137] = null;
				goto IL_8E36;
			}
			case -14:
				GameCanvas.debug("SA64", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), msg.reader().readShort(), @char.cx, @char.cy, (int)msg.reader().readShort(), (int)msg.reader().readShort()));
				goto IL_8E36;
			case -4:
			{
				GameCanvas.debug("SA76", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				GameCanvas.debug("SA76v1", 2);
				if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
				{
					@char.setSkillPaint(GameScr.sks[(int)msg.reader().readUnsignedByte()], 0);
				}
				else
				{
					@char.setSkillPaint(GameScr.sks[(int)msg.reader().readUnsignedByte()], 1);
				}
				GameCanvas.debug("SA76v2", 2);
				@char.attMobs = new Mob[(int)msg.reader().readByte()];
				for (int num138 = 0; num138 < @char.attMobs.Length; num138++)
				{
					Mob mob6 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readByte());
					@char.attMobs[num138] = mob6;
					if (num138 == 0)
					{
						if (@char.cx <= mob6.x)
						{
							@char.cdir = 1;
						}
						else
						{
							@char.cdir = -1;
						}
					}
				}
				GameCanvas.debug("SA76v3", 2);
				@char.charFocus = null;
				@char.mobFocus = @char.attMobs[0];
				global::Char[] array10 = new global::Char[10];
				i = 0;
				try
				{
					for (i = 0; i < array10.Length; i++)
					{
						int num139 = msg.reader().readInt();
						global::Char char9 = array10[i] = ((num139 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num139) : global::Char.myCharz());
						if (i == 0)
						{
							if (@char.cx <= char9.cx)
							{
								@char.cdir = 1;
							}
							else
							{
								@char.cdir = -1;
							}
						}
					}
				}
				catch (Exception ex2)
				{
					Cout.println("Loi PLAYER_ATTACK_N_P " + ex2.ToString());
				}
				GameCanvas.debug("SA76v4", 2);
				if (i > 0)
				{
					@char.attChars = new global::Char[i];
					for (i = 0; i < @char.attChars.Length; i++)
					{
						@char.attChars[i] = array10[i];
					}
					@char.charFocus = @char.attChars[0];
					@char.mobFocus = null;
				}
				GameCanvas.debug("SA76v5", 2);
				goto IL_8E36;
			}
			case 0:
				this.readLogin(msg);
				goto IL_8E36;
			case 1:
			{
				bool flag5 = msg.reader().readBool();
				Res.outz("isRes= " + flag5.ToString());
				if (!flag5)
				{
					GameCanvas.startOKDlg(msg.reader().readUTF());
					goto IL_8E36;
				}
				GameCanvas.loginScr.isLogin2 = false;
				Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, string.Empty);
				GameCanvas.endDlg();
				GameCanvas.loginScr.doLogin();
				goto IL_8E36;
			}
			case 2:
				global::Char.isLoadingMap = false;
				LoginScr.isLoggingIn = false;
				if (!GameScr.isLoadAllData)
				{
					GameScr.gI().initSelectChar();
				}
				BgItem.clearHashTable();
				GameCanvas.endDlg();
				CreateCharScr.isCreateChar = true;
				CreateCharScr.gI().switchToMe();
				goto IL_8E36;
			case 6:
				GameCanvas.debug("SA70", 2);
				global::Char.myCharz().xu = msg.reader().readLong();
				global::Char.myCharz().luong = msg.reader().readInt();
				global::Char.myCharz().luongKhoa = msg.reader().readInt();
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
				global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
				GameCanvas.endDlg();
				goto IL_8E36;
			case 7:
			{
				sbyte type4 = msg.reader().readByte();
				short id3 = msg.reader().readShort();
				string info3 = msg.reader().readUTF();
				GameCanvas.panel.saleRequest(type4, info3, id3);
				goto IL_8E36;
			}
			case 11:
			{
				GameCanvas.debug("SA9", 2);
				int num140 = (int)msg.reader().readShort();
				sbyte b52 = msg.reader().readByte();
				if (b52 != 0)
				{
					Mob.arrMobTemplate[num140].data.readDataNewBoss(NinjaUtil.readByteArray(msg), b52);
				}
				else
				{
					Mob.arrMobTemplate[num140].data.readData(NinjaUtil.readByteArray(msg));
				}
				for (int num141 = 0; num141 < GameScr.vMob.size(); num141++)
				{
					Mob mob7 = (Mob)GameScr.vMob.elementAt(num141);
					if (mob7.templateId == num140)
					{
						mob7.w = Mob.arrMobTemplate[num140].data.width;
						mob7.h = Mob.arrMobTemplate[num140].data.height;
					}
				}
				sbyte[] array11 = NinjaUtil.readByteArray(msg);
				Image img = Image.createImage(array11, 0, array11.Length);
				Mob.arrMobTemplate[num140].data.img = img;
				int num142 = (int)msg.reader().readByte();
				Mob.arrMobTemplate[num140].data.typeData = num142;
				if (num142 == 1 || num142 == 2)
				{
					this.readFrameBoss(msg, num140);
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case 12:
				this.read_cmdExtraBig(msg);
				goto IL_8E36;
			case 20:
				this.phuban_Info(msg);
				goto IL_8E36;
			case 24:
				this.read_cmdExtra(msg);
				goto IL_8E36;
			case 27:
			{
				myVector = new MyVector();
				msg.reader().readUTF();
				int num143 = (int)msg.reader().readByte();
				for (int num144 = 0; num144 < num143; num144++)
				{
					string caption3 = msg.reader().readUTF();
					short num145 = msg.reader().readShort();
					myVector.addElement(new Command(caption3, GameCanvas.instance, 88819, num145));
				}
				GameCanvas.menu.startWithoutCloseButton(myVector, 3);
				goto IL_8E36;
			}
			case 29:
				GameCanvas.debug("SA58", 2);
				GameScr.gI().openUIZone(msg);
				goto IL_8E36;
			case 32:
			{
				GameCanvas.debug("SA68", 2);
				int num146 = (int)msg.reader().readShort();
				for (int num147 = 0; num147 < GameScr.vNpc.size(); num147++)
				{
					Npc npc2 = (Npc)GameScr.vNpc.elementAt(num147);
					if (npc2.template.npcTemplateId == num146 && npc2.Equals(global::Char.myCharz().npcFocus))
					{
						string chat = msg.reader().readUTF();
						string[] array12 = new string[(int)msg.reader().readByte()];
						for (int num148 = 0; num148 < array12.Length; num148++)
						{
							array12[num148] = msg.reader().readUTF();
						}
						GameScr.gI().createMenu(array12, npc2);
						ChatPopup.addChatPopup(chat, 100000, npc2);
						return;
					}
				}
				Npc npc3 = new Npc(num146, 0, -100, 100, num146, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
				Res.outz((global::Char.myCharz().npcFocus == null) ? "null" : "!null");
				string chat2 = msg.reader().readUTF();
				string[] array13 = new string[(int)msg.reader().readByte()];
				for (int num149 = 0; num149 < array13.Length; num149++)
				{
					array13[num149] = msg.reader().readUTF();
				}
				try
				{
					short avatar2 = msg.reader().readShort();
					npc3.avatar = (int)avatar2;
				}
				catch (Exception)
				{
				}
				Res.outz((global::Char.myCharz().npcFocus == null) ? "null" : "!null");
				GameScr.gI().createMenu(array13, npc3);
				ChatPopup.addChatPopup(chat2, 100000, npc3);
				goto IL_8E36;
			}
			case 33:
				GameCanvas.debug("SA51", 2);
				InfoDlg.hide();
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				myVector = new MyVector();
				try
				{
					for (;;)
					{
						string caption4 = msg.reader().readUTF();
						myVector.addElement(new Command(caption4, GameCanvas.instance, 88822, null));
					}
				}
				catch (Exception ex3)
				{
					Cout.println("Loi OPEN_UI_MENU " + ex3.ToString());
				}
				if (global::Char.myCharz().npcFocus == null)
				{
					return;
				}
				for (int num150 = 0; num150 < global::Char.myCharz().npcFocus.template.menu.Length; num150++)
				{
					string[] array14 = global::Char.myCharz().npcFocus.template.menu[num150];
					myVector.addElement(new Command(array14[0], GameCanvas.instance, 88820, array14));
				}
				GameCanvas.menu.startAt(myVector, 3);
				goto IL_8E36;
			case 38:
			{
				GameCanvas.debug("SA67", 2);
				InfoDlg.hide();
				int num151 = (int)msg.reader().readShort();
				Res.outz("OPEN_UI_SAY ID= " + num151);
				string text5 = msg.reader().readUTF();
				text5 = Res.changeString(text5);
				for (int num152 = 0; num152 < GameScr.vNpc.size(); num152++)
				{
					Npc npc4 = (Npc)GameScr.vNpc.elementAt(num152);
					Res.outz("npc id= " + npc4.template.npcTemplateId);
					if (npc4.template.npcTemplateId == num151)
					{
						ChatPopup.addChatPopupMultiLine(text5, 100000, npc4);
						GameCanvas.panel.hideNow();
						return;
					}
				}
				Npc npc5 = new Npc(num151, 0, 0, 0, num151, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
				if (npc5.template.npcTemplateId == 5)
				{
					npc5.charID = 5;
				}
				try
				{
					npc5.avatar = (int)msg.reader().readShort();
				}
				catch (Exception)
				{
				}
				ChatPopup.addChatPopupMultiLine(text5, 100000, npc5);
				GameCanvas.panel.hideNow();
				goto IL_8E36;
			}
			case 39:
				GameCanvas.debug("SA49", 2);
				GameScr.gI().typeTradeOrder = 2;
				if (GameScr.gI().typeTrade >= 2 && GameScr.gI().typeTradeOrder >= 2)
				{
					InfoDlg.showWait();
					goto IL_8E36;
				}
				goto IL_8E36;
			case 40:
			{
				GameCanvas.debug("SA52", 2);
				GameCanvas.taskTick = 150;
				short taskId = msg.reader().readShort();
				sbyte index3 = msg.reader().readByte();
				string text6 = msg.reader().readUTF();
				text6 = Res.changeString(text6);
				string text7 = msg.reader().readUTF();
				text7 = Res.changeString(text7);
				string[] array15 = new string[(int)msg.reader().readByte()];
				string[] array16 = new string[array15.Length];
				GameScr.tasks = new int[array15.Length];
				GameScr.mapTasks = new int[array15.Length];
				short[] array17 = new short[array15.Length];
				short num153 = -1;
				for (int num154 = 0; num154 < array15.Length; num154++)
				{
					string text8 = msg.reader().readUTF();
					text8 = Res.changeString(text8);
					GameScr.tasks[num154] = (int)msg.reader().readByte();
					GameScr.mapTasks[num154] = (int)msg.reader().readShort();
					string text9 = msg.reader().readUTF();
					text9 = Res.changeString(text9);
					array17[num154] = -1;
					array15[num154] = text8;
					if (!text9.Equals(string.Empty))
					{
						array16[num154] = text9;
					}
				}
				try
				{
					num153 = msg.reader().readShort();
					Cout.println(" TASK_GET count:" + num153);
					for (int num155 = 0; num155 < array15.Length; num155++)
					{
						array17[num155] = msg.reader().readShort();
						Cout.println(num155 + " i TASK_GET   counts[i]:" + array17[num155]);
					}
				}
				catch (Exception ex4)
				{
					Cout.println("Loi TASK_GET " + ex4.ToString());
				}
				global::Char.myCharz().taskMaint = new Task(taskId, index3, text6, text7, array15, array17, num153, array16);
				if (global::Char.myCharz().npcFocus != null)
				{
					Npc.clearEffTask();
				}
				global::Char.taskAction(true);
				goto IL_8E36;
			}
			case 41:
				GameCanvas.debug("SA53", 2);
				GameCanvas.taskTick = 100;
				Res.outz("TASK NEXT");
				global::Char.myCharz().taskMaint.index++;
				global::Char.myCharz().taskMaint.count = 0;
				Npc.clearEffTask();
				global::Char.taskAction(true);
				goto IL_8E36;
			case 43:
				GameCanvas.taskTick = 50;
				GameCanvas.debug("SA55", 2);
				global::Char.myCharz().taskMaint.count = msg.reader().readShort();
				if (global::Char.myCharz().npcFocus != null)
				{
					Npc.clearEffTask();
				}
				try
				{
					short x_hint = msg.reader().readShort();
					short y_hint = msg.reader().readShort();
					global::Char.myCharz().x_hint = x_hint;
					global::Char.myCharz().y_hint = y_hint;
					goto IL_8E36;
				}
				catch (Exception)
				{
					goto IL_8E36;
				}
				goto IL_7D8C;
			case 46:
			{
				GameCanvas.debug("SA5", 2);
				Cout.LogWarning("Controler RESET_POINT  " + global::Char.ischangingMap.ToString());
				global::Char.isLockKey = false;
				int num156 = (int)msg.reader().readShort();
				int num157 = (int)msg.reader().readShort();
				global::Char.myCharz().setResetPoint(num156, num157);
				global::Char.myCharz().cx = num156;
				global::Char.myCharz().cy = num157;
				goto IL_8E36;
			}
			case 47:
				goto IL_7D8C;
			case 54:
			{
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				int num158 = (int)msg.reader().readUnsignedByte();
				if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
				{
					@char.setSkillPaint(GameScr.sks[num158], 0);
				}
				else
				{
					@char.setSkillPaint(GameScr.sks[num158], 1);
				}
				Mob[] array18 = new Mob[10];
				i = 0;
				try
				{
					for (i = 0; i < array18.Length; i++)
					{
						Mob mob8 = array18[i] = (Mob)GameScr.vMob.elementAt((int)msg.reader().readByte());
						if (i == 0)
						{
							if (@char.cx <= mob8.x)
							{
								@char.cdir = 1;
							}
							else
							{
								@char.cdir = -1;
							}
						}
					}
				}
				catch (Exception)
				{
				}
				if (i > 0)
				{
					@char.attMobs = new Mob[i];
					for (i = 0; i < @char.attMobs.Length; i++)
					{
						@char.attMobs[i] = array18[i];
					}
					@char.charFocus = null;
					@char.mobFocus = @char.attMobs[0];
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case 56:
			{
				GameCanvas.debug("SXX6", 2);
				@char = null;
				int num159 = msg.reader().readInt();
				if (num159 == global::Char.myCharz().charID)
				{
					bool flag6 = false;
					@char = global::Char.myCharz();
					@char.cHP = msg.reader().readLong();
					long num160 = msg.reader().readLong();
					Res.outz("dame hit = " + num160);
					if (num160 != 0L)
					{
						@char.doInjure();
					}
					int num161 = 0;
					try
					{
						flag6 = msg.reader().readBoolean();
						sbyte b53 = msg.reader().readByte();
						if (b53 != -1)
						{
							Res.outz("hit eff= " + b53);
							EffecMn.addEff(new Effect((int)b53, @char.cx, @char.cy, 3, 1, -1));
						}
					}
					catch (Exception)
					{
					}
					num160 += (long)num161;
					if (global::Char.myCharz().cTypePk == 4)
					{
						goto IL_8E36;
					}
					if (num160 == 0L)
					{
						GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS_ME);
						goto IL_8E36;
					}
					GameScr.startFlyText("-" + num160, @char.cx, @char.cy - @char.ch, 0, -3, flag6 ? mFont.FATAL : mFont.RED);
					goto IL_8E36;
				}
				else
				{
					@char = GameScr.findCharInMap(num159);
					if (@char == null)
					{
						return;
					}
					@char.cHP = msg.reader().readLong();
					bool flag7 = false;
					long num162 = msg.reader().readLong();
					if (num162 != 0L)
					{
						@char.doInjure();
					}
					int num163 = 0;
					try
					{
						flag7 = msg.reader().readBoolean();
						sbyte b54 = msg.reader().readByte();
						if (b54 != -1)
						{
							Res.outz("hit eff= " + b54);
							EffecMn.addEff(new Effect((int)b54, @char.cx, @char.cy, 3, 1, -1));
						}
					}
					catch (Exception)
					{
					}
					num162 += (long)num163;
					if (@char.cTypePk == 4)
					{
						goto IL_8E36;
					}
					if (num162 == 0L)
					{
						GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS);
						goto IL_8E36;
					}
					GameScr.startFlyText("-" + num162, @char.cx, @char.cy - @char.ch, 0, -3, flag7 ? mFont.FATAL : mFont.ORANGE);
					goto IL_8E36;
				}
				break;
			}
			case 57:
			{
				GameCanvas.debug("SZ6", 2);
				MyVector myVector2 = new MyVector();
				myVector2.addElement(new Command(msg.reader().readUTF(), GameCanvas.instance, 88817, null));
				GameCanvas.menu.startAt(myVector2, 3);
				goto IL_8E36;
			}
			case 58:
			{
				GameCanvas.debug("SZ7", 2);
				int num164 = msg.reader().readInt();
				global::Char char10 = (num164 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num164) : global::Char.myCharz();
				char10.moveFast = new short[3];
				char10.moveFast[0] = 0;
				short num165 = msg.reader().readShort();
				short num166 = msg.reader().readShort();
				char10.moveFast[1] = num165;
				char10.moveFast[2] = num166;
				try
				{
					num164 = msg.reader().readInt();
					global::Char char11 = (num164 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num164) : global::Char.myCharz();
					char11.cx = (int)num165;
					char11.cy = (int)num166;
					goto IL_8E36;
				}
				catch (Exception ex5)
				{
					Cout.println("Loi MOVE_FAST " + ex5.ToString());
					goto IL_8E36;
				}
				goto IL_8329;
			}
			case 62:
				GameCanvas.debug("SZ3", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.killCharId = global::Char.myCharz().charID;
					global::Char.myCharz().npcFocus = null;
					global::Char.myCharz().mobFocus = null;
					global::Char.myCharz().itemFocus = null;
					global::Char.myCharz().charFocus = @char;
					global::Char.isManualFocus = true;
					GameScr.info1.addInfo(@char.cName + mResources.CUU_SAT, 0);
					goto IL_8E36;
				}
				goto IL_8E36;
			case 63:
				goto IL_8329;
			case 64:
				GameCanvas.debug("SZ5", 2);
				@char = global::Char.myCharz();
				try
				{
					@char = GameScr.findCharInMap(msg.reader().readInt());
				}
				catch (Exception ex6)
				{
					Cout.println("Loi CLEAR_CUU_SAT " + ex6.ToString());
				}
				@char.killCharId = -9999;
				goto IL_8E36;
			case 65:
			{
				sbyte id4 = msg.reader().readSByte();
				string text10 = msg.reader().readUTF();
				short num167 = msg.reader().readShort();
				if (!string.IsNullOrEmpty(text10))
				{
					GameScr.info1.addInfo(text10, 0);
				}
				if (!ItemTime.isExistMessage((int)id4))
				{
					ItemTime itemTime = new ItemTime();
					itemTime.initTimeText(id4, text10, (int)num167);
					GameScr.textTime.addElement(itemTime);
					goto IL_8E36;
				}
				if (num167 != 0)
				{
					ItemTime.getMessageById((int)id4).initTimeText(id4, text10, (int)num167);
					goto IL_8E36;
				}
				GameScr.textTime.removeElement(ItemTime.getMessageById((int)id4));
				goto IL_8E36;
			}
			case 66:
				this.readGetImgByName(msg);
				goto IL_8E36;
			case 68:
			{
				Res.outz("ADD ITEM TO MAP --------------------------------------");
				GameCanvas.debug("SA6333", 2);
				short itemMapID = msg.reader().readShort();
				short itemTemplateID = msg.reader().readShort();
				int x = (int)msg.reader().readShort();
				int y = (int)msg.reader().readShort();
				int num168 = msg.reader().readInt();
				short r = 0;
				if (num168 == -2)
				{
					r = msg.reader().readShort();
				}
				ItemMap itemMap3 = new ItemMap(num168, itemMapID, itemTemplateID, x, y, r);
				bool flag8 = false;
				for (int num169 = 0; num169 < GameScr.vItemMap.size(); num169++)
				{
					if (((ItemMap)GameScr.vItemMap.elementAt(num169)).itemMapID == itemMap3.itemMapID)
					{
						flag8 = true;
						break;
					}
				}
				if (!flag8)
				{
					GameScr.vItemMap.addElement(itemMap3);
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case 69:
				SoundMn.IsDelAcc = (msg.reader().readByte() != 0);
				goto IL_8E36;
			case 81:
				GameCanvas.debug("SXX4", 2);
				((Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte())).isDisable = msg.reader().readBool();
				goto IL_8E36;
			case 82:
				GameCanvas.debug("SXX5", 2);
				((Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte())).isDontMove = msg.reader().readBool();
				goto IL_8E36;
			case 83:
			{
				GameCanvas.debug("SXX8", 2);
				int num170 = msg.reader().readInt();
				@char = ((num170 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num170) : global::Char.myCharz());
				if (@char == null)
				{
					return;
				}
				Mob mobToAttack = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				if (@char.mobMe != null)
				{
					@char.mobMe.attackOtherMob(mobToAttack);
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case 84:
			{
				int num171 = msg.reader().readInt();
				if (num171 == global::Char.myCharz().charID)
				{
					@char = global::Char.myCharz();
				}
				else
				{
					@char = GameScr.findCharInMap(num171);
					if (@char == null)
					{
						return;
					}
				}
				@char.cHP = @char.cHPFull;
				@char.cMP = @char.cMPFull;
				@char.cx = (int)msg.reader().readShort();
				@char.cy = (int)msg.reader().readShort();
				@char.liveFromDead();
				goto IL_8E36;
			}
			case 85:
				GameCanvas.debug("SXX5", 2);
				((Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte())).isFire = msg.reader().readBool();
				goto IL_8E36;
			case 86:
			{
				GameCanvas.debug("SXX5", 2);
				Mob mob9 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob9.isIce = msg.reader().readBool();
				if (!mob9.isIce)
				{
					ServerEffect.addServerEffect(77, mob9.x, mob9.y - 9, 1);
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case 87:
				GameCanvas.debug("SXX5", 2);
				((Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte())).isWind = msg.reader().readBool();
				goto IL_8E36;
			case 88:
			{
				string info4 = msg.reader().readUTF();
				short num172 = msg.reader().readShort();
				GameCanvas.inputDlg.show(info4, new Command(mResources.ACCEPT, GameCanvas.instance, 88818, num172), TField.INPUT_TYPE_ANY);
				goto IL_8E36;
			}
			case 90:
				GameCanvas.debug("SA577", 2);
				this.requestItemPlayer(msg);
				goto IL_8E36;
			case 92:
			{
				if (GameCanvas.currentScreen == GameScr.instance)
				{
					GameCanvas.endDlg();
				}
				string text11 = msg.reader().readUTF();
				string text12 = msg.reader().readUTF();
				text12 = Res.changeString(text12);
				string text13 = string.Empty;
				global::Char char12 = null;
				sbyte b55 = 0;
				if (!text11.Equals(string.Empty))
				{
					char12 = new global::Char();
					char12.charID = msg.reader().readInt();
					char12.head = (int)msg.reader().readShort();
					char12.headICON = (int)msg.reader().readShort();
					char12.body = (int)msg.reader().readShort();
					char12.bag = (int)msg.reader().readShort();
					char12.leg = (int)msg.reader().readShort();
					b55 = msg.reader().readByte();
					char12.cName = text11;
				}
				text13 += text12;
				InfoDlg.hide();
				if (text11.Equals(string.Empty))
				{
					GameScr.info1.addInfo(text13, 0);
					goto IL_8E36;
				}
				GameScr.info2.addInfoWithChar(text13, char12, b55 == 0);
				if (GameCanvas.panel.isShow && GameCanvas.panel.type == 8)
				{
					GameCanvas.panel.initLogMessage();
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			case 94:
				GameCanvas.debug("SA3", 2);
				GameScr.info1.addInfo(msg.reader().readUTF(), 0);
				goto IL_8E36;
			default:
			{
				if (b != 112)
				{
					goto IL_8E36;
				}
				sbyte b56 = msg.reader().readByte();
				Res.outz("spec type= " + b56);
				if (b56 == 0)
				{
					Panel.spearcialImage = msg.reader().readShort();
					Panel.specialInfo = msg.reader().readUTF();
					goto IL_8E36;
				}
				if (b56 == 1)
				{
					sbyte b57 = msg.reader().readByte();
					global::Char.myCharz().infoSpeacialSkill = new string[(int)b57][];
					global::Char.myCharz().imgSpeacialSkill = new short[(int)b57][];
					GameCanvas.panel.speacialTabName = new string[(int)b57][];
					for (int num173 = 0; num173 < (int)b57; num173++)
					{
						GameCanvas.panel.speacialTabName[num173] = new string[2];
						string[] array19 = Res.split(msg.reader().readUTF(), "\n", 0);
						if (array19.Length == 2)
						{
							GameCanvas.panel.speacialTabName[num173] = array19;
						}
						if (array19.Length == 1)
						{
							GameCanvas.panel.speacialTabName[num173][0] = array19[0];
							GameCanvas.panel.speacialTabName[num173][1] = string.Empty;
						}
						int num174 = (int)msg.reader().readByte();
						global::Char.myCharz().infoSpeacialSkill[num173] = new string[num174];
						global::Char.myCharz().imgSpeacialSkill[num173] = new short[num174];
						for (int num175 = 0; num175 < num174; num175++)
						{
							global::Char.myCharz().imgSpeacialSkill[num173][num175] = msg.reader().readShort();
							global::Char.myCharz().infoSpeacialSkill[num173][num175] = msg.reader().readUTF();
						}
					}
					GameCanvas.panel.tabName[25] = GameCanvas.panel.speacialTabName;
					GameCanvas.panel.setTypeSpeacialSkill();
					GameCanvas.panel.show();
					goto IL_8E36;
				}
				goto IL_8E36;
			}
			}
			string strInvite = msg.reader().readUTF();
			int clanID = msg.reader().readInt();
			int code = msg.reader().readInt();
			GameScr.gI().clanInvite(strInvite, clanID, code);
			goto IL_8E36;
			IL_7D8C:
			GameCanvas.debug("SA4", 2);
			GameScr.gI().resetButton();
			goto IL_8E36;
			IL_8329:
			GameCanvas.debug("SZ4", 2);
			global::Char.myCharz().killCharId = msg.reader().readInt();
			global::Char.myCharz().npcFocus = null;
			global::Char.myCharz().mobFocus = null;
			global::Char.myCharz().itemFocus = null;
			global::Char.myCharz().charFocus = GameScr.findCharInMap(global::Char.myCharz().killCharId);
			global::Char.isManualFocus = true;
			IL_8E36:
			b = msg.command;
			if (b <= 19)
			{
				if (b <= -73)
				{
					if (b != -75)
					{
						if (b == -73)
						{
							sbyte b58 = msg.reader().readByte();
							int num176 = 0;
							while (num176 < GameScr.vNpc.size())
							{
								Npc npc6 = (Npc)GameScr.vNpc.elementAt(num176);
								if (npc6.template.npcTemplateId == (int)b58)
								{
									if (msg.reader().readByte() == 0)
									{
										npc6.isHide = true;
										break;
									}
									npc6.isHide = false;
									break;
								}
								else
								{
									num176++;
								}
							}
						}
					}
					else
					{
						Mob mob10 = null;
						try
						{
							mob10 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						}
						catch (Exception)
						{
						}
						if (mob10 != null)
						{
							mob10.levelBoss = msg.reader().readByte();
							if (mob10.levelBoss > 0)
							{
								mob10.typeSuperEff = Res.random(0, 3);
							}
						}
					}
				}
				else
				{
					switch (b)
					{
					case -17:
						GameCanvas.debug("SA88", 2);
						global::Char.myCharz().meDead = true;
						global::Char.myCharz().cPk = msg.reader().readByte();
						global::Char.myCharz().startDie(msg.reader().readShort(), msg.reader().readShort());
						try
						{
							global::Char.myCharz().cPower = msg.reader().readLong();
							global::Char.myCharz().applyCharLevelPercent();
						}
						catch (Exception)
						{
							Cout.println("Loi tai ME_DIE " + msg.command);
						}
						global::Char.myCharz().countKill = 0;
						goto IL_A917;
					case -16:
						GameCanvas.debug("SA90", 2);
						if (global::Char.myCharz().wdx != 0 || global::Char.myCharz().wdy != 0)
						{
							global::Char.myCharz().cx = (int)global::Char.myCharz().wdx;
							global::Char.myCharz().cy = (int)global::Char.myCharz().wdy;
							global::Char.myCharz().wdx = (global::Char.myCharz().wdy = 0);
						}
						global::Char.myCharz().liveFromDead();
						global::Char.myCharz().isLockMove = false;
						global::Char.myCharz().meDead = false;
						goto IL_A917;
					case -15:
					case -14:
					case -4:
						goto IL_A917;
					case -13:
					{
						GameCanvas.debug("SA82", 2);
						int num177 = (int)msg.reader().readUnsignedByte();
						if (num177 > GameScr.vMob.size() - 1 || num177 < 0)
						{
							return;
						}
						Mob mob11 = (Mob)GameScr.vMob.elementAt(num177);
						mob11.sys = (int)msg.reader().readByte();
						mob11.levelBoss = msg.reader().readByte();
						if (mob11.levelBoss != 0)
						{
							mob11.typeSuperEff = Res.random(0, 3);
						}
						mob11.x = mob11.xFirst;
						mob11.y = mob11.yFirst;
						mob11.status = 5;
						mob11.injureThenDie = false;
						mob11.hp = msg.reader().readLong();
						mob11.maxHp = mob11.hp;
						mob11.updateHp_bar();
						ServerEffect.addServerEffect(60, mob11.x, mob11.y, 1);
						goto IL_A917;
					}
					case -12:
					{
						Res.outz("SERVER SEND MOB DIE");
						GameCanvas.debug("SA85", 2);
						Mob mob12 = null;
						try
						{
							mob12 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						}
						catch (Exception)
						{
							Cout.println("LOi tai NPC_DIE cmd " + msg.command);
						}
						if (mob12 == null || mob12.status == 0 || mob12.status == 0)
						{
							goto IL_A917;
						}
						mob12.startDie();
						try
						{
							long num178 = msg.reader().readLong();
							if (msg.reader().readBool())
							{
								GameScr.startFlyText("-" + num178, mob12.x, mob12.y - mob12.h, 0, -2, mFont.FATAL);
							}
							else
							{
								GameScr.startFlyText("-" + num178, mob12.x, mob12.y - mob12.h, 0, -2, mFont.ORANGE);
							}
							sbyte b59 = msg.reader().readByte();
							for (int num179 = 0; num179 < (int)b59; num179++)
							{
								ItemMap itemMap4 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob12.x, mob12.y, (int)msg.reader().readShort(), (int)msg.reader().readShort());
								int num180 = itemMap4.playerId = msg.reader().readInt();
								Res.outz(string.Concat(new object[]
								{
									"playerid= ",
									num180,
									" my id= ",
									global::Char.myCharz().charID
								}));
								GameScr.vItemMap.addElement(itemMap4);
								if (Res.abs(itemMap4.y - global::Char.myCharz().cy) < 24 && Res.abs(itemMap4.x - global::Char.myCharz().cx) < 24)
								{
									global::Char.myCharz().charFocus = null;
								}
							}
							goto IL_A917;
						}
						catch (Exception)
						{
							goto IL_A917;
						}
						break;
					}
					case -11:
					{
						GameCanvas.debug("SA86", 2);
						Mob mob13 = null;
						try
						{
							int index4 = (int)msg.reader().readUnsignedByte();
							mob13 = (Mob)GameScr.vMob.elementAt(index4);
						}
						catch (Exception ex7)
						{
							Res.outz(string.Concat(new object[]
							{
								"Loi tai NPC_ATTACK_ME ",
								msg.command,
								" err= ",
								ex7.StackTrace
							}));
						}
						if (mob13 == null)
						{
							goto IL_A917;
						}
						global::Char.myCharz().isDie = false;
						global::Char.isLockKey = false;
						long num181 = msg.reader().readLong();
						long num182;
						try
						{
							num182 = msg.reader().readLong();
						}
						catch (Exception)
						{
							num182 = 0L;
						}
						if (mob13.isBusyAttackSomeOne)
						{
							global::Char.myCharz().doInjure(num181, num182, false, true);
							goto IL_A917;
						}
						mob13.dame = num181;
						mob13.dameMp = num182;
						mob13.setAttack(global::Char.myCharz());
						goto IL_A917;
					}
					case -10:
						break;
					case -9:
					{
						GameCanvas.debug("SA83", 2);
						Mob mob14 = null;
						try
						{
							mob14 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						}
						catch (Exception)
						{
						}
						GameCanvas.debug("SA83v1", 2);
						if (mob14 != null)
						{
							mob14.hp = msg.reader().readLong();
							mob14.updateHp_bar();
							long num183 = msg.reader().readLong();
							if (num183 == 1L)
							{
								return;
							}
							if (num183 > 1L)
							{
								mob14.setInjure();
							}
							bool flag9 = false;
							try
							{
								flag9 = msg.reader().readBoolean();
							}
							catch (Exception)
							{
							}
							sbyte b60 = msg.reader().readByte();
							if (b60 != -1)
							{
								EffecMn.addEff(new Effect((int)b60, mob14.x, mob14.getY(), 3, 1, -1));
							}
							GameCanvas.debug("SA83v2", 2);
							if (flag9)
							{
								GameScr.startFlyText("-" + num183, mob14.x, mob14.getY() - mob14.getH(), 0, -2, mFont.FATAL);
							}
							else if (num183 == 0L)
							{
								mob14.x = mob14.xFirst;
								mob14.y = mob14.yFirst;
								GameScr.startFlyText(mResources.miss, mob14.x, mob14.getY() - mob14.getH(), 0, -2, mFont.MISS);
							}
							else if (num183 > 1L)
							{
								GameScr.startFlyText("-" + num183, mob14.x, mob14.getY() - mob14.getH(), 0, -2, mFont.ORANGE);
							}
						}
						GameCanvas.debug("SA83v3", 2);
						goto IL_A917;
					}
					case -8:
						GameCanvas.debug("SA89", 2);
						@char = GameScr.findCharInMap(msg.reader().readInt());
						if (@char == null)
						{
							return;
						}
						@char.cPk = msg.reader().readByte();
						@char.waitToDie(msg.reader().readShort(), msg.reader().readShort());
						goto IL_A917;
					case -7:
					{
						GameCanvas.debug("SA80", 2);
						int num184 = msg.reader().readInt();
						int num185 = 0;
						while (num185 < GameScr.vCharInMap.size())
						{
							global::Char char13 = null;
							try
							{
								char13 = (global::Char)GameScr.vCharInMap.elementAt(num185);
								goto IL_98BD;
							}
							catch (Exception)
							{
							}
							IL_98AD:
							num185++;
							continue;
							IL_98BD:
							if (char13 != null && char13.charID == num184)
							{
								GameCanvas.debug("SA8x2y" + num185, 2);
								char13.moveTo((int)msg.reader().readShort(), (int)msg.reader().readShort(), 0);
								char13.lastUpdateTime = mSystem.currentTimeMillis();
								break;
							}
							goto IL_98AD;
						}
						GameCanvas.debug("SA80x3", 2);
						goto IL_A917;
					}
					case -6:
					{
						GameCanvas.debug("SA81", 2);
						int num186 = msg.reader().readInt();
						for (int num187 = 0; num187 < GameScr.vCharInMap.size(); num187++)
						{
							global::Char char14 = (global::Char)GameScr.vCharInMap.elementAt(num187);
							if (char14 != null && char14.charID == num186)
							{
								if (!char14.isInvisiblez && !char14.isUsePlane)
								{
									ServerEffect.addServerEffect(60, char14.cx, char14.cy, 1);
								}
								if (!char14.isUsePlane)
								{
									GameScr.vCharInMap.removeElementAt(num187);
								}
								return;
							}
						}
						goto IL_A917;
					}
					case -5:
					{
						GameCanvas.debug("SA79", 2);
						int num188 = msg.reader().readInt();
						int num189 = msg.reader().readInt();
						global::Char char15;
						if (num189 != -100)
						{
							char15 = new global::Char();
							char15.charID = num188;
							char15.clanID = num189;
						}
						else
						{
							char15 = new Mabu();
							char15.charID = num188;
							char15.clanID = num189;
						}
						if (char15.clanID == -2)
						{
							char15.isCopy = true;
						}
						if (this.readCharInfo(char15, msg))
						{
							sbyte b61 = msg.reader().readByte();
							if (char15.cy <= 10 && b61 != 0 && b61 != 2)
							{
								Res.outz(string.Concat(new object[]
								{
									"nhân vật bay trên trời xuống x= ",
									char15.cx,
									" y= ",
									char15.cy
								}));
								Teleport teleport = new Teleport(char15.cx, char15.cy, char15.head, char15.cdir, 1, false, (b61 != 1) ? ((int)b61) : char15.cgender);
								teleport.id = char15.charID;
								char15.isTeleport = true;
								Teleport.addTeleport(teleport);
							}
							if (b61 == 2)
							{
								char15.show();
							}
							for (int num190 = 0; num190 < GameScr.vMob.size(); num190++)
							{
								Mob mob15 = (Mob)GameScr.vMob.elementAt(num190);
								if (mob15 != null && mob15.isMobMe && mob15.mobId == char15.charID)
								{
									Res.outz("co 1 con quai");
									char15.mobMe = mob15;
									char15.mobMe.x = char15.cx;
									char15.mobMe.y = char15.cy - 40;
									break;
								}
							}
							GameScr.RemovefindCharInMap(num188);
							GameScr.vCharInMap.addElement(char15);
							char15.isMonkey = msg.reader().readByte();
							short num191 = msg.reader().readShort();
							Res.outz("mount id= " + num191 + "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
							if (num191 != -1)
							{
								char15.isHaveMount = true;
								if (num191 <= 351)
								{
									if (num191 - 346 <= 2)
									{
										char15.isMountVip = false;
										goto IL_9DE0;
									}
									if (num191 - 349 <= 2)
									{
										char15.isMountVip = true;
										goto IL_9DE0;
									}
								}
								else
								{
									if (num191 == 396)
									{
										char15.isEventMount = true;
										goto IL_9DE0;
									}
									if (num191 == 532)
									{
										char15.isSpeacialMount = true;
										goto IL_9DE0;
									}
								}
								if (num191 >= global::Char.ID_NEW_MOUNT)
								{
									char15.idMount = num191;
								}
							}
							else
							{
								char15.isHaveMount = false;
							}
						}
						IL_9DE0:
						sbyte b62 = msg.reader().readByte();
						Res.outz("addplayer:   " + b62);
						char15.cFlag = b62;
						char15.isNhapThe = (msg.reader().readByte() == 1);
						try
						{
							char15.idAuraEff = msg.reader().readShort();
							char15.idEff_Set_Item = (short)msg.reader().readSByte();
							char15.idHat = msg.reader().readShort();
							if (char15.bag >= 201 && char15.bag < 255)
							{
								char15.addEffChar(new Effect(char15.bag, char15, 2, -1, 10, 1)
								{
									typeEff = 5
								});
							}
							else
							{
								for (int num192 = 0; num192 < 54; num192++)
								{
									char15.removeEffChar(0, 201 + num192);
								}
							}
						}
						catch (Exception ex8)
						{
							Res.outz("cmd: -5 err: " + ex8.StackTrace);
						}
						GameScr.gI().getFlagImage(char15.charID, char15.cFlag);
						goto IL_A917;
					}
					case -3:
					{
						GameCanvas.debug("SA78", 2);
						sbyte b63 = msg.reader().readByte();
						int num193 = msg.reader().readInt();
						if (b63 == 0)
						{
							global::Char.myCharz().cPower += (long)num193;
						}
						if (b63 == 1)
						{
							global::Char.myCharz().cTiemNang += (long)num193;
						}
						if (b63 == 2)
						{
							global::Char.myCharz().cPower += (long)num193;
							global::Char.myCharz().cTiemNang += (long)num193;
						}
						global::Char.myCharz().applyCharLevelPercent();
						if (global::Char.myCharz().cTypePk == 3)
						{
							goto IL_A917;
						}
						GameScr.startFlyText(((num193 <= 0) ? string.Empty : "+") + num193, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -4, mFont.GREEN);
						if (num193 > 0 && global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5002)
						{
							ServerEffect.addServerEffect(55, global::Char.myCharz().petFollow.cmx, global::Char.myCharz().petFollow.cmy, 1);
							ServerEffect.addServerEffect(55, global::Char.myCharz().cx, global::Char.myCharz().cy, 1);
							goto IL_A917;
						}
						goto IL_A917;
					}
					case -2:
					{
						GameCanvas.debug("SA77", 22);
						int num194 = msg.reader().readInt();
						global::Char.myCharz().yen += num194;
						GameScr.startFlyText((num194 <= 0) ? (string.Empty + num194) : ("+" + num194), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
						goto IL_A917;
					}
					case -1:
					{
						GameCanvas.debug("SA77", 222);
						int num195 = msg.reader().readInt();
						global::Char.myCharz().xu += (long)num195;
						global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
						global::Char.myCharz().yen -= num195;
						GameScr.startFlyText("+" + num195, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
						goto IL_A917;
					}
					default:
						if (b == 18)
						{
							sbyte b64 = msg.reader().readByte();
							for (int num196 = 0; num196 < (int)b64; num196++)
							{
								int charId = msg.reader().readInt();
								int cx = (int)msg.reader().readShort();
								int cy = (int)msg.reader().readShort();
								long cHPShow = msg.reader().readLong();
								global::Char char16 = GameScr.findCharInMap(charId);
								if (char16 != null)
								{
									char16.cx = cx;
									char16.cy = cy;
									char16.cHP = (char16.cHPShow = cHPShow);
									char16.lastUpdateTime = mSystem.currentTimeMillis();
								}
							}
							goto IL_A917;
						}
						if (b == 19)
						{
							global::Char.myCharz().countKill = (int)msg.reader().readUnsignedShort();
							global::Char.myCharz().countKillMax = (int)msg.reader().readUnsignedShort();
							goto IL_A917;
						}
						goto IL_A917;
					}
					GameCanvas.debug("SA87", 2);
					Mob mob16 = null;
					try
					{
						mob16 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
					}
					catch (Exception)
					{
					}
					GameCanvas.debug("SA87x1", 2);
					if (mob16 != null)
					{
						GameCanvas.debug("SA87x2", 2);
						@char = GameScr.findCharInMap(msg.reader().readInt());
						if (@char == null)
						{
							return;
						}
						GameCanvas.debug("SA87x3", 2);
						long num197 = msg.reader().readLong();
						mob16.dame = @char.cHP - num197;
						@char.cHPNew = num197;
						GameCanvas.debug("SA87x4", 2);
						try
						{
							@char.cMP = msg.reader().readLong();
						}
						catch (Exception)
						{
						}
						GameCanvas.debug("SA87x5", 2);
						if (mob16.isBusyAttackSomeOne)
						{
							@char.doInjure(mob16.dame, 0L, false, true);
						}
						else
						{
							mob16.setAttack(@char);
						}
						GameCanvas.debug("SA87x6", 2);
					}
				}
			}
			else if (b <= 45)
			{
				if (b != 44)
				{
					if (b == 45)
					{
						GameCanvas.debug("SA84", 2);
						Mob mob17 = null;
						try
						{
							mob17 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						}
						catch (Exception ex9)
						{
							Cout.println("Loi tai NPC_MISS  " + ex9.ToString());
						}
						if (mob17 != null)
						{
							mob17.hp = msg.reader().readLong();
							mob17.updateHp_bar();
							GameScr.startFlyText(mResources.miss, mob17.x, mob17.y - mob17.h, 0, -2, mFont.MISS);
						}
					}
				}
				else
				{
					GameCanvas.debug("SA91", 2);
					int num198 = msg.reader().readInt();
					string text14 = msg.reader().readUTF();
					Res.outz(string.Concat(new object[]
					{
						"user id= ",
						num198,
						" text= ",
						text14
					}));
					@char = ((global::Char.myCharz().charID != num198) ? GameScr.findCharInMap(num198) : global::Char.myCharz());
					if (@char == null)
					{
						return;
					}
					@char.addInfo(text14);
				}
			}
			else if (b == 66)
			{
				Res.outz("ME DIE XP DOWN NOT IMPLEMENT YET!!!!!!!!!!!!!!!!!!!!!!!!!!");
			}
			else if (b != 74)
			{
				switch (b)
				{
				case 95:
				{
					GameCanvas.debug("SA77", 22);
					int num199 = msg.reader().readInt();
					global::Char.myCharz().xu += (long)num199;
					global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
					GameScr.startFlyText((num199 <= 0) ? (string.Empty + num199) : ("+" + num199), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
					break;
				}
				case 96:
					GameCanvas.debug("SA77a", 22);
					global::Char.myCharz().taskOrders.addElement(new TaskOrder(msg.reader().readByte(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readByte(), msg.reader().readByte()));
					break;
				case 97:
				{
					sbyte b65 = msg.reader().readByte();
					for (int num200 = 0; num200 < global::Char.myCharz().taskOrders.size(); num200++)
					{
						TaskOrder taskOrder = (TaskOrder)global::Char.myCharz().taskOrders.elementAt(num200);
						if (taskOrder.taskId == (int)b65)
						{
							taskOrder.count = (int)msg.reader().readShort();
							break;
						}
					}
					break;
				}
				}
			}
			else
			{
				GameCanvas.debug("SA85", 2);
				Mob mob18 = null;
				try
				{
					mob18 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				}
				catch (Exception)
				{
					Cout.println("Loi tai NPC CHANGE " + msg.command);
				}
				if (mob18 != null && mob18.status != 0 && mob18.status != 0)
				{
					mob18.status = 0;
					ServerEffect.addServerEffect(60, mob18.x, mob18.y, 1);
					ItemMap itemMap5 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob18.x, mob18.y, (int)msg.reader().readShort(), (int)msg.reader().readShort());
					GameScr.vItemMap.addElement(itemMap5);
					if (Res.abs(itemMap5.y - global::Char.myCharz().cy) < 24 && Res.abs(itemMap5.x - global::Char.myCharz().cx) < 24)
					{
						global::Char.myCharz().charFocus = null;
					}
				}
			}
			IL_A917:
			GameCanvas.debug("SA92", 2);
		}
		catch (Exception ex10)
		{
			Res.err(string.Concat(new object[]
			{
				"[Controller] [error] ",
				ex10.StackTrace,
				" msg: ",
				ex10.Message,
				" cause ",
				ex10.Data
			}));
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x0003C404 File Offset: 0x0003A604
	private void readLogin(Message msg)
	{
		sbyte b = msg.reader().readByte();
		ChooseCharScr.playerData = new PlayerData[(int)b];
		Res.outz("[LEN] sl nguoi choi " + b);
		for (int i = 0; i < (int)b; i++)
		{
			int playerID = msg.reader().readInt();
			string name = msg.reader().readUTF();
			short head = msg.reader().readShort();
			short body = msg.reader().readShort();
			short leg = msg.reader().readShort();
			long ppoint = msg.reader().readLong();
			ChooseCharScr.playerData[i] = new PlayerData(playerID, name, head, body, leg, ppoint);
		}
		GameCanvas.chooseCharScr.switchToMe();
		GameCanvas.chooseCharScr.updateChooseCharacter((byte)b);
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x0003C4CC File Offset: 0x0003A6CC
	private void createSkill(myReader d)
	{
		GameScr.vcSkill = d.readByte();
		GameScr.gI().sOptionTemplates = new SkillOptionTemplate[(int)d.readByte()];
		for (int i = 0; i < GameScr.gI().sOptionTemplates.Length; i++)
		{
			GameScr.gI().sOptionTemplates[i] = new SkillOptionTemplate();
			GameScr.gI().sOptionTemplates[i].id = i;
			GameScr.gI().sOptionTemplates[i].name = d.readUTF();
		}
		GameScr.nClasss = new NClass[(int)d.readByte()];
		for (int j = 0; j < GameScr.nClasss.Length; j++)
		{
			GameScr.nClasss[j] = new NClass();
			GameScr.nClasss[j].classId = j;
			GameScr.nClasss[j].name = d.readUTF();
			GameScr.nClasss[j].skillTemplates = new SkillTemplate[(int)d.readByte()];
			for (int k = 0; k < GameScr.nClasss[j].skillTemplates.Length; k++)
			{
				GameScr.nClasss[j].skillTemplates[k] = new SkillTemplate();
				GameScr.nClasss[j].skillTemplates[k].id = d.readByte();
				GameScr.nClasss[j].skillTemplates[k].name = d.readUTF();
				GameScr.nClasss[j].skillTemplates[k].maxPoint = (int)d.readByte();
				GameScr.nClasss[j].skillTemplates[k].manaUseType = (int)d.readByte();
				GameScr.nClasss[j].skillTemplates[k].type = (int)d.readByte();
				GameScr.nClasss[j].skillTemplates[k].iconId = (int)d.readShort();
				GameScr.nClasss[j].skillTemplates[k].damInfo = d.readUTF();
				int lineWidth = 130;
				if (GameCanvas.w == 128 || GameCanvas.h <= 208)
				{
					lineWidth = 100;
				}
				GameScr.nClasss[j].skillTemplates[k].description = mFont.tahoma_7_green2.splitFontArray(d.readUTF(), lineWidth);
				GameScr.nClasss[j].skillTemplates[k].skills = new Skill[(int)d.readByte()];
				for (int l = 0; l < GameScr.nClasss[j].skillTemplates[k].skills.Length; l++)
				{
					GameScr.nClasss[j].skillTemplates[k].skills[l] = new Skill();
					GameScr.nClasss[j].skillTemplates[k].skills[l].skillId = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].template = GameScr.nClasss[j].skillTemplates[k];
					GameScr.nClasss[j].skillTemplates[k].skills[l].point = (int)d.readByte();
					GameScr.nClasss[j].skillTemplates[k].skills[l].powRequire = d.readLong();
					GameScr.nClasss[j].skillTemplates[k].skills[l].manaUse = (int)d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].coolDown = d.readInt();
					GameScr.nClasss[j].skillTemplates[k].skills[l].dx = (int)d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].dy = (int)d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].maxFight = (int)d.readByte();
					GameScr.nClasss[j].skillTemplates[k].skills[l].damage = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].price = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].moreInfo = d.readUTF();
					Skills.add(GameScr.nClasss[j].skillTemplates[k].skills[l]);
				}
			}
		}
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x0003C904 File Offset: 0x0003AB04
	private void createMap(myReader d)
	{
		GameScr.vcMap = d.readByte();
		TileMap.mapNames = new string[(int)d.readShort()];
		for (int i = 0; i < TileMap.mapNames.Length; i++)
		{
			TileMap.mapNames[i] = d.readUTF();
		}
		Npc.arrNpcTemplate = new NpcTemplate[(int)d.readByte()];
		sbyte b = 0;
		while ((int)b < Npc.arrNpcTemplate.Length)
		{
			Npc.arrNpcTemplate[(int)b] = new NpcTemplate();
			Npc.arrNpcTemplate[(int)b].npcTemplateId = (int)b;
			Npc.arrNpcTemplate[(int)b].name = d.readUTF();
			Npc.arrNpcTemplate[(int)b].headId = (int)d.readShort();
			Npc.arrNpcTemplate[(int)b].bodyId = (int)d.readShort();
			Npc.arrNpcTemplate[(int)b].legId = (int)d.readShort();
			Npc.arrNpcTemplate[(int)b].menu = new string[(int)d.readByte()][];
			for (int j = 0; j < Npc.arrNpcTemplate[(int)b].menu.Length; j++)
			{
				Npc.arrNpcTemplate[(int)b].menu[j] = new string[(int)d.readByte()];
				for (int k = 0; k < Npc.arrNpcTemplate[(int)b].menu[j].Length; k++)
				{
					Npc.arrNpcTemplate[(int)b].menu[j][k] = d.readUTF();
				}
			}
			b = (sbyte)((int)b + 1);
		}
		Mob.arrMobTemplate = new MobTemplate[(int)d.readShort()];
		for (int l = 0; l < Mob.arrMobTemplate.Length; l++)
		{
			Mob.arrMobTemplate[l] = new MobTemplate();
			Mob.arrMobTemplate[l].mobTemplateId = l;
			Mob.arrMobTemplate[l].type = d.readByte();
			Mob.arrMobTemplate[l].name = d.readUTF();
			Mob.arrMobTemplate[l].hp = d.readLong();
			Mob.arrMobTemplate[l].rangeMove = d.readByte();
			Mob.arrMobTemplate[l].speed = d.readByte();
			Mob.arrMobTemplate[l].dartType = d.readByte();
		}
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x0003CB2C File Offset: 0x0003AD2C
	private void createData(myReader d, bool isSaveRMS)
	{
		GameScr.vcData = d.readByte();
		if (isSaveRMS)
		{
			Rms.saveRMS("NR_dart", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_arrow", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_effect", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_image", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_part", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_skill", NinjaUtil.readByteArray(d));
			Rms.DeleteStorage("NRdata");
		}
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x0003CBB4 File Offset: 0x0003ADB4
	private Image createImage(sbyte[] arr)
	{
		try
		{
			return Image.createImage(arr, 0, arr.Length);
		}
		catch (Exception ex)
		{
		}
		return null;
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x0003CBEC File Offset: 0x0003ADEC
	public int[] arrayByte2Int(sbyte[] b)
	{
		int[] array = new int[b.Length];
		for (int i = 0; i < b.Length; i++)
		{
			int num = (int)b[i];
			if (num < 0)
			{
				num += 256;
			}
			array[i] = num;
		}
		return array;
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x0003CC30 File Offset: 0x0003AE30
	public void readClanMsg(Message msg, int index)
	{
		try
		{
			ClanMessage clanMessage = new ClanMessage();
			sbyte b = msg.reader().readByte();
			clanMessage.type = (int)b;
			clanMessage.id = msg.reader().readInt();
			clanMessage.playerId = msg.reader().readInt();
			clanMessage.playerName = msg.reader().readUTF();
			clanMessage.role = msg.reader().readByte();
			clanMessage.time = (long)(msg.reader().readInt() + 1000000000);
			bool flag = false;
			GameScr.isNewClanMessage = false;
			if ((int)b == 0)
			{
				string text = msg.reader().readUTF();
				GameScr.isNewClanMessage = true;
				if (mFont.tahoma_7.getWidth(text) > Panel.WIDTH_PANEL - 60)
				{
					clanMessage.chat = mFont.tahoma_7.splitFontArray(text, Panel.WIDTH_PANEL - 10);
				}
				else
				{
					clanMessage.chat = new string[1];
					clanMessage.chat[0] = text;
				}
				clanMessage.color = msg.reader().readByte();
			}
			else if ((int)b == 1)
			{
				clanMessage.recieve = (int)msg.reader().readByte();
				clanMessage.maxCap = (int)msg.reader().readByte();
				flag = ((int)msg.reader().readByte() == 1);
				if (flag)
				{
					GameScr.isNewClanMessage = true;
				}
				if (clanMessage.playerId != global::Char.myCharz().charID)
				{
					if (clanMessage.recieve < clanMessage.maxCap)
					{
						clanMessage.option = new string[]
						{
							mResources.donate
						};
					}
					else
					{
						clanMessage.option = null;
					}
				}
				if (GameCanvas.panel.cp != null)
				{
					GameCanvas.panel.updateRequest(clanMessage.recieve, clanMessage.maxCap);
				}
			}
			else if ((int)b == 2 && (int)global::Char.myCharz().role == 0)
			{
				GameScr.isNewClanMessage = true;
				clanMessage.option = new string[]
				{
					mResources.CANCEL,
					mResources.receive
				};
			}
			if (GameCanvas.currentScreen != GameScr.instance)
			{
				GameScr.isNewClanMessage = false;
			}
			else if (GameCanvas.panel.isShow && GameCanvas.panel.type == 0 && GameCanvas.panel.currentTabIndex == 3)
			{
				GameScr.isNewClanMessage = false;
			}
			ClanMessage.addMessage(clanMessage, index, flag);
		}
		catch (Exception ex)
		{
			Cout.println("LOI TAI CMD -= " + msg.command);
		}
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x0003CECC File Offset: 0x0003B0CC
	public void loadCurrMap(sbyte teleport3)
	{
		Res.outz("[CONTROLER] start load map " + teleport3);
		GameScr.gI().auto = 0;
		GameScr.isChangeZone = false;
		CreateCharScr.instance = null;
		GameScr.info1.isUpdate = false;
		GameScr.info2.isUpdate = false;
		GameScr.lockTick = 0;
		GameCanvas.panel.isShow = false;
		SoundMn.gI().stopAll();
		if (!GameScr.isLoadAllData && !CreateCharScr.isCreateChar)
		{
			GameScr.gI().initSelectChar();
		}
		GameScr.loadCamera(false, ((int)teleport3 != 1) ? -1 : global::Char.myCharz().cx, ((int)teleport3 != 0) ? 0 : -1);
		TileMap.loadMainTile();
		TileMap.loadMap(TileMap.tileID);
		Res.outz("LOAD GAMESCR 2");
		global::Char.myCharz().cvx = 0;
		global::Char.myCharz().statusMe = 4;
		global::Char.myCharz().currentMovePoint = null;
		global::Char.myCharz().mobFocus = null;
		global::Char.myCharz().charFocus = null;
		global::Char.myCharz().npcFocus = null;
		global::Char.myCharz().itemFocus = null;
		global::Char.myCharz().skillPaint = null;
		global::Char.myCharz().setMabuHold(false);
		global::Char.myCharz().skillPaintRandomPaint = null;
		GameCanvas.clearAllPointerEvent();
		if (global::Char.myCharz().cy >= TileMap.pxh - 100)
		{
			global::Char.myCharz().isFlyUp = true;
			global::Char.myCharz().cx += Res.abs(Res.random(0, 80));
			Service.gI().charMove();
		}
		GameScr.gI().loadGameScr();
		GameCanvas.loadBG(TileMap.bgID);
		global::Char.isLockKey = false;
		Res.outz("cy= " + global::Char.myCharz().cy + "---------------------------------------------");
		for (int i = 0; i < global::Char.myCharz().vEff.size(); i++)
		{
			EffectChar effectChar = (EffectChar)global::Char.myCharz().vEff.elementAt(i);
			if ((int)effectChar.template.type == 10)
			{
				global::Char.isLockKey = true;
				break;
			}
		}
		GameCanvas.clearKeyHold();
		GameCanvas.clearKeyPressed();
		GameScr.gI().dHP = global::Char.myCharz().cHP;
		GameScr.gI().dMP = global::Char.myCharz().cMP;
		global::Char.ischangingMap = false;
		GameScr.gI().switchToMe();
		if (global::Char.myCharz().cy <= 10 && (int)teleport3 != 0 && (int)teleport3 != 2)
		{
			Teleport p = new Teleport(global::Char.myCharz().cx, global::Char.myCharz().cy, global::Char.myCharz().head, global::Char.myCharz().cdir, 1, true, ((int)teleport3 != 1) ? ((int)teleport3) : global::Char.myCharz().cgender);
			Teleport.addTeleport(p);
			global::Char.myCharz().isTeleport = true;
		}
		if ((int)teleport3 == 2)
		{
			global::Char.myCharz().show();
		}
		if (GameScr.gI().isRongThanXuatHien)
		{
			if (TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID)
			{
				GameScr.gI().callRongThan(GameScr.gI().xR, GameScr.gI().yR);
			}
			if (mGraphics.zoomLevel > 1)
			{
				GameScr.gI().doiMauTroi();
			}
		}
		InfoDlg.hide();
		InfoDlg.show(TileMap.mapName, mResources.zone + " " + TileMap.zoneID, 30);
		GameCanvas.endDlg();
		GameCanvas.isLoading = false;
		Hint.clickMob();
		Hint.clickNpc();
		GameCanvas.debug("SA75x9", 2);
		GameCanvas.isRequestMapID = 2;
		GameCanvas.waitingTimeChangeMap = 0L;
		global::Char.isLoadingMap = false;
		Res.outz("[CONTROLLER] loadMap DONE!!!!!!!!!");
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x0003D28C File Offset: 0x0003B48C
	public void loadInfoMap(Message msg)
	{
		try
		{
			if (mGraphics.zoomLevel == 1)
			{
				SmallImage.clearHastable();
			}
			global::Char.myCharz().cx = (global::Char.myCharz().cxSend = (global::Char.myCharz().cxFocus = (int)msg.reader().readShort()));
			global::Char.myCharz().cy = (global::Char.myCharz().cySend = (global::Char.myCharz().cyFocus = (int)msg.reader().readShort()));
			global::Char.myCharz().xSd = global::Char.myCharz().cx;
			global::Char.myCharz().ySd = global::Char.myCharz().cy;
			Res.outz(string.Concat(new object[]
			{
				"head= ",
				global::Char.myCharz().head,
				" body= ",
				global::Char.myCharz().body,
				" left= ",
				global::Char.myCharz().leg,
				" x= ",
				global::Char.myCharz().cx,
				" y= ",
				global::Char.myCharz().cy,
				" chung toc= ",
				global::Char.myCharz().cgender
			}));
			if (global::Char.myCharz().cx >= 0 && global::Char.myCharz().cx <= 100)
			{
				global::Char.myCharz().cdir = 1;
			}
			else if (global::Char.myCharz().cx >= TileMap.tmw - 100 && global::Char.myCharz().cx <= TileMap.tmw)
			{
				global::Char.myCharz().cdir = -1;
			}
			GameCanvas.debug("SA75x4", 2);
			int num = (int)msg.reader().readByte();
			Res.outz("vGo size= " + num);
			if (!GameScr.info1.isDone)
			{
				GameScr.info1.cmx = global::Char.myCharz().cx - GameScr.cmx;
				GameScr.info1.cmy = global::Char.myCharz().cy - GameScr.cmy;
			}
			for (int i = 0; i < num; i++)
			{
				Waypoint waypoint = new Waypoint(msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readUTF());
				if ((TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23) && waypoint.minX >= 0 && waypoint.minX <= 24)
				{
				}
			}
			Resources.UnloadUnusedAssets();
			GameCanvas.debug("SA75x5", 2);
			num = (int)msg.reader().readByte();
			Mob.newMob.removeAllElements();
			sbyte b = 0;
			while ((int)b < num)
			{
				Mob mob = new Mob((int)b, msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), (int)msg.reader().readShort(), (int)msg.reader().readByte(), msg.reader().readLong(), msg.reader().readByte(), msg.reader().readLong(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readByte(), msg.reader().readByte());
				mob.xSd = mob.x;
				mob.ySd = mob.y;
				mob.isBoss = msg.reader().readBoolean();
				if ((int)Mob.arrMobTemplate[mob.templateId].type != 0)
				{
					if ((int)b % 3 == 0)
					{
						mob.dir = -1;
					}
					else
					{
						mob.dir = 1;
					}
					mob.x += 10 - (int)b % 20;
				}
				mob.isMobMe = false;
				BigBoss bigBoss = null;
				BachTuoc bachTuoc = null;
				BigBoss2 bigBoss2 = null;
				NewBoss newBoss = null;
				if (mob.templateId == 70)
				{
					bigBoss = new BigBoss((int)b, (short)mob.x, (short)mob.y, 70, mob.hp, mob.maxHp, mob.sys);
				}
				if (mob.templateId == 71)
				{
					bachTuoc = new BachTuoc((int)b, (short)mob.x, (short)mob.y, 71, mob.hp, mob.maxHp, mob.sys);
				}
				if (mob.templateId == 72)
				{
					bigBoss2 = new BigBoss2((int)b, (short)mob.x, (short)mob.y, 72, mob.hp, mob.maxHp, 3);
				}
				if (mob.isBoss)
				{
					newBoss = new NewBoss((int)b, (short)mob.x, (short)mob.y, mob.templateId, mob.hp, mob.maxHp, mob.sys);
				}
				if (newBoss != null)
				{
					GameScr.vMob.addElement(newBoss);
				}
				else if (bigBoss != null)
				{
					GameScr.vMob.addElement(bigBoss);
				}
				else if (bachTuoc != null)
				{
					GameScr.vMob.addElement(bachTuoc);
				}
				else if (bigBoss2 != null)
				{
					GameScr.vMob.addElement(bigBoss2);
				}
				else
				{
					GameScr.vMob.addElement(mob);
				}
				b = (sbyte)((int)b + 1);
			}
			if (global::Char.myCharz().mobMe != null && GameScr.findMobInMap(global::Char.myCharz().mobMe.mobId) == null)
			{
				global::Char.myCharz().mobMe.getData();
				global::Char.myCharz().mobMe.x = global::Char.myCharz().cx;
				global::Char.myCharz().mobMe.y = global::Char.myCharz().cy - 40;
				GameScr.vMob.addElement(global::Char.myCharz().mobMe);
			}
			num = (int)msg.reader().readByte();
			byte b2 = 0;
			while ((int)b2 < num)
			{
				b2 += 1;
			}
			GameCanvas.debug("SA75x6", 2);
			num = (int)msg.reader().readByte();
			Res.outz("NPC size= " + num);
			for (int j = 0; j < num; j++)
			{
				sbyte b3 = msg.reader().readByte();
				short cx = msg.reader().readShort();
				short num2 = msg.reader().readShort();
				sbyte b4 = msg.reader().readByte();
				short num3 = msg.reader().readShort();
				if ((int)b4 != 6)
				{
					if ((global::Char.myCharz().taskMaint.taskId >= 7 && (global::Char.myCharz().taskMaint.taskId != 7 || global::Char.myCharz().taskMaint.index > 1)) || ((int)b4 != 7 && (int)b4 != 8 && (int)b4 != 9))
					{
						if (global::Char.myCharz().taskMaint.taskId >= 6 || (int)b4 != 16)
						{
							if ((int)b4 == 4)
							{
								GameScr.gI().magicTree = new MagicTree(j, (int)b3, (int)cx, (int)num2, (int)b4, (int)num3);
								Service.gI().magicTree(2);
								GameScr.vNpc.addElement(GameScr.gI().magicTree);
							}
							else
							{
								Npc o = new Npc(j, (int)b3, (int)cx, (int)(num2 + 3), (int)b4, (int)num3);
								GameScr.vNpc.addElement(o);
							}
						}
					}
				}
			}
			GameCanvas.debug("SA75x7", 2);
			num = (int)msg.reader().readByte();
			string text = string.Empty;
			Res.outz("item size = " + num);
			if (mSystem.isTest)
			{
				text = text + "item: " + num;
			}
			for (int k = 0; k < num; k++)
			{
				short itemMapID = msg.reader().readShort();
				short num4 = msg.reader().readShort();
				int x = (int)msg.reader().readShort();
				int y = (int)msg.reader().readShort();
				int num5 = msg.reader().readInt();
				short r = 0;
				if (num5 == -2)
				{
					r = msg.reader().readShort();
				}
				ItemMap itemMap = new ItemMap(num5, itemMapID, num4, x, y, r);
				bool flag = false;
				for (int l = 0; l < GameScr.vItemMap.size(); l++)
				{
					ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(l);
					if (itemMap2.itemMapID == itemMap.itemMapID)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					GameScr.vItemMap.addElement(itemMap);
				}
				if (mSystem.isTest)
				{
					text = text + num4 + ",";
				}
			}
			Res.err("sl item on map " + text + "\n");
			TileMap.vCurrItem.removeAllElements();
			if (mGraphics.zoomLevel == 1)
			{
				BgItem.clearHashTable();
			}
			BgItem.vKeysNew.removeAllElements();
			if (!GameCanvas.lowGraphic || (GameCanvas.lowGraphic && TileMap.isVoDaiMap()) || TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 47 || TileMap.mapID == 48 || TileMap.mapID == 120 || TileMap.mapID == 128 || TileMap.mapID == 170 || TileMap.mapID == 49)
			{
				short num6 = msg.reader().readShort();
				text = (mSystem.isTest ? "item high graphic: " : string.Empty);
				for (int m = 0; m < (int)num6; m++)
				{
					short num7 = msg.reader().readShort();
					short num8 = msg.reader().readShort();
					short num9 = msg.reader().readShort();
					BgItem bibyId = TileMap.getBIById((int)num7);
					if (bibyId != null)
					{
						BgItem bgItem = new BgItem();
						bgItem.id = (int)num7;
						bgItem.idImage = bibyId.idImage;
						bgItem.dx = bibyId.dx;
						bgItem.dy = bibyId.dy;
						bgItem.x = (int)num8 * (int)TileMap.size;
						bgItem.y = (int)num9 * (int)TileMap.size;
						bgItem.layer = bibyId.layer;
						if (TileMap.isExistMoreOne(bgItem.id))
						{
							bgItem.trans = ((m % 2 != 0) ? 2 : 0);
							if (TileMap.mapID == 45)
							{
								bgItem.trans = 0;
							}
						}
						string strIdImage = bgItem.idImage.ToString();
						if (!BgItem.imgNew.containsKey(strIdImage))
						{
							if (mGraphics.zoomLevel == 1)
							{
								Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage + ".png");
								if (image == null)
								{
									image = Image.createRGBImage(new int[1], 1, 1, true);
									Service.gI().getBgTemplate(bgItem.idImage);
								}
								BgItem.imgNew.put(strIdImage, image);
							}
							else
							{
								bool flag2 = false;
								sbyte[] array = Rms.loadRMS(mGraphics.zoomLevel + "bgItem" + bgItem.idImage);
								if (array != null)
								{
									if (BgItem.newSmallVersion != null)
									{
										Res.outz(string.Concat(new object[]
										{
											"Small  last= ",
											array.Length % 127,
											"new Version= ",
											BgItem.newSmallVersion[(int)bgItem.idImage]
										}));
										if (array.Length % 127 != (int)BgItem.newSmallVersion[(int)bgItem.idImage])
										{
											flag2 = true;
										}
									}
									if (!flag2)
									{
										Image image = Image.createImage(array, 0, array.Length);
										if (image != null)
										{
											BgItem.imgNew.put(strIdImage, image);
										}
										else
										{
											flag2 = true;
										}
									}
								}
								else
								{
									flag2 = true;
								}
								if (flag2)
								{
									Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage + ".png");
									if (image == null)
									{
										image = Image.createRGBImage(new int[1], 1, 1, true);
										Service.gI().getBgTemplate(bgItem.idImage);
									}
									BgItem.imgNew.put(strIdImage, image);
								}
							}
							BgItem.vKeysLast.addElement(strIdImage);
						}
						if (!BgItem.isExistKeyNews(strIdImage))
						{
							BgItem.vKeysNew.addElement(strIdImage);
						}
						bgItem.changeColor();
						TileMap.vCurrItem.addElement(bgItem);
					}
					if (mSystem.isTest)
					{
						text = text + num7 + ",";
					}
				}
				Res.err("item High Graphics: " + text);
				for (int n = 0; n < BgItem.vKeysLast.size(); n++)
				{
					string text2 = (string)BgItem.vKeysLast.elementAt(n);
					if (!BgItem.isExistKeyNews(text2))
					{
						BgItem.imgNew.remove(text2);
						if (BgItem.imgNew.containsKey(text2 + "blend" + 1))
						{
							BgItem.imgNew.remove(text2 + "blend" + 1);
						}
						if (BgItem.imgNew.containsKey(text2 + "blend" + 3))
						{
							BgItem.imgNew.remove(text2 + "blend" + 3);
						}
						BgItem.vKeysLast.removeElementAt(n);
						n--;
					}
				}
				BackgroudEffect.isFog = false;
				BackgroudEffect.nCloud = 0;
				EffecMn.vEff.removeAllElements();
				BackgroudEffect.vBgEffect.removeAllElements();
				Effect.newEff.removeAllElements();
				short num10 = msg.reader().readShort();
				for (int num11 = 0; num11 < (int)num10; num11++)
				{
					string key = msg.reader().readUTF();
					string value = msg.reader().readUTF();
					this.keyValueAction(key, value);
				}
			}
			else
			{
				short num12 = msg.reader().readShort();
				for (int num13 = 0; num13 < (int)num12; num13++)
				{
					short num14 = msg.reader().readShort();
					short num15 = msg.reader().readShort();
					short num16 = msg.reader().readShort();
				}
				short num17 = msg.reader().readShort();
				for (int num18 = 0; num18 < (int)num17; num18++)
				{
					string text3 = msg.reader().readUTF();
					string text4 = msg.reader().readUTF();
				}
			}
			TileMap.bgType = (int)msg.reader().readByte();
			sbyte teleport = msg.reader().readByte();
			this.loadCurrMap(teleport);
			GameCanvas.debug("SA75x8", 2);
		}
		catch (Exception ex)
		{
			Res.err(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Loadmap khong thanh cong");
			GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
			ServerListScreen.waitToLogin = true;
			GameCanvas.endDlg();
		}
		GameCanvas.isLoading = false;
		Res.err(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Loadmap thanh cong");
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x0003E278 File Offset: 0x0003C478
	public void keyValueAction(string key, string value)
	{
		if (key.Equals("eff"))
		{
			if (Panel.graphics > 0)
			{
				return;
			}
			string[] array = Res.split(value, ".", 0);
			int id = int.Parse(array[0]);
			int layer = int.Parse(array[1]);
			int x = int.Parse(array[2]);
			int y = int.Parse(array[3]);
			int loop;
			int loopCount;
			if (array.Length <= 4)
			{
				loop = -1;
				loopCount = 1;
			}
			else
			{
				loop = int.Parse(array[4]);
				loopCount = int.Parse(array[5]);
			}
			Effect effect = new Effect(id, x, y, layer, loop, loopCount);
			if (array.Length > 6)
			{
				effect.typeEff = int.Parse(array[6]);
				if (array.Length > 7)
				{
					effect.indexFrom = int.Parse(array[7]);
					effect.indexTo = int.Parse(array[8]);
				}
			}
			EffecMn.addEff(effect);
		}
		else if (key.Equals("beff"))
		{
			if (Panel.graphics > 1)
			{
				return;
			}
			BackgroudEffect.addEffect(int.Parse(value));
		}
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x0003E380 File Offset: 0x0003C580
	public void messageNotMap(Message msg)
	{
		GameCanvas.debug("SA6", 2);
		try
		{
			sbyte b = msg.reader().readByte();
			Res.outz("---messageNotMap : " + b);
			switch (b)
			{
			case 4:
			{
				GameCanvas.debug("SA8", 2);
				GameCanvas.loginScr.savePass();
				GameScr.isAutoPlay = false;
				GameScr.canAutoPlay = false;
				LoginScr.isUpdateAll = true;
				LoginScr.isUpdateData = true;
				LoginScr.isUpdateMap = true;
				LoginScr.isUpdateSkill = true;
				LoginScr.isUpdateItem = true;
				GameScr.vsData = msg.reader().readByte();
				GameScr.vsMap = msg.reader().readByte();
				GameScr.vsSkill = msg.reader().readByte();
				GameScr.vsItem = msg.reader().readByte();
				sbyte b2 = msg.reader().readByte();
				if (GameCanvas.loginScr.isLogin2)
				{
					Rms.saveRMSString("acc", string.Empty);
					Rms.saveRMSString("pass", string.Empty);
				}
				else
				{
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, string.Empty);
				}
				if ((int)GameScr.vsData != (int)GameScr.vcData)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateData();
				}
				else
				{
					try
					{
						LoginScr.isUpdateData = false;
					}
					catch (Exception ex)
					{
						GameScr.vcData = -1;
						Service.gI().updateData();
					}
				}
				if ((int)GameScr.vsMap != (int)GameScr.vcMap)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateMap();
				}
				else
				{
					try
					{
						if (!GameScr.isLoadAllData)
						{
							DataInputStream dataInputStream = new DataInputStream(Rms.loadRMS("NRmap"));
							this.createMap(dataInputStream.r);
						}
						LoginScr.isUpdateMap = false;
					}
					catch (Exception ex2)
					{
						GameScr.vcMap = -1;
						Service.gI().updateMap();
					}
				}
				if ((int)GameScr.vsSkill != (int)GameScr.vcSkill)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateSkill();
				}
				else
				{
					try
					{
						if (!GameScr.isLoadAllData)
						{
							DataInputStream dataInputStream2 = new DataInputStream(Rms.loadRMS("NRskill"));
							this.createSkill(dataInputStream2.r);
						}
						LoginScr.isUpdateSkill = false;
					}
					catch (Exception ex3)
					{
						GameScr.vcSkill = -1;
						Service.gI().updateSkill();
					}
				}
				if ((int)GameScr.vsItem != (int)GameScr.vcItem)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateItem();
				}
				else
				{
					try
					{
						DataInputStream dataInputStream3 = new DataInputStream(Rms.loadRMS("NRitem0"));
						this.loadItemNew(dataInputStream3.r, 0, false);
						DataInputStream dataInputStream4 = new DataInputStream(Rms.loadRMS("NRitem1"));
						this.loadItemNew(dataInputStream4.r, 1, false);
						DataInputStream dataInputStream5 = new DataInputStream(Rms.loadRMS("NRitem100"));
						this.loadItemNew(dataInputStream5.r, 100, false);
						LoginScr.isUpdateItem = false;
					}
					catch (Exception ex4)
					{
						GameScr.vcItem = -1;
						Service.gI().updateItem();
					}
					try
					{
						DataInputStream dataInputStream6 = new DataInputStream(Rms.loadRMS("NRitem101"));
						this.loadItemNew(dataInputStream6.r, 101, false);
					}
					catch (Exception ex5)
					{
					}
				}
				if (!GameScr.isLoadAllData)
				{
					GameScr.gI().readOk();
				}
				else
				{
					Service.gI().clientOk();
				}
				sbyte b3 = msg.reader().readByte();
				Res.outz("CAPTION LENT= " + b3);
				GameScr.exps = new long[(int)b3];
				for (int i = 0; i < GameScr.exps.Length; i++)
				{
					GameScr.exps[i] = msg.reader().readLong();
				}
				break;
			}
			default:
				switch (b)
				{
				case 35:
					GameCanvas.endDlg();
					GameScr.gI().resetButton();
					GameScr.info1.addInfo(msg.reader().readUTF(), 0);
					break;
				case 36:
					GameScr.typeActive = msg.reader().readByte();
					Res.outz("load Me Active: " + GameScr.typeActive);
					break;
				}
				break;
			case 6:
			{
				Res.outz("GET UPDATE_MAP " + msg.reader().available() + " bytes");
				msg.reader().mark(500000);
				this.createMap(msg.reader());
				msg.reader().reset();
				sbyte[] data = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref data);
				Rms.saveRMS("NRmap", data);
				sbyte[] data2 = new sbyte[]
				{
					GameScr.vcMap
				};
				Rms.saveRMS("NRmapVersion", data2);
				LoginScr.isUpdateMap = false;
				GameScr.gI().readOk();
				break;
			}
			case 7:
			{
				Res.outz("GET UPDATE_SKILL " + msg.reader().available() + " bytes");
				msg.reader().mark(500000);
				this.createSkill(msg.reader());
				msg.reader().reset();
				sbyte[] data3 = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref data3);
				Rms.saveRMS("NRskill", data3);
				sbyte[] data4 = new sbyte[]
				{
					GameScr.vcSkill
				};
				Rms.saveRMS("NRskillVersion", data4);
				LoginScr.isUpdateSkill = false;
				GameScr.gI().readOk();
				break;
			}
			case 8:
				Res.outz("GET UPDATE_ITEM " + msg.reader().available() + " bytes");
				this.createItemNew(msg.reader());
				break;
			case 9:
				GameCanvas.debug("SA11", 2);
				break;
			case 10:
				try
				{
					global::Char.isLoadingMap = true;
					Res.outz("REQUEST MAP TEMPLATE");
					GameCanvas.isLoading = true;
					TileMap.maps = null;
					TileMap.types = null;
					GameCanvas.debug("SA99", 2);
					TileMap.tmw = (int)msg.reader().readByte();
					TileMap.tmh = (int)msg.reader().readByte();
					TileMap.maps = new int[TileMap.tmw * TileMap.tmh];
					Res.err("   M apsize= " + TileMap.tmw * TileMap.tmh);
					for (int j = 0; j < TileMap.maps.Length; j++)
					{
						int num = (int)msg.reader().readByte();
						if (num < 0)
						{
							num += 256;
						}
						TileMap.maps[j] = (int)((ushort)num);
					}
					TileMap.types = new int[TileMap.maps.Length];
					msg = this.messWait;
					this.loadInfoMap(msg);
					try
					{
						sbyte b4 = msg.reader().readByte();
						TileMap.isMapDouble = ((int)b4 != 0);
					}
					catch (Exception ex6)
					{
						Res.err(" 1 LOI TAI CASE REQUEST_MAPTEMPLATE " + ex6.ToString());
					}
				}
				catch (Exception ex7)
				{
					Res.err("2 LOI TAI CASE REQUEST_MAPTEMPLATE " + ex7.ToString());
				}
				msg.cleanup();
				this.messWait.cleanup();
				msg = (this.messWait = null);
				GameScr.gI().switchToMe();
				break;
			case 16:
				MoneyCharge.gI().switchToMe();
				break;
			case 17:
				GameCanvas.debug("SYB123", 2);
				global::Char.myCharz().clearTask();
				break;
			case 18:
			{
				GameCanvas.isLoading = false;
				GameCanvas.endDlg();
				int num2 = msg.reader().readInt();
				GameCanvas.inputDlg.show(mResources.changeNameChar, new Command(mResources.OK, GameCanvas.instance, 88829, num2), TField.INPUT_TYPE_ANY);
				break;
			}
			case 20:
				global::Char.myCharz().cPk = msg.reader().readByte();
				GameScr.info1.addInfo(mResources.PK_NOW + " " + global::Char.myCharz().cPk, 0);
				break;
			}
		}
		catch (Exception ex8)
		{
			Cout.LogError(string.Concat(new object[]
			{
				"LOI TAI messageNotMap=== ",
				msg.command,
				"  >>",
				ex8.StackTrace
			}));
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x0003ECD8 File Offset: 0x0003CED8
	public void messageNotLogin(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			Res.outz("---messageNotLogin : " + b);
			if (b == 2)
			{
				string linkDefault = msg.reader().readUTF();
				Res.outz(">>Get CLIENT_INFO");
				ServerListScreen.linkDefault = linkDefault;
				mSystem.AddIpTest();
				ServerListScreen.getServerList(ServerListScreen.linkDefault);
				try
				{
					sbyte b2 = msg.reader().readByte();
					Panel.CanNapTien = ((int)b2 == 1);
				}
				catch (Exception ex)
				{
				}
				Controller.isGet_CLIENT_INFO = true;
			}
		}
		catch (Exception ex2)
		{
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x0003EDB4 File Offset: 0x0003CFB4
	public void messageSubCommand(Message msg)
	{
		try
		{
			GameCanvas.debug("SA12", 2);
			sbyte b = msg.reader().readByte();
			Res.outz("---messageSubCommand : " + b);
			switch (b)
			{
			case 0:
			{
				GameCanvas.debug("SA21", 2);
				RadarScr.list = new MyVector();
				Teleport.vTeleport.removeAllElements();
				GameScr.vCharInMap.removeAllElements();
				GameScr.vItemMap.removeAllElements();
				global::Char.vItemTime.removeAllElements();
				GameScr.loadImg();
				GameScr.currentCharViewInfo = global::Char.myCharz();
				global::Char.myCharz().charID = msg.reader().readInt();
				global::Char.myCharz().ctaskId = (int)msg.reader().readByte();
				global::Char.myCharz().cgender = (int)msg.reader().readByte();
				global::Char.myCharz().head = (int)msg.reader().readShort();
				global::Char.myCharz().cName = msg.reader().readUTF();
				global::Char.myCharz().cPk = msg.reader().readByte();
				global::Char.myCharz().cTypePk = msg.reader().readByte();
				global::Char.myCharz().cPower = msg.reader().readLong();
				global::Char.myCharz().applyCharLevelPercent();
				global::Char.myCharz().eff5BuffHp = (int)msg.reader().readShort();
				global::Char.myCharz().eff5BuffMp = (int)msg.reader().readShort();
				global::Char.myCharz().nClass = GameScr.nClasss[(int)msg.reader().readByte()];
				global::Char.myCharz().vSkill.removeAllElements();
				global::Char.myCharz().vSkillFight.removeAllElements();
				GameScr.gI().dHP = global::Char.myCharz().cHP;
				GameScr.gI().dMP = global::Char.myCharz().cMP;
				sbyte b2 = msg.reader().readByte();
				for (sbyte b3 = 0; b3 < b2; b3 += 1)
				{
					Skill skill = Skills.get(msg.reader().readShort());
					this.useSkill(skill);
				}
				GameScr.gI().sortSkill();
				GameScr.gI().loadSkillShortcut();
				global::Char.myCharz().xu = msg.reader().readLong();
				global::Char.myCharz().luongKhoa = msg.reader().readInt();
				global::Char.myCharz().luong = msg.reader().readInt();
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
				global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
				global::Char.myCharz().arrItemBody = new Item[(int)msg.reader().readByte()];
				try
				{
					global::Char.myCharz().setDefaultPart();
					for (int i = 0; i < global::Char.myCharz().arrItemBody.Length; i++)
					{
						short num = msg.reader().readShort();
						if (num != -1)
						{
							ItemTemplate itemTemplate = ItemTemplates.get(num);
							int type = (int)itemTemplate.type;
							global::Char.myCharz().arrItemBody[i] = new Item();
							global::Char.myCharz().arrItemBody[i].template = itemTemplate;
							global::Char.myCharz().arrItemBody[i].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBody[i].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBody[i].content = msg.reader().readUTF();
							int num2 = (int)msg.reader().readUnsignedByte();
							if (num2 != 0)
							{
								global::Char.myCharz().arrItemBody[i].itemOption = new ItemOption[num2];
								for (int j = 0; j < global::Char.myCharz().arrItemBody[i].itemOption.Length; j++)
								{
									ItemOption itemOption = this.readItemOption(msg);
									if (itemOption != null)
									{
										global::Char.myCharz().arrItemBody[i].itemOption[j] = itemOption;
									}
								}
							}
							if (type == 0)
							{
								Res.outz("toi day =======================================" + global::Char.myCharz().body);
								global::Char.myCharz().body = (int)global::Char.myCharz().arrItemBody[i].template.part;
							}
							else if (type == 1)
							{
								global::Char.myCharz().leg = (int)global::Char.myCharz().arrItemBody[i].template.part;
								Res.outz("toi day =======================================" + global::Char.myCharz().leg);
							}
						}
					}
				}
				catch (Exception)
				{
				}
				global::Char.myCharz().arrItemBag = new Item[(int)msg.reader().readByte()];
				GameScr.hpPotion = 0;
				GameScr.isudungCapsun4 = false;
				GameScr.isudungCapsun3 = false;
				for (int k = 0; k < global::Char.myCharz().arrItemBag.Length; k++)
				{
					short num3 = msg.reader().readShort();
					if (num3 != -1)
					{
						global::Char.myCharz().arrItemBag[k] = new Item();
						global::Char.myCharz().arrItemBag[k].template = ItemTemplates.get(num3);
						global::Char.myCharz().arrItemBag[k].quantity = msg.reader().readInt();
						global::Char.myCharz().arrItemBag[k].info = msg.reader().readUTF();
						global::Char.myCharz().arrItemBag[k].content = msg.reader().readUTF();
						global::Char.myCharz().arrItemBag[k].indexUI = k;
						sbyte b4 = msg.reader().readByte();
						if (b4 != 0)
						{
							global::Char.myCharz().arrItemBag[k].itemOption = new ItemOption[(int)b4];
							for (int l = 0; l < global::Char.myCharz().arrItemBag[k].itemOption.Length; l++)
							{
								ItemOption itemOption2 = this.readItemOption(msg);
								if (itemOption2 != null)
								{
									global::Char.myCharz().arrItemBag[k].itemOption[l] = itemOption2;
									global::Char.myCharz().arrItemBag[k].getCompare();
								}
							}
						}
						if (global::Char.myCharz().arrItemBag[k].template.type == 6)
						{
							GameScr.hpPotion += global::Char.myCharz().arrItemBag[k].quantity;
						}
						if (num3 == 194)
						{
							GameScr.isudungCapsun4 = (global::Char.myCharz().arrItemBag[k].quantity > 0);
						}
						else if (num3 == 193 && !GameScr.isudungCapsun4)
						{
							GameScr.isudungCapsun3 = (global::Char.myCharz().arrItemBag[k].quantity > 0);
						}
					}
				}
				global::Char.myCharz().arrItemBox = new Item[(int)msg.reader().readByte()];
				GameCanvas.panel.hasUse = 0;
				for (int m = 0; m < global::Char.myCharz().arrItemBox.Length; m++)
				{
					short num4 = msg.reader().readShort();
					if (num4 != -1)
					{
						global::Char.myCharz().arrItemBox[m] = new Item();
						global::Char.myCharz().arrItemBox[m].template = ItemTemplates.get(num4);
						global::Char.myCharz().arrItemBox[m].quantity = msg.reader().readInt();
						global::Char.myCharz().arrItemBox[m].info = msg.reader().readUTF();
						global::Char.myCharz().arrItemBox[m].content = msg.reader().readUTF();
						global::Char.myCharz().arrItemBox[m].itemOption = new ItemOption[(int)msg.reader().readByte()];
						for (int n = 0; n < global::Char.myCharz().arrItemBox[m].itemOption.Length; n++)
						{
							ItemOption itemOption3 = this.readItemOption(msg);
							if (itemOption3 != null)
							{
								global::Char.myCharz().arrItemBox[m].itemOption[n] = itemOption3;
								global::Char.myCharz().arrItemBox[m].getCompare();
							}
						}
						GameCanvas.panel.hasUse++;
					}
				}
				global::Char.myCharz().statusMe = 4;
				if (Rms.loadRMSInt(global::Char.myCharz().cName + "vci") < 1)
				{
					GameScr.isViewClanInvite = false;
				}
				else
				{
					GameScr.isViewClanInvite = true;
				}
				short num5 = msg.reader().readShort();
				global::Char.idHead = new short[(int)num5];
				global::Char.idAvatar = new short[(int)num5];
				for (int num6 = 0; num6 < (int)num5; num6++)
				{
					global::Char.idHead[num6] = msg.reader().readShort();
					global::Char.idAvatar[num6] = msg.reader().readShort();
				}
				for (int num7 = 0; num7 < GameScr.info1.charId.Length; num7++)
				{
					GameScr.info1.charId[num7] = new int[3];
				}
				GameScr.info1.charId[global::Char.myCharz().cgender][0] = (int)msg.reader().readShort();
				GameScr.info1.charId[global::Char.myCharz().cgender][1] = (int)msg.reader().readShort();
				GameScr.info1.charId[global::Char.myCharz().cgender][2] = (int)msg.reader().readShort();
				global::Char.myCharz().isNhapThe = (msg.reader().readByte() == 1);
				Res.outz("NHAP THE= " + global::Char.myCharz().isNhapThe.ToString());
				GameScr.deltaTime = mSystem.currentTimeMillis() - (long)msg.reader().readInt() * 1000L;
				GameScr.isNewMember = msg.reader().readByte();
				Service.gI().updateCaption((sbyte)global::Char.myCharz().cgender);
				Service.gI().androidPack();
				try
				{
					global::Char.myCharz().idAuraEff = msg.reader().readShort();
					global::Char.myCharz().idEff_Set_Item = (short)msg.reader().readSByte();
					global::Char.myCharz().idHat = msg.reader().readShort();
					goto IL_1918;
				}
				catch (Exception)
				{
					goto IL_1918;
				}
				break;
			}
			case 1:
				GameCanvas.debug("SA13", 2);
				global::Char.myCharz().nClass = GameScr.nClasss[(int)msg.reader().readByte()];
				global::Char.myCharz().cTiemNang = msg.reader().readLong();
				global::Char.myCharz().vSkill.removeAllElements();
				global::Char.myCharz().vSkillFight.removeAllElements();
				global::Char.myCharz().myskill = null;
				goto IL_1918;
			case 2:
				break;
			case 3:
			case 16:
			case 17:
			case 18:
			case 20:
			case 22:
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
			case 30:
			case 31:
			case 32:
			case 33:
			case 34:
				goto IL_16C7;
			case 4:
				GameCanvas.debug("SA23", 2);
				global::Char.myCharz().xu = msg.reader().readLong();
				global::Char.myCharz().luong = msg.reader().readInt();
				global::Char.myCharz().cHP = msg.reader().readLong();
				global::Char.myCharz().cMP = msg.reader().readLong();
				global::Char.myCharz().luongKhoa = msg.reader().readInt();
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
				global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
				goto IL_1918;
			case 5:
			{
				GameCanvas.debug("SA24", 2);
				long cHP = global::Char.myCharz().cHP;
				global::Char.myCharz().cHP = msg.reader().readLong();
				if (global::Char.myCharz().cHP > cHP && global::Char.myCharz().cTypePk != 4)
				{
					GameScr.startFlyText(string.Concat(new object[]
					{
						"+",
						global::Char.myCharz().cHP - cHP,
						" ",
						mResources.HP
					}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 20, 0, -1, mFont.HP);
					SoundMn.gI().HP_MPup();
					if (global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5003)
					{
						MonsterDart.addMonsterDart(global::Char.myCharz().petFollow.cmx + ((global::Char.myCharz().petFollow.dir != 1) ? -10 : 10), global::Char.myCharz().petFollow.cmy + 10, true, -1L, -1L, global::Char.myCharz(), 29);
					}
				}
				if (global::Char.myCharz().cHP < cHP)
				{
					GameScr.startFlyText(string.Concat(new object[]
					{
						"-",
						cHP - global::Char.myCharz().cHP,
						" ",
						mResources.HP
					}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 20, 0, -1, mFont.HP);
				}
				GameScr.gI().dHP = global::Char.myCharz().cHP;
				if (!GameScr.isPaintInfoMe)
				{
					goto IL_1918;
				}
				goto IL_1918;
			}
			case 6:
			{
				GameCanvas.debug("SA25", 2);
				if (global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5)
				{
					goto IL_1918;
				}
				long cMP = global::Char.myCharz().cMP;
				global::Char.myCharz().cMP = msg.reader().readLong();
				if (global::Char.myCharz().cMP > cMP)
				{
					GameScr.startFlyText(string.Concat(new object[]
					{
						"+",
						global::Char.myCharz().cMP - cMP,
						" ",
						mResources.KI
					}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 23, 0, -2, mFont.MP);
					SoundMn.gI().HP_MPup();
					if (global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5001)
					{
						MonsterDart.addMonsterDart(global::Char.myCharz().petFollow.cmx + ((global::Char.myCharz().petFollow.dir != 1) ? -10 : 10), global::Char.myCharz().petFollow.cmy + 10, true, -1L, -1L, global::Char.myCharz(), 29);
					}
				}
				if (global::Char.myCharz().cMP < cMP)
				{
					GameScr.startFlyText(string.Concat(new object[]
					{
						"-",
						cMP - global::Char.myCharz().cMP,
						" ",
						mResources.KI
					}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 23, 0, -2, mFont.MP);
				}
				Res.outz("curr MP= " + global::Char.myCharz().cMP);
				GameScr.gI().dMP = global::Char.myCharz().cMP;
				if (!GameScr.isPaintInfoMe)
				{
					goto IL_1918;
				}
				goto IL_1918;
			}
			case 7:
			{
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.clanID = msg.reader().readInt();
					if (@char.clanID == -2)
					{
						@char.isCopy = true;
					}
					this.readCharInfo(@char, msg);
					try
					{
						@char.idAuraEff = msg.reader().readShort();
						@char.idEff_Set_Item = (short)msg.reader().readSByte();
						@char.idHat = msg.reader().readShort();
						if (@char.bag >= 201)
						{
							@char.addEffChar(new Effect(@char.bag, @char, 2, -1, 10, 1)
							{
								typeEff = 5
							});
						}
						else
						{
							@char.removeEffChar(0, 201);
						}
						goto IL_1918;
					}
					catch (Exception)
					{
						goto IL_1918;
					}
					goto IL_1094;
				}
				goto IL_1918;
			}
			case 8:
			{
				GameCanvas.debug("SA26", 2);
				global::Char char2 = GameScr.findCharInMap(msg.reader().readInt());
				if (char2 != null)
				{
					char2.cspeed = (int)msg.reader().readByte();
					goto IL_1918;
				}
				goto IL_1918;
			}
			case 9:
				goto IL_1094;
			case 10:
			{
				GameCanvas.debug("SA28", 2);
				global::Char char3 = GameScr.findCharInMap(msg.reader().readInt());
				if (char3 == null)
				{
					goto IL_1918;
				}
				char3.cHP = msg.reader().readLong();
				char3.cHPFull = msg.reader().readLong();
				char3.eff5BuffHp = (int)msg.reader().readShort();
				char3.eff5BuffMp = (int)msg.reader().readShort();
				char3.wp = (int)msg.reader().readShort();
				if (char3.wp == -1)
				{
					char3.setDefaultWeapon();
					goto IL_1918;
				}
				goto IL_1918;
			}
			case 11:
			{
				GameCanvas.debug("SA29", 2);
				global::Char char4 = GameScr.findCharInMap(msg.reader().readInt());
				if (char4 == null)
				{
					goto IL_1918;
				}
				char4.cHP = msg.reader().readLong();
				char4.cHPFull = msg.reader().readLong();
				char4.eff5BuffHp = (int)msg.reader().readShort();
				char4.eff5BuffMp = (int)msg.reader().readShort();
				char4.body = (int)msg.reader().readShort();
				if (char4.body == -1)
				{
					char4.setDefaultBody();
					goto IL_1918;
				}
				goto IL_1918;
			}
			case 12:
			{
				GameCanvas.debug("SA30", 2);
				global::Char char5 = GameScr.findCharInMap(msg.reader().readInt());
				if (char5 == null)
				{
					goto IL_1918;
				}
				char5.cHP = msg.reader().readLong();
				char5.cHPFull = msg.reader().readLong();
				char5.eff5BuffHp = (int)msg.reader().readShort();
				char5.eff5BuffMp = (int)msg.reader().readShort();
				char5.leg = (int)msg.reader().readShort();
				if (char5.leg == -1)
				{
					char5.setDefaultLeg();
					goto IL_1918;
				}
				goto IL_1918;
			}
			case 13:
			{
				GameCanvas.debug("SA31", 2);
				int num8 = msg.reader().readInt();
				global::Char char6;
				if (num8 == global::Char.myCharz().charID)
				{
					char6 = global::Char.myCharz();
				}
				else
				{
					char6 = GameScr.findCharInMap(num8);
				}
				if (char6 != null)
				{
					char6.cHP = msg.reader().readLong();
					char6.cHPFull = msg.reader().readLong();
					char6.eff5BuffHp = (int)msg.reader().readShort();
					char6.eff5BuffMp = (int)msg.reader().readShort();
					goto IL_1918;
				}
				goto IL_1918;
			}
			case 14:
			{
				GameCanvas.debug("SA32", 2);
				global::Char char7 = GameScr.findCharInMap(msg.reader().readInt());
				if (char7 != null)
				{
					char7.cHP = msg.reader().readLong();
					sbyte b5 = msg.reader().readByte();
					Res.outz("player load hp type= " + b5);
					if (b5 == 1)
					{
						ServerEffect.addServerEffect(11, char7, 5);
						ServerEffect.addServerEffect(104, char7, 4);
					}
					if (b5 == 2)
					{
						char7.doInjure();
					}
					try
					{
						char7.cHPFull = msg.reader().readLong();
						goto IL_1918;
					}
					catch (Exception)
					{
						goto IL_1918;
					}
					goto IL_13CE;
				}
				goto IL_1918;
			}
			case 15:
			{
				GameCanvas.debug("SA33", 2);
				global::Char char8 = GameScr.findCharInMap(msg.reader().readInt());
				if (char8 != null)
				{
					char8.cHP = msg.reader().readLong();
					char8.cHPFull = msg.reader().readLong();
					char8.cx = (int)msg.reader().readShort();
					char8.cy = (int)msg.reader().readShort();
					char8.statusMe = 1;
					char8.cp3 = 3;
					ServerEffect.addServerEffect(109, char8, 2);
					goto IL_1918;
				}
				goto IL_1918;
			}
			case 19:
				goto IL_13CE;
			case 21:
			{
				GameCanvas.debug("SA19", 2);
				int num9 = msg.reader().readInt();
				global::Char.myCharz().xuInBox -= num9;
				global::Char.myCharz().xu += (long)num9;
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				goto IL_1918;
			}
			case 23:
			{
				short num10 = msg.reader().readShort();
				Skill skill2 = Skills.get(num10);
				this.useSkill(skill2);
				if (num10 != 0 && num10 != 14 && num10 != 28)
				{
					GameScr.info1.addInfo(mResources.LEARN_SKILL + " " + skill2.template.name, 0);
					goto IL_1918;
				}
				goto IL_1918;
			}
			case 35:
			{
				GameCanvas.debug("SY3", 2);
				int num11 = msg.reader().readInt();
				Res.outz("CID = " + num11);
				if (TileMap.mapID == 130)
				{
					GameScr.gI().starVS();
				}
				if (num11 == global::Char.myCharz().charID)
				{
					global::Char.myCharz().cTypePk = msg.reader().readByte();
					if (GameScr.gI().isVS() && global::Char.myCharz().cTypePk != 0)
					{
						GameScr.gI().starVS();
					}
					Res.outz("type pk= " + global::Char.myCharz().cTypePk);
					global::Char.myCharz().npcFocus = null;
					if (!GameScr.gI().isMeCanAttackMob(global::Char.myCharz().mobFocus))
					{
						global::Char.myCharz().mobFocus = null;
					}
					global::Char.myCharz().itemFocus = null;
				}
				else
				{
					global::Char char9 = GameScr.findCharInMap(num11);
					if (char9 != null)
					{
						Res.outz("type pk= " + char9.cTypePk);
						char9.cTypePk = msg.reader().readByte();
						if (char9.isAttacPlayerStatus())
						{
							global::Char.myCharz().charFocus = char9;
						}
					}
				}
				for (int num12 = 0; num12 < GameScr.vCharInMap.size(); num12++)
				{
					global::Char char10 = GameScr.findCharInMap(num12);
					if (char10 != null && char10.cTypePk != 0 && char10.cTypePk == global::Char.myCharz().cTypePk)
					{
						if (!global::Char.myCharz().mobFocus.isMobMe)
						{
							global::Char.myCharz().mobFocus = null;
						}
						global::Char.myCharz().npcFocus = null;
						global::Char.myCharz().itemFocus = null;
						break;
					}
				}
				Res.outz("update type pk= ");
				goto IL_1918;
			}
			default:
				goto IL_16C7;
			}
			GameCanvas.debug("SA14", 2);
			if (global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5)
			{
				global::Char.myCharz().cHP = global::Char.myCharz().cHPFull;
				global::Char.myCharz().cMP = global::Char.myCharz().cMPFull;
				Cout.LogError2(" ME_LOAD_SKILL");
			}
			global::Char.myCharz().vSkill.removeAllElements();
			global::Char.myCharz().vSkillFight.removeAllElements();
			sbyte b6 = msg.reader().readByte();
			for (sbyte b7 = 0; b7 < b6; b7 += 1)
			{
				Skill skill3 = Skills.get(msg.reader().readShort());
				this.useSkill(skill3);
			}
			GameScr.gI().sortSkill();
			if (GameScr.isPaintInfoMe)
			{
				GameScr.indexRow = -1;
				GameScr.gI().left = (GameScr.gI().center = null);
				goto IL_1918;
			}
			goto IL_1918;
			IL_1094:
			GameCanvas.debug("SA27", 2);
			global::Char char11 = GameScr.findCharInMap(msg.reader().readInt());
			if (char11 != null)
			{
				char11.cHP = msg.reader().readLong();
				char11.cHPFull = msg.reader().readLong();
				goto IL_1918;
			}
			goto IL_1918;
			IL_13CE:
			GameCanvas.debug("SA17", 2);
			global::Char.myCharz().boxSort();
			goto IL_1918;
			IL_16C7:
			switch (b)
			{
			case 61:
			{
				string text = msg.reader().readUTF();
				sbyte[] array = new sbyte[msg.reader().readInt()];
				msg.reader().read(ref array);
				if (array.Length == 0)
				{
					array = null;
				}
				if (text.Equals("KSkill"))
				{
					GameScr.gI().onKSkill(array);
				}
				else if (text.Equals("OSkill"))
				{
					GameScr.gI().onOSkill(array);
				}
				else if (text.Equals("CSkill"))
				{
					GameScr.gI().onCSkill(array);
				}
				break;
			}
			case 62:
				Res.outz("ME UPDATE SKILL");
				this.read_UpdateSkill(msg);
				break;
			case 63:
			{
				sbyte b8 = msg.reader().readByte();
				if (b8 > 0)
				{
					GameCanvas.panel.vPlayerMenu_id.removeAllElements();
					InfoDlg.showWait();
					MyVector vPlayerMenu = GameCanvas.panel.vPlayerMenu;
					for (int num13 = 0; num13 < (int)b8; num13++)
					{
						string caption = msg.reader().readUTF();
						string caption2 = msg.reader().readUTF();
						short num14 = msg.reader().readShort();
						GameCanvas.panel.vPlayerMenu_id.addElement(num14 + string.Empty);
						global::Char.myCharz().charFocus.menuSelect = (int)num14;
						vPlayerMenu.addElement(new Command(caption, 11115, global::Char.myCharz().charFocus)
						{
							caption2 = caption2
						});
					}
					InfoDlg.hide();
					GameCanvas.panel.setTabPlayerMenu();
				}
				break;
			}
			}
			IL_1918:;
		}
		catch (Exception ex)
		{
			Cout.println("Loi tai Sub : " + ex.ToString());
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x00040794 File Offset: 0x0003E994
	private void useSkill(Skill skill)
	{
		if (global::Char.myCharz().myskill == null)
		{
			global::Char.myCharz().myskill = skill;
		}
		else if (skill.template.Equals(global::Char.myCharz().myskill.template))
		{
			global::Char.myCharz().myskill = skill;
		}
		global::Char.myCharz().vSkill.addElement(skill);
		if ((skill.template.type == 1 || skill.template.type == 4 || skill.template.type == 2 || skill.template.type == 3) && (skill.template.maxPoint == 0 || (skill.template.maxPoint > 0 && skill.point > 0)))
		{
			if ((int)skill.template.id == global::Char.myCharz().skillTemplateId)
			{
				Service.gI().selectSkill(global::Char.myCharz().skillTemplateId);
			}
			global::Char.myCharz().vSkillFight.addElement(skill);
		}
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x000408AC File Offset: 0x0003EAAC
	public bool readCharInfo(global::Char c, Message msg)
	{
		try
		{
			c.clevel = (int)msg.reader().readByte();
			c.isInvisiblez = msg.reader().readBoolean();
			c.cTypePk = msg.reader().readByte();
			Res.outz(string.Concat(new object[]
			{
				"ADD TYPE PK= ",
				c.cTypePk,
				" to player ",
				c.charID,
				" @@ ",
				c.cName
			}));
			c.nClass = GameScr.nClasss[(int)msg.reader().readByte()];
			c.cgender = (int)msg.reader().readByte();
			c.head = (int)msg.reader().readShort();
			c.cName = msg.reader().readUTF();
			c.cHP = msg.reader().readLong();
			c.dHP = c.cHP;
			if (c.cHP == 0L)
			{
				c.statusMe = 14;
			}
			c.cHPFull = msg.reader().readLong();
			if (c.cy >= TileMap.pxh - 100)
			{
				c.isFlyUp = true;
			}
			c.body = (int)msg.reader().readShort();
			c.leg = (int)msg.reader().readShort();
			c.bag = (int)msg.reader().readShort();
			Res.outz(string.Concat(new object[]
			{
				" body= ",
				c.body,
				" leg= ",
				c.leg,
				" bag=",
				c.bag,
				"BAG ==",
				c.bag,
				"*********************************"
			}));
			c.isShadown = true;
			sbyte b = msg.reader().readByte();
			if (c.wp == -1)
			{
				c.setDefaultWeapon();
			}
			if (c.body == -1)
			{
				c.setDefaultBody();
			}
			if (c.leg == -1)
			{
				c.setDefaultLeg();
			}
			c.cx = (int)msg.reader().readShort();
			c.cy = (int)msg.reader().readShort();
			c.xSd = c.cx;
			c.ySd = c.cy;
			c.eff5BuffHp = (int)msg.reader().readShort();
			c.eff5BuffMp = (int)msg.reader().readShort();
			int num = (int)msg.reader().readByte();
			for (int i = 0; i < num; i++)
			{
				EffectChar effectChar = new EffectChar((short)msg.reader().readByte(), msg.reader().readInt(), msg.reader().readInt(), msg.reader().readShort());
				c.vEff.addElement(effectChar);
				if ((int)effectChar.template.type == 12 || (int)effectChar.template.type == 11)
				{
					c.isInvisiblez = true;
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		return false;
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x00040BF4 File Offset: 0x0003EDF4
	private void readGetImgByName(Message msg)
	{
		try
		{
			string name = msg.reader().readUTF();
			sbyte nFrame = msg.reader().readByte();
			sbyte[] array = NinjaUtil.readByteArray(msg);
			Image img = this.createImage(array);
			ImgByName.SetImage(name, img, nFrame);
			if (array != null)
			{
				ImgByName.saveRMS(name, nFrame, array);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x00040C54 File Offset: 0x0003EE54
	private void createItemNew(myReader d)
	{
		try
		{
			this.loadItemNew(d, -1, true);
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x00040C88 File Offset: 0x0003EE88
	private void loadItemNew(myReader d, sbyte type, bool isSave)
	{
		try
		{
			d.mark(1000000);
			GameScr.vcItem = d.readByte();
			type = d.readByte();
			Res.err(string.Concat(new object[]
			{
				GameScr.vcItem,
				":<<GameScr.vcItem >>>>>>loadItemNew: ",
				type,
				"  isSave:",
				isSave
			}));
			if ((int)type == 0)
			{
				GameScr.gI().iOptionTemplates = new ItemOptionTemplate[(int)d.readShort()];
				for (int i = 0; i < GameScr.gI().iOptionTemplates.Length; i++)
				{
					GameScr.gI().iOptionTemplates[i] = new ItemOptionTemplate();
					GameScr.gI().iOptionTemplates[i].id = i;
					GameScr.gI().iOptionTemplates[i].name = d.readUTF();
					GameScr.gI().iOptionTemplates[i].type = (int)d.readByte();
				}
				try
				{
					short num = d.readShort();
					for (int j = 0; j < (int)num; j++)
					{
						short num2 = d.readShort();
						GameScr.gI().iOptionTemplates[(int)num2].color = (int)d.readUnsignedByte();
					}
				}
				catch (Exception ex)
				{
				}
				if (isSave)
				{
					d.reset();
					sbyte[] data = new sbyte[d.available()];
					d.readFully(ref data);
					Rms.saveRMS("NRitem0", data);
				}
			}
			else if ((int)type == 1)
			{
				ItemTemplates.itemTemplates.clear();
				int num3 = (int)d.readShort();
				for (int k = 0; k < num3; k++)
				{
					ItemTemplate it = new ItemTemplate((short)k, d.readByte(), d.readByte(), d.readUTF(), d.readUTF(), d.readByte(), d.readInt(), d.readShort(), d.readShort(), d.readBoolean());
					ItemTemplates.add(it);
				}
				if (isSave)
				{
					d.reset();
					sbyte[] data2 = new sbyte[d.available()];
					d.readFully(ref data2);
					Rms.saveRMS("NRitem1", data2);
					sbyte[] data3 = new sbyte[]
					{
						GameScr.vcItem
					};
					Rms.saveRMS("NRitemVersion", data3);
				}
				LoginScr.isUpdateItem = false;
				GameScr.gI().readOk();
			}
			else if ((int)type != 2)
			{
				if ((int)type == 100)
				{
					global::Char.Arr_Head_2Fr = this.readArrHead(d);
					if (isSave)
					{
						d.reset();
						sbyte[] data4 = new sbyte[d.available()];
						d.readFully(ref data4);
						Rms.saveRMS("NRitem100", data4);
					}
				}
				else if ((int)type == 101)
				{
					try
					{
						int num4 = (int)d.readShort();
						global::Char.Arr_Head_FlyMove = new short[num4];
						for (int l = 0; l < num4; l++)
						{
							short num5 = d.readShort();
							global::Char.Arr_Head_FlyMove[l] = num5;
						}
						if (isSave)
						{
							d.reset();
							sbyte[] data5 = new sbyte[d.available()];
							d.readFully(ref data5);
							Rms.saveRMS("NRitem101", data5);
						}
					}
					catch (Exception ex2)
					{
						global::Char.Arr_Head_FlyMove = new short[0];
					}
				}
			}
		}
		catch (Exception ex3)
		{
			ex3.ToString();
		}
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x00041008 File Offset: 0x0003F208
	private void readFrameBoss(Message msg, int mobTemplateId)
	{
		try
		{
			int num = (int)msg.reader().readByte();
			int[][] array = new int[num][];
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)msg.reader().readByte();
				array[i] = new int[num2];
				for (int j = 0; j < num2; j++)
				{
					array[i][j] = (int)msg.reader().readByte();
				}
			}
			Controller.frameHT_NEWBOSS.put(mobTemplateId + string.Empty, array);
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x000410B0 File Offset: 0x0003F2B0
	private int[][] readArrHead(myReader d)
	{
		int[][] array = new int[][]
		{
			new int[]
			{
				542,
				543
			}
		};
		try
		{
			int num = (int)d.readShort();
			array = new int[num][];
			for (int i = 0; i < array.Length; i++)
			{
				int num2 = (int)d.readByte();
				array[i] = new int[num2];
				for (int j = 0; j < num2; j++)
				{
					array[i][j] = (int)d.readShort();
				}
			}
		}
		catch (Exception ex)
		{
		}
		return array;
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x00041150 File Offset: 0x0003F350
	public void phuban_Info(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if ((int)b == 0)
			{
				this.readPhuBan_CHIENTRUONGNAMEK(msg, (int)b);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x00041194 File Offset: 0x0003F394
	private void readPhuBan_CHIENTRUONGNAMEK(Message msg, int type_PB)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if ((int)b == 0)
			{
				short idmapPaint = msg.reader().readShort();
				string nameTeam = msg.reader().readUTF();
				string nameTeam2 = msg.reader().readUTF();
				int maxPoint = msg.reader().readInt();
				short timeSecond = msg.reader().readShort();
				int maxLife = (int)msg.reader().readByte();
				GameScr.phuban_Info = new InfoPhuBan(type_PB, idmapPaint, nameTeam, nameTeam2, maxPoint, timeSecond);
				GameScr.phuban_Info.maxLife = maxLife;
				GameScr.phuban_Info.updateLife(type_PB, 0, 0);
			}
			else if ((int)b == 1)
			{
				int pointTeam = msg.reader().readInt();
				int pointTeam2 = msg.reader().readInt();
				if (GameScr.phuban_Info != null)
				{
					GameScr.phuban_Info.updatePoint(type_PB, pointTeam, pointTeam2);
				}
			}
			else if ((int)b == 2)
			{
				sbyte b2 = msg.reader().readByte();
				short type = 0;
				if ((int)b2 == 1)
				{
					type = 1;
				}
				else if ((int)b2 == 2)
				{
					type = 2;
				}
				short subtype = -1;
				GameScr.phuban_Info = null;
				GameScr.addEffectEnd((int)type, (int)subtype, 0, GameCanvas.hw, GameCanvas.hh, 0, 0, -1, null);
			}
			else if ((int)b == 5)
			{
				short timeSecond2 = msg.reader().readShort();
				if (GameScr.phuban_Info != null)
				{
					GameScr.phuban_Info.updateTime(type_PB, timeSecond2);
				}
			}
			else if ((int)b == 4)
			{
				int lifeTeam = (int)msg.reader().readByte();
				int lifeTeam2 = (int)msg.reader().readByte();
				if (GameScr.phuban_Info != null)
				{
					GameScr.phuban_Info.updateLife(type_PB, lifeTeam, lifeTeam2);
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x00041368 File Offset: 0x0003F568
	public void read_cmdExtra(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			mSystem.println(">>---read_cmdExtra-sub:" + b);
			if ((int)b == 0)
			{
				short idHat = msg.reader().readShort();
				global::Char.myCharz().idHat = idHat;
				SoundMn.gI().getStrOption();
			}
			else if ((int)b == 2)
			{
				int num = msg.reader().readInt();
				sbyte b2 = msg.reader().readByte();
				short num2 = msg.reader().readShort();
				string v = num2 + "," + b2;
				MainImage imagePath = ImgByName.getImagePath("banner_" + num2, ImgByName.hashImagePath);
				GameCanvas.danhHieu.put(num + string.Empty, v);
			}
			else if ((int)b == 3)
			{
				short num3 = msg.reader().readShort();
				SmallImage.createImage((int)num3);
				BackgroudEffect.id_water1 = num3;
			}
			else if ((int)b == 4)
			{
				string o = msg.reader().readUTF();
				GameCanvas.messageServer.addElement(o);
			}
			else if ((int)b == 5)
			{
				string text = "\n|ChienTruong|Log: ";
				sbyte b3 = msg.reader().readByte();
				if ((int)b3 == 0)
				{
					GameScr.nCT_team = msg.reader().readUTF();
					GameScr.nCT_TeamA = (GameScr.nCT_TeamB = (int)msg.reader().readByte());
					GameScr.nCT_nBoyBaller = GameScr.nCT_TeamA * 2;
					GameScr.isPaint_CT = false;
					string text2 = text;
					text = string.Concat(new object[]
					{
						text2,
						"\tsub    0|  nCT_team= ",
						GameScr.nCT_team,
						"|nCT_TeamA =",
						GameScr.nCT_TeamA,
						"  isPaint_CT=false \n"
					});
				}
				else if ((int)b3 == 1)
				{
					int num4 = msg.reader().readInt();
					sbyte b4 = msg.reader().readByte();
					GameScr.nCT_floor = b4;
					GameScr.nCT_timeBallte = (long)(num4 * 1000) + mSystem.currentTimeMillis();
					GameScr.isPaint_CT = true;
					string text2 = text;
					text = string.Concat(new object[]
					{
						text2,
						"\tsub    1 floor= ",
						b4,
						"|timeBallte= ",
						num4,
						"isPaint_CT=true \n"
					});
				}
				else if ((int)b3 == 2)
				{
					GameScr.nCT_TeamA = (int)msg.reader().readByte();
					GameScr.nCT_TeamB = (int)msg.reader().readByte();
					GameScr.res_CT.removeAllElements();
					sbyte b5 = msg.reader().readByte();
					for (int i = 0; i < (int)b5; i++)
					{
						string text3 = string.Empty;
						text3 = text3 + msg.reader().readByte() + "|";
						text3 = text3 + msg.reader().readUTF() + "|";
						text3 = text3 + msg.reader().readShort() + "|";
						text3 += msg.reader().readInt();
						GameScr.res_CT.addElement(text3);
					}
					string text2 = text;
					text = string.Concat(new object[]
					{
						text2,
						"\tsub   2|  A= ",
						GameScr.nCT_TeamA,
						"|B =",
						GameScr.nCT_TeamB,
						"  isPaint_CT=true \n"
					});
				}
				else if ((int)b3 == 3)
				{
					Service.gI().sendCT_ready(b, b3);
					GameScr.nCT_floor = 0;
					GameScr.nCT_timeBallte = 0L;
					GameScr.isPaint_CT = false;
					text += "\tsub    3|  isPaint_CT=false \n";
				}
				else if ((int)b3 == 4)
				{
					GameScr.nUSER_CT = (int)msg.reader().readByte();
					GameScr.nUSER_MAX_CT = (int)msg.reader().readByte();
				}
				text += "END LOG CT.";
				Res.err(text);
			}
			else
			{
				this.readExtra(b, msg);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x000417A8 File Offset: 0x0003F9A8
	public void read_UpdateSkill(Message msg)
	{
		try
		{
			short num = msg.reader().readShort();
			sbyte b = -1;
			try
			{
				b = msg.reader().readSByte();
			}
			catch (Exception ex)
			{
			}
			if ((int)b == 0)
			{
				short curExp = msg.reader().readShort();
				for (int i = 0; i < global::Char.myCharz().vSkill.size(); i++)
				{
					Skill skill = (Skill)global::Char.myCharz().vSkill.elementAt(i);
					if (skill.skillId == num)
					{
						skill.curExp = curExp;
						break;
					}
				}
			}
			else if ((int)b == 1)
			{
				sbyte b2 = msg.reader().readByte();
				for (int j = 0; j < global::Char.myCharz().vSkill.size(); j++)
				{
					Skill skill2 = (Skill)global::Char.myCharz().vSkill.elementAt(j);
					if (skill2.skillId == num)
					{
						for (int k = 0; k < 20; k++)
						{
							string nameImg = string.Concat(new object[]
							{
								"Skills_",
								skill2.template.id,
								"_",
								b2,
								"_",
								k
							});
							MainImage imagePath = ImgByName.getImagePath(nameImg, ImgByName.hashImagePath);
						}
						break;
					}
				}
			}
			else if ((int)b == -1)
			{
				Skill skill3 = Skills.get(num);
				for (int l = 0; l < global::Char.myCharz().vSkill.size(); l++)
				{
					Skill skill4 = (Skill)global::Char.myCharz().vSkill.elementAt(l);
					if ((int)skill4.template.id == (int)skill3.template.id)
					{
						global::Char.myCharz().vSkill.setElementAt(skill3, l);
						break;
					}
				}
				for (int m = 0; m < global::Char.myCharz().vSkillFight.size(); m++)
				{
					Skill skill5 = (Skill)global::Char.myCharz().vSkillFight.elementAt(m);
					if ((int)skill5.template.id == (int)skill3.template.id)
					{
						global::Char.myCharz().vSkillFight.setElementAt(skill3, m);
						break;
					}
				}
				for (int n = 0; n < GameScr.onScreenSkill.Length; n++)
				{
					if (GameScr.onScreenSkill[n] != null && (int)GameScr.onScreenSkill[n].template.id == (int)skill3.template.id)
					{
						GameScr.onScreenSkill[n] = skill3;
						break;
					}
				}
				for (int num2 = 0; num2 < GameScr.keySkill.Length; num2++)
				{
					if (GameScr.keySkill[num2] != null && (int)GameScr.keySkill[num2].template.id == (int)skill3.template.id)
					{
						GameScr.keySkill[num2] = skill3;
						break;
					}
				}
				if ((int)global::Char.myCharz().myskill.template.id == (int)skill3.template.id)
				{
					global::Char.myCharz().myskill = skill3;
				}
				GameScr.info1.addInfo(string.Concat(new object[]
				{
					mResources.hasJustUpgrade1,
					skill3.template.name,
					mResources.hasJustUpgrade2,
					skill3.point
				}), 0);
			}
		}
		catch (Exception ex2)
		{
		}
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x00041B8C File Offset: 0x0003FD8C
	public void readExtra(sbyte sub, Message msg)
	{
		try
		{
			if ((int)sub == 127)
			{
				GameCanvas.endDlg();
				try
				{
					string text = msg.reader().readUTF();
					ServerListScreen.linkDefault = text;
					mSystem.AddIpTest();
					ServerListScreen.getServerList(ServerListScreen.linkDefault);
					Res.outz(">>>>read.isEXTRA_LINK " + text);
					sbyte b = msg.reader().readByte();
					if ((int)b > 0)
					{
						ServerListScreen.typeClass = new sbyte[(int)b];
						ServerListScreen.listChar = new global::Char[(int)b];
						for (int i = 0; i < (int)b; i++)
						{
							ServerListScreen.typeClass[i] = msg.reader().readByte();
							Res.outz(ServerListScreen.nameServer[i] + ">>>>read.isEXTRA_LINK  typeClass: " + ServerListScreen.typeClass[i]);
							if ((int)ServerListScreen.typeClass[i] > -1)
							{
								ServerListScreen.isHaveChar = true;
								ServerListScreen.listChar[i] = new global::Char();
								ServerListScreen.listChar[i].cgender = (int)ServerListScreen.typeClass[i];
								ServerListScreen.listChar[i].head = (int)msg.reader().readShort();
								ServerListScreen.listChar[i].body = (int)msg.reader().readShort();
								ServerListScreen.listChar[i].leg = (int)msg.reader().readShort();
								ServerListScreen.listChar[i].bag = (int)msg.reader().readShort();
								ServerListScreen.listChar[i].cName = msg.reader().readUTF();
							}
						}
					}
				}
				catch (Exception)
				{
				}
				Controller.isEXTRA_LINK = true;
				ServerListScreen.saveRMS_ExtraLink();
				ServerListScreen.isWait = false;
				global::Char.isLoadingMap = false;
				LoginScr.isContinueToLogin = false;
				ServerListScreen.waitToLogin = false;
				bool flag = false;
				bool flag2 = false;
				try
				{
					if (!Rms.loadRMSString("acc").Equals(string.Empty))
					{
						flag = true;
					}
					if (!Rms.loadRMSString("userAo" + ServerListScreen.ipSelect).Equals(string.Empty))
					{
						flag2 = true;
					}
				}
				catch (Exception)
				{
				}
				if (!ServerListScreen.isHaveChar && !flag && !flag2)
				{
					GameCanvas.serverScreen.Login_New();
				}
				else if (Rms.loadRMSInt(ServerListScreen.RMS_svselect) == -1)
				{
					ServerScr.isShowSv_HaveChar = false;
					GameCanvas.serverScr.switchToMe();
				}
				else
				{
					ServerListScreen.SetIpSelect(Rms.loadRMSInt(ServerListScreen.RMS_svselect), false);
					if (ServerListScreen.listChar != null && ServerListScreen.listChar[ServerListScreen.ipSelect] != null)
					{
						GameCanvas._SelectCharScr.SetInfoChar(ServerListScreen.listChar[ServerListScreen.ipSelect]);
					}
					else
					{
						GameCanvas.serverScreen.Login_New();
					}
				}
			}
		}
		catch (Exception ex)
		{
			Res.outz(">>>>read.isEXTRA_LINK  errr:");
			GameCanvas.serverScr.switchToMe();
		}
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x00041E7C File Offset: 0x0004007C
	public ItemOption readItemOption(Message msg)
	{
		ItemOption result = null;
		try
		{
			int num = (int)msg.reader().readShort();
			int param = msg.reader().readInt();
			if (num != -1)
			{
				result = new ItemOption(num, param);
			}
		}
		catch (Exception ex)
		{
			Res.err(">>>>read.ItemOption  errr:");
		}
		return result;
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x00041ED8 File Offset: 0x000400D8
	public void read_cmdExtraBig(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			mSystem.println(">>---read_cmdExtraBig-sub:" + b);
			if ((int)b == 0)
			{
				this.loadItemNew(msg.reader(), 1, true);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x04000A2B RID: 2603
	protected static Controller me;

	// Token: 0x04000A2C RID: 2604
	protected static Controller me2;

	// Token: 0x04000A2D RID: 2605
	public Message messWait;

	// Token: 0x04000A2E RID: 2606
	public static bool isLoadingData = false;

	// Token: 0x04000A2F RID: 2607
	public static bool isConnectOK;

	// Token: 0x04000A30 RID: 2608
	public static bool isConnectionFail;

	// Token: 0x04000A31 RID: 2609
	public static bool isDisconnected;

	// Token: 0x04000A32 RID: 2610
	public static bool isMain;

	// Token: 0x04000A33 RID: 2611
	private float demCount;

	// Token: 0x04000A34 RID: 2612
	private int move;

	// Token: 0x04000A35 RID: 2613
	private int total;

	// Token: 0x04000A36 RID: 2614
	public static bool isStopReadMessage;

	// Token: 0x04000A37 RID: 2615
	public static bool isGet_CLIENT_INFO = false;

	// Token: 0x04000A38 RID: 2616
	public static MyHashTable frameHT_NEWBOSS = new MyHashTable();

	// Token: 0x04000A39 RID: 2617
	public const sbyte PHUBAN_TYPE_CHIENTRUONGNAMEK = 0;

	// Token: 0x04000A3A RID: 2618
	public const sbyte PHUBAN_START = 0;

	// Token: 0x04000A3B RID: 2619
	public const sbyte PHUBAN_UPDATE_POINT = 1;

	// Token: 0x04000A3C RID: 2620
	public const sbyte PHUBAN_END = 2;

	// Token: 0x04000A3D RID: 2621
	public const sbyte PHUBAN_LIFE = 4;

	// Token: 0x04000A3E RID: 2622
	public const sbyte PHUBAN_INFO = 5;

	// Token: 0x04000A3F RID: 2623
	public static bool isEXTRA_LINK = false;
}
