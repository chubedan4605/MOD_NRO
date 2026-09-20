using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class Command
{
	// Token: 0x060002E9 RID: 745 RVA: 0x0001D094 File Offset: 0x0001B294
	public Command(string caption, IActionListener actionListener, int action, object p, int x, int y)
	{
		this.caption = caption;
		this.idAction = action;
		this.actionListener = actionListener;
		this.p = p;
		this.x = x;
		this.y = y;
	}

	// Token: 0x060002EA RID: 746 RVA: 0x0000588C File Offset: 0x00003A8C
	public Command()
	{
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0001D0FC File Offset: 0x0001B2FC
	public Command(string caption, IActionListener actionListener, int action, object p)
	{
		this.caption = caption;
		this.idAction = action;
		this.actionListener = actionListener;
		this.p = p;
	}

	// Token: 0x060002EC RID: 748 RVA: 0x0001D154 File Offset: 0x0001B354
	public Command(string caption, int action, object p)
	{
		this.caption = caption;
		this.idAction = action;
		this.p = p;
	}

	// Token: 0x060002ED RID: 749 RVA: 0x000058BC File Offset: 0x00003ABC
	public Command(string caption, int action)
	{
		this.caption = caption;
		this.idAction = action;
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0001D1A4 File Offset: 0x0001B3A4
	public Command(string caption, int action, int x, int y)
	{
		this.caption = caption;
		this.idAction = action;
		this.x = x;
		this.y = y;
	}

	// Token: 0x060002EF RID: 751 RVA: 0x000058FA File Offset: 0x00003AFA
	public void perform(string str)
	{
		if (this.actionChat != null)
		{
			this.actionChat(str);
		}
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0001D1FC File Offset: 0x0001B3FC
	public void performAction()
	{
		GameCanvas.clearAllPointerEvent();
		Debug.Log("[BUTTON CLICK] idAction=" + this.idAction + ", caption=\"" + (this.caption ?? string.Empty) + "\"");
		if (this.isPlaySoundButton && ((this.caption != null && !this.caption.Equals(string.Empty) && !this.caption.Equals(mResources.saying)) || this.img != null))
		{
			SoundMn.gI().buttonClick();
		}
		if (this.idAction > 0)
		{
			if (this.actionListener != null)
			{
				this.actionListener.perform(this.idAction, this.p);
			}
			else
			{
				GameScr.gI().actionPerform(this.idAction, this.p);
			}
		}
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x00005913 File Offset: 0x00003B13
	public void setType()
	{
		this.type = 1;
		this.w = 160;
		this.hw = 80;
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x0001D2AC File Offset: 0x0001B4AC
	public void paint(mGraphics g)
	{
		if (this.img != null)
		{
			g.drawImage(this.img, this.x, this.y + mGraphics.addYWhenOpenKeyBoard, 0);
			if (this.isFocus)
			{
				if (this.imgFocus == null)
				{
					if (this.cmdClosePanel)
					{
						g.drawImage(ItemMap.imageFlare, this.x + 8, this.y + mGraphics.addYWhenOpenKeyBoard + 8, 3);
					}
					else
					{
						g.drawImage(ItemMap.imageFlare, this.x - ((!this.img.Equals(GameScr.imgMenu)) ? 0 : 10), this.y + mGraphics.addYWhenOpenKeyBoard, 0);
					}
				}
				else
				{
					g.drawImage(this.imgFocus, this.x, this.y + mGraphics.addYWhenOpenKeyBoard, 0);
				}
			}
			if (this.caption != "menu" && this.caption != null)
			{
				if (!this.isFocus)
				{
					mFont.tahoma_7b_dark.drawString(g, this.caption, this.x + mGraphics.getImageWidth(this.img) / 2, this.y + mGraphics.getImageHeight(this.img) / 2 - 5, 2);
				}
				else
				{
					mFont.tahoma_7b_green2.drawString(g, this.caption, this.x + mGraphics.getImageWidth(this.img) / 2, this.y + mGraphics.getImageHeight(this.img) / 2 - 5, 2);
				}
			}
			return;
		}
		if (this.caption != string.Empty)
		{
			if (!this.isFocus)
			{
				Command.paintOngMau(Command.btn0left, Command.btn0mid, Command.btn0right, this.x, this.y, this.w, g);
			}
			else
			{
				Command.paintOngMau(Command.btn1left, Command.btn1mid, Command.btn1right, this.x, this.y, this.w, g);
			}
		}
		int num = 0;
		int num2 = this.x + this.w / 2;
		if (this.imgBtn != null)
		{
			num = this.imgBtn.getWidth();
			num2 = this.x + num;
			if (!this.isFocus)
			{
				g.drawImage(this.imgBtn, this.x, this.y, 0);
			}
			else
			{
				g.drawImage(this.imgBtn, this.x, this.y + 1, 0);
			}
		}
		if (!this.isFocus)
		{
			mFont.tahoma_7b_dark.drawString(g, this.caption, num2, this.y + 7, (num != 0) ? 0 : 2);
		}
		else
		{
			mFont.tahoma_7b_green2.drawString(g, this.caption, num2, this.y + 7, (num != 0) ? 0 : 2);
		}
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x0001D57C File Offset: 0x0001B77C
	public static void paintOngMau(Image img0, Image img1, Image img2, int x, int y, int size, mGraphics g)
	{
		for (int i = 10; i <= size - 20; i += 10)
		{
			g.drawImage(img1, x + i, y, 0);
		}
		int num = size % 10;
		if (num > 0)
		{
			g.drawRegion(img1, 0, 0, num, 24, 0, x + size - 10 - num, y, 0);
		}
		g.drawImage(img0, x, y, 0);
		g.drawImage(img2, x + size - 10, y, 0);
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x0001D5F4 File Offset: 0x0001B7F4
	public bool isPointerPressInside()
	{
		this.isFocus = false;
		if (GameCanvas.isPointerHoldIn(this.x, this.y, this.w, this.h))
		{
			if (GameCanvas.isPointerDown)
			{
				this.isFocus = true;
			}
			if (GameCanvas.isPointerJustRelease)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x0001D648 File Offset: 0x0001B848
	public bool isPointerPressInsideCamera(int cmx, int cmy)
	{
		this.isFocus = false;
		if (GameCanvas.isPointerHoldIn(this.x - cmx, this.y - cmy, this.w, this.h))
		{
			Res.outz("w= " + this.w);
			if (GameCanvas.isPointerDown)
			{
				this.isFocus = true;
			}
			if (GameCanvas.isPointerJustRelease)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x040004D6 RID: 1238
	public bool isDisplay;

	// Token: 0x040004D7 RID: 1239
	public ActionChat actionChat;

	// Token: 0x040004D8 RID: 1240
	public string caption;

	// Token: 0x040004D9 RID: 1241
	public string[] subCaption;

	// Token: 0x040004DA RID: 1242
	public IActionListener actionListener;

	// Token: 0x040004DB RID: 1243
	public int idAction;

	// Token: 0x040004DC RID: 1244
	public bool isPlaySoundButton = true;

	// Token: 0x040004DD RID: 1245
	public Image img;

	// Token: 0x040004DE RID: 1246
	public Image imgFocus;

	// Token: 0x040004DF RID: 1247
	public Image imgBtn;

	// Token: 0x040004E0 RID: 1248
	public int x;

	// Token: 0x040004E1 RID: 1249
	public int y;

	// Token: 0x040004E2 RID: 1250
	public int w = mScreen.cmdW;

	// Token: 0x040004E3 RID: 1251
	public int h = mScreen.cmdH;

	// Token: 0x040004E4 RID: 1252
	public int hw;

	// Token: 0x040004E5 RID: 1253
	private int lenCaption;

	// Token: 0x040004E6 RID: 1254
	public bool isFocus;

	// Token: 0x040004E7 RID: 1255
	public object p;

	// Token: 0x040004E8 RID: 1256
	public int type;

	// Token: 0x040004E9 RID: 1257
	public string caption2 = string.Empty;

	// Token: 0x040004EA RID: 1258
	public static Image btn0left;

	// Token: 0x040004EB RID: 1259
	public static Image btn0mid;

	// Token: 0x040004EC RID: 1260
	public static Image btn0right;

	// Token: 0x040004ED RID: 1261
	public static Image btn1left;

	// Token: 0x040004EE RID: 1262
	public static Image btn1mid;

	// Token: 0x040004EF RID: 1263
	public static Image btn1right;

	// Token: 0x040004F0 RID: 1264
	public bool cmdClosePanel;

	// Token: 0x040004F1 RID: 1265
	public bool isPaintNew;
}
