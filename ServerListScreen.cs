using System;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class ServerListScreen : mScreen, IActionListener
{
	// Token: 0x060009FF RID: 2559 RVA: 0x0009673C File Offset: 0x0009493C
	public ServerListScreen()
	{
		int num = 4;
		int num2 = num * 32 + 23 + 33;
		if (num2 >= GameCanvas.w)
		{
			num--;
			num2 = num * 32 + 23 + 33;
		}
		this.initCommand();
		if (!GameCanvas.isTouch)
		{
			ServerListScreen.selected = 0;
			this.processInput();
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
		if (this.cmdCallHotline == null)
		{
			this.cmdCallHotline = new Command("Gọi hotline", this, 13, null);
			this.cmdCallHotline.x = GameCanvas.w - 75;
			if (mSystem.clientType == 1 && !GameCanvas.isTouch)
			{
				this.cmdCallHotline.y = GameCanvas.h - 20;
			}
			else
			{
				int num3 = 2;
				this.cmdCallHotline.y = num3 + 6;
			}
		}
		ServerListScreen.cmdUpdateServer = new Command();
		ServerListScreen.cmdUpdateServer.actionChat = delegate(string str)
		{
			string text = str;
			string text2 = str;
			if (text == null)
			{
				text = ServerListScreen.linkDefault;
				return;
			}
			if (text == null && text2 != null)
			{
				if (text2.Equals(string.Empty) || text2.Length < 20)
				{
					text2 = ServerListScreen.linkDefault;
				}
				ServerListScreen.getServerList(text2);
			}
			if (text != null && text2 == null)
			{
				if (text.Equals(string.Empty) || text.Length < 20)
				{
					text = ServerListScreen.linkDefault;
				}
				ServerListScreen.getServerList(text);
			}
			if (text != null && text2 != null)
			{
				if (text.Length > text2.Length)
				{
					ServerListScreen.getServerList(text);
				}
				else
				{
					ServerListScreen.getServerList(text2);
				}
			}
		};
		this.setLinkDefault(mSystem.LANGUAGE);
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x00096860 File Offset: 0x00094A60
	public static void createDeleteRMS()
	{
		if (ServerListScreen.cmdDeleteRMS == null)
		{
			if (GameCanvas.serverScreen == null)
			{
				GameCanvas.serverScreen = new ServerListScreen();
			}
			ServerListScreen.cmdDeleteRMS = new Command(string.Empty, GameCanvas.serverScreen, 14, null);
			ServerListScreen.cmdDeleteRMS.x = GameCanvas.w - 78;
			ServerListScreen.cmdDeleteRMS.y = GameCanvas.h - 26;
		}
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x000968C8 File Offset: 0x00094AC8
	private void initCommand()
	{
		this.nCmdPlay = 0;
		string text = Rms.loadRMSString("acc");
		if (text == null)
		{
			if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect) != null)
			{
				this.nCmdPlay = 1;
			}
		}
		else if (text.Equals(string.Empty))
		{
			if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect) != null)
			{
				this.nCmdPlay = 1;
			}
		}
		else
		{
			this.nCmdPlay = 1;
		}
		this.cmd = new Command[(mGraphics.zoomLevel <= 1) ? (4 + this.nCmdPlay) : (3 + this.nCmdPlay)];
		int num = GameCanvas.hh - 15 * this.cmd.Length + 28;
		for (int i = 0; i < this.cmd.Length; i++)
		{
			switch (i)
			{
			case 0:
				this.cmd[0] = new Command(string.Empty, this, 3, null);
				if (text == null)
				{
					this.cmd[0].caption = mResources.playNew;
					if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect) != null)
					{
						this.cmd[0].caption = mResources.choitiep;
					}
				}
				else if (text.Equals(string.Empty))
				{
					this.cmd[0].caption = mResources.playNew;
					if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect) != null)
					{
						this.cmd[0].caption = mResources.choitiep;
					}
				}
				else
				{
					this.cmd[0].caption = mResources.playAcc + ": " + text;
					if (this.cmd[0].caption.Length > 23)
					{
						this.cmd[0].caption = this.cmd[0].caption.Substring(0, 23);
						Command command = this.cmd[0];
						command.caption += "...";
					}
				}
				break;
			case 1:
				if (this.nCmdPlay == 1)
				{
					this.cmd[1] = new Command(string.Empty, this, 10100, null);
					this.cmd[1].caption = mResources.playNew;
				}
				else
				{
					this.cmd[1] = new Command(mResources.change_account, this, 7, null);
				}
				break;
			case 2:
				if (this.nCmdPlay == 1)
				{
					this.cmd[2] = new Command(mResources.change_account, this, 7, null);
				}
				else
				{
					this.cmd[2] = new Command(string.Empty, this, 17, null);
				}
				break;
			case 3:
				if (this.nCmdPlay == 1)
				{
					this.cmd[3] = new Command(string.Empty, this, 17, null);
				}
				else
				{
					this.cmd[3] = new Command(mResources.option, this, 8, null);
				}
				break;
			case 4:
				this.cmd[4] = new Command(mResources.option, this, 8, null);
				break;
			}
			this.cmd[i].y = num;
			this.cmd[i].setType();
			this.cmd[i].x = (GameCanvas.w - this.cmd[i].w) / 2;
			num += 30;
		}
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x00008885 File Offset: 0x00006A85
	public static void doUpdateServer()
	{
		if (ServerListScreen.cmdUpdateServer == null && GameCanvas.serverScreen == null)
		{
			GameCanvas.serverScreen = new ServerListScreen();
		}
		Net.connectHTTP2(ServerListScreen.linkDefault, ServerListScreen.cmdUpdateServer);
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x00096C38 File Offset: 0x00094E38
	public static void getServerList(string str)
	{
		string[] array = Res.split(str.Trim(), ",", 0);
		Res.outz(">>> getServerList= " + str);
		mResources.loadLanguague(sbyte.Parse(array[array.Length - 2]));
		ServerListScreen.nameServer = new string[array.Length - 2];
		ServerListScreen.address = new string[array.Length - 2];
		ServerListScreen.port = new short[array.Length - 2];
		ServerListScreen.language = new sbyte[array.Length - 2];
		ServerListScreen.typeSv = new sbyte[array.Length - 2];
		ServerListScreen.isNew = new sbyte[array.Length - 2];
		ServerListScreen.hasConnected = new bool[2];
		for (int i = 0; i < array.Length - 2; i++)
		{
			string[] array2 = Res.split(array[i].Trim(), ":", 0);
			ServerListScreen.nameServer[i] = array2[0];
			ServerListScreen.address[i] = array2[1];
			ServerListScreen.port[i] = short.Parse(array2[2]);
			ServerListScreen.language[i] = sbyte.Parse(array2[3].Trim());
			try
			{
				ServerListScreen.typeSv[i] = sbyte.Parse(array2[4].Trim());
			}
			catch (Exception ex)
			{
				ServerListScreen.typeSv[i] = 0;
			}
			try
			{
				ServerListScreen.isNew[i] = sbyte.Parse(array2[5].Trim());
			}
			catch (Exception ex2)
			{
				ServerListScreen.isNew[i] = 0;
			}
		}
		ServerListScreen.serverPriority = sbyte.Parse(array[array.Length - 1]);
		Res.outz(">>> getServerList= serverPriority: " + ServerListScreen.serverPriority);
		ServerListScreen.saveIP();
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x00096DD8 File Offset: 0x00094FD8
	public override void paint(mGraphics g)
	{
		if (!ServerListScreen.loadScreen && !ServerListScreen.bigOk)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		}
		else
		{
			GameCanvas.paintBGGameScr(g);
		}
		int num = 2;
		mFont.tahoma_7_white.drawString(g, string.Concat(new object[]
		{
			"v",
			GameMidlet.VERSION,
			"(",
			mGraphics.zoomLevel,
			")"
		}), GameCanvas.w - 2, num + 15, 1, mFont.tahoma_7_grey);
		try
		{
			bool isConnected = ServerListScreen.isServerOnline || Session_ME.gI().isConnected();
			string text = ServerListScreen.nameServer[ServerListScreen.ipSelect] + (isConnected ? " connected" : " disconnect");
			int textWidth = mFont.tahoma_7_white.getWidth(text);
			int dotX = GameCanvas.w - 2 - textWidth - 8;
			int dotY = num + 30 + 5;
			ServerListScreen.paintConnectionDot(g, dotX, dotY, isConnected);
			mFont.tahoma_7_white.drawString(g, text, GameCanvas.w - 2, num + 30, 1, mFont.tahoma_7_grey);
		}
		catch (Exception ex)
		{
		}
		if (!ServerListScreen.isGetData || ServerListScreen.loadScreen)
		{
			if (mSystem.clientType == 1 && !GameCanvas.isTouch)
			{
				mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
			}
			else
			{
				mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, num, 1, mFont.tahoma_7_grey);
			}
		}
		else
		{
			mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, num, 1, mFont.tahoma_7_grey);
		}
		int num2 = (GameCanvas.w < 200) ? 160 : 180;
		ServerListScreen.paintDeleteData(g);
		if (!ServerListScreen.loadScreen && !ServerListScreen.bigOk)
		{
			g.drawImage(LoginScr.imgTitle, GameCanvas.hw, GameCanvas.hh - 32, 3);
				if (!ServerListScreen.isGetData)
				{
					mFont.tahoma_7b_white.drawString(g, mResources.taidulieudechoi, GameCanvas.hw, GameCanvas.hh + 24, 2);
					if (ServerListScreen.cmdDownload != null)
					{
						ServerListScreen.cmdDownload.paint(g);
					}
				}
				else
				{
					if (ServerListScreen.cmdDownload != null)
					{
						ServerListScreen.cmdDownload.paint(g);
					}
					mFont.tahoma_7b_white.drawString(g, mResources.downloading_data + ServerListScreen.percent + "%", GameCanvas.w / 2, GameCanvas.hh + 24, 2);
					GameScr.paintOngMauPercent(GameScr.frBarPow20, GameScr.frBarPow21, GameScr.frBarPow22, (float)(GameCanvas.w / 2 - 50), (float)(GameCanvas.hh + 45), 100, 100f, g);
					GameScr.paintOngMauPercent(GameScr.frBarPow0, GameScr.frBarPow1, GameScr.frBarPow2, (float)(GameCanvas.w / 2 - 50), (float)(GameCanvas.hh + 45), 100, (float)ServerListScreen.percent, g);
			}
		}
		else
		{
			int num3 = GameCanvas.hh - 15 * this.cmd.Length - 15;
			if (num3 < 25)
			{
				num3 = 25;
			}
			if (LoginScr.imgTitle != null)
			{
				g.drawImage(LoginScr.imgTitle, GameCanvas.hw, num3, 3);
			}
			if (ServerListScreen.isNewUI)
			{
				this.paint_UI_New(g);
			}
			else
			{
				int num4 = this.cmd.Length;
				if (mGraphics.zoomLevel > 1)
				{
				}
				for (int i = 0; i < num4; i++)
				{
					this.cmd[i].paint(g);
				}
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				int serverBtnIndex = 2 + this.nCmdPlay;
				if (serverBtnIndex >= 0 && serverBtnIndex < this.cmd.Length && this.cmd[serverBtnIndex] != null)
				{
					bool isConnected = ServerListScreen.isServerOnline || Session_ME.gI().isConnected();
					int captionWidth = mFont.tahoma_7b_dark.getWidth(this.cmd[serverBtnIndex].caption);
					int btnDotX = ((GameCanvas.w - captionWidth) >> 1) - 10;
					int btnDotY = this.cmd[serverBtnIndex].y + 11;
					ServerListScreen.paintConnectionDot(g, btnDotX, btnDotY, isConnected);
				}
			}
		}
		base.paint(g);
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x00097248 File Offset: 0x00095448
	public void selectServer()
	{
		ServerListScreen.flagServer = 30;
		GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
		Session_ME.gI().close();
		GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
		GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
		GameMidlet.LANGUAGE = (int)ServerListScreen.language[ServerListScreen.ipSelect];
		Rms.saveRMSInt(ServerListScreen.RMS_svselect, ServerListScreen.ipSelect);
		Res.err("1>>>saveRMSInt:  RMS_svselect == " + ServerListScreen.ipSelect);
		if ((int)ServerListScreen.language[ServerListScreen.ipSelect] != (int)mResources.language)
		{
			mResources.loadLanguague(ServerListScreen.language[ServerListScreen.ipSelect]);
		}
		LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
		this.initCommand();
		ServerListScreen.loadScreen = true;
		ServerListScreen.countDieConnect = 0;
		Controller.isConnectOK = false;
		ServerListScreen.testConnect = -1;
		ServerListScreen.isAutoConect = true;
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x00097324 File Offset: 0x00095524
	public override void update()
	{
		if (ServerListScreen.waitToLogin)
		{
			ServerListScreen.tWaitToLogin++;
			if (ServerListScreen.tWaitToLogin == 50)
			{
				GameCanvas.serverScreen.selectServer();
				ServerListScreen.waitToLogin = false;
			}
			if (ServerListScreen.tWaitToLogin == 100)
			{
				if (GameCanvas.loginScr == null)
				{
					GameCanvas.loginScr = new LoginScr();
				}
				GameCanvas.loginScr.doLogin();
				Service.gI().finishUpdate();
				ServerListScreen.waitToLogin = false;
			}
		}
		for (int i = 0; i < this.cmd.Length; i++)
		{
			if (i == ServerListScreen.selected)
			{
				this.cmd[i].isFocus = true;
			}
			else
			{
				this.cmd[i].isFocus = false;
			}
		}
		GameScr.cmx++;
		if (!ServerListScreen.loadScreen && (ServerListScreen.bigOk || ServerListScreen.percent == 100))
		{
			ServerListScreen.cmdDownload = null;
		}
		base.update();
		ServerListScreen.UpdateServerCheck();
		if (global::Char.isLoadingMap)
		{
			return;
		}
		if (!ServerListScreen.loadScreen)
		{
			return;
		}
		if (!ServerListScreen.isAutoConect)
		{
			return;
		}
		if (GameCanvas.currentScreen != this)
		{
			return;
		}
		if (!Session_ME.gI().isConnected() && !Session_ME.connecting)
		{
			if (mSystem.currentTimeMillis() > ServerListScreen.count_reConnect)
			{
				ServerListScreen.count_reConnect = mSystem.currentTimeMillis() + 8000L;
				ServerListScreen.SetIpSelect(ServerListScreen.ipSelect, true);
				Session_ME.gI().close();
				ServerListScreen.ConnectIP();
			}
		}
		else
		{
			ServerListScreen.count_reConnect = mSystem.currentTimeMillis() + 8000L;
		}
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x000088B4 File Offset: 0x00006AB4
	private void processInput()
	{
		if (ServerListScreen.loadScreen)
		{
			this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
		}
		else
		{
			this.center = ServerListScreen.cmdDownload;
		}
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x000088F3 File Offset: 0x00006AF3
	public static void updateDeleteData()
	{
		if (ServerListScreen.cmdDeleteRMS != null && ServerListScreen.cmdDeleteRMS.isPointerPressInside())
		{
			ServerListScreen.cmdDeleteRMS.performAction();
		}
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x00008918 File Offset: 0x00006B18
	public static void paintDeleteData(mGraphics g)
	{
		if (ServerListScreen.cmdDeleteRMS != null)
		{
			mFont.tahoma_7_white.drawString(g, mResources.xoadulieu, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
		}
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x000974A8 File Offset: 0x000956A8
	public override void updateKey()
	{
		if (GameCanvas.isTouch)
		{
			ServerListScreen.updateDeleteData();
			if (this.cmdCallHotline != null && this.cmdCallHotline.isPointerPressInside())
			{
				this.cmdCallHotline.performAction();
			}
			if (!ServerListScreen.loadScreen)
			{
				if (ServerListScreen.cmdDownload != null && ServerListScreen.cmdDownload.isPointerPressInside())
				{
					ServerListScreen.cmdDownload.performAction();
				}
				base.updateKey();
				return;
			}
			if (ServerListScreen.isNewUI)
			{
				for (int i = 0; i < this.cmd_New_Ui.Length; i++)
				{
					if (this.cmd_New_Ui[i] != null && this.cmd_New_Ui[i].isPointerPressInside())
					{
						this.cmd_New_Ui[i].performAction();
					}
				}
			}
			else
			{
				int num = this.cmd.Length;
				if (mGraphics.zoomLevel > 1)
				{
				}
				for (int j = 0; j < num; j++)
				{
					if (this.cmd[j] != null && this.cmd[j].isPointerPressInside())
					{
						this.cmd[j].performAction();
					}
				}
			}
		}
		else if (ServerListScreen.loadScreen)
		{
			if (GameCanvas.keyPressed[8])
			{
				int num2 = (mGraphics.zoomLevel <= 1) ? 4 : 2;
				GameCanvas.keyPressed[8] = false;
				ServerListScreen.selected++;
				if (ServerListScreen.selected > num2)
				{
					ServerListScreen.selected = 0;
				}
				this.processInput();
			}
			if (GameCanvas.keyPressed[2])
			{
				int num3 = (mGraphics.zoomLevel <= 1) ? 4 : 2;
				GameCanvas.keyPressed[2] = false;
				ServerListScreen.selected--;
				if (ServerListScreen.selected < 0)
				{
					ServerListScreen.selected = num3;
				}
				this.processInput();
			}
		}
		if (ServerListScreen.isWait)
		{
			return;
		}
		base.updateKey();
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x0009767C File Offset: 0x0009587C
	public static void saveIP()
	{
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeByte(mResources.language);
			dataOutputStream.writeByte((sbyte)ServerListScreen.nameServer.Length);
			for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
			{
				dataOutputStream.writeUTF(ServerListScreen.nameServer[i]);
				dataOutputStream.writeUTF(ServerListScreen.address[i]);
				dataOutputStream.writeShort(ServerListScreen.port[i]);
				dataOutputStream.writeByte(ServerListScreen.language[i]);
				try
				{
					dataOutputStream.writeByte(ServerListScreen.typeSv[i]);
				}
				catch (Exception ex)
				{
					dataOutputStream.writeByte(0);
				}
				try
				{
					dataOutputStream.writeByte(ServerListScreen.isNew[i]);
				}
				catch (Exception ex2)
				{
					dataOutputStream.writeByte(0);
				}
			}
			dataOutputStream.writeByte(ServerListScreen.serverPriority);
			Rms.saveRMS(ServerListScreen.RMS_NRlink, dataOutputStream.toByteArray());
			dataOutputStream.close();
			SplashScr.loadIP();
		}
		catch (Exception ex3)
		{
		}
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x0009778C File Offset: 0x0009598C
	public static bool allServerConnected()
	{
		for (int i = 0; i < 2; i++)
		{
			if (!ServerListScreen.hasConnected[i])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x000977BC File Offset: 0x000959BC
	public static void loadIP()
	{
		sbyte[] array = Rms.loadRMS(ServerListScreen.RMS_NRlink);
		if (array == null)
		{
			ServerListScreen.getServerList(ServerListScreen.linkDefault);
			return;
		}
		DataInputStream dataInputStream = new DataInputStream(array);
		if (dataInputStream == null)
		{
			return;
		}
		try
		{
			mResources.loadLanguague(dataInputStream.readByte());
			sbyte b = dataInputStream.readByte();
			ServerListScreen.nameServer = new string[(int)b];
			ServerListScreen.address = new string[(int)b];
			ServerListScreen.port = new short[(int)b];
			ServerListScreen.language = new sbyte[(int)b];
			ServerListScreen.typeSv = new sbyte[(int)b];
			ServerListScreen.isNew = new sbyte[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				ServerListScreen.nameServer[i] = dataInputStream.readUTF();
				ServerListScreen.address[i] = dataInputStream.readUTF();
				ServerListScreen.port[i] = dataInputStream.readShort();
				ServerListScreen.language[i] = dataInputStream.readByte();
				try
				{
					ServerListScreen.typeSv[i] = dataInputStream.readByte();
				}
				catch (Exception ex)
				{
					ServerListScreen.typeSv[i] = 0;
				}
				try
				{
					ServerListScreen.isNew[i] = dataInputStream.readByte();
				}
				catch (Exception ex2)
				{
					ServerListScreen.isNew[i] = 0;
				}
			}
			ServerListScreen.serverPriority = dataInputStream.readByte();
			dataInputStream.close();
			SplashScr.loadIP();
		}
		catch (Exception ex3)
		{
		}
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x00097920 File Offset: 0x00095B20
	public override void switchToMe()
	{
		Res.outz(">>>>switchToMe  ServerListScreen: ");
		EffectManager.remove();
		GameScr.cmy = 0;
		GameScr.cmx = 0;
		this.initCommand();
		ServerListScreen.isWait = false;
		GameCanvas.loginScr = null;
		ServerListScreen.loadScreen = true;
		GameCanvas.loadBG(0);
		ServerListScreen.bigOk = true;
		this.cmd[2 + this.nCmdPlay].caption = mResources.server + ": " + ServerListScreen.nameServer[ServerListScreen.ipSelect];
		this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
		this.cmd[1 + this.nCmdPlay].caption = mResources.change_account;
		if (this.cmd.Length == 4 + this.nCmdPlay)
		{
			this.cmd[3 + this.nCmdPlay].caption = mResources.option;
		}
		global::Char.isLoadingMap = false;
		mSystem.resetCurInapp();
		base.switchToMe();
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x00097A4C File Offset: 0x00095C4C
	public void switchToMe2()
	{
		GameScr.cmy = 0;
		GameScr.cmx = 0;
		this.initCommand();
		ServerListScreen.isWait = false;
		GameCanvas.loginScr = null;
		ServerListScreen.loadScreen = true;
		GameCanvas.loadBG(0);
		ServerListScreen.bigOk = true;
		this.cmd[2 + this.nCmdPlay].caption = mResources.server + ": " + ServerListScreen.nameServer[ServerListScreen.ipSelect];
		this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
		this.cmd[1 + this.nCmdPlay].caption = mResources.change_account;
		if (this.cmd.Length == 4 + this.nCmdPlay)
		{
			this.cmd[3 + this.nCmdPlay].caption = mResources.option;
		}
		mSystem.resetCurInapp();
		base.switchToMe();
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x000045ED File Offset: 0x000027ED
	public void connectOk()
	{
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x00097B64 File Offset: 0x00095D64
	public void cancel()
	{
		if (GameCanvas.serverScreen == null)
		{
			GameCanvas.serverScreen = new ServerListScreen();
		}
		ServerListScreen.demPercent = 0;
		ServerListScreen.percent = 0;
		ServerListScreen.stopDownload = true;
		GameCanvas.serverScreen.show2();
		ServerListScreen.isGetData = false;
		ServerListScreen.cmdDownload.isFocus = true;
		this.center = new Command(string.Empty, this, 2, null);
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x00097BC8 File Offset: 0x00095DC8
	public void perform(int idAction, object p)
	{
		Res.outz("perform " + idAction);
		if (idAction == 1000)
		{
			GameCanvas.connect();
		}
		if (idAction == 1 || idAction == 4)
		{
			Session_ME.gI().close();
			ServerListScreen.isAutoConect = false;
			ServerListScreen.countDieConnect = 0;
			ServerListScreen.loadScreen = true;
			ServerListScreen.testConnect = 0;
			ServerListScreen.isGetData = false;
			mSystem.println(">>>>>isGetData: " + ServerListScreen.isGetData);
			Rms.clearAll();
			this.switchToMe();
		}
		if (idAction == 2)
		{
			ServerListScreen.stopDownload = false;
			ServerListScreen.cmdDownload = new Command(mResources.huy, this, 4, null);
			ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
			ServerListScreen.cmdDownload.y = GameCanvas.hh + 65;
			this.right = null;
			if (!GameCanvas.isTouch)
			{
				ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
				ServerListScreen.cmdDownload.y = GameCanvas.h - mScreen.cmdH - 1;
			}
			this.center = new Command(string.Empty, this, 4, null);
			if (!ServerListScreen.isGetData)
			{
				Service.gI().getResource(1, null);
				if (!GameCanvas.isTouch)
				{
					ServerListScreen.cmdDownload.isFocus = true;
					this.center = new Command(string.Empty, this, 4, null);
					mSystem.println(">>>>>isGetData: " + ServerListScreen.isGetData);
				}
				ServerListScreen.isGetData = true;
			}
		}
		if (idAction == 3)
		{
			Res.outz("toi day");
			this.Login_New();
		}
		if (idAction == 10100)
		{
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
			GameCanvas.connect();
			Service.gI().login2(string.Empty);
			Res.outz("tao user ao");
			GameCanvas.startWaitDlg();
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
		}
		if (idAction == 5)
		{
			ServerListScreen.doUpdateServer();
			if (ServerListScreen.nameServer.Length == 1)
			{
				return;
			}
			MyVector myVector = new MyVector(string.Empty);
			for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
			{
				myVector.addElement(new Command(ServerListScreen.nameServer[i], this, 6, null));
			}
			GameCanvas.menu.startAt(myVector, 0);
			if (!GameCanvas.isTouch)
			{
				GameCanvas.menu.menuSelectedItem = ServerListScreen.ipSelect;
			}
		}
		if (idAction == 6)
		{
			ServerListScreen.SetIpSelect(GameCanvas.menu.menuSelectedItem, false);
			this.selectServer();
		}
		if (idAction == 7)
		{
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
		}
		if (idAction == 8)
		{
			bool flag = Rms.loadRMSInt("lowGraphic") == 1;
			MyVector myVector2 = new MyVector("cau hinh");
			myVector2.addElement(new Command(mResources.cauhinhthap, this, 9, null));
			myVector2.addElement(new Command(mResources.cauhinhcao, this, 10, null));
			GameCanvas.menu.startAt(myVector2, 0);
			if (flag)
			{
				GameCanvas.menu.menuSelectedItem = 0;
			}
			else
			{
				GameCanvas.menu.menuSelectedItem = 1;
			}
		}
		if (idAction == 9)
		{
			Rms.saveRMSInt("lowGraphic", 1);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 10)
		{
			Rms.saveRMSInt("lowGraphic", 0);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 11)
		{
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
			string text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect);
			if (text == null || text.Equals(string.Empty))
			{
				Service.gI().login2(string.Empty);
			}
			else
			{
				GameCanvas.loginScr.isLogin2 = true;
				GameCanvas.connect();
				Service.gI().setClientType();
				Service.gI().login(text, string.Empty, GameMidlet.VERSION, 1);
			}
			GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
			Res.outz("tao user ao");
		}
		if (idAction == 12)
		{
			GameMidlet.instance.exit();
		}
		if (idAction == 13 && (!ServerListScreen.isGetData || ServerListScreen.loadScreen))
		{
			switch (mSystem.clientType)
			{
			case 1:
				mSystem.callHotlineJava();
				break;
			case 3:
			case 5:
				mSystem.callHotlineIphone();
				break;
			case 4:
				mSystem.callHotlinePC();
				break;
			case 6:
				mSystem.callHotlineWindowsPhone();
				break;
			}
		}
		if (idAction == 14)
		{
			Command cmdYes = new Command(mResources.YES, GameCanvas.serverScreen, 15, null);
			Command cmdNo = new Command(mResources.NO, GameCanvas.serverScreen, 16, null);
			GameCanvas.startYesNoDlg(mResources.deletaDataNote, cmdYes, cmdNo);
		}
		if (idAction == 15)
		{
			Rms.clearAll();
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 16)
		{
			InfoDlg.hide();
			GameCanvas.currentDialog = null;
		}
		if (idAction == 17)
		{
			if (GameCanvas.serverScr == null)
			{
				GameCanvas.serverScr = new ServerScr();
			}
			GameCanvas.serverScr.switchToMe();
		}
		if (idAction == 18)
		{
			GameCanvas.endDlg();
			InfoDlg.hide();
			if (GameCanvas.serverScr == null)
			{
				GameCanvas.serverScr = new ServerScr();
			}
			GameCanvas.serverScr.switchToMe();
		}
		if (idAction == 19)
		{
			if (mSystem.clientType == 1)
			{
				InfoDlg.hide();
				GameCanvas.currentDialog = null;
			}
			else
			{
				ServerListScreen.countDieConnect = 0;
				ServerListScreen.testConnect = 0;
				ServerListScreen.isAutoConect = true;
			}
		}
		if (idAction == 17020)
		{
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.ShowAutoManager();
		}
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x00098170 File Offset: 0x00096370
	public void init()
	{
		if (!ServerListScreen.loadScreen)
		{
			ServerListScreen.cmdDownload = new Command(mResources.taidulieu, this, 2, null);
			ServerListScreen.cmdDownload.isFocus = true;
			ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
			ServerListScreen.cmdDownload.y = GameCanvas.hh + 45;
			if (ServerListScreen.cmdDownload.y > GameCanvas.h - 26)
			{
				ServerListScreen.cmdDownload.y = GameCanvas.h - 26;
			}
		}
		if (!GameCanvas.isTouch)
		{
			ServerListScreen.selected = 0;
			this.processInput();
		}
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x00098210 File Offset: 0x00096410
	public void show2()
	{
		Debug.LogError(">>>>ServerListScreen show2: ");
		GameScr.cmx = 0;
		GameScr.cmy = 0;
		this.initCommand();
		ServerListScreen.loadScreen = false;
		ServerListScreen.percent = 0;
		ServerListScreen.bigOk = false;
		ServerListScreen.isGetData = false;
		ServerListScreen.p = 0;
		ServerListScreen.demPercent = 0;
		ServerListScreen.strWait = mResources.PLEASEWAIT;
		global::Char.isLoadingMap = false;
		this.init();
		base.switchToMe();
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x0009827C File Offset: 0x0009647C
	public void setLinkDefault(sbyte language)
	{
		if ((int)language == 2)
		{
			if (mSystem.clientType == 1)
			{
				ServerListScreen.linkDefault = ServerListScreen.javaIn;
			}
			else
			{
				ServerListScreen.linkDefault = ServerListScreen.smartPhoneIn;
			}
		}
		else if ((int)language == 1)
		{
			ServerListScreen.linkDefault = ServerListScreen.javaE;
			if (mSystem.clientType == 1)
			{
				ServerListScreen.linkDefault = ServerListScreen.javaE;
			}
			else
			{
				ServerListScreen.linkDefault = ServerListScreen.smartPhoneE;
			}
		}
		else
		{
			ServerListScreen.linkDefault = ServerListScreen.javaVN;
			if (mSystem.clientType == 1)
			{
				ServerListScreen.linkDefault = ServerListScreen.javaVN;
			}
			else
			{
				ServerListScreen.linkDefault = ServerListScreen.smartPhoneVN;
			}
		}
		mSystem.AddIpTest();
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x00098328 File Offset: 0x00096528
	public static void ConnectIP()
	{
		GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
		GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
		mResources.loadLanguague(ServerListScreen.language[ServerListScreen.ipSelect]);
		LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
		GameCanvas.connect();
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x0009837C File Offset: 0x0009657C
	public static void SetIpSelect(int index, bool issave)
	{
		Debug.LogError(string.Concat(new object[]
		{
			">>>>SetIpSelect: ",
			index,
			"  save:",
			issave
		}));
		ServerListScreen.ipSelect = index;
		if (issave)
		{
			Rms.saveRMSInt(ServerListScreen.RMS_svselect, ServerListScreen.ipSelect);
			Res.err("2>>>saveRMSInt:  RMS_svselect == " + ServerListScreen.ipSelect);
		}
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x000983F0 File Offset: 0x000965F0
	public void Login_New()
	{
		if (GameCanvas.loginScr == null)
		{
			GameCanvas.loginScr = new LoginScr();
		}
		GameCanvas.loginScr.switchToMe();
		bool flag = false;
		bool flag2 = false;
		string text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect);
		try
		{
			if (!Rms.loadRMSString("acc").Equals(string.Empty))
			{
				flag = true;
			}
			if (!text.Equals(string.Empty))
			{
				flag2 = true;
			}
		}
		catch (Exception)
		{
		}
		GameCanvas.connect();
		Service.gI().setClientType();
		if (!flag && !flag2)
		{
			if (text == null || text.Equals(string.Empty))
			{
				Debug.LogError(">>>>Login_New: login2: ");
				Service.gI().login2(string.Empty);
			}
			else
			{
				GameCanvas.loginScr.isLogin2 = true;
				Service.gI().login(text, string.Empty, GameMidlet.VERSION, 1);
			}
			Rms.saveRMSInt(ServerListScreen.RMS_svselect, ServerListScreen.ipSelect);
			if (Session_ME.connected)
			{
				GameCanvas.startWaitDlg();
			}
			else
			{
				GameCanvas.startOK(mResources.maychutathoacmatsong + " [3]", 8884, null);
			}
		}
		else
		{
			GameCanvas.loginScr.doLogin();
		}
		LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x00098550 File Offset: 0x00096750
	public static void LoadRMS_ExtraLink()
	{
		sbyte[] array = Rms.loadRMS(ServerListScreen.RMS_NR_Extralink);
		if (array == null)
		{
			Controller.isEXTRA_LINK = false;
			return;
		}
		DataInputStream dataInputStream = new DataInputStream(array);
		if (dataInputStream == null)
		{
			return;
		}
		try
		{
			sbyte b = dataInputStream.readByte();
			ServerListScreen.typeClass = new sbyte[(int)b];
			ServerListScreen.listChar = new global::Char[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				ServerListScreen.typeClass[i] = dataInputStream.readByte();
				if ((int)ServerListScreen.typeClass[i] > -1)
				{
					ServerListScreen.isHaveChar = true;
					ServerListScreen.listChar[i] = new global::Char();
					ServerListScreen.listChar[i].cgender = (int)ServerListScreen.typeClass[i];
					ServerListScreen.listChar[i].head = (int)dataInputStream.readShort();
					ServerListScreen.listChar[i].body = (int)dataInputStream.readShort();
					ServerListScreen.listChar[i].leg = (int)dataInputStream.readShort();
					ServerListScreen.listChar[i].bag = (int)dataInputStream.readShort();
					ServerListScreen.listChar[i].cName = dataInputStream.readUTF();
				}
			}
			dataInputStream.close();
			Controller.isEXTRA_LINK = true;
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00098678 File Offset: 0x00096878
	public static void saveRMS_ExtraLink()
	{
		if (ServerListScreen.typeClass == null)
		{
			return;
		}
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeByte((sbyte)ServerListScreen.typeClass.Length);
			for (int i = 0; i < ServerListScreen.typeClass.Length; i++)
			{
				dataOutputStream.writeByte(ServerListScreen.typeClass[i]);
				if ((int)ServerListScreen.typeClass[i] > -1 && ServerListScreen.listChar != null && ServerListScreen.listChar[i] != null)
				{
					dataOutputStream.writeShort((short)ServerListScreen.listChar[i].head);
					dataOutputStream.writeShort((short)ServerListScreen.listChar[i].body);
					dataOutputStream.writeShort((short)ServerListScreen.listChar[i].leg);
					dataOutputStream.writeShort((short)ServerListScreen.listChar[i].bag);
					dataOutputStream.writeUTF(ServerListScreen.listChar[i].cName);
				}
			}
			Rms.saveRMS(ServerListScreen.RMS_NR_Extralink, dataOutputStream.toByteArray());
			dataOutputStream.close();
			SplashScr.loadIP();
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x00098784 File Offset: 0x00096984
	public void Set_UI_New()
	{
		if (!GameCanvas.isTouch)
		{
			return;
		}
		ServerListScreen.isNewUI = true;
		this.cmd_New_Ui = new Command[3];
		int num = GameCanvas.hh - 15 * this.cmd_New_Ui.Length + 28;
		for (int i = 0; i < this.cmd_New_Ui.Length; i++)
		{
			if (i != 0)
			{
				if (i == 1)
				{
					this.cmd_New_Ui[1] = new Command(mResources.change_account, this, 7, null);
				}
				if (i == 2)
				{
					this.cmd_New_Ui[2] = new Command("Auto Manager", this, 17020, null);
				}
			}
			else
			{
				this.cmd_New_Ui[0] = new Command(string.Empty, this, 3, null);
				this.cmd_New_Ui[0].caption = mResources.playNew;
				if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect) != null)
				{
					this.cmd_New_Ui[0].caption = mResources.choitiep;
				}
			}
			this.cmd_New_Ui[i].y = num;
			this.cmd_New_Ui[i].setType();
			this.cmd_New_Ui[i].x = (GameCanvas.w - this.cmd_New_Ui[i].w) / 2;
			num += 30;
		}
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x000988A8 File Offset: 0x00096AA8
	public void paint_UI_New(mGraphics g)
	{
		if (!ServerListScreen.isNewUI)
		{
			return;
		}
		for (int i = 0; i < this.cmd_New_Ui.Length; i++)
		{
			this.cmd_New_Ui[i].paint(g);
		}
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x000988E8 File Offset: 0x00096AE8
	public static void CheckBack_ServerListScreen()
	{
		if (GameCanvas.serverScreen == null)
		{
			GameCanvas.serverScreen = new ServerListScreen();
		}
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
		catch (Exception ex)
		{
		}
		Debug.LogError(string.Concat(new object[]
		{
			">>>>CheckBack_ServerListScreen: ",
			ServerListScreen.ipSelect,
			"  auto login:",
			ServerListScreen.isAutoLogin
		}));
		if (ServerListScreen.ipSelect == -1 || !ServerListScreen.isAutoLogin)
		{
			GameCanvas.serverScreen.switchToMe();
		}
		else if (!flag && !flag2)
		{
			GameCanvas.serverScreen.switchToMe();
		}
		else
		{
			Controller.isEXTRA_LINK = false;
			GameCanvas.serverScreen.switchToMe();
			GameCanvas.serverScreen.Login_New();
		}
	}

	// Token: 0x0400128B RID: 4747
	public static string[] nameServer;

	// Token: 0x0400128C RID: 4748
	public static string[] address;

	// Token: 0x0400128D RID: 4749
	public static sbyte serverPriority;

	// Token: 0x0400128E RID: 4750
	public static bool[] hasConnected;

	// Token: 0x0400128F RID: 4751
	public static short[] port;

	// Token: 0x04001290 RID: 4752
	public static int selected;

	// Token: 0x04001291 RID: 4753
	public static bool isWait;

	// Token: 0x04001292 RID: 4754
	public static Command cmdUpdateServer;

	// Token: 0x04001293 RID: 4755
	public static sbyte[] language;

	// Token: 0x04001294 RID: 4756
	public static sbyte[] typeSv;

	// Token: 0x04001295 RID: 4757
	public static sbyte[] isNew;

	// Token: 0x04001296 RID: 4758
	public static sbyte[] typeClass;

	// Token: 0x04001297 RID: 4759
	public static global::Char[] listChar;

	// Token: 0x04001298 RID: 4760
	public static bool isHaveChar;

	// Token: 0x04001299 RID: 4761
	private Command[] cmd;

	// Token: 0x0400129A RID: 4762
	private Command cmdCallHotline;

	// Token: 0x0400129B RID: 4763
	private int nCmdPlay;

	// Token: 0x0400129C RID: 4764
	public static Command cmdDeleteRMS;

	// Token: 0x0400129D RID: 4765
	private int lY;

	// Token: 0x0400129E RID: 4766
	public static string smartPhoneVN = "Vũ trụ 1:dragon1.teamobi.com:14445:0:0:0,Vũ trụ 2:dragon2.teamobi.com:14445:0:0:0,Vũ trụ 3:dragon3.teamobi.com:14445:0:0:0,Vũ trụ 4:dragon4.teamobi.com:14445:0:0:0,Vũ trụ 5:dragon5.teamobi.com:14445:0:0:0,Vũ trụ 6:dragon6.teamobi.com:14445:0:0:0,Vũ trụ 7:dragon7.teamobi.com:14445:0:0:0,Vũ trụ 8:dragon10.teamobi.com:14446:0:0:0,Vũ trụ 9:dragon10.teamobi.com:14447:0:0:0,Vũ trụ 10:dragon10.teamobi.com:14445:0:0:0,Vũ trụ 11:dragon11.teamobi.com:14445:0:0:0,Võ đài liên vũ trụ:dragonwar.teamobi.com:20000:0:0:0,Universe 1:dragon.indonaga.com:14445:1:0:0,Naga:dragon.indonaga.com:14446:2:0:0,0,0";

	// Token: 0x0400129F RID: 4767
	public static string javaVN = "Vũ trụ 1:112.213.94.23:14445:0:0:0,Vũ trụ 2:210.211.109.199:14445:0:0:0,Vũ trụ 3:112.213.85.88:14445:0:0:0,Vũ trụ 4:27.0.12.164:14445:0:0:0,Vũ trụ 5:27.0.12.16:14445:0:0:0,Vũ trụ 6:27.0.12.173:14445:0:0:0,Vũ trụ 7:112.213.94.223:14445:0:0:0,Vũ trụ 8:27.0.14.66:14446:0:0:0,Vũ trụ 9:27.0.14.66:14447:0:0:0,Vũ trụ 10:27.0.14.66:14445:0:0:0,Vũ trụ 11:112.213.85.35:14445:0:0:0,Võ đài liên vũ trụ:27.0.12.173:20000:0:0:0,Universe 1:52.74.230.22:14445:1:0:0,Naga:52.74.230.22:14446:2:0:0,0,0";

	// Token: 0x040012A0 RID: 4768
	public static string smartPhoneIn = "Naga:dragon.indonaga.com:14446:2:0:0,2,0";

	// Token: 0x040012A1 RID: 4769
	public static string javaIn = "Naga:52.74.230.22:14446:2:0:0,2,0";

	// Token: 0x040012A2 RID: 4770
	public static string smartPhoneE = "Universe 1:dragon.indonaga.com:14445:1:0:0,1,0";

	// Token: 0x040012A3 RID: 4771
	public static string javaE = "Universe 1:52.74.230.22:14445:1:0:0,1,0";

	// Token: 0x040012A4 RID: 4772
	public static string linkGetHost = "http://112.213.94.23/mod/server_extra.php";

	// Token: 0x040012A5 RID: 4773
	public static string linkDefault = ServerListScreen.javaVN;

	// Token: 0x040012A6 RID: 4774
	public const sbyte languageVersion = 2;

	// Token: 0x040012A7 RID: 4775
	public new int keyTouch = -1;

	// Token: 0x040012A8 RID: 4776
	private int tam;

	// Token: 0x040012A9 RID: 4777
	public static bool stopDownload;

	// Token: 0x040012AA RID: 4778
	public static string linkweb = "http://ngocrongonline.com";

	// Token: 0x040012AB RID: 4779
	public static int countDieConnect;

	// Token: 0x040012AC RID: 4780
	public static bool waitToLogin;

	// Token: 0x040012AD RID: 4781
	public static int tWaitToLogin;

	// Token: 0x040012AE RID: 4782
	public static long count_reConnect;

	// Token: 0x040012AF RID: 4783
	public static string RMS_NRlink = "NRlink3";

	// Token: 0x040012B0 RID: 4784
	public static int ipSelect;

	// Token: 0x040012B1 RID: 4785
	public static int flagServer;

	// Token: 0x040012B2 RID: 4786
	public static bool bigOk;

	// Token: 0x040012B3 RID: 4787
	public static int percent;

	// Token: 0x040012B4 RID: 4788
	public static string strWait;

	// Token: 0x040012B5 RID: 4789
	public static int nBig;

	// Token: 0x040012B6 RID: 4790
	public static int nBg;

	// Token: 0x040012B7 RID: 4791
	public static int demPercent;

	// Token: 0x040012B8 RID: 4792
	public static int maxBg;

	// Token: 0x040012B9 RID: 4793
	public static bool isGetData = false;

	// Token: 0x040012BA RID: 4794
	public static Command cmdDownload;

	// Token: 0x040012BB RID: 4795
	private Command cmdStart;

	// Token: 0x040012BC RID: 4796
	public string dataSize;

	// Token: 0x040012BD RID: 4797
	public static int p;

	// Token: 0x040012BE RID: 4798
	public static int testConnect = -1;

	// Token: 0x040012BF RID: 4799
	public static bool loadScreen;

	// Token: 0x040012C0 RID: 4800
	public static bool isAutoConect = true;

	// Token: 0x040012C1 RID: 4801
	public static string RMS_svselect = "svselect";

	// Token: 0x040012C2 RID: 4802
	public static string RMS_NR_Extralink = "NRlink_extra";

	// Token: 0x040012C3 RID: 4803
	private Command[] cmd_New_Ui;

	// Token: 0x040012C4 RID: 4804
	public static bool isNewUI;

	// Token: 0x040012C5 RID: 4805
	public static bool isAutoLogin = true;

	public static bool isServerOnline;

	public static void paintConnectionDot(mGraphics g, int x, int y, bool isConnected)
	{
		int dotColor = isConnected ? 0x00E600 : 0x888888;
		int borderColor = isConnected ? 0x003300 : 0x222222;
		int shineColor = isConnected ? 0x99FF99 : 0xBBBBBB;

		g.setColor(borderColor);
		g.fillRect(x - 3, y - 4, 6, 8);
		g.fillRect(x - 4, y - 3, 8, 6);

		g.setColor(dotColor);
		g.fillRect(x - 2, y - 3, 4, 6);
		g.fillRect(x - 3, y - 2, 6, 4);

		g.setColor(shineColor);
		g.fillRect(x - 2, y - 2, 2, 2);
	}

	public static void UpdateServerCheck()
	{
		ServerListScreen.isServerOnline = Session_ME.gI().isConnected();
	}
}
