using System;

// Token: 0x020000C8 RID: 200
public class SplashScr : mScreen
{
	// Token: 0x06000A36 RID: 2614 RVA: 0x00008949 File Offset: 0x00006B49
	public SplashScr()
	{
		SplashScr.instance = this;
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x0000895F File Offset: 0x00006B5F
	public static void loadSplashScr()
	{
		SplashScr.splashScrStat = 0;
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x0009A374 File Offset: 0x00098574
	public override void update()
	{
		SplashScr.splashScrStat++;
		if (SplashScr.splashScrStat == 30 && !this.isCheckConnect)
		{
			this.isCheckConnect = true;
			if (Rms.loadRMSInt("serverchat") != -1)
			{
				GameScr.isPaintChatVip = (Rms.loadRMSInt("serverchat") == 0);
			}
			if (Rms.loadRMSInt("isPlaySound") != -1)
			{
				GameCanvas.isPlaySound = (Rms.loadRMSInt("isPlaySound") == 1);
			}
			if (GameCanvas.isPlaySound)
			{
				SoundMn.gI().loadSound(TileMap.mapID);
			}
			SoundMn.gI().getStrOption();
			ServerListScreen.loadIP();
		}
		if (SplashScr.splashScrStat >= 150)
		{
			ServerListScreen.loadScreen = true;
			if (Session_ME.gI().isConnected())
			{
				GameCanvas.serverScreen.switchToMe();
			}
			else
			{
				mSystem.onDisconnected();
				if (GameCanvas.serverScreen == null)
				{
					GameCanvas.serverScreen = new ServerListScreen();
				}
				GameCanvas.serverScreen.switchToMe();
			}
		}
		ServerListScreen.updateDeleteData();
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x0009A488 File Offset: 0x00098688
	public static void loadIP()
	{
		Res.err(">>>>>loadIP:  svselect == " + Rms.loadRMSInt(ServerListScreen.RMS_svselect));
		ServerListScreen.SetIpSelect(Rms.loadRMSInt(ServerListScreen.RMS_svselect), false);
		if (ServerListScreen.ipSelect == -1)
		{
			Res.err(">>>loadIP:  svselect == -1");
			if ((int)ServerListScreen.serverPriority == -1)
			{
				ServerListScreen.SetIpSelect((int)ServerListScreen.serverPriority, true);
			}
			else
			{
				ServerListScreen.SetIpSelect((int)ServerListScreen.serverPriority, true);
			}
		}
		ServerListScreen.ConnectIP();
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x0009A508 File Offset: 0x00098708
	public override void paint(mGraphics g)
	{
		if (SplashScr.imgLogo != null && SplashScr.splashScrStat < 30)
		{
			g.setColor(16777215);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			g.drawImage(SplashScr.imgLogo, GameCanvas.w / 2, GameCanvas.h / 2, 3);
		}
		if (SplashScr.nData != -1)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			g.drawImage(LoginScr.imgTitle, GameCanvas.w / 2, GameCanvas.h / 2 - 24, StaticObj.BOTTOM_HCENTER);
			GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.h / 2 + 24, g);
			mFont.tahoma_7b_white.drawString(g, mResources.downloading_data + SplashScr.nData * 100 / SplashScr.maxData + "%", GameCanvas.w / 2, GameCanvas.h / 2, 2);
		}
		else if (SplashScr.splashScrStat >= 30)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.hh, g);
			ServerListScreen.paintDeleteData(g);
		}
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x00008967 File Offset: 0x00006B67
	public static void loadImg()
	{
		SplashScr.imgLogo = GameCanvas.loadImage("/gamelogo.png");
	}

	// Token: 0x04001302 RID: 4866
	public static int splashScrStat;

	// Token: 0x04001303 RID: 4867
	private bool isCheckConnect;

	// Token: 0x04001304 RID: 4868
	private bool isSwitchToLogin;

	// Token: 0x04001305 RID: 4869
	public static int nData = -1;

	// Token: 0x04001306 RID: 4870
	public static int maxData = -1;

	// Token: 0x04001307 RID: 4871
	public static SplashScr instance;

	// Token: 0x04001308 RID: 4872
	public static Image imgLogo;

	// Token: 0x04001309 RID: 4873
	private int timeLoading = 10;

	// Token: 0x0400130A RID: 4874
	public long TIMEOUT;
}
