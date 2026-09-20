using System;

// Token: 0x020000A3 RID: 163
public class ChatTextField : IActionListener
{
	// Token: 0x060006D2 RID: 1746 RVA: 0x0005C5C8 File Offset: 0x0005A7C8
	public ChatTextField()
	{
		this.tfChat = new TField();
		this.tfChat.isVietnamese = true;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		this.tfChat.name = "chat";
		if (Main.isWindowsPhone)
		{
			this.tfChat.strInfo = this.tfChat.name;
		}
		this.tfChat.width = GameCanvas.w - 6;
		if (Main.isPC && this.tfChat.width > 250)
		{
			this.tfChat.width = 250;
		}
		this.tfChat.height = mScreen.ITEM_HEIGHT + 2;
		this.tfChat.x = GameCanvas.w / 2 - this.tfChat.width / 2;
		this.tfChat.isFocus = true;
		this.tfChat.setMaxTextLenght(80);
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x0005C6DC File Offset: 0x0005A8DC
	public void initChatTextField()
	{
		this.left = new Command(mResources.OK, this, 8000, null, 1, 1);
		this.right = new Command(mResources.DELETE, this, 8001, null, 1, 1);
		this.center = new Command("Dán", this, 8002, null, 1, 1);

		this.w = 260;
		if (this.w > GameCanvas.w - 10)
		{
			this.w = GameCanvas.w - 10;
		}
		this.h = 82;
		this.x = (GameCanvas.w - this.w) / 2;
		this.y = GameCanvas.h - this.h - 15;
		if (this.y < 10)
		{
			this.y = 10;
		}

		this.tfChat.width = this.w - 20;
		this.tfChat.height = 24;
		this.tfChat.x = this.x + 10;
		this.tfChat.y = this.y + 22;

		int btnW = 68;
		int totalBtnW = btnW * 3;
		int gap = (this.w - totalBtnW) / 4;
		if (gap < 4) gap = 4;
		int btnY = this.y + 50;

		this.left.w = btnW;
		this.left.h = 24;
		this.left.x = this.x + gap;
		this.left.y = btnY;

		this.center.w = btnW;
		this.center.h = 24;
		this.center.x = this.left.x + btnW + gap;
		this.center.y = btnY;

		this.right.w = btnW;
		this.right.h = 24;
		this.right.x = this.center.x + btnW + gap;
		this.right.y = btnY;

		this.cmdChat = new Command();
		ActionChat actionChat = delegate(string str)
		{
			this.tfChat.justReturnFromTextBox = false;
			this.tfChat.setText(str);
			this.parentScreen.onChatFromMe(str, this.to);
			this.tfChat.setText(string.Empty);
			this.right.caption = mResources.CLOSE;
		};
		this.cmdChat.actionChat = actionChat;
		this.cmdChat2 = new Command();
		this.cmdChat2.actionChat = delegate(string str)
		{
			this.tfChat.justReturnFromTextBox = false;
			if (this.parentScreen != null)
			{
				this.tfChat.setText(str);
				this.parentScreen.onChatFromMe(str, this.to);
				this.tfChat.setText(string.Empty);
				this.tfChat.clearKb();
				if (this.right != null)
				{
					this.right.performAction();
				}
			}
			this.isShow = false;
		};
		this.yBegin = this.tfChat.y;
		this.yUp = GameCanvas.h / 2 - 2 * this.tfChat.height;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x000045ED File Offset: 0x000027ED
	public void updateWhenKeyBoardVisible()
	{
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x0005C910 File Offset: 0x0005AB10
	public void keyPressed(int keyCode)
	{
		if (this.isShow)
		{
			this.tfChat.keyPressed(keyCode);
		}
		if (this.tfChat.getText().Equals(string.Empty))
		{
			this.right.caption = mResources.CLOSE;
		}
		else
		{
			this.right.caption = mResources.DELETE;
		}
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x0000759D File Offset: 0x0000579D
	public static ChatTextField gI()
	{
		return (ChatTextField.instance != null) ? ChatTextField.instance : (ChatTextField.instance = new ChatTextField());
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x0005C974 File Offset: 0x0005AB74
	public void startChat(int firstCharacter, IChatable parentScreen, string to)
	{
		this.right.caption = mResources.CLOSE;
		this.to = to;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		this.tfChat.keyPressed(firstCharacter);
		if (!this.tfChat.getText().Equals(string.Empty) && GameCanvas.currentDialog == null)
		{
			this.parentScreen = parentScreen;
			this.isShow = true;
		}
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x0005CA04 File Offset: 0x0005AC04
	public void startChat(IChatable parentScreen, string to)
	{
		this.initChatTextField();
		this.right.caption = mResources.CLOSE;
		this.to = to;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		if (GameCanvas.currentDialog == null)
		{
			this.isShow = true;
			this.tfChat.isFocus = true;
			if (!Main.isPC)
			{
				ipKeyboard.openKeyBoard(this.strChat, ipKeyboard.TEXT, string.Empty, this.cmdChat);
				this.tfChat.setFocusWithKb(true);
			}
		}
		this.tfChat.setText(string.Empty);
		this.tfChat.clearAll();
		this.isPublic = false;
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x0005CAC4 File Offset: 0x0005ACC4
	public void startChat2(IChatable parentScreen, string to)
	{
		this.initChatTextField();
		this.tfChat.setFocusWithKb(true);
		this.to = to;
		this.parentScreen = parentScreen;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		if (GameCanvas.currentDialog == null)
		{
			this.isShow = true;
			if (!Main.isPC)
			{
				ipKeyboard.openKeyBoard(this.strChat, ipKeyboard.TEXT, string.Empty, this.cmdChat2);
				this.tfChat.setFocusWithKb(true);
			}
		}
		this.tfChat.setText(string.Empty);
		this.tfChat.clearAll();
		this.isPublic = false;
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x000045ED File Offset: 0x000027ED
	public void updateKey()
	{
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x0005CB7C File Offset: 0x0005AD7C
	public void update()
	{
		if (!this.isShow)
		{
			return;
		}
		this.tfChat.update();
		if (Main.isWindowsPhone)
		{
			this.updateWhenKeyBoardVisible();
		}
		if (this.tfChat.justReturnFromTextBox)
		{
			this.tfChat.justReturnFromTextBox = false;
			this.parentScreen.onChatFromMe(this.tfChat.getText(), this.to);
			this.tfChat.setText(string.Empty);
			this.right.caption = mResources.CLOSE;
		}
		if (GameCanvas.isPointerJustRelease)
		{
			if (this.center != null && this.center.isPointerPressInside())
			{
				this.center.performAction();
				GameCanvas.isPointerJustRelease = false;
			}
		}
		if (Main.isPC)
		{
			if (GameCanvas.keyPressed[15])
			{
				if (this.left != null && this.tfChat.getText() != string.Empty)
				{
					this.left.performAction();
				}
				GameCanvas.keyPressed[15] = false;
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			}
			if (GameCanvas.keyPressed[14])
			{
				if (this.right != null)
				{
					this.right.performAction();
				}
				GameCanvas.keyPressed[14] = false;
			}
		}
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x000075BE File Offset: 0x000057BE
	public void close()
	{
		this.tfChat.setText(string.Empty);
		this.isShow = false;
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x0005CCA0 File Offset: 0x0005AEA0
	public void paint(mGraphics g)
	{
		if (!this.isShow)
		{
			return;
		}
		if (Main.isIPhone)
		{
			return;
		}
		PopUp.paintPopUp(g, this.x, this.y, this.w, this.h, -1, true);
		mFont.tahoma_7b_green2.drawString(g, this.strChat + this.to, this.x + 10, this.y + 6, 0);
		this.tfChat.paint(g);
		if (this.left != null) this.left.paint(g);
		if (this.center != null) this.center.paint(g);
		if (this.right != null) this.right.paint(g);
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x0005CDAC File Offset: 0x0005AFAC
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 8000:
			Cout.LogError("perform chat 8000");
			if (this.parentScreen != null)
			{
				long num = mSystem.currentTimeMillis();
				if (num - this.lastChatTime < 1000L)
				{
					return;
				}
				this.lastChatTime = num;
				this.parentScreen.onChatFromMe(this.tfChat.getText(), this.to);
				this.tfChat.setText(string.Empty);
				this.right.caption = mResources.CLOSE;
				this.tfChat.clearKb();
			}
			break;
		case 8001:
			Cout.LogError("perform chat 8001");
			if (this.tfChat.getText().Equals(string.Empty))
			{
				this.isShow = false;
				this.parentScreen.onCancelChat();
			}
			this.tfChat.clear();
			break;
		case 8002:
			string clip = UnityEngine.GUIUtility.systemCopyBuffer;
			if (!string.IsNullOrEmpty(clip))
			{
				clip = clip.Trim();
				this.tfChat.insertText(clip);
				if (TField.kb != null)
				{
					TField.kb.text = this.tfChat.getText();
				}
				this.right.caption = mResources.DELETE;
				GameScr.info1.addInfo("Đã dán: " + clip, 0);
			}
			else
			{
				GameScr.info1.addInfo("Clipboard trống!", 0);
			}
			break;
		}
	}

	// Token: 0x04000CA7 RID: 3239
	private static ChatTextField instance;

	// Token: 0x04000CA8 RID: 3240
	public TField tfChat;

	// Token: 0x04000CA9 RID: 3241
	public bool isShow;

	// Token: 0x04000CAA RID: 3242
	public IChatable parentScreen;

	// Token: 0x04000CAB RID: 3243
	private long lastChatTime;

	// Token: 0x04000CAC RID: 3244
	public Command left;

	// Token: 0x04000CAD RID: 3245
	public Command cmdChat;

	// Token: 0x04000CAE RID: 3246
	public Command right;

	// Token: 0x04000CAF RID: 3247
	public Command center;

	// Token: 0x04000CB0 RID: 3248
	private int x;

	// Token: 0x04000CB1 RID: 3249
	private int y;

	// Token: 0x04000CB2 RID: 3250
	private int w;

	// Token: 0x04000CB3 RID: 3251
	private int h;

	// Token: 0x04000CB4 RID: 3252
	private bool isPublic;

	// Token: 0x04000CB5 RID: 3253
	public Command cmdChat2;

	// Token: 0x04000CB6 RID: 3254
	public int yBegin;

	// Token: 0x04000CB7 RID: 3255
	public int yUp;

	// Token: 0x04000CB8 RID: 3256
	public int KC;

	// Token: 0x04000CB9 RID: 3257
	public string to;

	// Token: 0x04000CBA RID: 3258
	public string strChat = "Chat ";
}
