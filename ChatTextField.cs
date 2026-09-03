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
		this.left = new Command(mResources.OK, this, 8000, null, 1, GameCanvas.h - mScreen.cmdH + 1);
		this.right = new Command(mResources.DELETE, this, 8001, null, GameCanvas.w - 70, GameCanvas.h - mScreen.cmdH + 1);
		this.center = null;
		this.w = this.tfChat.width + 20;
		this.h = this.tfChat.height + 26;
		this.x = GameCanvas.w / 2 - this.w / 2;
		this.y = this.tfChat.y - 18;
		if (Main.isPC && this.w > 320)
		{
			this.w = 320;
		}
		this.left.x = this.x;
		this.right.x = this.x + this.w - 68;
		if (GameCanvas.isTouch)
		{
			this.tfChat.y -= 5;
			this.y -= 20;
			this.h += 30;
			this.left.x = GameCanvas.w / 2 - 68 - 5;
			this.right.x = GameCanvas.w / 2 + 5;
			this.left.y = GameCanvas.h - 30;
			this.right.y = GameCanvas.h - 30;
		}
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
		int num = (!Main.isWindowsPhone) ? (this.y - this.KC) : (this.tfChat.y - 5);
		int num2 = (!Main.isWindowsPhone) ? this.x : 0;
		int num3 = (!Main.isWindowsPhone) ? this.w : GameCanvas.w;
		PopUp.paintPopUp(g, num2, num, num3, this.h, -1, true);
		if (Main.isPC)
		{
			mFont.tahoma_7b_green2.drawString(g, this.strChat + this.to, this.tfChat.x, this.tfChat.y - ((!GameCanvas.isTouch) ? 12 : 17), 0);
			GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
		}
		this.tfChat.paint(g);
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
