using System;
using Assets.src.e;
using Assets.src.g;
using Mod.DungPham.KoiOctiiu957;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class GameCanvas : IActionListener
{
	// Token: 0x06000A78 RID: 2680 RVA: 0x0009C854 File Offset: 0x0009AA54
	public GameCanvas()
	{
		int num = Rms.loadRMSInt("languageVersion");
		if (num == -1)
		{
			Rms.saveRMSInt("languageVersion", 2);
		}
		else if (num != 2)
		{
			Main.main.doClearRMS();
			Rms.saveRMSInt("languageVersion", 2);
		}
		GameCanvas.clearOldData = Rms.loadRMSInt(GameMidlet.VERSION);
		if (GameCanvas.clearOldData != 1)
		{
			Main.main.doClearRMS();
			Rms.saveRMSInt(GameMidlet.VERSION, 1);
		}
		this.initGame();
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x00008BA2 File Offset: 0x00006DA2
	public static string getPlatformName()
	{
		return "Pc platform xxx";
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x0009C904 File Offset: 0x0009AB04
	public void initGame()
	{
		try
		{
			MotherCanvas.instance.setChildCanvas(this);
			GameCanvas.w = MotherCanvas.instance.getWidthz();
			GameCanvas.h = MotherCanvas.instance.getHeightz();
			GameCanvas.hw = GameCanvas.w / 2;
			GameCanvas.hh = GameCanvas.h / 2;
			GameCanvas.isTouch = true;
			if (GameCanvas.w >= 240)
			{
				GameCanvas.isTouchControl = true;
			}
			if (GameCanvas.w < 320)
			{
				GameCanvas.isTouchControlSmallScreen = true;
			}
			if (GameCanvas.w >= 320)
			{
				GameCanvas.isTouchControlLargeScreen = true;
			}
			GameCanvas.msgdlg = new MsgDlg();
			if (GameCanvas.h <= 160)
			{
				Paint.hTab = 15;
				mScreen.cmdH = 17;
			}
			GameScr.d = ((GameCanvas.w <= GameCanvas.h) ? GameCanvas.h : GameCanvas.w) + 20;
			GameCanvas.instance = this;
			mFont.init();
			mScreen.ITEM_HEIGHT = mFont.tahoma_8b.getHeight() + 8;
			this.initPaint();
			this.loadDust();
			this.loadWaterSplash();
			GameCanvas.panel = new Panel();
			GameCanvas.imgShuriken = GameCanvas.loadImage("/mainImage/myTexture2df.png");
			int num = Rms.loadRMSInt("clienttype");
			if (num != -1)
			{
				if (num > 7)
				{
					Rms.saveRMSInt("clienttype", mSystem.clientType);
				}
				else
				{
					mSystem.clientType = num;
				}
			}
			if (mSystem.clientType == 7 && (Rms.loadRMSString("fake") == null || Rms.loadRMSString("fake") == string.Empty))
			{
				GameCanvas.imgShuriken = GameCanvas.loadImage("/mainImage/wait.png");
			}
			GameCanvas.imgClear = GameCanvas.loadImage("/mainImage/myTexture2der.png");
			GameCanvas.img18 = GameCanvas.loadImage("/mainImage/18+.png");
			GameCanvas.debugUpdate = new MyVector();
			GameCanvas.debugPaint = new MyVector();
			GameCanvas.debugSession = new MyVector();
			for (int i = 0; i < 3; i++)
			{
				GameCanvas.imgBorder[i] = GameCanvas.loadImage("/mainImage/myTexture2dbd" + i + ".png");
			}
			GameCanvas.borderConnerW = mGraphics.getImageWidth(GameCanvas.imgBorder[0]);
			GameCanvas.borderConnerH = mGraphics.getImageHeight(GameCanvas.imgBorder[0]);
			GameCanvas.borderCenterW = mGraphics.getImageWidth(GameCanvas.imgBorder[1]);
			GameCanvas.borderCenterH = mGraphics.getImageHeight(GameCanvas.imgBorder[1]);
			Panel.graphics = Rms.loadRMSInt("lowGraphic");
			GameCanvas.lowGraphic = (Rms.loadRMSInt("lowGraphic") == 1);
			GameScr.isPaintChatVip = (Rms.loadRMSInt("serverchat") != 1);
			global::Char.isPaintAura = (Rms.loadRMSInt("isPaintAura") == 1);
			global::Char.isPaintAura2 = (Rms.loadRMSInt("isPaintAura2") == 1);
			Res.init();
			SmallImage.loadBigImage();
			Panel.WIDTH_PANEL = 176;
			if (Panel.WIDTH_PANEL > GameCanvas.w)
			{
				Panel.WIDTH_PANEL = GameCanvas.w;
			}
			InfoMe.gI().loadCharId();
			Command.btn0left = GameCanvas.loadImage("/mainImage/btn0left.png");
			Command.btn0mid = GameCanvas.loadImage("/mainImage/btn0mid.png");
			Command.btn0right = GameCanvas.loadImage("/mainImage/btn0right.png");
			Command.btn1left = GameCanvas.loadImage("/mainImage/btn1left.png");
			Command.btn1mid = GameCanvas.loadImage("/mainImage/btn1mid.png");
			Command.btn1right = GameCanvas.loadImage("/mainImage/btn1right.png");
			GameCanvas.serverScreen = new ServerListScreen();
			GameCanvas.img18 = GameCanvas.loadImage("/mainImage/18+.png");
			for (int j = 0; j < 7; j++)
			{
				GameCanvas.imgBlue[j] = GameCanvas.loadImage("/effectdata/blue/" + j + ".png");
				GameCanvas.imgViolet[j] = GameCanvas.loadImage("/effectdata/violet/" + j + ".png");
			}
			ServerListScreen.createDeleteRMS();
			GameCanvas.serverScr = new ServerScr();
			GameCanvas.loginScr = new LoginScr();
			GameCanvas._SelectCharScr = new SelectCharScr();
		}
		catch (Exception)
		{
			Debug.LogError("----------------->>>>>>>>>>errr");
		}
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x00008BA9 File Offset: 0x00006DA9
	public static GameCanvas gI()
	{
		return GameCanvas.instance;
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x00008BB0 File Offset: 0x00006DB0
	public void initPaint()
	{
		GameCanvas.paintz = new Paint();
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x00008BBC File Offset: 0x00006DBC
	public static void closeKeyBoard()
	{
		mGraphics.addYWhenOpenKeyBoard = 0;
		GameCanvas.timeOpenKeyBoard = 0;
		Main.closeKeyBoard();
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x0009CCC8 File Offset: 0x0009AEC8
	public void update()
	{
		Mod.DungPham.KoiOctiiu957.MainMod.UpdateGlobal();
		if (GameCanvas.currentScreen == GameCanvas._SelectCharScr)
		{
			if (GameCanvas.gameTick % 2 == 0 && SmallImage.vt_images_watingDowload.size() > 0)
			{
				Small small = (Small)SmallImage.vt_images_watingDowload.elementAt(0);
				Service.gI().requestIcon(small.id);
				SmallImage.vt_images_watingDowload.removeElementAt(0);
			}
		}
		else if (GameCanvas.isRequestMapID == 2 && GameCanvas.waitingTimeChangeMap < mSystem.currentTimeMillis() && GameCanvas.currentScreen != null)
		{
			if (GameCanvas.currentScreen == GameScr.gI())
			{
				if (global::Char.isLoadingMap)
				{
					global::Char.isLoadingMap = false;
				}
				if (ServerListScreen.waitToLogin)
				{
					ServerListScreen.waitToLogin = false;
				}
			}
			if (SmallImage.vt_images_watingDowload.size() > 0)
			{
				Small small2 = (Small)SmallImage.vt_images_watingDowload.elementAt(0);
				Service.gI().requestIcon(small2.id);
				SmallImage.vt_images_watingDowload.removeElementAt(0);
			}
			Effect.dowloadEff.size();
		}
		if (mSystem.currentTimeMillis() > this.timefps)
		{
			this.timefps += 1000L;
			GameCanvas.max = GameCanvas.fps;
			GameCanvas.fps = 0;
		}
		GameCanvas.fps++;
		if (GameCanvas.messageServer.size() > 0 && GameCanvas.thongBaoTest == null)
		{
			GameCanvas.startserverThongBao((string)GameCanvas.messageServer.elementAt(0));
			GameCanvas.messageServer.removeElementAt(0);
		}
		if (GameCanvas.gameTick % 5 == 0)
		{
			GameCanvas.timeNow = mSystem.currentTimeMillis();
		}
		Res.updateOnScreenDebug();
		try
		{
			if (global::TouchScreenKeyboard.visible)
			{
				GameCanvas.timeOpenKeyBoard++;
				if (GameCanvas.timeOpenKeyBoard > ((!Main.isWindowsPhone) ? 10 : 5))
				{
					mGraphics.addYWhenOpenKeyBoard = 94;
				}
			}
			else
			{
				mGraphics.addYWhenOpenKeyBoard = 0;
				GameCanvas.timeOpenKeyBoard = 0;
			}
			GameCanvas.debugUpdate.removeAllElements();
			long num = mSystem.currentTimeMillis();
			if (num - GameCanvas.timeTickEff1 >= 780L && !GameCanvas.isEff1)
			{
				GameCanvas.timeTickEff1 = num;
				GameCanvas.isEff1 = true;
			}
			else
			{
				GameCanvas.isEff1 = false;
			}
			if (num - GameCanvas.timeTickEff2 >= 7800L && !GameCanvas.isEff2)
			{
				GameCanvas.timeTickEff2 = num;
				GameCanvas.isEff2 = true;
			}
			else
			{
				GameCanvas.isEff2 = false;
			}
			if (GameCanvas.taskTick > 0)
			{
				GameCanvas.taskTick--;
			}
			GameCanvas.gameTick++;
			if (GameCanvas.gameTick > 10000)
			{
				if (mSystem.currentTimeMillis() - GameCanvas.lastTimePress > 20000L && GameCanvas.currentScreen == GameCanvas.loginScr)
				{
					GameMidlet.instance.exit();
				}
				GameCanvas.gameTick = 0;
			}
			if (GameCanvas.currentScreen != null)
			{
				if (GameCanvas.currentScreen == GameScr.gI() && !global::Char.isLoadingMap && Session_ME.gI().isConnected() && mSystem.currentTimeMillis() - GameCanvas.lastTimeKeepAlive > 15000L)
				{
					Service.gI().sendCheckController();
					Service.gI().sendCheckMap();
					GameCanvas.lastTimeKeepAlive = mSystem.currentTimeMillis();
				}
				if (ChatPopup.serverChatPopUp != null)
				{
					ChatPopup.serverChatPopUp.update();
					ChatPopup.serverChatPopUp.updateKey();
				}
				else if (ChatPopup.currChatPopup != null)
				{
					ChatPopup.currChatPopup.update();
					ChatPopup.currChatPopup.updateKey();
				}
				else if (GameCanvas.currentDialog != null)
				{
					GameCanvas.debug("B", 0);
					GameCanvas.currentDialog.update();
				}
				else if (GameCanvas.menu.showMenu)
				{
					GameCanvas.debug("C", 0);
					GameCanvas.menu.updateMenu();
					GameCanvas.debug("D", 0);
					GameCanvas.menu.updateMenuKey();
				}
				else if (GameCanvas.panel.isShow)
				{
					GameCanvas.panel.update();
					if (GameCanvas.isPointer(GameCanvas.panel.X, GameCanvas.panel.Y, GameCanvas.panel.W, GameCanvas.panel.H))
					{
						GameCanvas.isFocusPanel2 = false;
					}
					if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow)
					{
						GameCanvas.panel2.update();
						if (GameCanvas.isPointer(GameCanvas.panel2.X, GameCanvas.panel2.Y, GameCanvas.panel2.W, GameCanvas.panel2.H))
						{
							GameCanvas.isFocusPanel2 = true;
						}
					}
					if (GameCanvas.panel2 != null)
					{
						if (GameCanvas.isFocusPanel2)
						{
							GameCanvas.panel2.updateKey();
						}
						else
						{
							GameCanvas.panel.updateKey();
						}
					}
					else
					{
						GameCanvas.panel.updateKey();
					}
					if (GameCanvas.panel.chatTField != null && GameCanvas.panel.chatTField.isShow)
					{
						GameCanvas.panel.chatTFUpdateKey();
					}
					else if (GameCanvas.panel2 != null && GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow)
					{
						GameCanvas.panel2.chatTFUpdateKey();
					}
					else if ((GameCanvas.isPointer(GameCanvas.panel.X, GameCanvas.panel.Y, GameCanvas.panel.W, GameCanvas.panel.H) && GameCanvas.panel2 != null) || GameCanvas.panel2 == null)
					{
						GameCanvas.panel.updateKey();
					}
					else if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow && GameCanvas.isPointer(GameCanvas.panel2.X, GameCanvas.panel2.Y, GameCanvas.panel2.W, GameCanvas.panel2.H))
					{
						GameCanvas.panel2.updateKey();
					}
					if (GameCanvas.isPointer(GameCanvas.panel.X + GameCanvas.panel.W, GameCanvas.panel.Y, GameCanvas.w - GameCanvas.panel.W * 2, GameCanvas.panel.H) && GameCanvas.isPointerJustRelease && GameCanvas.panel.isDoneCombine)
					{
						GameCanvas.panel.hide();
					}
				}
				GameCanvas.debug("E", 0);
				if (!GameCanvas.isLoading)
				{
					GameCanvas.currentScreen.update();
				}
				GameCanvas.debug("F", 0);
				if (!GameCanvas.panel.isShow && ChatPopup.serverChatPopUp == null)
				{
					GameCanvas.currentScreen.updateKey();
				}
				Hint.update();
				SoundMn.gI().update();
			}
			GameCanvas.debug("Ix", 0);
			Timer.update();
			GameCanvas.debug("Hx", 0);
			InfoDlg.update();
			GameCanvas.debug("G", 0);
			if (this.resetToLoginScr)
			{
				this.resetToLoginScr = false;
				this.doResetToLoginScr(GameCanvas.loginScr);
			}
			GameCanvas.debug("Zzz", 0);
			if ((GameCanvas.currentScreen != GameCanvas.serverScr || !GameCanvas.serverScr.isPaintNewUi) && Controller.isConnectOK)
			{
				if (Controller.isMain)
				{
					ServerListScreen.testConnect = 2;
					Service.gI().setClientType();
					Service.gI().androidPack();
				}
				else
				{
					Service.gI().setClientType2();
					Service.gI().androidPack2();
				}
				Controller.isConnectOK = false;
			}
			if (Controller.isDisconnected)
			{
				if (!Controller.isMain)
				{
					if (GameCanvas.currentScreen == GameCanvas.serverScreen && !Service.reciveFromMainSession)
					{
						GameCanvas.serverScreen.cancel();
					}
					if (GameCanvas.currentScreen == GameCanvas.loginScr && !Service.reciveFromMainSession)
					{
						this.onDisconnected();
					}
				}
				else
				{
					this.onDisconnected();
				}
				Controller.isDisconnected = false;
			}
			if (Controller.isConnectionFail)
			{
				if (!Controller.isMain)
				{
					if (GameCanvas.currentScreen == GameCanvas.serverScreen && ServerListScreen.isGetData && !Service.reciveFromMainSession)
					{
						ServerListScreen.testConnect = 0;
						GameCanvas.serverScreen.cancel();
						Debug.Log("connect fail 1");
					}
					if (GameCanvas.currentScreen == GameCanvas.loginScr && !Service.reciveFromMainSession)
					{
						this.onConnectionFail();
						Debug.Log("connect fail 2");
					}
				}
				else
				{
					if (Session_ME.gI().isCompareIPConnect())
					{
						this.onConnectionFail();
					}
					Debug.Log("connect fail 3");
				}
				Controller.isConnectionFail = false;
			}
			if (Main.isResume)
			{
				Main.isResume = false;
				if (GameCanvas.currentDialog != null && GameCanvas.currentDialog.left != null && GameCanvas.currentDialog.left.actionListener != null)
				{
					GameCanvas.currentDialog.left.performAction();
				}
			}
			if (GameCanvas.currentScreen != null && GameCanvas.currentScreen is GameScr)
			{
				GameCanvas.xThongBaoTranslate += GameCanvas.dir_ * 2;
				if (GameCanvas.xThongBaoTranslate - Panel.imgNew.getWidth() <= 60)
				{
					GameCanvas.dir_ = 0;
					this.tickWaitThongBao++;
					if (this.tickWaitThongBao > 150)
					{
						this.tickWaitThongBao = 0;
						GameCanvas.thongBaoTest = null;
					}
				}
			}
			if (GameCanvas.currentScreen != null && GameCanvas.currentScreen.Equals(GameScr.gI()))
			{
				if (GameScr.info1 != null)
				{
					GameScr.info1.update();
				}
				if (GameScr.info2 != null)
				{
					GameScr.info2.update();
				}
			}
			GameCanvas.isPointerSelect = false;
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x0009D4F4 File Offset: 0x0009B6F4
	public void onDisconnected()
	{
		if (Mod.DungPham.KoiOctiiu957.MainMod.isAutoLogin)
		{
			Mod.DungPham.KoiOctiiu957.MainMod.isDisconnecting = true;
			Mod.DungPham.KoiOctiiu957.MainMod.timeDisconnect = mSystem.currentTimeMillis();
		}
		if (Controller.isConnectionFail)
		{
			Controller.isConnectionFail = false;
		}
		GameCanvas.isResume = true;
		Session_ME.gI().clearSendingMessage();
		Session_ME2.gI().clearSendingMessage();
		Session_ME.gI().close();
		Session_ME2.gI().close();
		if (Controller.isLoadingData)
		{
			GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
			Controller.isDisconnected = false;
			return;
		}
		Debug.LogError(">>>>onDisconnected");
		if (GameCanvas.currentScreen != GameCanvas.serverScreen)
		{
			GameCanvas.serverScreen.switchToMe();
			GameCanvas.startOK(mResources.maychutathoacmatsong + " [4]", 8884, null);
		}
		else
		{
			GameCanvas.endDlg();
		}
		global::Char.isLoadingMap = false;
		if (Controller.isMain)
		{
			ServerListScreen.testConnect = 0;
		}
		mSystem.endKey();
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x0009D5B4 File Offset: 0x0009B7B4
	public void onConnectionFail()
	{
		if (Mod.DungPham.KoiOctiiu957.MainMod.isAutoLogin)
		{
			Mod.DungPham.KoiOctiiu957.MainMod.isDisconnecting = true;
			Mod.DungPham.KoiOctiiu957.MainMod.timeDisconnect = mSystem.currentTimeMillis();
		}
		if (GameCanvas.currentScreen.Equals(SplashScr.instance))
		{
			GameCanvas.startOK(mResources.maychutathoacmatsong + " [1]", 8884, null);
			return;
		}
		Session_ME.gI().clearSendingMessage();
		Session_ME2.gI().clearSendingMessage();
		ServerListScreen.isWait = false;
		if (Controller.isLoadingData)
		{
			GameCanvas.startOK(mResources.maychutathoacmatsong + " [2]", 8884, null);
			Controller.isConnectionFail = false;
			return;
		}
		GameCanvas.isResume = true;
		LoginScr.isContinueToLogin = false;
		LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
		if (GameCanvas.currentScreen != GameCanvas.serverScreen)
		{
			ServerListScreen.countDieConnect = 0;
		}
		else
		{
			GameCanvas.endDlg();
			ServerListScreen.loadScreen = true;
			GameCanvas.serverScreen.switchToMe();
		}
		global::Char.isLoadingMap = false;
		if (Controller.isMain)
		{
			ServerListScreen.testConnect = 0;
		}
		mSystem.endKey();
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x00008BCF File Offset: 0x00006DCF
	public static bool isWaiting()
	{
		return InfoDlg.isShow || (GameCanvas.msgdlg != null && GameCanvas.msgdlg.info.Equals(mResources.PLEASEWAIT)) || global::Char.isLoadingMap || LoginScr.isContinueToLogin;
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x00008C03 File Offset: 0x00006E03
	public static void connect()
	{
		if (!Session_ME.gI().isConnected())
		{
			Session_ME.gI().connect(GameMidlet.IP, GameMidlet.PORT);
		}
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x0009D68C File Offset: 0x0009B88C
	public static void connect2()
	{
		if (!Session_ME2.gI().isConnected())
		{
			Res.outz(string.Concat(new object[]
			{
				"IP2= ",
				GameMidlet.IP2,
				" PORT 2= ",
				GameMidlet.PORT2
			}));
			Session_ME2.gI().connect(GameMidlet.IP2, GameMidlet.PORT2);
		}
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x00008C25 File Offset: 0x00006E25
	public static void resetTrans(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x0009D6F0 File Offset: 0x0009B8F0
	public static void resetTransGameScr(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.translate(0, 0);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.translate(-GameScr.cmx, -GameScr.cmy);
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x0009D740 File Offset: 0x0009B940
	public void initGameCanvas()
	{
		GameCanvas.debug("SP2i1", 0);
		GameCanvas.w = MotherCanvas.instance.getWidthz();
		GameCanvas.h = MotherCanvas.instance.getHeightz();
		GameCanvas.debug("SP2i2", 0);
		GameCanvas.hw = GameCanvas.w / 2;
		GameCanvas.hh = GameCanvas.h / 2;
		GameCanvas.wd3 = GameCanvas.w / 3;
		GameCanvas.hd3 = GameCanvas.h / 3;
		GameCanvas.w2d3 = 2 * GameCanvas.w / 3;
		GameCanvas.h2d3 = 2 * GameCanvas.h / 3;
		GameCanvas.w3d4 = 3 * GameCanvas.w / 4;
		GameCanvas.h3d4 = 3 * GameCanvas.h / 4;
		GameCanvas.wd6 = GameCanvas.w / 6;
		GameCanvas.hd6 = GameCanvas.h / 6;
		GameCanvas.debug("SP2i3", 0);
		mScreen.initPos();
		GameCanvas.debug("SP2i4", 0);
		GameCanvas.debug("SP2i5", 0);
		GameCanvas.inputDlg = new InputDlg();
		GameCanvas.debug("SP2i6", 0);
		GameCanvas.listPoint = new MyVector();
		GameCanvas.debug("SP2i7", 0);
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x000045ED File Offset: 0x000027ED
	public void start()
	{
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x00008C4D File Offset: 0x00006E4D
	public int getWidth()
	{
		return (int)ScaleGUI.WIDTH;
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x00008C55 File Offset: 0x00006E55
	public int getHeight()
	{
		return (int)ScaleGUI.HEIGHT;
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x000045ED File Offset: 0x000027ED
	public static void debug(string s, int type)
	{
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x0009D854 File Offset: 0x0009BA54
	public void doResetToLoginScr(mScreen screen)
	{
		try
		{
			SoundMn.gI().stopAll();
			LoginScr.isContinueToLogin = false;
			TileMap.lastType = (TileMap.bgType = 0);
			global::Char.clearMyChar();
			GameScr.clearGameScr();
			GameScr.resetAllvector();
			InfoDlg.hide();
			GameScr.info1.hide();
			GameScr.info2.hide();
			GameScr.info2.cmdChat = null;
			Hint.isShow = false;
			ChatPopup.currChatPopup = null;
			Controller.isStopReadMessage = false;
			GameScr.loadCamera(true, -1, -1);
			GameScr.cmx = 100;
			GameCanvas.panel.currentTabIndex = 0;
			GameCanvas.panel.selected = ((!GameCanvas.isTouch) ? 0 : -1);
			GameCanvas.panel.init();
			GameCanvas.panel2 = null;
			GameScr.isPaint = true;
			ClanMessage.vMessage.removeAllElements();
			GameScr.textTime.removeAllElements();
			GameScr.vClan.removeAllElements();
			GameScr.vFriend.removeAllElements();
			GameScr.vEnemies.removeAllElements();
			TileMap.vCurrItem.removeAllElements();
			BackgroudEffect.vBgEffect.removeAllElements();
			EffecMn.vEff.removeAllElements();
			Effect.newEff.removeAllElements();
			GameCanvas.menu.showMenu = false;
			GameCanvas.panel.vItemCombine.removeAllElements();
			GameCanvas.panel.isShow = false;
			if (GameCanvas.panel.tabIcon != null)
			{
				GameCanvas.panel.tabIcon.isShow = false;
			}
			if (mGraphics.zoomLevel == 1)
			{
				SmallImage.clearHastable();
			}
			Session_ME.gI().close();
			Session_ME2.gI().close();
		}
		catch (Exception ex)
		{
			Cout.println("Loi tai doResetToLoginScr " + ex.ToString());
		}
		ServerListScreen.isAutoConect = true;
		ServerListScreen.countDieConnect = 0;
		ServerListScreen.testConnect = -1;
		ServerListScreen.loadScreen = true;
		if (ServerListScreen.ipSelect == -1)
		{
			GameCanvas.serverScr.switchToMe();
			return;
		}
		if (GameCanvas.serverScreen == null)
		{
			GameCanvas.serverScreen = new ServerListScreen();
		}
		GameCanvas.serverScreen.switchToMe();
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x000045ED File Offset: 0x000027ED
	public static void showErrorForm(int type, string moreInfo)
	{
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x000045ED File Offset: 0x000027ED
	public static void paintCloud(mGraphics g)
	{
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x000045ED File Offset: 0x000027ED
	public static void updateBG()
	{
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x0009DA40 File Offset: 0x0009BC40
	public static void fillRect(mGraphics g, int color, int x, int y, int w, int h, int detalY)
	{
		g.setColor(color);
		int cmy = GameScr.cmy;
		if (cmy > GameCanvas.h)
		{
			cmy = GameCanvas.h;
		}
		g.fillRect(x, y - ((detalY == 0) ? 0 : (cmy >> detalY)), w, h + ((detalY == 0) ? 0 : (cmy >> detalY)));
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x0009DA94 File Offset: 0x0009BC94
	public static void paintBackgroundtLayer(mGraphics g, int layer, int deltaY, int color1, int color2)
	{
		try
		{
			int num = layer - 1;
			if (num == GameCanvas.imgBG.Length - 1 && (GameScr.gI().isRongThanXuatHien || GameScr.gI().isFireWorks))
			{
				g.setColor(GameScr.gI().mautroi);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				if (GameCanvas.typeBg == 2 || GameCanvas.typeBg == 4 || GameCanvas.typeBg == 7)
				{
					GameCanvas.drawSun1(g);
					GameCanvas.drawSun2(g);
				}
				if (GameScr.gI().isFireWorks && !GameCanvas.lowGraphic)
				{
					FireWorkEff.paint(g);
				}
			}
			else if (GameCanvas.imgBG != null && GameCanvas.imgBG[num] != null)
			{
				if (GameCanvas.moveX[num] != 0)
				{
					GameCanvas.moveX[num] += GameCanvas.moveXSpeed[num];
				}
				int cmy = GameScr.cmy;
				if (cmy > GameCanvas.h)
				{
					cmy = GameCanvas.h;
				}
				if (GameCanvas.layerSpeed[num] != 0)
				{
					for (int i = -((GameScr.cmx + GameCanvas.moveX[num] >> GameCanvas.layerSpeed[num]) % GameCanvas.bgW[num]); i < GameScr.gW; i += GameCanvas.bgW[num])
					{
						g.drawImage(GameCanvas.imgBG[num], i, GameCanvas.yb[num] - ((deltaY <= 0) ? 0 : (cmy >> deltaY)), 0);
					}
				}
				else
				{
					for (int j = 0; j < GameScr.gW; j += GameCanvas.bgW[num])
					{
						g.drawImage(GameCanvas.imgBG[num], j, GameCanvas.yb[num] - ((deltaY <= 0) ? 0 : (cmy >> deltaY)), 0);
					}
				}
				if (color1 != -1)
				{
					if (num == GameCanvas.nBg - 1)
					{
						GameCanvas.fillRect(g, color1, 0, -(cmy >> deltaY), GameScr.gW, GameCanvas.yb[num], deltaY);
					}
					else
					{
						GameCanvas.fillRect(g, color1, 0, GameCanvas.yb[num - 1] + GameCanvas.bgH[num - 1], GameScr.gW, GameCanvas.yb[num] - (GameCanvas.yb[num - 1] + GameCanvas.bgH[num - 1]), deltaY);
					}
				}
				if (color2 != -1)
				{
					if (num == 0)
					{
						GameCanvas.fillRect(g, color2, 0, GameCanvas.yb[num] + GameCanvas.bgH[num], GameScr.gW, GameScr.gH - (GameCanvas.yb[num] + GameCanvas.bgH[num]), deltaY);
					}
					else
					{
						GameCanvas.fillRect(g, color2, 0, GameCanvas.yb[num] + GameCanvas.bgH[num], GameScr.gW, GameCanvas.yb[num - 1] - (GameCanvas.yb[num] + GameCanvas.bgH[num]) + 80, deltaY);
					}
				}
				if (GameCanvas.currentScreen == GameScr.instance)
				{
					if (layer == 1 && GameCanvas.typeBg == 11)
					{
						g.drawImage(GameCanvas.imgSun2, -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 400, GameCanvas.yb[0] + 30 - (cmy >> 2), StaticObj.BOTTOM_HCENTER);
					}
					if (layer == 1 && GameCanvas.typeBg == 13)
					{
						g.drawImage(GameCanvas.imgBG[1], -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + TileMap.tmw * 24 / 4, GameCanvas.yb[0] - (cmy >> 3) + 30, 0);
						g.drawRegion(GameCanvas.imgBG[1], 0, 0, GameCanvas.bgW[1], GameCanvas.bgH[1], 2, -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + TileMap.tmw * 24 / 4 + GameCanvas.bgW[1], GameCanvas.yb[0] - (cmy >> 3) + 30, 0);
					}
					if (layer == 3 && TileMap.mapID == 1)
					{
						for (int k = 0; k < TileMap.pxh / mGraphics.getImageHeight(GameCanvas.imgCaycot); k++)
						{
							g.drawImage(GameCanvas.imgCaycot, -(GameScr.cmx >> GameCanvas.layerSpeed[2]) + 300, k * mGraphics.getImageHeight(GameCanvas.imgCaycot) - (cmy >> 3), 0);
						}
					}
				}
				int x = -(GameScr.cmx + GameCanvas.moveX[num] >> GameCanvas.layerSpeed[num]);
				EffecMn.paintBackGroundUnderLayer(g, x, GameCanvas.yb[num] + GameCanvas.bgH[num] - (cmy >> deltaY), num);
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham paint bground: " + ex.ToString());
		}
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x0009DEC0 File Offset: 0x0009C0C0
	public static void drawSun1(mGraphics g)
	{
		if (GameCanvas.imgSun != null)
		{
			g.drawImage(GameCanvas.imgSun, GameCanvas.sunX, GameCanvas.sunY, 0);
		}
		if (GameCanvas.isBoltEff)
		{
			if (GameCanvas.gameTick % 200 == 0)
			{
				GameCanvas.boltActive = true;
			}
			if (GameCanvas.boltActive)
			{
				GameCanvas.tBolt++;
				if (GameCanvas.tBolt == 10)
				{
					GameCanvas.tBolt = 0;
					GameCanvas.boltActive = false;
				}
				if (GameCanvas.tBolt % 2 == 0)
				{
					g.setColor(16777215);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				}
			}
		}
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x00008C5D File Offset: 0x00006E5D
	public static void drawSun2(mGraphics g)
	{
		if (GameCanvas.imgSun2 != null)
		{
			g.drawImage(GameCanvas.imgSun2, GameCanvas.sunX2, GameCanvas.sunY2, 0);
		}
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x00008C7C File Offset: 0x00006E7C
	public static bool isHDVersion()
	{
		return mGraphics.zoomLevel > 1;
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x0009DF54 File Offset: 0x0009C154
	public static void paint_ios_bg(mGraphics g)
	{
		if (mSystem.clientType != 5)
		{
			return;
		}
		if (GameCanvas.imgBgIOS != null)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			for (int i = 0; i < 3; i++)
			{
				g.drawImage(GameCanvas.imgBgIOS, GameCanvas.imgBgIOS.getWidth() * i, GameCanvas.h / 2, mGraphics.VCENTER | mGraphics.HCENTER);
			}
			return;
		}
		int num = (TileMap.bgID % 2 != 0) ? 1 : 2;
		GameCanvas.imgBgIOS = mSystem.loadImage("/bg/bg_ios_" + num + ".png");
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x0009DFF0 File Offset: 0x0009C1F0
	public static void paintBGGameScr(mGraphics g)
	{
		if (!GameCanvas.isLoadBGok)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		}
		if (global::Char.isLoadingMap)
		{
			return;
		}
		
		if (Mod.DungPham.KoiOctiiu957.MainMod.isPaintBackground)
		{
			// --- TEA MOBI ORIGINAL CODE ---
			int gW = GameScr.gW;
			int gH = GameScr.gH;
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			try
			{
				if (GameCanvas.currentScreen == GameScr.gI())
				{
					if (TileMap.mapID != 172)
					{
						if (TileMap.mapID == 137 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 120 || TileMap.isMapDouble)
						{
							g.setColor(0);
							g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
							return;
						}
					}
					if (TileMap.mapID == 138)
					{
						g.setColor(6776679);
						g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
						return;
					}
				}
				if (GameCanvas.typeBg == 0)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
					GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 1)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 6, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 3, 3, -1, -1);
					GameCanvas.fillRect(g, GameCanvas.colorTop[2], 0, -(GameScr.cmy >> 5), gW, GameCanvas.yb[2], 5);
					GameCanvas.fillRect(g, GameCanvas.colorBotton[2], 0, GameCanvas.yb[2] + GameCanvas.bgH[2] - (GameScr.cmy >> 3), gW, 70, 3);
					GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 2)
				{
					GameCanvas.paintBackgroundtLayer(g, 5, 10, GameCanvas.colorTop[4], GameCanvas.colorBotton[4]);
					GameCanvas.paintBackgroundtLayer(g, 4, 8, -1, GameCanvas.colorTop[2]);
					GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
					GameCanvas.paintCloud(g);
				}
				else if (GameCanvas.typeBg == 3)
				{
					int num = GameScr.cmy - (325 - GameScr.gH23);
					g.translate(0, -num);
					GameCanvas.fillRect(g, (!GameScr.gI().isRongThanXuatHien && !GameScr.gI().isFireWorks) ? GameCanvas.colorTop[2] : GameScr.gI().mautroi, 0, num - (GameScr.cmy >> 3), gW, GameCanvas.yb[2] - num + (GameScr.cmy >> 3) + 100, 2);
					GameCanvas.paintBackgroundtLayer(g, 3, 2, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 0, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 1, 0, -1, GameCanvas.colorBotton[0]);
					g.translate(0, -g.getTranslateY());
				}
				else if (GameCanvas.typeBg == 4)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 7, GameCanvas.colorTop[3], -1);
					GameCanvas.paintBackgroundtLayer(g, 3, 3, -1, (!GameCanvas.isHDVersion()) ? GameCanvas.colorTop[1] : GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 2, GameCanvas.colorTop[1], GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 5)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 15, GameCanvas.colorTop[3], -1);
					GameCanvas.drawSun1(g);
					g.translate(100, 10);
					GameCanvas.drawSun1(g);
					g.translate(-100, -10);
					GameCanvas.drawSun2(g);
					GameCanvas.paintBackgroundtLayer(g, 3, 10, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 2, 6, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 1, 4, -1, -1);
					g.translate(0, 27);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, -1);
					g.translate(0, 20);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
					g.translate(-g.getTranslateX(), -g.getTranslateY());
				}
				else if (GameCanvas.typeBg == 6)
				{
					GameCanvas.paintBackgroundtLayer(g, 5, 10, GameCanvas.colorTop[4], GameCanvas.colorBotton[4]);
					GameCanvas.drawSun1(g);
					GameCanvas.drawSun2(g);
					g.translate(60, 40);
					GameCanvas.drawSun2(g);
					g.translate(-60, -40);
					GameCanvas.paintBackgroundtLayer(g, 4, 7, -1, GameCanvas.colorBotton[3]);
					BackgroudEffect.paintFarAll(g);
					GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 7)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
					GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 2, 4, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 1, 3, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 8)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 8, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
					GameCanvas.drawSun1(g);
					GameCanvas.drawSun2(g);
					GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
					if (((TileMap.mapID < 92 || TileMap.mapID > 96) && TileMap.mapID != 51 && TileMap.mapID != 52) || GameCanvas.currentScreen == GameCanvas.loginScr)
					{
						GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
					}
				}
				else if (GameCanvas.typeBg == 9)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 8, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
					GameCanvas.drawSun1(g);
					GameCanvas.drawSun2(g);
					g.translate(-80, 20);
					GameCanvas.drawSun2(g);
					g.translate(80, -20);
					BackgroudEffect.paintFarAll(g);
					GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 10)
				{
					int num2 = GameScr.cmy - (380 - GameScr.gH23);
					g.translate(0, -num2);
					GameCanvas.fillRect(g, (!GameScr.gI().isRongThanXuatHien) ? GameCanvas.colorTop[1] : GameScr.gI().mautroi, 0, num2 - (GameScr.cmy >> 2), gW, GameCanvas.yb[1] - num2 + (GameScr.cmy >> 2) + 100, 2);
					GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
					GameCanvas.drawSun1(g);
					GameCanvas.drawSun2(g);
					GameCanvas.paintBackgroundtLayer(g, 1, 0, -1, -1);
					g.translate(0, -g.getTranslateY());
				}
				else if (GameCanvas.typeBg == 11)
				{
					GameCanvas.paintBackgroundtLayer(g, 3, 6, GameCanvas.colorTop[2], GameCanvas.colorBotton[2]);
					GameCanvas.drawSun1(g);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 12)
				{
					g.setColor(9161471);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, 14417919);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, 14417919);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, 14417919);
					GameCanvas.paintCloud(g);
				}
				else if (GameCanvas.typeBg == 13)
				{
					g.setColor(15268088);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					GameCanvas.paintBackgroundtLayer(g, 1, 5, -1, 15268088);
				}
				else if (GameCanvas.typeBg == 15)
				{
					g.setColor(2631752);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 16)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
					for (int i = 0; i < GameCanvas.imgSunSpec.Length; i++)
					{
						g.drawImage(GameCanvas.imgSunSpec[i], GameCanvas.cloudX[i], GameCanvas.cloudY[i], 33);
					}
					GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 19)
				{
					GameCanvas.paintBackgroundtLayer(g, 5, 10, GameCanvas.colorTop[4], GameCanvas.colorBotton[4]);
					GameCanvas.paintBackgroundtLayer(g, 4, 8, -1, GameCanvas.colorTop[2]);
					GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
					GameCanvas.paintCloud(g);
				}
				else
				{
					GameCanvas.fillRect(g, GameCanvas.colorBotton[3], 0, GameCanvas.yb[3] + GameCanvas.bgH[3], GameScr.gW, GameCanvas.yb[2] + GameCanvas.bgH[2], 6);
					GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
					GameCanvas.drawSun1(g);
					GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
			}
			catch (Exception ex)
			{
				g.setColor(0);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			}
		}
		else
		{
			// --- DUNG PHAM CODE ---
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setColor(999999999);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			if (MainMod.listBackgroundImages.Count <= 0 || MainMod.isReduceGraphics)
			{
				return;
			}
			if (string.IsNullOrEmpty(global::Char.myCharz().cName))
			{
				g.drawImage(MainMod.listBackgroundImages[0], 0, 0);
				return;
			}
			if (MainMod.isMeInNRDMap() || !MainMod.isPaintBackground)
			{
				return;
			}
			if (MainMod.listBackgroundImages.Count <= 2)
			{
				g.drawImage(MainMod.listBackgroundImages[0], 0, 0);
				return;
			}
			if (MainMod.indexBackgroundImages < 2)
			{
				MainMod.indexBackgroundImages = 2;
				MainMod.lastTimeChangeBackground = mSystem.currentTimeMillis();
			}
			g.drawImage(MainMod.listBackgroundImages[MainMod.indexBackgroundImages], 0, 0);
			if (mSystem.currentTimeMillis() - MainMod.lastTimeChangeBackground > 60000L)
			{
				MainMod.lastTimeChangeBackground = mSystem.currentTimeMillis();
				MainMod.indexBackgroundImages++;
				if (MainMod.indexBackgroundImages >= MainMod.listBackgroundImages.Count)
				{
					MainMod.indexBackgroundImages = 2;
				}
			}
		}
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x000045ED File Offset: 0x000027ED
	public static void resetBg()
	{
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x0009E130 File Offset: 0x0009C330
	public static void getYBackground(int typeBg)
	{
		try
		{
			int gH = GameScr.gH23;
			switch (typeBg)
			{
			case 0:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 70;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 20;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 30;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 50;
				goto IL_67A;
			case 1:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 120;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 40;
				GameCanvas.yb[2] = GameCanvas.yb[1] - 90;
				GameCanvas.yb[3] = GameCanvas.yb[2] - 25;
				goto IL_67A;
			case 2:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 150;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 60;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] - 10;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4];
				goto IL_67A;
			case 3:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 10;
				GameCanvas.yb[1] = GameCanvas.yb[0] + 80;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 10;
				goto IL_67A;
			case 4:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 130;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1];
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 20;
				GameCanvas.yb[3] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 80;
				goto IL_67A;
			case 5:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 40;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 10;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 15;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 50;
				goto IL_67A;
			case 6:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 100;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 30;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 10;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 15;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4] + 15;
				goto IL_67A;
			case 7:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 20;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 15;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 20;
				GameCanvas.yb[3] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 10;
				goto IL_67A;
			case 8:
				GameCanvas.yb[0] = gH - 103 + 150;
				if (TileMap.mapID == 103)
				{
					GameCanvas.yb[0] -= 100;
				}
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 10;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 10;
				goto IL_67A;
			case 9:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 100;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 22;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3];
				goto IL_67A;
			case 10:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] - 45;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 10;
				goto IL_67A;
			case 11:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 60;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 5;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 15;
				goto IL_67A;
			case 12:
				GameCanvas.yb[0] = gH + 40;
				GameCanvas.yb[1] = GameCanvas.yb[0] - 40;
				GameCanvas.yb[2] = GameCanvas.yb[1] - 40;
				goto IL_67A;
			case 13:
				GameCanvas.yb[0] = gH - 80;
				GameCanvas.yb[1] = GameCanvas.yb[0];
				goto IL_67A;
			case 15:
				GameCanvas.yb[0] = gH - 20;
				GameCanvas.yb[1] = GameCanvas.yb[0] - 80;
				goto IL_67A;
			case 16:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 75;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 50;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 90;
				goto IL_67A;
			case 19:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 150;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 60;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] - 10;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4];
				goto IL_67A;
			}
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 75;
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 50;
			GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
			GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 90;
			IL_67A:;
		}
		catch (Exception)
		{
			int gH2 = GameScr.gH23;
			for (int i = 0; i < GameCanvas.yb.Length; i++)
			{
				GameCanvas.yb[i] = 1;
			}
		}
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x0009E7F8 File Offset: 0x0009C9F8
	public static void loadBG(int typeBG)
	{
		try
		{
			GameCanvas.isLoadBGok = true;
			if (GameCanvas.typeBg == 12)
			{
				BackgroudEffect.yfog = TileMap.pxh - 100;
			}
			else
			{
				BackgroudEffect.yfog = TileMap.pxh - 160;
			}
			BackgroudEffect.clearImage();
			GameCanvas.randomRaintEff(typeBG);
			if ((TileMap.lastBgID != typeBG || TileMap.lastType != TileMap.bgType) && typeBG != -1)
			{
				GameCanvas.transY = 12;
				TileMap.lastBgID = (int)((sbyte)typeBG);
				TileMap.lastType = (int)((sbyte)TileMap.bgType);
				GameCanvas.layerSpeed = new int[]
				{
					1,
					2,
					3,
					7,
					8
				};
				GameCanvas.moveX = new int[5];
				GameCanvas.moveXSpeed = new int[5];
				GameCanvas.typeBg = typeBG;
				GameCanvas.isBoltEff = false;
				GameScr.firstY = GameScr.cmy;
				GameCanvas.imgBG = null;
				GameCanvas.imgCloud = null;
				GameCanvas.imgSun = null;
				GameCanvas.imgCaycot = null;
				GameScr.firstY = -1;
				switch (GameCanvas.typeBg)
				{
				case 0:
					GameCanvas.imgCaycot = GameCanvas.loadImageRMS("/bg/caycot.png");
					GameCanvas.layerSpeed = new int[]
					{
						1,
						3,
						5,
						7
					};
					GameCanvas.nBg = 4;
					if (TileMap.bgType == 2)
					{
						GameCanvas.transY = 8;
						goto IL_31F;
					}
					goto IL_31F;
				case 1:
					GameCanvas.transY = 7;
					GameCanvas.nBg = 4;
					goto IL_31F;
				case 2:
				{
					int[] array = new int[5];
					array[2] = 1;
					GameCanvas.moveX = array;
					int[] array2 = new int[5];
					array2[2] = 2;
					GameCanvas.moveXSpeed = array2;
					GameCanvas.nBg = 5;
					goto IL_31F;
				}
				case 3:
					GameCanvas.nBg = 3;
					goto IL_31F;
				case 4:
				{
					BackgroudEffect.addEffect(3);
					int[] array3 = new int[5];
					array3[1] = 1;
					GameCanvas.moveX = array3;
					int[] array4 = new int[5];
					array4[1] = 1;
					GameCanvas.moveXSpeed = array4;
					GameCanvas.nBg = 4;
					goto IL_31F;
				}
				case 5:
					GameCanvas.nBg = 4;
					goto IL_31F;
				case 6:
				{
					int[] array5 = new int[5];
					array5[0] = 1;
					GameCanvas.moveX = array5;
					int[] array6 = new int[5];
					array6[0] = 2;
					GameCanvas.moveXSpeed = array6;
					GameCanvas.nBg = 5;
					goto IL_31F;
				}
				case 7:
					GameCanvas.nBg = 4;
					goto IL_31F;
				case 8:
					GameCanvas.transY = 8;
					GameCanvas.nBg = 4;
					goto IL_31F;
				case 9:
					BackgroudEffect.addEffect(9);
					GameCanvas.nBg = 4;
					goto IL_31F;
				case 10:
					GameCanvas.nBg = 2;
					goto IL_31F;
				case 11:
					GameCanvas.transY = 7;
					GameCanvas.layerSpeed[2] = 0;
					GameCanvas.nBg = 3;
					goto IL_31F;
				case 12:
				{
					int[] array7 = new int[5];
					array7[0] = 1;
					array7[1] = 1;
					GameCanvas.moveX = array7;
					int[] array8 = new int[5];
					array8[0] = 2;
					array8[1] = 1;
					GameCanvas.moveXSpeed = array8;
					GameCanvas.nBg = 3;
					goto IL_31F;
				}
				case 13:
					GameCanvas.nBg = 2;
					goto IL_31F;
				case 15:
					Res.outz("HELL");
					GameCanvas.nBg = 2;
					goto IL_31F;
				case 16:
					GameCanvas.layerSpeed = new int[]
					{
						1,
						3,
						5,
						7
					};
					GameCanvas.nBg = 4;
					goto IL_31F;
				case 19:
				{
					int[] array9 = new int[5];
					array9[1] = 2;
					array9[2] = 1;
					GameCanvas.moveX = array9;
					int[] array10 = new int[5];
					array10[1] = 2;
					array10[2] = 1;
					GameCanvas.moveXSpeed = array10;
					GameCanvas.nBg = 5;
					goto IL_31F;
				}
				}
				GameCanvas.layerSpeed = new int[]
				{
					1,
					3,
					5,
					7
				};
				GameCanvas.nBg = 4;
				IL_31F:
				if (typeBG <= 16)
				{
					GameCanvas.skyColor = StaticObj.SKYCOLOR[GameCanvas.typeBg];
				}
				else
				{
					try
					{
						string path = string.Concat(new object[]
						{
							"/bg/b",
							GameCanvas.typeBg,
							3,
							".png"
						});
						if (TileMap.bgType != 0)
						{
							path = string.Concat(new object[]
							{
								"/bg/b",
								GameCanvas.typeBg,
								3,
								"-",
								TileMap.bgType,
								".png"
							});
						}
						int[] array11 = new int[1];
						Image image = GameCanvas.loadImageRMS(path);
						image.getRGB(ref array11, 0, 1, mGraphics.getRealImageWidth(image) / 2, 0, 1, 1);
						GameCanvas.skyColor = array11[0];
					}
					catch (Exception)
					{
						GameCanvas.skyColor = StaticObj.SKYCOLOR[StaticObj.SKYCOLOR.Length - 1];
					}
				}
				GameCanvas.colorTop = new int[StaticObj.SKYCOLOR.Length];
				GameCanvas.colorBotton = new int[StaticObj.SKYCOLOR.Length];
				for (int i = 0; i < StaticObj.SKYCOLOR.Length; i++)
				{
					GameCanvas.colorTop[i] = StaticObj.SKYCOLOR[i];
					GameCanvas.colorBotton[i] = StaticObj.SKYCOLOR[i];
				}
				if (GameCanvas.lowGraphic)
				{
					GameCanvas.tam = GameCanvas.loadImageRMS("/bg/b63.png");
				}
				else
				{
					GameCanvas.imgBG = new Image[GameCanvas.nBg];
					GameCanvas.bgW = new int[GameCanvas.nBg];
					GameCanvas.bgH = new int[GameCanvas.nBg];
					GameCanvas.colorBotton = new int[GameCanvas.nBg];
					GameCanvas.colorTop = new int[GameCanvas.nBg];
					if (TileMap.bgType == 100)
					{
						GameCanvas.imgBG[0] = GameCanvas.loadImageRMS("/bg/b100.png");
						GameCanvas.imgBG[1] = GameCanvas.loadImageRMS("/bg/b100.png");
						GameCanvas.imgBG[2] = GameCanvas.loadImageRMS("/bg/b82-1.png");
						GameCanvas.imgBG[3] = GameCanvas.loadImageRMS("/bg/b93.png");
						for (int j = 0; j < GameCanvas.nBg; j++)
						{
							if (GameCanvas.imgBG[j] != null)
							{
								int[] array12 = new int[1];
								GameCanvas.imgBG[j].getRGB(ref array12, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[j]) / 2, 0, 1, 1);
								GameCanvas.colorTop[j] = array12[0];
								array12 = new int[1];
								GameCanvas.imgBG[j].getRGB(ref array12, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[j]) / 2, mGraphics.getRealImageHeight(GameCanvas.imgBG[j]) - 1, 1, 1);
								GameCanvas.colorBotton[j] = array12[0];
								GameCanvas.bgW[j] = mGraphics.getImageWidth(GameCanvas.imgBG[j]);
								GameCanvas.bgH[j] = mGraphics.getImageHeight(GameCanvas.imgBG[j]);
							}
							else if (GameCanvas.nBg > 1)
							{
								GameCanvas.imgBG[j] = GameCanvas.loadImageRMS("/bg/b" + GameCanvas.typeBg + "0.png");
								GameCanvas.bgW[j] = mGraphics.getImageWidth(GameCanvas.imgBG[j]);
								GameCanvas.bgH[j] = mGraphics.getImageHeight(GameCanvas.imgBG[j]);
							}
						}
					}
					else
					{
						for (int k = 0; k < GameCanvas.nBg; k++)
						{
							string path2 = string.Concat(new object[]
							{
								"/bg/b",
								GameCanvas.typeBg,
								k,
								".png"
							});
							if (TileMap.bgType != 0)
							{
								path2 = string.Concat(new object[]
								{
									"/bg/b",
									GameCanvas.typeBg,
									k,
									"-",
									TileMap.bgType,
									".png"
								});
							}
							GameCanvas.imgBG[k] = GameCanvas.loadImageRMS(path2);
							if (GameCanvas.imgBG[k] != null)
							{
								int[] array13 = new int[1];
								GameCanvas.imgBG[k].getRGB(ref array13, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[k]) / 2, 0, 1, 1);
								GameCanvas.colorTop[k] = array13[0];
								array13 = new int[1];
								GameCanvas.imgBG[k].getRGB(ref array13, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[k]) / 2, mGraphics.getRealImageHeight(GameCanvas.imgBG[k]) - 1, 1, 1);
								GameCanvas.colorBotton[k] = array13[0];
								GameCanvas.bgW[k] = mGraphics.getImageWidth(GameCanvas.imgBG[k]);
								GameCanvas.bgH[k] = mGraphics.getImageHeight(GameCanvas.imgBG[k]);
							}
							else if (GameCanvas.nBg > 1)
							{
								GameCanvas.imgBG[k] = GameCanvas.loadImageRMS("/bg/b" + GameCanvas.typeBg + "0.png");
								GameCanvas.bgW[k] = mGraphics.getImageWidth(GameCanvas.imgBG[k]);
								GameCanvas.bgH[k] = mGraphics.getImageHeight(GameCanvas.imgBG[k]);
							}
						}
					}
					GameCanvas.getYBackground(GameCanvas.typeBg);
					GameCanvas.cloudX = new int[]
					{
						GameScr.gW / 2 - 40,
						GameScr.gW / 2 + 40,
						GameScr.gW / 2 - 100,
						GameScr.gW / 2 - 80,
						GameScr.gW / 2 - 120
					};
					GameCanvas.cloudY = new int[]
					{
						130,
						100,
						150,
						140,
						80
					};
					GameCanvas.imgSunSpec = null;
					if (GameCanvas.typeBg != 0)
					{
						if (GameCanvas.typeBg == 2)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun0.png");
							GameCanvas.sunX = GameScr.gW / 2 + 50;
							GameCanvas.sunY = GameCanvas.yb[4] - 40;
							TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/wts");
						}
						else if (GameCanvas.typeBg == 19)
						{
							TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/water_flow_32");
						}
						else if (GameCanvas.typeBg == 4)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun2.png");
							GameCanvas.sunX = GameScr.gW / 2 + 30;
							GameCanvas.sunY = GameCanvas.yb[3];
						}
						else if (GameCanvas.typeBg == 7)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun3" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun4" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
							GameCanvas.sunY = GameCanvas.yb[3] - 80;
							GameCanvas.sunX2 = GameCanvas.sunX - 100;
							GameCanvas.sunY2 = GameCanvas.yb[3] - 30;
						}
						else if (GameCanvas.typeBg == 6)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun5" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun6" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
							GameCanvas.sunY = GameCanvas.yb[4];
							GameCanvas.sunX2 = GameCanvas.sunX - 100;
							GameCanvas.sunY2 = GameCanvas.yb[4] + 20;
						}
						else if (typeBG == 5)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun8" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun7" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW / 2 - 50;
							GameCanvas.sunY = GameCanvas.yb[3] + 20;
							GameCanvas.sunX2 = GameScr.gW / 2 + 20;
							GameCanvas.sunY2 = GameCanvas.yb[3] - 30;
						}
						else if (GameCanvas.typeBg == 8 && TileMap.mapID < 90)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun9" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun10" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW / 2 - 30;
							GameCanvas.sunY = GameCanvas.yb[3] + 60;
							GameCanvas.sunX2 = GameScr.gW / 2 + 20;
							GameCanvas.sunY2 = GameCanvas.yb[3] + 10;
						}
						else if (typeBG == 9)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun11" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun12" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
							GameCanvas.sunY = GameCanvas.yb[4] + 20;
							GameCanvas.sunX2 = GameCanvas.sunX - 80;
							GameCanvas.sunY2 = GameCanvas.yb[4] + 40;
						}
						else if (typeBG == 10)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun13" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun14" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
							GameCanvas.sunY = GameCanvas.yb[1] - 30;
							GameCanvas.sunX2 = GameCanvas.sunX - 80;
							GameCanvas.sunY2 = GameCanvas.yb[1];
						}
						else if (typeBG == 11)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun15" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/b113" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW / 2 - 30;
							GameCanvas.sunY = GameCanvas.yb[2] - 30;
						}
						else if (typeBG == 12)
						{
							GameCanvas.cloudY = new int[]
							{
								200,
								170,
								220,
								150,
								250
							};
						}
						else if (typeBG == 16)
						{
							GameCanvas.cloudX = new int[]
							{
								90,
								170,
								250,
								320,
								400,
								450,
								500
							};
							GameCanvas.cloudY = new int[]
							{
								GameCanvas.yb[2] + 5,
								GameCanvas.yb[2] - 20,
								GameCanvas.yb[2] - 50,
								GameCanvas.yb[2] - 30,
								GameCanvas.yb[2] - 50,
								GameCanvas.yb[2],
								GameCanvas.yb[2] - 40
							};
							GameCanvas.imgSunSpec = new Image[7];
							for (int l = 0; l < GameCanvas.imgSunSpec.Length; l++)
							{
								int num = 161;
								if (l == 0 || l == 2 || l == 3 || l == 2 || l == 6)
								{
									num = 160;
								}
								GameCanvas.imgSunSpec[l] = GameCanvas.loadImageRMS("/bg/sun" + num + ".png");
							}
						}
						else if (typeBG == 19)
						{
							int[] array14 = new int[5];
							array14[1] = 2;
							array14[2] = 1;
							GameCanvas.moveX = array14;
							int[] array15 = new int[5];
							array15[1] = 2;
							array15[2] = 1;
							GameCanvas.moveXSpeed = array15;
							GameCanvas.nBg = 5;
						}
						else
						{
							GameCanvas.imgCloud = null;
							GameCanvas.imgSun = null;
							GameCanvas.imgSun2 = null;
							GameCanvas.imgSun = GameCanvas.loadImageRMS(string.Concat(new object[]
							{
								"/bg/sun",
								typeBG,
								(TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty,
								".png"
							}));
							if (GameCanvas.loadImageRMS("/tWater/water_flow_" + typeBG) != null)
							{
								TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/water_flow_" + typeBG);
							}
							GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
							GameCanvas.sunY = GameCanvas.yb[2] - 30;
						}
					}
					// Removed buggy GameCanvas.paintBG forcing
				}
			}
		}
		catch (Exception)
		{
			GameCanvas.isLoadBGok = false;
		}
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x0009F88C File Offset: 0x0009DA8C
	private static void randomRaintEff(int typeBG)
	{
		for (int i = 0; i < GameCanvas.bgRain.Length; i++)
		{
			if (typeBG == GameCanvas.bgRain[i] && Res.random(0, 2) == 0)
			{
				BackgroudEffect.addEffect(0);
				return;
			}
		}
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x0009F8C8 File Offset: 0x0009DAC8
	public void keyPressedz(int keyCode)
	{
		GameCanvas.lastTimePress = mSystem.currentTimeMillis();
		if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 122) || keyCode == 10 || keyCode == 8 || keyCode == 13 || keyCode == 32 || keyCode == 31)
		{
			GameCanvas.keyAsciiPress = keyCode;
		}
		this.mapKeyPress(keyCode);
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x0009F918 File Offset: 0x0009DB18
	public void mapKeyPress(int keyCode)
	{
		if (GameCanvas.currentDialog != null)
		{
			GameCanvas.currentDialog.keyPress(keyCode);
			GameCanvas.keyAsciiPress = 0;
			return;
		}
		GameCanvas.currentScreen.keyPress(keyCode);
		switch (keyCode)
		{
		case 48:
			GameCanvas.keyHold[0] = true;
			GameCanvas.keyPressed[0] = true;
			return;
		case 49:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[1] = true;
				GameCanvas.keyPressed[1] = true;
			}
			return;
		case 50:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[2] = true;
				GameCanvas.keyPressed[2] = true;
			}
			return;
		case 51:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[3] = true;
				GameCanvas.keyPressed[3] = true;
			}
			return;
		case 52:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[4] = true;
				GameCanvas.keyPressed[4] = true;
			}
			return;
		case 53:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[5] = true;
				GameCanvas.keyPressed[5] = true;
			}
			return;
		case 54:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[6] = true;
				GameCanvas.keyPressed[6] = true;
			}
			return;
		case 55:
			GameCanvas.keyHold[7] = true;
			GameCanvas.keyPressed[7] = true;
			return;
		case 56:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[8] = true;
				GameCanvas.keyPressed[8] = true;
			}
			return;
		case 57:
			GameCanvas.keyHold[9] = true;
			GameCanvas.keyPressed[9] = true;
			return;
		default:
			switch (keyCode + 8)
			{
			case 0:
				GameCanvas.keyHold[14] = true;
				GameCanvas.keyPressed[14] = true;
				return;
			case 1:
				goto IL_46A;
			case 2:
				goto IL_457;
			case 3:
				goto IL_403;
			case 4:
				if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack)
				{
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					return;
				}
				GameCanvas.keyHold[24] = true;
				GameCanvas.keyPressed[24] = true;
				return;
			case 5:
				if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack)
				{
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					return;
				}
				GameCanvas.keyHold[23] = true;
				GameCanvas.keyPressed[23] = true;
				return;
			case 6:
				goto IL_3C1;
			case 7:
				break;
			default:
				if (keyCode == -39)
				{
					goto IL_3C1;
				}
				if (keyCode != -38)
				{
					if (keyCode == -22)
					{
						goto IL_46A;
					}
					if (keyCode == -21)
					{
						goto IL_457;
					}
					if (keyCode == -26)
					{
						GameCanvas.keyHold[16] = true;
						GameCanvas.keyPressed[16] = true;
						return;
					}
					if (keyCode == 10)
					{
						goto IL_403;
					}
					if (keyCode == 35)
					{
						GameCanvas.keyHold[11] = true;
						GameCanvas.keyPressed[11] = true;
						return;
					}
					if (keyCode == 42)
					{
						GameCanvas.keyHold[10] = true;
						GameCanvas.keyPressed[10] = true;
						return;
					}
					if (keyCode != 113)
					{
						return;
					}
					GameCanvas.keyHold[17] = true;
					GameCanvas.keyPressed[17] = true;
					return;
				}
				break;
			}
			if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			GameCanvas.keyHold[21] = true;
			GameCanvas.keyPressed[21] = true;
			return;
			IL_3C1:
			if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			GameCanvas.keyHold[22] = true;
			GameCanvas.keyPressed[22] = true;
			return;
			IL_403:
			if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			GameCanvas.keyHold[25] = true;
			GameCanvas.keyPressed[25] = true;
			GameCanvas.keyHold[15] = true;
			GameCanvas.keyPressed[15] = true;
			return;
			IL_457:
			GameCanvas.keyHold[12] = true;
			GameCanvas.keyPressed[12] = true;
			return;
			IL_46A:
			GameCanvas.keyHold[13] = true;
			GameCanvas.keyPressed[13] = true;
			return;
		}
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x00008C86 File Offset: 0x00006E86
	public void keyReleasedz(int keyCode)
	{
		GameCanvas.keyAsciiPress = 0;
		this.mapKeyRelease(keyCode);
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x0009FDA4 File Offset: 0x0009DFA4
	public void mapKeyRelease(int keyCode)
	{
		switch (keyCode)
		{
		case 48:
			GameCanvas.keyHold[0] = false;
			GameCanvas.keyReleased[0] = true;
			return;
		case 49:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[1] = false;
				GameCanvas.keyReleased[1] = true;
			}
			return;
		case 50:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[2] = false;
				GameCanvas.keyReleased[2] = true;
			}
			return;
		case 51:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[3] = false;
				GameCanvas.keyReleased[3] = true;
			}
			return;
		case 52:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[4] = false;
				GameCanvas.keyReleased[4] = true;
			}
			return;
		case 53:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[5] = false;
				GameCanvas.keyReleased[5] = true;
			}
			return;
		case 54:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[6] = false;
				GameCanvas.keyReleased[6] = true;
			}
			return;
		case 55:
			GameCanvas.keyHold[7] = false;
			GameCanvas.keyReleased[7] = true;
			return;
		case 56:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[8] = false;
				GameCanvas.keyReleased[8] = true;
			}
			return;
		case 57:
			GameCanvas.keyHold[9] = false;
			GameCanvas.keyReleased[9] = true;
			return;
		default:
			switch (keyCode + 8)
			{
			case 0:
				GameCanvas.keyHold[14] = false;
				return;
			case 1:
				goto IL_31C;
			case 2:
				goto IL_309;
			case 3:
				goto IL_2E4;
			case 4:
				GameCanvas.keyHold[24] = false;
				return;
			case 5:
				GameCanvas.keyHold[23] = false;
				return;
			case 6:
				goto IL_2DA;
			case 7:
				break;
			default:
				if (keyCode == -39)
				{
					goto IL_2DA;
				}
				if (keyCode != -38)
				{
					if (keyCode == -22)
					{
						goto IL_31C;
					}
					if (keyCode == -21)
					{
						goto IL_309;
					}
					if (keyCode == -26)
					{
						GameCanvas.keyHold[16] = false;
						return;
					}
					if (keyCode == 10)
					{
						goto IL_2E4;
					}
					if (keyCode == 35)
					{
						GameCanvas.keyHold[11] = false;
						GameCanvas.keyReleased[11] = true;
						return;
					}
					if (keyCode == 42)
					{
						GameCanvas.keyHold[10] = false;
						GameCanvas.keyReleased[10] = true;
						return;
					}
					if (keyCode != 113)
					{
						return;
					}
					GameCanvas.keyHold[17] = false;
					GameCanvas.keyReleased[17] = true;
					return;
				}
				break;
			}
			GameCanvas.keyHold[21] = false;
			return;
			IL_2DA:
			GameCanvas.keyHold[22] = false;
			return;
			IL_2E4:
			GameCanvas.keyHold[25] = false;
			GameCanvas.keyReleased[25] = true;
			GameCanvas.keyHold[15] = true;
			GameCanvas.keyPressed[15] = true;
			return;
			IL_309:
			GameCanvas.keyHold[12] = false;
			GameCanvas.keyReleased[12] = true;
			return;
			IL_31C:
			GameCanvas.keyHold[13] = false;
			GameCanvas.keyReleased[13] = true;
			return;
		}
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x00008C95 File Offset: 0x00006E95
	public void pointerMouse(int x, int y)
	{
		GameCanvas.pxMouse = x;
		GameCanvas.pyMouse = y;
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x00008CA3 File Offset: 0x00006EA3
	public void scrollMouse(int a)
	{
		GameCanvas.pXYScrollMouse = a;
		if (GameCanvas.panel != null && GameCanvas.panel.isShow)
		{
			GameCanvas.panel.updateScroolMouse(a);
		}
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x000A00E0 File Offset: 0x0009E2E0
	public void pointerDragged(int x, int y)
	{
		GameCanvas.isPointerSelect = false;
		if (Res.abs(x - GameCanvas.pxLast) >= 10 || Res.abs(y - GameCanvas.pyLast) >= 10)
		{
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerDown = true;
			GameCanvas.isPointerMove = true;
		}
		GameCanvas.px = x;
		GameCanvas.py = y;
		GameCanvas.curPos++;
		if (GameCanvas.curPos > 3)
		{
			GameCanvas.curPos = 0;
		}
		GameCanvas.arrPos[GameCanvas.curPos] = new Position(x, y);
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x00008CC9 File Offset: 0x00006EC9
	public static bool isHoldPress()
	{
		return mSystem.currentTimeMillis() - GameCanvas.lastTimePress >= 800L;
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x000A0160 File Offset: 0x0009E360
	public void pointerPressed(int x, int y)
	{
		GameCanvas.isPointerSelect = false;
		GameCanvas.isPointerJustRelease = false;
		GameCanvas.isPointerJustDown = true;
		GameCanvas.isPointerDown = true;
		GameCanvas.isPointerClick = false;
		GameCanvas.isPointerMove = false;
		GameCanvas.lastTimePress = mSystem.currentTimeMillis();
		GameCanvas.pxFirst = x;
		GameCanvas.pyFirst = y;
		GameCanvas.pxLast = x;
		GameCanvas.pyLast = y;
		GameCanvas.px = x;
		GameCanvas.py = y;
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x00008CE1 File Offset: 0x00006EE1
	public void pointerReleased(int x, int y)
	{
		if (!GameCanvas.isPointerMove)
		{
			GameCanvas.isPointerSelect = true;
		}
		GameCanvas.isPointerDown = false;
		GameCanvas.isPointerMove = false;
		GameCanvas.isPointerJustRelease = true;
		GameCanvas.isPointerClick = true;
		mScreen.keyTouch = -1;
		GameCanvas.px = x;
		GameCanvas.py = y;
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x00008D1A File Offset: 0x00006F1A
	public static bool isPointerHoldIn(int x, int y, int w, int h)
	{
		return (GameCanvas.isPointerDown || GameCanvas.isPointerJustRelease) && (GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y) && GameCanvas.py <= y + h;
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x00008D55 File Offset: 0x00006F55
	public static bool isPointSelect(int x, int y, int w, int h)
	{
		return GameCanvas.isPointerSelect && (GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y) && GameCanvas.py <= y + h;
	}

	// Token: 0x06000AA6 RID: 2726 RVA: 0x00008D89 File Offset: 0x00006F89
	public static bool isMouseFocus(int x, int y, int w, int h)
	{
		return GameCanvas.pxMouse >= x && GameCanvas.pxMouse <= x + w && GameCanvas.pyMouse >= y && GameCanvas.pyMouse <= y + h;
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x000A01C0 File Offset: 0x0009E3C0
	public static void clearKeyPressed()
	{
		for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
		{
			GameCanvas.keyPressed[i] = false;
		}
		GameCanvas.isPointerJustRelease = false;
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x000A01F0 File Offset: 0x0009E3F0
	public static void clearKeyHold()
	{
		for (int i = 0; i < GameCanvas.keyHold.Length; i++)
		{
			GameCanvas.keyHold[i] = false;
		}
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x000A0218 File Offset: 0x0009E418
	public static void checkBackButton()
	{
		if (ChatPopup.serverChatPopUp == null && ChatPopup.currChatPopup == null)
		{
			GameCanvas.startYesNoDlg(mResources.DOYOUWANTEXIT, new Command(mResources.YES, GameCanvas.instance, 8885, null), new Command(mResources.NO, GameCanvas.instance, 8882, null));
		}
	}

	public void paintChangeMap(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		g.setColor(0);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		if (LoginScr.imgTitle != null)
		{
			g.drawImage(LoginScr.imgTitle, GameCanvas.w / 2, GameCanvas.h / 2 - 24, StaticObj.BOTTOM_HCENTER);
		}
		string text = mResources.PLEASEWAIT;
		if (LoginScr.timeLogin > 0)
		{
			text = text + " " + LoginScr.timeLogin + "s";
		}
		else if (GameCanvas.timeLoading > 0)
		{
			text = text + " " + GameCanvas.timeLoading + "s";
		}
		GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.h / 2 + 24, g);
		mFont.tahoma_7b_white.drawString(g, text, GameCanvas.w / 2, GameCanvas.h / 2, 2);
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x000A0334 File Offset: 0x0009E534
	public void paint(mGraphics gx)
	{
		try
		{
			GameCanvas.debugPaint.removeAllElements();
			GameCanvas.debug("PA", 1);
			if (global::Char.isLoadingMap && GameCanvas.currentScreen == GameScr.gI())
			{
				this.paintChangeMap(this.g);
				return;
			}
			if (GameCanvas.currentScreen != null)
			{
				GameCanvas.currentScreen.paint(this.g);
			}
			GameCanvas.debug("PB", 1);
			this.g.translate(-this.g.getTranslateX(), -this.g.getTranslateY());
			this.g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (GameCanvas.panel.isShow)
			{
				GameCanvas.panel.paint(this.g);
				if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow)
				{
					GameCanvas.panel2.paint(this.g);
				}
				if (GameCanvas.panel.chatTField != null && GameCanvas.panel.chatTField.isShow)
				{
					GameCanvas.panel.chatTField.paint(this.g);
				}
				if (GameCanvas.panel2 != null && GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow)
				{
					GameCanvas.panel2.chatTField.paint(this.g);
				}
			}
			Res.paintOnScreenDebug(this.g);
			InfoDlg.paint(this.g);
			if (GameCanvas.currentDialog != null)
			{
				GameCanvas.debug("PC", 1);
				GameCanvas.currentDialog.paint(this.g);
			}
			else if (GameCanvas.menu.showMenu)
			{
				GameCanvas.debug("PD", 1);
				GameCanvas.resetTrans(this.g);
				GameCanvas.menu.paintMenu(this.g);
			}
			GameScr.info1.paint(this.g);
			GameScr.info2.paint(this.g);
			if (GameScr.gI().popUpYesNo != null)
			{
				GameScr.gI().popUpYesNo.paint(this.g);
			}
			if (ChatPopup.currChatPopup != null)
			{
				ChatPopup.currChatPopup.paint(this.g);
			}
			Hint.paint(this.g);
			if (ChatPopup.serverChatPopUp != null)
			{
				ChatPopup.serverChatPopUp.paint(this.g);
			}
			for (int i = 0; i < Effect2.vEffect2.size(); i++)
			{
				Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
				if (effect is ChatPopup && !effect.Equals(ChatPopup.currChatPopup) && !effect.Equals(ChatPopup.serverChatPopUp))
				{
					effect.paint(this.g);
				}
			}
			if (GameCanvas.currentDialog != null)
			{
				GameCanvas.currentDialog.paint(this.g);
			}
			if (GameCanvas.isWait())
			{
				this.paintChangeMap(this.g);
				if (GameCanvas.timeLoading > 0 && LoginScr.timeLogin <= 0 && mSystem.currentTimeMillis() - GameCanvas.TIMEOUT >= 1000L)
				{
					GameCanvas.timeLoading--;
					if (GameCanvas.timeLoading == 0)
					{
						GameCanvas.timeLoading = 0;
					}
					GameCanvas.TIMEOUT = mSystem.currentTimeMillis();
				}
			}
			GameCanvas.debug("PE", 1);
			GameCanvas.resetTrans(this.g);
			EffecMn.paintLayer4(this.g);
			if (GameCanvas.open3Hour && !GameCanvas.isLoading)
			{
				if (GameCanvas.currentScreen == GameCanvas.loginScr || GameCanvas.currentScreen == GameCanvas.serverScreen || GameCanvas.currentScreen == GameCanvas.serverScr)
				{
					this.g.drawImage(GameCanvas.img18, 5, 5, 0);
				}
				if (GameCanvas.currentScreen == CreateCharScr.instance)
				{
					this.g.drawImage(GameCanvas.img18, GameCanvas.hw, 5, 0);
				}
			}
			GameCanvas.resetTrans(this.g);
			int num = GameCanvas.h / 4;
			if (GameCanvas.currentScreen != null && GameCanvas.currentScreen is GameScr && GameCanvas.thongBaoTest != null)
			{
				this.g.setClip(60, num, GameCanvas.w - 120, mFont.tahoma_7_white.getHeight() + 2);
				mFont.tahoma_7_grey.drawString(this.g, GameCanvas.thongBaoTest, GameCanvas.xThongBaoTranslate, num + 1, 0);
				mFont.tahoma_7_yellow.drawString(this.g, GameCanvas.thongBaoTest, GameCanvas.xThongBaoTranslate, num, 0);
				this.g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x00008DB4 File Offset: 0x00006FB4
	public static void endDlg()
	{
		if (GameCanvas.inputDlg != null)
		{
			GameCanvas.inputDlg.tfInput.setMaxTextLenght(500);
		}
		GameCanvas.currentDialog = null;
		InfoDlg.hide();
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x00008DDC File Offset: 0x00006FDC
	public static void startOKDlg(string info)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x00008E0F File Offset: 0x0000700F
	public static void startWaitDlg(string info)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
		GameCanvas.msgdlg.isWait = true;
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x00008E0F File Offset: 0x0000700F
	public static void startOKDlg(string info, bool isError)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
		GameCanvas.msgdlg.isWait = true;
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x00008E4D File Offset: 0x0000704D
	public static void startWaitDlg()
	{
		GameCanvas.closeKeyBoard();
		global::Char.isLoadingMap = true;
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x00008E5A File Offset: 0x0000705A
	public void openWeb(string strLeft, string strRight, string url, string str)
	{
		GameCanvas.msgdlg.setInfo(str, new Command(strLeft, this, 8881, url), null, new Command(strRight, this, 8882, null));
		GameCanvas.currentDialog = GameCanvas.msgdlg;
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x00008E8D File Offset: 0x0000708D
	public static void startOK(string info, int actionID, object p)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, actionID, p), null);
		GameCanvas.msgdlg.show();
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x000A0758 File Offset: 0x0009E958
	public static void startYesNoDlg(string info, int iYes, object pYes, int iNo, object pNo)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, new Command(mResources.YES, GameCanvas.instance, iYes, pYes), new Command(string.Empty, GameCanvas.instance, iYes, pYes), new Command(mResources.NO, GameCanvas.instance, iNo, pNo));
		GameCanvas.msgdlg.show();
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x00008EBC File Offset: 0x000070BC
	public static void startYesNoDlg(string info, Command cmdYes, Command cmdNo)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, cmdYes, null, cmdNo);
		GameCanvas.msgdlg.show();
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x00008EDB File Offset: 0x000070DB
	public static void startserverThongBao(string msgSv)
	{
		GameCanvas.thongBaoTest = msgSv;
		GameCanvas.xThongBaoTranslate = GameCanvas.w - 60;
		GameCanvas.dir_ = -1;
	}

	// Token: 0x06000AB6 RID: 2742 RVA: 0x000A07B4 File Offset: 0x0009E9B4
	public static string getMoneys(int m)
	{
		string text = string.Empty;
		int num = m / 1000 + 1;
		for (int i = 0; i < num; i++)
		{
			if (m < 1000)
			{
				text = m + text;
				break;
			}
			int num2 = m % 1000;
			if (num2 == 0)
			{
				text = ".000" + text;
			}
			else if (num2 < 10)
			{
				text = ".00" + num2 + text;
			}
			else if (num2 < 100)
			{
				text = ".0" + num2 + text;
			}
			else
			{
				text = "." + num2 + text;
			}
			m /= 1000;
		}
		return text;
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x00008EF6 File Offset: 0x000070F6
	public static int getX(int start, int w)
	{
		return (GameCanvas.px - start) / w;
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x00008F01 File Offset: 0x00007101
	public static int getY(int start, int w)
	{
		return (GameCanvas.py - start) / w;
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x000045ED File Offset: 0x000027ED
	protected void sizeChanged(int w, int h)
	{
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x00004E4B File Offset: 0x0000304B
	public static bool isGetResourceFromServer()
	{
		return true;
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x000A0864 File Offset: 0x0009EA64
	public static Image loadImageRMS(string path)
	{
		path = string.Concat(new object[]
		{
			Main.res,
			"/x",
			mGraphics.zoomLevel,
			path
		});
		path = GameCanvas.cutPng(path);
		Image result = null;
		try
		{
			result = Image.createImage(path);
		}
		catch (Exception ex)
		{
			try
			{
				string[] array = Res.split(path, "/", 0);
				sbyte[] array2 = Rms.loadRMS("x" + mGraphics.zoomLevel + array[array.Length - 1]);
				if (array2 != null)
				{
					result = Image.createImage(array2, 0, array2.Length);
				}
			}
			catch (Exception)
			{
				Cout.LogError("Loi ham khong tim thay a: " + ex.ToString());
			}
		}
		return result;
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x000A0928 File Offset: 0x0009EB28
	public static Image loadImage(string path)
	{
		path = string.Concat(new object[]
		{
			Main.res,
			"/x",
			mGraphics.zoomLevel,
			path
		});
		path = GameCanvas.cutPng(path);
		Image result = null;
		try
		{
			result = Image.createImage(path);
		}
		catch (Exception)
		{
		}
		return result;
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x000A098C File Offset: 0x0009EB8C
	public static string cutPng(string str)
	{
		string result = str;
		if (str.Contains(".png"))
		{
			result = str.Replace(".png", string.Empty);
		}
		return result;
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x00008F0C File Offset: 0x0000710C
	public static int random(int a, int b)
	{
		return a + GameCanvas.r.nextInt(b - a);
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x000A09BC File Offset: 0x0009EBBC
	public bool startDust(int dir, int x, int y)
	{
		if (GameCanvas.lowGraphic)
		{
			return false;
		}
		int num = (dir != 1) ? 1 : 0;
		if (this.dustState[num] != -1)
		{
			return false;
		}
		this.dustState[num] = 0;
		this.dustX[num] = x;
		this.dustY[num] = y;
		return true;
	}

	// Token: 0x06000AC0 RID: 2752 RVA: 0x000A0A04 File Offset: 0x0009EC04
	public void loadWaterSplash()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		GameCanvas.imgWS = new Image[3];
		for (int i = 0; i < 3; i++)
		{
			GameCanvas.imgWS[i] = GameCanvas.loadImage("/e/w" + i + ".png");
		}
		GameCanvas.wsX = new int[2];
		GameCanvas.wsY = new int[2];
		GameCanvas.wsState = new int[2];
		GameCanvas.wsF = new int[2];
		GameCanvas.wsState[0] = (GameCanvas.wsState[1] = -1);
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x000A0A90 File Offset: 0x0009EC90
	public bool startWaterSplash(int x, int y)
	{
		if (GameCanvas.lowGraphic)
		{
			return false;
		}
		int num = (GameCanvas.wsState[0] != -1) ? 1 : 0;
		if (GameCanvas.wsState[num] != -1)
		{
			return false;
		}
		GameCanvas.wsState[num] = 0;
		GameCanvas.wsX[num] = x;
		GameCanvas.wsY[num] = y;
		return true;
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x000A0ADC File Offset: 0x0009ECDC
	public void updateWaterSplash()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (GameCanvas.wsState[i] != -1)
			{
				GameCanvas.wsY[i]--;
				if (GameCanvas.gameTick % 2 == 0)
				{
					GameCanvas.wsState[i]++;
					if (GameCanvas.wsState[i] > 2)
					{
						GameCanvas.wsState[i] = -1;
					}
					else
					{
						GameCanvas.wsF[i] = GameCanvas.wsState[i];
					}
				}
			}
		}
	}

	// Token: 0x06000AC3 RID: 2755 RVA: 0x000A0B54 File Offset: 0x0009ED54
	public void updateDust()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.dustState[i] != -1)
			{
				this.dustState[i]++;
				if (this.dustState[i] >= 5)
				{
					this.dustState[i] = -1;
				}
				if (i == 0)
				{
					this.dustX[i]--;
				}
				else
				{
					this.dustX[i]++;
				}
				this.dustY[i]--;
			}
		}
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x00008F1D File Offset: 0x0000711D
	public static bool isPaint(int x, int y)
	{
		return x >= GameScr.cmx && x <= GameScr.cmx + GameScr.gW && y >= GameScr.cmy && y <= GameScr.cmy + GameScr.gH + 30;
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x000A0BE0 File Offset: 0x0009EDE0
	public void paintDust(mGraphics g)
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.dustState[i] != -1 && GameCanvas.isPaint(this.dustX[i], this.dustY[i]))
			{
				g.drawImage(GameCanvas.imgDust[i][this.dustState[i]], this.dustX[i], this.dustY[i], 3);
			}
		}
	}

	// Token: 0x06000AC6 RID: 2758 RVA: 0x000A0C4C File Offset: 0x0009EE4C
	public void loadDust()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		if (GameCanvas.imgDust == null)
		{
			GameCanvas.imgDust = new Image[2][];
			for (int i = 0; i < GameCanvas.imgDust.Length; i++)
			{
				GameCanvas.imgDust[i] = new Image[5];
			}
			for (int j = 0; j < 2; j++)
			{
				for (int k = 0; k < 5; k++)
				{
					GameCanvas.imgDust[j][k] = GameCanvas.loadImage(string.Concat(new object[]
					{
						"/e/d",
						j,
						k,
						".png"
					}));
				}
			}
		}
		this.dustX = new int[2];
		this.dustY = new int[2];
		this.dustState = new int[2];
		this.dustState[0] = (this.dustState[1] = -1);
	}

	// Token: 0x06000AC7 RID: 2759 RVA: 0x000A0D20 File Offset: 0x0009EF20
	public static void paintShukiren(int x, int y, mGraphics g)
	{
		g.drawRegion(GameCanvas.imgShuriken, 0, Main.f * 16, 16, 16, 0, x, y, mGraphics.HCENTER | mGraphics.VCENTER);
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x00008F53 File Offset: 0x00007153
	public void resetToLoginScrz()
	{
		this.resetToLoginScr = true;
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x00008D1A File Offset: 0x00006F1A
	public static bool isPointer(int x, int y, int w, int h)
	{
		return (GameCanvas.isPointerDown || GameCanvas.isPointerJustRelease) && (GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y) && GameCanvas.py <= y + h;
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x000A0D54 File Offset: 0x0009EF54
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 88810:
		{
			int playerMapId = (int)p;
			GameCanvas.endDlg();
			Service.gI().acceptInviteTrade(playerMapId);
			return;
		}
		case 88811:
			GameCanvas.endDlg();
			Service.gI().cancelInviteTrade();
			return;
		case 88814:
		{
			Item[] items = (Item[])p;
			GameCanvas.endDlg();
			Service.gI().crystalCollectLock(items);
			return;
		}
		case 88815:
			return;
		case 88817:
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, GameCanvas.menu.menuSelectedItem, 0);
			return;
		case 88818:
		{
			short menuId = (short)p;
			Service.gI().textBoxId(menuId, GameCanvas.inputDlg.tfInput.getText());
			GameCanvas.endDlg();
			return;
		}
		case 88819:
		{
			short menuId2 = (short)p;
			Service.gI().menuId(menuId2);
			return;
		}
		case 88820:
		{
			string[] array = (string[])p;
			if (global::Char.myCharz().npcFocus == null)
			{
				return;
			}
			int menuSelectedItem = GameCanvas.menu.menuSelectedItem;
			if (array.Length > 1)
			{
				MyVector myVector = new MyVector();
				for (int i = 0; i < array.Length - 1; i++)
				{
					myVector.addElement(new Command(array[i + 1], GameCanvas.instance, 88821, menuSelectedItem));
				}
				GameCanvas.menu.startAt(myVector, 3);
				return;
			}
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, menuSelectedItem, 0);
			return;
		}
		case 88821:
		{
			int menuId3 = (int)p;
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, menuId3, GameCanvas.menu.menuSelectedItem);
			return;
		}
		case 88822:
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, GameCanvas.menu.menuSelectedItem, 0);
			return;
		case 88823:
			GameCanvas.startOKDlg(mResources.SENTMSG);
			return;
		case 88824:
			GameCanvas.startOKDlg(mResources.NOSENDMSG);
			return;
		case 88825:
			GameCanvas.startOKDlg(mResources.sendMsgSuccess, false);
			return;
		case 88826:
			GameCanvas.startOKDlg(mResources.cannotSendMsg, false);
			return;
		case 88827:
			GameCanvas.startOKDlg(mResources.sendGuessMsgSuccess);
			return;
		case 88828:
			GameCanvas.startOKDlg(mResources.sendMsgFail);
			return;
		case 88829:
		{
			string text = GameCanvas.inputDlg.tfInput.getText();
			if (text.Equals(string.Empty))
			{
				return;
			}
			Service.gI().changeName(text, (int)p);
			InfoDlg.showWait();
			return;
		}
		case 88836:
			GameCanvas.inputDlg.tfInput.setMaxTextLenght(6);
			GameCanvas.inputDlg.show(mResources.INPUT_PRIVATE_PASS, new Command(mResources.ACCEPT, GameCanvas.instance, 888361, null), TField.INPUT_TYPE_NUMERIC);
			return;
		case 88837:
		{
			string text2 = GameCanvas.inputDlg.tfInput.getText();
			GameCanvas.endDlg();
			try
			{
				Service.gI().openLockAccProtect(int.Parse(text2.Trim()));
				return;
			}
			catch (Exception ex)
			{
				Cout.println("Loi tai 88837 " + ex.ToString());
				return;
			}
			goto IL_735;
		}
		case 88839:
			goto IL_735;
		}
		switch (idAction)
		{
		case 8881:
		{
			string url = (string)p;
			try
			{
				GameMidlet.instance.platformRequest(url);
			}
			catch (Exception)
			{
			}
			GameCanvas.currentDialog = null;
			return;
		}
		case 8882:
			InfoDlg.hide();
			GameCanvas.currentDialog = null;
			ServerListScreen.isAutoConect = false;
			ServerListScreen.countDieConnect = 0;
			return;
		case 8884:
			GameCanvas.endDlg();
			if (GameCanvas.serverScr == null)
			{
				GameCanvas.serverScr = new ServerScr();
			}
			GameCanvas.serverScr.switchToMe();
			return;
		case 8885:
			GameMidlet.instance.exit();
			return;
		case 8886:
		{
			GameCanvas.endDlg();
			string name = (string)p;
			Service.gI().addFriend(name);
			return;
		}
		case 8887:
		{
			GameCanvas.endDlg();
			int charId = (int)p;
			Service.gI().addPartyAccept(charId);
			return;
		}
		case 8888:
		{
			int charId2 = (int)p;
			Service.gI().addPartyCancel(charId2);
			GameCanvas.endDlg();
			return;
		}
		case 8889:
		{
			string str = (string)p;
			GameCanvas.endDlg();
			Service.gI().acceptPleaseParty(str);
			return;
		}
		}
		switch (idAction)
		{
		case 888391:
		{
			string s = (string)p;
			GameCanvas.endDlg();
			Service.gI().clearAccProtect(int.Parse(s));
			return;
		}
		case 888392:
			Service.gI().menu(4, GameCanvas.menu.menuSelectedItem, 0);
			return;
		case 888393:
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.doLogin();
			Main.closeKeyBoard();
			return;
		case 888394:
			GameCanvas.endDlg();
			return;
		case 888395:
			GameCanvas.endDlg();
			return;
		case 888396:
			GameCanvas.endDlg();
			return;
		case 888397:
		{
			string text3 = (string)p;
			return;
		}
		default:
			switch (idAction)
			{
			case 100001:
				Service.gI().getFlag(0, -1);
				InfoDlg.showWait();
				return;
			case 100002:
				if (GameCanvas.loginScr == null)
				{
					GameCanvas.loginScr = new LoginScr();
				}
				GameCanvas.loginScr.backToRegister();
				return;
			case 100003:
			case 100004:
				return;
			case 100005:
				if (global::Char.myCharz().statusMe == 14)
				{
					GameCanvas.startOKDlg(mResources.can_not_do_when_die);
					return;
				}
				Service.gI().openUIZone();
				GameCanvas.panel.setTypeZone();
				GameCanvas.panel.show();
				return;
			case 100006:
				mSystem.onDisconnected();
				return;
			default:
				switch (idAction)
				{
				case 101023:
					Main.numberQuit = 0;
					return;
				case 101024:
					Res.outz("output 101024");
					GameCanvas.endDlg();
					return;
				case 101025:
					GameCanvas.endDlg();
					if (ServerListScreen.loadScreen)
					{
						GameCanvas.serverScreen.switchToMe();
						return;
					}
					GameCanvas.serverScreen.show2();
					return;
				case 101026:
					mSystem.onDisconnected();
					return;
				default:
					if (idAction == 999)
					{
						mSystem.closeBanner();
						GameCanvas.endDlg();
						return;
					}
					if (idAction != 9000)
					{
						if (idAction != 9999)
						{
							if (idAction != 100016)
							{
								if (idAction != 888361)
								{
									return;
								}
								string text4 = GameCanvas.inputDlg.tfInput.getText();
								GameCanvas.endDlg();
								if (text4.Length < 6 || text4.Equals(string.Empty))
								{
									GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_1);
									return;
								}
								try
								{
									Service.gI().activeAccProtect(int.Parse(text4));
									return;
								}
								catch (Exception ex2)
								{
									GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
									Cout.println("Loi tai 888361 Gamescavas " + ex2.ToString());
									return;
								}
							}
							ServerListScreen.SetIpSelect(17, false);
							GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
							ServerListScreen.waitToLogin = true;
							GameCanvas.endDlg();
							return;
						}
						GameCanvas.endDlg();
						GameCanvas.connect();
						Service.gI().setClientType();
						if (GameCanvas.loginScr == null)
						{
							GameCanvas.loginScr = new LoginScr();
						}
						GameCanvas.loginScr.doLogin();
						return;
					}
					else
					{
						GameCanvas.endDlg();
						SplashScr.imgLogo = null;
						SmallImage.loadBigRMS();
						mSystem.gcc();
						ServerListScreen.bigOk = true;
						ServerListScreen.loadScreen = true;
						GameScr.gI().loadGameScr();
						if (GameCanvas.currentScreen != GameCanvas.loginScr)
						{
							GameCanvas.serverScreen.switchToMe2();
							return;
						}
						return;
					}
					break;
				}
				break;
			}
			break;
		}
		IL_735:
		string text5 = GameCanvas.inputDlg.tfInput.getText();
		GameCanvas.endDlg();
		if (text5.Length < 6 || text5.Equals(string.Empty))
		{
			GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_1);
			return;
		}
		try
		{
			GameCanvas.startYesNoDlg(mResources.cancelAccountProtection, 888391, text5, 8882, null);
		}
		catch (Exception)
		{
			GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
		}
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x00008F5C File Offset: 0x0000715C
	public static void clearAllPointerEvent()
	{
		GameCanvas.isPointerClick = false;
		GameCanvas.isPointerDown = false;
		GameCanvas.isPointerJustDown = false;
		GameCanvas.isPointerJustRelease = false;
		GameCanvas.isPointerSelect = false;
		GameScr.gI().lastSingleClick = 0L;
		GameScr.gI().isPointerDowning = false;
	}

	// Token: 0x06000ACC RID: 2764 RVA: 0x00008F93 File Offset: 0x00007193
	public static bool isWait()
	{
		return global::Char.isLoadingMap || LoginScr.isContinueToLogin || ServerListScreen.waitToLogin || ServerListScreen.isWait || SelectCharScr.isWait;
	}

	// Token: 0x040013C5 RID: 5061
	public static long timeNow = 0L;

	// Token: 0x040013C6 RID: 5062
	public static bool open3Hour;

	// Token: 0x040013C7 RID: 5063
	public static bool lowGraphic = false;

	// Token: 0x040013C8 RID: 5064
	public static bool serverchat = false;

	// Token: 0x040013C9 RID: 5065
	public static bool isMoveNumberPad = true;

	// Token: 0x040013CA RID: 5066
	public static bool isLoading;

	// Token: 0x040013CB RID: 5067
	public static bool isTouch = false;

	// Token: 0x040013CC RID: 5068
	public static bool isTouchControl;

	// Token: 0x040013CD RID: 5069
	public static bool isTouchControlSmallScreen;

	// Token: 0x040013CE RID: 5070
	public static bool isTouchControlLargeScreen;

	// Token: 0x040013CF RID: 5071
	public static bool isConnectFail;

	// Token: 0x040013D0 RID: 5072
	public static GameCanvas instance;

	// Token: 0x040013D1 RID: 5073
	public static bool bRun;

	// Token: 0x040013D2 RID: 5074
	public static bool[] keyPressed = new bool[30];

	// Token: 0x040013D3 RID: 5075
	public static bool[] keyReleased = new bool[30];

	// Token: 0x040013D4 RID: 5076
	public static bool[] keyHold = new bool[30];

	// Token: 0x040013D5 RID: 5077
	public static bool isPointerDown;

	// Token: 0x040013D6 RID: 5078
	public static bool isPointerClick;

	// Token: 0x040013D7 RID: 5079
	public static bool isPointerJustRelease;

	// Token: 0x040013D8 RID: 5080
	public static bool isPointerSelect;

	// Token: 0x040013D9 RID: 5081
	public static bool isPointerMove;

	// Token: 0x040013DA RID: 5082
	public static int px;

	// Token: 0x040013DB RID: 5083
	public static int py;

	// Token: 0x040013DC RID: 5084
	public static int pxFirst;

	// Token: 0x040013DD RID: 5085
	public static int pyFirst;

	// Token: 0x040013DE RID: 5086
	public static int pxLast;

	// Token: 0x040013DF RID: 5087
	public static int pyLast;

	// Token: 0x040013E0 RID: 5088
	public static int pxMouse;

	// Token: 0x040013E1 RID: 5089
	public static int pyMouse;

	// Token: 0x040013E2 RID: 5090
	public static Position[] arrPos = new Position[4];

	// Token: 0x040013E3 RID: 5091
	public static int gameTick;

	// Token: 0x040013E4 RID: 5092
	public static int taskTick;

	// Token: 0x040013E5 RID: 5093
	public static bool isEff1;

	// Token: 0x040013E6 RID: 5094
	public static bool isEff2;

	// Token: 0x040013E7 RID: 5095
	public static long timeTickEff1;

	// Token: 0x040013E8 RID: 5096
	public static long timeTickEff2;

	// Token: 0x040013E9 RID: 5097
	public static int w;

	// Token: 0x040013EA RID: 5098
	public static int h;

	// Token: 0x040013EB RID: 5099
	public static int hw;

	// Token: 0x040013EC RID: 5100
	public static int hh;

	// Token: 0x040013ED RID: 5101
	public static int wd3;

	// Token: 0x040013EE RID: 5102
	public static int hd3;

	// Token: 0x040013EF RID: 5103
	public static int w2d3;

	// Token: 0x040013F0 RID: 5104
	public static int h2d3;

	// Token: 0x040013F1 RID: 5105
	public static int w3d4;

	// Token: 0x040013F2 RID: 5106
	public static int h3d4;

	// Token: 0x040013F3 RID: 5107
	public static int wd6;

	// Token: 0x040013F4 RID: 5108
	public static int hd6;

	// Token: 0x040013F5 RID: 5109
	public static mScreen currentScreen;

	// Token: 0x040013F6 RID: 5110
	public static Menu menu = new Menu();

	// Token: 0x040013F7 RID: 5111
	public static Panel panel;

	// Token: 0x040013F8 RID: 5112
	public static Panel panel2;

	// Token: 0x040013F9 RID: 5113
	public static ChooseCharScr chooseCharScr;

	// Token: 0x040013FA RID: 5114
	public static LoginScr loginScr;

	// Token: 0x040013FB RID: 5115
	public static RegisterScreen registerScr;

	// Token: 0x040013FC RID: 5116
	public static Dialog currentDialog;

	// Token: 0x040013FD RID: 5117
	public static MsgDlg msgdlg;

	// Token: 0x040013FE RID: 5118
	public static InputDlg inputDlg;

	// Token: 0x040013FF RID: 5119
	public static MyVector currentPopup = new MyVector();

	// Token: 0x04001400 RID: 5120
	public static int requestLoseCount;

	// Token: 0x04001401 RID: 5121
	public static MyVector listPoint;

	// Token: 0x04001402 RID: 5122
	public static Paint paintz;

	// Token: 0x04001403 RID: 5123
	public static bool isGetResFromServer;

	// Token: 0x04001404 RID: 5124
	public static Image[] imgBG;

	// Token: 0x04001405 RID: 5125
	public static int skyColor;

	// Token: 0x04001406 RID: 5126
	public static int curPos = 0;

	// Token: 0x04001407 RID: 5127
	public static int[] bgW;

	// Token: 0x04001408 RID: 5128
	public static int[] bgH;

	// Token: 0x04001409 RID: 5129
	public static int planet = 0;

	// Token: 0x0400140A RID: 5130
	private mGraphics g = new mGraphics();

	// Token: 0x0400140B RID: 5131
	public static Image img18;

	// Token: 0x0400140C RID: 5132
	public static Image[] imgBlue = new Image[7];

	// Token: 0x0400140D RID: 5133
	public static Image[] imgViolet = new Image[7];

	// Token: 0x0400140E RID: 5134
	public static MyHashTable danhHieu = new MyHashTable();

	// Token: 0x0400140F RID: 5135
	public static MyVector messageServer = new MyVector(string.Empty);

	// Token: 0x04001410 RID: 5136
	public static bool isPlaySound = true;

	// Token: 0x04001411 RID: 5137
	private static int clearOldData;

	// Token: 0x04001412 RID: 5138
	public static int timeOpenKeyBoard;

	// Token: 0x04001413 RID: 5139
	public static bool isFocusPanel2;

	// Token: 0x04001414 RID: 5140
	public static int fps = 0;

	// Token: 0x04001415 RID: 5141
	public static int max;

	// Token: 0x04001416 RID: 5142
	public static int up;

	// Token: 0x04001417 RID: 5143
	public static int upmax;

	// Token: 0x04001418 RID: 5144
	private long timefps = mSystem.currentTimeMillis() + 1000L;

	// Token: 0x04001419 RID: 5145
	private long timeup = mSystem.currentTimeMillis() + 1000L;

	// Token: 0x0400141A RID: 5146
	public static int isRequestMapID = -1;

	// Token: 0x0400141B RID: 5147
	public static long waitingTimeChangeMap;

	// Token: 0x0400141C RID: 5148
	private static int dir_ = -1;

	// Token: 0x0400141D RID: 5149
	private int tickWaitThongBao;

	// Token: 0x0400141E RID: 5150
	public bool isPaintCarret;

	// Token: 0x0400141F RID: 5151
	public static MyVector debugUpdate;

	// Token: 0x04001420 RID: 5152
	public static MyVector debugPaint;

	// Token: 0x04001421 RID: 5153
	public static MyVector debugSession;

	// Token: 0x04001422 RID: 5154
	private static bool isShowErrorForm = false;

	// Token: 0x04001423 RID: 5155
	public static bool paintBG;

	// Token: 0x04001424 RID: 5156
	public static int gsskyHeight;

	// Token: 0x04001425 RID: 5157
	public static int gsgreenField1Y;

	// Token: 0x04001426 RID: 5158
	public static int gsgreenField2Y;

	// Token: 0x04001427 RID: 5159
	public static int gshouseY;

	// Token: 0x04001428 RID: 5160
	public static int gsmountainY;

	// Token: 0x04001429 RID: 5161
	public static int bgLayer0y;

	// Token: 0x0400142A RID: 5162
	public static int bgLayer1y;

	// Token: 0x0400142B RID: 5163
	public static Image imgCloud;

	// Token: 0x0400142C RID: 5164
	public static Image imgSun;

	// Token: 0x0400142D RID: 5165
	public static Image imgSun2;

	// Token: 0x0400142E RID: 5166
	public static Image imgClear;

	// Token: 0x0400142F RID: 5167
	public static Image[] imgBorder = new Image[3];

	// Token: 0x04001430 RID: 5168
	public static Image[] imgSunSpec = new Image[3];

	// Token: 0x04001431 RID: 5169
	public static int borderConnerW;

	// Token: 0x04001432 RID: 5170
	public static int borderConnerH;

	// Token: 0x04001433 RID: 5171
	public static int borderCenterW;

	// Token: 0x04001434 RID: 5172
	public static int borderCenterH;

	// Token: 0x04001435 RID: 5173
	public static int[] cloudX;

	// Token: 0x04001436 RID: 5174
	public static int[] cloudY;

	// Token: 0x04001437 RID: 5175
	public static int sunX;

	// Token: 0x04001438 RID: 5176
	public static int sunY;

	// Token: 0x04001439 RID: 5177
	public static int sunX2;

	// Token: 0x0400143A RID: 5178
	public static int sunY2;

	// Token: 0x0400143B RID: 5179
	public static int[] layerSpeed;

	// Token: 0x0400143C RID: 5180
	public static int[] moveX;

	// Token: 0x0400143D RID: 5181
	public static int[] moveXSpeed;

	// Token: 0x0400143E RID: 5182
	public static bool isBoltEff;

	// Token: 0x0400143F RID: 5183
	public static bool boltActive;

	// Token: 0x04001440 RID: 5184
	public static int tBolt;

	// Token: 0x04001441 RID: 5185
	public static Image imgBgIOS;

	// Token: 0x04001442 RID: 5186
	public static int typeBg = -1;

	// Token: 0x04001443 RID: 5187
	public static int transY;

	// Token: 0x04001444 RID: 5188
	public static int[] yb = new int[5];

	// Token: 0x04001445 RID: 5189
	public static int[] colorTop;

	// Token: 0x04001446 RID: 5190
	public static int[] colorBotton;

	// Token: 0x04001447 RID: 5191
	public static int yb1;

	// Token: 0x04001448 RID: 5192
	public static int yb2;

	// Token: 0x04001449 RID: 5193
	public static int yb3;

	// Token: 0x0400144A RID: 5194
	public static int nBg = 0;

	// Token: 0x0400144B RID: 5195
	public static int lastBg = -1;

	// Token: 0x0400144C RID: 5196
	public static int[] bgRain = new int[]
	{
		1,
		4,
		11
	};

	// Token: 0x0400144D RID: 5197
	public static int[] bgRainFont = new int[]
	{
		-1
	};

	// Token: 0x0400144E RID: 5198
	public static Image imgCaycot;

	// Token: 0x0400144F RID: 5199
	public static Image tam;

	// Token: 0x04001450 RID: 5200
	public static int typeBackGround = -1;

	// Token: 0x04001451 RID: 5201
	public static int saveIDBg = -10;

	// Token: 0x04001452 RID: 5202
	public static bool isLoadBGok;

	// Token: 0x04001453 RID: 5203
	private static long lastTimePress = 0L;

	private static long lastTimeKeepAlive = 0L;

	// Token: 0x04001454 RID: 5204
	public static int keyAsciiPress;

	// Token: 0x04001455 RID: 5205
	public static int pXYScrollMouse;

	// Token: 0x04001456 RID: 5206
	private static Image imgSignal;

	// Token: 0x04001457 RID: 5207
	public static MyVector flyTexts = new MyVector();

	// Token: 0x04001458 RID: 5208
	public int longTime;

	// Token: 0x04001459 RID: 5209
	public static long timeBreakLoading;

	// Token: 0x0400145A RID: 5210
	private static string thongBaoTest;

	// Token: 0x0400145B RID: 5211
	public static int xThongBaoTranslate = GameCanvas.w - 60;

	// Token: 0x0400145C RID: 5212
	public static bool isPointerJustDown = false;

	// Token: 0x0400145D RID: 5213
	private int count = 1;

	// Token: 0x0400145E RID: 5214
	public static bool csWait;

	// Token: 0x0400145F RID: 5215
	public static MyRandom r = new MyRandom();

	// Token: 0x04001460 RID: 5216
	public static bool isBlackScreen;

	// Token: 0x04001461 RID: 5217
	public static int[] bgSpeed;

	// Token: 0x04001462 RID: 5218
	public static int cmdBarX;

	// Token: 0x04001463 RID: 5219
	public static int cmdBarY;

	// Token: 0x04001464 RID: 5220
	public static int cmdBarW;

	// Token: 0x04001465 RID: 5221
	public static int cmdBarH;

	// Token: 0x04001466 RID: 5222
	public static int cmdBarLeftW;

	// Token: 0x04001467 RID: 5223
	public static int cmdBarRightW;

	// Token: 0x04001468 RID: 5224
	public static int cmdBarCenterW;

	// Token: 0x04001469 RID: 5225
	public static int hpBarX;

	// Token: 0x0400146A RID: 5226
	public static int hpBarY;

	// Token: 0x0400146B RID: 5227
	public static int hpBarW;

	// Token: 0x0400146C RID: 5228
	public static int expBarW;

	// Token: 0x0400146D RID: 5229
	public static int lvPosX;

	// Token: 0x0400146E RID: 5230
	public static int moneyPosX;

	// Token: 0x0400146F RID: 5231
	public static int hpBarH;

	// Token: 0x04001470 RID: 5232
	public static int girlHPBarY;

	// Token: 0x04001471 RID: 5233
	public int timeOut;

	// Token: 0x04001472 RID: 5234
	public int[] dustX;

	// Token: 0x04001473 RID: 5235
	public int[] dustY;

	// Token: 0x04001474 RID: 5236
	public int[] dustState;

	// Token: 0x04001475 RID: 5237
	public static int[] wsX;

	// Token: 0x04001476 RID: 5238
	public static int[] wsY;

	// Token: 0x04001477 RID: 5239
	public static int[] wsState;

	// Token: 0x04001478 RID: 5240
	public static int[] wsF;

	// Token: 0x04001479 RID: 5241
	public static Image[] imgWS;

	// Token: 0x0400147A RID: 5242
	public static Image imgShuriken;

	// Token: 0x0400147B RID: 5243
	public static Image[][] imgDust;

	// Token: 0x0400147C RID: 5244
	public static bool isResume;

	// Token: 0x0400147D RID: 5245
	public static ServerListScreen serverScreen;

	// Token: 0x0400147E RID: 5246
	public static ServerScr serverScr;

	// Token: 0x0400147F RID: 5247
	public static SelectCharScr _SelectCharScr;

	// Token: 0x04001480 RID: 5248
	public bool resetToLoginScr;

	// Token: 0x04001481 RID: 5249
	public static long TIMEOUT;

	// Token: 0x04001482 RID: 5250
	public static int timeLoading = 15;
}
