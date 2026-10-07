using System;

// Token: 0x020000CA RID: 202
public class TabClanIcon : IActionListener
{
	// Token: 0x06000A3F RID: 2623 RVA: 0x0009A6DC File Offset: 0x000988DC
	public TabClanIcon()
	{
		this.left = new Command(mResources.SELECT, this, 1, null);
		this.right = new Command(mResources.CLOSE, this, 2, null);
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x0009A730 File Offset: 0x00098930
	public void init()
	{
		if (this.isGetName)
		{
			this.w = 170;
			this.h = 118;
			this.x = GameCanvas.w / 2 - this.w / 2;
			this.y = GameCanvas.h / 2 - this.h / 2;
		}
		else
		{
			this.w = 170;
			this.h = 170;
			this.x = GameCanvas.w / 2 - this.w / 2;
			this.y = GameCanvas.h / 2 - this.h / 2;
			if (GameCanvas.h < 240)
			{
				this.y -= 10;
			}
		}
		this.cmx = this.x;
		this.cmtoX = 0;
		if (!this.isRequest)
		{
			this.nItem = ClanImage.vClanImage.size();
		}
		else
		{
			this.nItem = this.vItems.size();
		}
		if (GameCanvas.isTouch)
		{
			this.left.x = this.x;
			this.left.y = this.y + this.h + 5;
			this.right.x = this.x + this.w - 68;
			this.right.y = this.y + this.h + 5;
		}
		TabClanIcon.scrMain = new Scroll();
		TabClanIcon.scrMain.setStyle(this.nItem, this.WIDTH, this.x, this.y + this.disStart, this.w, this.h - this.disStart, true, 1);
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x00008986 File Offset: 0x00006B86
	public void show(bool isGetName)
	{
		if (global::Char.myCharz().clan != null)
		{
			this.isUpdate = true;
		}
		this.isShow = true;
		this.isGetName = isGetName;
		this.init();
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x000089B2 File Offset: 0x00006BB2
	public void showRequest(int msgID)
	{
		this.isShow = true;
		this.isRequest = true;
		this.msgID = msgID;
		this.init();
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x000089CF File Offset: 0x00006BCF
	public void hide()
	{
		this.cmtoX = this.x + this.w;
		SmallImage.clearHastable();
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x000045ED File Offset: 0x000027ED
	public void paintPeans(mGraphics g)
	{
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x0009A8E4 File Offset: 0x00098AE4
	public void paintIcon(mGraphics g)
	{
		g.translate(-this.cmx, 0);
		PopUp.paintPopUp(g, this.x, this.y - 17, this.w, this.h + 17, -1, true);
		mFont.tahoma_7b_dark.drawString(g, mResources.select_clan_icon, this.x + this.w / 2, this.y - 7, 2);
		if (this.lastSelect >= 0 && this.lastSelect <= ClanImage.vClanImage.size() - 1)
		{
			ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect);
			if (clanImage.idImage != null)
			{
				global::Char.myCharz().paintBag(g, clanImage.idImage, GameCanvas.w / 2, this.y + 45, 1, false);
			}
		}
		global::Char.myCharz().paintCharBody(g, GameCanvas.w / 2, this.y + 45, 1, global::Char.myCharz().cf, false);
		g.setClip(this.x, this.y + this.disStart, this.w, this.h - this.disStart - 10);
		if (TabClanIcon.scrMain != null)
		{
			g.translate(0, -TabClanIcon.scrMain.cmy);
		}
		for (int i = 0; i < this.nItem; i++)
		{
			int num = this.x + 10;
			int num2 = this.y + i * this.WIDTH + this.disStart;
			if (num2 + this.WIDTH - ((TabClanIcon.scrMain == null) ? 0 : TabClanIcon.scrMain.cmy) >= this.y + this.disStart && num2 - ((TabClanIcon.scrMain == null) ? 0 : TabClanIcon.scrMain.cmy) <= this.y + this.disStart + this.h)
			{
				ClanImage clanImage2 = (ClanImage)ClanImage.vClanImage.elementAt(i);
				mFont f = mFont.tahoma_7_grey;
				if (i == this.lastSelect)
				{
					f = mFont.tahoma_7_blue;
				}
				if (clanImage2.name != null)
				{
					f.drawString(g, clanImage2.name, num + 20, num2, 0);
				}
				if (clanImage2.xu > 0)
				{
					f.drawString(g, clanImage2.xu + " " + mResources.XU, num + this.w - 20, num2, mFont.RIGHT);
				}
				else if (clanImage2.luong > 0)
				{
					f.drawString(g, clanImage2.luong + " " + mResources.LUONG, num + this.w - 20, num2, mFont.RIGHT);
				}
				if (clanImage2.idImage != null)
				{
					SmallImage.drawSmallImage(g, (int)clanImage2.idImage[0], num, num2, 0, 0);
				}
			}
		}
		g.translate(0, -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x000089E9 File Offset: 0x00006BE9
	public void paint(mGraphics g)
	{
		if (!this.isRequest)
		{
			this.paintIcon(g);
		}
		else
		{
			this.paintPeans(g);
		}
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x0009ABF8 File Offset: 0x00098DF8
	public void update()
	{
		if (TabClanIcon.scrMain != null)
		{
			TabClanIcon.scrMain.updatecm();
		}
		if (this.cmx != this.cmtoX)
		{
			this.cmvx = this.cmtoX - this.cmx << 2;
			this.cmdx += this.cmvx;
			this.cmx += this.cmdx >> 3;
			this.cmdx &= 15;
		}
		if (global::Math.abs(this.cmtoX - this.cmx) < 10)
		{
			this.cmx = this.cmtoX;
		}
		if (this.cmx >= this.x + this.w - 10 && this.cmtoX >= this.x + this.w - 10)
		{
			this.isShow = false;
		}
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x0009ACD8 File Offset: 0x00098ED8
	public void updateKey()
	{
		if (this.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.left)))
		{
			this.left.performAction();
		}
		if (this.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right)))
		{
			this.right.performAction();
		}
		if (this.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.center)))
		{
			this.center.performAction();
		}
		if (!this.isGetName)
		{
			if (TabClanIcon.scrMain == null)
			{
				return;
			}
			if (GameCanvas.isTouch)
			{
				TabClanIcon.scrMain.updateKey();
				this.select = TabClanIcon.scrMain.selectedItem;
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = false;
				this.select--;
				if (this.select < 0)
				{
					this.select = this.nItem - 1;
				}
				TabClanIcon.scrMain.moveTo(this.select * TabClanIcon.scrMain.ITEM_SIZE);
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
				this.select++;
				if (this.select > this.nItem - 1)
				{
					this.select = 0;
				}
				TabClanIcon.scrMain.moveTo(this.select * TabClanIcon.scrMain.ITEM_SIZE);
			}
			if (this.select != -1)
			{
				this.lastSelect = this.select;
			}
		}
		GameCanvas.clearKeyHold();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x0009AEE4 File Offset: 0x000990E4
	public void perform(int idAction, object p)
	{
		if (idAction == 2)
		{
			this.hide();
		}
		if (idAction == 1)
		{
			if (!this.isGetName)
			{
				if (!this.isRequest)
				{
					if (this.lastSelect >= 0)
					{
						this.hide();
						if (global::Char.myCharz().clan == null)
						{
							Service.gI().getClan(2, ((ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect)).ID, this.text);
						}
						else
						{
							Service.gI().getClan(4, ((ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect)).ID, string.Empty);
						}
					}
				}
				else if (this.lastSelect >= 0)
				{
					Item item = (Item)this.vItems.elementAt(this.select);
				}
			}
		}
	}

	// Token: 0x04001330 RID: 4912
	private int x;

	// Token: 0x04001331 RID: 4913
	private int y;

	// Token: 0x04001332 RID: 4914
	private int w;

	// Token: 0x04001333 RID: 4915
	private int h;

	// Token: 0x04001334 RID: 4916
	private Command left;

	// Token: 0x04001335 RID: 4917
	private Command right;

	// Token: 0x04001336 RID: 4918
	private Command center;

	// Token: 0x04001337 RID: 4919
	private int WIDTH = 24;

	// Token: 0x04001338 RID: 4920
	public int nItem;

	// Token: 0x04001339 RID: 4921
	private int disStart = 50;

	// Token: 0x0400133A RID: 4922
	public static Scroll scrMain;

	// Token: 0x0400133B RID: 4923
	public int cmtoX;

	// Token: 0x0400133C RID: 4924
	public int cmx;

	// Token: 0x0400133D RID: 4925
	public int cmvx;

	// Token: 0x0400133E RID: 4926
	public int cmdx;

	// Token: 0x0400133F RID: 4927
	public bool isShow;

	// Token: 0x04001340 RID: 4928
	public bool isGetName;

	// Token: 0x04001341 RID: 4929
	public string text;

	// Token: 0x04001342 RID: 4930
	private bool isRequest;

	// Token: 0x04001343 RID: 4931
	private bool isUpdate;

	// Token: 0x04001344 RID: 4932
	public MyVector vItems = new MyVector();

	// Token: 0x04001345 RID: 4933
	private int msgID;

	// Token: 0x04001346 RID: 4934
	private int select;

	// Token: 0x04001347 RID: 4935
	private int lastSelect;

	// Token: 0x04001348 RID: 4936
	private ScrollResult sr;
}

