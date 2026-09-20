using System;
using Assets.src.g;
using Mod.DungPham.KoiOctiiu957;

// Token: 0x0200009C RID: 156
public class Service
{
	// Token: 0x0600050A RID: 1290 RVA: 0x00006C9E File Offset: 0x00004E9E
	public static Service gI()
	{
		if (Service.instance == null)
		{
			Service.instance = new Service();
		}
		return Service.instance;
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00044FA8 File Offset: 0x000431A8
	public void gotoPlayer(int id)
	{
		Message message = null;
		try
		{
			message = new Message(18);
			message.writer().writeInt(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00045014 File Offset: 0x00043214
	public void androidPack()
	{
		if (mSystem.android_pack == null)
		{
			return;
		}
		Message message = null;
		try
		{
			message = new Message(126);
			message.writer().writeUTF(mSystem.android_pack);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x0004508C File Offset: 0x0004328C
	public void charInfo(string day, string month, string year, string address, string cmnd, string dayCmnd, string noiCapCmnd, string sdt, string name)
	{
		Message message = null;
		try
		{
			message = new Message(42);
			message.writer().writeUTF(day);
			message.writer().writeUTF(month);
			message.writer().writeUTF(year);
			message.writer().writeUTF(address);
			message.writer().writeUTF(cmnd);
			message.writer().writeUTF(dayCmnd);
			message.writer().writeUTF(noiCapCmnd);
			message.writer().writeUTF(sdt);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x0004515C File Offset: 0x0004335C
	public void androidPack2()
	{
		if (mSystem.android_pack == null)
		{
			return;
		}
		Message message = null;
		try
		{
			message = new Message(126);
			message.writer().writeUTF(mSystem.android_pack);
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x00045214 File Offset: 0x00043414
	public void checkAd(sbyte status)
	{
		Message message = null;
		try
		{
			message = new Message(-44);
			message.writer().writeByte(status);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x00045280 File Offset: 0x00043480
	public void combine(sbyte action, MyVector id)
	{
		Res.outz("combine");
		Message message = null;
		try
		{
			message = new Message(-81);
			message.writer().writeByte(action);
			if ((int)action == 1)
			{
				message.writer().writeByte(id.size());
				for (int i = 0; i < id.size(); i++)
				{
					message.writer().writeByte(((Item)id.elementAt(i)).indexUI);
					Res.outz("gui id " + ((Item)id.elementAt(i)).indexUI);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00045358 File Offset: 0x00043558
	public void giaodich(sbyte action, int playerID, sbyte index, int num)
	{
		Res.outz2("giao dich action = " + action);
		Message message = null;
		try
		{
			message = new Message(-86);
			message.writer().writeByte(action);
			if ((int)action == 0 || (int)action == 1)
			{
				Res.outz2(">>>> len playerID =" + playerID);
				message.writer().writeInt(playerID);
			}
			if ((int)action == 2)
			{
				Res.outz2(string.Concat(new object[]
				{
					"gui len index =",
					index,
					" num= ",
					num
				}));
				message.writer().writeByte(index);
				message.writer().writeInt(num);
			}
			if ((int)action == 4)
			{
				Res.outz2(">>>> len index =" + index);
				message.writer().writeByte(index);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00045478 File Offset: 0x00043678
	public void sendClientInput(TField[] t)
	{
		Message message = null;
		try
		{
			Res.outz(" gui input ");
			message = new Message(-125);
			Res.outz("byte lent = " + t.Length);
			message.writer().writeByte(t.Length);
			for (int i = 0; i < t.Length; i++)
			{
				message.writer().writeUTF(t[i].getText());
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00045520 File Offset: 0x00043720
	public void speacialSkill(sbyte index)
	{
		Message message = null;
		try
		{
			message = new Message(112);
			message.writer().writeByte(index);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x0004558C File Offset: 0x0004378C
	public void test(short x, short y)
	{
		Res.outz(string.Concat(new object[]
		{
			"gui x= ",
			x,
			" y= ",
			y
		}));
		Message message = null;
		try
		{
			message = new Message(0);
			message.writer().writeShort(x);
			message.writer().writeShort(y);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x00045634 File Offset: 0x00043834
	public void test2()
	{
		Res.outz("gui test1");
		Message message = null;
		try
		{
			message = new Message(1);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x000045ED File Offset: 0x000027ED
	public void testJoint()
	{
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x0004569C File Offset: 0x0004389C
	public void mobCapcha(char ch)
	{
		Res.outz("cap char c= " + ch);
		Message message = null;
		try
		{
			message = new Message(-85);
			message.writer().writeChar(ch);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00045710 File Offset: 0x00043910
	public void friend(sbyte action, int playerId)
	{
		Res.outz("add friend");
		Message message = null;
		try
		{
			message = new Message(-80);
			message.writer().writeByte(action);
			if (playerId != -1)
			{
				message.writer().writeInt(playerId);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x000457A0 File Offset: 0x000439A0
	public void getArchivemnt(int index)
	{
		Res.outz("get ngoc");
		Message message = null;
		try
		{
			message = new Message(-76);
			message.writer().writeByte(index);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x00045820 File Offset: 0x00043A20
	public void getPlayerMenu(int playerID)
	{
		Message message = null;
		try
		{
			message = new Message(-79);
			message.writer().writeInt(playerID);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x00045880 File Offset: 0x00043A80
	public void clanImage(sbyte id)
	{
		Message message = null;
		try
		{
			message = new Message(-62);
			message.writer().writeByte(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x000458F4 File Offset: 0x00043AF4
	public void skill_not_focus(sbyte status)
	{
		Message message = null;
		try
		{
			message = new Message(-45);
			message.writer().writeByte(status);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x00045968 File Offset: 0x00043B68
	public void clanDonate(int id)
	{
		Message message = null;
		try
		{
			message = new Message(-54);
			message.writer().writeInt(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x000459DC File Offset: 0x00043BDC
	public void clanMessage(int type, string text, int clanID)
	{
		Message message = null;
		try
		{
			message = new Message(-51);
			message.writer().writeByte(type);
			if (type == 0)
			{
				message.writer().writeUTF(text);
			}
			if (type == 2)
			{
				message.writer().writeInt(clanID);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x00045A74 File Offset: 0x00043C74
	public void useItem(sbyte type, sbyte where, sbyte index, short template)
	{
		Cout.println("USE ITEM! " + type);
		if (global::Char.myCharz().statusMe == 14)
		{
			return;
		}
		Message message = null;
		try
		{
			message = new Message(-43);
			message.writer().writeByte(type);
			message.writer().writeByte(where);
			message.writer().writeByte(index);
			if ((int)index == -1)
			{
				message.writer().writeShort(template);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00045B28 File Offset: 0x00043D28
	public void joinClan(int id, sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-49);
			message.writer().writeInt(id);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x00045BA8 File Offset: 0x00043DA8
	public void clanMember(int id)
	{
		Message message = null;
		try
		{
			message = new Message(-50);
			message.writer().writeInt(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x00045C1C File Offset: 0x00043E1C
	public void searchClan(string text)
	{
		Message message = null;
		try
		{
			message = new Message(-47);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x00045C90 File Offset: 0x00043E90
	public void requestClan(short id)
	{
		Message message = null;
		try
		{
			message = new Message(-53);
			message.writer().writeShort(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x00045D04 File Offset: 0x00043F04
	public void clanRemote(int id, sbyte role)
	{
		Message message = null;
		try
		{
			message = new Message(-56);
			message.writer().writeInt(id);
			message.writer().writeByte(role);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x00045D84 File Offset: 0x00043F84
	public void leaveClan()
	{
		Message message = null;
		try
		{
			message = new Message(-55);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x00045DEC File Offset: 0x00043FEC
	public void clanInvite(sbyte action, int playerID, int clanID, int code)
	{
		Message message = null;
		try
		{
			message = new Message(-57);
			message.writer().writeByte(action);
			if ((int)action == 0)
			{
				message.writer().writeInt(playerID);
			}
			if ((int)action == 1 || (int)action == 2)
			{
				message.writer().writeInt(clanID);
				message.writer().writeInt(code);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x00045E9C File Offset: 0x0004409C
	public void getClan(sbyte action, int id, string text)
	{
		Message message = null;
		try
		{
			message = new Message(-46);
			message.writer().writeByte(action);
			if ((int)action == 2 || (int)action == 4)
			{
				message.writer().writeShort((short)id);
				message.writer().writeUTF(text);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x00045F38 File Offset: 0x00044138
	public void updateCaption(sbyte gender)
	{
		Message message = null;
		try
		{
			message = new Message(-41);
			message.writer().writeByte(gender);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x00045FAC File Offset: 0x000441AC
	public void getItem(sbyte type, sbyte id)
	{
		Message message = null;
		try
		{
			message = new Message(-40);
			message.writer().writeByte(type);
			message.writer().writeByte(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x0004602C File Offset: 0x0004422C
	public void getTask(int npcTemplateId, int menuId, int optionId)
	{
		Message message = null;
		try
		{
			message = new Message(40);
			message.writer().writeByte(npcTemplateId);
			message.writer().writeByte(menuId);
			if (optionId >= 0)
			{
				message.writer().writeByte(optionId);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x000460C0 File Offset: 0x000442C0
	public Message messageNotLogin(sbyte command)
	{
		Message message = new Message(-29);
		message.writer().writeByte(command);
		return message;
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x000460E4 File Offset: 0x000442E4
	public Message messageNotMap(sbyte command)
	{
		Message message = new Message(-28);
		message.writer().writeByte(command);
		return message;
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x00046108 File Offset: 0x00044308
	public static Message messageSubCommand(sbyte command)
	{
		Message message = new Message(-30);
		message.writer().writeByte(command);
		return message;
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x0004612C File Offset: 0x0004432C
	public void setClientType()
	{
		if (Rms.loadRMSInt("clienttype") != -1)
		{
			Main.typeClient = Rms.loadRMSInt("clienttype");
		}
		try
		{
			Res.outz(">>send ClientType1");
			Message message = this.messageNotLogin(2);
			message.writer().writeByte(Main.typeClient);
			message.writer().writeByte(mGraphics.zoomLevel);
			message.writer().writeBoolean(false);
			message.writer().writeInt(GameCanvas.w);
			message.writer().writeInt(GameCanvas.h);
			message.writer().writeBoolean(TField.isQwerty);
			message.writer().writeBoolean(GameCanvas.isTouch);
			message.writer().writeUTF(GameCanvas.getPlatformName() + "|" + GameMidlet.VERSION);
			DataInputStream dataInputStream = MyStream.readFile("/info");
			if (dataInputStream != null)
			{
				sbyte[] array = new sbyte[dataInputStream.r.buffer.Length];
				dataInputStream.read(ref array);
				if (array != null)
				{
					message.writer().writeShort(array.Length);
					message.writer().write(array);
				}
			}
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x00046290 File Offset: 0x00044490
	public void setClientType2()
	{
		Res.outz("SET CLIENT TYPE");
		if (Rms.loadRMSInt("clienttype") != -1)
		{
			mSystem.clientType = Rms.loadRMSInt("clienttype");
		}
		try
		{
			Res.outz(">>send ClientType2");
			Message message = this.messageNotLogin(2);
			message.writer().writeByte(mSystem.clientType);
			message.writer().writeByte(mGraphics.zoomLevel);
			Res.outz("gui zoomlevel = " + mGraphics.zoomLevel);
			message.writer().writeBoolean(false);
			message.writer().writeInt(GameCanvas.w);
			message.writer().writeInt(GameCanvas.h);
			message.writer().writeBoolean(TField.isQwerty);
			message.writer().writeBoolean(GameCanvas.isTouch);
			message.writer().writeUTF(GameCanvas.getPlatformName() + "|" + GameMidlet.VERSION);
			DataInputStream dataInputStream = MyStream.readFile("/info");
			if (dataInputStream != null)
			{
				sbyte[] array = new sbyte[dataInputStream.r.buffer.Length];
				dataInputStream.read(ref array);
				if (array != null)
				{
					message.writer().writeShort(array.Length);
					message.writer().write(array);
				}
			}
			this.session = Session_ME2.gI();
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
			message.cleanup();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x00046424 File Offset: 0x00044624
	public void sendCheckController()
	{
		this.sendKeepAlive((sbyte)(-120), Session_ME.gI());
		Service.curCheckController = mSystem.currentTimeMillis();
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x00046480 File Offset: 0x00044680
	public void sendCheckMap()
	{
		ISession isession = (Session_ME2.gI().isConnected() && !Session_ME2.connecting) ? ((ISession)Session_ME2.gI()) : ((ISession)Session_ME.gI());
		this.sendKeepAlive((sbyte)(-121), isession);
		Service.curCheckMap = mSystem.currentTimeMillis();
	}

	private void sendKeepAlive(sbyte command, ISession targetSession)
	{
		Message message = null;
		try
		{
			if (targetSession == null)
			{
				return;
			}
			message = new Message(command);
			targetSession.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			if (message != null)
			{
				message.cleanup();
			}
		}
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x000464DC File Offset: 0x000446DC
	public void login(string username, string pass, string version, sbyte type)
	{
		Res.outz(string.Concat(new string[]
		{
			"Login ",
			username,
			" *** ",
			version
		}));
		try
		{
			Message message = this.messageNotLogin(0);
			message.writer().writeUTF(username);
			message.writer().writeUTF(pass);
			message.writer().writeUTF(version);
			message.writer().writeByte(type);
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x0004659C File Offset: 0x0004479C
	public void requestRegister(string username, string pass, string usernameAo, string passAo, string version)
	{
		try
		{
			Message message = this.messageNotLogin(1);
			message.writer().writeUTF(username);
			message.writer().writeUTF(pass);
			if (usernameAo != null && !usernameAo.Equals(string.Empty))
			{
				message.writer().writeUTF(usernameAo);
				message.writer().writeUTF("a");
			}
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x00046640 File Offset: 0x00044840
	public void requestChangeMap()
	{
		if (MainMod.isLockMap && !AutoMap.isAutoChangeMap)
		{
			return;
		}
		global::Char.isLoadingMap = true;
		GameCanvas.timeLoading = 0;
		GameCanvas.TIMEOUT = mSystem.currentTimeMillis();
		Message message = new Message(-23);
		this.session.sendMessage(message);
		message.cleanup();
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x00046678 File Offset: 0x00044878
	public void magicTree(sbyte type)
	{
		Message message = new Message(-34);
		try
		{
			message.writer().writeByte(type);
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x000466C8 File Offset: 0x000448C8
	public void requestChangeZone(int zoneId, int indexUI)
	{
		Message message = new Message(21);
		try
		{
			global::Char.isLoadingMap = true;
			GameCanvas.timeLoading = 0;
			GameCanvas.TIMEOUT = mSystem.currentTimeMillis();
			message.writer().writeByte(zoneId);
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x00046718 File Offset: 0x00044918
	public void checkMMove(int second)
	{
		Message message = new Message(-78);
		try
		{
			message.writer().writeInt(second);
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x00046768 File Offset: 0x00044968
	public void charMove()
	{
		int num = global::Char.myCharz().cx - global::Char.myCharz().cxSend;
		int num2 = global::Char.myCharz().cy - global::Char.myCharz().cySend;
		if (global::Char.ischangingMap || (num == 0 && num2 == 0) || Controller.isStopReadMessage || global::Char.myCharz().isTeleport || global::Char.myCharz().cy <= 0 || global::Char.myCharz().telePortSkill)
		{
			return;
		}
		try
		{
			Message message = new Message(-7);
			global::Char.myCharz().cxSend = global::Char.myCharz().cx;
			global::Char.myCharz().cySend = global::Char.myCharz().cy;
			global::Char.myCharz().cdirSend = global::Char.myCharz().cdir;
			global::Char.myCharz().cactFirst = global::Char.myCharz().statusMe;
			if (TileMap.tileTypeAt(global::Char.myCharz().cx / (int)TileMap.size, global::Char.myCharz().cy / (int)TileMap.size) == 0)
			{
				message.writer().writeByte(1);
			}
			else
			{
				message.writer().writeByte(0);
			}
			message.writer().writeShort(global::Char.myCharz().cx);
			if (num2 != 0)
			{
				message.writer().writeShort(global::Char.myCharz().cy);
			}
			this.session.sendMessage(message);
			GameScr.tickMove++;
			message.cleanup();
		}
		catch (Exception ex)
		{
			Cout.LogError("LOI CHAR MOVE " + ex.ToString());
		}
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x00046910 File Offset: 0x00044B10
	public void selectCharToPlay(string charname)
	{
		Message message = new Message(-28);
		try
		{
			message.writer().writeByte(1);
			message.writer().writeUTF(charname);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		this.session.sendMessage(message);
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x000045ED File Offset: 0x000027ED
	public void selectZone(sbyte sub, int value)
	{
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x0004697C File Offset: 0x00044B7C
	public void createChar(string name, int gender, int hair)
	{
		Message message = new Message(-28);
		try
		{
			message.writer().writeByte(2);
			message.writer().writeUTF(name);
			message.writer().writeByte(gender);
			message.writer().writeByte(hair);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		this.session.sendMessage(message);
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x00046A00 File Offset: 0x00044C00
	public void requestModTemplate(int modTemplateId)
	{
		Message message = null;
		try
		{
			message = new Message(11);
			message.writer().writeShort(modTemplateId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x00046A74 File Offset: 0x00044C74
	public void requestSkill(int skillId)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(9);
			message.writer().writeShort(skillId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x00046AE8 File Offset: 0x00044CE8
	public void requestItemInfo(int typeUI, int indexUI)
	{
		Message message = null;
		try
		{
			message = new Message(35);
			message.writer().writeByte(typeUI);
			message.writer().writeByte(indexUI);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x00046B68 File Offset: 0x00044D68
	public void requestItemPlayer(int charId, int indexUI)
	{
		Message message = null;
		try
		{
			message = new Message(90);
			message.writer().writeInt(charId);
			message.writer().writeByte(indexUI);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x00046BE8 File Offset: 0x00044DE8
	public void upSkill(int skillTemplateId, int point)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(17);
			message.writer().writeShort(skillTemplateId);
			message.writer().writeByte(point);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00046C68 File Offset: 0x00044E68
	public void saleItem(sbyte action, sbyte type, short id)
	{
		Message message = null;
		try
		{
			message = new Message(7);
			message.writer().writeByte(action);
			message.writer().writeByte(type);
			message.writer().writeShort(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x00046CF4 File Offset: 0x00044EF4
	public void buyItem(sbyte type, int id, int quantity)
	{
		Message message = null;
		try
		{
			message = new Message(6);
			message.writer().writeByte(type);
			message.writer().writeShort(id);
			if (quantity > 1)
			{
				message.writer().writeShort(quantity);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x00046D88 File Offset: 0x00044F88
	public void selectSkill(int skillTemplateId)
	{
		Cout.println(global::Char.myCharz().cName + " SELECT SKILL " + skillTemplateId);
		Message message = null;
		try
		{
			message = new Message(34);
			message.writer().writeShort(skillTemplateId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00046E1C File Offset: 0x0004501C
	public void getEffData(short id)
	{
		Message message = null;
		try
		{
			message = new Message(-66);
			message.writer().writeShort(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00046E90 File Offset: 0x00045090
	public void openUIZone()
	{
		Message message = null;
		try
		{
			message = new Message(29);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00046EF8 File Offset: 0x000450F8
	public void confirmMenu(short npcID, sbyte select)
	{
		Res.outz("confirme menu" + select);
		Message message = null;
		try
		{
			message = new Message(32);
			message.writer().writeShort(npcID);
			message.writer().writeByte(select);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x00046F8C File Offset: 0x0004518C
	public void openMenu(int npcId)
	{
		Message message = null;
		try
		{
			message = new Message(33);
			message.writer().writeShort(npcId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00047000 File Offset: 0x00045200
	public void menu(int npcId, int menuId, int optionId)
	{
		Cout.println("menuid: " + menuId);
		Message message = null;
		try
		{
			message = new Message(22);
			message.writer().writeByte(npcId);
			message.writer().writeByte(menuId);
			message.writer().writeByte(optionId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x000470A0 File Offset: 0x000452A0
	public void menuId(short menuId)
	{
		Message message = null;
		try
		{
			message = new Message(27);
			message.writer().writeShort(menuId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00047114 File Offset: 0x00045314
	public void textBoxId(short menuId, string str)
	{
		Message message = null;
		try
		{
			message = new Message(88);
			message.writer().writeShort(menuId);
			message.writer().writeUTF(str);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00047194 File Offset: 0x00045394
	public void requestItem(int typeUI)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(22);
			message.writer().writeByte(typeUI);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x00047208 File Offset: 0x00045408
	public void boxSort()
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(19);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x00047270 File Offset: 0x00045470
	public void boxCoinOut(int coinOut)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(21);
			message.writer().writeInt(coinOut);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x000472E4 File Offset: 0x000454E4
	public void upgradeItem(Item item, Item[] items, bool isGold)
	{
		GameCanvas.msgdlg.pleasewait();
		Message message = null;
		try
		{
			message = new Message(14);
			message.writer().writeBoolean(isGold);
			message.writer().writeByte(item.indexUI);
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] != null)
				{
					message.writer().writeByte(items[i].indexUI);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x000473A4 File Offset: 0x000455A4
	public void crystalCollectLock(Item[] items)
	{
		GameCanvas.msgdlg.pleasewait();
		Message message = null;
		try
		{
			message = new Message(13);
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] != null)
				{
					message.writer().writeByte(items[i].indexUI);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x00047444 File Offset: 0x00045644
	public void acceptInviteTrade(int playerMapId)
	{
		Message message = null;
		try
		{
			message = new Message(37);
			message.writer().writeInt(playerMapId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x000474B8 File Offset: 0x000456B8
	public void cancelInviteTrade()
	{
		Message message = null;
		try
		{
			message = new Message(50);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x00047520 File Offset: 0x00045720
	public void tradeAccept()
	{
		Message message = null;
		try
		{
			message = new Message(39);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x00047588 File Offset: 0x00045788
	public void tradeItemLock(int coin, Item[] items)
	{
		Message message = null;
		try
		{
			message = new Message(38);
			message.writer().writeInt(coin);
			int num = 0;
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] != null)
				{
					num++;
				}
			}
			message.writer().writeByte(num);
			for (int j = 0; j < items.Length; j++)
			{
				if (items[j] != null)
				{
					message.writer().writeByte(items[j].indexUI);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x0004765C File Offset: 0x0004585C
	public void sendPlayerAttack(MyVector vMob, MyVector vChar, int type)
	{
		try
		{
			Res.outz(string.Concat(new object[]
			{
				">>SEND ATTACT  vMob=",
				vMob.size(),
				"  vChar=",
				vChar.size()
			}));
			Message message = null;
			if (type != 0)
			{
				if (vMob.size() > 0 && vChar.size() > 0)
				{
					if (type == 1)
					{
						message = new Message(-4);
					}
					else if (type == 2)
					{
						message = new Message(67);
					}
					message.writer().writeByte(vMob.size());
					for (int i = 0; i < vMob.size(); i++)
					{
						Mob mob = (Mob)vMob.elementAt(i);
						message.writer().writeByte(mob.mobId);
					}
					for (int j = 0; j < vChar.size(); j++)
					{
						global::Char @char = (global::Char)vChar.elementAt(j);
						if (@char != null)
						{
							message.writer().writeInt(@char.charID);
						}
						else
						{
							message.writer().writeInt(-1);
						}
					}
				}
				else if (vMob.size() > 0)
				{
					message = new Message(54);
					for (int k = 0; k < vMob.size(); k++)
					{
						Mob mob2 = (Mob)vMob.elementAt(k);
						if (!mob2.isMobMe)
						{
							message.writer().writeByte(mob2.mobId);
						}
						else
						{
							message.writer().writeByte(-1);
							message.writer().writeInt(mob2.mobId);
						}
					}
				}
				else if (vChar.size() > 0)
				{
					message = new Message(-60);
					for (int l = 0; l < vChar.size(); l++)
					{
						global::Char char2 = (global::Char)vChar.elementAt(l);
						message.writer().writeInt(char2.charID);
					}
				}
				message.writer().writeSByte((sbyte)global::Char.myCharz().cdir);
				if (message != null)
				{
					this.session.sendMessage(message);
				}
			}
		}
		catch (Exception ex)
		{
			Res.err(string.Concat(new object[]
			{
				">>err ATTACT  vMob=",
				vMob.size(),
				"  vChar=",
				vChar.size()
			}));
		}
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x000478EC File Offset: 0x00045AEC
	public void pickItem(int itemMapId)
	{
		Message message = null;
		try
		{
			message = new Message(-20);
			message.writer().writeShort(itemMapId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x00047960 File Offset: 0x00045B60
	public void throwItem(int index)
	{
		Message message = null;
		try
		{
			message = new Message(-18);
			message.writer().writeByte(index);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x000479D4 File Offset: 0x00045BD4
	public void returnTownFromDead()
	{
		Message message = null;
		try
		{
			message = new Message(-15);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x00047A3C File Offset: 0x00045C3C
	public void wakeUpFromDead()
	{
		Message message = null;
		try
		{
			message = new Message(-16);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x00047AA4 File Offset: 0x00045CA4
	public void chat(string text)
	{
		Message message = null;
		try
		{
			message = new Message(44);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x00047B18 File Offset: 0x00045D18
	public void updateData()
	{
		Message message = null;
		try
		{
			message = new Message(-87);
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x00047BC0 File Offset: 0x00045DC0
	public void updateMap()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(6);
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x00047C68 File Offset: 0x00045E68
	public void updateSkill()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(7);
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x00047D04 File Offset: 0x00045F04
	public void updateItem()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(8);
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x00047DA0 File Offset: 0x00045FA0
	public void clientOk()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(13);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x00047E08 File Offset: 0x00046008
	public void tradeInvite(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(36);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x00047E7C File Offset: 0x0004607C
	public void addFriend(string name)
	{
		Message message = null;
		try
		{
			message = new Message(53);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x00047EF0 File Offset: 0x000460F0
	public void addPartyAccept(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(76);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x00047F64 File Offset: 0x00046164
	public void addPartyCancel(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(77);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x00047FD8 File Offset: 0x000461D8
	public void testInvite(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(59);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x0004804C File Offset: 0x0004624C
	public void addCuuSat(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(62);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x000480C0 File Offset: 0x000462C0
	public void addParty(string name)
	{
		Message message = null;
		try
		{
			message = new Message(75);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x00048134 File Offset: 0x00046334
	public void player_vs_player(sbyte action, sbyte type, int playerId)
	{
		Message message = null;
		try
		{
			message = new Message(-59);
			message.writer().writeByte(action);
			message.writer().writeByte(type);
			message.writer().writeInt(playerId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x000481C0 File Offset: 0x000463C0
	public void requestMaptemplate(int maptemplateId)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(10);
			message.writer().writeByte(maptemplateId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x00048234 File Offset: 0x00046434
	public void outParty()
	{
		Message message = null;
		try
		{
			message = new Message(79);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0004829C File Offset: 0x0004649C
	public void requestPlayerInfo(MyVector chars)
	{
		Message message = null;
		try
		{
			message = new Message(18);
			message.writer().writeByte(chars.size());
			for (int i = 0; i < chars.size(); i++)
			{
				global::Char @char = (global::Char)chars.elementAt(i);
				message.writer().writeInt(@char.charID);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x0004834C File Offset: 0x0004654C
	public void pleaseInputParty(string str)
	{
		Message message = null;
		try
		{
			message = new Message(16);
			message.writer().writeUTF(str);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x000483C0 File Offset: 0x000465C0
	public void acceptPleaseParty(string str)
	{
		Message message = null;
		try
		{
			message = new Message(17);
			message.writer().writeUTF(str);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x00048434 File Offset: 0x00046634
	public void chatPlayer(string text, int id)
	{
		Res.outz("chat player text = " + text);
		Message message = null;
		try
		{
			message = new Message(-72);
			message.writer().writeInt(id);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x000484C4 File Offset: 0x000466C4
	public void chatGlobal(string text)
	{
		Message message = null;
		try
		{
			message = new Message(-71);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x00048538 File Offset: 0x00046738
	public void chatPrivate(string to, string text)
	{
		Message message = null;
		try
		{
			message = new Message(91);
			message.writer().writeUTF(to);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x000485B8 File Offset: 0x000467B8
	public void sendCardInfo(string NAP, string PIN)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(16);
			message.writer().writeUTF(NAP);
			message.writer().writeUTF(PIN);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x00048638 File Offset: 0x00046838
	public void saveRms(string key, sbyte[] data)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(60);
			message.writer().writeUTF(key);
			message.writer().writeInt(data.Length);
			message.writer().write(data);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x000486C8 File Offset: 0x000468C8
	public void loadRMS(string key)
	{
		Cout.println("REQUEST RMS");
		Message message = null;
		try
		{
			message = Service.messageSubCommand(61);
			message.writer().writeUTF(key);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x00048748 File Offset: 0x00046948
	public void clearTask()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(17);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x000487B0 File Offset: 0x000469B0
	public void changeName(string name, int id)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(18);
			message.writer().writeInt(id);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x00048830 File Offset: 0x00046A30
	public void requestIcon(int id)
	{
		GameCanvas.connect();
		Message message = null;
		try
		{
			message = new Message(-67);
			message.writer().writeInt(id);
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			Res.outz(string.Concat(new object[]
			{
				">>>>>>>>>>>>>REQUEST ICON ",
				id,
				"  isConnected:",
				Controller.isGet_CLIENT_INFO
			}));
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x00048920 File Offset: 0x00046B20
	public void doConvertUpgrade(int index1, int index2, int index3)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(33);
			message.writer().writeByte(index1);
			message.writer().writeByte(index2);
			message.writer().writeByte(index3);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x000489AC File Offset: 0x00046BAC
	public void inviteClanDun(string name)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(34);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x00048A20 File Offset: 0x00046C20
	public void inputNumSplit(int indexItem, int numSplit)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(40);
			message.writer().writeByte(indexItem);
			message.writer().writeInt(numSplit);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00048AA0 File Offset: 0x00046CA0
	public void activeAccProtect(int pass)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(37);
			message.writer().writeInt(pass);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x00048B14 File Offset: 0x00046D14
	public void clearAccProtect(int pass)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(41);
			message.writer().writeInt(pass);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x00048B88 File Offset: 0x00046D88
	public void updateActive(int passOld, int passNew)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(38);
			message.writer().writeInt(passOld);
			message.writer().writeInt(passNew);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x00048C08 File Offset: 0x00046E08
	public void openLockAccProtect(int pass2)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(39);
			message.writer().writeInt(pass2);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x00048C7C File Offset: 0x00046E7C
	public void getBgTemplate(short id)
	{
		Message message = null;
		try
		{
			message = new Message(-32);
			message.writer().writeShort(id);
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x00048D30 File Offset: 0x00046F30
	public void getMapOffline()
	{
		Message message = null;
		try
		{
			global::Char.isLoadingMap = true;
			GameCanvas.timeLoading = 0;
			GameCanvas.TIMEOUT = mSystem.currentTimeMillis();
			message = new Message(-33);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x00048D98 File Offset: 0x00046F98
	public void finishUpdate()
	{
		Message message = null;
		try
		{
			message = new Message(-38);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x00048E00 File Offset: 0x00047000
	public void finishUpdate(int playerID)
	{
		Message message = null;
		try
		{
			message = new Message(-38);
			message.writer().writeInt(playerID);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x00048E60 File Offset: 0x00047060
	public void finishLoadMap()
	{
		Message message = null;
		try
		{
			message = new Message(-39);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x00048EC8 File Offset: 0x000470C8
	public void getChest(sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-35);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x00048F3C File Offset: 0x0004713C
	public void requestBagImage(int ID)
	{
		Message message = null;
		try
		{
			message = new Message(-63);
			message.writer().writeShort(ID);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x00048FB0 File Offset: 0x000471B0
	public void getBag(sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-36);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x00049024 File Offset: 0x00047224
	public void getBody(sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-37);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x00049098 File Offset: 0x00047298
	public void login2(string user)
	{
		Res.outz("Login 2:  " + user);
		Message message = null;
		try
		{
			message = new Message(-101);
			message.writer().writeUTF(user);
			message.writer().writeByte(1);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x00049114 File Offset: 0x00047314
	public void getMagicTree(sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-34);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00049188 File Offset: 0x00047388
	public void upPotential(int typePotential, int num)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(16);
			message.writer().writeByte(typePotential);
			message.writer().writeShort(num);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x00049208 File Offset: 0x00047408
	public void getResource(sbyte action, MyVector vResourceIndex)
	{
		Res.outz("request resource action= " + action);
		Message message = null;
		try
		{
			message = new Message(-74);
			message.writer().writeByte(action);
			if ((int)action == 2 && vResourceIndex != null)
			{
				message.writer().writeShort(vResourceIndex.size());
				for (int i = 0; i < vResourceIndex.size(); i++)
				{
					message.writer().writeShort(short.Parse((string)vResourceIndex.elementAt(i)));
				}
			}
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				Service.reciveFromMainSession = true;
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x00049328 File Offset: 0x00047528
	public void requestMapSelect(int selected)
	{
		Res.outz("request magic tree");
		Message message = null;
		try
		{
			message = new Message(-91);
			message.writer().writeByte(selected);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x00049390 File Offset: 0x00047590
	public void petInfo()
	{
		Message message = null;
		try
		{
			message = new Message(-107);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x000493E4 File Offset: 0x000475E4
	public void sendTop(string topName, sbyte selected)
	{
		Message message = null;
		try
		{
			message = new Message(-96);
			message.writer().writeUTF(topName);
			message.writer().writeByte(selected);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00049450 File Offset: 0x00047650
	public void enemy(sbyte b, int charID)
	{
		Message message = null;
		Res.outz("add enemy");
		try
		{
			message = new Message(-99);
			message.writer().writeByte(b);
			if ((int)b == 1 || (int)b == 2)
			{
				message.writer().writeInt(charID);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x000494D4 File Offset: 0x000476D4
	public void kigui(sbyte action, int itemId, sbyte moneyType, int money, int quaintly)
	{
		Message message = null;
		try
		{
			Res.outz("ki gui action= " + action);
			message = new Message(-100);
			message.writer().writeByte(action);
			if ((int)action == 0)
			{
				message.writer().writeShort(itemId);
				message.writer().writeByte(moneyType);
				message.writer().writeInt(money);
				message.writer().writeInt(quaintly);
			}
			if ((int)action == 1 || (int)action == 2)
			{
				message.writer().writeShort(itemId);
			}
			if ((int)action == 3)
			{
				message.writer().writeShort(itemId);
				message.writer().writeByte(moneyType);
				message.writer().writeInt(money);
			}
			if ((int)action == 4)
			{
				message.writer().writeByte(moneyType);
				message.writer().writeByte(money);
				Res.outz(string.Concat(new object[]
				{
					"currTab= ",
					moneyType,
					" page= ",
					money
				}));
			}
			if ((int)action == 5)
			{
				message.writer().writeShort(itemId);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x00049648 File Offset: 0x00047848
	public void getFlag(sbyte action, sbyte flagType)
	{
		Message message = null;
		try
		{
			message = new Message(-103);
			message.writer().writeByte(action);
			Res.outz(string.Concat(new object[]
			{
				"------------service--  ",
				action,
				"   ",
				flagType
			}));
			if ((int)action != 0)
			{
				message.writer().writeByte(flagType);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x000496EC File Offset: 0x000478EC
	public void setLockInventory(int pass)
	{
		Message message = null;
		try
		{
			Res.outz("------------setLockInventory:     ***");
			message = new Message(-104);
			message.writer().writeInt(pass);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00049760 File Offset: 0x00047960
	public void petStatus(sbyte status)
	{
		Message message = null;
		try
		{
			message = new Message(-108);
			message.writer().writeByte(status);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x000497C0 File Offset: 0x000479C0
	public void transportNow()
	{
		Message message = null;
		try
		{
			Res.outz("------------transportNow  ");
			message = new Message(-105);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x0004981C File Offset: 0x00047A1C
	public void funsion(sbyte type)
	{
		Message message = null;
		try
		{
			Res.outz("FUNSION");
			message = new Message(125);
			message.writer().writeByte(type);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00049890 File Offset: 0x00047A90
	public void imageSource(MyVector vID)
	{
		Message message = null;
		try
		{
			Res.outz("IMAGE SOURCE size= " + vID.size());
			message = new Message(-111);
			message.writer().writeShort(vID.size());
			if (vID.size() > 0)
			{
				for (int i = 0; i < vID.size(); i++)
				{
					Res.outz("gui len str " + ((ImageSource)vID.elementAt(i)).id);
					message.writer().writeUTF(((ImageSource)vID.elementAt(i)).id);
				}
			}
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
				Service.reciveFromMainSession = true;
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x000499BC File Offset: 0x00047BBC
	public void getQuayso()
	{
		Message message = null;
		try
		{
			message = new Message(-126);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x00049A1C File Offset: 0x00047C1C
	public void sendServerData(sbyte action, int id, sbyte[] data)
	{
		Message message = null;
		try
		{
			Res.outz("SERVER DATA");
			message = new Message(-110);
			message.writer().writeByte(action);
			if ((int)action == 1)
			{
				message.writer().writeInt(id);
				if (data != null)
				{
					int num = data.Length;
					message.writer().writeShort(num);
					message.writer().write(ref data, 0, num);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x00049ABC File Offset: 0x00047CBC
	public void changeOnKeyScr(sbyte[] skill)
	{
		Message message = null;
		try
		{
			message = new Message(-113);
			for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
			{
				message.writer().writeByte(skill[i]);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x00049B40 File Offset: 0x00047D40
	public void requestPean()
	{
		Message message = null;
		try
		{
			message = new Message(-114);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x00049BA0 File Offset: 0x00047DA0
	public void sendThachDau(int id)
	{
		Res.outz("GUI THACH DAU");
		Message message = null;
		try
		{
			message = new Message(-118);
			message.writer().writeInt(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x00049C14 File Offset: 0x00047E14
	public void messagePlayerMenu(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(-30);
			message.writer().writeByte(63);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x00049C8C File Offset: 0x00047E8C
	public void playerMenuAction(int charId, short select)
	{
		Message message = null;
		try
		{
			message = new Message(-30);
			message.writer().writeByte(64);
			message.writer().writeInt(charId);
			message.writer().writeShort(select);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x00049D10 File Offset: 0x00047F10
	public void getImgByName(string nameImg)
	{
		Message message = null;
		try
		{
			message = new Message(66);
			message.writer().writeUTF(nameImg);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x00049D70 File Offset: 0x00047F70
	public void SendCrackBall(byte type, byte soluong)
	{
		Message message = new Message(-127);
		try
		{
			message.writer().writeByte((int)type);
			if (soluong > 0)
			{
				message.writer().writeByte((int)soluong);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x00049DE0 File Offset: 0x00047FE0
	public void SendRada(int i, int id)
	{
		Message message = new Message(sbyte.MaxValue);
		try
		{
			message.writer().writeByte(i);
			if (id != -1)
			{
				message.writer().writeShort(id);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x00049E50 File Offset: 0x00048050
	public void sendOptHat(sbyte action)
	{
		Message message = new Message(24);
		try
		{
			if ((int)action == 1)
			{
				sbyte[] array = Res.TakeSnapShot();
				message.writer().writeByte(1);
				message.writer().writeShort(array.Length);
				message.writer().write(array);
			}
			else
			{
				message.writer().writeByte((global::Char.myCharz().idHat != -1) ? -1 : 0);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x00049EFC File Offset: 0x000480FC
	public void sendDelAcc()
	{
		Message message = new Message(69);
		try
		{
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x00049F4C File Offset: 0x0004814C
	public void new_skill_not_focus(sbyte idTemplateSkill, sbyte dir, short x, short y)
	{
		Message message = null;
		try
		{
			message = new Message(-45);
			message.writer().writeSByte(20);
			message.writer().writeSByte(idTemplateSkill);
			message.writer().writeShort(global::Char.myCharz().cx);
			message.writer().writeShort(global::Char.myCharz().cy);
			message.writer().writeSByte(dir);
			message.writer().writeShort(x);
			message.writer().writeShort(y);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x0004A01C File Offset: 0x0004821C
	public void sendCT_ready(sbyte sub, sbyte sub_sub)
	{
		Message message = null;
		try
		{
			message = new Message(24);
			message.writer().writeByte(sub);
			message.writer().writeByte(sub_sub);
			Res.err(string.Concat(new object[]
			{
				" =====> SEND OPTION_HAT ",
				sub,
				"_",
				sub_sub
			}));
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x0004A0B8 File Offset: 0x000482B8
	public void sendCmdExtra(sbyte sub, string user, string pass)
	{
		Message message = new Message(24);
		try
		{
			message.writer().writeByte(sub);
			if ((int)sub == 127)
			{
				message.writer().writeUTF(user);
				message.writer().writeUTF(pass);
				Controller.isEXTRA_LINK = false;
				Res.err(string.Concat(new object[]
				{
					" =====> SEND EXTRA_LINK ",
					sub,
					" user:",
					user,
					" pass:",
					pass
				}));
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x04000A40 RID: 2624
	private ISession session = Session_ME.gI();

	// Token: 0x04000A41 RID: 2625
	protected static Service instance;

	// Token: 0x04000A42 RID: 2626
	public static long curCheckController;

	// Token: 0x04000A43 RID: 2627
	public static long curCheckMap;

	// Token: 0x04000A44 RID: 2628
	public static long logController;

	// Token: 0x04000A45 RID: 2629
	public static long logMap;

	// Token: 0x04000A46 RID: 2630
	public int demGui;

	// Token: 0x04000A47 RID: 2631
	public static bool reciveFromMainSession;
}
