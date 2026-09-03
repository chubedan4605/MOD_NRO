using System;
using UnityEngine;

// Token: 0x020000B5 RID: 181
public class LoginScr : mScreen, IActionListener
{
	// Token: 0x0600080A RID: 2058 RVA: 0x000729A8 File Offset: 0x00070BA8
	public LoginScr()
	{
		this.yLog = GameCanvas.hh - 30;
		TileMap.bgID = (int)((sbyte)(mSystem.currentTimeMillis() % 9L));
		if (TileMap.bgID == 5 || TileMap.bgID == 6)
		{
			TileMap.bgID = 4;
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
		Main.closeKeyBoard();
		if (GameCanvas.h > 200)
		{
			this.defYL = GameCanvas.hh - 80;
		}
		else
		{
			this.defYL = GameCanvas.hh - 65;
		}
		this.resetLogo();
		int num = (GameCanvas.w < 200) ? 140 : 160;
		this.wC = num;
		this.yt = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;
		if (GameCanvas.h <= 160)
		{
			this.yt = 20;
		}
		this.tfUser = new TField();
		this.tfUser.y = GameCanvas.hh - mScreen.ITEM_HEIGHT - 9;
		this.tfUser.width = this.wC;
		this.tfUser.height = mScreen.ITEM_HEIGHT + 2;
		this.tfUser.isFocus = true;
		this.tfUser.setIputType(TField.INPUT_TYPE_ANY);
		this.tfUser.isVietnamese = false;
		this.tfUser.name = (((int)mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
		this.tfPass = new TField();
		this.tfPass.y = GameCanvas.hh - 4;
		this.tfPass.setIputType(TField.INPUT_TYPE_PASSWORD);
		this.tfPass.isVietnamese = false;
		this.tfPass.width = this.wC;
		this.tfPass.height = mScreen.ITEM_HEIGHT + 2;
		this.yt += 35;
		this.isCheck = true;
		int num2 = Rms.loadRMSInt("check");
		if (num2 == 1)
		{
			this.isCheck = true;
		}
		else if (num2 == 2)
		{
			this.isCheck = false;
		}
		this.tfUser.setText(Rms.loadRMSString("acc"));
		this.tfPass.setText(Rms.loadRMSString("pass"));
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
		this.focus = 0;
		this.cmdLogin = new Command((GameCanvas.w <= 200) ? mResources.login2 : mResources.login, GameCanvas.instance, 888393, null);
		this.cmdCheck = new Command(mResources.remember, this, 2001, null);
		this.cmdRes = new Command(mResources.register, this, 2002, null);
		this.cmdBackFromRegister = new Command(mResources.CANCEL, this, 10021, null);
		this.cmdBack = new Command(mResources.BACK, this, 101, null);
		this.left = (this.cmdMenu = new Command(mResources.MENU, this, 2003, null));
		this.freeAreaHeight = this.tfUser.y - 2 * this.tfUser.height;
		if (GameCanvas.isTouch)
		{
			this.cmdLogin.x = GameCanvas.w / 2 + 8;
			this.cmdMenu.x = GameCanvas.w / 2 - mScreen.cmdW - 8;
			if (GameCanvas.h >= 200)
			{
				this.cmdLogin.y = this.yLog + 110;
				this.cmdMenu.y = this.yLog + 110;
			}
			this.cmdBackFromRegister.x = GameCanvas.w / 2 + 3;
			this.cmdBackFromRegister.y = this.yLog + 110;
			this.cmdRes.x = GameCanvas.w / 2 - 84;
			this.cmdRes.y = this.cmdMenu.y;
			this.cmdBack.x = 2;
			this.cmdBack.y = GameCanvas.h - mScreen.cmdH;
		}
		this.wP = 170;
		this.hP = ((!this.isRes) ? 100 : 110);
		this.xP = GameCanvas.hw - this.wP / 2;
		this.yP = this.tfUser.y - 15;
		int num4 = 4;
		int num5 = num4 * 32 + 23 + 33;
		if (num5 >= GameCanvas.w)
		{
			num4--;
			num5 = num4 * 32 + 23 + 33;
		}
		this.xLog = GameCanvas.w / 2 - num5 / 2;
		this.yLog = GameCanvas.hh - 30;
		this.lY = ((GameCanvas.w < 200) ? (this.tfUser.y - 30) : (this.yLog - 30));
		this.tfUser.x = this.xLog + 10;
		this.tfUser.y = this.yLog + 20;
		this.cmdOK = new Command(mResources.OK, this, 2008, null);
		this.cmdOK.x = GameCanvas.w / 2 - 84;
		this.cmdOK.y = this.cmdLogin.y;
		this.cmdFogetPass = new Command(mResources.forgetPass, this, 1003, null);
		this.cmdFogetPass.x = GameCanvas.w / 2 + 3;
		this.cmdFogetPass.y = this.cmdLogin.y;
		this.center = this.cmdOK;
		this.left = this.cmdFogetPass;
		this.InitAutoManager();
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x00073004 File Offset: 0x00071204
	public static void getServerLink()
	{
		try
		{
			if (!LoginScr.isTryGetIPFromWap)
			{
				Command command = new Command();
				ActionChat actionChat = delegate(string str)
				{
					try
					{
						if (str == null)
						{
							return;
						}
						if (str == string.Empty)
						{
							return;
						}
						Rms.saveIP(str);
						if (!str.Contains(":"))
						{
							return;
						}
						int num = str.IndexOf(":");
						string text = str.Substring(0, num);
						string s = str.Substring(num + 1);
						GameMidlet.IP = text;
						GameMidlet.PORT = int.Parse(s);
						Session_ME.gI().connect(text, int.Parse(s));
						LoginScr.isTryGetIPFromWap = true;
					}
					catch (Exception ex)
					{
					}
				};
				command.actionChat = actionChat;
				Net.connectHTTP(ServerListScreen.linkGetHost, command);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x00073074 File Offset: 0x00071274
	public override void switchToMe()
	{
		this.isRegistering = false;
		SoundMn.gI().stopAll();
		this.tfUser.isFocus = true;
		this.tfPass.isFocus = false;
		if (GameCanvas.isTouch)
		{
			this.tfUser.isFocus = false;
		}
		GameCanvas.loadBG(0);
		this.left = new Command(mResources.BACK, this, 101, null);
		base.switchToMe();
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x000730E0 File Offset: 0x000712E0
	public void setUserPass()
	{
		string text = Rms.loadRMSString("acc");
		if (text != null && !text.Equals(string.Empty))
		{
			this.tfUser.setText(text);
		}
		string text2 = Rms.loadRMSString("pass");
		if (text2 != null && !text2.Equals(string.Empty))
		{
			this.tfPass.setText(text2);
		}
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x000045ED File Offset: 0x000027ED
	public void updateTfWhenOpenKb()
	{
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00073148 File Offset: 0x00071348
	protected void doMenu()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(mResources.registerNewAcc, this, 2004, null));
		if (!this.isLogin2)
		{
			myVector.addElement(new Command(mResources.selectServer, this, 1004, null));
		}
		myVector.addElement(new Command(mResources.forgetPass, this, 1003, null));
		myVector.addElement(new Command(mResources.website, this, 1005, null));
		if (Main.isPC)
		{
			myVector.addElement(new Command(mResources.EXIT, GameCanvas.instance, 8885, null));
		}
		GameCanvas.menu.startAt(myVector, 0);
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x000731F4 File Offset: 0x000713F4
	protected void doRegister()
	{
		if (this.tfUser.getText().Equals(string.Empty))
		{
			GameCanvas.startOKDlg(mResources.userBlank);
			return;
		}
		char[] array = this.tfUser.getText().ToCharArray();
		if (this.tfPass.getText().Equals(string.Empty))
		{
			GameCanvas.startOKDlg(mResources.passwordBlank);
			return;
		}
		if (this.tfUser.getText().Length < 5)
		{
			GameCanvas.startOKDlg(mResources.accTooShort);
			return;
		}
		int num = 0;
		string text = null;
		if ((int)mResources.language == 2)
		{
			if (this.tfUser.getText().IndexOf("@") == -1 || this.tfUser.getText().IndexOf(".") == -1)
			{
				text = mResources.emailInvalid;
			}
			num = 0;
		}
		else
		{
			try
			{
				long num2 = long.Parse(this.tfUser.getText());
				if (this.tfUser.getText().Length < 8 || this.tfUser.getText().Length > 12 || (!this.tfUser.getText().StartsWith("0") && !this.tfUser.getText().StartsWith("84")))
				{
					text = mResources.phoneInvalid;
				}
				num = 1;
			}
			catch (Exception ex)
			{
				if (this.tfUser.getText().IndexOf("@") == -1 || this.tfUser.getText().IndexOf(".") == -1)
				{
					text = mResources.emailInvalid;
				}
				num = 0;
			}
		}
		if (text != null)
		{
			GameCanvas.startOKDlg(text);
		}
		else
		{
			GameCanvas.msgdlg.setInfo(string.Concat(new string[]
			{
				mResources.plsCheckAcc,
				(num != 1) ? (mResources.email + ": ") : (mResources.phone + ": "),
				this.tfUser.getText(),
				"\n",
				mResources.password,
				": ",
				this.tfPass.getText()
			}), new Command(mResources.ACCEPT, this, 4000, null), null, new Command(mResources.NO, GameCanvas.instance, 8882, null));
		}
		GameCanvas.currentDialog = GameCanvas.msgdlg;
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00073464 File Offset: 0x00071664
	protected void doRegister(string user)
	{
		this.isFAQ = false;
		GameCanvas.startWaitDlg(mResources.CONNECTING);
		GameCanvas.connect();
		GameCanvas.startWaitDlg(mResources.REGISTERING);
		this.passRe = this.tfPass.getText();
		Service.gI().requestRegister(user, this.tfPass.getText(), Rms.loadRMSString("userAo" + ServerListScreen.ipSelect), Rms.loadRMSString("passAo" + ServerListScreen.ipSelect), GameMidlet.VERSION);
		Rms.saveRMSString("acc", user);
		Rms.saveRMSString("pass", this.tfPass.getText());
		this.t = 20;
		this.isRegistering = true;
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00073520 File Offset: 0x00071720
	public void doViewFAQ()
	{
		if (!this.listFAQ.Equals(string.Empty) || !this.listFAQ.Equals(string.Empty))
		{
		}
		if (!Session_ME.connected)
		{
			this.isFAQ = true;
			GameCanvas.connect();
		}
		GameCanvas.startWaitDlg();
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00073574 File Offset: 0x00071774
	protected void doSelectServer()
	{
		MyVector myVector = new MyVector();
		if (LoginScr.isLocal)
		{
			myVector.addElement(new Command("Server LOCAL", this, 20004, null));
		}
		myVector.addElement(new Command("Server Bokken", this, 20001, null));
		myVector.addElement(new Command("Server Shuriken", this, 20002, null));
		myVector.addElement(new Command("Server Tessen (mới)", this, 20003, null));
		GameCanvas.menu.startAt(myVector, 0);
		if (this.loadIndexServer() != -1 && !GameCanvas.isTouch)
		{
			GameCanvas.menu.menuSelectedItem = this.loadIndexServer();
		}
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00007C4B File Offset: 0x00005E4B
	protected void saveIndexServer(int index)
	{
		Rms.saveRMSInt("indServer", index);
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x00007C58 File Offset: 0x00005E58
	protected int loadIndexServer()
	{
		return Rms.loadRMSInt("indServer");
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x00073620 File Offset: 0x00071820
	public void doLogin()
	{
		string text = Rms.loadRMSString("acc");
		string text2 = Rms.loadRMSString("pass");
		if (text != null && !text.Equals(string.Empty))
		{
			this.isLogin2 = false;
		}
		else if (Rms.loadRMSString("userAo" + ServerListScreen.ipSelect) != null && !Rms.loadRMSString("userAo" + ServerListScreen.ipSelect).Equals(string.Empty))
		{
			this.isLogin2 = true;
		}
		else
		{
			this.isLogin2 = false;
		}
		if ((text == null || text.Equals(string.Empty)) && this.isLogin2)
		{
			text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect);
			text2 = "a";
		}
		if (text == null || text2 == null || GameMidlet.VERSION == null || text.Equals(string.Empty))
		{
			return;
		}
		if (text2.Equals(string.Empty))
		{
			this.focus = 1;
			this.tfUser.isFocus = false;
			this.tfPass.isFocus = true;
			if (!GameCanvas.isTouch)
			{
				this.right = this.tfPass.cmdClear;
			}
			return;
		}
		if (!Session_ME.gI().isConnected())
		{
			GameCanvas.connect();
		}
		Service.gI().login(text, text2, GameMidlet.VERSION, (!this.isLogin2) ? 0 : 1);
		Res.outz(string.Concat(new object[]
		{
			Controller.isEXTRA_LINK,
			" = Controller.isEXTRA_LINK ",
			text,
			" ",
			text2,
			" ",
			GameMidlet.VERSION,
			" ",
			(!this.isLogin2) ? 0 : 1
		}));
		Rms.saveRMSInt(ServerListScreen.RMS_svselect, ServerListScreen.ipSelect);
		if (Session_ME.connected)
		{
			GameCanvas.startWaitDlg();
		}
		else
		{
			GameCanvas.startOK(mResources.maychutathoacmatsong + " [0]", 8884, null);
		}
		this.focus = 0;
		if (!this.isLogin2)
		{
			this.actRegisterLeft();
		}
		GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x00073878 File Offset: 0x00071A78
	public void savePass()
	{
		if (AutoManagerData.IsAutoSaveEnabled())
		{
			AutoManagerData.UpsertAccount(this.tfUser.getText().Trim(), this.tfPass.getText().Trim());
		}
		if (this.isCheck)
		{
			Rms.saveRMSInt("check", 1);
			Rms.saveRMSString("acc", this.tfUser.getText().ToLower().Trim());
			Rms.saveRMSString("pass", this.tfPass.getText().ToLower().Trim());
		}
		else
		{
			Rms.saveRMSInt("check", 2);
			Rms.saveRMSString("acc", string.Empty);
			Rms.saveRMSString("pass", string.Empty);
		}
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x00073908 File Offset: 0x00071B08
	public override void update()
	{
		if (LoginScr.timeLogin > 0)
		{
			GameCanvas.startWaitDlg();
			LoginScr.currTimeLogin = mSystem.currentTimeMillis();
			if (LoginScr.currTimeLogin - LoginScr.lastTimeLogin >= 1000L)
			{
				LoginScr.timeLogin -= 1;
				if (LoginScr.timeLogin == 0)
				{
					GameCanvas.loginScr.doLogin();
				}
				LoginScr.lastTimeLogin = LoginScr.currTimeLogin;
			}
		}
		if (this.isLogin2 && !this.isRes)
		{
			this.tfUser.name = (((int)mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
			this.tfPass.name = mResources.password;
			this.tfUser.isPaintCarret = false;
			this.tfPass.isPaintCarret = false;
			this.tfUser.update();
			this.tfPass.update();
		}
		else
		{
			this.tfUser.name = (((int)mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
			this.tfPass.name = mResources.password;
			this.tfUser.update();
			this.tfPass.update();
		}
		if (global::TouchScreenKeyboard.visible)
		{
			mGraphics.addYWhenOpenKeyBoard = 50;
		}
		for (int i = 0; i < Effect2.vEffect2.size(); i++)
		{
			Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
			effect.update();
		}
		if (LoginScr.isUpdateAll && !LoginScr.isUpdateData && !LoginScr.isUpdateItem && !LoginScr.isUpdateMap && !LoginScr.isUpdateSkill)
		{
			LoginScr.isUpdateAll = false;
			mSystem.gcc();
			Service.gI().finishUpdate();
		}
		GameScr.cmx++;
		if (GameScr.cmx > GameCanvas.w * 3 + 100)
		{
			GameScr.cmx = 100;
		}
		if (ChatPopup.currChatPopup != null)
		{
			return;
		}
		GameCanvas.debug("LGU1", 0);
		GameCanvas.debug("LGU2", 0);
		GameCanvas.debug("LGU3", 0);
		this.updateLogo();
		GameCanvas.debug("LGU4", 0);
		GameCanvas.debug("LGU5", 0);
		if (this.g >= 0)
		{
			this.ylogo += this.dir * this.g;
			this.g += this.dir * this.v;
			if (this.g <= 0)
			{
				this.dir *= -1;
			}
			if (this.ylogo > 0)
			{
				this.dir *= -1;
				this.g -= 2 * this.v;
			}
		}
		GameCanvas.debug("LGU6", 0);
		if (this.tipid >= 0 && GameCanvas.gameTick % 100 == 0)
		{
			this.doChangeTip();
		}
		if (this.isLogin2 && !this.isRes)
		{
			this.tfUser.isPaintCarret = false;
			this.tfPass.isPaintCarret = false;
			this.tfUser.update();
			this.tfPass.update();
		}
		else
		{
			this.tfUser.name = (((int)mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
			this.tfPass.name = mResources.password;
			this.tfUser.update();
			this.tfPass.update();
		}
		if (GameCanvas.isTouch)
		{
			if (this.isRes)
			{
				this.center = this.cmdRes;
				this.left = this.cmdBackFromRegister;
			}
			else
			{
				this.center = this.cmdOK;
				this.left = this.cmdFogetPass;
			}
			if (this.cmdBack != null && this.cmdBack.isPointerPressInside())
			{
				this.cmdBack.performAction();
			}
		}
		else if (this.isRes)
		{
			this.center = this.cmdRes;
			this.left = this.cmdBackFromRegister;
		}
		else
		{
			this.center = this.cmdOK;
			this.left = this.cmdFogetPass;
		}
		if (!Main.isPC && !global::TouchScreenKeyboard.visible && !Main.isMiniApp && !Main.isWindowsPhone)
		{
			string text = this.tfUser.getText().ToLower().Trim();
			string text2 = this.tfPass.getText().ToLower().Trim();
			if (!text.Equals(string.Empty) && !text2.Equals(string.Empty))
			{
				this.doLogin();
			}
			Main.isMiniApp = true;
		}
		this.updateTfWhenOpenKb();
	}

	private void InitAutoManager()
	{
		this.cmdAutoManager = new Command("Auto Manager", this, 17001, null);
		this.cmdAutoManager.w = ((GameCanvas.w < 220) ? 92 : 102);
		this.cmdAutoManager.h = 30;
		this.cmdAutoManagerSave = new Command("Lưu TK", this, 17003, null);
		this.cmdAutoManagerSave.w = 84;
		this.cmdAutoManagerPrevPage = new Command("<", this, 17004, null);
		this.cmdAutoManagerPrevPage.w = 34;
		this.cmdAutoManagerNextPage = new Command(">", this, 17005, null);
		this.cmdAutoManagerNextPage.w = 34;
		this.cmdAutoManagerToggleAutoSave = new Command(string.Empty, this, 17006, null);
		this.cmdAutoManagerToggleAutoSave.w = 94;
		this.cmdAutoManagerToggleRemember = new Command(string.Empty, this, 17007, null);
		this.cmdAutoManagerToggleRemember.w = 94;
		this.autoManagerDelete = new Command[6];
		for (int i = 0; i < this.autoManagerDelete.Length; i++)
		{
			this.autoManagerDelete[i] = new Command("Xóa", this, 17100 + i, null);
			this.autoManagerDelete[i].w = 60;
		}
		this.RefreshAutoManager();
	}

	private void RefreshAutoManager()
	{
		this.autoManagerAccounts = AutoManagerData.LoadAccounts();
		this.autoManagerAutoSave = AutoManagerData.IsAutoSaveEnabled();
		this.autoManagerRememberLogin = this.isCheck;
		this.ClampAutoManagerPage();
	}

	private void OpenAutoManager()
	{
		this.RefreshAutoManager();
		this.isAutoManagerOpen = true;
		this.tfUser.isFocus = false;
		this.tfPass.isFocus = false;
		GameCanvas.clearAllPointerEvent();
	}

	public void ShowAutoManager()
	{
		this.switchToMe();
		this.OpenAutoManager();
	}

	private void CloseAutoManager(bool focusUserField)
	{
		this.isAutoManagerOpen = false;
		this.focus = (focusUserField ? 0 : -1);
		this.tfUser.isFocus = focusUserField;
		this.tfPass.isFocus = false;
		if (!focusUserField)
		{
			GameCanvas.closeKeyBoard();
		}
		GameCanvas.clearAllPointerEvent();
	}

	private void ClampAutoManagerPage()
	{
		int num = this.GetAutoManagerPageCount();
		if (num <= 0)
		{
			this.autoManagerPage = 0;
			return;
		}
		if (this.autoManagerPage >= num)
		{
			this.autoManagerPage = num - 1;
		}
		if (this.autoManagerPage < 0)
		{
			this.autoManagerPage = 0;
		}
	}

	private int GetAutoManagerPageSize()
	{
		int num = 4;
		if (num > this.autoManagerDelete.Length)
		{
			num = this.autoManagerDelete.Length;
		}
		return num;
	}

	private int GetAutoManagerPageCount()
	{
		int num = this.GetAutoManagerPageSize();
		if (num <= 0 || this.autoManagerAccounts == null || this.autoManagerAccounts.size() == 0)
		{
			return 1;
		}
		int num2 = this.autoManagerAccounts.size() / num;
		if (this.autoManagerAccounts.size() % num != 0)
		{
			num2++;
		}
		return (num2 > 0) ? num2 : 1;
	}

	private int GetAutoManagerListStartY()
	{
		return this.autoManagerPopupY + 80;
	}

	private int GetAutoManagerItemHeight()
	{
		int num = this.GetAutoManagerPageSize();
		if (num <= 0)
		{
			return 30;
		}
		int num2 = this.cmdAutoManagerSave.y - 8 - this.GetAutoManagerListStartY();
		int num3 = num2 / num;
		if (num3 < 24)
		{
			num3 = 24;
		}
		if (num3 > 30)
		{
			num3 = 30;
		}
		return num3;
	}

	private void UpdateAutoManagerLayout()
	{
		this.cmdAutoManager.w = ((GameCanvas.w < 220) ? 84 : 102);
		int num = 2;
		if (this.cmdBack != null)
		{
			num = this.cmdBack.x;
		}
		this.cmdAutoManager.x = GameCanvas.w - this.cmdAutoManager.w - num;
		if (this.cmdBack != null)
		{
			this.cmdAutoManager.y = this.cmdBack.y + 3;
		}
		else
		{
			this.cmdAutoManager.y = GameCanvas.h - this.cmdAutoManager.h - 2;
		}
		if (this.cmdAutoManager.y + this.cmdAutoManager.h > GameCanvas.h - 2)
		{
			this.cmdAutoManager.y = GameCanvas.h - this.cmdAutoManager.h - 2;
		}
		if (this.cmdAutoManager.y < 2)
		{
			this.cmdAutoManager.y = 2;
		}
		int num2 = GameCanvas.w - 18;
		if (num2 > 330)
		{
			num2 = 330;
		}
		if (num2 < 220)
		{
			num2 = GameCanvas.w - 8;
		}
		int num3 = GameCanvas.h - 36;
		if (num3 > 240)
		{
			num3 = 240;
		}
		if (num3 < 150)
		{
			num3 = GameCanvas.h - 10;
		}
		this.autoManagerPopupW = num2;
		this.autoManagerPopupH = num3;
		this.autoManagerPopupX = GameCanvas.hw - num2 / 2;
		this.autoManagerPopupY = GameCanvas.hh - num3 / 2;
		int num4 = this.autoManagerPopupX + 12;
		this.cmdAutoManagerSave.x = num4;
		this.cmdAutoManagerSave.y = this.autoManagerPopupY + this.autoManagerPopupH - this.cmdAutoManagerSave.h - 12;
		this.cmdAutoManagerPrevPage.x = this.autoManagerPopupX + this.autoManagerPopupW - 86;
		this.cmdAutoManagerPrevPage.y = this.cmdAutoManagerSave.y;
		this.cmdAutoManagerNextPage.x = this.autoManagerPopupX + this.autoManagerPopupW - 44;
		this.cmdAutoManagerNextPage.y = this.cmdAutoManagerSave.y;
		this.cmdAutoManagerToggleAutoSave.caption = this.autoManagerAutoSave ? "Đang bật" : "Đang tắt";
		this.cmdAutoManagerToggleAutoSave.w = 96;
		this.cmdAutoManagerToggleAutoSave.x = this.autoManagerPopupX + this.autoManagerPopupW - this.cmdAutoManagerToggleAutoSave.w - 16;
		this.cmdAutoManagerToggleAutoSave.y = this.autoManagerPopupY + 88;
		this.cmdAutoManagerToggleRemember.caption = this.autoManagerRememberLogin ? "Đang bật" : "Đang tắt";
		this.cmdAutoManagerToggleRemember.w = 96;
		this.cmdAutoManagerToggleRemember.x = this.autoManagerPopupX + this.autoManagerPopupW - this.cmdAutoManagerToggleRemember.w - 16;
		this.cmdAutoManagerToggleRemember.y = this.autoManagerPopupY + 134;
	}

	private void HandleAutoManagerPointer()
	{
		if (GameCanvas.isPointerJustRelease && !GameCanvas.isPointerHoldIn(this.autoManagerPopupX, this.autoManagerPopupY, this.autoManagerPopupW, this.autoManagerPopupH))
		{
			this.CloseAutoManager(true);
			return;
		}
		int num = this.autoManagerPopupY + 44;
		int num2 = this.autoManagerPopupW / 2 - 14;
		if (GameCanvas.isPointerHoldIn(this.autoManagerPopupX + 10, num, num2, 26) && GameCanvas.isPointerJustRelease)
		{
			this.autoManagerTab = 0;
			GameCanvas.clearAllPointerEvent();
			return;
		}
		if (GameCanvas.isPointerHoldIn(this.autoManagerPopupX + this.autoManagerPopupW / 2 + 4, num, num2, 26) && GameCanvas.isPointerJustRelease)
		{
			this.autoManagerTab = 1;
			GameCanvas.clearAllPointerEvent();
			return;
		}
		if (this.autoManagerTab == 0)
		{
			if (this.cmdAutoManagerSave.isPointerPressInside())
			{
				this.cmdAutoManagerSave.performAction();
				return;
			}
			if (this.GetAutoManagerPageCount() > 1)
			{
				if (this.cmdAutoManagerPrevPage.isPointerPressInside())
				{
					this.cmdAutoManagerPrevPage.performAction();
					return;
				}
				if (this.cmdAutoManagerNextPage.isPointerPressInside())
				{
					this.cmdAutoManagerNextPage.performAction();
					return;
				}
			}
			int pageSize = this.GetAutoManagerPageSize();
			int startIndex = this.autoManagerPage * pageSize;
			int startY = this.GetAutoManagerListStartY();
			int itemHeight = this.GetAutoManagerItemHeight();
			int itemWidth = this.autoManagerPopupW - 100;
			int deleteW = 50;
			int deleteH = itemHeight - 4;
			for (int i = 0; i < pageSize; i++)
			{
				int index = startIndex + i;
				if (index >= this.autoManagerAccounts.size())
				{
					break;
				}
				int y = startY + i * itemHeight;
				this.autoManagerDelete[i].w = deleteW;
				this.autoManagerDelete[i].h = deleteH;
				this.autoManagerDelete[i].x = this.autoManagerPopupX + this.autoManagerPopupW - deleteW - 10;
				this.autoManagerDelete[i].y = y + 2;
				if (this.autoManagerDelete[i].isPointerPressInside())
				{
					this.autoManagerDelete[i].performAction();
					return;
				}
				if (GameCanvas.isPointerHoldIn(this.autoManagerPopupX + 12, y, itemWidth, itemHeight) && GameCanvas.isPointerJustRelease)
				{
					this.SelectAutoManagerAccount(index);
					return;
				}
			}
		}
		else
		{
			if (this.cmdAutoManagerToggleAutoSave.isPointerPressInside())
			{
				this.cmdAutoManagerToggleAutoSave.performAction();
				return;
			}
			if (this.cmdAutoManagerToggleRemember.isPointerPressInside())
			{
				this.cmdAutoManagerToggleRemember.performAction();
			}
		}
	}

	private void SelectAutoManagerAccount(int index)
	{
		AutoManagerAccount autoManagerAccount = (AutoManagerAccount)this.autoManagerAccounts.elementAt(index);
		if (autoManagerAccount == null)
		{
			return;
		}
		this.tfUser.setText(autoManagerAccount.Username);
		this.tfPass.setText(autoManagerAccount.Password);
		Rms.saveRMSString("acc", autoManagerAccount.Username);
		Rms.saveRMSString("pass", autoManagerAccount.Password);
		AutoManagerData.UpsertAccount(autoManagerAccount.Username, autoManagerAccount.Password);
		this.CloseAutoManager(false);
		this.isLogin2 = false;
		if (GameCanvas.serverScreen == null)
		{
			GameCanvas.serverScreen = new ServerListScreen();
		}
		GameCanvas.serverScreen.Login_New();
	}

	private void SaveCurrentAccountToAutoManager()
	{
		string text = this.tfUser.getText().Trim();
		string text2 = this.tfPass.getText().Trim();
		if (text.Length == 0)
		{
			GameCanvas.startOKDlg(mResources.userBlank);
			return;
		}
		if (text2.Length == 0)
		{
			GameCanvas.startOKDlg(mResources.passwordBlank);
			return;
		}
		AutoManagerData.UpsertAccount(text, text2);
		this.RefreshAutoManager();
		GameCanvas.startOKDlg("Đã lưu tài khoản");
	}

	private void DeleteAutoManagerAccount(int index)
	{
		int num = this.autoManagerPage * this.GetAutoManagerPageSize() + index;
		AutoManagerData.RemoveAt(num);
		this.RefreshAutoManager();
	}

	private void ToggleRememberLogin()
	{
		this.isCheck = !this.isCheck;
		this.autoManagerRememberLogin = this.isCheck;
		Rms.saveRMSInt("check", this.isCheck ? 1 : 2);
		if (this.isCheck)
		{
			Rms.saveRMSString("acc", this.tfUser.getText().Trim());
			Rms.saveRMSString("pass", this.tfPass.getText().Trim());
			return;
		}
		Rms.saveRMSString("acc", string.Empty);
		Rms.saveRMSString("pass", string.Empty);
	}

	private string GetAutoManagerText(string text, int width)
	{
		if (text == null)
		{
			return string.Empty;
		}
		if (mFont.tahoma_7b_dark.getWidth(text) <= width)
		{
			return text;
		}
		string text2 = text;
		while (text2.Length > 0 && mFont.tahoma_7b_dark.getWidth(text2 + "...") > width)
		{
			text2 = text2.Substring(0, text2.Length - 1);
		}
		return text2 + "...";
	}

	private void PaintAutoManagerTab(mGraphics g, string title, int x, int y, bool isActive)
	{
		int w = this.autoManagerPopupW / 2 - 14;
		int h = 28;
		int r = 20;
		if (isActive)
		{
			g.setColor(16164410);
			g.fillRoundRect(x, y, w, h, r, r);
			g.setColor(14715422);
			g.drawRoundRect(x, y, w, h, r, r);
			g.setColor(16765567);
			g.fillRect(x + 6, y + 3, w - 12, 3);
			mFont.tahoma_7b_dark.drawString(g, title, x + w / 2, y + (h - 10) / 2, 2);
			return;
		}
		g.setColor(13215610);
		g.fillRoundRect(x, y, w, h, r, r);
		g.setColor(10583637);
		g.drawRoundRect(x, y, w, h, r, r);
		mFont.tahoma_7b_dark.drawString(g, title, x + w / 2, y + (h - 10) / 2, 2);
	}
	private void PaintAutoManager(mGraphics g)
	{
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h, 0, 90);
		PopUp.paintPopUp(g, this.autoManagerPopupX, this.autoManagerPopupY, this.autoManagerPopupW, this.autoManagerPopupH, -1, true);
		g.setColor(13215610);
		g.fillRect(this.autoManagerPopupX + 4, this.autoManagerPopupY + 4, this.autoManagerPopupW - 8, 30);
		mFont.tahoma_7b_yellow.drawString(g, "Auto Manager", this.autoManagerPopupX + this.autoManagerPopupW / 2, this.autoManagerPopupY + 5 + (30 - mFont.tahoma_7b_yellow.getHeight()) / 2, 2);
		this.PaintAutoManagerTab(g, "Tài khoản", this.autoManagerPopupX + 10, this.autoManagerPopupY + 42, this.autoManagerTab == 0);
		this.PaintAutoManagerTab(g, "Điều khiển", this.autoManagerPopupX + this.autoManagerPopupW / 2 + 4, this.autoManagerPopupY + 42, this.autoManagerTab == 1);
		if (this.autoManagerTab == 0)
		{
			int pageSize = this.GetAutoManagerPageSize();
			int startIndex = this.autoManagerPage * pageSize;
			int startY = this.GetAutoManagerListStartY();
			int itemHeight = this.GetAutoManagerItemHeight();
			if (this.autoManagerAccounts.size() == 0)
			{
				mFont.tahoma_7b_dark.drawString(g, "Chưa có tài khoản được lưu", this.autoManagerPopupX + this.autoManagerPopupW / 2, this.autoManagerPopupY + 110, 2);
			}
			for (int i = 0; i < pageSize; i++)
			{
				int index = startIndex + i;
				if (index >= this.autoManagerAccounts.size())
				{
					break;
				}
				AutoManagerAccount acc = (AutoManagerAccount)this.autoManagerAccounts.elementAt(index);
				int y = startY + i * itemHeight;
				int itemWidth = this.autoManagerPopupW - 100;
				Command.paintOngMau(Command.btn0left, Command.btn0mid, Command.btn0right, this.autoManagerPopupX + 12, y, itemWidth, g);
				string text = index + 1 + ". " + acc.Username;
				mFont.tahoma_7b_dark.drawString(g, this.GetAutoManagerText(text, itemWidth - 20), this.autoManagerPopupX + 20, y + (itemHeight - mFont.tahoma_7b_dark.getHeight()) / 2, 0);
				int deleteW = 50;
				int deleteH = itemHeight - 4;
				this.autoManagerDelete[i].w = deleteW;
				this.autoManagerDelete[i].h = deleteH;
				this.autoManagerDelete[i].x = this.autoManagerPopupX + this.autoManagerPopupW - deleteW - 10;
				this.autoManagerDelete[i].y = y + 2;
				this.autoManagerDelete[i].paint(g);
			}
			this.cmdAutoManagerSave.paint(g);
			if (this.GetAutoManagerPageCount() > 1)
			{
				this.cmdAutoManagerPrevPage.paint(g);
				this.cmdAutoManagerNextPage.paint(g);
				mFont.tahoma_7b_dark.drawString(g, this.autoManagerPage + 1 + "/" + this.GetAutoManagerPageCount(), this.autoManagerPopupX + this.autoManagerPopupW - 128, this.cmdAutoManagerSave.y + 8, 0);
				return;
			}
		}
		else
		{
			mFont.tahoma_7b_dark.drawString(g, "Tự lưu sau đăng nhập", this.autoManagerPopupX + 16, this.autoManagerPopupY + 96, 0);
			mFont.tahoma_7b_dark.drawString(g, "Ghi nhớ ở đăng nhập", this.autoManagerPopupX + 16, this.autoManagerPopupY + 142, 0);
			this.cmdAutoManagerToggleAutoSave.paint(g);
			this.cmdAutoManagerToggleRemember.paint(g);
			mFont.tahoma_7_grey.drawString(g, "Bấm dòng tài khoản để nạp nhanh TK/MK.", this.autoManagerPopupX + 16, this.autoManagerPopupY + 194, 0);
			mFont.tahoma_7_grey.drawString(g, "Nút Lưu TK sẽ thêm cặp đăng nhập hiện tại.", this.autoManagerPopupX + 16, this.autoManagerPopupY + 208, 0);
		}
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x00007EC5 File Offset: 0x000060C5
	public void ShowAutoManager()
	{
		this.switchToMe();
		this.OpenAutoManager();
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x00073DF4 File Offset: 0x00071FF4
	private void doChangeTip()
	{
		this.tipid++;
		if (this.tipid >= mResources.tips.Length)
		{
			this.tipid = 0;
		}
		if (GameCanvas.currentDialog == GameCanvas.msgdlg && GameCanvas.msgdlg.isWait)
		{
			GameCanvas.msgdlg.setInfo(mResources.tips[this.tipid]);
		}
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x00007C64 File Offset: 0x00005E64
	public void updateLogo()
	{
		if (this.defYL != this.yL)
		{
			this.yL += this.defYL - this.yL >> 1;
		}
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x00073E5C File Offset: 0x0007205C
	public override void keyPress(int keyCode)
	{
		if (this.tfUser.isFocus)
		{
			this.tfUser.keyPressed(keyCode);
		}
		else if (this.tfPass.isFocus)
		{
			this.tfPass.keyPressed(keyCode);
		}
		base.keyPress(keyCode);
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x00007C93 File Offset: 0x00005E93
	public override void unLoad()
	{
		base.unLoad();
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x00073EB0 File Offset: 0x000720B0
	public override void paint(mGraphics g)
	{
		this.UpdateAutoManagerLayout();
		GameCanvas.debug("PLG1", 1);
		GameCanvas.paintBGGameScr(g);
		GameCanvas.debug("PLG2", 2);
		int num = this.tfUser.y - 50;
		if (GameCanvas.h <= 220)
		{
			num += 5;
		}
		mFont.tahoma_7_white.drawString(g, "v" + GameMidlet.VERSION, GameCanvas.w - 2, 17, 1, mFont.tahoma_7_grey);
		if (mSystem.clientType == 1 && !GameCanvas.isTouch)
		{
			mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
		}
		else
		{
			mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, 2, 1, mFont.tahoma_7_grey);
		}
		if (GameCanvas.currentDialog == null)
		{
			int h = 105;
			int w = (GameCanvas.w < 200) ? 160 : 180;
			PopUp.paintPopUp(g, this.xLog, this.yLog - 10, w, h, -1, true);
			if (GameCanvas.h > 160 && LoginScr.imgTitle != null)
			{
				g.drawImage(LoginScr.imgTitle, GameCanvas.hw, num, 3);
			}
			GameCanvas.debug("PLG4", 1);
			int num2 = 4;
			int num3 = num2 * 32 + 23 + 33;
			if (num3 >= GameCanvas.w)
			{
				num2--;
				num3 = num2 * 32 + 23 + 33;
			}
			this.xLog = GameCanvas.w / 2 - num3 / 2;
			this.tfUser.x = this.xLog + 10;
			this.tfUser.y = this.yLog + 20;
			this.tfPass.x = this.xLog + 10;
			this.tfPass.y = this.yLog + 55;
			this.tfUser.paint(g);
			this.tfPass.paint(g);
			if (GameCanvas.w < 176)
			{
				mFont.tahoma_7b_green2.drawString(g, mResources.acc + ":", this.tfUser.x - 35, this.tfUser.y + 7, 0);
				mFont.tahoma_7b_green2.drawString(g, mResources.pwd + ":", this.tfPass.x - 35, this.tfPass.y + 7, 0);
				mFont.tahoma_7b_green2.drawString(g, mResources.server + ":" + LoginScr.serverName, GameCanvas.w / 2, this.tfPass.y + 32, 2);
			}
		}
		base.paint(g);
		if (!this.isAutoManagerOpen)
		{
			this.cmdBack.paint(g);
			if (GameCanvas.currentDialog == null)
			{
				this.cmdAutoManager.paint(g);
			}
			return;
		}
		this.PaintAutoManager(g);
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x00074174 File Offset: 0x00072374
	public override void updateKey()
	{
		this.UpdateAutoManagerLayout();
		if (this.isAutoManagerOpen)
		{
			this.HandleAutoManagerPointer();
			if (GameCanvas.keyPressed[12])
			{
				GameCanvas.keyPressed[12] = false;
				this.CloseAutoManager(true);
			}
			GameCanvas.clearKeyPressed();
			return;
		}
		if (this.cmdAutoManager.isPointerPressInside())
		{
			this.cmdAutoManager.performAction();
			return;
		}
		if (GameCanvas.isTouch)
		{
			if (this.cmdCallHotline != null && this.cmdCallHotline.isPointerPressInside())
			{
				this.cmdCallHotline.performAction();
			}
		}
		else if (mSystem.clientType == 1 && GameCanvas.keyPressed[13])
		{
			GameCanvas.keyPressed[13] = false;
			this.cmdCallHotline.performAction();
		}
		if (LoginScr.isContinueToLogin)
		{
			return;
		}
		if (!GameCanvas.isTouch)
		{
			if (this.tfUser.isFocus)
			{
				this.right = this.tfUser.cmdClear;
			}
			else
			{
				this.right = this.tfPass.cmdClear;
			}
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
		{
			this.focus--;
			if (this.focus < 0)
			{
				this.focus = 1;
			}
		}
		else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[16])
		{
			this.focus++;
			if (this.focus > 1)
			{
				this.focus = 0;
			}
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[16])
		{
			GameCanvas.clearKeyPressed();
			if (!this.isLogin2 || this.isRes)
			{
				if (this.focus == 1)
				{
					this.tfUser.isFocus = false;
					this.tfPass.isFocus = true;
				}
				else if (this.focus == 0)
				{
					this.tfUser.isFocus = true;
					this.tfPass.isFocus = false;
				}
				else
				{
					this.tfUser.isFocus = false;
					this.tfPass.isFocus = false;
				}
			}
		}
		if (GameCanvas.isTouch)
		{
			if (this.isRes)
			{
				this.center = this.cmdRes;
				this.left = this.cmdBackFromRegister;
			}
			else
			{
				this.center = this.cmdOK;
				this.left = this.cmdFogetPass;
			}
		}
		else if (this.isRes)
		{
			this.center = this.cmdRes;
			this.left = this.cmdBackFromRegister;
		}
		else
		{
			this.center = this.cmdOK;
			this.left = this.cmdFogetPass;
		}
		if (GameCanvas.isPointerJustRelease)
		{
			if (!this.isLogin2 || this.isRes)
			{
				if (GameCanvas.isPointerHoldIn(this.tfUser.x, this.tfUser.y, this.tfUser.width, this.tfUser.height))
				{
					this.focus = 0;
				}
				else if (GameCanvas.isPointerHoldIn(this.tfPass.x, this.tfPass.y, this.tfPass.width, this.tfPass.height))
				{
					this.focus = 1;
				}
			}
		}
		if (Main.isPC && GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] && this.right != null)
		{
			this.right.performAction();
		}
		base.updateKey();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00007C9B File Offset: 0x00005E9B
	public void resetLogo()
	{
		this.yL = -50;
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x00074508 File Offset: 0x00072708
	public void perform(int idAction, object p)
	{
		Debug.LogError(">>>>Loginscr perform: " + idAction);
		switch (idAction)
		{
		case 1000:
			try
			{
				GameMidlet.instance.platformRequest((string)p);
			}
			catch (Exception ex)
			{
			}
			GameCanvas.endDlg();
			break;
		case 1001:
			GameCanvas.endDlg();
			this.isRes = false;
			break;
		case 1002:
		{
			GameCanvas.startWaitDlg();
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
			break;
		}
		case 1003:
			GameCanvas.startOKDlg(mResources.goToWebForPassword);
			break;
		case 1004:
			ServerListScreen.doUpdateServer();
			GameCanvas.serverScreen.switchToMe();
			break;
		case 1005:
			try
			{
				GameMidlet.instance.platformRequest("http://ngocrongonline.com");
			}
			catch (Exception ex2)
			{
			}
			break;
		default:
			switch (idAction)
			{
			case 2000:
				break;
			case 2001:
				this.ToggleRememberLogin();
				break;
			case 2002:
				this.doRegister();
				break;
			case 2003:
				this.doMenu();
				break;
			case 2004:
				this.actRegister();
				break;
			default:
				if (idAction != 10041)
				{
					if (idAction != 10042)
					{
						if (idAction != 13)
						{
							if (idAction != 101)
							{
								if (idAction != 4000)
								{
									if (idAction == 10021)
									{
										this.actRegisterLeft();
									}
								}
								else
								{
									this.doRegister(this.tfUser.getText());
								}
							}
							else
							{
								GameCanvas.serverScreen.switchToMe();
							}
						}
						else
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
					}
					else
					{
						Rms.saveRMSInt("lowGraphic", 1);
						GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
					}
				}
				else
				{
					Rms.saveRMSInt("lowGraphic", 0);
					GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
				}
				break;
			case 2008:
				Rms.saveRMSString("acc", this.tfUser.getText().Trim());
				Rms.saveRMSString("pass", this.tfPass.getText().Trim());
				if (ServerListScreen.isNewUI)
				{
					Controller.isEXTRA_LINK = false;
					GameCanvas.serverScreen.Login_New();
				}
				else if (ServerListScreen.loadScreen)
				{
					GameCanvas.serverScreen.switchToMe();
				}
				else
				{
					GameCanvas.serverScreen.show2();
				}
				break;
			case 17001:
				this.OpenAutoManager();
				break;
			case 17003:
				this.SaveCurrentAccountToAutoManager();
				break;
			case 17004:
				this.autoManagerPage--;
				this.ClampAutoManagerPage();
				GameCanvas.clearAllPointerEvent();
				break;
			case 17005:
				this.autoManagerPage++;
				this.ClampAutoManagerPage();
				GameCanvas.clearAllPointerEvent();
				break;
			case 17006:
				this.autoManagerAutoSave = !this.autoManagerAutoSave;
				AutoManagerData.SetAutoSaveEnabled(this.autoManagerAutoSave);
				GameCanvas.clearAllPointerEvent();
				break;
			case 17007:
				this.ToggleRememberLogin();
				GameCanvas.clearAllPointerEvent();
				break;
			default:
				if (idAction >= 17100 && idAction < 17100 + this.autoManagerDelete.Length)
				{
					this.DeleteAutoManagerAccount(idAction - 17100);
					GameCanvas.clearAllPointerEvent();
				}
				break;
			}
			break;
		}
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x00007CA5 File Offset: 0x00005EA5
	public void actRegisterLeft()
	{
		if (this.isLogin2)
		{
			this.doLogin();
			return;
		}
		this.isRes = false;
		this.tfPass.isFocus = false;
		this.tfUser.isFocus = true;
		this.left = this.cmdMenu;
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x00007CE4 File Offset: 0x00005EE4
	public void actRegister()
	{
		GameCanvas.endDlg();
		this.isRes = true;
		this.tfPass.isFocus = false;
		this.tfUser.isFocus = true;
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x00074860 File Offset: 0x00072A60
	public void backToRegister()
	{
		GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
		ServerListScreen.countDieConnect = 0;
		if (GameCanvas.loginScr.isLogin2)
		{
			GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, GameCanvas.panel, 10019, null), new Command(mResources.NO, GameCanvas.panel, 10020, null));
			return;
		}
		if (Main.isWindowsPhone)
		{
			GameMidlet.isBackWindowsPhone = true;
		}
		GameCanvas.instance.resetToLoginScr = false;
		ServerListScreen.isAutoLogin = false;
		ServerScr.isShowSv_HaveChar = false;
		GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
	}

	// Token: 0x04000F5F RID: 3935
	public TField tfUser;

	// Token: 0x04000F60 RID: 3936
	public TField tfPass;

	// Token: 0x04000F61 RID: 3937
	public static bool isContinueToLogin = false;

	// Token: 0x04000F62 RID: 3938
	private int focus;

	// Token: 0x04000F63 RID: 3939
	private int wC;

	// Token: 0x04000F64 RID: 3940
	private int yL;

	// Token: 0x04000F65 RID: 3941
	private int defYL;

	// Token: 0x04000F66 RID: 3942
	public bool isCheck;

	// Token: 0x04000F67 RID: 3943
	public bool isRes;

	// Token: 0x04000F68 RID: 3944
	public Command cmdLogin;

	// Token: 0x04000F69 RID: 3945
	public Command cmdCheck;

	// Token: 0x04000F6A RID: 3946
	public Command cmdFogetPass;

	// Token: 0x04000F6B RID: 3947
	public Command cmdRes;

	// Token: 0x04000F6C RID: 3948
	public Command cmdMenu;

	// Token: 0x04000F6D RID: 3949
	public Command cmdBackFromRegister;

	// Token: 0x04000F6E RID: 3950
	public Command cmdBack;

	// Token: 0x04000F6F RID: 3951
	public string listFAQ = string.Empty;

	// Token: 0x04000F70 RID: 3952
	public string titleFAQ;

	// Token: 0x04000F71 RID: 3953
	public string subtitleFAQ;

	// Token: 0x04000F72 RID: 3954
	private string numSupport = string.Empty;

	// Token: 0x04000F73 RID: 3955
	public static bool isLocal = false;

	// Token: 0x04000F74 RID: 3956
	public static bool isUpdateAll;

	// Token: 0x04000F75 RID: 3957
	public static bool isUpdateData;

	// Token: 0x04000F76 RID: 3958
	public static bool isUpdateMap;

	// Token: 0x04000F77 RID: 3959
	public static bool isUpdateSkill;

	// Token: 0x04000F78 RID: 3960
	public static bool isUpdateItem;

	// Token: 0x04000F79 RID: 3961
	public static string serverName;

	// Token: 0x04000F7A RID: 3962
	public static Image imgTitle;

	// Token: 0x04000F7B RID: 3963
	public int plX;

	// Token: 0x04000F7C RID: 3964
	public int plY;

	// Token: 0x04000F7D RID: 3965
	public int lY;

	// Token: 0x04000F7E RID: 3966
	public int lX;

	// Token: 0x04000F7F RID: 3967
	public int logoDes;

	// Token: 0x04000F80 RID: 3968
	public int lineX;

	// Token: 0x04000F81 RID: 3969
	public int lineY;

	// Token: 0x04000F82 RID: 3970
	public static int[] bgId = new int[]
	{
		0,
		8,
		2,
		6,
		9
	};

	// Token: 0x04000F83 RID: 3971
	public static bool isTryGetIPFromWap;

	// Token: 0x04000F84 RID: 3972
	public static short timeLogin;

	// Token: 0x04000F85 RID: 3973
	public static long lastTimeLogin;

	// Token: 0x04000F86 RID: 3974
	public static long currTimeLogin;

	// Token: 0x04000F87 RID: 3975
	private int yt;

	// Token: 0x04000F88 RID: 3976
	private Command cmdSelect;

	// Token: 0x04000F89 RID: 3977
	private Command cmdOK;

	// Token: 0x04000F8A RID: 3978
	private int xLog;

	// Token: 0x04000F8B RID: 3979
	private int yLog;

	// Token: 0x04000F8C RID: 3980
	public static GameMidlet m;

	// Token: 0x04000F8D RID: 3981
	private int yy = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;

	// Token: 0x04000F8E RID: 3982
	private int freeAreaHeight;

	// Token: 0x04000F8F RID: 3983
	private int xP;

	// Token: 0x04000F90 RID: 3984
	private int yP;

	// Token: 0x04000F91 RID: 3985
	private int wP;

	// Token: 0x04000F92 RID: 3986
	private int hP;

	// Token: 0x04000F93 RID: 3987
	private int t = 20;

	// Token: 0x04000F94 RID: 3988
	private bool isRegistering;

	// Token: 0x04000F95 RID: 3989
	private string passRe = string.Empty;

	// Token: 0x04000F96 RID: 3990
	public bool isFAQ;

	// Token: 0x04000F97 RID: 3991
	private int tipid = -1;

	// Token: 0x04000F98 RID: 3992
	public bool isLogin2;

	// Token: 0x04000F99 RID: 3993
	private int v = 2;

	// Token: 0x04000F9A RID: 3994
	private int g;

	// Token: 0x04000F9B RID: 3995
	private int ylogo = -40;

	// Token: 0x04000F9C RID: 3996
	private int dir = 1;

	// Token: 0x04000F9D RID: 3997
	private Command cmdCallHotline;

	// Token: 0x04000F9E RID: 3998
	public static bool isLoggingIn;

	private Command cmdAutoManager;

	private Command cmdAutoManagerSave;

	private Command cmdAutoManagerPrevPage;

	private Command cmdAutoManagerNextPage;

	private Command cmdAutoManagerToggleAutoSave;

	private Command cmdAutoManagerToggleRemember;

	private Command[] autoManagerDelete;

	private MyVector autoManagerAccounts = new MyVector();

	private bool isAutoManagerOpen;

	private bool autoManagerAutoSave;

	private bool autoManagerRememberLogin;

	private int autoManagerTab;

	private int autoManagerPage;

	private int autoManagerPopupX;

	private int autoManagerPopupY;

	private int autoManagerPopupW;

	private int autoManagerPopupH;
}
